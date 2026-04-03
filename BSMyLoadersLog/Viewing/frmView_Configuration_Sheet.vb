Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.ViewReports
Imports BurnSoft.Applications.MLL.ConfigSheets
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.Types
Imports BurnSoft.Universal

Namespace Viewing

    Public Class FrmViewConfigurationSheet
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim errOut as String
        ''' <summary>
        ''' The configuration identifier
        ''' </summary>
        Public ConfigID As Long
        ''' <summary>
        ''' The configuration name
        ''' </summary>
        Public ConfigName As String
        ''' <summary>
        ''' The is personal
        ''' </summary>
        Dim IsPersonal As Boolean
        ''' <summary>
        ''' The is shot gun
        ''' </summary>
        Dim IsShotGun As Boolean
        ''' <summary>
        ''' The is fav
        ''' </summary>
        Dim isFav As Boolean
        ''' <summary>
        ''' The is active
        ''' </summary>
        Dim isActive As Boolean
        ''' <summary>
        ''' The cost bullet
        ''' </summary>
        Dim COST_BULLET As Double
        ''' <summary>
        ''' The cost primer
        ''' </summary>
        Dim COST_PRIMER As Double
        ''' <summary>
        ''' The cost case
        ''' </summary>
        Dim COST_CASE As Double
        ''' <summary>
        ''' The cost powder
        ''' </summary>
        Dim COST_POWDER As Double
        ''' <summary>
        ''' The mid powder
        ''' </summary>
        Dim MID_POWDER As Double
        ''' <summary>
        ''' The instock bullet
        ''' </summary>
        Dim INSTOCK_BULLET As Long
        ''' <summary>
        ''' The instock primer
        ''' </summary>
        Dim INSTOCK_PRIMER As Long
        ''' <summary>
        ''' The instock case
        ''' </summary>
        Dim INSTOCK_CASE As Long
        ''' <summary>
        ''' The instock powder
        ''' </summary>
        Dim INSTOCK_POWDER As Double
        ''' <summary>
        ''' The preffered powder identifier
        ''' </summary>
        Dim PrefferedPowderID As Long
#Region "Subs"
        ''' <summary>
        ''' Updates the activity.
        ''' </summary>
        ''' <param name="iStat">The i stat.</param>
        <Obsolete("Repalced by BurnSoft.Applications.MLL.ConfigSheets.ConfigListDataName.SetActivity")>
        Sub UpdateActivity(ByVal iStat As Integer)
            Dim Obj As New BSDatabase
            Dim SQL As String = "UPDATE Config_List_Name set IsActive=" & iStat & " where id=" & ConfigID
            Obj.ConnExec(SQL)
        End Sub
        <Obsolete("Repalced by BurnSoft.Applications.MLL.ConfigSheets.ConfigListDataName.SetFavorite")>
        Sub UpdateFav(ByVal iStat As Integer)
            Dim Obj As New BSDatabase
            Dim SQL As String = "UPDATE Config_List_Name set IsFav=" & iStat & " where id=" & ConfigID
            Obj.ConnExec(SQL)
        End Sub
        ''' <summary>
        ''' Loads the powder grid.
        ''' Columns 7,8,9 are FPS, and columns 10,11,12 are CUPS
        ''' </summary>
        Sub LoadPowderGrid()
            DataGridView1.Columns(7).Visible = ViewFps
            DataGridView1.Columns(8).Visible = ViewFps
            DataGridView1.Columns(9).Visible = ViewFps
            DataGridView1.Columns(10).Visible = ViewCups
            DataGridView1.Columns(11).Visible = ViewCups
            DataGridView1.Columns(12).Visible = ViewCups
            Config_List_Powder_Data_NSG_ViewTableAdapter.FillBy_ConfigID(MLLDataSet.Config_List_Powder_Data_NSG_View, ConfigID)
        End Sub
        ''' <summary>
        ''' Loads the costs.
        ''' </summary>
        Sub LoadCosts()
            Try
                'Dim lnmr As Long = 0
                'Dim dPowPerB As Double = 0
                Dim costForOneRound As Double = 0
                'Dim Obj As New InventoryMath
                txtCPB.Text = Converters.ConvertToDollars(COST_BULLET)
                txtCPP.Text = Converters.ConvertToDollars(COST_PRIMER)
                txtCPC.Text = Converters.ConvertToDollars(COST_CASE)
                txtCOPMid.Text = Converters.ConvertToDollars((COST_POWDER * MID_POWDER))
                'Cost Seems higher txtC1RA
                'dC1RA = ((COST_POWDER * MID_POWDER) + COST_CASE + COST_PRIMER + COST_BULLET)
                costForOneRound = Converters.CostOfRoundsOfAmmoMetalic(COST_PRIMER, COST_CASE, COST_BULLET, COST_POWDER, MID_POWDER)
                txtC1RA.Text = costForOneRound
                txtCBIS.Text = INSTOCK_BULLET
                txtCPriIS.Text = INSTOCK_PRIMER
                txtCPowIS.Text = INSTOCK_POWDER
                txtCCIS.Text = INSTOCK_CASE

                'lnmr = INSTOCK_BULLET
                'If lnmr < INSTOCK_CASE Then
                '    lnmr = INSTOCK_BULLET
                'ElseIf lnmr > INSTOCK_CASE Then
                '    lnmr = INSTOCK_CASE
                'End If
                'dPowPerB = (INSTOCK_POWDER / MID_POWDER)
                'If lnmr > INSTOCK_PRIMER Then lnmr = INSTOCK_PRIMER
                'If lnmr > dPowPerB Then lnmr = CLng(dPowPerB)
                Dim lowestQtyInStock As Long = GeneralCalculations.CalculateMetallicRoundsToMake(INSTOCK_BULLET, INSTOCK_CASE, INSTOCK_PRIMER, 
                                                                         INSTOCK_POWDER, MID_POWDER, errOut)
                If errOut.Length > 0 Then throw New Exception(errOut)
                txtNMR.Text = lowestQtyInStock
                txtTCR.Text = lowestQtyInStock * Converters.ConvertToDollars(costForOneRound)
            Catch ex As Exception
                Call LogError(Name, "LoadCosts", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Sub LoadData()
            Lastconfigedviewed = ConfigID
            Try
                Loaders_Log_Ammunition_AuditTableAdapter.FillByConfigID(MLLDataSet.Loaders_Log_Ammunition_Audit, ConfigID)
                IsShotGun = False
                IsPersonal = False
                txtConfigName.Text = ConfigName
                'Dim Obj As New InventoryMath
                'PrefferedPowderID = Obj.GetPrefNSGPowderID(ConfigID, MID_POWDER)
                'COST_POWDER = Obj.GetPricePerPowder(PrefferedPowderID)
                'INSTOCK_POWDER = Obj.GetQTYPerPowder(PrefferedPowderID)

                PrefferedPowderID = ConfigListDataPowder.GetDefaultPowderId(DatabasePath, ConfigID, MID_POWDER, errOut)
                COST_POWDER = PowderInventory.GetPricePerPowder(DatabasePath, PrefferedPowderID, errOut)
                INSTOCK_POWDER = PowderInventory.GetQtyPerPowder(DatabasePath, PrefferedPowderID, errOut)

                Call LoadPowderGrid()
                'Call Obj.LoadConfig(ConfigID, IsPersonal, IsShotGun, txtNotes.Text, isActive, isFav)
                Dim lst as List(Of ConfigNameList) = ConfigListDataName.GetDetails(DatabasePath, ConfigID, errOut)
                For Each o As ConfigNameList In lst
                    IsPersonal = o.IsPersonal
                    IsShotGun = o.IsShotGun
                    txtNotes.Text = o.Notes
                    isActive = o.IsActive
                    isFav = o.IsFavorite
                Next
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
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Loads the configuration rifle pistol.
        ''' </summary>
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
                Call LogError(Name, "LoadConfig_RiflePistol", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        <Obsolete("Replace by BurnSoft.Applications.MLL.Xml.ConfigurationSheets.Generate")>
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
                'Dim ObjFS As New BSFileSystem
                'ObjFS.DeleteFile(strPath)
                'ObjFS.OutPutToFile(strPath, sAns)
                Dim ObjFS As New FileIO
                ObjFS.DeleteFile(strPath)
                ObjFS.AppendToFile(strPath, sAns)
                MsgBox("Config was exported to " & Chr(10) & strPath)
            Catch ex As Exception
                Call LogError(Name, "XML_Generate", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        <Obsolete("Replace by BurnSoft.Applications.MLL.Xml.ConfigurationSheets.Generate")>
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
                Call LogError(Name, "XML_GeneratePowderList", Err.Number, ex.Message.ToString)
            End Try
            Return sAns
        End Function
        <Obsolete("Replace by BurnSoft.Applications.MLL.Xml.ConfigurationSheets.Generate")>
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
        <Obsolete("Replace by BurnSoft.Applications.MLL.Xml.ConfigurationSheets.Generate")>
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
        <Obsolete("Replace by BurnSoft.Applications.MLL.Xml.ConfigurationSheets.Generate")>
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
        <Obsolete("Replace by BurnSoft.Applications.MLL.Xml.ConfigurationSheets.Generate")>
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
        ''' <summary>
        ''' Handles the Load event of the frmView_Configuration_Sheet control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmView_Configuration_Sheet_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                'Dim ObjG As New GlobalFunctions
                'ConfigName = ObjG.GetTitle(ConfigID)
                ConfigName = GeneralFunctions.GetTitle(DatabasePath,ConfigID, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
                Text = $"{ConfigName} Configuration Sheet"
                Call LoadData()
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnAddNotes control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnAddNotes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddNotes.Click
            btnAddNotes.Enabled = False
            btnUpdate.Visible = True
            txtNotes.ReadOnly = False
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnUpdate control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
            btnUpdate.Visible = False
            btnAddNotes.Enabled = True
            txtNotes.ReadOnly = True
            Try
                Dim Obj As New BSDatabase
                Dim strNotes As String = GeneralHelpers.FluffContent(txtNotes.Text)
                'TODO: Repalced by BurnSoft.Applications.MLL.ConfigSheets.ConfigListDataName.UpdateNotes
                Dim SQL As String = "UPDATE Config_List_Name set Notes='" & strNotes & "' where ID=" & ConfigID
                Obj.ConnExec(SQL)
            Catch ex As Exception
                Call LogError(Name, "btnUpdate.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Resize event of the frmView_Configuration_Sheet control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_Configuration_Sheet_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
            If Width > 0 Then
                TabControl1.Width = Width - 5
                TabControl1.Height = Height - 60
                DataGridView1.Width = TabControl1.Width - 27
                DataGridView1.Height = TabControl1.Height - 69
                txtNotes.Width = TabControl1.Width - 27
                txtNotes.Height = TabControl1.Height - 69
            End If
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnAdd control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Dim frmNew As New frmConfig_Add_Wizard_Powder
            frmNew.ConfigID = ConfigID
            frmNew.ConfigName = ConfigName
            frmNew.MdiParent = MdiParent
            frmNew.Show()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnRefresh control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
            Call LoadPowderGrid()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton2.Click
            Close()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
            Dim frmNew As New FrmLoadMakeReadyDetails
            frmNew.MdiParent = MdiParent
            frmNew.ConfigId = ConfigID
            frmNew.ConfigName = ConfigName
            frmNew.Show()
            Close()
        End Sub
        ''' <summary>
        ''' Handles the CheckedChanged event of the rbstatus1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub rbstatus1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbstatus1.CheckedChanged
            If rbstatus1.Checked Then
                rbstatus2.Checked = False
                isActive = True
                Call UpdateActivity(1)
                Call MDIParentMain.RefreshConfigData()
            End If
        End Sub
        ''' <summary>
        ''' Handles the CheckedChanged event of the rbstatus2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub rbstatus2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbstatus2.CheckedChanged
            If rbstatus2.Checked Then
                rbstatus1.Checked = False
                isActive = False
                Call UpdateActivity(0)
                Call MDIParentMain.RefreshConfigData()
            End If
        End Sub
        ''' <summary>
        ''' Handles the CheckedChanged event of the chkFav control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub chkFav_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkFav.CheckedChanged
            If chkFav.Checked Then
                isFav = True
                Call UpdateFav(1)
            Else
                isFav = False
                Call UpdateFav(0)
            End If
            Call MDIParentMain.RefreshConfigData()
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
        ''' Handles the Click event of the ToolStripButton3 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton3.Click
            Cursor = Cursors.WaitCursor
            Dim frmNew As New frmEditConfig
            frmNew.ConfigID = ConfigID
            frmNew.ConfigName = ConfigName
            frmNew.MdiParent = MdiParent
            frmNew.Show()
            Close()
            Cursor = Cursors.Arrow
        End Sub
        Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
            Try
                Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim Obj As New BSDatabase
                Dim ObjG As New GlobalFunctions
                Dim strSQLTable As String = "Config_List_Powder_Data_NSG"
                Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
                Obj.ConnExec(SQL)
                Call LoadPowderGrid()
            Catch ex As Exception
                Call LogError(Name, "DeleteToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton4 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton4.Click
            Cursor = Cursors.WaitCursor
            Try
                Dim frmNew As New FrmReportConfigurationSheet
                frmNew.ConfigId = ConfigID
                frmNew.ConfigName = ConfigName
                frmNew.ConfigAmmoType = txtAmmoType.Text
                frmNew.ConfigCaliber = txtCal.Text
                frmNew.ConfigNotes = txtNotes.Text
                frmNew.BulletManufacturer = txtBManu.Text
                frmNew.BulletName = txtBName.Text
                frmNew.BulletDiameter = txtBDia.Text
                frmNew.BulletWeight = txtBWei.Text
                frmNew.BulletSectionalDensity = txtBSecDen.Text
                frmNew.BulletPartNumber = txtBPartNo.Text
                frmNew.BulletBallisticCoeffcient = txtBBCO.Text
                frmNew.BulletType = txtBType.Text
                frmNew.PrimerManufacturer = txtPManu.Text
                frmNew.PrimerName = txtPName.Text
                frmNew.PrimerType = txtPType.Text
                frmNew.CaseManufacturer = txtCManu.Text
                frmNew.CaseName = txtCName.Text
                frmNew.CaseTrimToLength = txtCTOL.Text
                frmNew.CaseTimesUsed = txtCTU.Text
                frmNew.ConfigIsPersonal = IsPersonal
                frmNew.ConfigReferance = lblReffer.Text
                frmNew.ConfigFavorite = chkFav.Checked
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton4_Click", Err.Number, ex.Message.ToString)
            End Try
            Cursor = Cursors.Arrow
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton5 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton5.Click
            Dim DefaultFileName As String = "ExportConfig_" & ConfigName & ".xml"
            SaveFileDialog1.FilterIndex = 1
            SaveFileDialog1.Filter = "XML File(*.xml)|*.xml"
            SaveFileDialog1.Title = "Export Data to XML File"
            SaveFileDialog1.FileName = Replace(Replace(Replace(DefaultFileName, " ", "_"), "/", "-"), "\", "-")
            If SaveFileDialog1.ShowDialog() = DialogResult.Cancel Then Exit Sub
            Dim strFilePath As String = SaveFileDialog1.FileName
            Call XML_Generate(strFilePath)
            Close()
        End Sub
#End Region
        ''' <summary>
        ''' Handles the Click event of the SetAsDefaultToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub SetAsDefaultToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SetAsDefaultToolStripMenuItem.Click
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
                Call LogError(Name, "DeleteToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace