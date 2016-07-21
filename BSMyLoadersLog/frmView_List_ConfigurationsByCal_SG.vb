Imports BSMyLoadersLog.LoadersClass
Public Class frmView_List_ConfigurationsByCal_SG
    Public CALID As Long
    Public Sub LoadData()
        ConfigListSimpleSGBindingSource.ResetBindings(True)
        Dim SelectedView As String = ToolStripComboBox1.SelectedItem.ToString
        Select Case UCase(SelectedView)
            Case UCase("All")
                Me.ConfigList_Simple_SGTableAdapter.FillBy_Caliber(Me.MLLDataSet.ConfigList_Simple_SG, CALID)
            Case UCase("Active Only")
                Me.ConfigList_Simple_SGTableAdapter.FillBy_Active(Me.MLLDataSet.ConfigList_Simple_SG, CALID)
            Case UCase("Inactive Only")
                Me.ConfigList_Simple_SGTableAdapter.FillBy_Inactive(Me.MLLDataSet.ConfigList_Simple_SG, CALID)
            Case UCase("All Favorites")
                Me.ConfigList_Simple_SGTableAdapter.FillBy_Fav(Me.MLLDataSet.ConfigList_Simple_SG, CALID)
            Case UCase("Personal Loads")
                Me.ConfigList_Simple_SGTableAdapter.FillBy_Personal(Me.MLLDataSet.ConfigList_Simple_SG, CALID)
            Case UCase("Reffered Loads")
                Me.ConfigList_Simple_SGTableAdapter.FillBy_NonPersonal(Me.MLLDataSet.ConfigList_Simple_SG, CALID)
            Case Else
                Me.ConfigList_Simple_SGTableAdapter.FillBy_Caliber(Me.MLLDataSet.ConfigList_Simple_SG, CALID)
                'Me.ConfigList_Simple_SGTableAdapter.Fill(Me.MLLDataSet.ConfigList_Simple_SG)
        End Select
        lstConfigSheets.Refresh()
    End Sub
    Private Sub frmView_List_ConfigurationsByCal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If CALID = 0 Then
            Dim sMsg As String = "Please Select a Caliber from the Side Caliber List!"
            MsgBox(sMsg)
            Me.Close()
        Else
            lstConfigSheets.Text = "All"
            Call LoadData()
        End If
    End Sub
    Private Sub lstConfigSheets_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstConfigSheets.DoubleClick
        Dim lngConfigID As Long = lstConfigSheets.SelectedValue
        Dim frmNew As New frmView_Configuration_Shotgun_Sheet
        frmNew.ConfigID = lngConfigID
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        frmConfig_Add_Wizard.MdiParent = Me.MdiParent
        frmConfig_Add_Wizard.Show()
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim lngConfigID As Long = lstConfigSheets.SelectedValue
        Dim Obj As New BSDatabase
        Dim ObjG As New GlobalFunctions
        Dim strName As String = ObjG.GetName("SELECT * from Config_List_Name where ID=" & lngConfigID, "ConfigName")
        Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
        Dim SQL As String = "DELETE from Config_List_Powder_Data_SG where CLNID=" & lngConfigID
        If strAns = vbYes Then
            Obj.ConnExec(SQL)
            SQL = "DELETE from Config_List_Data_SG where CLNID=" & lngConfigID
            Obj.ConnExec(SQL)
            SQL = "DELETE from Config_List_Name where ID=" & lngConfigID
            Obj.ConnExec(SQL)
            Call LoadData()
            Call MDIParentMain.RefreshConfigData()
        End If
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadData()
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Call LoadData()
    End Sub
End Class