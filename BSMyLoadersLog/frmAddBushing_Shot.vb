Imports BSMyLoadersLog.LoadersClass
Public Class frmAddBushing_Shot
    Sub PreLoadData()
        Try
            Dim ObjAF As New AutoFillCollections
            txtCharge.AutoCompleteCustomSource = ObjAF.List_SG_Bushings_Shot_sCharge
            txtManu.AutoCompleteCustomSource = ObjAF.List_SG_Bushings_Shot_Manufacturer
            txtName.AutoCompleteCustomSource = ObjAF.List_SG_Bushings_Shot_Name
        Catch ex As Exception
            Call LogError(Me.Name, "PreLoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub ClearFields()
        If chkKeepOpen.Checked Then
            Call PreloadData()
        Else
            Me.Close()
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Sub SaveData()
        Try
            Dim Manu As String = FluffContent(txtManu.Text)
            Dim sName As String = FluffContent(txtName.Text)
            Dim sCharge As String = FluffContent(txtCharge.Text)
            Dim sType As String = cmbType.Text

            If Not IsRequired(Manu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(sName, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(sCharge, "Charge Amount", Me.Text) Then Exit Sub
            Dim SQL As String = "INSERT INTO List_SG_Bushing_Shot(Manufacturer,sName,sCharge,sType) VALUES('" & _
                                Manu & "','" & sName & "','" & sCharge & "','" & sType & "')"
            Dim ObjDb As New BSDatabase
            ObjDb.ConnExec(SQL)
            Call ClearFields()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
    Private Sub txtCharge_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCharge.Enter
        txtCharge.SelectAll()
    End Sub

    Private Sub txtManu_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtManu.Enter
        txtManu.SelectAll()
    End Sub

    Private Sub txtName_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.Enter
        txtName.SelectAll()
    End Sub

    Private Sub frmAddBushing_Shot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call PreLoadData()
    End Sub
End Class