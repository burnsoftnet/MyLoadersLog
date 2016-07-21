Public Class frmReport_CaseInventory
    Sub LoadData()
        Try
            Select Case LCase(ToolStripComboBox1.SelectedItem.ToString)
                Case LCase("All Manufacturer")
                    Me.List_Case_DetailsTableAdapter.FillByAllManu(Me.MLLDataSet.List_Case_Details)
                Case LCase("All Caliber")
                    Me.List_Case_DetailsTableAdapter.FillByAllCaliber(Me.MLLDataSet.List_Case_Details)
                Case LCase("All Trim to Length")
                    Me.List_Case_DetailsTableAdapter.FillByAllTTL(Me.MLLDataSet.List_Case_Details)
                Case LCase("All Times Used")
                    Me.List_Case_DetailsTableAdapter.FillByAllTimesUsed(Me.MLLDataSet.List_Case_Details)
                Case LCase("All Qty")
                    Me.List_Case_DetailsTableAdapter.FillByAllQty(Me.MLLDataSet.List_Case_Details)
                Case LCase("All Price")
                    Me.List_Case_DetailsTableAdapter.FillByAllPrice(Me.MLLDataSet.List_Case_Details)
                Case LCase("Instock Manufacturer")
                    Me.List_Case_DetailsTableAdapter.FillByINSManu(Me.MLLDataSet.List_Case_Details)
                Case LCase("Instock Caliber")
                    Me.List_Case_DetailsTableAdapter.FillByINSCaliber(Me.MLLDataSet.List_Case_Details)
                Case LCase("Instock Trim to Length")
                    Me.List_Case_DetailsTableAdapter.FillByINSTTL(Me.MLLDataSet.List_Case_Details)
                Case LCase("Instock Times Used")
                    Me.List_Case_DetailsTableAdapter.FillByINSTimesUsed(Me.MLLDataSet.List_Case_Details)
                Case LCase("Instock Qty")
                    Me.List_Case_DetailsTableAdapter.FillByINSQty(Me.MLLDataSet.List_Case_Details)
                Case LCase("Instock Price")
                    Me.List_Case_DetailsTableAdapter.FillByINSPrice(Me.MLLDataSet.List_Case_Details)
                Case LCase("Out-Of-Stock Manufacturer")
                    Me.List_Case_DetailsTableAdapter.FillByOOSManu(Me.MLLDataSet.List_Case_Details)
                Case LCase("Out-Of-Stock Caliber")
                    Me.List_Case_DetailsTableAdapter.FillByOOSCaliber(Me.MLLDataSet.List_Case_Details)
                Case LCase("Out-Of-Stock Trim to Length")
                    Me.List_Case_DetailsTableAdapter.FillByOOSTTL(Me.MLLDataSet.List_Case_Details)
                Case LCase("Out-Of-Stock Times Used")
                    Me.List_Case_DetailsTableAdapter.FillByOOSTimesUsed(Me.MLLDataSet.List_Case_Details)
                Case LCase("Out-Of-Stock Qty")
                    Me.List_Case_DetailsTableAdapter.FillByOOSQty(Me.MLLDataSet.List_Case_Details)
                Case LCase("Out-Of-Stock Price")
                    Me.List_Case_DetailsTableAdapter.FillByOOSPrice(Me.MLLDataSet.List_Case_Details)
                Case Else
                    Me.List_Case_DetailsTableAdapter.Fill(Me.MLLDataSet.List_Case_Details)
            End Select
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmReport_CaseInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.List_Case_DetailsTableAdapter.Fill(Me.MLLDataSet.List_Case_Details)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadData()
    End Sub
End Class