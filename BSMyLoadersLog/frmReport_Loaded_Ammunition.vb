Imports Microsoft.Reporting.WinForms
Public Class frmReport_Loaded_Ammunition

    Private Sub frmReport_Loaded_Ammunition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Loaders_Log_AmmunitionTableAdapter.Fill(Me.MLLDataSet.Loaders_Log_Ammunition)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub frmReport_Loaded_Ammunition_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Height <> 0 Then
            Me.ReportViewer1.Height = Me.Height - (46 + 15)
            Me.ReportViewer1.Width = Me.Width - 5
        End If
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Try
            Select Case LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
                Case LCase("Manufacturer")
                    Me.Loaders_Log_AmmunitionTableAdapter.FillByOrderbyManName(Me.MLLDataSet.Loaders_Log_Ammunition)
                Case LCase("Caliber")
                    Me.Loaders_Log_AmmunitionTableAdapter.FillByOrderByCal(Me.MLLDataSet.Loaders_Log_Ammunition)
                Case LCase("Grains")
                    Me.Loaders_Log_AmmunitionTableAdapter.FillByOrderByGrains(Me.MLLDataSet.Loaders_Log_Ammunition)
                Case LCase("Qty.")
                    Me.Loaders_Log_AmmunitionTableAdapter.FillByOrderByQty(Me.MLLDataSet.Loaders_Log_Ammunition)
                Case Else
                    Me.Loaders_Log_AmmunitionTableAdapter.Fill(Me.MLLDataSet.Loaders_Log_Ammunition)
            End Select
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "ToolStripComboBox1_SelectedIndexChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class