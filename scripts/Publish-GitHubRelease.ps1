# Publica SistemaGestion_Instalador.exe en GitHub Releases.
# Requisito:  gh auth login   (una vez)  o  $env:GH_TOKEN con permiso repo
# Uso (raíz del repo):  .\scripts\Publish-GitHubRelease.ps1

param(
    [string] $Repo = "tomas125/Sistema_Ferreteria"
)

$ErrorActionPreference = 'Stop'
$Root = Split-Path $PSScriptRoot -Parent

$ManifestPath = Join-Path $Root 'updates\manifest.json'
if (-not (Test-Path -LiteralPath $ManifestPath)) { throw "No se encontró updates\manifest.json" }

$manifest = Get-Content -LiteralPath $ManifestPath -Raw | ConvertFrom-Json
$Version = $manifest.version.Trim()
$Tag = "v$Version"

$Exe = Join-Path $Root 'SistemaGestion\dist\SistemaGestion_Instalador.exe'
if (-not (Test-Path -LiteralPath $Exe)) {
    throw "No existe el instalador. Ejecutá: cd SistemaGestion; .\build-setup.ps1`nEsperado: $Exe"
}

$notes = if ($manifest.releaseNotes) { [string]$manifest.releaseNotes } else { "Release $Tag" }

if (-not (Get-Command gh -ErrorAction SilentlyContinue)) {
    throw "Instalá GitHub CLI: winget install GitHub.cli"
}

$hasAuth = $false
if ($env:GH_TOKEN -or $env:GITHUB_TOKEN) { $hasAuth = $true }
else {
    gh auth status 2>$null | Out-Null
    if ($LASTEXITCODE -eq 0) { $hasAuth = $true }
}
if (-not $hasAuth) {
    throw "Ejecutá en una terminal: gh auth login`nO definí `$env:GH_TOKEN con un token (scope repo)."
}

$notesFile = [System.IO.Path]::GetTempFileName()
try {
    [System.IO.File]::WriteAllText($notesFile, $notes, [System.Text.UTF8Encoding]::new($false))
} catch {
    [System.IO.File]::WriteAllText($notesFile, "Release $Tag")
}

gh release view $Tag --repo $Repo 2>$null | Out-Null
if ($LASTEXITCODE -eq 0) {
    Write-Host "Release $Tag ya existe; actualizando asset..." -ForegroundColor Cyan
    gh release upload $Tag $Exe --repo $Repo --clobber
} else {
    gh release create $Tag $Exe --repo $Repo --title "Sistema Ferretería $Tag" --notes-file $notesFile
}

Remove-Item -LiteralPath $notesFile -Force -ErrorAction SilentlyContinue

if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
Write-Host "OK: https://github.com/$Repo/releases/tag/$Tag" -ForegroundColor Green
