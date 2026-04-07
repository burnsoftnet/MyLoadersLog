Imports BSMyLoadersLog.Adding
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Inventory

Namespace Viewing
    ' TODO: #20 clean up code
    ''' <summary>
    ''' Class FrmViewListShellHulls.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmViewListShellHulls
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' Registry View name
        ''' </summary>
        Const RegViewName As String = "View_ShellHulls"
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            Try
                List_SG_CaseTableAdapter.Fill(MLLDataSet.List_SG_Case)
                Select Case LCase(ToolStripComboBox1.SelectedItem.ToString)
                    Case LCase("All")
                        List_SG_CaseTableAdapter.Fill(MLLDataSet.List_SG_Case)
                    Case LCase("Instock")
                        'List_SG_CaseTableAdapter.
                        List_SG_CaseTableAdapter.FillBy_Menu_InStock(MLLDataSet.List_SG_Case)
                    Case LCase("Out-Of-Stock")
                        List_SG_CaseTableAdapter.FillBy_Menu_OutOfStock(MLLDataSet.List_SG_Case)
                    Case LCase("Reference")
                        List_SG_CaseTableAdapter.FillBy_Menu_Refferance(MLLDataSet.List_SG_Case)
                    Case Else
                        List_SG_CaseTableAdapter.Fill(MLLDataSet.List_SG_Case)
                End Select
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Deletes the data.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub DeleteData()
            Try
                Dim itemId As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                'Dim Obj As New BSDatabase
                Dim objG As New GlobalFunctions
                Dim strSQLTable As String = "List_SG_Case"
                Dim strName As String = objG.GetName("SELECT * from " & strSQLTable & " where ID=" & itemId, "Name")
                'Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & itemId
                'If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
                ' TODO #19 Replace function above with on below after next library update
                'Dim strName As String = ShotgunHullInventory.GetName(DatabasePath, itemId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & itemId
                'If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
                If strAns = vbYes Then
                    If Not ShotgunHullInventory.Delete(DatabasePath, itemId, _errOut) Then Throw New Exception(_errOut)
                    Call LoadData()
                End If
            Catch ex As Exception
                Call LogError(Name, "DeleteData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmView_List_ShellHulls control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_List_ShellHulls_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton3 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton3.Click
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Adds the shell.
        ''' </summary>
        Sub AddShell()
            Try
                Dim frmNew As New frmAddShell
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "ADDShell", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
            Call AddShell()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton2.Click
            Call DeleteData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the AddToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub AddToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddToolStripMenuItem.Click
            Call AddShell()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the DeleteToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
            Call DeleteData()
        End Sub
        ''' <summary>
        ''' Edits the hulls.
        ''' </summary>
        Sub EditHulls()
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New frmEditHulls
                frmNew.MdiParent = MdiParent
                frmNew.SID = itemId
                frmNew.FromView = True
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "EditHulls", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the EditToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub EditToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditToolStripMenuItem.Click
            Call EditHulls()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton4 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton4.Click
            Cursor = Cursors.WaitCursor
            Try
                frmReport_HullInventory.MdiParent = MdiParent
                frmReport_HullInventory.Show()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton4_Click", Err.Number, ex.Message.ToString)
            End Try
            Cursor = Cursors.Arrow
        End Sub
        ''' <summary>
        ''' Handles the BindingContextChanged event of the DataGridView1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        ''' <summary>
        ''' Handles the Click event of the AddToQtyToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub AddToQtyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToQtyToolStripMenuItem.Click
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New FrmAddQtyHulls
                frmNew.MdiParent = MdiParent
                frmNew.SID = itemId
                frmNew.FromView = True
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "AddToQtyToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the MarkAsOutOfStockToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub MarkAsOutOfStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MarkAsOutOfStockToolStripMenuItem.Click
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                If Not ShotgunHullInventory.UpdateQty(DatabasePath, itemId, 0, _errOut) Then Throw New Exception(_errOut)
                'Dim SQL As String = "UPDATE List_SG_Case set QTY=0 where ID=" & itemId
                'Dim Obj As New BSDatabase
                'Obj.ConnExec(SQL)
                Call LoadData()
            Catch ex As Exception
                Call LogError(Name, "MarkAsOutOfStockToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the ToolStripComboBox1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Handles the FormClosing event of the FrmViewListShellHulls control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub FrmViewListShellHulls_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
            Try
                If Not MyRegistry.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString, 
                                                   _errOut) Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "FrmViewListShellHulls_FormClosing", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End Namespace