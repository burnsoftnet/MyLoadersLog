Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Imports System.IO
Imports System.Xml
Imports System.Data
Public Class frmView_List_Powder
    Const RegViewName As String = "View_Powders"
    Public Sub LoadData()
        Try
            'Me.General_PowderTableAdapter.Fill(Me.MLLDataSet.General_Powder)
            Select Case LCase(ToolStripComboBox1.SelectedItem.ToString)
                Case LCase("All")
                    Me.General_PowderTableAdapter.Fill(Me.MLLDataSet.General_Powder)
                Case LCase("Instock")
                    Me.General_PowderTableAdapter.FillByInManu(Me.MLLDataSet.General_Powder)
                Case LCase("Out-Of-Stock")
                    Me.General_PowderTableAdapter.FillByOutManu(Me.MLLDataSet.General_Powder)
                Case LCase("Reference")
                    Me.General_PowderTableAdapter.FillBy_Reference(Me.MLLDataSet.General_Powder)
                Case Else
                    Me.General_PowderTableAdapter.Fill(Me.MLLDataSet.General_Powder)
            End Select
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmView_List_Powder_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim ObjR As New BSRegistry
        Call ObjR.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString)
    End Sub
    Private Sub frmView_List_Powder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim ObjR As New BSRegistry
            ToolStripComboBox1.Text = ObjR.GetViewSettings(RegViewName, "All")
            Call LoadData()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub frmView_List_Powder_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Height <> 0 Then
            Me.DataGridView1.Height = Me.Height - (65)
            Me.DataGridView1.Width = Me.Width - 15
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim frmNew As New frmAddPowder
        frmNew.MdiParent = Me.MdiParent
        frmNew.FromView = True
        frmNew.Show()
    End Sub
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Call LoadData()
    End Sub
    Sub DeletePowder()
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strSQLTable As String = "General_Powder"
            Dim strName As String = ObjG.GetName("SELECT * from " & strSQLTable & " where ID=" & ItemID, "Name")
            Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
            If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
        Catch ex As Exception
            Call LogError(Me.Name, "DeletePowder", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Call DeletePowder()
        Catch ex As Exception
            Call LogError(Me.Name, "ToolStripButton2.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Call DeletePowder()
    End Sub
    Private Sub AddToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click
        Dim frmNew As New frmAddPowder
        frmNew.MdiParent = Me.MdiParent
        frmNew.FromView = True
        frmNew.Show()
    End Sub
    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim frmNew As New frmEditPowder
            frmNew.MdiParent = Me.MdiParent
            frmNew.PID = ItemID
            frmNew.FromView = True
            frmNew.Show()
        Catch ex As Exception
            Call LogError(Me.Name, "EditToolStripMenuItem.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_PowderInventory.MdiParent = Me.MdiParent
        frmReport_PowderInventory.Show()
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub AddtoCurrentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddtoCurrentToolStripMenuItem.Click
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim frmNew As New frmAddQtyPowder
            frmNew.MdiParent = Me.MdiParent
            frmNew.PID = ItemID
            frmNew.FromView = True
            frmNew.Show()
        Catch ex As Exception
            Call LogError(Me.Name, "AddtoCurrentToolStripMenuItem.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadData()
    End Sub

    Private Sub MarkAsOutOfStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarkAsOutOfStockToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim Obj As New BSDatabase
        Dim SQL As String = "UPDATE General_Powder set weightgn=0, weightlbs=0 where ID=" & ItemID
        Obj.ConnExec(SQL)
        Call LoadData()
    End Sub

    Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.BindingContextChanged
        If DataGridView1.DataSource Is Nothing Then
            Return
        End If
        DataGridView1.AutoResizeColumns()
    End Sub

    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        Dim DefaultFileName As String = "Export_Inventory_Powder.xml"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.Filter = "XML File(*.xml)|*.xml"
        SaveFileDialog1.Title = "Export Data to XML File"
        SaveFileDialog1.FileName = Replace(Replace(Replace(DefaultFileName, " ", "_"), "/", "-"), "\", "-")
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim strFilePath As String = SaveFileDialog1.FileName
        Dim objE As New Inventory_Export_Import
        objE.XML_Generate_Powder_Export(strFilePath)
        MsgBox("Export Completed!")
    End Sub

    Private Sub ImportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportToolStripMenuItem.Click
        Dim DefaultFileName As String = "Export_Inventory_Powder.xml"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.Filter = "XML File(*.xml)|*.xml"
        OpenFileDialog1.Title = "Import Powder XML into Database"
        OpenFileDialog1.FileName = Replace(Replace(Replace(DefaultFileName, " ", "_"), "/", "-"), "\", "-")
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim sFilePath As String = OpenFileDialog1.FileName
        Dim objE As New Inventory_Export_Import
        Me.Cursor = Cursors.WaitCursor
        objE.XML_Generate_Powder_Import(sFilePath)
        Me.Cursor = Cursors.Arrow
        Call LoadData()
        MsgBox("Import Completed!")
    End Sub
End Class