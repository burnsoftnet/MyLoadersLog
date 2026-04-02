Namespace ViewReports
    ''' <summary>
    ''' Class FrmReportBulletInventory.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmReportBulletInventory
        Sub LoadData()
            Try
                Select Case LCase(ToolStripComboBox1.SelectedItem.ToString)
                    Case LCase("All Manufacturer")
                        List_Bullets_DetailsTableAdapter.FillByAllManu(MLLDataSet.List_Bullets_Details)
                    Case LCase("All Caliber")
                        List_Bullets_DetailsTableAdapter.FillByAllCalibers(MLLDataSet.List_Bullets_Details)
                    Case LCase("All Diameter")
                        List_Bullets_DetailsTableAdapter.FillByAllDiameter(MLLDataSet.List_Bullets_Details)
                    Case LCase("All Weight")
                        List_Bullets_DetailsTableAdapter.FillByAllWeight(MLLDataSet.List_Bullets_Details)
                    Case LCase("All Section Density")
                        List_Bullets_DetailsTableAdapter.FillByAllSectionDensity(MLLDataSet.List_Bullets_Details)
                    Case LCase("All Part Number")
                        List_Bullets_DetailsTableAdapter.FillByAllPart_number(MLLDataSet.List_Bullets_Details)
                    Case LCase("All Ballistic Coefficient")
                        List_Bullets_DetailsTableAdapter.FillByAllBallistic_Coefficient(MLLDataSet.List_Bullets_Details)
                    Case LCase("All Firearm Type")
                        List_Bullets_DetailsTableAdapter.FillByAllFirearmType(MLLDataSet.List_Bullets_Details)
                    Case LCase("All Qty.")
                        List_Bullets_DetailsTableAdapter.FillByAllQty(MLLDataSet.List_Bullets_Details)
                    Case LCase("All Price")
                        List_Bullets_DetailsTableAdapter.FillByAllPrice(MLLDataSet.List_Bullets_Details)
                    Case LCase("Instock Manufacturer")
                        List_Bullets_DetailsTableAdapter.FillByINSManu(MLLDataSet.List_Bullets_Details)
                    Case LCase("Instock Caliber")
                        List_Bullets_DetailsTableAdapter.FillByINSCalibers(MLLDataSet.List_Bullets_Details)
                    Case LCase("Instock Diameter")
                        List_Bullets_DetailsTableAdapter.FillByINSDiameter(MLLDataSet.List_Bullets_Details)
                    Case LCase("Instock Weight")
                        List_Bullets_DetailsTableAdapter.FillByINSWeight(MLLDataSet.List_Bullets_Details)
                    Case LCase("Instock Section Density")
                        List_Bullets_DetailsTableAdapter.FillByINSSectionDensity(MLLDataSet.List_Bullets_Details)
                    Case LCase("Instock Part Number")
                        List_Bullets_DetailsTableAdapter.FillByINSPart_number(MLLDataSet.List_Bullets_Details)
                    Case LCase("Instock Ballistic Coefficient")
                        List_Bullets_DetailsTableAdapter.FillByINSBallistic_Coefficient(MLLDataSet.List_Bullets_Details)
                    Case LCase("Instock Firearm Type")
                        List_Bullets_DetailsTableAdapter.FillByINSFirearmType(MLLDataSet.List_Bullets_Details)
                    Case LCase("Instock Qty.")
                        List_Bullets_DetailsTableAdapter.FillByINSQty(MLLDataSet.List_Bullets_Details)
                    Case LCase("Instock Price")
                        List_Bullets_DetailsTableAdapter.FillByINSPrice(MLLDataSet.List_Bullets_Details)
                    Case LCase("Out-Of-Stock Manufacturer")
                        List_Bullets_DetailsTableAdapter.FillByOOSManu(MLLDataSet.List_Bullets_Details)
                    Case LCase("Out-Of-Stock Caliber")
                        List_Bullets_DetailsTableAdapter.FillByOOSCalibers(MLLDataSet.List_Bullets_Details)
                    Case LCase("Out-Of-Stock Diameter")
                        List_Bullets_DetailsTableAdapter.FillByOOSDiameter(MLLDataSet.List_Bullets_Details)
                    Case LCase("Out-Of-Stock Weight")
                        List_Bullets_DetailsTableAdapter.FillByOOSWeight(MLLDataSet.List_Bullets_Details)
                    Case LCase("Out-Of-Stock Section Density")
                        List_Bullets_DetailsTableAdapter.FillByOOSSectionDensity(MLLDataSet.List_Bullets_Details)
                    Case LCase("Out-Of-Stock Part Number")
                        List_Bullets_DetailsTableAdapter.FillByOOSPart_number(MLLDataSet.List_Bullets_Details)
                    Case LCase("Out-Of-Stock Ballistic Coefficient")
                        List_Bullets_DetailsTableAdapter.FillByOOSBallistic_Coefficient(MLLDataSet.List_Bullets_Details)
                    Case LCase("Out-Of-Stock Firearm Type")
                        List_Bullets_DetailsTableAdapter.FillByOOSFirearmType(MLLDataSet.List_Bullets_Details)
                    Case LCase("Out-Of-Stock Qty.")
                        List_Bullets_DetailsTableAdapter.FillByOOSQty(MLLDataSet.List_Bullets_Details)
                    Case LCase("Out-Of-Stock Price")
                        List_Bullets_DetailsTableAdapter.FillByOOSPrice(MLLDataSet.List_Bullets_Details)
                    Case Else
                        List_Bullets_DetailsTableAdapter.Fill(MLLDataSet.List_Bullets_Details)
                End Select
                ReportViewer1.RefreshReport()
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmReport_BulletInventory control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmReport_BulletInventory_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                List_Bullets_DetailsTableAdapter.Fill(MLLDataSet.List_Bullets_Details)
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