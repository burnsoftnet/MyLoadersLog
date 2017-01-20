Imports BSMyLoadersLog.LoadersClass
Public Class frmView_List_ShellHulls
    Const RegViewName As String = "View_ShellHulls"
    Public Sub LoadData()
        Try
            Me.List_SG_CaseTableAdapter.Fill(Me.MLLDataSet.List_SG_Case)
            Select Case LCase(ToolStripComboBox1.SelectedItem.ToString)
                Case LCase("All")
                    Me.List_SG_CaseTableAdapter.Fill(Me.MLLDataSet.List_SG_Case)
                Case LCase("Instock")
                    'Me.List_SG_CaseTableAdapter.
                    Me.List_SG_CaseTableAdapter.FillBy_Menu_InStock(Me.MLLDataSet.List_SG_Case)
                Case LCase("Out-Of-Stock")
                    Me.List_SG_CaseTableAdapter.FillBy_Menu_OutOfStock(Me.MLLDataSet.List_SG_Case)
                Case LCase("Reference")
                    Me.List_SG_CaseTableAdapter.FillBy_Menu_Refferance(Me.MLLDataSet.List_SG_Case)
                Case Else
                    Me.List_SG_CaseTableAdapter.Fill(Me.MLLDataSet.List_SG_Case)
            End Select
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub DeleteData()
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strSQLTable As String = "List_SG_Case"
            Dim strName As String = ObjG.GetName("SELECT * from " & strSQLTable & " where ID=" & ItemID, "Name")
            Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
            If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
        Catch ex As Exception
            Call LogError(Me.Name, "DeleteData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmView_List_ShellHulls_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Call LoadData()
    End Sub
    Sub ADDShell()
        Dim frmNew As New frmAddShell
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Call ADDShell()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Call DeleteData()
    End Sub

    Private Sub AddToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click
        Call ADDShell()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Call DeleteData()
    End Sub
    Sub EditHulls()
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim frmNew As New frmEditHulls
        frmNew.MdiParent = Me.MdiParent
        frmNew.SID = ItemID
        frmNew.FromView = True
        frmNew.Show()
    End Sub

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Call EditHulls()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_HullInventory.MdiParent = Me.MdiParent
        frmReport_HullInventory.Show()
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.BindingContextChanged
        If DataGridView1.DataSource Is Nothing Then
            Return
        End If
        DataGridView1.AutoResizeColumns()
    End Sub

    Private Sub AddToQtyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddToQtyToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim frmNew As New frmAddQtyHulls
        frmNew.MdiParent = Me.MdiParent
        frmNew.SID = ItemID
        frmNew.FromView = True
        frmNew.Show()
    End Sub

    Private Sub MarkAsOutOfStockToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MarkAsOutOfStockToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim SQL As String = "UPDATE List_SG_Case set QTY=0 where ID=" & ItemID
        Dim Obj As New BSDatabase
        Obj.ConnExec(SQL)
        Call LoadData()
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadData()
    End Sub
End Class