@echo off

::go to parent folder
cd ..

::create nuget_packages
if not exist nuget_packages (
    md nuget_packages
    echo Created nuget_packages folder.
)

::clear nuget_packages
for /R "nuget_packages" %%s in (*) do (
    del "%%s"
)
echo Cleaned up all nuget packages.
echo.

::get nuget key
set /p key=input key:

::start to package all projects
dotnet pack src/Cosmos.Extensions.Prowess -c Release -o nuget_packages
dotnet pack src/Cosmos.Extensions.Dependency.Core -c Release -o nuget_packages
dotnet pack src/Cosmos.Extensions.Autofac -c Release -o nuget_packages
dotnet pack src/Cosmos.Extensions.AspectCoreInjector -c Release -o nuget_packages
dotnet pack src/Cosmos.Extensions.DependencyInjection -c Release -o nuget_packages

for /R "nuget_packages" %%s in (*symbols.nupkg) do (
    del "%%s"
)

echo.
echo.

::set target nuget server url
set source=http://nuget.alexinea.com/api/v2/package

::push nuget packages to server
for /R "nuget_packages" %%s in (*.nupkg) do ( 	
    dotnet nuget push "%%s" -k %key% -s %source% --skip-duplicate
	echo.
)

::get back to build folder
cd build

pause