# Publica el instalador en GitHub Releases (mismo tag y nombre que updates/manifest.json).
# Requisito: gh autenticado (`gh auth login`) o variable de entorno GH_TOKEN con permiso repo.
param(
    [string]$Tag = 'v1.2.0',
    [string]$InstallerPath = (Join-Path $PSScriptRoot '..\SistemaGestion\dist\SistemaGestion_Instalador.exe')
)

$ErrorActionPreference = 'Stop'
$InstallerPath = [System.IO.Path]::GetFullPath($InstallerPath)

if (-not (Test-Path -LiteralPath $InstallerPath)) {
    Write-Error "No existe el instalador: $InstallerPath`nCompilá antes con SistemaGestion\build-setup.ps1"
}

$null = gh auth status 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Error "GitHub CLI no está autenticado. Ejecutá: gh auth login`nO definí GH_TOKEN y volvé a correr este script."
}

$repo = 'tomas125/Sistema_Ferreteria'
$null = gh release view $Tag --repo $repo 2>&1
if ($LASTEXITCODE -eq 0) {
    Write-Host "Release $Tag ya existe; subiendo/reemplazando asset..." -ForegroundColor Yellow
    gh release upload $Tag $InstallerPath --repo $repo --clobber
} else {
    gh release create $Tag $InstallerPath `
        --repo $repo `
        --title "Sistema Gestion $($Tag.TrimStart('v'))" `
        --notes "Instalador publicado desde scripts/publish-release.ps1"
}

if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
Write-Host "Listo. Verificá updates/manifest.json (version, downloadUrl, sha256) y hacé push a main." -ForegroundColor Green
