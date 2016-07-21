Imports BSMyLoadersLog.LoadersClass
Public Class frmView_List_Shot
    Const RegViewName As String = "View_Shot"
    Public Sub LoadData()
        Try
            Dim sValue As String = LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
            Select Case LCase(sValue)
                Case LCase("All")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Instock")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_In_Stock(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Out-Of-Stock")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_Out_Of_Stock(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Reference")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_Referance(Me.MLLDataSet.List_SG_ShotType_Details)
                Case Else
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Shot_All(Me.MLLDataSet.List_SG_ShotType_Details)
            End Select
        Catch ex As Exception
            Dim strProcedure As String = "LoadData"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub DeleteShot()
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strSQLTable As String = "List_SG_ShotType_Details"
            Dim strName As String = ObjG.GetName("SELECT * from " & strSQLTable & " where ID=" & ItemID, "Name")
            Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
            If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
        Catch ex As Exception
            Dim strProcedure As String = "DeleteShot"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmView_List_Shot_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim ObjR As New BSRegistry
        Call ObjR.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString)
    End Sub
    Private Sub frmViewShotList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ObjR As New BSRegistry
        ToolStripComboBox1.Text = ObjR.GetViewSettings(RegViewName, "All")
        Call LoadData()
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Call LoadData()
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim frmNew As New frmAddShot
        frmNew.FromView = True
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Call DeleteShot()
    End Sub
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_ShotInventory.MdiParent = Me.MdiParent
        frmReport_ShotInventory.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadData()
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Call DeleteShot()
    End Sub
    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim frmNew As New frmEditShot
            frmNew.BID = ItemID
            frmNew.FromView = True
            frmNew.MdiParent = Me.MdiParent
            frmNew.Show()
        Catch ex As Exception
            Dim strProcedure As String = "EditToolStripMenuItem_Click"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub OutOfStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutOfStockToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim SQL As String = "UPDATE List_SG_ShotType_Details set weight=0 where ID=" & ItemID
        Dim Obj As New BSDatabase
        Obj.ConnExec(SQL)
        Call LoadData()
    End Sub
    Private Sub AddToQtyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToQtyToolStripMenuItem.Click
        Dim frmNew As New frmAddQtyShot
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        frmNew.BID = ItemID
        frmNew.FromView = True
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
    Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.BindingContextChanged
        If DataGridView1.DataSource Is Nothing Then
            Return
        End If
        DataGridView1.AutoResizeColumns()
    End Sub
End Class