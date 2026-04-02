Namespace ViewReports
    Public Class FrmReportCaseInventory
        Sub LoadData()
            Try
                Select Case LCase(ToolStripComboBox1.SelectedItem.ToString)
                    Case LCase("All Manufacturer")
                        List_Case_DetailsTableAdapter.FillByAllManu(MLLDataSet.List_Case_Details)
                    Case LCase("All Caliber")
                        List_Case_DetailsTableAdapter.FillByAllCaliber(MLLDataSet.List_Case_Details)
                    Case LCase("All Trim to Length")
                        List_Case_DetailsTableAdapter.FillByAllTTL(MLLDataSet.List_Case_Details)
                    Case LCase("All Times Used")
                        List_Case_DetailsTableAdapter.FillByAllTimesUsed(MLLDataSet.List_Case_Details)
                    Case LCase("All Qty")
                        List_Case_DetailsTableAdapter.FillByAllQty(MLLDataSet.List_Case_Details)
                    Case LCase("All Price")
                        List_Case_DetailsTableAdapter.FillByAllPrice(MLLDataSet.List_Case_Details)
                    Case LCase("Instock Manufacturer")
                        List_Case_DetailsTableAdapter.FillByINSManu(MLLDataSet.List_Case_Details)
                    Case LCase("Instock Caliber")
                        List_Case_DetailsTableAdapter.FillByINSCaliber(MLLDataSet.List_Case_Details)
                    Case LCase("Instock Trim to Length")
                        List_Case_DetailsTableAdapter.FillByINSTTL(MLLDataSet.List_Case_Details)
                    Case LCase("Instock Times Used")
                        List_Case_DetailsTableAdapter.FillByINSTimesUsed(MLLDataSet.List_Case_Details)
                    Case LCase("Instock Qty")
                        List_Case_DetailsTableAdapter.FillByINSQty(MLLDataSet.List_Case_Details)
                    Case LCase("Instock Price")
                        List_Case_DetailsTableAdapter.FillByINSPrice(MLLDataSet.List_Case_Details)
                    Case LCase("Out-Of-Stock Manufacturer")
                        List_Case_DetailsTableAdapter.FillByOOSManu(MLLDataSet.List_Case_Details)
                    Case LCase("Out-Of-Stock Caliber")
                        List_Case_DetailsTableAdapter.FillByOOSCaliber(MLLDataSet.List_Case_Details)
                    Case LCase("Out-Of-Stock Trim to Length")
                        List_Case_DetailsTableAdapter.FillByOOSTTL(MLLDataSet.List_Case_Details)
                    Case LCase("Out-Of-Stock Times Used")
                        List_Case_DetailsTableAdapter.FillByOOSTimesUsed(MLLDataSet.List_Case_Details)
                    Case LCase("Out-Of-Stock Qty")
                        List_Case_DetailsTableAdapter.FillByOOSQty(MLLDataSet.List_Case_Details)
                    Case LCase("Out-Of-Stock Price")
                        List_Case_DetailsTableAdapter.FillByOOSPrice(MLLDataSet.List_Case_Details)
                    Case Else
                        List_Case_DetailsTableAdapter.Fill(MLLDataSet.List_Case_Details)
                End Select
                ReportViewer1.RefreshReport()
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Private Sub frmReport_CaseInventory_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                List_Case_DetailsTableAdapter.Fill(MLLDataSet.List_Case_Details)
                ReportViewer1.RefreshReport()
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub

        Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
            Call LoadData()
        End Sub
    End Class
End NameSpace