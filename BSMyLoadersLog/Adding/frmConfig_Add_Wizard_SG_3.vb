Imports System.Data
Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers

Namespace Adding

    ''' <summary>
    ''' Class frmConfig_Add_Wizard_SG_3.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmConfigAddWizardSg3
        ' TODO: #20 Code Cleanup
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim errOut as String
        ''' <summary>
        ''' The configuration name
        ''' </summary>
        Public ConfigName As String
        ''' <summary>
        ''' The cal identifier
        ''' </summary>
        Public CaliberId As Long
        ''' <summary>
        ''' The configuration identifier
        ''' </summary>
        Public ConfigId As Long
        ''' <summary>
        ''' The cal name
        ''' </summary>
        Public CaliberName As String
        ''' <summary>
        ''' The gid
        ''' </summary>
        Public GaugeId As Long
        ''' <summary>
        ''' Handles the Load event of the frmConfig_Add_Wizard_SG_3 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmConfig_Add_Wizard_SG_3_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                List_SG_ShotType_DetailsTableAdapter.FillBy_CFG_List_Slug(MLLDataSet.List_SG_ShotType_Details, CaliberName)
                List_SG_ShotCharge_LoadsTableAdapter.Fill(MLLDataSet.List_SG_ShotCharge_Loads)
                List_SG_WADTableAdapter.FillBy_CFG_WADList(MLLDataSet.List_SG_WAD, GaugeId)
                List_SG_CaseTableAdapter.FillBy_CFG_List(MLLDataSet.List_SG_Case, GaugeId)
                ViewPrimerListTableAdapter.Fill(MLLDataSet.viewPrimerList)
                'Dim ObjAF As New AutoFillCollections.ShotGun
                'txtSource.AutoCompleteCustomSource = ObjAF.Config_Source_SG
                txtSource.AutoCompleteCustomSource = ConfigShotgun.Source(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
            Catch ex As Exception
                Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the CheckedChanged event of the chkPersonal control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub chkPersonal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPersonal.CheckedChanged
            If chkPersonal.Checked Then
                txtSource.ReadOnly = True
            Else
                txtSource.ReadOnly = False
            End If
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnNext control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
            Try
                Dim primerId As Long = cmbPrimer.SelectedValue
                Dim hullId As Long = cmdHull.SelectedValue
                Dim wadId As Long = cmdWAD.SelectedValue
                Dim slugId As Long = cmbSlug.SelectedValue
                Dim loadTypeId As Long = cmbLoadType.SelectedValue
                Dim source As String = GeneralHelpers.FluffContent(txtSource.Text)
                Dim isPersonal As Boolean = chkPersonal.Checked
                Dim iPersonal As Integer = 1
                Dim sql As String = ""
                Dim obj As New BSDatabase
                If Not isPersonal Then iPersonal = 0
                sql = "INSERT INTO Config_List_Data_SG (CLNID,ATID,CALID,PRID,CAID,WAD" & _
                      ",SCL,Source,GID,IsPersonal,LTID) VALUES(" & ConfigId & "," & CaliberId & "," & GaugeId & "," & primerId & _
                      "," & hullId & "," & wadId & "," & slugId & ",'" & _
                      source & "'," & GaugeId & "," & iPersonal & "," & loadTypeId & ")"
                obj.ConnExec(sql)
                frmConfig_Add_Wizard_SG_Powder.MdiParent = Me.MdiParent
                frmConfig_Add_Wizard_SG_Powder.ConfigID = ConfigId
                frmConfig_Add_Wizard_SG_Powder.ConfigName = ConfigName
                frmConfig_Add_Wizard_SG_Powder.CalName = CaliberName
                frmConfig_Add_Wizard_SG_Powder.CalID = CaliberId
                frmConfig_Add_Wizard_SG_Powder.FromConfigWiz = True
                frmConfig_Add_Wizard_SG_Powder.GID = GaugeId
                frmConfig_Add_Wizard_SG_Powder.Show()
                Me.Close()
            Catch ex As Exception
                Call LogError(Me.Name, "btnNext_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace