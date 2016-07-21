Imports BSMyLoadersLog.LoadersClass
Public Class frmAddShells
    Public FromView As Boolean
    Sub AutoFill()
        Try
            Dim ObjAf As New AutoFillCollections
            txtManu.AutoCompleteCustomSource = ObjAf.List_Case_Manufacturer
            txtName.AutoCompleteCustomSource = ObjAf.List_Case_Name
            txtTTL.AutoCompleteCustomSource = ObjAf.List_Case_Trim_to_length
            txtPrice.AutoCompleteCustomSource = ObjAf.List_Case_Price
        Catch ex As Exception
            Dim ObjFS As New BSFileSystem
            Dim sMessage As String = "frmAddShells.AutoFill" & "::" & Err.Number & "::" & ex.Message.ToString()
            ObjFS.LogFile(MyLogFile, sMessage)
        End Try
    End Sub
    Private Sub frmAddShells_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.List_CalibersTableAdapter.Fill(Me.MLLDataSet.List_Calibers)
        Call AutoFill()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim strManu As String = FluffContent(txtManu.Text)
            Dim strName As String = FluffContent(txtName.Text)
            Dim strTrim As Double = FluffContent(txtTTL.Text, 0)
            Dim intNew As Integer = 0
            Dim intUsed As Integer = nudUsed.Value
            Dim intQty As Integer = nudQty.Value
            Dim dbPrice As Double = FluffContent(txtPrice.Text, 0)
            Dim LngCalID As Long = cmbCal.SelectedValue
            If Not IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(strName, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(strTrim, "Trim to Lenght", Me.Text) Then Exit Sub
            If chkNew.Checked Then intNew = 1
            Dim EstCostPerItem As Double = 0
            If dbPrice <> 0 Then
                EstCostPerItem = (dbPrice / intQty)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "INSERT INTO List_Case (Manufacturer,Name,ttl," & _
                "IsNew,Qty,Price,CID, ePPC,TimesUsed) VALUES('" & strManu & "','" & strName & "','" & strTrim & _
                "'," & intNew & "," & intQty & "," & dbPrice & "," & LngCalID & "," & EstCostPerItem & "," & intUsed & ")"
            Obj.ConnExec(SQL)
            If FromView Then Call frmView_List_Shells.LoadData()
            Me.Close()
        Catch ex As Exception
            Dim ObjFS As New BSFileSystem
            Dim sMessage As String = "frmAddShells.btnAdd.Click" & "::" & Err.Number & "::" & ex.Message.ToString()
            ObjFS.LogFile(MyLogFile, sMessage)
        End Try
    End Sub
End Class