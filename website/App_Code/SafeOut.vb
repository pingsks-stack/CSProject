Imports System.Web

' ใช้ตอนเอาค่าจากฐานข้อมูล/URL ไปใส่ในสตริง JavaScript ของหน้า .aspx กันหน้าพังและ XSS
'   ใน attribute เช่น onclick="f('<%# SafeOut.JsAttr(Eval("Doc_Name")) %>')"
'   ใน <script>     เช่น var id = '<%= SafeOut.Js(Request.QueryString("DocID")) %>';
Public Module SafeOut

    Public Function Js(value As Object) As String
        Return HttpUtility.JavaScriptStringEncode(Convert.ToString(value))
    End Function

    Public Function JsAttr(value As Object) As String
        Return HttpUtility.HtmlAttributeEncode(Js(value))
    End Function

End Module
