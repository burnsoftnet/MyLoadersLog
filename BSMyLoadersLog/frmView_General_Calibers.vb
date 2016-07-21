Public Class frmView_General_Calibers
    Public UpdatePending As Boolean
    Private Sub frmView_General_Calibers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.General_CalibersTableAdapter.Fill(Me.MLLDataSet.General_Calibers)
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub GeneralCalibersBindingSource_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles GeneralCalibersBindingSource.ListChanged
        Try
            If Me.MLLDataSet.HasChanges Then
                Me.UpdatePending = True
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "GeneralCalibersBindingSource_ListChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub DataGridView1_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
        Try
            If Me.UpdatePending Then
                Me.General_CalibersTableAdapter.Update(Me.MLLDataSet.General_Calibers)
                Me.UpdatePending = False
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "DataGridView1_RowValidated", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class