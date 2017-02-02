Imports System.Data
Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Public Class frmConfig_Add_Wizard_SG_2
    Public ConfigName As String
    Public CalID As Long
    Public CalName As String
    Public GID As Long
    Public ConfigID As Long
    Private Sub frmConfig_Add_Wizard_SG_2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.List_SG_ShotType_DetailsTableAdapter.FillBy_CFG_List(Me.MLLDataSet.List_SG_ShotType_Details)
            Me.List_SG_CaseTableAdapter.FillBy_CFG_List(Me.MLLDataSet.List_SG_Case, GID)
            Me.List_SG_WADTableAdapter.FillBy_CFG_WADList(Me.MLLDataSet.List_SG_WAD, GID)
            Me.ViewPrimerListTableAdapter.FillBy_CFG_List(Me.MLLDataSet.viewPrimerList)
            Dim WID As Long = cmdWAD.SelectedValue
            txtShotCharge.Text = GetMaxWADCharge(WID)
            Me.List_SG_ShotCharge_LoadsTableAdapter.Fill(Me.MLLDataSet.List_SG_ShotCharge_Loads)
            Dim ObjAF As New AutoFillCollections.ShotGun
            txtSource.AutoCompleteCustomSource = ObjAF.Config_Source_SG
            txtShotCharge.AutoCompleteCustomSource = ObjAF.Config_LoadInOZ_SG
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Function GetMaxWADCharge(ByVal iID As Long) As String
        Dim sAns As String = ""
        Try
            Dim SQL As String = "SELECT * from List_SG_WAD where ID=" & iID
            Dim Obj As New BSDatabase
            Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                sAns = RS("load_t")
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "GetMaxWADCharge", Err.Number, ex.Message.ToString)
        End Try
        Return sAns
    End Function
    Private Sub cmdWAD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWAD.SelectedIndexChanged
        Dim WID As Long = cmdWAD.SelectedValue
        txtShotCharge.Text = GetMaxWADCharge(WID)
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            Dim PID As Long = cmbPrimer.SelectedValue
            Dim HID As Long = cmdHull.SelectedValue
            Dim WID As Long = cmdWAD.SelectedValue
            Dim sLoad As String = txtShotCharge.Text
            Dim dLoad As Double = ConvertOZToDouble(sLoad)
            Dim SLTID As Long = cmdShotType.SelectedValue
            Dim LTID As Long = cmbLoadType.SelectedValue
            Dim sSource As String = FluffContent(txtSource.Text)
            Dim IsPersonal As Boolean = chkPersonal.Checked
            Dim iPersonal As Integer = 1
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            If Not IsPersonal Then iPersonal = 0
            SQL = "INSERT INTO Config_List_Data_SG (CLNID,ATID,CALID,PRID,CAID,SW,SW_t,WAD" & _
                    ",SCL,Source,GID,IsPersonal,LTID) VALUES(" & ConfigID & "," & CalID & "," & GID & "," & PID & _
                    "," & HID & "," & dLoad & ",'" & sLoad & "'," & WID & "," & SLTID & ",'" & _
                    sSource & "'," & GID & "," & iPersonal & "," & LTID & ")"
            Obj.ConnExec(SQL)
            SQL = "UPDATE Config_List_Name set IsPersonal=" & iPersonal & " where id=" & ConfigID
            Obj.ConnExec(SQL)
            frmConfig_Add_Wizard_SG_Powder.MdiParent = Me.MdiParent
            frmConfig_Add_Wizard_SG_Powder.ConfigID = ConfigID
            frmConfig_Add_Wizard_SG_Powder.ConfigName = ConfigName
            frmConfig_Add_Wizard_SG_Powder.CalName = CalName
            frmConfig_Add_Wizard_SG_Powder.FromConfigWiz = True
            frmConfig_Add_Wizard_SG_Powder.CalID = CalID
            frmConfig_Add_Wizard_SG_Powder.GID = GID
            frmConfig_Add_Wizard_SG_Powder.Show()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnNext.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub chkPersonal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPersonal.CheckedChanged
        If chkPersonal.Checked Then
            txtSource.ReadOnly = True
        Else
            txtSource.ReadOnly = False
        End If
    End Sub
End Class