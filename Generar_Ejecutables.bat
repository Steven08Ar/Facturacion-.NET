@echo off
echo ========================================================
echo   Generando ejecutables de InvoiceApp (API y Web)
echo ========================================================
echo.

echo 1. Publicando API Backend...
dotnet publish src\InvoiceApp.API -c Release -o Ejecutables\InvoiceApp.API

echo.
echo 2. Publicando Interfaz Web (Blazor)...
dotnet publish src\InvoiceApp.Web\InvoiceApp.Web -c Release -o Ejecutables\InvoiceApp.Web

echo.
echo ========================================================
echo   ¡Listo! Los archivos para iniciar la aplicacion se 
echo   encuentran en la carpeta: 'Ejecutables'
echo ========================================================
pause
