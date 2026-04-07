'Imports System.Data
'Imports System.Data.Odbc
'Imports System.IO
'Imports System.Xml
Imports BSMyLoadersLog.Adding
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Global

Namespace Viewing
    ''' <summary>
    ''' Class FrmViewListPowder.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmViewListPowder
        ''' <summary>
        ''' The error out
        ''' </summary>
        Private errOut as String
        ''' <summary>
        ''' Registry View Name for settings
        ''' </summary>
        Const RegViewName As String = "View_Powders"
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            Try
                'General_PowderTableAdapter.Fill(MLLDataSet.General_Powder)
                Select Case LCase(ToolStripComboBox1.SelectedItem.ToString)
                    Case LCase("All")
                        General_PowderTableAdapter.Fill(MLLDataSet.General_Powder)
                    Case LCase("Instock")
                        General_PowderTableAdapter.FillByInManu(MLLDataSet.General_Powder)
                    Case LCase("Out-Of-Stock")
                        General_PowderTableAdapter.FillByOutManu(MLLDataSet.General_Powder)
                    Case LCase("Reference")
                        General_PowderTableAdapter.FillBy_Reference(MLLDataSet.General_Powder)
                    Case Else
                        General_PowderTableAdapter.Fill(MLLDataSet.General_Powder)
                End Select
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the FormClosing event of the frmView_List_Powder control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmView_List_Powder_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
            'Dim ObjR As New BSRegistry
            'Call ObjR.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString)
            Try
                If Not MyRegistry.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString, 
                                                   errOut) Then Throw New Exception(errOut)
            Catch ex As Exception
                Call LogError(Name, "frmView_List_Powder_FormClosing", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmView_List_Powder control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmView_List_Powder_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                'Dim ObjR As New BSRegistry
                'ToolStripComboBox1.Text = ObjR.GetViewSettings(RegViewName, "All")
                ToolStripComboBox1.Text = MyRegistry.GetViewSettings(RegViewName, errOut, "All")
                if errOut.Length > 0 Then Throw New Exception(errOut)
                Call LoadData()
            Catch ex As Exception
                Call LogError(Name, "frmView_List_Powder_Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Resize event of the frmView_List_Powder control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_List_Powder_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
            Try
                If Height <> 0 Then
                    DataGridView1.Height = Height - (65)
                    DataGridView1.Width = Width - 15
                End If
            Catch ex As Exception
                Call LogError(Name, "frmView_List_Powder_Resize", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
            try
                Dim frmNew As New frmAddPowder
                frmNew.MdiParent = MdiParent
                frmNew.FromView = True
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton1_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Private Sub ToolStripButton4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton4.Click
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
                Call LogError(Name, "DeletePowder", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Private Sub ToolStripButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton2.Click
            Try
                Call DeletePowder()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton2.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
            Call DeletePowder()
        End Sub
        Private Sub AddToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddToolStripMenuItem.Click
            Try
                Dim frmNew As New frmAddPowder
                frmNew.MdiParent = MdiParent
                frmNew.FromView = True
                frmNew.Show()    
            Catch ex As Exception
                Call LogError(Name, "AddToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Private Sub EditToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditToolStripMenuItem.Click
            Try
                Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New frmEditPowder
                frmNew.MdiParent = MdiParent
                frmNew.PID = ItemID
                frmNew.FromView = True
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "EditToolStripMenuItem.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub

        Private Sub ToolStripButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton3.Click
            Cursor = Cursors.WaitCursor
            Try
                frmReport_PowderInventory.MdiParent = MdiParent
                frmReport_PowderInventory.Show()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton3_Click", Err.Number, ex.Message.ToString)
            End Try
            Cursor = Cursors.Arrow
        End Sub

        Private Sub AddtoCurrentToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddtoCurrentToolStripMenuItem.Click
            Try
                Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New frmAddQtyPowder
                frmNew.MdiParent = MdiParent
                frmNew.PID = ItemID
                frmNew.FromView = True
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "AddtoCurrentToolStripMenuItem.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Private Sub ToolStripComboBox1_SelectedIndexChanged1(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
            Call LoadData()
        End Sub

        Private Sub MarkAsOutOfStockToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MarkAsOutOfStockToolStripMenuItem.Click
            Try
                Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim Obj As New BSDatabase
                Dim SQL As String = "UPDATE General_Powder set weightgn=0, weightlbs=0 where ID=" & ItemID
                Obj.ConnExec(SQL)
                Call LoadData()
            Catch ex As Exception
                Call LogError(Name, "MarkAsOutOfStockToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub

        Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DataGridView1.BindingContextChanged
            try
                If DataGridView1.DataSource Is Nothing Then
                    Return
                End If
                DataGridView1.AutoResizeColumns()
            Catch ex As Exception
                Call LogError(Name, "DataGridView1_BindingContextChanged", Err.Number, ex.Message.ToString)
            End Try
        End Sub

        Private Sub ExportToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExportToolStripMenuItem.Click
            Try
                Dim DefaultFileName As String = "Export_Inventory_Powder.xml"
                SaveFileDialog1.FilterIndex = 1
                SaveFileDialog1.Filter = $"XML File(*.xml)|*.xml"
                SaveFileDialog1.Title = $"Export Data to XML File"
                SaveFileDialog1.FileName = Replace(Replace(Replace(DefaultFileName, " ", "_"), "/", "-"), "\", "-")
                If SaveFileDialog1.ShowDialog() = DialogResult.Cancel Then Exit Sub
                Dim strFilePath As String = SaveFileDialog1.FileName
                Dim objE As New Inventory_Export_Import
                objE.XML_Generate_Powder_Export(strFilePath)
                MsgBox("Export Completed!")
            Catch ex As Exception
                Call LogError(Name, "ExportToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub

        Private Sub ImportToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ImportToolStripMenuItem.Click
            Try
                Dim DefaultFileName As String = "Export_Inventory_Powder.xml"
                OpenFileDialog1.FilterIndex = 1
                OpenFileDialog1.Filter = $"XML File(*.xml)|*.xml"
                OpenFileDialog1.Title = $"Import Powder XML into Database"
                OpenFileDialog1.FileName = Replace(Replace(Replace(DefaultFileName, " ", "_"), "/", "-"), "\", "-")
                If OpenFileDialog1.ShowDialog() = DialogResult.Cancel Then Exit Sub
                Dim sFilePath As String = OpenFileDialog1.FileName
                Dim objE As New Inventory_Export_Import
                Cursor = Cursors.WaitCursor
                objE.XML_Generate_Powder_Import(sFilePath)
                Cursor = Cursors.Arrow
                Call LoadData()
                MsgBox("Import Completed!")
            Catch ex As Exception
                Call LogError(Name, "ImportToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace