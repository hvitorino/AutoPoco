xbuild AutoPoco.sln /property:Configuration=Debug
mono ./_Libs/NUnit/nunit-console.exe ./AutoPoco.Tests.Unit/bin/Debug/AutoPoco.Tests.Unit.dll
mono ./_Libs/NUnit/nunit-console.exe ./AutoPoco.Tests.Integration/bin/Debug/AutoPoco.Tests.Integration.dll

