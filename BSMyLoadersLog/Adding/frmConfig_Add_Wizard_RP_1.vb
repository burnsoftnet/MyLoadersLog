Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.ConfigSheets
Imports BurnSoft.Applications.MLL.Helpers

Namespace Adding
    ''' <summary>
    ''' Class frmConfig_Add_Wizard_RP_1.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmConfigAddWizardRp1
        ' TODO: #20 Code Cleanup
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim errOut as String
        ''' <summary>
        ''' The cal identifier
        ''' </summary>
        Public CalID As Long
        ''' <summary>
        ''' The configuration name
        ''' </summary>
        Public ConfigName As String
        ''' <summary>
        ''' The configuration identifier
        ''' </summary>
        Public ConfigID As Long
        ''' <summary>
        ''' The is personal
        ''' </summary>
        Dim isPersonal As Boolean
        ''' <summary>
        ''' Handles the Load event of the frmConfig_Add_Wizard_RP_1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmConfig_Add_Wizard_RP_1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                General_Ammunition_TypeTableAdapter.Fill(MLLDataSet.General_Ammunition_Type)
                isPersonal = True
                List_CaseTableAdapter.FillBy_CALID(MLLDataSet.List_Case, CalID)
                General_PrimerTableAdapter.Fill(MLLDataSet.General_Primer)
                List_BulletsTableAdapter.FillBy_CALID(MLLDataSet.List_Bullets, CalID)
                If isPersonal Then
                    txtLoad.Enabled = False
                    chkPersonal.Checked = True
                End If
                'Dim ObjAF As New AutoFillCollections
                'txtLoad.AutoCompleteCustomSource = ObjAF.Config_Source_NSG
                txtLoad.AutoCompleteCustomSource = ConfigMetalic.Source(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the CheckedChanged event of the chkPersonal control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub chkPersonal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPersonal.CheckedChanged
            If chkPersonal.Checked Then
                txtLoad.Enabled = False
                chkBook.Checked = False
            End If
        End Sub
        ''' <summary>
        ''' Handles the CheckedChanged event of the chkBook control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub chkBook_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkBook.CheckedChanged
            If chkBook.Checked Then
                txtLoad.Enabled = True
                chkPersonal.Checked = False
            End If
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnAdd control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Try
                Dim lngBullet As Long = cmbBullet.SelectedValue
                Dim lngPrimer As Long = cmbPrimer.SelectedValue
                Dim lngCase As Long = cmbCase.SelectedValue
                Dim lngAmmoType As Long = cmbAmmo.SelectedValue
                'Dim bPersonal As Boolean = chkPersonal.Checked
                'Dim bOther As Boolean = chkBook.Checked
                'Dim SQL As String = ""
                'Dim Obj As New BSDatabase
                Dim strSource As String = GeneralHelpers.FluffContent(txtLoad.Text)
                If chkBook.Checked Then
                    ' TODO: #19 Update when newer library is applied
                    'If Not ConfigListDataName.SetPersonal(DatabasePath, ConfigID, false, errOut) Then Throw New Exception(errOut)
                    'SQL = "UPDATE Config_List_Name set IsPersonal=0 where id=" & ConfigID
                    'Obj.ConnExec(SQL)
                End If

                If Not ConfigListDataMetalic.Add(DatabasePath, ConfigID, lngAmmoType, CalID, 
                                                 lngBullet, lngPrimer, lngCase, strSource, 
                                                 errOut) Then Throw New Exception(errOut)

                'SQL = "INSERT INTO Config_List_Data_NSG(CLNID,ATID,CALID,BID,PRID,CAID,Source) VALUES (" & _
                '      ConfigID & "," & lngAmmoType & "," & CalID & "," & lngBullet & "," & _
                '      lngPrimer & "," & lngCase & ",'" & strSource & "')"
                'Obj.ConnExec(SQL)
                FrmConfigAddWizardPowder.ConfigId = ConfigID
                FrmConfigAddWizardPowder.ConfigName = ConfigName
                FrmConfigAddWizardPowder.FromConfigWiz = True
                FrmConfigAddWizardPowder.MdiParent = MdiParent
                FrmConfigAddWizardPowder.Show()
                Close()
            Catch ex As Exception
                Call LogError(Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace