; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "D-Jetronic Studio"
#define OutputFile "DJetronicStudio"

[Setup]
AppName={#MyAppName}
AppVerName={#MyAppName} 1.1.0.0
AppPublisher=djetronic.org
AppPublisherURL=http://djetronic.org
AppSupportURL=http://djetronic.org
AppUpdatesURL=http://djetronic.org
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=license.txt
DisableStartupPrompt=yes
DisableWelcomePage=no
DisableDirPage=no
DisableProgramGroupPage=no
ChangesAssociations=yes
OutputBaseFilename={#OutputFile}
OutputDir=.
WizardImageFile=InstallerBanner.bmp
WizardImageStretch=yes
WizardSmallImageFile=InstallerSmallLogo.bmp
ArchitecturesInstallIn64BitMode=x64

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop icon"; GroupDescription: "Additional icons:"; Flags: unchecked

[Files]
Source: "..\bin\x64\Release\DJetronicStudio.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\ReleaseNotes.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\DLLs\*.*"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\DLLs\x64\*.*"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\license.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Documentation\*.png"; DestDir: "{app}\Documentation"; Flags: ignoreversion
; mps database
Source: "..\MPS Database\*.mps"; DestDir: "{app}\MPS Database"; Excludes: ".svn"; Flags: ignoreversion
; spice
Source: "..\Spice\*.*"; DestDir: "{app}\Spice"; Excludes: ".svn"; Flags: ignoreversion
; gac
;Source: "..\DLLs\Microsoft.Office.Interop.Word.dll"; DestDir: "{app}"; StrongAssemblyName: "Microsoft.Office.Interop.Word, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c, ProcessorArchitecture=MSIL"; Flags: "gacinstall sharedfile uninsnosharedfileprompt"
; third party licenses
;Source: "..\Licenses\*"; DestDir: "{app}\Licenses"; Excludes: ".svn"; Flags: ignoreversion recursesubdirs createallsubdirs;
; installer support files
Source: "isxdl.dll"; Flags: dontcopy

[INI]
Filename: "{app}\djetronicorg.url"; Section: "InternetShortcut"; Key: "URL"; String: "http://djetronic.org"

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\DJetronicStudio.exe"
Name: "{group}\Release Notes"; Filename: "{app}\ReleaseNotes.txt"
Name: "{group}\{#MyAppName} on the Web"; Filename: "{app}\djetronicorg.url"
Name: "{group}\Uninstall {#MyAppName}"; Filename: "{uninstallexe}"
Name: "{userdesktop}\{#MyAppName}"; Filename: "{app}\DJetronicStudio.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\DJetronicStudio.exe"; Description: "Launch {#MyAppName}"; Flags: nowait postinstall skipifsilent unchecked
Filename: "{app}\ReleaseNotes.txt"; Description: "View the Release Notes"; Flags: nowait postinstall shellexec skipifsilent unchecked
; visual c++ runtime
;Filename: "{tmp}\VC_redist.x86.exe"; Parameters: "/q /norestart"; StatusMsg: "Installing Visual C++ 32-bit Runtime..."; BeforeInstall: SetMarqueeProgress(True);

[Messages]
BeveledLabel=(C) andy@britishideas.com

[UninstallDelete]
Type: files; Name: "{app}\djetronicorg.url"

[Code]
var
 dotnetRedistPath: string;
 downloadNeeded: boolean;
 dotNetNeeded: boolean;

procedure isxdl_AddFile(URL, Filename: PAnsiChar);
external 'isxdl_AddFile@files:isxdl.dll stdcall';
function isxdl_DownloadFiles(hWnd: Integer): Integer;
external 'isxdl_DownloadFiles@files:isxdl.dll stdcall';
function isxdl_SetOption(Option, Value: PAnsiChar): Integer;
external 'isxdl_SetOption@files:isxdl.dll stdcall';

const
  // .NET 4.7.2
  dotnetRedistURL = 'https://download.visualstudio.microsoft.com/download/pr/1f5af042-d0e4-4002-9c59-9ba66bcf15f6/089f837de42708daacaae7c04b7494db/ndp472-kb4054530-x86-x64-allos-enu.exe';

function InitializeSetup(): Boolean;
var
  IsInstalled: Cardinal;
  DotNetReleaseVer: Cardinal;
begin
  Result := true;
  dotNetNeeded := true;
 
  // Check for required netfx installation
  // https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
  // 461808 = .NET 4.7.2
  if(Is64BitInstallMode()) then begin
   if (RegValueExists(HKLM, 'SOFTWARE\Wow6432Node\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install')) then begin
    RegQueryDWordValue(HKLM, 'SOFTWARE\Wow6432Node\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install', IsInstalled);
    RegQueryDWordValue(HKLM, 'SOFTWARE\Wow6432Node\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', DotNetReleaseVer);
    if((IsInstalled = 1) and (DotNetReleaseVer >= 461808)) then begin
     dotNetNeeded := false;
     downloadNeeded := false;
    end;
   end;
  end
  else begin
   if (RegValueExists(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install')) then begin
    RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install', IsInstalled);
    RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', DotNetReleaseVer);
    if((IsInstalled = 1) and (DotNetReleaseVer >= 461808)) then begin
     dotNetNeeded := false;
     downloadNeeded := false;
    end;
   end;
  end;

  if(dotNetNeeded) then begin
   if (not IsAdminLoggedOn()) then begin
    MsgBox('This application needs the Microsoft .NET 4.7.2 Framework to be installed by an Administrator.', mbError, MB_OK);
    Result := false;
   end
   else begin
    dotnetRedistPath := ExpandConstant('{src}\dotnetfx.exe');
    if not FileExists(dotnetRedistPath) then begin
     dotnetRedistPath := ExpandConstant('{tmp}\dotnetfx.exe');
     if not FileExists(dotnetRedistPath) then begin
      isxdl_AddFile(dotnetRedistURL, dotnetRedistPath);
      downloadNeeded := true;
     end;
    end;
   end;
  end;
end;

procedure SetMarqueeProgress(Marquee: Boolean);
begin
  if Marquee then
  begin
    WizardForm.ProgressGauge.Style := npbstMarquee;
  end
    else
  begin
    WizardForm.ProgressGauge.Style := npbstNormal;
  end;
end;
 
procedure InitializeWizard();
begin
end;

function NextButtonClick(CurPage: Integer): Boolean;
var
  hWnd: Integer;
  ResultCode: Integer;
begin
  Result := true;
 
  if CurPage = wpReady then begin
  hWnd := StrToInt(ExpandConstant('{wizardhwnd}'));
 
  // don't try to init isxdl if it's not needed because it will error on < ie 3
  if (downloadNeeded) then begin
   isxdl_SetOption('label', 'Downloading Microsoft .NET 4.7.2 Framework');
   isxdl_SetOption('description', 'This Application needs to install the Microsoft .NET 4.7.2 Framework. Please wait while setup is downloading extra files to your computer.');
   if isxdl_DownloadFiles(hWnd) = 0 then Result := false;
   end;
   if (Result = true) and (dotNetNeeded = true) then begin
    if Exec(ExpandConstant(dotnetRedistPath), '/norestart', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then begin
     // handle success if necessary; ResultCode contains the exit code
     if not (ResultCode = 0) then begin
      Result := false;
     end;
    end
    else begin
     // handle failure if necessary; ResultCode contains the error code
     Result := false;
    end;
   end;
  end;
end;
