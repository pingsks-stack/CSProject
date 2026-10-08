Imports System.Configuration
Imports System.Data
Imports System.Globalization
Imports Npgsql
Imports NpgsqlTypes

' ชั้นเชื่อมต่อฐานข้อมูลกลาง (PostgreSQL ผ่าน Npgsql)
'
' - PgDb.Platform() คืนออบเจกต์ที่มีเมธอดชื่อเดียวกับ Zulu.Data เดิม (GetDataTable / GetValue / Execute / NewParam / Close)
'   โค้ดหน้าเดิมจึงเปลี่ยนแค่บรรทัดเปิดการเชื่อมต่อ
' - พารามิเตอร์ทุกตัวส่งเป็นชนิด "unknown" ให้ PostgreSQL อนุมานชนิดจากบริบทเอง
'   (SQL Server แปลง '5' เป็น int ให้อัตโนมัติ โค้ดเดิมจำนวนมากส่ง DocID เป็นข้อความ)
' - ชื่อคอลัมน์ที่ PostgreSQL คืนมาเป็นตัวพิมพ์เล็ก จะถูกเปลี่ยนกลับเป็นชื่อเดิม (DocID, Doc_Name ฯลฯ)
Public Module PgDb

    Private ReadOnly Canonical As Dictionary(Of String, String) = BuildNames()

    Private Function BuildNames() As Dictionary(Of String, String)
        Dim list As String =
        "Action,At,Chapter,Chapters,Class_ID,Class_No,Class_Year_No,Class_name,Codes,Comment,Comment_ID,Comment_Text,Created_At,D,Date_Confirm,Date_Delete,Date" &
        "_Save,Date_Send_NS,Deadline_ID,Detail,DocID,DocID_File,Doc_Date_Save,Doc_File,Doc_File_ID,Doc_File_LogID,Doc_Functions_Code,Doc_List_Code,Doc_List_Cod" &
        "e_Type,Doc_List_File,Doc_List_ID,Doc_List_Log_ID,Doc_List_Type,Doc_Log_ID,Doc_Name,Doc_NameEN,Doc_Type,Doc_dt_Save,Due_Date,Email,Email2,Email_AJ,Emai" &
        "l_NS,FacultyID,File_Name,File_Size,Files,Icon,Invites,IsMine,Is_Read,LastVersion,Last_Email,Last_Mobile,Last_Post_Name,Last_StudentID,LateDays,Mine,Mo" &
        "bile,Mobile2,MySubmit,N,Name,Name_teacherPJ,Note,Partner,Password,Pct,Permission_ID,PermisstionTC_ID,Post_Name,Post_Name_RS,Projects,Review_ID,Reviewe" &
        "r_Email,Reviewer_Name,Sender_Email,Status,StatusMessage,Status_Add,Status_Change,Status_Change1,Status_ID,Stored_Name,Student_ID,Student_ID2,Student_N" &
        "ame,Student_Name2,Student_Status,SubAddAj_ID,SubInfor_ID,SubNPJ_ID,SubNamePJ_ID,SubPermission_ID,Sub_ID,Submit_TC,Submitted,Talk_ID,Teachers,Title,Tot" &
        "al,TypeID,TypeName,Unread,Uploaded_At,Uploaded_By,Uploaded_Email,Url,Urole,Urole_Permis,Urole_TC,Used,UserID,Username,Verdict,Version,Year_ID," &
        "Max,User,Doc_list,Expr1,Expr2,Expr3,Expr4,Doc_File_Log_ID"
        Dim d As New Dictionary(Of String, String)(StringComparer.Ordinal)
        For Each n In list.Split(","c)
            If n <> "" AndAlso Not d.ContainsKey(n.ToLowerInvariant()) Then d(n.ToLowerInvariant()) = n
        Next
        Return d
    End Function

    Public Function ConnectionString() As String
        Return ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
    End Function

    Public Function Open() As NpgsqlConnection
        Dim c As New NpgsqlConnection(ConnectionString())
        c.Open()
        Return c
    End Function

    ' พารามิเตอร์แบบ unknown: ส่งค่าเป็นข้อความให้ PostgreSQL แปลงตามคอลัมน์ที่เทียบ
    Public Function Param(name As String, value As Object) As NpgsqlParameter
        Dim p As New NpgsqlParameter()
        p.ParameterName = name.TrimStart("@"c)
        If value Is Nothing OrElse value Is DBNull.Value Then
            p.Value = DBNull.Value
            p.NpgsqlDbType = NpgsqlDbType.Unknown
            Return p
        End If
        Dim s As String
        If TypeOf value Is DateTime Then
            s = CDate(value).ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)
        ElseIf TypeOf value Is Boolean Then
            s = If(CBool(value), "1", "0")
        ElseIf TypeOf value Is IFormattable Then
            s = CType(value, IFormattable).ToString(Nothing, CultureInfo.InvariantCulture)
        Else
            s = value.ToString()
        End If
        p.Value = s
        p.NpgsqlDbType = NpgsqlDbType.Unknown
        Return p
    End Function

    ' ทำพารามิเตอร์ที่สร้างจากที่อื่น (เช่น SqlDataSource) ให้เป็นแบบ unknown
    Public Sub Normalize(p As NpgsqlParameter)
        Dim v As Object = p.Value
        Dim fixedP = Param(p.ParameterName, v)
        p.NpgsqlDbType = NpgsqlDbType.Unknown
        p.Value = fixedP.Value
    End Sub

    ' เปลี่ยนชื่อคอลัมน์ตัวพิมพ์เล็กกลับเป็นชื่อเดิม
    Public Sub FixColumnNames(dt As DataTable)
        For Each c As DataColumn In dt.Columns
            Dim n As String = Nothing
            If Canonical.TryGetValue(c.ColumnName.ToLowerInvariant(), n) AndAlso n <> c.ColumnName Then c.ColumnName = n
        Next
    End Sub

    ' ชื่อคอลัมน์ตามรูปแบบเดิม (ถ้าไม่รู้จักก็ใช้ชื่อที่ PostgreSQL ส่งมา)
    Public Function CanonicalName(name As String) As String
        Dim n As String = Nothing
        If Canonical.TryGetValue(name.ToLowerInvariant(), n) Then Return n
        Return name
    End Function

    ' สร้าง DataTable จาก reader โดยตั้งชื่อคอลัมน์ให้ถูกตั้งแต่แรก
    ' (ถ้าเปลี่ยนชื่อหลัง Fill แล้ว DataTable.Merge จะมองว่าเป็นคอลัมน์ใหม่ ทำให้ข้อมูลหาย)
    Public Function Query(sql As String, ParamArray ps As NpgsqlParameter()) As DataTable
        Using c = Open()
            Using cmd As New NpgsqlCommand(sql, c)
                cmd.Parameters.AddRange(ps)
                Dim dt As New DataTable()
                Using rd = cmd.ExecuteReader()
                    For i As Integer = 0 To rd.FieldCount - 1
                        Dim baseName As String = CanonicalName(rd.GetName(i))
                        Dim colName As String = baseName
                        Dim k As Integer = 1
                        While dt.Columns.Contains(colName)
                            colName = baseName & k
                            k += 1
                        End While
                        dt.Columns.Add(colName, rd.GetFieldType(i))
                    Next
                    Dim vals(rd.FieldCount - 1) As Object
                    dt.BeginLoadData()
                    While rd.Read()
                        rd.GetValues(vals)
                        dt.Rows.Add(vals)
                    End While
                    dt.EndLoadData()
                End Using
                dt.AcceptChanges()
                Return dt
            End Using
        End Using
    End Function

    Public Function Scalar(sql As String, ParamArray ps As NpgsqlParameter()) As Object
        Using c = Open()
            Using cmd As New NpgsqlCommand(sql, c)
                cmd.Parameters.AddRange(ps)
                Dim v = cmd.ExecuteScalar()
                Return If(v Is Nothing, DBNull.Value, v)
            End Using
        End Using
    End Function

    Public Function Exec(sql As String, ParamArray ps As NpgsqlParameter()) As Integer
        Using c = Open()
            Using cmd As New NpgsqlCommand(sql, c)
                cmd.Parameters.AddRange(ps)
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    ' ใช้แทน Zulu.Data.PlatformFactory.GetPlatform(...) ในหน้าเดิม
    Public Function Platform() As LegacyPlatform
        Return New LegacyPlatform()
    End Function

    ' บันทึก error ของฐานข้อมูลลง App_Data\sql-errors.log (ไว้ตรวจ SQL ที่ยังแปลงไม่ครบ)
    Public Sub LogError(sql As String, ex As Exception)
        Try
            Dim path As String = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/sql-errors.log")
            System.IO.File.AppendAllText(path, DateTime.Now.ToString("s") & " " & System.Web.HttpContext.Current.Request.RawUrl & Environment.NewLine &
                                  "  " & ex.Message & Environment.NewLine & "  SQL: " & sql.Replace(Environment.NewLine, " ") & Environment.NewLine)
        Catch
        End Try
    End Sub

    ' แทน Zulu.Data: Zulu ไม่โยน error แต่เก็บไว้ให้เรียกถามด้วย IsError / GetErrorMessage
    ' จึงทำแบบเดียวกันเพื่อให้หน้าเดิมทำงานเหมือนเดิม (และบันทึก error ไว้ใน log)
    Public Class LegacyPlatform
        Private lastError As String

        Private Sub Record(sql As String, ex As Exception)
            lastError = ex.Message
            PgDb.LogError(sql, ex)
        End Sub

        Public Function NewParam(name As String, value As Object) As NpgsqlParameter
            Return PgDb.Param(name, value)
        End Function
        Public Function GetDataTable(sql As String, ParamArray ps As NpgsqlParameter()) As DataTable
            Try
                Return PgDb.Query(sql, ps)
            Catch ex As NpgsqlException
                Record(sql, ex)
                Return New DataTable()
            End Try
        End Function
        ' คืนค่าคอลัมน์แรกของแถวแรก (Nothing ถ้าไม่มีแถว) เหมือน Zulu
        Public Function GetValue(sql As String, ParamArray ps As NpgsqlParameter()) As Object
            Try
                Dim v = PgDb.Scalar(sql, ps)
                If v Is DBNull.Value Then Return Nothing
                If TypeOf v Is Long Then Return CInt(v) ' COUNT(*) ของ PostgreSQL เป็น bigint
                Return v
            Catch ex As NpgsqlException
                Record(sql, ex)
                Return Nothing
            End Try
        End Function
        Public Function Execute(sql As String, ParamArray ps As NpgsqlParameter()) As Integer
            Try
                Return PgDb.Exec(sql, ps)
            Catch ex As NpgsqlException
                Record(sql, ex)
                Return 0
            End Try
        End Function
        Public Function IsError(Optional ByRef err As Object = Nothing) As Boolean
            err = lastError
            Return lastError IsNot Nothing
        End Function
        Public Function GetErrorMessage() As String
            Return If(lastError, "")
        End Function
        Public Sub Open()
        End Sub
        Public Sub Close()
        End Sub
    End Class

End Module
