Namespace ViewReports
    ''' <summary>
    ''' Class FrmReportCaseInventory.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmReportCaseInventory
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
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
        ''' <summary>
        ''' Handles the Load event of the frmReport_CaseInventory control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmReport_CaseInventory_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                List_Case_DetailsTableAdapter.Fill(MLLDataSet.List_Case_Details)
                ReportViewer1.RefreshReport()
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the ToolStripComboBox1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
            Call LoadData()
        End Sub
    End Class
End NameSpace