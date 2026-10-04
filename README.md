# CS Project — ระบบติดตามโครงงาน มหาวิทยาลัยพะเยา

เว็บ ASP.NET Web Forms (VB.NET) สำหรับนิสิต อาจารย์ และผู้ดูแลระบบ ใช้ส่งเอกสารโครงงานรายบท
เชิญอาจารย์ที่ปรึกษา/กรรมการ ให้ความเห็น พิจารณาผ่าน และเก็บเป็นคลังโครงงาน/ซอร์สโค้ดสำหรับรุ่นต่อไป

## โครงสร้างโฟลเดอร์

```
CSProject/
├─ website/            เว็บไซต์ (เปิดเป็น Web Site ใน Visual Studio หรือรันด้วย IIS Express)
│  ├─ app/             หน้าเว็บทั้งหมด (URL: /app/...)
│  │  ├─ assets/       CSS/JS/รูป ของ template + tailwind.css + ui.js
│  │  ├─ Docs/         ไฟล์ที่อัปโหลดแบบเดิม (ไม่เก็บใน git)
│  │  └─ Fonts/        ฟอนต์สำหรับสร้าง PDF ใบยืนยัน
│  ├─ App_Code/        โค้ดกลาง (สิทธิ์, รหัสผ่าน, แจ้งเตือน, dashboard, ส่งเอกสารรายบท ฯลฯ)
│  ├─ App_Data/        ไฟล์เอกสารรายบท (submissions/) — IIS ไม่เปิดให้ดาวน์โหลดตรง
│  ├─ Bin/             DLL ที่ใช้ (Zulu, iTextSharp ฯลฯ)
│  ├─ Web.config
│  └─ CSProject.sln
├─ database/           ไฟล์ฐานข้อมูลเริ่มต้น (.bacpac) และสคริปต์เปลี่ยนโครงสร้าง (.sql)
├─ tailwind/           ตั้งค่า Tailwind CSS (build.cmd สร้าง website/app/assets/css/tailwind.css)
└─ README.md
```

## วิธีรันบนเครื่อง

1. ติดตั้ง **IIS Express 10**, **SQL Server LocalDB** และ **DevExpress v21.1** (ใช้ในหน้าเดิมบางหน้า)
2. นำเข้าฐานข้อมูล
   ```
   sqlpackage /Action:Import /SourceFile:database\DOCDB_2023-10-28.bacpac /TargetConnectionString:"Server=(localdb)\MSSQLLocalDB;Database=DOCDB;Integrated Security=True"
   ```
   แล้วรัน `database/2026-10_schema_changes.sql` กับฐานข้อมูล DOCDB
3. รันเว็บ
   ```
   "C:\Program Files\IIS Express\iisexpress.exe" /path:"<โฟลเดอร์นี้>\website" /port:8080
   ```
4. เปิด http://localhost:8080/app/Login.aspx

## บัญชีทดสอบ (เฉพาะเครื่องพัฒนา)

ทุกบัญชีใช้รหัสผ่าน `Demo@1234`

| บทบาท | Username | หน้าแรก |
|---|---|---|
| นิสิต | demo_student | dashboard |
| อาจารย์ | demo_teacher | dashboard_Teacher |
| ผู้ดูแลระบบ | demo_admin | dashboard_Admin |

## แก้หน้าตา (Tailwind)

class ของ Tailwind ใช้ prefix `tw-` (กันชนกับ Bootstrap ของ DevExpress) สีอยู่ในตัวแปร `--c-*` ของ `tailwind/input.css`
หลังแก้ class ในหน้าเว็บ ให้ดับเบิลคลิก `tailwind/build.cmd`
(ต้องมี `tailwind/tailwindcss.exe` — ดาวน์โหลด `tailwindcss-windows-x64.exe` v3.4.19 จาก GitHub ของ Tailwind แล้วเปลี่ยนชื่อ)
