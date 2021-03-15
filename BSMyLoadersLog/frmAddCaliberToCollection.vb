Imports BSMyLoadersLog.LoadersClass
Public Class FrmAddCaliberToCollection
    Private Sub frmAddCaliberToCollection_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim objAf As New AutoFillCollections
        txtCal.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtCal.AutoCompleteCustomSource = objAf.General_Calibers
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Dim strCal As String = FluffContent(txtCal.Text)
            If Not IsRequired(strCal, "Caliber", Text) Then Exit Sub
            Dim obj As New BSDatabase
            Dim sql As String = "INSERT INTO List_Calibers(CAL) VALUES('" & strCal & "')"
            obj.ConnExec(sql)
            MDIParentMain.RefreshCalData()
            If Not chkKeep.Checked Then
                Close()
            Else
                txtCal.Text = ""
            End If
        Catch ex As Exception
            Call LogError(Name, "btnAdd_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class