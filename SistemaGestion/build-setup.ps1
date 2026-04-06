# Publica la app (carpeta completa, self-contained), opcionalmente compila el .iss con Inno Setup 6,
# y siempre genera dist\SistemaGestion_Portable.zip para compartir sin instalador.
# Uso: PowerShell en esta carpeta: .\build-setup.ps1

$ErrorActionPreference = 'Stop'
$ProjectDir = $PSScriptRoot
$PublishOut = Join-Path $ProjectDir 'dist\win-x64-instalador'
$ZipOut = Join-Path $ProjectDir 'dist\SistemaGestion_Portable.zip'

function Get-InnoSetupCompilerPath {
    $direct = @(
        (Join-Path ${env:ProgramFiles(x86)} 'Inno Setup 6\ISCC.exe')
        (Join-Path $env:ProgramFiles 'Inno Setup 6\ISCC.exe')
        (Join-Path $env:LOCALAPPDATA 'Programs\Inno Setup 6\ISCC.exe')
    )
    foreach ($p in $direct) {
        if ($p -and (Test-Path -LiteralPath $p)) { return $p }
    }

    foreach ($root in @(${env:ProgramFiles(x86)}, $env:ProgramFiles)) {
        if (-not $root -or -not (Test-Path -LiteralPath $root)) { continue }
        foreach ($innoDir in Get-ChildItem -LiteralPath $root -Directory -Filter '*Inno*' -ErrorAction SilentlyContinue) {
            $hit = Get-ChildItem -LiteralPath $innoDir.FullName -Filter ISCC.exe -Recurse -Depth 4 -ErrorAction SilentlyContinue |
                Select-Object -First 1
            if ($hit) { return $hit.FullName }
        }
    }

    $uninstallRoots = @(
        'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall'
        'HKLM:\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall'
    )
    foreach ($ur in $uninstallRoots) {
        if (-not (Test-Path -LiteralPath $ur)) { continue }
        foreach ($sub in Get-ChildItem -LiteralPath $ur -ErrorAction SilentlyContinue) {
            $x = Get-ItemProperty $sub.PSPath -ErrorAction SilentlyContinue
            if (-not $x.DisplayName -or $x.DisplayName -notmatch 'Inno Setup') { continue }
            $dir = $x.InstallLocation
            if (-not $dir -and $x.DisplayIcon) {
                $ic = ($x.DisplayIcon -split ',')[0].Trim().Trim('"')
                if ($ic -and (Test-Path -LiteralPath $ic)) { $dir = Split-Path -Parent $ic }
            }
            if ($dir) {
                $exe = Join-Path $dir.TrimEnd('\') 'ISCC.exe'
                if (Test-Path -LiteralPath $exe) { return $exe }
            }
        }
    }

    return $null
}

New-Item -ItemType Directory -Force -Path $PublishOut | Out-Null

Write-Host 'Publicando (win-x64, self-contained, sin single-file)...' -ForegroundColor Cyan
dotnet publish (Join-Path $ProjectDir 'SistemaGestion.csproj') `
    -c Release `
    -r win-x64 `
    --self-contained true `
    -p:PublishSingleFile=false `
    -p:DebugType=none `
    -p:DebugSymbols=false `
    -o $PublishOut

if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host 'Creando ZIP portable...' -ForegroundColor Cyan
if (Test-Path -LiteralPath $ZipOut) { Remove-Item -LiteralPath $ZipOut -Force }
Compress-Archive -Path (Join-Path $PublishOut '*') -DestinationPath $ZipOut -CompressionLevel Optimal

Write-Host "ZIP: $ZipOut" -ForegroundColor Green

$iscc = Get-InnoSetupCompilerPath
if ($iscc) {
    Write-Host "Compilando instalador con: $iscc" -ForegroundColor Cyan
    & $iscc (Join-Path $ProjectDir 'installer\SistemaGestion.iss')
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Instalador: $(Join-Path $ProjectDir 'dist\SistemaGestion_Instalador.exe')" -ForegroundColor Green
    }
    exit $LASTEXITCODE
}

Write-Warning @"
No se encontro ISCC.exe (Inno Setup 6).
Instalalo con: winget install JRSoftware.InnoSetup
Luego volve a ejecutar este script solo para generar dist\SistemaGestion_Instalador.exe
Ya tenes la carpeta publicada y el ZIP portable arriba.
"@
