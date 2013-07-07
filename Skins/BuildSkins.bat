@ECHO OFF
ECHO Building Skin Files  (WIN/XNA)...

REM Project file passes solution directory as first parameter.
set SKIN_BASE="%1\Skins"
set SKIN_DIR="%1\Skins"
set CONTENT_DIR="%1\Skins\Bin\x86\Content\Skins"
set Z_DIR="%1\Tools\7zip"

ECHO +7ZIP: %Z_DIR%
ECHO +SKIN: %SKIN_BASE%
ECHO +PLAT: %SKIN_DIR%
ECHO +CONT: %CONTENT_DIR%

del %SKIN_DIR%\*.skin

SET SKINS=Default,Green,Blue,Magenta,Purple

FOR %%A in (%SKINS%) do "%Z_DIR%\7za.exe" a -tzip -mx9 -r -x!Addons "%SKIN_DIR%\%%A.skin" "%CONTENT_DIR%\%%A\*.*"



