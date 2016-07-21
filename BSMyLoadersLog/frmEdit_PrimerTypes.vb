Public Class frmEdit_PrimerTypes
    Public UpdatePending As Boolean
    Private Sub frmEdit_PrimerTypes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.General_Primer_TypeTableAdapter.Fill(Me.MLLDataSet.General_Primer_Type)
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub GeneralPrimerTypeBindingSource_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles GeneralPrimerTypeBindingSource.ListChanged
        Try
            If Me.MLLDataSet.HasChanges Then
                Me.UpdatePending = True
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "GeneralPrimerTypeBindingSource_ListChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub DataGridView1_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
        Try
            If Me.UpdatePending Then
                Me.General_Primer_TypeTableAdapter.Update(Me.MLLDataSet.General_Primer_Type)
                Me.UpdatePending = False
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "GeneralPrimerTypeBindingSource_ListChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class