Public Class frmReport_HullInventory

    Private Sub frmReport_HullInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.List_SG_CaseTableAdapter.Fill(Me.MLLDataSet.List_SG_Case)
        Me.ReportViewer1.RefreshReport()
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Try
            Select Case LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
                Case LCase("Manufacturer")
                    Me.List_SG_CaseTableAdapter.FillBy_Manu(Me.MLLDataSet.List_SG_Case)
                Case LCase("Gauge")
                    Me.List_SG_CaseTableAdapter.FillBy_Gauge(Me.MLLDataSet.List_SG_Case)
                Case LCase("Price")
                    Me.List_SG_CaseTableAdapter.FillBy_Price(Me.MLLDataSet.List_SG_Case)
                Case LCase("Qty")
                    Me.List_SG_CaseTableAdapter.FillBy_Qty(Me.MLLDataSet.List_SG_Case)
                Case LCase("Length")
                    Me.List_SG_CaseTableAdapter.FillBy_Length(Me.MLLDataSet.List_SG_Case)
                Case Else
                    Me.List_SG_CaseTableAdapter.Fill(Me.MLLDataSet.List_SG_Case)
            End Select
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "ToolStripComboBox1_SelectedIndexChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class