@ECHO OFF
ECHO Building Skin Files  (360)...

REM Project file passes solution directory as first parameter.
set SKIN_BASE="%1\Skins"
set SKIN_DIR="%1\Skins\360"
set Z_DIR="%1\Tools\7zip"

ECHO +7ZIP: %Z_DIR%
ECHO +SKIN: %SKIN_BASE%
ECHO +PLAT: %SKIN_DIR%

del %SKIN_DIR%\*.skin

%Z_DIR%\7za.exe a -tzip -mx9 -r -x!Addons "%SKIN_DIR%\Default.skin" "%SKIN_BASE%\Content\Skins\Default\*.*"
%Z_DIR%\7za.exe a -tzip -mx9 -r -x!Addons "%SKIN_DIR%\Green.skin" "%SKIN_BASE%\Content\Skins\Green\*.*"

