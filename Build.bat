@REM %systemroot%\Microsoft.Net\Framework\v4.0.30319\MSBuild.exe build.proj /t:%*

set pathMSBuild="C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin"
set pathProjectFile="C:\Projects\DocumentArchiver4\DocumentArchiver4.sln"
@echo off
cls
cd %pathMSBuild%
msbuild.exe %pathProjectFile% /p:configuration=release 

pause
@REM pause

