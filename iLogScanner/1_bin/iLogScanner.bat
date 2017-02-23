@echo off
rem ------------------------------------------------------------------------------------------
rem iLogScanner.bat
rem  - This is start-up script to Java application for Windows.
rem Copyright(c) Information-technology Promotion Agency, Japan. All rights reserve
rem ------------------------------------------------------------------------------------------

cd /d %~dp0

set CP=.
for %%i in (.\*.jar) do call :setpath %%i
goto :endsubs
:setpath
set CP=%CP%;%1
goto :EOF
:endsubs

set MAIN_CLASS="jp.go.ipa.ilogscanner.ui.StandaloneMain"

java -classpath "%CP%" %MAIN_CLASS% %*
exit /b %ERRORLEVEL%
