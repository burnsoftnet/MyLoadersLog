Public Class frmReport_WADInventory
    Public Sub LoadData()
        Try
            Me.List_SG_WADTableAdapter.Fill(Me.MLLDataSet.List_SG_WAD)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmReport_WADInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Try
            Select Case LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
                Case LCase("All - Manufacturer")
                    Me.List_SG_WADTableAdapter.FillBy_Manufacturer(Me.MLLDataSet.List_SG_WAD)
                Case LCase("All - Price")
                    Me.List_SG_WADTableAdapter.FillBy_Price(Me.MLLDataSet.List_SG_WAD)
                Case LCase("All - Qty")
                    Me.List_SG_WADTableAdapter.FillBy_Qty(Me.MLLDataSet.List_SG_WAD)
                Case LCase("InStock Manufacturer")
                    Me.List_SG_WADTableAdapter.FillBy_In_Stock(Me.MLLDataSet.List_SG_WAD)
                Case LCase("InStock Price")
                    Me.List_SG_WADTableAdapter.FillBy_InStock_Price(Me.MLLDataSet.List_SG_WAD)
                Case LCase("InStock Qty.")
                    Me.List_SG_WADTableAdapter.FillBy_InStock_Qty(Me.MLLDataSet.List_SG_WAD)
                Case LCase("Out of Stock - Manufacturer")
                    Me.List_SG_WADTableAdapter.FillBy_Out_Of_Stock(Me.MLLDataSet.List_SG_WAD)
                Case LCase("Out of Stock - Price")
                    Me.List_SG_WADTableAdapter.FillBy_Out_Of_Stock_Price(Me.MLLDataSet.List_SG_WAD)
                Case LCase("Reference Only")
                    Me.List_SG_WADTableAdapter.FillBy_Referance(Me.MLLDataSet.List_SG_WAD)
                Case Else
                    Me.List_SG_WADTableAdapter.Fill(Me.MLLDataSet.List_SG_WAD)
            End Select
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "ToolStripComboBox1_SelectedIndexChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class