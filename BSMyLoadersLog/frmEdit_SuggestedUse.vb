Public Class frmEdit_SuggestedUse
    Public UpdatePending As Boolean
    Sub LoadData()
        Try
            Me.General_Suggested_UseTableAdapter.Fill(Me.MLLDataSet.General_Suggested_Use)
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmEdit_SuggestedUse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
    Private Sub GeneralSuggestedUseBindingSource_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles GeneralSuggestedUseBindingSource.ListChanged
        Try
            If Me.MLLDataSet.HasChanges Then
                Me.UpdatePending = True
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "GeneralSuggestedUseBindingSource_ListChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub DataGridView1_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
        Try
            If Me.UpdatePending Then
                Me.General_Suggested_UseTableAdapter.Update(Me.MLLDataSet.General_Suggested_Use)
                Me.UpdatePending = False
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "DataGridView1_RowValidated", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class