Imports BSMyLoadersLog.Adding
'Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.LoadersLog

Namespace Viewing
    ' TODO: #20 Clean up code
    ''' <summary>
    ''' Class FrmViewListFirearms.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmViewListFirearms
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut as String
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            Loaders_Log_FirearmsTableAdapter.Fill(MLLDataSet.Loaders_Log_Firearms)
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmView_List_Firearms control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_List_Firearms_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                Call LoadData()
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Resize event of the frmView_List_Firearms control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_List_Firearms_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
            Try
                If Height <> 0 Then
                    DataGridView1.Height = Height - (65)
                    DataGridView1.Width = Width - 15
                End If
            Catch ex As Exception
                Call LogError(Name, "frmView_List_Firearms_Resize", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
            try
                FrmAddFirearm.MdiParent = MdiParent
                FrmAddFirearm.FromView = True
                FrmAddFirearm.Show()
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
                Call DeleteFirearm()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton2.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the DeleteToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
            Call DeleteFirearm()
        End Sub
        ''' <summary>
        ''' Deletes the firearm.
        ''' </summary>
        Sub DeleteFirearm()
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                'Dim Obj As New BSDatabase
                'Dim ObjG As New GlobalFunctions
                'Dim strName As String = ObjG.GetName("SELECT * from Loaders_Log_Firearms where ID=" & itemId, "FullName")
                Dim strName As String = Firearms.GetName(DatabasePath, itemId, _errOut)
                if _errOut.Length > 0 Then Throw New Exception(_errOut)
                Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim SQL As String = "DELETE from Loaders_Log_Firearms where ID=" & itemId
                'If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
                If strAns = vbYes Then
                    If Not Firearms.Delete(DatabasePath, itemId, _errOut) then Throw New Exception(_errOut)
                    Call LoadData()
                End If
            Catch ex As Exception
                Call LogError(Name, "DeleteFirearm", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the AddToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub AddToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddToolStripMenuItem.Click
            Try
                FrmAddFirearm.MdiParent = MdiParent
                FrmAddFirearm.FromView = True
                FrmAddFirearm.Show()
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
            try
                Dim itemId As long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New frmEditFirearm
                frmNew.MdiParent = MdiParent
                frmNew.FID = itemId
                frmNew.FromView = True
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
            Try
                Cursor = Cursors.WaitCursor
                frmReport_List_Firearms.MdiParent = MdiParent
                frmReport_List_Firearms.Show()
                Cursor = Cursors.Arrow
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton4_Click", Err.Number, ex.Message.ToString)
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