Public Class frmEditGuages
    Public UpdatePending As Boolean
    Private Sub frmEditGuages_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.List_SG_GaugeTableAdapter.Fill(Me.MLLDataSet.List_SG_Gauge)
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub ListSGGaugeBindingSource_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles ListSGGaugeBindingSource.ListChanged
        Try
            If Me.MLLDataSet.HasChanges Then
                Me.UpdatePending = True
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "ListSGGaugeBindingSource_ListChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub DataGridView1_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
        Try
            If Me.UpdatePending Then
                Me.List_SG_GaugeTableAdapter.Update(Me.MLLDataSet.List_SG_Gauge)
                Me.UpdatePending = False
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "DataGridView1_RowValidated", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class