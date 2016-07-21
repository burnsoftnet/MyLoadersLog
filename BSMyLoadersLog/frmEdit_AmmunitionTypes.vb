Public Class frmEdit_AmmunitionTypes
    Public UpdatePending As Boolean
    Private Sub frmEdit_AmmunitionTypes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.General_Ammunition_TypeTableAdapter.Fill(Me.MLLDataSet.General_Ammunition_Type)
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub GeneralAmmunitionTypeBindingSource_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles GeneralAmmunitionTypeBindingSource.ListChanged
        Try
            If Me.MLLDataSet.HasChanges Then
                Me.UpdatePending = True
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "GeneralAmmunitionTypeBindingSource_ListChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub DataGridView1_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
        Try
            If Me.UpdatePending Then
                Me.General_Ammunition_TypeTableAdapter.Update(Me.MLLDataSet.General_Ammunition_Type)
                Me.UpdatePending = False
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "DataGridView1_RowValidated", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class