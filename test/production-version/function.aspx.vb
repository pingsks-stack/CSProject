
Imports System.Data.SqlClient
Imports DevExpress.Web
Imports DevExpress.Web.Bootstrap

Partial Class production_version_function
    Inherits System.Web.UI.Page


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim DocID = Request.QueryString("DocID")
        Dim save1 = db.GetValue("INSERT INTO Doc_List_TB (Doc_List_Code, DocID, Doc_Date_Save, Doc_List_Type, Doc_Functions_Code) VALUES (@Doc_List_Code, @DocID, GETDATE(), @Doc_List_Type, @Doc_Functions_Code); SELECT SCOPE_IDENTITY()",
                db.NewParam("Doc_List_Code", TextArea1.Value),
                db.NewParam("Doc_Functions_Code", TextBox2.Text),
                db.NewParam("Doc_List_Type", TextBox1.Text),
                db.NewParam("DocID", DocID))


        Dim docListId As Integer = Convert.ToInt32(save1)
        Dim docDateSave As DateTime = DateTime.Now
        Dim postName As String = Session("Name").ToString()
        Dim action As String = "เพิ่ม"

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString)
            connection.Open()
            Using command As New SqlCommand("INSERT INTO Doc_List_Log (DocID, Doc_List_ID, Doc_List_Type, Doc_Functions_Code, Doc_Date_Save, Post_Name, Action) VALUES (@DocID, @Doc_List_ID, @Doc_List_Type, @Doc_Functions_Code, @Doc_Date_Save, @Post_Name, @Action)", connection)
                command.Parameters.AddWithValue("@DocID", DocID)
                command.Parameters.AddWithValue("@Doc_List_ID", docListId)
                command.Parameters.AddWithValue("@Doc_List_Type", TextBox1.Text)
                command.Parameters.AddWithValue("@Doc_Functions_Code", TextBox2.Text)
                command.Parameters.AddWithValue("@Doc_Date_Save", docDateSave)
                command.Parameters.AddWithValue("@Post_Name", postName)
                command.Parameters.AddWithValue("@Action", action)
                command.ExecuteNonQuery()
            End Using
        End Using

        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('เพิ่มข้อมูลสำเร็จ !');window.location.href='Main_Doc.aspx';", True)
        End If

        db.Close()
    End Sub


    Public Class LanguageDetection
        Private Shared Function GetKeywordMap() As Dictionary(Of String, String)
            Dim keywordMap As New Dictionary(Of String, String)()
            keywordMap.Add("VB.NET", "Function,Sub,Class,Dim,Private,Public,End,If,Then,Else,For,Each,Next,While")
            keywordMap.Add("C#", "namespace,class,static,void,int,string,if,else,for,foreach,while")
            keywordMap.Add("JavaScript", "function,var,let,const,if,else,for,foreach,while")
            keywordMap.Add("Python", "def,class,if,else,for,while,return")
            keywordMap.Add("Java", "public,class,void,int,String,if,else,for,while")
            keywordMap.Add("PHP", "function,class,public,private,if,else,for,while,return,echo")

            Return keywordMap
        End Function



        Public Shared Function DetectProgrammingLanguage(code As String) As String
            Dim keywordMap As Dictionary(Of String, String) = GetKeywordMap()
            Dim detectedLanguage As String = ""
            Dim maxMatchCount As Integer = 0

            For Each language As String In keywordMap.Keys
                Dim keywords As String = ""
                keywordMap.TryGetValue(language, keywords)

                If Not String.IsNullOrEmpty(keywords) Then
                    Dim keywordArray As String() = keywords.Split(",")

                    Dim count As Integer = 0

                    For Each keyword As String In keywordArray
                        If Not String.IsNullOrEmpty(keyword) AndAlso Not String.IsNullOrEmpty(code) AndAlso code.Contains(keyword.Trim()) Then
                            count += 1
                        End If
                    Next

                    If count > maxMatchCount Then
                        detectedLanguage = language
                        maxMatchCount = count
                    End If
                End If
            Next

            If Not String.IsNullOrEmpty(detectedLanguage) Then
                Return detectedLanguage
            Else
                Return "ไม่พบภาษาโปรแกรม"
            End If
        End Function


    End Class


    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Dim code As String = TextArea1.Value
        Dim languages As String = LanguageDetection.DetectProgrammingLanguage(code)
        TextBox1.Text = languages
    End Sub


    ' หน้าซอร์สโค้ดเก่า ย้ายไปใช้ Code.aspx / CodeEdit.aspx
    Protected Sub RedirectToNewCode(sender As Object, e As EventArgs) Handles Me.PreInit
        Response.Redirect("CodeEdit.aspx?DocID=" & HttpUtility.UrlEncode(If(Request.QueryString("DocID"), "")), True)
    End Sub
End Class
