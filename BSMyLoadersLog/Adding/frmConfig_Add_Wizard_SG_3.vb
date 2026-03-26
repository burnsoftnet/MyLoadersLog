Imports System.Data
Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers

Public Class frmConfig_Add_Wizard_SG_3
    ''' <summary>
    ''' The error out
    ''' </summary>
    Dim errOut as String
    Public ConfigName As String
    Public CalID As Long
    Public ConfigID As Long
    Public CalName As String
    Public GID As Long
    Private Sub frmConfig_Add_Wizard_SG_3_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            List_SG_ShotType_DetailsTableAdapter.FillBy_CFG_List_Slug(MLLDataSet.List_SG_ShotType_Details, CalName)
            List_SG_ShotCharge_LoadsTableAdapter.Fill(MLLDataSet.List_SG_ShotCharge_Loads)
            List_SG_WADTableAdapter.FillBy_CFG_WADList(MLLDataSet.List_SG_WAD, GID)
            List_SG_CaseTableAdapter.FillBy_CFG_List(MLLDataSet.List_SG_Case, GID)
            ViewPrimerListTableAdapter.Fill(MLLDataSet.viewPrimerList)
            'Dim ObjAF As New AutoFillCollections.ShotGun
            'txtSource.AutoCompleteCustomSource = ObjAF.Config_Source_SG
            txtSource.AutoCompleteCustomSource = ConfigShotgun.Source(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub chkPersonal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPersonal.CheckedChanged
        If chkPersonal.Checked Then
            txtSource.ReadOnly = True
        Else
            txtSource.ReadOnly = False
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
        Try
            Dim PID As Long = cmbPrimer.SelectedValue
            Dim HID As Long = cmdHull.SelectedValue
            Dim WID As Long = cmdWAD.SelectedValue
            Dim STID As Long = cmbSlug.SelectedValue
            Dim LTID As Long = cmbLoadType.SelectedValue
            Dim sSource As String = GeneralHelpers.FluffContent(txtSource.Text)
            Dim IsPersonal As Boolean = chkPersonal.Checked
            Dim iPersonal As Integer = 1
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            If Not IsPersonal Then iPersonal = 0
            SQL = "INSERT INTO Config_List_Data_SG (CLNID,ATID,CALID,PRID,CAID,WAD" & _
                    ",SCL,Source,GID,IsPersonal,LTID) VALUES(" & ConfigID & "," & CalID & "," & GID & "," & PID & _
                    "," & HID & "," & WID & "," & STID & ",'" & _
                    sSource & "'," & GID & "," & iPersonal & "," & LTID & ")"
            Obj.ConnExec(SQL)
            frmConfig_Add_Wizard_SG_Powder.MdiParent = Me.MdiParent
            frmConfig_Add_Wizard_SG_Powder.ConfigID = ConfigID
            frmConfig_Add_Wizard_SG_Powder.ConfigName = ConfigName
            frmConfig_Add_Wizard_SG_Powder.CalName = CalName
            frmConfig_Add_Wizard_SG_Powder.CalID = CalID
            frmConfig_Add_Wizard_SG_Powder.FromConfigWiz = True
            frmConfig_Add_Wizard_SG_Powder.GID = GID
            frmConfig_Add_Wizard_SG_Powder.Show()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnNext_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class