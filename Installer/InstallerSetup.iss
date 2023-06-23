; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Tinfoil Resource Downloader"
#define AppVersion GetVersionNumbersString("C:\Users\Ashton\Documents\GitHub\Tinfoil-Resource-Downloader\bin\Release\Tinfoil Resource Downloader.exe")
#define MyAppPublisher "AKAS Gaming"
#define MyAppExeName "Tinfoil Resource Downloader.exe"
#define MyAppIcoName "C:\Users\Ashton\Documents\GitHub\Tinfoil-Resource-Downloader\assets\Logo.ico"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{14E9AD90-DAC0-49C9-BA6F-2C3AF85017B4}
AppName={#MyAppName}
AppVerName={#MyAppName} {#AppVersion}
VersionInfoVersion={#AppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
OutputDir=C:\Users\Ashton\Documents\GitHub\Tinfoil-Resource-Downloader\Installers
OutputBaseFilename=TRD-{#AppVersion}
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Ashton\Documents\GitHub\Tinfoil-Resource-Downloader\bin\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Ashton\Documents\GitHub\Tinfoil-Resource-Downloader\bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
