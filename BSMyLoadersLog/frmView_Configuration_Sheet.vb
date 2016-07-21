Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Imports System.IO
Imports System.Xml
Imports System.Data
Public Class frmView_Configuration_Sheet
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
    Dim MID_POWDER As Double
    Dim INSTOCK_BULLET As Long
    Dim INSTOCK_PRIMER As Long
    Dim INSTOCK_CASE As Long
    Dim INSTOCK_POWDER As Double
    Dim PrefferedPowderID As Long
#Region "Subs"
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
        '7,8,9 are FPS
        '10,11,12 as CUPS
        DataGridView1.Columns(7).Visible = VIEW_FPS
        DataGridView1.Columns(8).Visible = VIEW_FPS
        DataGridView1.Columns(9).Visible = VIEW_FPS
        DataGridView1.Columns(10).Visible = VIEW_CUPS
        DataGridView1.Columns(11).Visible = VIEW_CUPS
        DataGridView1.Columns(12).Visible = VIEW_CUPS
        Me.Config_List_Powder_Data_NSG_ViewTableAdapter.FillBy_ConfigID(Me.MLLDataSet.Config_List_Powder_Data_NSG_View, ConfigID)
    End Sub
    Sub LoadCosts()
        Try
            Dim lnmr As Long = 0
            Dim dPowPerB As Double = 0
            Dim dC1RA As Double = 0
            Dim Obj As New InventoryMath
            txtCPB.Text = Obj.ConvertToDollars(COST_BULLET)
            txtCPP.Text = Obj.ConvertToDollars(COST_PRIMER)
            txtCPC.Text = Obj.ConvertToDollars(COST_CASE)
            txtCOPMid.Text = Obj.ConvertToDollars((COST_POWDER * MID_POWDER))
            'Cost Seems higher txtC1RA
            'dC1RA = ((COST_POWDER * MID_POWDER) + COST_CASE + COST_PRIMER + COST_BULLET)
            dC1RA = CostOf1RndOfAmmo(COST_PRIMER, COST_CASE, COST_BULLET, COST_POWDER, MID_POWDER)
            txtC1RA.Text = dC1RA
            txtCBIS.Text = INSTOCK_BULLET
            txtCPriIS.Text = INSTOCK_PRIMER
            txtCPowIS.Text = INSTOCK_POWDER
            txtCCIS.Text = INSTOCK_CASE

            lnmr = INSTOCK_BULLET
            If lnmr < INSTOCK_CASE Then
                lnmr = INSTOCK_BULLET
            ElseIf lnmr > INSTOCK_CASE Then
                lnmr = INSTOCK_CASE
            End If
            dPowPerB = (INSTOCK_POWDER / MID_POWDER)
            If lnmr > INSTOCK_PRIMER Then lnmr = INSTOCK_PRIMER
            If lnmr > dPowPerB Then lnmr = CLng(dPowPerB)
            txtNMR.Text = lnmr
            txtTCR.Text = lnmr * Obj.ConvertToDollars(dC1RA)
        Catch ex As Exception
            Call LogError(Me.Name, "LoadCosts", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadData()
        LASTCONFIGEDVIEWED = ConfigID
        Try
            Me.Loaders_Log_Ammunition_AuditTableAdapter.FillByConfigID(Me.MLLDataSet.Loaders_Log_Ammunition_Audit, ConfigID)
            IsShotGun = False
            IsPersonal = False
            txtConfigName.Text = ConfigName
            Dim Obj As New InventoryMath
            PrefferedPowderID = Obj.GetPrefNSGPowderID(ConfigID, MID_POWDER)
            COST_POWDER = Obj.GetPricePerPowder(PrefferedPowderID)
            INSTOCK_POWDER = Obj.GetQTYPerPowder(PrefferedPowderID)
            Call LoadPowderGrid()
            Call Obj.LoadConfig(ConfigID, IsPersonal, IsShotGun, txtNotes.Text, isActive, isFav)
            ChkPerLoad.Checked = IsPersonal
            If isActive Then
                rbstatus1.Checked = True
                rbstatus2.Checked = False
            Else
                rbstatus1.Checked = False
                rbstatus2.Checked = True
            End If
            chkFav.Checked = isFav
            Call LoadConfig_RiflePistol()
            Call LoadCosts()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub LoadConfig_RiflePistol()
        Try
            Dim Obj As New BSDatabase
            Dim ObjIM As New InventoryMath
            Dim SQL As String = "SELECT * from Config_List_Data_NSG where CLNID=" & ConfigID
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                txtAmmoType.Text = ObjIM.GetAmmoType(RS("ATID"))
                txtCal.Text = ObjIM.GetCaliber(RS("CALID"))
                Call ObjIM.LoadBulletInfo(RS("BID"), txtBManu.Text, txtBName.Text, txtBDia.Text, _
                        txtBWei.Text, txtBSecDen.Text, txtBPartNo.Text, txtBBCO.Text, _
                        INSTOCK_BULLET, txtBType.Text, COST_BULLET)
                Call ObjIM.LoadPrimerInfo(RS("PRID"), txtPManu.Text, txtPName.Text, _
                        txtPType.Text, COST_PRIMER, INSTOCK_PRIMER)
                Call ObjIM.LoadCaseInfo(RS("CAID"), txtCManu.Text, txtCName.Text, txtCTOL.Text, _
                        txtCTU.Text, INSTOCK_CASE, COST_CASE)
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
            Call LogError(Me.Name, "LoadConfig_RiflePistol", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub XML_Generate(ByVal strPath As String)
        Try
            Dim sAns As String = ""
            Dim NL As String = Chr(10) & Chr(13)
            sAns = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
            sAns &= "<Inventory>" & NL
            sAns &= XML_GenerateConfig()
            sAns &= XML_GenerateBullets()
            sAns &= XML_GeneratePrimers()
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
    Function XML_GeneratePowderList() As String
        Dim sAns As String = ""
        Dim NL As String = Chr(10) & Chr(13)
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT * from qry_CFG_SR_PowderList where CLNID=" & ConfigID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                sAns &= "    <General_Powder>" & NL
                sAns &= "       <Manufacturer>" & RS("Manufacturer") & "</Manufacturer>" & NL
                sAns &= "       <Name>" & RS("Name") & "</Name>" & NL
                sAns &= "       <Load_Min>" & RS("Load_Min") & "</Load_Min>" & NL
                sAns &= "       <Load_Mid>" & RS("Load_Mid") & "</Load_Mid>" & NL
                sAns &= "       <Load_Max>" & RS("Load_Max") & "</Load_Max>" & NL
                sAns &= "       <FPS_Min>" & RS("FPS_Min") & "</FPS_Min>" & NL
                sAns &= "       <FPS_MID>" & RS("FPS_MID") & "</FPS_MID>" & NL
                sAns &= "       <FPS_Max>" & RS("FPS_Max") & "</FPS_Max>" & NL
                sAns &= "       <CUPS_Min>" & RS("CUPS_Min") & "</CUPS_Min>" & NL
                sAns &= "       <CUPS_Mid>" & RS("CUPS_Mid") & "</CUPS_Mid>" & NL
                sAns &= "       <CUPS_Max>" & RS("CUPS_Max") & "</CUPS_Max>" & NL
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
        sAns &= "    </Details>" & NL
        'sAns &= "</Config>" & NL
        Return sAns
    End Function
    Function XML_GenerateCases() As String
        Dim sAns As String = ""
        Dim NL As String = Chr(10) & Chr(13)
        sAns = "   <List_Case>" & NL
        sAns &= "       <Manufacturer>" & txtCManu.Text & "</Manufacturer>" & NL
        sAns &= "       <Name>" & txtCName.Text & "</Name>" & NL
        sAns &= "       <ttl>" & txtCTOL.Text & "</ttl>" & NL
        sAns &= "       <TimesUsed>" & txtCTU.Text & "</TimesUsed>" & NL
        sAns &= "   </List_Case>" & NL
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
    Function XML_GenerateBullets() As String
        Dim sAns As String = ""
        Dim NL As String = Chr(10) & Chr(13)
        sAns = "   <List_Bullets>" & NL
        sAns &= "       <Manufacturer>" & txtBManu.Text & "</Manufacturer>" & NL
        sAns &= "       <Name>" & txtBName.Text & "</Name>" & NL
        sAns &= "       <Diameter>" & txtBDia.Text & "</Diameter>" & NL
        sAns &= "       <Weight>" & txtBWei.Text & "</Weight>" & NL
        sAns &= "       <Sec_Den>" & txtBSecDen.Text & "</Sec_Den>" & NL
        sAns &= "       <Part_number>" & txtBPartNo.Text & "</Part_number>" & NL
        sAns &= "       <Ballistic_Coefficient>" & txtBBCO.Text & "</Ballistic_Coefficient>" & NL
        sAns &= "       <Bullet_Type>" & txtBType.Text & "</Bullet_Type>" & NL
        sAns &= "   </List_Bullets>" & NL
        Return sAns
    End Function
#End Region
#Region "Form Related Subs"
    Private Sub frmView_Configuration_Sheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim ObjG As New GlobalFunctions
            ConfigName = ObjG.GetTitle(ConfigID)
            Me.Text = ConfigName & " Configuration Sheet"
            Call LoadData()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
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
    Private Sub frmView_Configuration_Sheet_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Width > 0 Then
            TabControl1.Width = Me.Width - 5
            TabControl1.Height = Me.Height - 60
            DataGridView1.Width = TabControl1.Width - 27
            DataGridView1.Height = TabControl1.Height - 69
            txtNotes.Width = TabControl1.Width - 27
            txtNotes.Height = TabControl1.Height - 69
        End If
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim frmNew As New frmConfig_Add_Wizard_Powder
        frmNew.ConfigID = ConfigID
        frmNew.ConfigName = ConfigName
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Call LoadPowderGrid()
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
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Me.Cursor = Cursors.WaitCursor
        Dim frmNew As New frmEditConfig
        frmNew.ConfigID = ConfigID
        frmNew.ConfigName = ConfigName
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
        Me.Close()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strSQLTable As String = "Config_List_Powder_Data_NSG"
            Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
            Obj.ConnExec(SQL)
            Call LoadPowderGrid()
        Catch ex As Exception
            Call LogError(Me.Name, "DeleteToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim frmNew As New frmReport_Configuration_Sheet
            frmNew.Config_ID = ConfigID
            frmNew.Config_Name = ConfigName
            frmNew.Config_AT = txtAmmoType.Text
            frmNew.Config_Cal = txtCal.Text
            frmNew.Config_Notes = txtNotes.Text
            frmNew.Bul_Manu = txtBManu.Text
            frmNew.Bul_Name = txtBName.Text
            frmNew.Bul_Dia = txtBDia.Text
            frmNew.Bul_Wei = txtBWei.Text
            frmNew.Bul_SD = txtBSecDen.Text
            frmNew.Bul_PN = txtBPartNo.Text
            frmNew.Bul_BC = txtBBCO.Text
            frmNew.Bul_BT = txtBType.Text
            frmNew.Pri_Manu = txtPManu.Text
            frmNew.Pri_Name = txtPName.Text
            frmNew.Pri_PT = txtPType.Text
            frmNew.Case_Manu = txtCManu.Text
            frmNew.Case_Name = txtCName.Text
            frmNew.Case_TTL = txtCTOL.Text
            frmNew.Case_TU = txtCTU.Text
            frmNew.Config_ISPersonal = IsPersonal
            frmNew.Config_Ref = lblReffer.Text
            frmNew.Config_Fav = chkFav.Checked
            frmNew.MdiParent = Me.MdiParent
            frmNew.Show()
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
        SaveFileDialog1.FileName = Replace(Replace(Replace(DefaultFileName, " ", "_"), "/", "-"), "\", "-")
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim strFilePath As String = SaveFileDialog1.FileName
        Call XML_Generate(strFilePath)
        Me.Close()
    End Sub
#End Region

    Private Sub SetAsDefaultToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetAsDefaultToolStripMenuItem.Click
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strSQLTable As String = "Config_List_Powder_Data_NSG"
            Dim SQL As String = "Update " & strSQLTable & " set IsPref=0 where CLNID=" & ConfigID
            Obj.ConnExec(SQL)
            SQL = "Update " & strSQLTable & " set IsPref=1 where ID=" & ItemID
            Obj.ConnExec(SQL)
            Call LoadData()
        Catch ex As Exception
            Call LogError(Me.Name, "DeleteToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class