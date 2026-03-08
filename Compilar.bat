@echo off
echo ========================================================
echo   InvoiceApp Enterprise — Generador de Ejecutable WPF
echo ========================================================
echo.
echo Compilando y publicando InvoiceApp...
echo.

dotnet publish src\InvoiceApp.WPF -c Release -r win-x64 --self-contained false -o publish\InvoiceApp

echo.
echo ========================================================
echo   LISTO. El ejecutable se encuentra en:
echo   publish\InvoiceApp\InvoiceApp.exe
echo ========================================================
pause
