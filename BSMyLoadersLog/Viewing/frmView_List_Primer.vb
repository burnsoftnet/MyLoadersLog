Imports BSMyLoadersLog.Adding
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Inventory

Namespace Viewing
    ''' <summary>
    ''' Class FrmViewListPrimer.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmViewListPrimer
        ''' <summary>
        ''' The error out
        ''' </summary>
        Private _errOut as String
        ''' <summary>
        ''' Registry view setting name
        ''' </summary>
        Const RegViewName As String = "View_Primers"
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            Try
                Select Case LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
                    Case LCase("All")
                        ViewPrimerListTableAdapter.FillByAllManu(MLLDataSet.viewPrimerList)
                    Case LCase("Instock")
                        ViewPrimerListTableAdapter.FillByInManu(MLLDataSet.viewPrimerList)
                    Case LCase("Out-Of-Stock")
                        ViewPrimerListTableAdapter.FillByOutManu(MLLDataSet.viewPrimerList)
                    Case LCase("Reference")
                        ViewPrimerListTableAdapter.FillBy_Reference(MLLDataSet.viewPrimerList)
                    Case Else
                        ViewPrimerListTableAdapter.Fill(MLLDataSet.viewPrimerList)
                End Select
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the FormClosing event of the frmView_List_Primer control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmView_List_Primer_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
            Try
                'Dim ObjR As New BSRegistry
                'Call ObjR.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString)
                If Not MyRegistry.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString, 
                                                   _errOut) Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "frmView_List_Primer_FormClosing", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmView_List_Primer control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmView_List_Primer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                'Dim ObjR As New BSRegistry
                'ToolStripComboBox1.Text = ObjR.GetViewSettings(RegViewName, "All")
                ToolStripComboBox1.Text = MyRegistry.GetViewSettings(RegViewName, _errOut, "All")
                if _errOut.Length > 0 Then Throw New Exception(_errOut)
                Call LoadData()
            Catch ex As Exception
                Call LogError(Name, "frmView_List_Primer_Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Resize event of the frmView_List_Primer control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_List_Primer_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
            Try
                If Height <> 0 Then
                    DataGridView1.Height = Height - (65)
                    DataGridView1.Width = Width - 15
                End If
            Catch ex As Exception
                Call LogError(Name, "frmView_List_Primer_Resize", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
            Try
                Dim frmNew As New frmAddPrimer
                frmNew.FromView = True
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton1_Click", Err.Number, ex.Message.ToString)
            End Try
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
        ''' Handles the Click event of the ToolStripButton2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton2.Click
            Try
                Call DeletePrimers()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton2.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Deletes the primers.
        ''' </summary>
        Sub DeletePrimers()
            Try
                Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                'Dim Obj As New BSDatabase
                Dim ObjG As New GlobalFunctions
                Dim strSQLTable As String = "General_Primer"
                Dim strName As String = ObjG.GetName("SELECT * from " & strSQLTable & " where ID=" & ItemID, "Name")
                'Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
                'If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
                ' TODO #19 Replace function above with on below after next library update
                'Dim strName As String = PrimerInventory.GetName(DatabasePath, itemId, _errOut)
                if _errOut.Length > 0 Then Throw New Exception(_errOut)
                Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & itemId
                'If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
                If strAns = vbYes Then
                    If Not PrimerInventory.Delete(DatabasePath, itemId, _errOut) then Throw new Exception(_errOut)
                    Call LoadData()
                End If
            Catch ex As Exception
                Call LogError(Name, "DeletePrimers", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the DeleteToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
            Call DeletePrimers()
        End Sub

        Private Sub AddToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddToolStripMenuItem.Click
            Try
                Dim frmNew As New frmAddPrimer
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "AddToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the EditToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub EditToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditToolStripMenuItem.Click
            Try
                Dim itemId As long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New frmEditPrimer
                frmNew.MdiParent = MdiParent
                frmNew.PID = itemId
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "EditToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton4 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton4.Click
            Cursor = Cursors.WaitCursor
            Try
                frmReport_PrimerInventory.MdiParent = MdiParent
                frmReport_PrimerInventory.Show()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton4_Click", Err.Number, ex.Message.ToString)
            End Try
            Cursor = Cursors.Arrow
        End Sub
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the ToolStripComboBox1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the AddToQtyToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub AddToQtyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddToQtyToolStripMenuItem.Click
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New frmAddQtyPrimers
                frmNew.MdiParent = MdiParent
                frmNew.PrimerId = itemId
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
        ''' <exception cref="System.Exception"></exception>
        Private Sub MarkAsOutOfStockToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MarkAsOutOfStockToolStripMenuItem.Click
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                If Not PrimerInventory.UpdateQty(DatabasePath,itemId, 0, _errOut) Then Throw New Exception(_errOut)
                'Dim SQL As String = "UPDATE General_Primer set QTY=0 where ID=" & itemId
                'Dim Obj As New BSDatabase
                'Obj.ConnExec(SQL)
                Call LoadData()
            Catch ex As Exception
                Call LogError(Name, "MarkAsOutOfStockToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the BindingContextChanged event of the DataGridView1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DataGridView1.BindingContextChanged
            Try
                If DataGridView1.DataSource Is Nothing Then
                    Return
                End If
                DataGridView1.AutoResizeColumns()
            Catch ex As Exception
                Call LogError(Name, "DataGridView1_BindingContextChanged", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace