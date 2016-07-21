Public Class frmReport_PrimerInventory

    Private Sub frmReport_PrimerInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.viewPrimerListTableAdapter.Fill(Me.MLLDataSet.viewPrimerList)
        Me.ReportViewer1.RefreshReport()
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Try
            Select Case LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
                Case LCase("All Manufacturer")
                    Me.viewPrimerListTableAdapter.FillByAllManu(Me.MLLDataSet.viewPrimerList)
                Case LCase("All Primer Type")
                    Me.viewPrimerListTableAdapter.FillByAllPriType(Me.MLLDataSet.viewPrimerList)
                Case LCase("All Price")
                    Me.viewPrimerListTableAdapter.FillByAllPrice(Me.MLLDataSet.viewPrimerList)
                Case LCase("All Qty")
                    Me.viewPrimerListTableAdapter.FillByAllQty(Me.MLLDataSet.viewPrimerList)
                Case LCase("Instock Manufacturer")
                    Me.viewPrimerListTableAdapter.FillByInManu(Me.MLLDataSet.viewPrimerList)
                Case LCase("Instock Primer Type")
                    Me.viewPrimerListTableAdapter.FillByInPriType(Me.MLLDataSet.viewPrimerList)
                Case LCase("Instock Price")
                    Me.viewPrimerListTableAdapter.FillByInPrice(Me.MLLDataSet.viewPrimerList)
                Case LCase("Instock Qty")
                    Me.viewPrimerListTableAdapter.FillByInQty(Me.MLLDataSet.viewPrimerList)
                Case LCase("Out-Of-Stock Manufacturer")
                    Me.viewPrimerListTableAdapter.FillByOutManu(Me.MLLDataSet.viewPrimerList)
                Case LCase("Out-Of-Stock Primer Type")
                    Me.viewPrimerListTableAdapter.FillByOutPriType(Me.MLLDataSet.viewPrimerList)
                Case LCase("Out-Of-Stock Price")
                    Me.viewPrimerListTableAdapter.FillByOutPrice(Me.MLLDataSet.viewPrimerList)
                Case LCase("Out-Of-Stock Qty")
                    Me.viewPrimerListTableAdapter.FillByOutQty(Me.MLLDataSet.viewPrimerList)
                Case Else
                    Me.viewPrimerListTableAdapter.Fill(Me.MLLDataSet.viewPrimerList)
            End Select
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "ToolStripComboBox1_SelectedIndexChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class