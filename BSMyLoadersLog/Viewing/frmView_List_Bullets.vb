Imports BSMyLoadersLog.Adding
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Global
Imports BSMyLoadersLog.ViewReports

Namespace Viewing
    ''' <summary>
    ''' Class frmView_List_Bullets.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class frmView_List_Bullets
        ''' <summary>
        ''' The error out
        ''' </summary>
        Private _errOut as String
        ''' <summary>
        ''' 
        ''' </summary>
        Const RegViewName As String = "View_Bullets"
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            Try
                Select Case LCase(ToolStripComboBox1.Text)
                    Case LCase("All")
                        Me.List_Bullets_DetailsTableAdapter.FillByAllManu(Me.MLLDataSet.List_Bullets_Details)
                    Case LCase("Instock")
                        Me.List_Bullets_DetailsTableAdapter.FillByINSManu(Me.MLLDataSet.List_Bullets_Details)
                    Case LCase("Out-Of-Stock")
                        Me.List_Bullets_DetailsTableAdapter.FillByOOSManu(Me.MLLDataSet.List_Bullets_Details)
                    Case LCase("Reference")
                        Me.List_Bullets_DetailsTableAdapter.FillBy_Reference(Me.MLLDataSet.List_Bullets_Details)
                    Case Else
                        Me.List_Bullets_DetailsTableAdapter.Fill(Me.MLLDataSet.List_Bullets_Details)
                End Select
            Catch ex As Exception
                Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the FormClosing event of the frmView_List_Bullets control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmView_List_Bullets_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
            'Dim ObjR As New BSRegistry
            'Call ObjR.SaveViewSettings(RegViewName, ToolStripComboBox1.Text)
            Try
                If Not MyRegistry.SaveViewSettings(RegViewName, ToolStripComboBox1.Text, _errOut) Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Me.Name, "frmView_List_Bullets_FormClosing", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Private Sub frmView_List_Bullets_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
            If Me.Height <> 0 Then
                Me.DataGridView1.Height = Me.Height - (65)
                Me.DataGridView1.Width = Me.Width - 15
            End If
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmView_List_Bullets control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmView_List_Bullets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Dim ObjR As New BSRegistry
            'ToolStripComboBox1.Text = ObjR.GetViewSettings(RegViewName, "All")
        
            Try
                ToolStripComboBox1.Text = MyRegistry.GetViewSettings(RegViewName, _errOut, "All")
                if _errOut.Length > 0 Then Throw New Exception(_errOut)
                Call LoadData()
            Catch ex As Exception
                Call LogError(Me.Name, "frmView_List_Bullets_Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
            Try
                Dim frmNew As New frmAddBullets
                frmNew.MdiParent = Me.MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Me.Name, "ToolStripButton1_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Deletes the bullet.
        ''' </summary>
        Private Sub DeleteBullet()
            Try
                Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim Obj As New BSDatabase
                Dim ObjG As New GlobalFunctions
                Dim strSQLTable As String = "List_Bullets"
                Dim strName As String = ObjG.GetName("SELECT * from " & strSQLTable & " where ID=" & ItemID, "Name")
                Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
                If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
            Catch ex As Exception
                Call LogError(Me.Name, "DeleteBullet", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
            Try
                Call DeleteBullet()
            Catch ex As Exception
                Call LogError(Me.Name, "ToolStripButton2.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton3 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
            Call LoadData()
        End Sub
        Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
            Try
                Dim itemId As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New frmEditBullets
                frmNew.BID = itemId
                frmNew.FromView = True
                frmNew.MdiParent = Me.MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Me.Name, "EditToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the AddToQtyToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub AddToQtyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToQtyToolStripMenuItem.Click
            Try
                Dim frmNew As New frmAddQtyBullets
                Dim itemId As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                frmNew.BulletId = itemId
                frmNew.FromView = True
                frmNew.MdiParent = Me.MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Me.Name, "AddToQtyToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the DeleteToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
            Call DeleteBullet()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton4 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
            Me.Cursor = Cursors.WaitCursor
            Try
                FrmReportBulletInventory.MdiParent = Me.MdiParent
                FrmReportBulletInventory.Show()
            Catch ex As Exception
                Call LogError(Me.Name, "ToolStripButton4_Click", Err.Number, ex.Message.ToString)
            End Try
            Me.Cursor = Cursors.Arrow
        End Sub
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the ToolStripComboBox1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the OutOfStockToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub OutOfStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutOfStockToolStripMenuItem.Click
            Try
                Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim SQL As String = "UPDATE List_Bullets set QTY=0 where ID=" & ItemID
                Dim Obj As New BSDatabase
                Obj.ConnExec(SQL)
                Call LoadData()
            Catch ex As Exception
                Call LogError(Me.Name, "OutOfStockToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the BindingContextChanged event of the DataGridView1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.BindingContextChanged
            Try
                If DataGridView1.DataSource Is Nothing Then
                    Return
                End If
                DataGridView1.AutoResizeColumns()
            Catch ex As Exception
                Call LogError(Me.Name, "DataGridView1_BindingContextChanged", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the CopyToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
            Try
                Dim itemId As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New frmAddBullets
                frmNew.DoCopy = True
                frmNew.BulletId = itemId
                frmNew.MdiParent = Me.MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Me.Name, "CopyToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace