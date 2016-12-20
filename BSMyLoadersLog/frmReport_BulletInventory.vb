Public Class frmReport_BulletInventory
    Sub LoadData()
        Try
            Select Case LCase(ToolStripComboBox1.SelectedItem.ToString)
                Case LCase("All Manufacturer")
                    Me.List_Bullets_DetailsTableAdapter.FillByAllManu(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("All Caliber")
                    Me.List_Bullets_DetailsTableAdapter.FillByAllCalibers(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("All Diameter")
                    Me.List_Bullets_DetailsTableAdapter.FillByAllDiameter(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("All Weight")
                    Me.List_Bullets_DetailsTableAdapter.FillByAllWeight(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("All Section Density")
                    Me.List_Bullets_DetailsTableAdapter.FillByAllSectionDensity(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("All Part Number")
                    Me.List_Bullets_DetailsTableAdapter.FillByAllPart_number(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("All Ballistic Coefficient")
                    Me.List_Bullets_DetailsTableAdapter.FillByAllBallistic_Coefficient(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("All Firearm Type")
                    Me.List_Bullets_DetailsTableAdapter.FillByAllFirearmType(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("All Qty.")
                    Me.List_Bullets_DetailsTableAdapter.FillByAllQty(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("All Price")
                    Me.List_Bullets_DetailsTableAdapter.FillByAllPrice(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Instock Manufacturer")
                    Me.List_Bullets_DetailsTableAdapter.FillByINSManu(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Instock Caliber")
                    Me.List_Bullets_DetailsTableAdapter.FillByINSCalibers(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Instock Diameter")
                    Me.List_Bullets_DetailsTableAdapter.FillByINSDiameter(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Instock Weight")
                    Me.List_Bullets_DetailsTableAdapter.FillByINSWeight(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Instock Section Density")
                    Me.List_Bullets_DetailsTableAdapter.FillByINSSectionDensity(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Instock Part Number")
                    Me.List_Bullets_DetailsTableAdapter.FillByINSPart_number(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Instock Ballistic Coefficient")
                    Me.List_Bullets_DetailsTableAdapter.FillByINSBallistic_Coefficient(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Instock Firearm Type")
                    Me.List_Bullets_DetailsTableAdapter.FillByINSFirearmType(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Instock Qty.")
                    Me.List_Bullets_DetailsTableAdapter.FillByINSQty(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Instock Price")
                    Me.List_Bullets_DetailsTableAdapter.FillByINSPrice(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Out-Of-Stock Manufacturer")
                    Me.List_Bullets_DetailsTableAdapter.FillByOOSManu(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Out-Of-Stock Caliber")
                    Me.List_Bullets_DetailsTableAdapter.FillByOOSCalibers(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Out-Of-Stock Diameter")
                    Me.List_Bullets_DetailsTableAdapter.FillByOOSDiameter(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Out-Of-Stock Weight")
                    Me.List_Bullets_DetailsTableAdapter.FillByOOSWeight(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Out-Of-Stock Section Density")
                    Me.List_Bullets_DetailsTableAdapter.FillByOOSSectionDensity(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Out-Of-Stock Part Number")
                    Me.List_Bullets_DetailsTableAdapter.FillByOOSPart_number(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Out-Of-Stock Ballistic Coefficient")
                    Me.List_Bullets_DetailsTableAdapter.FillByOOSBallistic_Coefficient(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Out-Of-Stock Firearm Type")
                    Me.List_Bullets_DetailsTableAdapter.FillByOOSFirearmType(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Out-Of-Stock Qty.")
                    Me.List_Bullets_DetailsTableAdapter.FillByOOSQty(Me.MLLDataSet.List_Bullets_Details)
                Case LCase("Out-Of-Stock Price")
                    Me.List_Bullets_DetailsTableAdapter.FillByOOSPrice(Me.MLLDataSet.List_Bullets_Details)
                Case Else
                    Me.List_Bullets_DetailsTableAdapter.Fill(Me.MLLDataSet.List_Bullets_Details)
            End Select
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmReport_BulletInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'TODO:  put in $ for all reports,
            'example expression:
            '="$" & IIF(isnumeric(Fields!InsuredValue.Value),cdbl(Fields!InsuredValue.Value),"0.00")
            Me.List_Bullets_DetailsTableAdapter.Fill(Me.MLLDataSet.List_Bullets_Details)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadData()
    End Sub
End Class