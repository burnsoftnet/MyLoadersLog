Imports BSMyLoadersLog.LoadersClass
Public Class frmAddCaliberToCollection
    Private Sub frmAddCaliberToCollection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ObjAF As New AutoFillCollections
        txtCal.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtCal.AutoCompleteCustomSource = ObjAF.General_Calibers
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim strCal As String = FluffContent(txtCal.Text)
            If Not IsRequired(strCal, "Caliber", Me.Text) Then Exit Sub
            Dim Obj As New BSDatabase
            Dim SQL As String = "INSERT INTO List_Calibers(CAL) VALUES('" & strCal & "')"
            Obj.ConnExec(SQL)
            MDIParentMain.RefreshCalData()
            If Not chkKeep.Checked Then
                Me.Close()
            Else
                txtCal.Text = ""
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class