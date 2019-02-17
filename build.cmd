@echo off
cls

IF EXIST "paket.lock" (
  .paket\paket.exe restore
) ELSE (
  .paket\paket.exe install
)

if errorlevel 1 (
  exit /b %errorlevel%
)
dotnet restore build.proj
dotnet fake build %*
