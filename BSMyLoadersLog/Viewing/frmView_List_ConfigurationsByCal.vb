'Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Adding
Imports BurnSoft.Applications.MLL.ConfigSheets

Namespace Viewing

    ''' <summary>
    ''' Class frmView_List_ConfigurationsByCal.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmViewListConfigurationsByCal
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' The calid
        ''' </summary>
        Public CaliberId As Long
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            Try
                ConfigListSimpleBindingSource.ResetBindings(True)
                Dim selectedView As String = ToolStripComboBox1.SelectedItem.ToString
                Select Case UCase(selectedView)
                    Case UCase("All")
                        ConfigList_SimpleTableAdapter.FillBy_Caliber(MLLDataSet.ConfigList_Simple, CaliberId)
                    Case UCase("Active Only")
                        ConfigList_SimpleTableAdapter.FillBy_Active(MLLDataSet.ConfigList_Simple, CaliberId)
                    Case UCase("Inactive Only")
                        ConfigList_SimpleTableAdapter.FillBy_Inactive(MLLDataSet.ConfigList_Simple, CaliberId)
                    Case UCase("All Favorites")
                        ConfigList_SimpleTableAdapter.FillBy_Fav(MLLDataSet.ConfigList_Simple, CaliberId)
                    Case UCase("Personal Loads")
                        ConfigList_SimpleTableAdapter.FillBy_Personal(MLLDataSet.ConfigList_Simple, CaliberId)
                    Case UCase("Reffered Loads")
                        ConfigList_SimpleTableAdapter.FillBy_NonPersonal(MLLDataSet.ConfigList_Simple, CaliberId)
                    Case Else
                        ConfigList_SimpleTableAdapter.FillBy_Caliber(MLLDataSet.ConfigList_Simple, CaliberId)
                End Select
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
            lstConfigSheets.Refresh()
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmView_List_ConfigurationsByCal control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_List_ConfigurationsByCal_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                If CaliberId = 0 Then
                    Dim sMsg As String = "Please Select a Caliber from the Side Caliber List!"
                    MsgBox(sMsg)
                    Close()
                Else
                    lstConfigSheets.Text = $"All"
                    Call LoadData()
                End If
            Catch ex As Exception
                Call LogError(Name, "frmView_List_ConfigurationsByCal_Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the DoubleClick event of the lstConfigSheets control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub lstConfigSheets_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstConfigSheets.DoubleClick
            Try
                Dim lngConfigId As Long = lstConfigSheets.SelectedValue
                Dim frmNew As New FrmViewConfigurationSheet
                frmNew.ConfigId = lngConfigId
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "lstConfigSheets_DoubleClick", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
            Try
                FrmConfigAddWizard.MdiParent = MdiParent
                FrmConfigAddWizard.Show()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton1_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton2.Click
            Try
                Dim lngConfigId As Long = lstConfigSheets.SelectedValue
                'Dim Obj As New BSDatabase
                'Dim ObjG As New GlobalFunctions
                'Dim strName As String = ObjG.GetName("SELECT * from Config_List_Name where ID=" & lngConfigId, "ConfigName")
                Dim strName As String = ConfigListDataName.GetName(DatabasePath, lngConfigId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim SQL As String = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & lngConfigId
                If strAns = vbYes Then
                    If Not ConfigListDataName.Delete(DatabasePath, lngConfigId, _errOut) Then Throw New Exception(_errOut)
                    If _errOut.Length > 0 Then Throw New Exception(_errOut)
                    'Obj.ConnExec(SQL)
                    'SQL = "DELETE from Config_List_Data_NSG where CLNID=" & lngConfigId
                    'Obj.ConnExec(SQL)
                    'SQL = "DELETE from Config_List_Name where ID=" & lngConfigId
                    'Obj.ConnExec(SQL)
                    Call LoadData()
                    Call MdiParentMain.RefreshConfigData()
                End If
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton2_Click", Err.Number, ex.Message.ToString)
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
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton3 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton3.Click
            Call LoadData()
        End Sub
    End Class
End Namespace