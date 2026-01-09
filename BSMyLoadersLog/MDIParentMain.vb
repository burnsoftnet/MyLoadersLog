
Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Imports System.IO
Imports BurnSoft.MsgBox
''' <summary>
''' Class MdiParentMain.
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class MdiParentMain

#Region "Form Subs"
    ''' <summary>
    ''' Handles the Disposed event of the MDIParentMain control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MDIParentMain_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        Try
            If DoAutoBackup Then
                Dim myProcess As New Process
                myProcess.StartInfo.FileName = Application.StartupPath & "\" & MY_BACKUP
                myProcess.StartInfo.Arguments = "/auto"
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                myProcess.Start()
            End If
        Catch ex As Exception
            Call LogError(Name, "Disposed", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Load event of the MDIParent2 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MDIParent2_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            LASTCONFIGEDVIEWED = 0
            'MyLogFile = Application.StartupPath & "\err.log"
            Call CheckforHotFix()
            If LoginEnabled(UseMyPWD, UseMyUID, UseMyForgotWord, UseMyForgotPhrase) And Not IsLoggedIN Then
                frmLogin.Show()
                Close()
            End If
            Dim obj As New BSRegistry
            OwnerID = GetOwnerID()
            Call obj.UpDateAppDetails()
            Call obj.GetSettings(LastSucBackup, AlertOnBackUp, TrackHistoryDays, TrackHistory, DoAutoBackup, DoOriginalImage, UsePetLoads, cmbConfigSort.Text)

            ToolStripStatusLabel.Text = ""
            ToolStripSeparator4.Visible = False

            If OwnerID = 0 Then
                Dim frmNew As New frmOptions
                frmNew.MdiParent = Me
                frmNew.Show()
            End If
            If Not USE_SHOTGUN Then
                ToolStripButton6.Visible = False
                WADListToolStripMenuItem.Visible = False
                ShellListToolStripMenuItem.Visible = False
                ShotListToolStripMenuItem.Visible = False
                SlugListToolStripMenuItem.Visible = False
                BushingsChargeBarToolStripMenuItem.Visible = False
                WADInventoryToolStripMenuItem.Visible = False
                ShellInventoryToolStripMenuItem.Visible = False
                ShotInventoryToolStripMenuItem.Visible = False
                SlugInventoryToolStripMenuItem.Visible = False
                ShotgunsToolStripMenuItem.Visible = False
                ShotgunGaugesToolStripMenuItem.Visible = False
                ShotWeightToolStripMenuItem.Visible = False

            End If
            OwnerLoadName = Replace(GetLoadName(), "''", "'")
            If OwnerLoadName <> "My Loaders Log" Then Text = OwnerLoadName & " Loaders Log"
            Call RefreshData()
            Call InitForm()
            Call InitLoaderType()
            Dim objFs As New BSFileSystem
            If objFs.FileExists(MY_HOTFIX_FILE) Then ReRunHotfixUpdatesToolStripMenuItem.Enabled = True
        Catch ex As Exception
            Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Resize event of the MDIParentMain control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MDIParentMain_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
        If Height > 0 Then
            lstCal.Height = TabControl1.Height - 90
            lstConfigSheets.Height = TabControl1.Height - 110
        End If
    End Sub
#End Region
#Region "Subs and Functions"
    ''' <summary>
    ''' Refreshes the data.
    ''' </summary>
    Public Sub RefreshData()
        Call RefreshCalData()
        Call RefreshConfigData()
    End Sub
    ''' <summary>
    ''' Refreshes the cal data.
    ''' </summary>
    Public Sub RefreshCalData()
        Try
            List_CalibersTableAdapter.Fill(MLLDataSet.List_Calibers)
        Catch ex As Exception
            Call LogError(Name, "RefreshCalData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Refreshes the configuration data.
    ''' </summary>
    Public Sub RefreshConfigData()
        Try
            ConfigListNameBindingSource.ResetBindings(True)
            Dim selectedView As String = cmbConfigSort.SelectedItem.ToString
            Select Case UCase(selectedView)
                Case UCase("All")
                    Config_List_NameTableAdapter.Fill(MLLDataSet.Config_List_Name)
                Case UCase("Active Only")
                    Config_List_NameTableAdapter.FillBy_Active(MLLDataSet.Config_List_Name)
                Case UCase("Inactive Only")
                    Config_List_NameTableAdapter.FillBy_Inactive(MLLDataSet.Config_List_Name)
                Case UCase("All Favorites")
                    Config_List_NameTableAdapter.FillBy_Fav(MLLDataSet.Config_List_Name)
                Case UCase("Shotgun Loads")
                    Config_List_NameTableAdapter.FillBy_Shotgun(MLLDataSet.Config_List_Name)
                Case UCase("Rifle & Pistol Loads")
                    Config_List_NameTableAdapter.FillBy_RiflePistol(MLLDataSet.Config_List_Name)
                Case UCase("Personal Loads")
                    Config_List_NameTableAdapter.FillBy_Personal(MLLDataSet.Config_List_Name)
                Case UCase("Reffered Loads")
                    Config_List_NameTableAdapter.FillBy_NonPersonal(MLLDataSet.Config_List_Name)
                Case Else
                    Config_List_NameTableAdapter.Fill(MLLDataSet.Config_List_Name)
            End Select
            lstConfigSheets.Refresh()
            If LASTCONFIGEDVIEWED > 0 Then lstConfigSheets.SelectedValue = LASTCONFIGEDVIEWED
            Dim objR As New BSRegistry
            objR.SaveConfigSort(selectedView)
        Catch ex As Exception
            Call LogError(Name, "RefreshConfigData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Deinits the type of the loader.
    ''' </summary>
    Private Sub DeinitLoaderType()
        BulletToolStripMenuItem.Visible = False
        CaseToolStripMenuItem.Visible = False
        WADToolStripMenuItem.Visible = False
        ShellToolStripMenuItem.Visible = False
        BulletToolStripMenuItem1.Visible = False
        CaseToolStripMenuItem1.Visible = False
        WADListToolStripMenuItem.Visible = False
        ShellListToolStripMenuItem.Visible = False
        ToolStripSeparator7.Visible = False
        ToolStripSeparator10.Visible = False
        ToolStripSeparator13.Visible = False
        ToolStripSeparator14.Visible = False
        ShotgunToolStripMenuItem.Visible = False
        ShotgunToolStripMenuItem1.Visible = False
        RiflePistolToolStripMenuItem.Visible = False
        RiflePistolToolStripMenuItem1.Visible = False
        WADInventoryToolStripMenuItem.Visible = False
        ShellInventoryToolStripMenuItem.Visible = False
        CaseBrassInventoryToolStripMenuItem.Visible = False
        BulletInventoryToolStripMenuItem.Visible = False
        ShotgunGaugesToolStripMenuItem.Visible = False
        ShotWeightToolStripMenuItem.Visible = False
        ShotgunsToolStripMenuItem.Visible = False
        RifleAndPistolsToolStripMenuItem.Visible = False
        ToolStripButton6.Visible = False
        ToolStripButton5.Visible = False
        SlugsToolStripMenuItem.Visible = False
        ShotToolStripMenuItem.Visible = False
        SlugListToolStripMenuItem.Visible = False
        ShotListToolStripMenuItem.Visible = False
        ShotInventoryToolStripMenuItem.Visible = False
        SlugInventoryToolStripMenuItem.Visible = False
        PowderBushingsToolStripMenuItem.Visible = False
        BushingsChargeBarToolStripMenuItem.Visible = False
    End Sub
    ''' <summary>
    ''' Initializes the reg values.
    ''' </summary>
    Private Sub InitRegValues()
        Dim objr As New BSRegistry
        Call objr.UpDateAppDetails()
    End Sub
    ''' <summary>
    ''' Initializes the type of the loader.
    ''' </summary>
    Public Sub InitLoaderType()
        Try
            Call DeinitLoaderType()
            If LOADERTYPE_SHOTGUN Then
                ToolStripSeparator7.Visible = True
                ToolStripSeparator14.Visible = True
                WADToolStripMenuItem.Visible = True
                ShellToolStripMenuItem.Visible = True
                WADListToolStripMenuItem.Visible = True
                ShellListToolStripMenuItem.Visible = True
                ShotgunToolStripMenuItem.Visible = True
                ShotgunToolStripMenuItem1.Visible = True
                WADInventoryToolStripMenuItem.Visible = True
                ShellInventoryToolStripMenuItem.Visible = True
                ShotgunGaugesToolStripMenuItem.Visible = True
                ShotWeightToolStripMenuItem.Visible = True
                ShotgunsToolStripMenuItem.Visible = True
                ToolStripButton6.Visible = True
                SlugsToolStripMenuItem.Visible = True
                ShotToolStripMenuItem.Visible = True
                SlugListToolStripMenuItem.Visible = True
                ShotListToolStripMenuItem.Visible = True
                ShotInventoryToolStripMenuItem.Visible = True
                SlugInventoryToolStripMenuItem.Visible = True
                PowderBushingsToolStripMenuItem.Visible = True
                BushingsChargeBarToolStripMenuItem.Visible = True
            End If
            If LOADERTYPE_NONSHOTGUN Then
                ToolStripSeparator13.Visible = True
                ToolStripSeparator10.Visible = True
                BulletToolStripMenuItem.Visible = True
                CaseToolStripMenuItem.Visible = True
                BulletToolStripMenuItem1.Visible = True
                CaseToolStripMenuItem1.Visible = True
                RiflePistolToolStripMenuItem.Visible = True
                RiflePistolToolStripMenuItem1.Visible = True
                CaseBrassInventoryToolStripMenuItem.Visible = True
                BulletInventoryToolStripMenuItem.Visible = True
                RifleAndPistolsToolStripMenuItem.Visible = True
                ToolStripButton5.Visible = True
            End If
        Catch ex As Exception
            Call LogError(Name, "InitLoaderType", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Initializes the form.
    ''' </summary>
    Sub InitForm()
        Try
            Dim obj As New BSMGC
            If obj.MyGunCollectionIsInstalled Then
                tsslMGCEnabled.Enabled = True
                tsslMGCEnabled.Visible = True
                MGCPath = obj.GetMGCPath
                SaveAsToolStripMenuItem.Enabled = True
                ToolStripButton1.Enabled = True
                ExportFirearmsToMGCToolStripMenuItem.Enabled = True
            Else
                SaveAsToolStripMenuItem.Enabled = False
                ToolStripButton1.Enabled = False
                ExportFirearmsToMGCToolStripMenuItem.Enabled = False
            End If
            Select Case DEFAULTLIST
                Case "Caliber List"
                    TabControl1.SelectedTab = TabPage1  'System.Windows.Forms.TabPage(1)
                Case "Configuration List"
                    TabControl1.SelectedTab = TabPage2
            End Select
        Catch ex As Exception
            Call LogError(Name, "InitForm", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Does the backup.
    ''' </summary>
    Sub DoBackup()
        Try
            DoAutoBackup = False
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = Application.StartupPath & "\" & MY_BACKUP
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            myProcess.Start()
            Close()
        Catch ex As Exception
            Call LogError(Name, "DoBackup", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Does the restore.
    ''' </summary>
    Sub DoRestore()
        Try
            DoAutoBackup = False
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = Application.StartupPath & "\" & MY_RESTORE
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            myProcess.Start()
            Close()
        Catch ex As Exception
            Call LogError(Name, "DoRestore", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Checkfors the hot fix.
    ''' </summary>
    Sub CheckforHotFix()
        Dim objf As New BSFileSystem
        If objf.FileExists(Application.StartupPath & "\hotfix.ini") Then
            Dim myProcess As New Process
            Dim runThiSApp As String = Application.StartupPath & "\" & MY_HOTFIX_FILE
            myProcess.StartInfo.FileName = runThiSApp
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            DoAutoBackup = False
            myProcess.Start()
            Application.Exit()
        End If
    End Sub
    ''' <summary>
    ''' Does the help.
    ''' </summary>
    Sub DoHelp()
        Try
            Help.ShowHelp(Me, MY_HELP_FILE)
        Catch ex As Exception
            Call LogError(Name, "DoHelp", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Checks the backup.
    ''' </summary>
    Sub CheckBackup()
        Try
            Dim objR As New BSRegistry
            If Not AlertOnBackUp Then Exit Sub
            Dim myLastDateDiff As Long = DateDiff(DateInterval.Day, CDate(LastSucBackup), DateTime.Now)
            Dim obj As New MsgClass
            If myLastDateDiff > TrackHistoryDays Then obj.DoMessage("It has been " & myLastDateDiff & " days since your last backup.", MgboxStyle.Inf_OK, MgBtnStyle.mb_Exclamantion, "Last Backup Notice", , True, "Backup Warning", False)
        Catch ex As Exception
            Call LogError(Name, "CheckBackup", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Copies the configuration details NSG.
    ''' </summary>
    ''' <param name="myId">My identifier.</param>
    ''' <param name="configId">The configuration identifier.</param>
    Private Sub CopyConfigDetailsNsg(ByVal myId As Long, ByVal configId As Long)
        Try
            Dim sql As String = "SELECT * from Config_List_Data_NSG where CLNID=" & configId
            Dim obj As New BSDatabase
            Call obj.ConnectDB()
            Dim cmd As New OdbcCommand(sql, obj.Conn)
            Dim rs As OdbcDataReader
            rs = cmd.ExecuteReader
            While rs.Read
                sql = "INSERT INTO Config_List_Data_NSG (CLNID,ATID,CALID,BID,PRID,CAID,Source) VALUES(" & _
                        myId & "," & rs("ATID") & "," & rs("CALID") & "," & rs("BID") & "," & rs("PRID") & "," & rs("CAID") & ",'" & _
                        rs("Source") & "')"
                obj.ConnExec(sql)
            End While
            rs.Close()

        Catch ex As Exception
            Call LogError(Name, "CopyConfigDetailsNSG", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Copies the configuration powders NSG.
    ''' </summary>
    ''' <param name="myId">My identifier.</param>
    ''' <param name="configId">The configuration identifier.</param>
    Private Sub CopyConfigPowdersNsg(ByVal myId As Long, ByVal configId As Long)
        Try
            Dim sql As String = "SELECT * from Config_List_Powder_Data_NSG where CLNID=" & configId
            Dim obj As New BSDatabase
            Call obj.ConnectDB()
            Dim cmd As New OdbcCommand(sql, obj.Conn)
            Dim rs As OdbcDataReader
            rs = cmd.ExecuteReader
            While rs.Read
                sql = "INSERT INTO Config_List_Powder_Data_NSG (CLNID,PID,Load_Min,Load_Mid,Load_Max," & _
                            "FPS_Min,FPS_MID,FPS_Max,CUPS_Min,CUPS_Mid,CUPS_Max,IsPref) VALUES(" & myId & _
                            "," & rs("PID") & "," & rs("Load_Min") & "," & rs("Load_Mid") & "," & rs("Load_Max") & "," & _
                             rs("FPS_Min") & "," & rs("FPS_MID") & "," & rs("FPS_Max") & "," & rs("CUPS_Min") & "," & _
                              rs("CUPS_Mid") & "," & rs("CUPS_Max") & "," & rs("IsPref") & ")"
                obj.ConnExec(sql)
            End While
            rs.Close()
        Catch ex As Exception
            Call LogError(Name, "CopyConfigPowdersNSG", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Copies the configuration details sg.
    ''' </summary>
    ''' <param name="myId">My identifier.</param>
    ''' <param name="configId">The configuration identifier.</param>
    Private Sub CopyConfigDetailsSg(ByVal myId As Long, ByVal configId As Long)
        Try
            Dim sql As String = "SELECT * from Config_List_Data_SG where CLNID=" & configId
            Dim obj As New BSDatabase
            Call obj.ConnectDB()
            Dim cmd As New OdbcCommand(sql, obj.Conn)
            Dim rs As OdbcDataReader
            rs = cmd.ExecuteReader
            While rs.Read
                sql = "INSERT INTO Config_List_Data_SG (CLNID,ATID,CALID,PRID,CAID,Source,SW,SS,Bushing,WAD,SCL,SW_t,GID,IsPersonal) VALUES(" & _
                        myId & "," & rs("ATID") & "," & rs("CALID") & "," & rs("PRID") & "," & rs("CAID") & ",'" & _
                        rs("Source") & "'," & rs("SW") & "," & rs("SS") & "," & rs("Bushing") & "," & rs("WAD") & _
                        "," & rs("SCL") & ",'" & rs("SW_t") & "'," & rs("GID") & "," & rs("IsPersonal") & ")"
                obj.ConnExec(sql)
            End While
            rs.Close()
            rs = Nothing
            cmd = Nothing
        Catch ex As Exception
            Call LogError(Name, "CopyConfigDetailsSG", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Copies the configuration powders sg.
    ''' </summary>
    ''' <param name="myId">My identifier.</param>
    ''' <param name="configId">The configuration identifier.</param>
    Private Sub CopyConfigPowdersSg(ByVal myId As Long, ByVal configId As Long)
        Try
            Dim sql As String = "SELECT * from Config_List_Powder_Data_SG where CLNID=" & configId
            Dim obj As New BSDatabase
            Call obj.ConnectDB()
            Dim cmd As New OdbcCommand(sql, obj.Conn)
            Dim rs As OdbcDataReader
            rs = cmd.ExecuteReader
            While rs.Read
                sql = "INSERT INTO Config_List_Powder_Data_SG (CLNID,PID,Load_Min,Load_Mid,Load_Max," & _
                            "FPS_Min,FPS_MID,FPS_Max,PSI_Min,PSI_Mid,PSI_Max,IsPref) VALUES(" & myId & _
                            "," & rs("PID") & "," & rs("Load_Min") & "," & rs("Load_Mid") & "," & rs("Load_Max") & "," & _
                             rs("FPS_Min") & "," & rs("FPS_MID") & "," & rs("FPS_Max") & "," & rs("PSI_Min") & "," & _
                              rs("PSI_Mid") & "," & rs("PSI_Max") & "," & rs("IsPref") & ")"
                obj.ConnExec(sql)
            End While
            rs.Close()
            rs = Nothing
            cmd = Nothing
        Catch ex As Exception
            Call LogError(Name, "CopyConfigPowdersSG", Err.Number, ex.Message.ToString)
        End Try
    End Sub
#End Region
#Region "Tool Bar And Misc. Components Subs"
    ''' <summary>
    ''' Handles the Click event of the ToolStripButton1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
        frmImportFirearms.MdiParent = Me
        frmImportFirearms.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the SaveToolStripButton control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub SaveToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripButton.Click
        frmOptions.MdiParent = Me
        frmOptions.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the HelpToolStripButton control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub HelpToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles HelpToolStripButton.Click
        Call DoHelp()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnAddCal2List control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnAddCal2List_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddCal2List.Click
        frmAddCaliberToCollection.MdiParent = Me
        frmAddCaliberToCollection.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnAddConfig control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnAddConfig_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddConfig.Click
        frmConfig_Add_Wizard.MdiParent = Me
        frmConfig_Add_Wizard.Show()
    End Sub
    ''' <summary>
    ''' Views the configs.
    ''' </summary>
    Sub ViewConfigs()
        Dim objGs As New GlobalFunctions
        Dim lngConfigId As Long = lstConfigSheets.SelectedValue
        Dim configType As Boolean = objGs.IsShotGunCOnfig(lngConfigId)

        If Not configType Then
            Dim frmNew As New frmView_Configuration_Sheet
            frmNew.ConfigID = lngConfigId
            frmNew.MdiParent = Me
            frmNew.Show()
        Else
            Dim frmNew As New frmView_Configuration_Shotgun_Sheet
            frmNew.ConfigID = lngConfigId
            frmNew.MdiParent = Me
            frmNew.Show()
        End If
        LASTCONFIGEDVIEWED = lngConfigId
        lstConfigSheets.SelectedItem = LASTCONFIGEDVIEWED
    End Sub
    ''' <summary>
    ''' Handles the DoubleClick event of the lstConfigSheets control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub lstConfigSheets_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstConfigSheets.DoubleClick
        Call ViewConfigs()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ToolStripButton3 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ToolStripButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton3.Click
        Cursor = Cursors.WaitCursor
        frmView_List_Firearms.MdiParent = Me
        frmView_List_Firearms.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the DoubleClick event of the lstCal control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub lstCal_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstCal.DoubleClick
        Dim objGf As New GlobalFunctions
        Dim lngCalId As Long = lstCal.SelectedValue
        Dim isNsg As Boolean = objGf.IsNotInShotgunConfigbyCal(lngCalId)
        If isNsg Then
            Dim frmNew As New frmView_List_ConfigurationsByCal
            frmNew.CALID = lngCalId
            frmNew.MdiParent = Me
            frmNew.Show()
        Else
            Dim frmNewS As New frmView_List_ConfigurationsByCal_SG
            frmNewS.CALID = lngCalId
            frmNewS.MdiParent = Me
            frmNewS.Show()
        End If
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnDelete control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Dim lngConfigId As Long = lstConfigSheets.SelectedValue
        Dim obj As New BSDatabase
        Dim objG As New GlobalFunctions
        Dim strName As String = objG.GetName("SELECT * from Config_List_Name where ID=" & lngConfigId, "ConfigName")
        Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
        Dim sql As String = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & lngConfigId
        If strAns = vbYes Then
            obj.ConnExec(sql)
            sql = "DELETE from Config_List_Data_NSG where CLNID=" & lngConfigId
            obj.ConnExec(sql)
            sql = "DELETE from Loaders_Log_Ammunition_Audit where CFID=" & lngConfigId
            obj.ConnExec(sql)
            sql = "DELETE from Config_List_Name where ID=" & lngConfigId
            obj.ConnExec(sql)
            Call RefreshConfigData()
        End If
    End Sub
    ''' <summary>
    ''' Handles the Click event of the tsslErrorsFound control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub tsslErrorsFound_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsslErrorsFound.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MyLogFile
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        myProcess.Start()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the tsslMGCEnabled control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub tsslMGCEnabled_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsslMGCEnabled.Click
        Dim obj As New BSMGC
        Dim strPath As String = obj.GetMGCEXEPath
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = strPath
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        myProcess.Start()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ToolStripButton4 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ToolStripButton4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton4.Click
        Cursor = Cursors.WaitCursor
        frmView_Loaded_Ammunition.MdiParent = Me
        frmView_Loaded_Ammunition.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnImportConfig control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnImportConfig_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImportConfig.Click
        frmImportConfiguration.MdiParent = Me
        frmImportConfiguration.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ToolStripButton5 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ToolStripButton5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton5.Click
        Cursor = Cursors.WaitCursor
        frmSearchConfig_RiflePistol.MdiParent = Me
        frmSearchConfig_RiflePistol.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the SelectedIndexChanged event of the cmbConfigSort control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub cmbConfigSort_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbConfigSort.SelectedIndexChanged
        Call RefreshConfigData()
    End Sub
#End Region
#Region "Menu Subs"
    ''' <summary>
    ''' Handles the Click event of the SaveAsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        frmImportFirearms.MdiParent = Me
        frmImportFirearms.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ExitToolsStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ToolBarToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        ToolStrip.Visible = ToolBarToolStripMenuItem.Checked
    End Sub
    ''' <summary>
    ''' Handles the Click event of the StatusBarToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        'StatusStrip.Visible = StatusBarToolStripMenuItem.Checked
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CascadeToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        LayoutMdi(MdiLayout.Cascade)
    End Sub
    ''' <summary>
    ''' Handles the Click event of the TileVerticleToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        LayoutMdi(MdiLayout.TileVertical)
    End Sub
    ''' <summary>
    ''' Handles the Click event of the TileHorizontalToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        LayoutMdi(MdiLayout.TileHorizontal)
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ArrangeIconsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CloseAllToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each childForm As Form In MdiChildren
            childForm.Close()
        Next
    End Sub
    ''' <summary>
    ''' Handles the Click event of the AboutToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub AboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.MdiParent = Me
        AboutBox1.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the OptionsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OptionsToolStripMenuItem.Click
        frmOptions.MdiParent = Me
        frmOptions.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the SaveToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub SaveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click
        Call DoBackup()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PurchaseToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PurchaseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) 
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MENU_SHOP
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        myProcess.Start()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the TechnicalSupportToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub TechnicalSupportToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TechnicalSupportToolStripMenuItem.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MENU_SUPPORT
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        myProcess.Start()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ReportABugToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ReportABugToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ReportABugToolStripMenuItem.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MENU_BUG
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        myProcess.Start()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the KnowledgeBaseToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub KnowledgeBaseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles KnowledgeBaseToolStripMenuItem.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MENU_WIKI
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        myProcess.Start()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the SearchToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub SearchToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchToolStripMenuItem.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MENU_SITESEARCH
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        myProcess.Start()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the IndexToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndexToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles IndexToolStripMenuItem.Click
        Help.ShowHelpIndex(Me, MY_HELP_FILE)
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ContentsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ContentsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ContentsToolStripMenuItem.Click
        Call DoHelp()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PowderToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PowderToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PowderToolStripMenuItem.Click
        frmAddPowder.MdiParent = Me
        frmAddPowder.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PrimerToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PrimerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PrimerToolStripMenuItem.Click
        frmAddPrimer.MdiParent = Me
        frmAddPrimer.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the BulletToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BulletToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BulletToolStripMenuItem.Click
        frmAddBullets.MdiParent = Me
        frmAddBullets.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CaseToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CaseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CaseToolStripMenuItem.Click
        frmAddShells.MdiParent = Me
        frmAddShells.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the MyFirearmCollectionsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MyFirearmCollectionsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MyFirearmCollectionsToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmView_List_Firearms.MdiParent = Me
        frmView_List_Firearms.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the EquipmentToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub EquipmentToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EquipmentToolStripMenuItem.Click
        frmAddEquipment.MdiParent = Me
        frmAddEquipment.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the AddFirearmToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub AddFirearmToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddFirearmToolStripMenuItem.Click
        frmAddFirearm.MdiParent = Me
        frmAddFirearm.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the EquipmentToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub EquipmentToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EquipmentToolStripMenuItem1.Click
        Cursor = Cursors.WaitCursor
        frmView_List_Equipment.MdiParent = Me
        frmView_List_Equipment.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CaseToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CaseToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CaseToolStripMenuItem1.Click
        Cursor = Cursors.WaitCursor
        frmView_List_Shells.MdiParent = Me
        frmView_List_Shells.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PrimerListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PrimerListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PrimerListToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmView_List_Primer.MdiParent = Me
        frmView_List_Primer.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PowderListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PowderListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PowderListToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmView_List_Powder.MdiParent = Me
        frmView_List_Powder.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the BulletToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BulletToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BulletToolStripMenuItem1.Click
        Cursor = Cursors.WaitCursor
        frmView_List_Bullets.MdiParent = Me
        frmView_List_Bullets.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the MakeReadyToUseAmmunitionToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MakeReadyToUseAmmunitionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MakeReadyToUseAmmunitionToolStripMenuItem.Click
        Dim configId As Long = lstConfigSheets.SelectedValue
        Dim configName As String
        Dim frmNew As New frmLoadMakeReady_Details
        Dim objG As New GlobalFunctions
        configName = objG.GetTitle(configId)
        frmNew.ConfigID = configId
        frmNew.ConfigName = configName
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the DeleteToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim lngConfigId As Long = lstConfigSheets.SelectedValue
        Dim obj As New BSDatabase
        Dim objG As New GlobalFunctions
        Dim strName As String = objG.GetName("SELECT * from Config_List_Name where ID=" & lngConfigId, "ConfigName")
        Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
        Dim isShotGun As Boolean = objG.IsShotGunCOnfig(lngConfigId)
        Dim sql As String = ""
        If strAns = vbYes Then
            If Not isShotGun Then
                sql = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & lngConfigId
                obj.ConnExec(sql)
                sql = "DELETE from Config_List_Data_NSG where CLNID=" & lngConfigId
                obj.ConnExec(sql)
                sql = "DELETE from Config_List_Name where ID=" & lngConfigId
                obj.ConnExec(sql)
            Else
                sql = "DELETE from Config_List_Powder_Data_SG where CLNID=" & lngConfigId
                obj.ConnExec(sql)
                sql = "DELETE from Config_List_Data_SG where CLNID=" & lngConfigId
                obj.ConnExec(sql)
                sql = "DELETE from Config_List_Name where ID=" & lngConfigId
                obj.ConnExec(sql)
            End If
            Call RefreshConfigData()
        End If
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim configId As Long = lstConfigSheets.SelectedValue
        Dim configName As String
        Dim objG As New GlobalFunctions
        configName = objG.GetTitle(configId)
        Dim sMsg As String = "Renaming " & configName & " to:"
        Dim strNewName As String = Trim(FluffContent(InputBox(sMsg, "Rename Configuration Name", configName)))
        If Len(strNewName) <> 0 And LCase(strNewName) <> LCase(configName) Then
            Dim sql As String = "UPDATE Config_List_Name set ConfigName='" & strNewName & "' where id=" & configId
            Dim obj As New BSDatabase
            obj.ConnExec(sql)
            Call RefreshConfigData()
        End If
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ViewToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ViewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        Call ViewConfigs()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CopyToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
        Dim configId As Long = lstConfigSheets.SelectedValue
        Dim configName As String
        Dim objG As New GlobalFunctions
        configName = objG.GetTitle(configId)
        Dim isShotGun As Boolean = False
        Dim sMsg As String = "What do you wish to call this new configuration?"
        Dim strNewName As String = Trim(FluffContent(InputBox(sMsg, "Copy Configuration", configName)))
        If Len(strNewName) <> 0 And LCase(strNewName) <> LCase(configName) Then
            Dim sql As String = "SELECT * from Config_List_Name where ID=" & configId
            Dim obj As New BSDatabase
            Call obj.ConnectDB()
            Dim cmd As New OdbcCommand(sql, obj.Conn)
            Dim rs As OdbcDataReader
            rs = cmd.ExecuteReader
            While rs.Read()
                If rs("IsShotGun") = 1 Then isShotGun = True
                Dim strNotes As String = " "
                If Not IsDBNull(rs("notes")) Then strNotes = FluffContent(rs("Notes"))
                sql = "INSERT INTO Config_List_Name(ConfigName,IsPersonal,IsShotGun,Notes,IsActive,IsFav) VALUES('" & _
                        strNewName & "'," & rs("IsPersonal") & "," & rs("IsShotGun") & ",'" & _
                        strNotes & "'," & rs("IsActive") & "," & rs("IsFav") & ")"
                obj.ConnExec(sql)
            End While
            rs.Close()
            rs = Nothing
            cmd = Nothing
            Dim myId As Long = objG.GetID("SELECT * from Config_list_Name where ConfigName='" & strNewName & "'")
            If Not isShotGun Then
                Call CopyConfigDetailsNsg(myId, configId)
                Call CopyConfigPowdersNsg(myId, configId)
            Else
                Call CopyConfigDetailsSg(myId, configId)
                Call CopyConfigPowdersSg(myId, configId)
            End If
            Call RefreshConfigData()
        End If
    End Sub
    ''' <summary>
    ''' Handles the Click event of the EditToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub EditToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditToolStripMenuItem.Click
        Dim frmNew As New frmEditConfig
        Dim configId As Long = lstConfigSheets.SelectedValue
        Dim configName As String
        Dim objG As New GlobalFunctions
        configName = objG.GetTitle(configId)
        frmNew.ConfigID = configId
        frmNew.ConfigName = configName
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the UseConfigurationToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub UseConfigurationToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles UseConfigurationToolStripMenuItem.Click
        Dim frmNew As New FrmAddDataSheetRiflePistolsCfg
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the RiflePistolToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RiflePistolToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RiflePistolToolStripMenuItem1.Click
        Cursor = Cursors.WaitCursor
        Dim frmnew As New frmViewDataSheet_RiflePistols
        frmnew.MdiParent = Me
        frmnew.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the AmmunitionInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub AmmunitionInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AmmunitionInventoryToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmReport_Loaded_Ammunition.MdiParent = Me
        frmReport_Loaded_Ammunition.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the EquipmentListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub EquipmentListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EquipmentListToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmReport_List_Equipment.MdiParent = Me
        frmReport_List_Equipment.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the FirearmInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FirearmInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles FirearmInventoryToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmReport_List_Firearms.MdiParent = Me
        frmReport_List_Firearms.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PowderInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PowderInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PowderInventoryToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmReport_PowderInventory.MdiParent = Me
        frmReport_PowderInventory.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PrimerInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PrimerInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PrimerInventoryToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmReport_PrimerInventory.MdiParent = Me
        frmReport_PrimerInventory.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the BulletInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BulletInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BulletInventoryToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmReport_BulletInventory.MdiParent = Me
        frmReport_BulletInventory.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CaseBrassInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CaseBrassInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CaseBrassInventoryToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmReport_CaseInventory.MdiParent = Me
        frmReport_CaseInventory.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CaliberReloadToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CaliberReloadToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CaliberReloadToolStripMenuItem.Click
        frmAddCaliberToCollection.MdiParent = Me
        frmAddCaliberToCollection.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PreLoadedCaliberListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PreLoadedCaliberListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PreLoadedCaliberListToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmView_General_Calibers.MdiParent = Me
        frmView_General_Calibers.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PrimerTypeToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PrimerTypeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PrimerTypeToolStripMenuItem.Click
        frmEdit_PrimerTypes.MdiParent = Me
        frmEdit_PrimerTypes.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the AmmunitionTypesToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub AmmunitionTypesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AmmunitionTypesToolStripMenuItem.Click
        frmEdit_AmmunitionTypes.MdiParent = Me
        frmEdit_AmmunitionTypes.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ManuallyToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ManuallyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ManuallyToolStripMenuItem.Click
        FrmAddDataSheetRiflePistolsMan.MdiParent = Me
        FrmAddDataSheetRiflePistolsMan.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the SuggestedUsesToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub SuggestedUsesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SuggestedUsesToolStripMenuItem.Click
        frmEdit_SuggestedUse.MdiParent = Me
        frmEdit_SuggestedUse.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ShotgunToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ShotgunToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShotgunToolStripMenuItem1.Click
        Cursor = Cursors.WaitCursor
        Dim frmNew As New frmViewDataSheet_Shotgun
        frmNew.MdiParent = Me
        frmNew.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the WADToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub WADToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WADToolStripMenuItem.Click
        Dim frmNew As New frmAddWad
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ShellToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ShellToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShellToolStripMenuItem.Click
        Dim frmnew As New frmAddShell
        frmnew.MdiParent = Me
        frmnew.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ShotgunGaugesToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ShotgunGaugesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShotgunGaugesToolStripMenuItem.Click
        frmEditGuages.MdiParent = Me
        frmEditGuages.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ShotWeightToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ShotWeightToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShotWeightToolStripMenuItem.Click
        frmEdit_ShotWeight.MdiParent = Me
        frmEdit_ShotWeight.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ShellListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ShellListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShellListToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmView_List_ShellHulls.MdiParent = Me
        frmView_List_ShellHulls.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the WADListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub WADListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WADListToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmView_List_WADS.MdiParent = Me
        frmView_List_WADS.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ShellInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ShellInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShellInventoryToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmReport_HullInventory.MdiParent = Me
        frmReport_HullInventory.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the WADInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub WADInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WADInventoryToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmReport_WADInventory.MdiParent = Me
        frmReport_WADInventory.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the RifleAndPistolsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RifleAndPistolsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RifleAndPistolsToolStripMenuItem.Click
        frmSearchConfig_RiflePistol.MdiParent = Me
        frmSearchConfig_RiflePistol.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the DeleteCaliberToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub DeleteCaliberToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteCaliberToolStripMenuItem.Click
        Try
            Dim lngCalId As Long = lstCal.SelectedValue
            Dim obj As New BSDatabase
            Dim objG As New GlobalFunctions
            Dim strSqlTable As String = "List_Calibers"
            Dim strName As String = objG.GetName("SELECT * from " & strSqlTable & " where ID=" & lngCalId, "Cal")
            Dim cOnfigCount As Long = objG.TotalConfigByCal(lngCalId)
            Dim strAns As String = ""
            Dim sql As String = ""
            If cOnfigCount = 0 Then
                strAns = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            Else
                strAns = MsgBox("Are you sure you want to delete " & strName & " and the " & cOnfigCount & " configurations with it?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            End If
            If strAns = vbYes Then
                If cOnfigCount = 0 Then
                    Cursor = Cursors.WaitCursor
                    sql = "DELETE from " & strSqlTable & " where ID=" & lngCalId
                    obj.ConnExec(sql)
                    Cursor = Cursors.Arrow
                Else
                    sql = "Select ID,IsShotGun from qry_ConfigCal_NSG where CalID=" & lngCalId
                    If objG.IsShotGunCOnfig(lngCalId) Then sql = "Select ID,IsShotGun from qry_ConfigCal_SG where CalID=" & lngCalId
                    obj.ConnectDB()
                    Dim cmd As New OdbcCommand(sql, obj.Conn)
                    Dim rs As OdbcDataReader
                    rs = cmd.ExecuteReader
                    Dim configId As Long = 0
                    Cursor = Cursors.WaitCursor
                    While rs.Read
                        configId = rs("CLNID")
                        If rs("IsShotGun") = 0 Then
                            sql = "DELETE from Loaders_Log_Ammunition_Audit where CFID=" & configId
                            obj.ConnExec(sql)
                            sql = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & configId
                            obj.ConnExec(sql)
                            sql = "DELETE from Config_List_Data_NSG where CLNID=" & configId
                            obj.ConnExec(sql)
                            sql = "DELETE from Config_List_Name where ID=" & configId
                            obj.ConnExec(sql)
                        Else
                            sql = "DELETE from Loaders_Log_Ammunition_Audit where CFID=" & configId
                            obj.ConnExec(sql)
                            sql = "DELETE from Config_List_Powder_Data_SG where CLNID=" & configId
                            obj.ConnExec(sql)
                            sql = "DELETE from Config_List_Data_SG where CLNID=" & configId
                            obj.ConnExec(sql)
                            sql = "DELETE from Config_List_Name where ID=" & configId
                            obj.ConnExec(sql)
                        End If
                    End While
                    rs.Close()
                    rs = Nothing
                    cmd = Nothing
                    sql = "DELETE from " & strSqlTable & " where ID=" & lngCalId
                    obj.ConnExec(sql)
                    Call RefreshCalData()
                    Call RefreshConfigData()
                    Cursor = Cursors.Arrow
                End If
                Call RefreshCalData()
            End If
        Catch ex As Exception
            Call LogError(Name, "DeleteCaliberToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            Cursor = Cursors.Arrow
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ExportFirearmsToMGCToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ExportFirearmsToMGCToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExportFirearmsToMGCToolStripMenuItem.Click
        frmExportFirearms.MdiParent = Me
        frmExportFirearms.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CleanUpDatabaseToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CleanUpDatabaseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CleanUpDatabaseToolStripMenuItem.Click
        frmDBCleanup.MdiParent = Me
        frmDBCleanup.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the LoadedAmmunitionToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub LoadedAmmunitionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoadedAmmunitionToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        frmView_Loaded_Ammunition.MdiParent = Me
        frmView_Loaded_Ammunition.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ReRunHotfixUpdatesToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ReRunHotfixUpdatesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ReRunHotfixUpdatesToolStripMenuItem.Click
        DoAutoBackup = False
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MY_HOTFIX_FILE
        myProcess.StartInfo.Arguments = "/redo"
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        myProcess.Start()
        Application.Exit()
    End Sub
#End Region
    ''' <summary>
    ''' Handles the Click event of the ToolStripButton6 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ToolStripButton6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton6.Click
        Cursor = Cursors.WaitCursor

        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ShotToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ShotToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShotToolStripMenuItem.Click
        frmAddShot.MdiParent = Me
        frmAddShot.FromView = False
        frmAddShot.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the SlugsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub SlugsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SlugsToolStripMenuItem.Click
        frmAddSlugs.MdiParent = Me
        frmAddSlugs.FromView = False
        frmAddSlugs.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ShotListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ShotListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShotListToolStripMenuItem.Click
        frmView_List_Shot.MdiParent = Me
        frmView_List_Shot.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the SlugListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub SlugListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SlugListToolStripMenuItem.Click
        frmView_List_Slug.MdiParent = Me
        frmView_List_Slug.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the SlugInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub SlugInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SlugInventoryToolStripMenuItem.Click
        frmReport_SlugInventory.MdiParent = Me
        frmReport_SlugInventory.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ShotInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ShotInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShotInventoryToolStripMenuItem.Click
        frmReport_ShotInventory.MdiParent = Me
        frmReport_ShotInventory.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ShotgunsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ShotgunsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShotgunsToolStripMenuItem.Click
        'TODO: Finish Search Option for Shotgun after you get the configs working
    End Sub
    ''' <summary>
    ''' Handles the SelectedIndexChanged event of the lstCal control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub lstCal_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstCal.SelectedIndexChanged

    End Sub
    ''' <summary>
    ''' Handles the SelectedIndexChanged event of the lstConfigSheets control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub lstConfigSheets_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstConfigSheets.SelectedIndexChanged

    End Sub
    ''' <summary>
    ''' Handles the Click event of the PowderToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PowderToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PowderToolStripMenuItem1.Click
        Dim frmNew As New FrmAddBushingPowder
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PowderToolStripMenuItem2 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PowderToolStripMenuItem2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PowderToolStripMenuItem2.Click
        Dim frmNew As New frmView_Bushings_Powder
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ShotToolStripMenuItem2 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ShotToolStripMenuItem2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShotToolStripMenuItem2.Click
        Dim frmNew As New frmView_Bushings_Shot
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the OpenToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Call DoRestore()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the OpenToolStripButton control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Call DoBackup()
    End Sub
    ''' <summary>
    ''' Creates new toolstripbutton_click.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub NewToolStripButton_Click(sender As Object, e As EventArgs) Handles NewToolStripButton.Click
        Call DoRestore()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the DeleteErrorLogToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub DeleteErrorLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteErrorLogToolStripMenuItem.Click
        If File.Exists(MyLogFile) Then
            File.Delete(MyLogFile)
            MsgBox("Error Log was Deleted!")
        Else
            MsgBox("Error Log does not exist!")
        End If
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ViewErrorLogToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ViewErrorLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewErrorLogToolStripMenuItem.Click
        If File.Exists(MyLogFile) Then
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = MyLogFile
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            myProcess.Start()
        Else
            MsgBox("Error Log does not exist!")
        End If
    End Sub
End Class
