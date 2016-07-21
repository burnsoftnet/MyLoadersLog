Public Class frmReport_PowderInventory

    Private Sub frmReport_PowderInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.General_PowderTableAdapter.Fill(Me.MLLDataSet.General_Powder)
        Me.ReportViewer1.RefreshReport()
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Try
            Select Case LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
                Case LCase("All Manufacturer")
                    Me.General_PowderTableAdapter.Fill(Me.MLLDataSet.General_Powder)
                Case LCase("All Weight in Pounds")
                    Me.General_PowderTableAdapter.FillByAllLBS(Me.MLLDataSet.General_Powder)
                Case LCase("All Weight in Grains")
                    Me.General_PowderTableAdapter.FillByAllweightgn(Me.MLLDataSet.General_Powder)
                Case LCase("All Price")
                    Me.General_PowderTableAdapter.FillByAllPrice(Me.MLLDataSet.General_Powder)
                Case LCase("Instock Manufacturer")
                    Me.General_PowderTableAdapter.FillByInManu(Me.MLLDataSet.General_Powder)
                Case LCase("Instock Weight in Pounds")
                    Me.General_PowderTableAdapter.FillByInLBS(Me.MLLDataSet.General_Powder)
                Case LCase("Instock Weight in Grains")
                    Me.General_PowderTableAdapter.FillByIGRNS(Me.MLLDataSet.General_Powder)
                Case LCase("Instock Price")
                    Me.General_PowderTableAdapter.FillByInPrice(Me.MLLDataSet.General_Powder)
                Case LCase("Out-Of-Stock Manufacturer")
                    Me.General_PowderTableAdapter.FillByOutManu(Me.MLLDataSet.General_Powder)
                Case LCase("Out-Of-Stock Weight in Pounds")
                    Me.General_PowderTableAdapter.FillByOutLBS(Me.MLLDataSet.General_Powder)
                Case LCase("Out-Of-Stock Weight in Grains")
                    Me.General_PowderTableAdapter.FillByOutGrns(Me.MLLDataSet.General_Powder)
                Case LCase("Out-Of-Stock Price")
                    Me.General_PowderTableAdapter.FillByOutPrice(Me.MLLDataSet.General_Powder)
                Case Else
                    Me.General_PowderTableAdapter.Fill(Me.MLLDataSet.General_Powder)
            End Select
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "ToolStripComboBox1_SelectedIndexChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class