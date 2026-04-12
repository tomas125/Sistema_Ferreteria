; Requiere Inno Setup 6: https://jrsoftware.org/isdl.php
; Compilar: ejecutar build-setup.ps1 desde la carpeta SistemaGestion (publica y llama a ISCC)

#define MyAppName "Sistema Gestion Ferreteria"
#define MyAppExeName "SistemaGestion.exe"
#define MyAppVersion "1.0.9"
#define PublishDir "..\dist\win-x64-instalador"

[Setup]
AppId={{7C4A9E31-2B0D-4F6A-9E1C-8D5F3A2B1C00}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher=CMR
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputDir=..\dist
OutputBaseFilename=SistemaGestion_Instalador
SetupIconFile=..\IMAGENES\logo.ico
; Icono en "Agregar o quitar programas"
UninstallDisplayIcon={app}\IMAGENES\logo.ico
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#PublishDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
; IconFilename: fuerza el logo en el acceso directo (el .exe a veces muestra icono genérico en el escritorio)
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\IMAGENES\logo.ico"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\IMAGENES\logo.ico"; Tasks: desktopicon

[Run]
; Sin skipifsilent: tras /VERYSILENT (actualización por internet) Inno vuelve a lanzar el programa.
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#MyAppName}}"; Flags: nowait postinstall
