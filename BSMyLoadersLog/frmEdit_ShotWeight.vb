Public Class frmEdit_ShotWeight
    Public UpdatePending As Boolean
    Private Sub frmEdit_ShotWeight_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.List_SG_ShotWeightTableAdapter.Fill(Me.MLLDataSet.List_SG_ShotWeight)
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub ListSGShotWeightBindingSource_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListSGShotWeightBindingSource.CurrentChanged
        Try
            If Me.MLLDataSet.HasChanges Then
                Me.UpdatePending = True
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "ListSGShotWeightBindingSource_CurrentChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub DataGridView1_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
        Try
            If Me.UpdatePending Then
                Me.List_SG_ShotWeightTableAdapter.Update(Me.MLLDataSet.List_SG_ShotWeight)
                Me.UpdatePending = False
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "DataGridView1_RowValidated", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class