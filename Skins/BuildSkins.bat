ECHO Building Skin Files...

del ..\..\Skins\*.skin

..\..\Tools\7zip\7za.exe a -tzip -mx9 -r -x!Addons "..\..\Skins\Default.skin" ".\Bin\x86\Content\Skins\Default\*.*"
..\..\Tools\7zip\7za.exe a -tzip -mx9 -r -x!Addons "..\..\Skins\Green.skin" ".\Bin\x86\Content\Skins\Green\*.*"
..\..\Tools\7zip\7za.exe a -tzip -mx9 -r -x!Addons "..\..\Skins\Blue.skin" ".\Bin\x86\Content\Skins\Blue\*.*"
..\..\Tools\7zip\7za.exe a -tzip -mx9 -r -x!Addons "..\..\Skins\Magenta.skin" ".\Bin\x86\Content\Skins\Magenta\*.*"
..\..\Tools\7zip\7za.exe a -tzip -mx9 -r -x!Addons "..\..\Skins\Purple.skin" ".\Bin\x86\Content\Skins\Purple\*.*"



