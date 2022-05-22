@echo off
rem # // -------------------------------------------------------------------------------------------------
rem # // <copyright file=".RunTestWithCoverage.cmd" company="Osc@rNET SOFTware">
rem # //
rem # //          Copyright (c) Óscar Fernández González a.k.a. Osc@rNET and Contributors.
rem # //
rem # //          This open source software is licensed under the MIT License.
rem # //          Please review the LICENSE file for more information.
rem # //
rem # //          All the relevant licenses for third party software and tools
rem # //          that have been made possible this open source software are
rem # //          mentioned in the 3DPARTYLICENSES file.
rem # //
rem # // </copyright>
rem # // -------------------------------------------------------------------------------------------------
rem
rem The 'RunTestWithCoverage.cmd' file allows to test and generate code coverage reports.
rem
color 0b
cls
echo Osc@rNET SOFTware LOBA-Framework .::. Tests and generates code coverage reports.
echo.
echo Copyright (c) Oscar Fernandez Gonzalez a.k.a. Osc@rNET and Contributors.
echo This open source software is licensed under the MIT License.
echo.
echo _________________________________________________________________________________
echo.

rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
rem + SETS VARIABLES AND PERFORMS OTHER INITIAL TASKS
rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

set BaseDir=%~dp0
set SourceDir=%BaseDir%..\Source
set TestsDir=%BaseDir%..\Tests
set CoverageExclude=[xunit.*]*,[OscarNET.LOBAFramework.LanguageResources*]OscarNET.LOBAFramework.*Resources*,[OscarNET.LOBAFramework.Testing]*
set CoverageReports=%BaseDir%BuildReports\Coverage\OscarNET.LOBAFramework.Configuration.Tests\coverage.cobertura.xml
set DotNetConfiguration=Debug
set DotNetFramework=net6.0
set GotoLabel=END

if not exist %BaseDir%BuildReports\ mkdir %BaseDir%BuildReports
if not exist %BaseDir%BuildReports\Coverage\ mkdir %BaseDir%BuildReports\Coverage
if not exist %BaseDir%BuildReports\History\ mkdir %BaseDir%BuildReports\History
if not exist %BaseDir%BuildReports\UnitTests\ mkdir %BaseDir%BuildReports\UnitTests

<nul set /p ="[+] Cleaning old report folders . . .   "
dir %BaseDir%BuildReports /s /b /o:n /ad | for /f "tokens=*" %%a in ('findstr "\\Coverage$"') do @rmdir /s /q %%a
dir %BaseDir%BuildReports /s /b /o:n /ad | for /f "tokens=*" %%a in ('findstr "\\UnitTests$"') do @rmdir /s /q %%a
echo Ok!

rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
rem + COMPILES THE SOLUTION
rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

set GotoLabel=COMPILE_SOLUTION
goto CLEAN_BIN_OBJ_FOLDERS
:COMPILE_SOLUTION
echo.
echo [+] Compiling the solution . . .
dotnet build --configuration %DotNetConfiguration% --nologo --verbosity quiet %BaseDir%..\
if %ERRORLEVEL% neq 0 (
  set GotoLabel=RESTORE_SOLUTION
  goto CLEAN_BIN_OBJ_FOLDERS
)

rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
rem + TESTS AND RECOLETS THE CODE COVERAGE DATA
rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

echo.
echo [+] Testing and getting code coverage data . . .
echo.

dotnet test %BaseDir%..\Tests\OscarNET.LOBAFramework.Configuration.Tests\OscarNET.LOBAFramework.Configuration.Tests.csproj ^
            --configuration %DotNetConfiguration% ^
            --framework %DotNetFramework% ^
            --logger "trx;LogFileName=OscarNET.LOBAFramework.Configuration.Tests.trx" ^
            --logger "xunit;LogFileName=OscarNET.LOBAFramework.Configuration.Tests.xml" ^
            --no-build ^
            --nologo ^
            --results-directory %BaseDir%BuildReports\UnitTests ^
            --verbosity quiet ^
            /p:CollectCoverage=true ^
            /p:CoverletOutput=%BaseDir%BuildReports\Coverage\OscarNET.LOBAFramework.Configuration.Tests\ ^
            /p:CoverletOutputFormat=cobertura ^
            /p:Exclude=\"%CoverageExclude%\"

if %ERRORLEVEL% neq 0 (
  set GotoLabel=RESTORE_SOLUTION
  goto CLEAN_BIN_OBJ_FOLDERS
)

rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
rem + GENERATES CODE COVERAGE REPORTS
rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

echo.
echo.
echo [+] Generating code coverage reports ...
echo.
echo.

dotnet %UserProfile%\.nuget\packages\reportgenerator\5.1.9\tools\net6.0\ReportGenerator.dll ^
       "-reports:%CoverageReports%" ^
       "-sourcedirs:%BaseDir%..\Source" ^
       "-targetdir:%BaseDir%BuildReports\Coverage" ^
       "-historydir:%BaseDir%BuildReports\History" ^
       -reporttypes:HTML;HTMLSummary

if %ERRORLEVEL% neq 0 (
  set GotoLabel=RESTORE_SOLUTION
  goto CLEAN_BIN_OBJ_FOLDERS
)

rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
rem + ALLOWS TO SHOW THE CODE COVERAGE REPORTS (in default browser)
rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

echo.
echo.
echo [+] Showing code coverage reports ...
echo.
echo.

start %BaseDir%BuildReports\Coverage\index.htm

set GotoLabel=RESTORE_SOLUTION
goto CLEAN_BIN_OBJ_FOLDERS

rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
rem + RESTORE SOLUTION (SUBROUTINE)
rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

:RESTORE_SOLUTION
echo [+] Restoring solution . . .
echo.
dotnet restore --configfile %BaseDir%..\.nuget\nuget.config --verbosity minimal %BaseDir%..\
goto END

rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
rem + CLEANS BIN AND OBJ FOLDER (SUBROUTINE)
rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

:CLEAN_BIN_OBJ_FOLDERS
<nul set /p ="[+] Cleaning BIN folders . . .   "
dir %SourceDir% /s /b /o:n /ad | for /f "tokens=*" %%a in ('findstr "\\bin$"') do @rmdir /s /q %%a
dir %TestsDir% /s /b /o:n /ad | for /f "tokens=*" %%a in ('findstr "\\bin$"') do @rmdir /s /q %%a
echo Ok!

<nul set /p ="[+] Cleaning OBJ folders . . .   "
dir %SourceDir% /s /b /o:n /ad | for /f "tokens=*" %%a in ('findstr "\\obj$"') do @rmdir /s /q %%a
dir %TestsDir% /s /b /o:n /ad | for /f "tokens=*" %%a in ('findstr "\\obj$"') do @rmdir /s /q %%a
echo Ok!

goto %GotoLabel%

rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
rem + THE END
rem ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

:END
set BaseDir=
set SourceDir=
set TestsDir=
set CoverageOutputFormat=
set CoverageReports=
set DotNetConfiguration=
set DotNetFramework=
set GotoLabel=
echo.
echo.
echo.
pause
color
cls