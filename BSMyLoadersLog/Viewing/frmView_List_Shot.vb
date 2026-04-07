Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Inventory

Namespace Viewing

    Public Class FrmViewListShot
        ' TODO: #20 clean up code
        ''' <summary>
        ''' The error out
        ''' </summary>
        Private _errOut as String
        ''' <summary>
        ''' Registry setting for the selected list to view
        ''' </summary>
        Const RegViewName As String = "View_Shot"
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            Try
                Dim sValue As String = LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
                Select Case LCase(sValue)
                    Case LCase("All")
                        List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All(MLLDataSet.List_SG_ShotType_Details)
                    Case LCase("Instock")
                        List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_In_Stock(MLLDataSet.List_SG_ShotType_Details)
                    Case LCase("Out-Of-Stock")
                        List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_Out_Of_Stock(MLLDataSet.List_SG_ShotType_Details)
                    Case LCase("Reference")
                        List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_Referance(MLLDataSet.List_SG_ShotType_Details)
                    Case Else
                        List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All(MLLDataSet.List_SG_ShotType_Details)
                End Select
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Deletes the shot.
        ''' </summary>
        Private Sub DeleteShot()
            Try
                Dim itemId As long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                'Dim Obj As New BSDatabase
                Dim objG As New GlobalFunctions
                Dim strSQLTable As String = "List_SG_ShotType_Details"
                Dim strName As String = objG.GetName("SELECT * from " & strSQLTable & " where ID=" & itemId, "Name")
                'Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
                'If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()

                'TODO #19 Replace function above with on below after next library update
                'Dim strName As String = ShotgunShotTypeInventory.GetName(DatabasePath, itemId, _errOut)
                if _errOut.Length > 0 Then Throw New Exception(_errOut)
                Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                    
                If strAns = vbYes Then
                    If Not ShotgunShotTypeInventory.Delete(DatabasePath, itemId, _errOut) then Throw new Exception(_errOut)
                    Call LoadData()
                End If
            Catch ex As Exception
                Call LogError(Name, "DeleteShot", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the FormClosing event of the frmView_List_Shot control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmView_List_Shot_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        
            Try
                'Dim ObjR As New BSRegistry
                'Call ObjR.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString)
                If Not MyRegistry.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString, 
                                                   _errOut) Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "frmView_List_Shot_FormClosing", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmViewShotList control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmViewShotList_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            'Dim ObjR As New BSRegistry
            'ToolStripComboBox1.Text = ObjR.GetViewSettings(RegViewName, "All")
            'Call LoadData()
            Try
                ToolStripComboBox1.Text = MyRegistry.GetViewSettings(RegViewName, _errOut, "All")
                if _errOut.Length > 0 Then Throw New Exception(_errOut)
                'Dim ObjR As New BSRegistry
                'ToolStripComboBox1.Text = ObjR.GetViewSettings(RegViewName, "All")
                Call LoadData()
            Catch ex As Exception
                Call LogError(Name, "frmViewShotList_Load", Err.Number, ex.Message.ToString)
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
        ''' Handles the Click event of the ToolStripButton1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
            Try
                Dim frmNew As New frmAddShot
                frmNew.FromView = True
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton1_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton2.Click
            Call DeleteShot()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton4 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton4.Click
            Cursor = Cursors.WaitCursor
            Try
                frmReport_ShotInventory.MdiParent = MdiParent
                frmReport_ShotInventory.Show()
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
        ''' Handles the Click event of the DeleteToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
            Call DeleteShot()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the EditToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub EditToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditToolStripMenuItem.Click
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New frmEditShot
                frmNew.BID = itemId
                frmNew.FromView = True
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "EditToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the OutOfStockToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub OutOfStockToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OutOfStockToolStripMenuItem.Click
           Try
               Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
               If Not ShotgunShotTypeInventory.UpdateQty(DatabasePath, itemId, 0, 
                                                         0, 0, _errOut) Then Throw new Exception(_errOut)
               'Dim SQL As String = "UPDATE List_SG_ShotType_Details set weight=0 where ID=" & itemId
               'Dim Obj As New BSDatabase
               'Obj.ConnExec(SQL)
               Call LoadData()
           Catch ex As Exception
               Call LogError(Name, "OutOfStockToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
           End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the AddToQtyToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub AddToQtyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddToQtyToolStripMenuItem.Click
            Try
                Dim frmNew As New frmAddQtyShot
                Dim itemId As long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                frmNew.BID = itemId
                frmNew.FromView = True
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "AddToQtyToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
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