Imports System.Security.Cryptography

' เก็บรหัสผ่านแบบ PBKDF2-SHA256 + salt   รูปแบบ: pbkdf2$<รอบ>$<salt base64>$<hash base64>
' บัญชีเก่าที่ยังเป็นข้อความธรรมดาจะตรวจได้ตามเดิม และถูกแปลงเป็น hash เมื่อล็อกอินสำเร็จ (ดู Login.aspx.vb)
Public Module PasswordHasher

    Private Const Prefix As String = "pbkdf2$"
    Private Const Iterations As Integer = 100000

    Public Function Hash(password As String) As String
        Dim salt(15) As Byte
        Using rng = RandomNumberGenerator.Create()
            rng.GetBytes(salt)
        End Using
        Return Prefix & Iterations & "$" & Convert.ToBase64String(salt) & "$" & Convert.ToBase64String(Derive(password, salt, Iterations))
    End Function

    Public Function IsHashed(stored As String) As Boolean
        Return stored IsNot Nothing AndAlso stored.StartsWith(Prefix)
    End Function

    Public Function Verify(password As String, stored As String) As Boolean
        If String.IsNullOrEmpty(stored) OrElse password Is Nothing Then Return False
        If Not IsHashed(stored) Then
            ' บัญชีเก่า: เทียบข้อความธรรมดาแบบใช้เวลาคงที่
            Return FixedTimeEquals(Text.Encoding.UTF8.GetBytes(password), Text.Encoding.UTF8.GetBytes(stored))
        End If
        Dim parts = stored.Split("$"c)
        If parts.Length <> 4 Then Return False
        Dim rounds As Integer
        If Not Integer.TryParse(parts(1), rounds) Then Return False
        Dim salt = Convert.FromBase64String(parts(2))
        Dim expected = Convert.FromBase64String(parts(3))
        Return FixedTimeEquals(Derive(password, salt, rounds), expected)
    End Function

    Private Function Derive(password As String, salt As Byte(), rounds As Integer) As Byte()
        Using kdf As New Rfc2898DeriveBytes(password, salt, rounds, HashAlgorithmName.SHA256)
            Return kdf.GetBytes(32)
        End Using
    End Function

    Private Function FixedTimeEquals(a As Byte(), b As Byte()) As Boolean
        Dim diff As Integer = a.Length Xor b.Length
        For i As Integer = 0 To Math.Min(a.Length, b.Length) - 1
            diff = diff Or (CInt(a(i)) Xor CInt(b(i)))
        Next
        Return diff = 0
    End Function

End Module
