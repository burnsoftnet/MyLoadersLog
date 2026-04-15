'Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.ConfigSheets
Imports BurnSoft.Applications.MLL.Helpers

Namespace Adding
    ' TODO #20 Code Clean Up
    ''' <summary>
    ''' Class frmConfig_Add_Wizard.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmConfigAddWizard
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut as String
        ''' <summary>
        ''' Handles the Load event of the frmConfig_Add control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmConfig_Add_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                List_CalibersTableAdapter.Fill(MLLDataSet.List_Calibers)
                'Dim Obj As New AutoFillCollections
                'txtConfigID.AutoCompleteCustomSource = Obj.ConfigName
                txtConfigID.AutoCompleteCustomSource = ConfigMetalic.ConfigName(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                chkShotgun.Enabled = UseShotgun
                If LoaderTypeShotGun And LoaderTypeMetalic Then
                    chkShotgun.Checked = False
                    chkRP.Checked = False
                ElseIf LoaderTypeMetalic And Not LoaderTypeShotGun Then
                    chkShotgun.Checked = False
                    chkRP.Checked = True
                    chkShotgun.Enabled = False
                ElseIf Not LoaderTypeMetalic And LoaderTypeShotGun Then
                    chkShotgun.Checked = True
                    chkRP.Checked = False
                    chkRP.Enabled = False
                End If
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnNext control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
            Try
                Dim configName As String = GeneralHelpers.FluffContent(txtConfigID.Text)
                Dim isMetallic As Boolean = chkRP.Checked
                Dim isShotgun As Boolean = chkShotgun.Checked
                Dim caliberId As Long = cmbCal.SelectedValue
                'Dim loadType As Integer = 0
                'If isShotgun Then loadType = 1
                If Not GeneralHelpers.IsRequired(configName, "Configuration ID", Text) Then Exit Sub
                
                'Dim Obj As New BSDatabase
                'Dim ObjG As New GlobalFunctions
                'Dim SQL As String = ""
                Dim configId As Long = 0
                If ConfigListDataName.DataExists(DatabasePath, configName, _errOut) Then
                    Dim sAns As String = MsgBox($"{configName} already exists! {Environment.NewLine} Do you wish to overwrite?", MsgBoxStyle.YesNo)
                    If sAns = vbNo Then Exit Sub
                    configId = ConfigListDataName.GetId(DatabasePath, configName, _errOut)
                    If _errOut.Length > 0 Then Throw New Exception(_errOut)
                    If Not ConfigListDataName.Delete(DatabasePath, configId, _errOut) Then Throw New Exception(_errOut)
                Else 
                    If Not ConfigListDataName.Add(DatabasePath, configName, True, isShotgun, "  ", 
                                                  True, False, _errOut) Then Throw New Exception(_errOut)
                End If
                'If ObjG.ObjectExistsinDB(configName, "ConfigName", "Config_List_Name") Then
                '    Dim sAns As String = MsgBox(configName & " already exists!" & Chr(10) & "Do you wish to overwrite?", MsgBoxStyle.YesNo)
                '    If sAns = vbYes Then
                '        configId = ObjG.GetID("SELECT * from Config_list_Name where ConfigName='" & configName & "'")
                '        SQL = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & configId
                '        Obj.ConnExec(SQL)
                '        SQL = "DELETE from Config_List_Data_NSG where CLNID=" & configId
                '        Obj.ConnExec(SQL)
                '    Else
                '        Exit Sub
                '    End If
                'Else
                '    SQL = "INSERT INTO Config_List_Name(ConfigName,IsPersonal,IsShotGun) VALUES('" & _
                '          configName & "',1," & loadType & ")"
                '    Obj.ConnExec(SQL)
                '    configId = ObjG.GetID("SELECT * from Config_list_Name where ConfigName='" & configName & "'")
                'End If
                If isMetallic Then
                    FrmConfigAddWizardRp1.ConfigName = configName
                    FrmConfigAddWizardRp1.CaliberId = caliberId
                    FrmConfigAddWizardRp1.ConfigId = configId
                    FrmConfigAddWizardRp1.MdiParent = MdiParent
                    FrmConfigAddWizardRp1.Show()
                    Close()
                End If
                If isShotgun Then
                    FrmConfigAddWizardSg1.ConfigName = configName
                    FrmConfigAddWizardSg1.CalID = caliberId
                    FrmConfigAddWizardSg1.ConfigID = configId
                    FrmConfigAddWizardSg1.CalName = cmbCal.Text
                    FrmConfigAddWizardSg1.MdiParent = MdiParent
                    FrmConfigAddWizardSg1.Show()
                    Close()
                End If
            Catch ex As Exception
                Call LogError(Name, "btnNext", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the CheckedChanged event of the chkRP control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub chkRP_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRP.CheckedChanged
            If chkShotgun.Checked Then chkShotgun.Checked = False
        End Sub
        ''' <summary>
        ''' Handles the CheckedChanged event of the chkShotgun control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub chkShotgun_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShotgun.CheckedChanged
            If chkRP.Checked Then chkRP.Checked = False
        End Sub
    End Class
End NameSpace