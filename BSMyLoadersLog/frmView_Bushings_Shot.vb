Imports BSMyLoadersLog.LoadersClass
Public Class frmView_Bushings_Shot

    Sub LoadData()
        Try
            Select Case LCase(ToolStripComboBox1.Text)
                Case "default"
                    Me.List_SG_Bushing_ShotTableAdapter.Fill(Me.MLLDataSet.List_SG_Bushing_Shot)
                Case "manufacturer"
                    Me.List_SG_Bushing_ShotTableAdapter.FillBy_Manufacturer(Me.MLLDataSet.List_SG_Bushing_Shot)
                Case "name"
                    Me.List_SG_Bushing_ShotTableAdapter.FillBy_sName(Me.MLLDataSet.List_SG_Bushing_Shot)
                Case "charge"
                    Me.List_SG_Bushing_ShotTableAdapter.FillBy_sCharge(Me.MLLDataSet.List_SG_Bushing_Shot)
                Case "type"
                    Me.List_SG_Bushing_ShotTableAdapter.FillBy_sType(Me.MLLDataSet.List_SG_Bushing_Shot)
                Case Else
                    Me.List_SG_Bushing_ShotTableAdapter.Fill(Me.MLLDataSet.List_SG_Bushing_Shot)
            End Select
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub frmView_Bushings_Shot_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Dim objS As New ViewSizeSettings
        objS.SaveBushings_Shot_List(Me.Height, Me.Width, Me.Location.X, Me.Location.Y)
    End Sub
    Private Sub frmView_Bushings_Shot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objS As New ViewSizeSettings
        objS.LoadBushings_Shot_List(Me.Height, Me.Width, Me.Location)
        Call LoadData()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim frmNew As New FrmAddBushingShot
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub

    Private Sub ExportAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportAllToolStripMenuItem.Click
        Dim DefaultFileName As String = "Export_Inventory_Bushings_Shot.xml"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.Filter = "XML File(*.xml)|*.xml"
        SaveFileDialog1.Title = "Export Data to XML File"
        SaveFileDialog1.FileName = Replace(Replace(Replace(DefaultFileName, " ", "_"), "/", "-"), "\", "-")
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim strFilePath As String = SaveFileDialog1.FileName
        Dim objE As New Inventory_Export_Import
        objE.XML_Generate_Bushings_Shot_Export(strFilePath)
    End Sub

    Private Sub ImportAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportAllToolStripMenuItem.Click
        Dim DefaultFileName As String = "Export_Inventory_Bushings_Shot.xml"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.Filter = "XML File(*.xml)|*.xml"
        OpenFileDialog1.Title = "Import Bushings Shot XML into Database"
        OpenFileDialog1.FileName = Replace(Replace(Replace(DefaultFileName, " ", "_"), "/", "-"), "\", "-")
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim sFilePath As String = OpenFileDialog1.FileName
        Dim objE As New Inventory_Export_Import
        Me.Cursor = Cursors.WaitCursor
        objE.XML_Generate_Bushings_Shot_Import(sFilePath)
        Me.Cursor = Cursors.Arrow
        Call LoadData()
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
            SQL = "DELETE from List_SG_Bushing_Shot where ID=" & ItemID
            Dim Obj As New BSDatabase
            Obj.ConnExec(SQL)
            Obj = Nothing
        End If
        Call LoadData()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        Dim CID As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim frmNew As New frmEditBushing_Shot
        frmNew.CID = CID
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
End Class