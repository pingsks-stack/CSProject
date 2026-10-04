
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web

Partial Class production_version_EditUploadfile
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim docID As String = Request.QueryString("DocID")
            Dim permissionID As String = Request.QueryString("Permission_ID")

        End If
    End Sub

    Public ReadOnly Property docID() As String
        Get
            If ViewState("docID") IsNot Nothing Then
                Return ViewState("docID").ToString()
            Else
                ' ตั้งค่าค่าเริ่มต้นหรือการจัดการเมื่อไม่มีค่า ViewState("docID")
                Return String.Empty ' หรือค่าอื่นที่คุณต้องการ
            End If
        End Get
    End Property








End Class
