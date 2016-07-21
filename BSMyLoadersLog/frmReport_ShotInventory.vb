Public Class frmReport_ShotInventory
    Public Sub LoadData()
        Try
            Dim sValue As String = ToolStripComboBox1.SelectedItem.ToString
            Select Case LCase(sValue)
                Case LCase("All by Manufacturer")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("All by Material")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All_Material(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("All by Weight")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All_Weight(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("All by Shot No.")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All_ShotNo(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("All by Price")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All_Price(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("InStock - Manufacturer")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_In_Stock(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("InStock - Material")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_In_Stock_Material(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("InStock - Weight")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All_Weight(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("InStock - Shot No.")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_In_Stock_ShotNo(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("InStock - Price")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_In_Stock_Price(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Out of Stock - Manufacturer")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_Out_Of_Stock(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Out of Stock - Material")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_Out_Of_Stock_Material(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Out of Stock - Shot No.")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_Out_Of_Stock_ShotNo(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Out of Stock - Price")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_Out_Of_Stock_Price(Me.MLLDataSet.List_SG_ShotType_Details)
                Case Else
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All(Me.MLLDataSet.List_SG_ShotType_Details)
            End Select
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmReport_ShotInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All(Me.MLLDataSet.List_SG_ShotType_Details)
        Me.ReportViewer1.RefreshReport()
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadData()
    End Sub
End Class