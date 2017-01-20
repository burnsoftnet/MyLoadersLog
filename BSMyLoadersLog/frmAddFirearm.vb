Imports BSMyLoadersLog.LoadersClass
Public Class frmAddFirearm
    Public FromView As Boolean
    Sub AutoFill()
        Try
            Dim ObjAF As New AutoFillCollections
        Catch ex As Exception
            Call LogError(Me.Name, "AutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim strManu As String = FluffContent(txtManu.Text)
            Dim strModel As String = FluffContent(txtModel.Text)
            Dim strSerial As String = FluffContent(txtSerial.Text)
            Dim strCal As String = FluffContent(txtCal.Text)
            Dim strBarrel As String = FluffContent(txtBarrel.Text)
            Dim strType As String = FluffContent(txtType.Text)
            Dim MGCID As Integer = 0
            Dim iExclude As Integer = 0
            If chkExlude.Checked Then iExclude = 1

            If Not IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(strModel, "model", Me.Text) Then Exit Sub
            If Not IsRequired(strSerial, "Serial Number", Me.Text) Then Exit Sub
            If Not IsRequired(strCal, "Caliber", Me.Text) Then Exit Sub
            If Not IsRequired(strType, "Type", Me.Text) Then Exit Sub

            Dim strFullName As String = strManu & " " & strModel
            Dim Obj As New BSDatabase
            Dim SQL As String = "INSERT INTO Loaders_Log_Firearms (MGCID,FullName,Manu,Model,Cal,Barrel,SerialNo,GType,exclude)" & _
                        " VALUES (" & MGCID & ",'" & strFullName & "','" & strManu & "','" & _
                        strModel & "','" & strCal & "','" & strBarrel & "','" & strSerial & _
                        "','" & strType & "'" & iExclude & ")"
            Obj.ConnExec(SQL)
            MsgBox(strFullName & " was added to the database!")
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class