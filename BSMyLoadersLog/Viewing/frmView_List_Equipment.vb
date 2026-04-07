Imports BSMyLoadersLog.Adding
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Inventory

Namespace Viewing
    ''' <summary>
    ''' Class FrmViewListEquipment.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmViewListEquipment
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' Handles the Resize event of the frmView_List_Equipment control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_List_Equipment_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
            Try
                If Height <> 0 Then
                    DataGridView1.Height = Height - (65)
                    DataGridView1.Width = Width - 15
                End If
            Catch ex As Exception
                Call LogError(Name, "frmView_List_Equipment_Resize", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            try
                General_EquipmentTableAdapter.Fill(MLLDataSet.General_Equipment)
                'Dim Obj As New GlobalFunctions
                tslCost.Text = $"Current Total: {GeneralFunctions.TotalCostEquipment(DatabasePath, _errOut)}"
                if _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmView_List_Equipment control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_List_Equipment_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                Call LoadData()
            Catch ex As Exception
                'Dim ObjFS As New BSFileSystem
                'Dim sMessage As String = "frmView_List_Equipment.load" & "::" & Err.Number & "::" & ex.Message.ToString()
                'ObjFS.LogFile(MyLogFile, sMessage)
                Call LogError(Name, "frmView_List_Equipment_Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
            try
                frmAddEquipment.MdiParent = MdiParent
                frmAddEquipment.Show()
            Catch ex As Exception
                Call LogError(Name, "", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Deletes the data.
        ''' </summary>
        Sub DeleteData()
            Try
                Dim itemId As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                'Dim Obj As New BSDatabase
                Dim ObjG As New GlobalFunctions
                Dim strName As String = ObjG.GetName("SELECT * from General_Equipment where ID=" & itemId, "Name")
                ' TODO #19 Replace function above with on below after next library update
                'Dim strName As String = EquipmentInventory.GetName(DatabasePath, ItemID, errOut)
                if _errOut.Length > 0 Then Throw New Exception(_errOut)
                Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim SQL As String = "DELETE from General_Equipment where ID=" & ItemID
                'If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
                If strAns = vbYes Then 
                    If Not EquipmentInventory.Delete(DatabasePath, itemId, _errOut) Then Throw New Exception(_errOut)
                    Call LoadData()
                End If
            Catch ex As Exception
                Call LogError(Name, "DeleteData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton2.Click
            Try
                Call DeleteData()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton2.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton3 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton3.Click
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the AddToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub AddToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddToolStripMenuItem.Click
            Try
                frmAddEquipment.MdiParent = MdiParent
                frmAddEquipment.FromView = True
                frmAddEquipment.Show()
            Catch ex As Exception
                Call LogError(Name, "AddToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the DeleteToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
            Call DeleteData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the EditToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub EditToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditToolStripMenuItem.Click
            Try
                Dim itemId As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New frmEditEquipment
                frmNew.MdiParent = MdiParent
                frmNew.EID = itemId
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
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton4.Click
            Try
                Cursor = Cursors.WaitCursor
                frmReport_List_Equipment.MdiParent = MdiParent
                frmReport_List_Equipment.Show()
                Cursor = Cursors.Arrow
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton4_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the BindingContextChanged event of the DataGridView1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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