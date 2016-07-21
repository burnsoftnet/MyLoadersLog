Imports BSMyLoadersLog.LoadersClass
Public Class frmView_List_WADS
    Const RegViewName As String = "View_WADS"
    Public Sub LoadData()
        Try
            Select Case LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
                Case LCase("All")
                    Me.List_SG_WADTableAdapter.FillBy_Manufacturer(Me.MLLDataSet.List_SG_WAD)
                Case LCase("Instock")
                    Me.List_SG_WADTableAdapter.FillBy_In_Stock(Me.MLLDataSet.List_SG_WAD)
                Case LCase("Out-Of-Stock")
                    Me.List_SG_WADTableAdapter.FillBy_Out_Of_Stock(Me.MLLDataSet.List_SG_WAD)
                Case LCase("Reference")
                    Me.List_SG_WADTableAdapter.FillBy_Referance(Me.MLLDataSet.List_SG_WAD)
                Case Else
                    Me.List_SG_WADTableAdapter.Fill(Me.MLLDataSet.List_SG_WAD)
            End Select
        Catch ex As Exception
            Dim strProcedure As String = "LoadData"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub DeleteData()
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strSQLTable As String = "List_SG_WAD"
            Dim strName As String = ObjG.GetName("SELECT * from " & strSQLTable & " where ID=" & ItemID, "WAD")
            Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
            If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
        Catch ex As Exception
            Dim strProcedure As String = "DeleteData"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub AddWAD()
        Dim frmNew As New frmAddWad
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
    Private Sub frmView_List_WADS_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim ObjR As New BSRegistry
        Call ObjR.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString)
    End Sub
    Private Sub frmView_List_WADS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Call AddWAD()
    End Sub
    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim frmNew As New frmEditWADS
        frmNew.MdiParent = Me.MdiParent
        frmNew.SID = ItemID
        frmNew.FromView = True
        frmNew.Show()
    End Sub
    Private Sub AddToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click
        Call AddWAD()
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Call DeleteData()
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Call DeleteData()
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Call LoadData()
    End Sub
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_WADInventory.MdiParent = Me.MdiParent
        frmReport_WADInventory.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadData()
    End Sub

    Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.BindingContextChanged
        If DataGridView1.DataSource Is Nothing Then
            Return
        End If
        DataGridView1.AutoResizeColumns()
    End Sub

    Private Sub AddToQtyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToQtyToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim frmNew As New frmAddQtyWAD
        frmNew.FromView = True
        frmNew.WID = ItemID
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub

    Private Sub MarkAsOutOfStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarkAsOutOfStockToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim SQL As String = "UPDATE List_SG_WAD set qty=0 where ID=" & ItemID
        Dim Obj As New BSDatabase
        Obj.ConnExec(SQL)
        Call LoadData()
    End Sub
End Class