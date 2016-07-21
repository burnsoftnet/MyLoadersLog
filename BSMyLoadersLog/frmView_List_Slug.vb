Imports BSMyLoadersLog.LoadersClass
Public Class frmView_List_Slug
    Const RegViewName As String = "View_Slug"
#Region "Subs and Functions"
    Public Sub LoadData()
        Try
            Dim sValue As String = LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
            Select Case LCase(sValue)
                Case LCase("All")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_All(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Instock")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Instock_Manu(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Out-Of-Stock")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_OutOfstock_Manu(Me.MLLDataSet.List_SG_ShotType_Details)
                Case LCase("Reference")
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Ref_Manu(Me.MLLDataSet.List_SG_ShotType_Details)
                Case Else
                    Me.List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_All(Me.MLLDataSet.List_SG_ShotType_Details)
            End Select
        Catch ex As Exception
            Dim strProcedure As String = "LoadData"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub DeleteSlug()
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
            Dim strProcedure As String = "DeleteSlug"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub
#End Region
#Region "Form & DataGRid Subs"
    Private Sub frmView_List_Slug_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim ObjR As New BSRegistry
        Call ObjR.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString)
    End Sub
    Private Sub frmView_List_Slug_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ObjR As New BSRegistry
        ToolStripComboBox1.Text = ObjR.GetViewSettings(RegViewName, "All")
        Call LoadData()
    End Sub
    Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.BindingContextChanged
        If DataGridView1.DataSource Is Nothing Then
            Return
        End If
        DataGridView1.AutoResizeColumns()
    End Sub
#End Region
#Region "Tool Strip Subs"
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        frmAddSlugs.MdiParent = Me.MdiParent
        frmAddSlugs.FromView = True
        frmAddSlugs.Show()
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Call LoadData()
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Call DeleteSlug()
    End Sub
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        frmReport_SlugInventory.MdiParent = Me.MdiParent
        frmReport_SlugInventory.Show()
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadData()
    End Sub
#End Region
#Region "Menu Subs"
    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        frmEditSlug.MdiParent = Me.MdiParent
        frmEditSlug.FromView = True
        frmEditSlug.SID = ItemID
        frmEditSlug.Show()
    End Sub
    Private Sub AddToQtyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToQtyToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        frmAddQtySlug.MdiParent = Me.MdiParent
        frmAddQtySlug.fromview = True
        frmAddQtySlug.SID = ItemID
        frmAddQtySlug.Show()
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Call DeleteSlug()
    End Sub
    Private Sub OutOfStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutOfStockToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim SQL As String = "UPDATE List_SG_ShotType_Details set qty=0 where ID=" & ItemID
        Dim Obj As New BSDatabase
        Obj.ConnExec(SQL)
        Call LoadData()
    End Sub
#End Region
End Class