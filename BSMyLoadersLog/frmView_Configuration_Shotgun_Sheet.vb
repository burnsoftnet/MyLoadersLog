Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Imports System.IO
Imports System.Xml
Imports System.Data
Public Class frmView_Configuration_Shotgun_Sheet
    Public ConfigID As Long
    Public ConfigName As String
    Dim IsPersonal As Boolean
    Dim IsShotGun As Boolean
    Dim isFav As Boolean
    Dim isActive As Boolean
    Dim COST_BULLET As Double
    Dim COST_PRIMER As Double
    Dim COST_CASE As Double
    Dim COST_POWDER As Double
    Dim COST_WAD As Double
    Dim COST_SLUG As Double
    Dim COST_SHOT As Double
    Dim WAD_MAXLOAD As Double
    Dim SHOT_PREFLOAD As Double
    Dim MID_POWDER As Double
    Dim INSTOCK_BULLET As Long
    Dim INSTOCK_PRIMER As Long
    Dim INSTOCK_CASE As Long
    Dim INSTOCK_WAD As Long
    Dim INSTOCK_POWDER As Double
    Dim INSTOCK_SHOT As Double
    Dim INSTOCK_SHOT_OZ As Double
    Dim INSTOCK_SLUG As Double
    Dim PrefferedPowderID As Long
    Dim IsSlug As Boolean

    Sub UpdateActivity(ByVal iStat As Integer)
        Dim Obj As New BSDatabase
        Dim SQL As String = "UPDATE Config_List_Name set IsActive=" & iStat & " where id=" & ConfigID
        Obj.ConnExec(SQL)
    End Sub
    Sub UpdateFav(ByVal iStat As Integer)
        Dim Obj As New BSDatabase
        Dim SQL As String = "UPDATE Config_List_Name set IsFav=" & iStat & " where id=" & ConfigID
        Obj.ConnExec(SQL)
    End Sub
    Sub LoadPowderGrid()
        Try
            DataGridView1.Columns(3).Visible = VIEW_FPS
            DataGridView1.Columns(4).Visible = VIEW_CUPS
            Me.Config_List_Powder_Data_SG_ViewTableAdapter.FillBy_ConfigID(Me.MLLDataSet.Config_List_Powder_Data_SG_View, ConfigID)
        Catch ex As Exception
            Call LogError(Me.Name, "LoadPowderGrid", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub LoadConfig_ShotGun()
        Try
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim ObjIM As New InventoryMath
            Dim SQL As String = "SELECT * from Config_List_Data_SG where CLNID=" & ConfigID
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            Dim ShotDetails_Manu As String = ""
            Dim ShotDetails_Name As String = ""
            Dim ShotDetails_QTY As Double
            Dim ShotDetails_EPPS As Double
            Dim ShotDetails_GR As Double
            RS = CMD.ExecuteReader
            While RS.Read
                'NOTE: the ATID field if the list_Calibers Table and the CALID is the List_SG_Gauge Table
                'The SCL is the ref to the shot/slug list id
                txtAmmoType.Text = ObjG.GetAmmoTypeName_SG(RS("LTID")) '"Shotgun"
                txtCal.Text = ObjIM.GetCaliber(RS("ATID"))
                Call ObjIM.LoadSG_ShotType_Details(RS("SCL"), ShotDetails_Manu, ShotDetails_Name, IsSlug, _
                                    txtShotMat.Text, txtShotNo.Text, txtSlugWeight.Text, "", ShotDetails_QTY, ShotDetails_EPPS, _
                                    0, INSTOCK_SHOT_OZ, ShotDetails_GR)
                If Not IsDBNull(RS("SW_t")) Then txtPrefLoad.Text = RS("SW_t")
                SHOT_PREFLOAD = RS("SW")
                If Not IsSlug Then
                    COST_SHOT = ShotDetails_EPPS
                    INSTOCK_SHOT = ShotDetails_GR
                    txtShotManu.Text = ShotDetails_Manu
                    txtShotName.Text = ShotDetails_Name
                    GroupBox1.Visible = True
                    GroupBox4.Visible = False
                    Label45.Visible = True
                    Label23.Visible = False
                Else
                    COST_SLUG = ShotDetails_EPPS
                    INSTOCK_SLUG = ShotDetails_QTY
                    GroupBox1.Visible = False
                    GroupBox4.Visible = True
                    txtSlugManu.Text = ShotDetails_Manu
                    txtSlugName.Text = ShotDetails_Name
                    Label45.Visible = False
                    Label23.Visible = True
                End If

                Call ObjIM.LoadWADInfo(RS("WAD"), txtWADManu.Text, txtWADName.Text, txtWADLoad.Text, _
                                    WAD_MAXLOAD, INSTOCK_WAD, COST_WAD)
                Call ObjIM.LoadPrimerInfo(RS("PRID"), txtPManu.Text, txtPName.Text, _
                        txtPType.Text, COST_PRIMER, INSTOCK_PRIMER)
                Call ObjIM.LoadHullInfo(RS("CAID"), txtCManu.Text, txtCName.Text, txtCTOL.Text, _
                        INSTOCK_CASE, COST_CASE, txtDRAM.Text)
                If Not IsPersonal Then
                    If Not IsDBNull(RS("Source")) Then
                        lblReffer.Text = "(Refer to " & RS("Source") & ")"
                    Else
                        lblReffer.Text = "Unknown Referance"
                    End If
                End If
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "LoadConfig_ShotGun", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadCosts()
        Try
            Dim lnmr As Long = 0
            Dim dPowPerB As Double = 0
            Dim dC1RA As Double = 0
            Dim Obj As New InventoryMath
            If Not IsSlug Then
                COST_BULLET = COST_SHOT * (SHOT_PREFLOAD * WEIGHT_GRAMS_OZ) ' * COST_SHOT
            Else
                COST_BULLET = COST_SLUG
            End If
            txtCPB.Text = Obj.ConvertToDollars(COST_BULLET)
            txtCPP.Text = Obj.ConvertToDollars(COST_PRIMER)
            txtCPC.Text = Obj.ConvertToDollars(COST_CASE)
            txtCOPMid.Text = Obj.ConvertToDollars((COST_POWDER * MID_POWDER))
            txtCPW.Text = Obj.ConvertToDollars(COST_WAD)
            dC1RA = CostOf1RndOfAmmoSG(COST_PRIMER, COST_CASE, COST_BULLET, COST_POWDER, MID_POWDER, COST_WAD)
            txtC1RA.Text = dC1RA
            txtCNWIS.Text = INSTOCK_WAD
            txtCPriIS.Text = INSTOCK_PRIMER
            txtCPowIS.Text = INSTOCK_POWDER
            txtCCIS.Text = INSTOCK_CASE
            If IsSlug Then
                txtCBIS.Text = INSTOCK_SLUG
                lnmr = INSTOCK_SLUG
                If lnmr < INSTOCK_CASE Then
                    lnmr = INSTOCK_SLUG
                ElseIf lnmr > INSTOCK_CASE Then
                    lnmr = INSTOCK_CASE
                End If
                If lnmr > INSTOCK_WAD Then lnmr = INSTOCK_WAD
                dPowPerB = (INSTOCK_POWDER / MID_POWDER)
                If lnmr > INSTOCK_PRIMER Then lnmr = INSTOCK_PRIMER
                If lnmr > dPowPerB Then lnmr = CLng(dPowPerB)
            Else
                txtCBIS.Text = INSTOCK_SHOT_OZ
                Dim CountMakeAble As Double = INSTOCK_SHOT_OZ / SHOT_PREFLOAD
                lnmr = CountMakeAble
                If lnmr < INSTOCK_CASE Then
                    lnmr = CountMakeAble
                ElseIf lnmr > INSTOCK_CASE Then
                    lnmr = INSTOCK_CASE
                End If
                If lnmr > INSTOCK_WAD Then lnmr = INSTOCK_WAD
                dPowPerB = (INSTOCK_POWDER / MID_POWDER)
                If lnmr > INSTOCK_PRIMER Then lnmr = INSTOCK_PRIMER
                If lnmr > dPowPerB Then lnmr = CLng(dPowPerB)
            End If

            txtNMR.Text = lnmr
            txtTCR.Text = lnmr * Obj.ConvertToDollars(dC1RA)
        Catch ex As Exception
            Call LogError(Me.Name, "LoadCosts", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Public Sub LoadData()
        Try
            LASTCONFIGEDVIEWED = ConfigID
            Dim ObjG As New GlobalFunctions
            Dim Obj As New InventoryMath
            Me.Loaders_Log_Ammunition_AuditTableAdapter.FillByConfigID(Me.MLLDataSet.Loaders_Log_Ammunition_Audit, ConfigID)
            IsShotGun = False
            IsPersonal = False
            ConfigName = ObjG.GetTitle(ConfigID)
            Me.Text = ConfigName & " Configuration Sheet"
            txtConfigName.Text = ConfigName
            PrefferedPowderID = Obj.GetPrefSGPowderID(ConfigID, MID_POWDER)
            INSTOCK_POWDER = Obj.GetQTYPerPowder(PrefferedPowderID)
            Call Obj.LoadConfig(ConfigID, IsPersonal, IsShotGun, txtNotes.Text, isActive, isFav)
            COST_POWDER = Obj.GetPricePerPowder(PrefferedPowderID)
            ChkPerLoad.Checked = IsPersonal
            If isActive Then
                rbstatus1.Checked = True
                rbstatus2.Checked = False
            Else
                rbstatus1.Checked = False
                rbstatus2.Checked = True
            End If
            chkFav.Checked = isFav
            Call LoadConfig_ShotGun()
            Call LoadPowderGrid()
            Call LoadCosts()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub XML_Generate(ByVal strPath As String)
        Try
            Dim sAns As String = ""
            Dim NL As String = Chr(10) & Chr(13)
            sAns = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
            sAns &= "<Inventory>" & NL
            sAns &= XML_GenerateConfig()
            sAns &= XML_GenerateShotTypeDetails()
            sAns &= XML_GeneratePrimers()
            sAns &= XML_GenerateWADS()
            sAns &= XML_GenerateCases()
            sAns &= XML_GeneratePowderList()
            sAns &= "</Inventory>" & NL
            sAns = Replace(sAns, "&", "&amp;")
            Dim ObjFS As New BSFileSystem
            ObjFS.DeleteFile(strPath)
            ObjFS.OutPutToFile(strPath, sAns)
            MsgBox("Config was exported to " & Chr(10) & strPath)
        Catch ex As Exception
            Call LogError(Me.Name, "XML_Generate", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Function XML_GenerateConfig() As String
        Dim sAns As String = ""
        Dim NL As String = Chr(10) & Chr(13)
        'sAns = "<Config>" & NL
        sAns &= "    <Details>" & NL
        sAns &= "       <ConfigName>" & ConfigName & "</ConfigName>" & NL
        sAns &= "       <IsPersonal>" & IsPersonal & "</IsPersonal>" & NL
        sAns &= "       <IsShotGun>" & IsShotGun & "</IsShotGun>" & NL
        sAns &= "       <Notes>" & txtNotes.Text & "</Notes>" & NL
        sAns &= "       <AmmoType>" & txtAmmoType.Text & "</AmmoType>" & NL
        sAns &= "       <Caliber>" & txtCal.Text & "</Caliber>" & NL
        sAns &= "       <Refferance>" & lblReffer.Text & "</Refferance>" & NL
        sAns &= "       <sw>" & SHOT_PREFLOAD & "</sw>" & NL
        sAns &= "       <sw_t>" & txtPrefLoad.Text & "</sw_t>" & NL
        sAns &= "    </Details>" & NL
        'sAns &= "</Config>" & NL
        Return sAns
    End Function
    Function XML_GeneratePrimers() As String
        Dim sAns As String = ""
        Dim NL As String = Chr(10) & Chr(13)
        sAns = "   <General_Primer>" & NL
        sAns &= "       <Manufacturer>" & txtPManu.Text & "</Manufacturer>" & NL
        sAns &= "       <Name>" & txtPName.Text & "</Name>" & NL
        sAns &= "       <Primer_Type>" & txtPType.Text & "</Primer_Type>" & NL
        sAns &= "   </General_Primer>" & NL
        Return sAns
    End Function
    Function XML_GenerateWADS() As String
        Dim sAns As String = ""
        Dim NL As String = Chr(10) & Chr(13)
        sAns = "   <List_SG_WAD>" & NL
        sAns &= "       <Manufacturer>" & txtWADManu.Text & "</Manufacturer>" & NL
        sAns &= "       <Name>" & txtWADName.Text & "</Name>" & NL
        sAns &= "       <load_t>" & txtWADLoad.Text & "</load_t>" & NL
        sAns &= "       <load_d>" & WAD_MAXLOAD & "</load_d>" & NL
        sAns &= "   </List_SG_WAD>" & NL
        Return sAns
    End Function
    Function XML_GeneratePowderList() As String
        Dim sAns As String = ""
        Dim NL As String = Chr(10) & Chr(13)
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT * from qry_CFG_SR_PowderList_SG where CLNID=" & ConfigID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                sAns &= "    <General_Powder>" & NL
                sAns &= "       <Manufacturer>" & RS("PowderManu") & "</Manufacturer>" & NL
                sAns &= "       <Name>" & RS("PowderName") & "</Name>" & NL
                sAns &= "       <Load_Mid>" & RS("Load_Mid") & "</Load_Mid>" & NL
                sAns &= "       <FPS_MID>" & RS("FPS_MID") & "</FPS_MID>" & NL
                sAns &= "       <PSI_Mid>" & RS("PSI_Mid") & "</PSI_Mid>" & NL
                sAns &= "       <IsPref>" & RS("IsPref") & "</IsPref>" & NL
                sAns &= "    </General_Powder>" & NL
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "XML_GeneratePowderList", Err.Number, ex.Message.ToString)
        End Try
        Return sAns
    End Function
    Function XML_GenerateCases() As String
        Dim sAns As String = ""
        Dim NL As String = Chr(10) & Chr(13)
        sAns = "   <List_SG_Case>" & NL
        sAns &= "       <Manufacturer>" & txtCManu.Text & "</Manufacturer>" & NL
        sAns &= "       <Name>" & txtCName.Text & "</Name>" & NL
        sAns &= "       <Length>" & txtCTOL.Text & "</Length>" & NL
        sAns &= "       <Gauge>" & txtCal.Text & "</Gauge>" & NL
        sAns &= "       <DRAM>" & txtDRAM.Text & "</DRAM>" & NL
        sAns &= "   </List_SG_Case>" & NL
        Return sAns
    End Function
    Function XML_GenerateShotTypeDetails() As String
        Dim sAns As String = ""
        Dim NL As String = Chr(10) & Chr(13)
        sAns = "   <List_SG_ShotType_Details>" & NL
        If IsSlug Then
            sAns &= "       <Manufacturer>" & txtSlugManu.Text & "</Manufacturer>" & NL
            sAns &= "       <Name>" & txtSlugName.Text & "</Name>" & NL
            sAns &= "       <Weight>" & txtSlugWeight.Text & "</Weight>" & NL
            sAns &= "       <Cal>" & txtCal.Text & "</Cal>" & NL
            sAns &= "       <IsSlug>1</IsSlug>" & NL
        Else
            sAns &= "       <Manufacturer>" & txtShotManu.Text & "</Manufacturer>" & NL
            sAns &= "       <Name>" & txtShotName.Text & "</Name>" & NL
            sAns &= "       <Cal>" & txtCal.Text & "</Cal>" & NL
            sAns &= "       <IsSlug>0</IsSlug>" & NL
            sAns &= "       <Weight>0</Weight>" & NL
            sAns &= "       <Mat>" & txtShotMat.Text & "</Mat>" & NL
            sAns &= "       <ShotNo>" & txtShotNo.Text & "</ShotNo>" & NL
        End If
        sAns &= "   </List_SG_ShotType_Details>" & NL
        Return sAns
    End Function

    Private Sub frmView_Configuration_Shotgun_Sheet_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Dim objS As New ViewSizeSettings
        objS.SaveView_Configuration_Shotgun_Sheet(Me.Height, Me.Width, Me.Location.X, Me.Location.Y)
    End Sub
    Private Sub frmView_Configuration_Shotgun_Sheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim objS As New ViewSizeSettings
            objS.LoadView_Configuration_Shotgun_Sheet(Me.Height, Me.Width, Me.Location)
            LASTCONFIGEDVIEWED = ConfigID
            GroupBox4.Visible = False
            GroupBox1.Visible = False
            Call LoadData()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmView_Configuration_Shotgun_Sheet_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Width > 0 Then
            TabControl1.Width = Me.Width - 5
            TabControl1.Height = Me.Height - 60
            DataGridView1.Width = TabControl1.Width - 27
            DataGridView1.Height = TabControl1.Height - 69
            txtNotes.Width = TabControl1.Width - 27
            txtNotes.Height = TabControl1.Height - 69
        End If
    End Sub
    Private Sub chkFav_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFav.CheckedChanged
        If chkFav.Checked Then
            isFav = True
            Call UpdateFav(1)
        Else
            isFav = False
            Call UpdateFav(0)
        End If
        Call MDIParentMain.RefreshConfigData()
    End Sub
    Private Sub rbstatus1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbstatus1.CheckedChanged
        If rbstatus1.Checked Then
            rbstatus2.Checked = False
            isActive = True
            Call UpdateActivity(1)
            Call MDIParentMain.RefreshConfigData()
        End If
    End Sub
    Private Sub rbstatus2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbstatus2.CheckedChanged
        If rbstatus2.Checked Then
            rbstatus1.Checked = False
            isActive = False
            Call UpdateActivity(0)
            Call MDIParentMain.RefreshConfigData()
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strSQLTable As String = "Config_List_Powder_Data_SG"
            Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
            Obj.ConnExec(SQL)
            Call LoadPowderGrid()
        Catch ex As Exception
            Call LogError(Me.Name, "DeleteToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub SetAsDefaultToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetAsDefaultToolStripMenuItem.Click
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strSQLTable As String = "Config_List_Powder_Data_SG"
            Dim SQL As String = "Update " & strSQLTable & " set IsPref=0 where CLNID=" & ConfigID
            Obj.ConnExec(SQL)
            SQL = "Update " & strSQLTable & " set IsPref=1 where ID=" & ItemID
            Obj.ConnExec(SQL)
            Call LoadData()
        Catch ex As Exception
            Call LogError(Me.Name, "DeleteToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Call LoadPowderGrid()
    End Sub
    Private Sub btnAddNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNotes.Click
        btnAddNotes.Enabled = False
        btnUpdate.Visible = True
        txtNotes.ReadOnly = False
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        btnUpdate.Visible = False
        btnAddNotes.Enabled = True
        txtNotes.ReadOnly = True
        Try
            Dim Obj As New BSDatabase
            Dim strNotes As String = FluffContent(txtNotes.Text)
            Dim SQL As String = "UPDATE Config_List_Name set Notes='" & strNotes & "' where ID=" & ConfigID
            Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Me.Name, "btnUpdate.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim frmNew As New frmConfig_Add_Wizard_SG_Powder
        frmNew.ConfigID = ConfigID
        frmNew.ConfigName = ConfigName
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Me.Close()
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim frmNew As New frmLoadMakeReady_Details
        frmNew.MdiParent = Me.MdiParent
        frmNew.ConfigID = ConfigID
        frmNew.ConfigName = ConfigName
        frmNew.Show()
        Me.Close()
    End Sub
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Not IsSlug Then
                Dim frmNew As New frmReport_Configuration_Sheet_SG
                frmNew.Config_ID = ConfigID
                frmNew.Config_Name = ConfigName
                frmNew.Config_AT = txtAmmoType.Text
                frmNew.Config_Cal = txtCal.Text
                frmNew.Config_Notes = txtNotes.Text
                frmNew.Pro_Manu = txtShotManu.Text
                frmNew.Pro_Name = txtShotName.Text
                frmNew.Pro_Material = txtShotMat.Text
                frmNew.Pro_ShotNo = txtShotNo.Text
                frmNew.Pro_SelectedLoad = txtPrefLoad.Text
                frmNew.WAD_Manu = txtWADManu.Text
                frmNew.WAD_Name = txtWADName.Text
                frmNew.WAD_MaxLoad = txtWADLoad.Text
                frmNew.Pri_Manu = txtPManu.Text
                frmNew.Pri_Name = txtPName.Text
                frmNew.Pri_PT = txtPType.Text
                frmNew.Case_Manu = txtCManu.Text
                frmNew.Case_Name = txtCName.Text
                frmNew.Case_TTL = txtCTOL.Text
                frmNew.Case_DRAM = txtDRAM.Text
                frmNew.Config_ISPersonal = IsPersonal
                frmNew.Config_Ref = lblReffer.Text
                frmNew.Config_Fav = chkFav.Checked
                frmNew.MdiParent = Me.MdiParent
                frmNew.Show()
            Else
                Dim frmNew As New frmReport_Configuration_Sheet_SG_Slug
                frmNew.Config_ID = ConfigID
                frmNew.Config_Name = ConfigName
                frmNew.Config_AT = txtAmmoType.Text
                frmNew.Config_Cal = txtCal.Text
                frmNew.Config_Notes = txtNotes.Text
                frmNew.Pro_Manu = txtSlugManu.Text
                frmNew.Pro_Name = txtSlugName.Text
                frmNew.Pro_Weight = txtSlugWeight.Text
                frmNew.WAD_Manu = txtWADManu.Text
                frmNew.WAD_Name = txtWADName.Text
                frmNew.WAD_MaxLoad = txtWADLoad.Text
                frmNew.Pri_Manu = txtPManu.Text
                frmNew.Pri_Name = txtPName.Text
                frmNew.Pri_PT = txtPType.Text
                frmNew.Case_Manu = txtCManu.Text
                frmNew.Case_Name = txtCName.Text
                frmNew.Case_TTL = txtCTOL.Text
                frmNew.Case_DRAM = txtDRAM.Text
                frmNew.Config_ISPersonal = IsPersonal
                frmNew.Config_Ref = lblReffer.Text
                frmNew.Config_Fav = chkFav.Checked
                frmNew.MdiParent = Me.MdiParent
                frmNew.Show()
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "ToolStripButton4_Click", Err.Number, ex.Message.ToString)
        End Try
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        Dim DefaultFileName As String = "ExportConfig_" & ConfigName & ".xml"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.Filter = "XML File(*.xml)|*.xml"
        SaveFileDialog1.Title = "Export Data to XML File"
        SaveFileDialog1.FileName = Replace(Replace(Replace(Replace(DefaultFileName, " ", "_"), "/", "-"), "\", "-"), Chr(34), "")
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim strFilePath As String = SaveFileDialog1.FileName
        Call XML_Generate(strFilePath)
        Me.Close()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim frmNew As New frmEditConfig
        frmNew.ConfigID = ConfigID
        frmNew.ConfigName = ConfigName
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
        Me.Close()
    End Sub

    Private Sub GroupBox6_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub
End Class