Imports Microsoft.VisualBasic

Public Class EncodeBase64

    Function encode(ByVal data As String) As String
        Dim bytesToEncode As Byte() = Encoding.UTF8.GetBytes(data)
        Dim encodedText As String = Convert.ToBase64String(bytesToEncode)
        Return encodedText
    End Function
    Function decode(ByVal data As String) As String
        Try
            Dim decodedBytes As Byte() = Convert.FromBase64String(data)
            Dim decodedText As String = Encoding.UTF8.GetString(decodedBytes)
            Return decodedText
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function KeyDay() As String
        Dim key = Now.Month & Now.Year & Now.Day
        Return key
    End Function
    Function getEncode(ByVal data As String) As String
        Return encode(data & "@" & KeyDay())
    End Function
    Function getDecode(ByVal data As String) As String
        Try
            Dim ret = decode(data)
            Dim str() = ret.Split("@")
            If str.Length <> 2 AndAlso str(1) <> KeyDay() Then
                Return ""
            Else
                Return str(0)
            End If
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
End Class
