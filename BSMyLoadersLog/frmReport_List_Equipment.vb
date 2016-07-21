Public Class frmReport_List_Equipment

    Private Sub frmReport_List_Equipment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.General_EquipmentTableAdapter.Fill(Me.MLLDataSet.General_Equipment)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Try
            Select Case LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
                Case LCase("Manufacturer")
                    Me.General_EquipmentTableAdapter.FillByManufacturer(Me.MLLDataSet.General_Equipment)
                Case LCase("Use")
                    Me.General_EquipmentTableAdapter.FillByUse(Me.MLLDataSet.General_Equipment)
                Case LCase("Cost")
                    Me.General_EquipmentTableAdapter.FillByCost(Me.MLLDataSet.General_Equipment)
                Case Else
                    Me.General_EquipmentTableAdapter.Fill(Me.MLLDataSet.General_Equipment)
            End Select
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "ToolStripComboBox1_SelectedIndexChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class