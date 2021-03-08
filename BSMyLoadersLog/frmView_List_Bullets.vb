Imports BSMyLoadersLog.LoadersClass
Public Class frmView_List_Bullets
    Const RegViewName As String = "View_Bullets"
    Public Sub LoadData()
        Try
            Select Case LCase(ToolStripComboBox1.SelectedItem.ToString)
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
    Private Sub frmView_List_Bullets_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim ObjR As New BSRegistry
        Call ObjR.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString)
    End Sub
    Private Sub frmView_List_Bullets_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Height <> 0 Then
            Me.DataGridView1.Height = Me.Height - (65)
            Me.DataGridView1.Width = Me.Width - 15
        End If
    End Sub
    Private Sub frmView_List_Bullets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ObjR As New BSRegistry
        ToolStripComboBox1.Text = ObjR.GetViewSettings(RegViewName, "All")
        Call LoadData()

    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim frmNew As New frmAddBullets
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
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
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Call DeleteBullet()
        Catch ex As Exception
            Call LogError(Me.Name, "ToolStripButton2.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Call LoadData()
    End Sub
    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim frmNew As New frmEditBullets
        frmNew.BID = ItemID
        frmNew.FromView = True
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
    Private Sub AddToQtyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToQtyToolStripMenuItem.Click
        Dim frmNew As New frmAddQtyBullets
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        frmNew.BID = ItemID
        frmNew.FromView = True
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Call DeleteBullet()
    End Sub
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_BulletInventory.MdiParent = Me.MdiParent
        frmReport_BulletInventory.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadData()
    End Sub
    Private Sub OutOfStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutOfStockToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim SQL As String = "UPDATE List_Bullets set QTY=0 where ID=" & ItemID
        Dim Obj As New BSDatabase
        Obj.ConnExec(SQL)
        Call LoadData()
    End Sub

    Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.BindingContextChanged
        If DataGridView1.DataSource Is Nothing Then
            Return
        End If
        DataGridView1.AutoResizeColumns()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim frmNew As New frmAddBullets
        frmNew.DoCopy = True
        frmNew.Bid = ItemID
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
End Class