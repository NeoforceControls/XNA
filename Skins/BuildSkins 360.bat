ECHO Building Skin Files 360...

del ..\..\..\..\Skins\360\*.skin

..\..\..\..\Tools\7zip\7za.exe a -tzip -mx9 -r -x!Addons "..\..\..\..\Skins\360\Default.skin" ".\Content\Skins\Default\*.*"
..\..\..\..\Tools\7zip\7za.exe a -tzip -mx9 -r -x!Addons "..\..\..\..\Skins\360\Green.skin" ".\Content\Skins\Green\*.*"

