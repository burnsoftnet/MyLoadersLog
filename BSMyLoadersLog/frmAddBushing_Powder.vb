Imports BSMyLoadersLog.LoadersClass
Public Class frmAddBushing_Powder
    Sub PreloadData()
        Try
            Dim Objaf As New AutoFillCollections
            txtManu.AutoCompleteCustomSource = Objaf.List_SG_Bushings_Powder_Manufacturer
            txtName.AutoCompleteCustomSource = Objaf.List_SG_Bushings_Powder_Name
            txtCharge.AutoCompleteCustomSource = Objaf.List_SG_Bushings_Powder_sCharge
            txtPowderName.AutoCompleteCustomSource = Objaf.List_SG_Bushings_Powder_Powder
        Catch ex As Exception
            Call LogError(Me.Name, "PreloadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddChargeBushing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Call PreloadData()
        Catch ex As Exception
            Call LogError(Me.Name, "load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Sub ClearFields()
        If chkKeepOpen.Checked Then
            Call PreloadData()
        Else
            Me.Close()
        End If
    End Sub
    Sub SaveData()
        Try
            Dim Manu As String = FluffContent(txtManu.Text)
            Dim sName As String = FluffContent(txtName.Text)
            Dim sCharge As String = FluffContent(txtCharge.Text)
            Dim sType As String = cmbType.Text
            Dim PowderName As String = FluffContent(txtPowderName.Text)
            
            If Not IsRequired(Manu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(sName, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(sCharge, "Charge Amount", Me.Text) Then Exit Sub
            Dim SQL As String = "INSERT INTO List_SG_Bushing_Powder(Manufacturer,sName,sCharge,sType,PowderName) VALUES('" & _
                                Manu & "','" & sName & "','" & sCharge & "','" & sType & "','" & PowderName & "')"
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

    Private Sub txtPowderName_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPowderName.Enter
        txtPowderName.SelectAll()
    End Sub
End Class