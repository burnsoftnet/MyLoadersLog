Imports BSMyLoadersLog.LoadersClass
Public Class frmView_Bushings_Powder
    Sub LoadData()
        Try
            Select Case LCase(ToolStripComboBox1.Text)
                Case "default"
                    Me.List_SG_Bushing_PowderTableAdapter.Fill(Me.MLLDataSet.List_SG_Bushing_Powder)
                Case "manufacturer"
                    Me.List_SG_Bushing_PowderTableAdapter.FillBy_Manufacturer(Me.MLLDataSet.List_SG_Bushing_Powder)
                Case "name"
                    Me.List_SG_Bushing_PowderTableAdapter.FillBy_Name(Me.MLLDataSet.List_SG_Bushing_Powder)
                Case "charge"
                    Me.List_SG_Bushing_PowderTableAdapter.FillBy_Charge(Me.MLLDataSet.List_SG_Bushing_Powder)
                Case "type"
                    Me.List_SG_Bushing_PowderTableAdapter.FillBy_Type(Me.MLLDataSet.List_SG_Bushing_Powder)
                Case "powder name"
                    Me.List_SG_Bushing_PowderTableAdapter.FillBy_Powdername(Me.MLLDataSet.List_SG_Bushing_Powder)
                Case Else
                    Me.List_SG_Bushing_PowderTableAdapter.Fill(Me.MLLDataSet.List_SG_Bushing_Powder)
            End Select
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub frmView_Bushings_Powder_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Dim objS As New ViewSizeSettings
        objS.SaveBushings_Powder_List(Me.Height, Me.Width, Me.Location.X, Me.Location.Y)
    End Sub

    Private Sub frmView_Bushings_Powder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objS As New ViewSizeSettings
        objS.LoadBushings_Powder_List(Me.Height, Me.Width, Me.Location)
        Call LoadData()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim frmNew As New FrmAddBushingPowder
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Call LoadData()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim ItemID As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim Itemname As String = DataGridView1.SelectedRows.Item(0).Cells.Item(1).Value & " " & _
                                    DataGridView1.SelectedRows.Item(0).Cells.Item(2).Value
        Dim sMsg As String = "Are you sure you wish to delete " & Itemname & "?"
        Dim sAns As String = MsgBox(sMsg, MsgBoxStyle.YesNo)
        Dim SQL As String = ""
        If sAns = vbYes Then
            SQL = "DELETE from List_SG_Bushing_Powder where ID=" & ItemID
            Dim Obj As New BSDatabase
            Obj.ConnExec(SQL)
            Obj = Nothing
        End If
        Call LoadData()
    End Sub
    Private Sub ToolStripComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.TextChanged
        Call LoadData()
    End Sub

    Private Sub ExportAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportAllToolStripMenuItem.Click
        Dim DefaultFileName As String = "Export_Inventory_Bushings_Powder.xml"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.Filter = "XML File(*.xml)|*.xml"
        SaveFileDialog1.Title = "Export Data to XML File"
        SaveFileDialog1.FileName = Replace(Replace(Replace(DefaultFileName, " ", "_"), "/", "-"), "\", "-")
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim strFilePath As String = SaveFileDialog1.FileName
        Dim objE As New Inventory_Export_Import
        objE.XML_Generate_Bushings_Powder_Export(strFilePath)
        'MsgBox("Export Completed!")
    End Sub

    Private Sub ImportAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportAllToolStripMenuItem.Click
        Dim DefaultFileName As String = "Export_Inventory_Bushings_Powder.xml"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.Filter = "XML File(*.xml)|*.xml"
        OpenFileDialog1.Title = "Import Bushings Powder XML into Database"
        OpenFileDialog1.FileName = Replace(Replace(Replace(DefaultFileName, " ", "_"), "/", "-"), "\", "-")
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim sFilePath As String = OpenFileDialog1.FileName
        Dim objE As New Inventory_Export_Import
        Me.Cursor = Cursors.WaitCursor
        objE.XML_Generate_Bushings_Powder_Import(sFilePath)
        Me.Cursor = Cursors.Arrow
        Call LoadData()
        'MsgBox("Import Completed!")
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        Dim CID As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim frmNew As New frmEditBushing_Powder
        frmNew.CID = CID
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
End Class