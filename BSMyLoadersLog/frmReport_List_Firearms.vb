Public Class frmReport_List_Firearms

    Private Sub frmReport_List_Firearms_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Loaders_Log_FirearmsTableAdapter.Fill(Me.MLLDataSet.Loaders_Log_Firearms)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Try
            Select Case LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
                Case LCase("Manufacturer")
                    Me.Loaders_Log_FirearmsTableAdapter.FillByManuName(Me.MLLDataSet.Loaders_Log_Firearms)
                Case LCase("Full Name")
                    Me.Loaders_Log_FirearmsTableAdapter.FillByFullName(Me.MLLDataSet.Loaders_Log_Firearms)
                Case LCase("Firearm Type")
                    Me.Loaders_Log_FirearmsTableAdapter.FillByFirearmType(Me.MLLDataSet.Loaders_Log_Firearms)
                Case LCase("Caliber")
                    Me.Loaders_Log_FirearmsTableAdapter.FillByCaliber(Me.MLLDataSet.Loaders_Log_Firearms)
                Case LCase("Barrel")
                    Me.Loaders_Log_FirearmsTableAdapter.FillByBarrel(Me.MLLDataSet.Loaders_Log_Firearms)
                Case LCase("Serial No.")
                    Me.Loaders_Log_FirearmsTableAdapter.FillBySerialNo(Me.MLLDataSet.Loaders_Log_Firearms)
                Case Else
                    Me.Loaders_Log_FirearmsTableAdapter.Fill(Me.MLLDataSet.Loaders_Log_Firearms)
            End Select
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "ToolStripComboBox1_SelectedIndexChanged", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class