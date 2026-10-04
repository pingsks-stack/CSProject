
Imports System.IO
Imports System.Collections.Generic
Imports DevExpress.XtraRichEdit.Layout

Partial Class production_version_EditUploadfile
    Inherits System.Web.UI.Page



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


    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Dim code As String = TextArea1.Value
        Dim languages As String = LanguageDetection.DetectProgrammingLanguage(code)
        TextBox1.Text = languages
    End Sub






    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)

    'End Sub
End Class
