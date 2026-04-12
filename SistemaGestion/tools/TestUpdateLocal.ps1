# Servidor HTTP local para probar "Buscar actualización" sin subir nada a GitHub.
# Uso (desde la carpeta SistemaGestion, en PowerShell):
#   .\tools\TestUpdateLocal.ps1
# Luego ejecutá SistemaGestion.exe desde bin\Release\net8.0-windows y pulsá "Buscar actualización".
# Ctrl+C en esta ventana restaura update-config.json y detiene el servidor.

$ErrorActionPreference = 'Stop'

$Port = 8765
$Base = "http://127.0.0.1:$Port"
$ProjectRoot = Split-Path -Parent $PSScriptRoot
$AppDir = Join-Path $ProjectRoot 'bin\Release\net8.0-windows'
$ConfigPath = Join-Path $AppDir 'update-config.json'
$BackupPath = Join-Path $AppDir 'update-config.json.before-local-test'

$InstallerPath = if ($args.Count -ge 1 -and $args[0]) { $args[0] } else {
    Join-Path $AppDir 'SistemaGestion.exe'
}

if (-not (Test-Path -LiteralPath $InstallerPath)) {
    Write-Error "No se encontró el archivo para simular la descarga: $InstallerPath`nCompilá antes: dotnet build -c Release"
}

if (-not (Test-Path -LiteralPath $AppDir)) {
    Write-Error "No existe la carpeta de salida: $AppDir`nEjecutá: dotnet build -c Release"
}

if (Test-Path -LiteralPath $ConfigPath) {
    Copy-Item -LiteralPath $ConfigPath -Destination $BackupPath -Force
}

@{
    manifestUrl = "$Base/manifest.json"
} | ConvertTo-Json | Set-Content -LiteralPath $ConfigPath -Encoding UTF8

$downloadUrl = "$Base/paquete-instalador.exe"
$manifestObj = [ordered]@{
    version      = '9.9.9'
    downloadUrl  = $downloadUrl
    sha256       = ''
    releaseNotes = 'Prueba local (TestUpdateLocal.ps1). Es una copia del .exe actual solo para probar la descarga.'
}
$manifestJson = $manifestObj | ConvertTo-Json -Compress

$installerBytes = [System.IO.File]::ReadAllBytes($InstallerPath)

$listener = [System.Net.HttpListener]::new()
$listener.Prefixes.Add("$Base/")
try {
    $listener.Start()
}
catch {
    Write-Error "No se pudo abrir $Base. Probar otro puerto o ejecutar: netsh http add urlacl url=$Base/ user=$env:USERNAME"
}

Write-Host "Servidor de prueba: $Base" -ForegroundColor Green
Write-Host "Manifiesto: $Base/manifest.json (version 9.9.9 > tu build)" -ForegroundColor Cyan
Write-Host "Paquete:    $downloadUrl" -ForegroundColor Cyan
Write-Host "Config escrito en: $ConfigPath" -ForegroundColor Yellow
Write-Host ""
Write-Host "Ahora abrí: $AppDir\SistemaGestion.exe y pulsá Buscar actualización." -ForegroundColor White
Write-Host "Ctrl+C aquí para detener el servidor y restaurar update-config." -ForegroundColor DarkGray

try {
    while ($listener.IsListening) {
        $ctx = $listener.GetContext()
        $req = $ctx.Request
        $res = $ctx.Response
        $path = $req.Url.AbsolutePath.TrimEnd('/')
        if ($path -eq '' -or $path -eq '/') { $path = '/' }

        try {
            if ($path -eq '/manifest.json') {
                $buf = [System.Text.Encoding]::UTF8.GetBytes($manifestJson)
                $res.ContentType = 'application/json; charset=utf-8'
                $res.ContentLength64 = $buf.LongLength
                $res.OutputStream.Write($buf, 0, $buf.Length)
            }
            elseif ($path -eq '/paquete-instalador.exe') {
                $res.ContentType = 'application/octet-stream'
                $res.ContentLength64 = $installerBytes.LongLength
                $res.OutputStream.Write($installerBytes, 0, $installerBytes.Length)
            }
            else {
                $res.StatusCode = 404
                $msg = [Text.Encoding]::UTF8.GetBytes('Not found')
                $res.OutputStream.Write($msg, 0, $msg.Length)
            }
        }
        finally {
            $res.OutputStream.Close()
        }
    }
}
finally {
    if ($null -ne $listener -and $listener.IsListening) { $listener.Stop() }
    if ($null -ne $listener) { $listener.Close() }
    if (Test-Path -LiteralPath $BackupPath) {
        Copy-Item -LiteralPath $BackupPath -Destination $ConfigPath -Force
        Remove-Item -LiteralPath $BackupPath -Force
        Write-Host "update-config.json restaurado." -ForegroundColor Green
    }
}
