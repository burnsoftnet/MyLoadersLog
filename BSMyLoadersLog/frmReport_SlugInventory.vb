Public Class frmReport_SlugInventory
    Sub LoadData()
        Try
            Dim sValue As String = ToolStripComboBox1.SelectedItem.ToString
            Select Case LCase(sValue)
                Case LCase("All by Manufacturer")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_All(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("All by Caliber")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_All_Caliber(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("All by Weight")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_All_Weight(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("All by Qty")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_All_Qty(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("All by Price")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Instock_Price(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("InStock - Manufacturer")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Instock_Manu(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("InStock - Caliber")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Instock_Caliber(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("InStock - Weight")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Instock_Weight(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("InStock - Qty")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Instock_Qty(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("InStock - Price")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Instock_Price(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Out of Stock - Manufacturer")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_OutOfstock_Manu(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Out of Stock - Caliber")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_OutOfstock_Caliber(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Out of Stock - Price")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_OutOfstock_Price(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Reference")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Ref_Manu(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Reference - Weight")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Ref_Weight(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Reference - Caliber")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Ref_Caliber(Me.MLLDataSet.List_SG_ShotType_Details)
                Case Else
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_All(Me.MLLDataSet.List_SG_ShotType_Details)
            End Select
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmReport_SlugInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_All(Me.MLLDataSet.List_SG_ShotType_Details)
        Me.ReportViewer1.RefreshReport()
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadData()
    End Sub
End Class