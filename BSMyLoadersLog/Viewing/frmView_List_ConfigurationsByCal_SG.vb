Imports BSMyLoadersLog.LoadersClass

Namespace Viewing
    ''' <summary>
    ''' Class FrmViewListConfigurationsByCalSg.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmViewListConfigurationsByCalSg
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim errOut as String
        ''' <summary>
        ''' The calid
        ''' </summary>
        Public CaliberId As Long
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            Try
                ConfigListSimpleSGBindingSource.ResetBindings(True)
                Dim selectedView As String = ToolStripComboBox1.SelectedItem.ToString
                Select Case UCase(selectedView)
                    Case UCase("All")
                        ConfigList_Simple_SGTableAdapter.FillBy_Caliber(MLLDataSet.ConfigList_Simple_SG, CaliberId)
                    Case UCase("Active Only")
                        ConfigList_Simple_SGTableAdapter.FillBy_Active(MLLDataSet.ConfigList_Simple_SG, CaliberId)
                    Case UCase("Inactive Only")
                        ConfigList_Simple_SGTableAdapter.FillBy_Inactive(MLLDataSet.ConfigList_Simple_SG, CaliberId)
                    Case UCase("All Favorites")
                        ConfigList_Simple_SGTableAdapter.FillBy_Fav(MLLDataSet.ConfigList_Simple_SG, CaliberId)
                    Case UCase("Personal Loads")
                        ConfigList_Simple_SGTableAdapter.FillBy_Personal(MLLDataSet.ConfigList_Simple_SG, CaliberId)
                    Case UCase("Reffered Loads")
                        ConfigList_Simple_SGTableAdapter.FillBy_NonPersonal(MLLDataSet.ConfigList_Simple_SG, CaliberId)
                    Case Else
                        ConfigList_Simple_SGTableAdapter.FillBy_Caliber(MLLDataSet.ConfigList_Simple_SG, CaliberId)
                        'ConfigList_Simple_SGTableAdapter.Fill(MLLDataSet.ConfigList_Simple_SG)
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
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub lstConfigSheets_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstConfigSheets.DoubleClick
            Try
                Dim lngConfigId As Long = lstConfigSheets.SelectedValue
                Dim frmNew As New frmView_Configuration_Shotgun_Sheet
                frmNew.ConfigID = lngConfigId
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
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
            Try
                frmConfig_Add_Wizard.MdiParent = MdiParent
                frmConfig_Add_Wizard.Show()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton1_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton2.Click
            Try
                Dim lngConfigId As Long = lstConfigSheets.SelectedValue
                Dim obj As New BSDatabase
                Dim objG As New GlobalFunctions
                Dim strName As String = objG.GetName("SELECT * from Config_List_Name where ID=" & lngConfigId, "ConfigName")
                Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                Dim SQL As String = "DELETE from Config_List_Powder_Data_SG where CLNID=" & lngConfigId
                If strAns = vbYes Then
                    obj.ConnExec(SQL)
                    SQL = "DELETE from Config_List_Data_SG where CLNID=" & lngConfigId
                    obj.ConnExec(SQL)
                    SQL = "DELETE from Config_List_Name where ID=" & lngConfigId
                    obj.ConnExec(SQL)
                    Call LoadData()
                    Call MDIParentMain.RefreshConfigData()
                End If
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton2_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the ToolStripComboBox1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton3 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton3.Click
            Call LoadData()
        End Sub
    End Class
End NameSpace