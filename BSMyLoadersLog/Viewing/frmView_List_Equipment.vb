Imports BSMyLoadersLog.Adding
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Global

Public Class frmView_List_Equipment
    ''' <summary>
    ''' The error out
    ''' </summary>
    Dim errOut As String
    ''' <summary>
    ''' Handles the Resize event of the frmView_List_Equipment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub frmView_List_Equipment_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Height <> 0 Then
            Me.DataGridView1.Height = Me.Height - (65)
            Me.DataGridView1.Width = Me.Width - 15
        End If
    End Sub
    ''' <summary>
    ''' Loads the data.
    ''' </summary>
    Public Sub LoadData()
        Me.General_EquipmentTableAdapter.Fill(Me.MLLDataSet.General_Equipment)
        'Dim Obj As New GlobalFunctions
        tslCost.Text = "Current Total: " & GeneralFunctions.TotalCostEquipment(DatabasePath, errOut)
    End Sub
    ''' <summary>
    ''' Handles the Load event of the frmView_List_Equipment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub frmView_List_Equipment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        frmAddEquipment.MdiParent = Me.MdiParent
        frmAddEquipment.Show()
    End Sub
    ''' <summary>
    ''' Deletes the data.
    ''' </summary>
    Sub DeleteData()
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strName As String = ObjG.GetName("SELECT * from General_Equipment where ID=" & ItemID, "Name")
            Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            Dim SQL As String = "DELETE from General_Equipment where ID=" & ItemID
            If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
        Catch ex As Exception
            Call LogError(Me.Name, "DeleteData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ToolStripButton2 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Call DeleteData()
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
    ''' <summary>
    ''' Handles the Click event of the AddToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub AddToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click
        frmAddEquipment.MdiParent = Me.MdiParent
        frmAddEquipment.FromView = True
        frmAddEquipment.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the DeleteToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Call DeleteData()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the EditToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim frmNew As New frmEditEquipment
        frmNew.MdiParent = Me.MdiParent
        frmNew.EID = ItemID
        frmNew.FromView = True
        frmNew.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ToolStripButton4 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_List_Equipment.MdiParent = Me.MdiParent
        frmReport_List_Equipment.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the BindingContextChanged event of the DataGridView1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.BindingContextChanged
        If DataGridView1.DataSource Is Nothing Then
            Return
        End If
        DataGridView1.AutoResizeColumns()
    End Sub
End Class