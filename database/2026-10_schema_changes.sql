-- การเปลี่ยนแปลงโครงสร้างฐานข้อมูล DOCDB (ต.ค. 2569) ต้องรันบนฐานข้อมูลเดิมก่อนใช้โค้ดเวอร์ชันนี้
-- รันซ้ำได้ (ตรวจก่อนสร้างทุกคำสั่ง)

-- รหัสผ่านเก็บเป็น hash PBKDF2 (ยาว ~83 ตัว) บัญชีเก่าแปลงอัตโนมัติเมื่อล็อกอิน
ALTER TABLE Signin_Signup ALTER COLUMN Password nvarchar(200) NULL;

-- แชท: ระบุผู้ส่ง และสถานะอ่านแล้ว
IF COL_LENGTH('Talk_TB', 'Sender_Email') IS NULL ALTER TABLE Talk_TB ADD Sender_Email nvarchar(50) NULL;
IF COL_LENGTH('Talk_TB', 'Is_Read') IS NULL ALTER TABLE Talk_TB ADD Is_Read bit NOT NULL CONSTRAINT DF_Talk_TB_Is_Read DEFAULT (0);
GO
UPDATE t SET Sender_Email = CASE WHEN EXISTS (SELECT 1 FROM Signin_Signup s WHERE s.Email = t.Email_AJ AND s.Name = t.Post_Name_RS) THEN t.Email_AJ ELSE t.Email_NS END, Is_Read = 1
FROM Talk_TB t WHERE t.Sender_Email IS NULL;

-- ประเภทโครงงานเพิ่มเติม
INSERT INTO DocTypeTB (TypeID, Doc_Type)
SELECT v.id, v.t FROM (VALUES (5, N'Web Application'), (6, N'Mobile Application'), (7, N'IoT / Embedded'), (8, N'Data Science')) v(id, t)
WHERE NOT EXISTS (SELECT 1 FROM DocTypeTB d WHERE d.Doc_Type = v.t);

-- ส่งเอกสารรายบท + ความเห็นอาจารย์ + กำหนดส่ง
IF OBJECT_ID('Doc_Submission') IS NULL
CREATE TABLE Doc_Submission (
    Sub_ID int IDENTITY(1,1) PRIMARY KEY, DocID int NOT NULL, Chapter nvarchar(50) NOT NULL, Version int NOT NULL,
    File_Name nvarchar(260) NOT NULL, Stored_Name nvarchar(100) NOT NULL, File_Size int NOT NULL, Note nvarchar(500) NULL,
    Uploaded_By nvarchar(50) NULL, Uploaded_Email nvarchar(50) NULL, Uploaded_At datetime NOT NULL DEFAULT GETDATE());
IF OBJECT_ID('Doc_Review') IS NULL
CREATE TABLE Doc_Review (
    Review_ID int IDENTITY(1,1) PRIMARY KEY, Sub_ID int NOT NULL, Reviewer_Email nvarchar(50) NOT NULL, Reviewer_Name nvarchar(50) NULL,
    Verdict nvarchar(20) NOT NULL, Comment nvarchar(1000) NULL, Created_At datetime NOT NULL DEFAULT GETDATE(), Is_Read bit NOT NULL DEFAULT 0);
IF OBJECT_ID('Deadline_TB') IS NULL
CREATE TABLE Deadline_TB (
    Deadline_ID int IDENTITY(1,1) PRIMARY KEY, Class_Year_No nvarchar(20) NOT NULL, Chapter nvarchar(50) NOT NULL,
    Due_Date datetime NOT NULL, Note nvarchar(300) NULL);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Doc_Submission_Doc') CREATE INDEX IX_Doc_Submission_Doc ON Doc_Submission (DocID, Chapter, Version);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Doc_Review_Sub') CREATE INDEX IX_Doc_Review_Sub ON Doc_Review (Sub_ID);
