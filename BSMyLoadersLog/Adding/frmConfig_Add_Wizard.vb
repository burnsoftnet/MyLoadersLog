Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers

Namespace Adding
    ''' <summary>
    ''' Class frmConfig_Add_Wizard.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmConfigAddWizard
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim errOut as String
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
                txtConfigID.AutoCompleteCustomSource = ConfigMetalic.ConfigName(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
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
                Dim strConfigName As String = GeneralHelpers.FluffContent(txtConfigID.Text)
                Dim bRP As Boolean = chkRP.Checked
                Dim bSG As Boolean = chkShotgun.Checked
                Dim lngCal As Long = cmbCal.SelectedValue
                Dim LoadType As Integer = 0
                If bSG Then LoadType = 1
                If Not GeneralHelpers.IsRequired(strConfigName, "Configuration ID", Text) Then Exit Sub
                Dim Obj As New BSDatabase
                Dim ObjG As New GlobalFunctions
                Dim SQL As String = ""
                Dim MyID As Long = 0
                If ObjG.ObjectExistsinDB(strConfigName, "ConfigName", "Config_List_Name") Then
                    Dim sAns As String = MsgBox(strConfigName & " already exists!" & Chr(10) & "Do you wish to overwrite?", MsgBoxStyle.YesNo)
                    If sAns = vbYes Then
                        MyID = ObjG.GetID("SELECT * from Config_list_Name where ConfigName='" & strConfigName & "'")
                        SQL = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & MyID
                        Obj.ConnExec(SQL)
                        SQL = "DELETE from Config_List_Data_NSG where CLNID=" & MyID
                        Obj.ConnExec(SQL)
                    Else
                        Exit Sub
                    End If
                Else
                    SQL = "INSERT INTO Config_List_Name(ConfigName,IsPersonal,IsShotGun) VALUES('" & _
                          strConfigName & "',1," & LoadType & ")"
                    Obj.ConnExec(SQL)
                    MyID = ObjG.GetID("SELECT * from Config_list_Name where ConfigName='" & strConfigName & "'")
                End If
                If bRP Then
                    frmConfig_Add_Wizard_RP_1.ConfigName = strConfigName
                    frmConfig_Add_Wizard_RP_1.CalID = lngCal
                    frmConfig_Add_Wizard_RP_1.ConfigID = MyID
                    frmConfig_Add_Wizard_RP_1.MdiParent = MdiParent
                    frmConfig_Add_Wizard_RP_1.Show()
                    Close()
                End If
                If bSG Then
                    frmConfig_Add_Wizard_SG_1.ConfigName = strConfigName
                    frmConfig_Add_Wizard_SG_1.CalID = lngCal
                    frmConfig_Add_Wizard_SG_1.ConfigID = MyID
                    frmConfig_Add_Wizard_SG_1.CalName = cmbCal.Text
                    frmConfig_Add_Wizard_SG_1.MdiParent = MdiParent
                    frmConfig_Add_Wizard_SG_1.Show()
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