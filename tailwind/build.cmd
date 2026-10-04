@echo off
rem สร้างไฟล์ website\app\assets\css\tailwind.css ใหม่ หลังแก้ class tw-* ในหน้าเว็บ
cd /d "%~dp0"
tailwindcss.exe -c tailwind.config.js -i input.css -o ..\website\app\assets\css\tailwind.css --minify
