Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmEditConfig
    Public ConfigID As Long
    Public ConfigName As String
    Dim IsPersonal As Boolean
    Dim IsShotGun As Boolean
    Dim isActive As Boolean
    Dim isFav As Boolean
    Dim Notes As String
    Dim isPrefPowder As Boolean
    Dim PrefferedPowderID As Long
    Dim LPPID As Long
    Dim isLoading As Boolean
    Dim IsSlug As Boolean
    Dim SHOT_PREFLOAD As Double
    Dim GAID As Long
    Sub LoadPowderData(ByVal PID As Long)
        Try
            Dim Obj As New BSDatabase
            Dim SQL As String = "SELECT * from Config_List_Powder_Data_NSG where ID=" & PID
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                If Not IsDBNull(RS("Load_Min")) Then txtLMin.Text = RS("Load_Min")
                If Not IsDBNull(RS("Load_Mid")) Then txtLMid.Text = RS("Load_Mid")
                If Not IsDBNull(RS("Load_Max")) Then txtLMax.Text = RS("Load_Max")
                If Not IsDBNull(RS("FPS_Min")) Then txtMVMin.Text = RS("FPS_Min")
                If Not IsDBNull(RS("FPS_MID")) Then txtMVMid.Text = RS("FPS_MID")
                If Not IsDBNull(RS("FPS_Max")) Then txtMVMax.Text = RS("FPS_Max")
                If Not IsDBNull(RS("CUPS_Min")) Then txtCUPSMin.Text = RS("CUPS_Min")
                If Not IsDBNull(RS("CUPS_Mid")) Then txtCUPSMid.Text = RS("CUPS_Mid")
                If Not IsDBNull(RS("CUPS_Max")) Then txtCUPSMax.Text = RS("CUPS_Max")
                If RS("ispref") = 1 Then
                    isPrefPowder = True
                Else
                    isPrefPowder = False
                End If
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "LoadPowderData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadPowderDataSG(ByVal PID As Long)
        Try
            Dim Obj As New BSDatabase
            Dim SQL As String = "SELECT * from Config_List_Powder_Data_SG where ID=" & PID
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                If Not IsDBNull(RS("Load_Mid")) Then txtCharge.Text = RS("Load_Mid")
                If Not IsDBNull(RS("FPS_MID")) Then txtFPS.Text = RS("FPS_MID")
                If Not IsDBNull(RS("PSI_Mid")) Then txtPSI.Text = RS("PSI_Mid")
                If RS("ispref") = 1 Then
                    isPrefPowder = True
                Else
                    isPrefPowder = False
                End If
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "LoadPowderDataSG", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadRPConfig()
        Try
            Dim Obj As New BSDatabase
            Dim ObjIM As New InventoryMath
            Dim SQL As String = "SELECT * from Config_List_Data_NSG where CLNID=" & ConfigID
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                cmbCal.SelectedValue = RS("calid")
                Call LoadDropDownBoxes()
                cmbAmmo.SelectedValue = RS("atid")
                cmbPrimer.SelectedValue = RS("prid")
                cmbCase.SelectedValue = RS("caid")
                PrefferedPowderID = ObjIM.GetPrefNSGPowderIDID(ConfigID)
                cmbPowder.SelectedValue = PrefferedPowderID
                Call LoadPowderData(PrefferedPowderID)
                Call FillBullets()
                chkPrefPowder.Checked = isPrefPowder
                cmbBullet.SelectedValue = RS("bid")
                If IsPersonal Then
                    chkPersonal.Checked = True
                    chkBook.Checked = False
                    txtLoad.Enabled = False
                Else
                    chkPersonal.Checked = False
                    chkBook.Checked = True
                    If Not IsDBNull(RS("source")) Then txtLoad.Text = RS("source")
                End If
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "LoadRPConfig", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadShotgunConfig()
        Try
            Dim Obj As New BSDatabase
            Dim ObjIM As New InventoryMath
            Dim ObjGF As New GlobalFunctions
            Dim SQL As String = "SELECT * from Config_List_Data_SG where CLNID=" & ConfigID
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                cmbCal.SelectedValue = RS("ATID")
                IsSlug = ObjGF.IsSlugConfig(RS("SCL"))
                GAID = RS("CALID")
                Call LoadDropDownBoxesSG(GAID)
                If IsSlug Then
                    cmdShotType.Visible = False
                    txtShotCharge.Visible = False
                    Label27.Visible = False
                    Label28.Visible = False
                    cmbSlug.SelectedValue = RS("SCL")
                    cmbSlug.Location = cmdShotType.Location
                    Label29.Location = Label27.Location
                Else
                    cmdShotType.SelectedValue = RS("SCL")
                    txtShotCharge.Text = RS("SW_t")
                    SHOT_PREFLOAD = RS("SW")
                    cmbSlug.Visible = False
                    Label29.Visible = False
                End If
                cmbSGPrimer.SelectedValue = RS("PRID")
                cmdHull.SelectedValue = RS("CAID")
                cmdWAD.SelectedValue = RS("WAD")
                cmbLoadType.SelectedValue = RS("LTID")
                PrefferedPowderID = ObjIM.GetPrefSGPowderIDID(ConfigID)
                cmbPowderSG.SelectedValue = PrefferedPowderID
                Call LoadPowderDataSG(PrefferedPowderID)
                chkDefaultPowderSG.Checked = isPrefPowder
                If IsPersonal Then
                    chkPersonalSG.Checked = True
                    chkBookSG.Checked = False
                    txtSourceSG.Enabled = False
                Else
                    chkPersonalSG.Checked = False
                    chkBookSG.Checked = True
                    If Not IsDBNull(RS("source")) Then txtSourceSG.Text = RS("source")
                End If
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "LoadShotgunConfig", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadData()
        IsPersonal = False
        IsShotGun = False
        LASTCONFIGEDVIEWED = ConfigID
        txtConfigID.Text = ConfigName
        Dim Obj As New InventoryMath
        Call Obj.LoadConfig(ConfigID, IsPersonal, IsShotGun, Notes, isActive, isFav)
        If IsShotGun Then
            chkRP.Checked = False
            chkShotgun.Checked = True
            TabControl1.TabPages.Remove(TabControl1.TabPages.Item(1))
            'After removing 1, minus - from the orginal tab number.
            TabControl1.TabPages.Remove(TabControl1.TabPages.Item(2))
        Else
            chkRP.Checked = True
            chkShotgun.Checked = False
            TabControl1.TabPages.Remove(TabControl1.TabPages.Item(2))  '2
            TabControl1.TabPages.Remove(TabControl1.TabPages.Item(3))
        End If
        If IsShotGun Then
            Call LoadShotgunConfig()
        Else
            Call LoadRPConfig()
        End If
    End Sub
    Sub FillBullets()
        Try
            Me.List_BulletsTableAdapter.FillBy_CALAmmoType(Me.MLLDataSet.List_Bullets, cmbCal.SelectedValue, cmbAmmo.SelectedValue)
        Catch ex As Exception
            Call LogError(Me.Name, "FillBullets", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadDropDownBoxes()
        Try
            Me.General_Ammunition_TypeTableAdapter.Fill(Me.MLLDataSet.General_Ammunition_Type)
            Me.Config_List_Powder_Data_NSG_ViewTableAdapter.FillBy_ConfigID(Me.MLLDataSet.Config_List_Powder_Data_NSG_View, ConfigID)
            Me.List_CaseTableAdapter.FillBy_CALID(Me.MLLDataSet.List_Case, cmbCal.SelectedValue)
            Me.General_PrimerTableAdapter.Fill(Me.MLLDataSet.General_Primer)
        Catch ex As Exception
            Call LogError(Me.Name, "LoadDropDownBoxes", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadDropDownBoxesSG(Optional ByVal GID As Long = 0)
        Try
            If IsSlug Then
                Me.List_SG_ShotType_DetailsTableAdapter.FillBy_CFG_List_Slug(Me.MLLDataSet.List_SG_ShotType_Details, cmbCal.Text)
            Else
                Me.List_SG_ShotType_DetailsTableAdapter.FillBy_CFG_List(Me.MLLDataSet.List_SG_ShotType_Details)
            End If
            Me.ViewPrimerListTableAdapter.Fill(Me.MLLDataSet.viewPrimerList)
            Me.Config_List_Powder_Data_SG_ViewTableAdapter.FillBy_ConfigID(Me.MLLDataSet.Config_List_Powder_Data_SG_View, ConfigID)
            Me.List_SG_ShotCharge_LoadsTableAdapter.Fill(Me.MLLDataSet.List_SG_ShotCharge_Loads)
            Me.List_SG_WADTableAdapter.FillBy_CFG_WADList(Me.MLLDataSet.List_SG_WAD, GID)
            Me.List_SG_CaseTableAdapter.FillBy_CFG_List(Me.MLLDataSet.List_SG_Case, GID)
        Catch ex As Exception
            Call LogError(Me.Name, "LoadDropDownBoxesSG", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmEditConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            isLoading = True
            Me.List_CalibersTableAdapter.Fill(Me.MLLDataSet.List_Calibers)
            Me.Text = "Editing Configuration " & ConfigName
            Call LoadData()
            isLoading = False
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub cmbPowder_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPowder.SelectedIndexChanged
        Dim PID As Long = cmbPowder.SelectedValue
        Call LoadPowderData(PID)
        chkPrefPowder.Checked = isPrefPowder
    End Sub
    Sub SavePowder()
        Try
            Dim lngPowderID As Long = cmbPowder.SelectedValue
            Dim LMin As Double = FluffContent(txtLMin.Text, 0)
            Dim LMid As Double = FluffContent(txtLMid.Text, 0)
            Dim LMax As Double = FluffContent(txtLMax.Text, 0)
            Dim MVMin As Double = FluffContent(txtMVMin.Text, 0)
            Dim MVMid As Double = FluffContent(txtMVMid.Text, 0)
            Dim MVMax As Double = FluffContent(txtMVMax.Text, 0)
            Dim CUPSMin As Double = FluffContent(txtCUPSMin.Text, 0)
            Dim CUPSMid As Double = FluffContent(txtCUPSMid.Text, 0)
            Dim CUPSMax As Double = FluffContent(txtCUPSMax.Text, 0)
            Dim intPerf As Integer = 0
            If Not IsRequired(LMid, 0, "Mid Load/Preferred Load", Me.Text) Then Exit Sub
            Dim Obj As New BSDatabase
            If chkPrefPowder.Checked Then
                intPerf = 1
                Obj.ConnExec("UPDATE Config_List_Powder_Data_NSG set ispref=0 where CLNID=" & ConfigID)
            End If
            Dim SQL As String = "UPDATE Config_List_Powder_Data_NSG set Load_Min=" & LMin & "," & _
                                    "Load_Mid=" & LMid & ",Load_Max=" & LMax & "," & _
                                    "FPS_Min=" & MVMin & ",FPS_MID=" & MVMid & ",FPS_Max=" & MVMax & "," & _
                                    "CUPS_Min=" & CUPSMin & ",CUPS_Mid=" & CUPSMid & ",CUPS_Max=" & CUPSMax & "," & _
                                    "IsPref=" & intPerf & " where ID=" & lngPowderID
            Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Me.Name, "SavePowder", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SavePowderSG()
        Try
            Dim lngPowderID As Long = cmbPowderSG.SelectedValue
            Dim dCharge As Double = FluffContent(txtCharge.Text, 0)
            Dim FPS As Double = FluffContent(txtFPS.Text, 0)
            Dim PSI As Double = FluffContent(txtPSI.Text, 0)
            Dim intPerf As Integer = 0
            If Not IsRequired(dCharge, 0, "Mid Load/Preferred Load", Me.Text) Then Exit Sub
            Dim Obj As New BSDatabase
            If chkDefaultPowderSG.Checked Then
                intPerf = 1
                Obj.ConnExec("UPDATE Config_List_Powder_Data_SG set ispref=0 where CLNID=" & ConfigID)
            End If
            Dim SQL As String = "UPDATE Config_List_Powder_Data_SG set Load_Mid=" & dCharge & "," & _
                                    "FPS_MID=" & FPS & ",PSI_Mid=" & PSI & "," & _
                                    "IsPref=" & intPerf & " where ID=" & lngPowderID
            Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Me.Name, "SavePowderSG", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnSavePowder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePowder.Click
        Call SavePowder()
    End Sub
    Sub SaveGD()
        Try
            Dim strConfigName As String = FluffContent(txtConfigID.Text)
            Dim bRP As Boolean = chkRP.Checked
            Dim bSG As Boolean = chkShotgun.Checked
            Dim lngCal As Long = cmbCal.SelectedValue
            Dim LoadType As Integer = 0
            If bSG Then LoadType = 1
            If Not IsRequired(strConfigName, "Configuration ID", Me.Text) Then Exit Sub
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim SQL As String = "UPDATE Config_List_Name set ConfigName='" & strConfigName & _
                                "',IsShotGun=" & LoadType & " where ID=" & configid
            Obj.ConnExec(SQL)
            SQL = "UPDATE Config_List_Data_NSG set CALID=" & lngCal & " where CLNID=" & ConfigID
            Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Me.Name, "SaveGD", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnSaveGD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveGD.Click
        Call SaveGD()
    End Sub
    Sub SaveRPDetails()
        Try
            Dim lngBullet As Long = cmbBullet.SelectedValue
            Dim lngPrimer As Long = cmbPrimer.SelectedValue
            Dim lngCase As Long = cmbCase.SelectedValue
            Dim lngAmmoType As Long = cmbAmmo.SelectedValue
            Dim bPersonal As Boolean = chkPersonal.Checked
            Dim bOther As Boolean = chkBook.Checked
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            Dim strSource As String = FluffContent(txtLoad.Text)
            If bOther Then
                SQL = "UPDATE Config_List_Name set IsPersonal=0  where id=" & ConfigID
            Else
                SQL = "UPDATE Config_List_Name set IsPersonal=1 where id=" & ConfigID
            End If
            Obj.ConnExec(SQL)
            SQL = "UPDATE Config_List_Data_NSG set ATID=" & lngAmmoType & ",BID=" & lngBullet & "," & _
                            "PRID=" & lngPrimer & ",CAID=" & lngCase & ",Source='" & strSource & _
                            "' where CLNID=" & ConfigID
            Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Me.Name, "SaveRPDetails", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveSGDetails()
        Try
            Dim lProjectile As Long = 0
            Dim lngPrimer As Long = cmbSGPrimer.SelectedValue
            Dim lngCase As Long = cmdHull.SelectedValue
            Dim lWAD As Long = cmdWAD.SelectedValue
            Dim lngAmmoType As Long = cmbLoadType.SelectedValue
            Dim bPersonal As Boolean = chkPersonalSG.Checked
            Dim bOther As Boolean = chkBookSG.Checked
            Dim sCharge As String = txtShotCharge.Text
            Dim sSource As String = FluffContent(txtSourceSG.Text)
            Dim CAL As Long = cmbCal.SelectedValue
            Dim dCharge As Double = ConvertOZToDouble(sCharge)
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            If IsSlug Then
                lProjectile = cmbSlug.SelectedValue
            Else
                lProjectile = cmdShotType.SelectedValue
            End If
            If bOther Then
                SQL = "UPDATE Config_List_Name set IsPersonal=0  where id=" & ConfigID
            Else
                SQL = "UPDATE Config_List_Name set IsPersonal=1 where id=" & ConfigID
            End If
            Obj.ConnExec(SQL)
            If Not IsSlug Then
                SQL = "UPDATE Config_List_Data_SG set ATID=" & CAL & ",CALID=" & GAID & _
                        ",PRID=" & lngPrimer & ",CAID=" & lngCase & ",SW=" & dCharge & _
                        ",SW_t='" & sCharge & "',WAD=" & lWAD & ",SCL=" & lProjectile & _
                        ",Source='" & sSource & "',GID=" & GAID & ",LTID=" & lngAmmoType & _
                        " where CLNID=" & ConfigID
            Else
                SQL = "UPDATE Config_List_Data_SG set ATID=" & CAL & ",CALID=" & GAID & _
                        ",PRID=" & lngPrimer & ",CAID=" & lngCase & ",WAD=" & lWAD & _
                        ",SCL=" & lProjectile & ",Source='" & sSource & "',GID=" & GAID & _
                        ",LTID=" & lngAmmoType & " where CLNID=" & ConfigID
            End If
            Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Me.Name, "SaveSGDetails", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnSaveRPDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRPDetails.Click
        Call SaveRPDetails()
    End Sub
    Private Sub btnSaveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAll.Click
        If Not IsShotGun Then
            Call SavePowder()
            Call SaveGD()
            Call SaveRPDetails()
        Else
            Call SavePowderSG()
            Call SaveGD()
            Call SaveSGDetails()
        End If
        Call MDIParentMain.RefreshConfigData()
        Me.Close()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub cmbAmmo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAmmo.SelectedIndexChanged
        If Not isLoading Then Call FillBullets()
    End Sub
    Private Sub chkPersonal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPersonal.CheckedChanged
        If chkPersonal.Checked Then
            chkBook.Checked = False
            txtLoad.Text = ""
            txtLoad.Enabled = False
        End If
    End Sub
    Private Sub chkBook_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBook.CheckedChanged
        If chkBook.Checked Then
            chkPersonal.Checked = False
            txtLoad.Enabled = True
        End If
    End Sub
    Private Sub cmbPowderSG_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPowderSG.SelectedIndexChanged
        Dim PID As Long = cmbPowderSG.SelectedValue
        Call LoadPowderDataSG(PID)
        chkDefaultPowderSG.Checked = isPrefPowder
    End Sub
    Private Sub btnSavePowderSG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePowderSG.Click
        Call SavePowderSG()
    End Sub
    Private Sub btnSaveShotgunDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveShotgunDetails.Click
        Call SaveSGDetails()
    End Sub
    Private Sub chkPersonalSG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPersonalSG.CheckedChanged
        If chkPersonal.Checked Then
            chkBookSG.Checked = False
            txtSourceSG.Text = ""
            txtSourceSG.Enabled = False
        End If
    End Sub
    Private Sub chkBookSG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBookSG.CheckedChanged
        If chkBookSG.Checked Then
            chkPersonalSG.Checked = False
            txtSourceSG.Enabled = True
        End If
    End Sub
End Class