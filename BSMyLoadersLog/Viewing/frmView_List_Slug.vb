Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Inventory

Namespace Viewing
    ' TODO: #20 clean up code
    ''' <summary>
    ''' Class FrmViewListSlug.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmViewListSlug
        ''' <summary>
        ''' The error out
        ''' </summary>
        Private _errOut as String
        ''' <summary>
        ''' Registry Selected View Settings
        ''' </summary>
        Const RegViewName As String = "View_Slug"
#Region "Subs and Functions"
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            Try
                Dim sValue As String = LCase(UCase(ToolStripComboBox1.SelectedItem.ToString))
                Select Case LCase(sValue)
                    Case LCase("All")
                        List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_All(MLLDataSet.List_SG_ShotType_Details)
                    Case LCase("Instock")
                        List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Instock_Manu(MLLDataSet.List_SG_ShotType_Details)
                    Case LCase("Out-Of-Stock")
                        List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_OutOfstock_Manu(MLLDataSet.List_SG_ShotType_Details)
                    Case LCase("Reference")
                        List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_Ref_Manu(MLLDataSet.List_SG_ShotType_Details)
                    Case Else
                        List_SG_ShotType_DetailsTableAdapter.FillBy_Slug_All(MLLDataSet.List_SG_ShotType_Details)
                End Select
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Deletes the slug.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Private Sub DeleteSlug()
            Try
                Dim itemId As long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                'Dim obj As New BSDatabase
                'Dim objG As New GlobalFunctions
                'Dim strSqlTable As String = "List_SG_ShotType_Details"
                'Dim strName As String = objG.GetName("SELECT * from " & strSqlTable & " where ID=" & itemId, "Name")
                'Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim sql As String = "DELETE from " & strSqlTable & " where ID=" & itemId
                'If strAns = vbYes Then obj.ConnExec(sql) : Call LoadData()
                Dim strName As String = ShotgunShotTypeInventory.GetName(DatabasePath, itemId, _errOut)
                if _errOut.Length > 0 Then Throw New Exception(_errOut)
                Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                    
                If strAns = vbYes Then
                    If Not ShotgunShotTypeInventory.Delete(DatabasePath, itemId, _errOut) then Throw new Exception(_errOut)
                    Call LoadData()
                End If
            Catch ex As Exception
                Call LogError(Name, "DeleteSlug", Err.Number, ex.Message.ToString)
            End Try
        End Sub
#End Region
#Region "Form & DataGRid Subs"
        ''' <summary>
        ''' Handles the FormClosing event of the frmView_List_Slug control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmView_List_Slug_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
            'Dim ObjR As New BSRegistry
            'Call ObjR.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString)
            Try
                'Dim ObjR As New BSRegistry
                'Call ObjR.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString)
                If Not MyRegistry.SaveViewSettings(RegViewName, ToolStripComboBox1.SelectedItem.ToString, 
                                                   _errOut) Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "frmView_List_Slug_FormClosing", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmView_List_Slug control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmView_List_Slug_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
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
                Call LogError(Name, "frmView_List_Slug_Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the BindingContextChanged event of the DataGridView1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DataGridView1.BindingContextChanged
            try
                If DataGridView1.DataSource Is Nothing Then
                    Return
                End If
                DataGridView1.AutoResizeColumns()
            Catch ex As Exception
                Call LogError(Name, "DataGridView1_BindingContextChanged", Err.Number, ex.Message.ToString)
            End Try
        End Sub
#End Region
#Region "Tool Strip Subs"
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
            try
                frmAddSlugs.MdiParent = MdiParent
                frmAddSlugs.FromView = True
                frmAddSlugs.Show()
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
            Call DeleteSlug()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton4 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton4.Click
            Try
                frmReport_SlugInventory.MdiParent = MdiParent
                frmReport_SlugInventory.Show()
            Catch ex As Exception
                Call LogError(Name, "", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the ToolStripComboBox1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
            Call LoadData()
        End Sub
#End Region
#Region "Menu Subs"
        ''' <summary>
        ''' Handles the Click event of the EditToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub EditToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditToolStripMenuItem.Click
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                frmEditSlug.MdiParent = MdiParent
                frmEditSlug.FromView = True
                frmEditSlug.SID = itemId
                frmEditSlug.Show()
            Catch ex As Exception
                Call LogError(Name, "", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the AddToQtyToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub AddToQtyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddToQtyToolStripMenuItem.Click
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                frmAddQtySlug.MdiParent = MdiParent
                frmAddQtySlug.fromview = True
                frmAddQtySlug.SlugId = itemId
                frmAddQtySlug.Show()
            Catch ex As Exception
                Call LogError(Name, "", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the DeleteToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
            Call DeleteSlug()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the OutOfStockToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub OutOfStockToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OutOfStockToolStripMenuItem.Click
            Try
                Dim itemId As long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                If Not ShotgunShotTypeInventory.UpdateSlugQty(DatabasePath, itemId, 0, _errOut) Then Throw new Exception(_errOut)
                'Dim SQL As String = "UPDATE List_SG_ShotType_Details set qty=0 where ID=" & itemId
                'Dim Obj As New BSDatabase
                'Obj.ConnExec(SQL)
                Call LoadData()
            Catch ex As Exception
                Call LogError(Name, "", Err.Number, ex.Message.ToString)
            End Try
        End Sub
#End Region
    End Class
End NameSpace