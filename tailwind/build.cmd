@echo off
rem สร้างไฟล์ assets\css\tailwind.css ใหม่ หลังแก้ class tw-* ในหน้าเว็บ
cd /d "%~dp0"
tailwindcss.exe -c tailwind.config.js -i input.css -o ..\test\production-version\assets\css\tailwind.css --minify
