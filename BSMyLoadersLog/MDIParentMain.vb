Imports System.Windows.Forms
Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Cyhper
Imports System.Data.Odbc
Imports BurnSoft.MsgBox
Public Class MDIParentMain
    Private m_ChildFormNumber As Integer
    Public DaysLeftToTry As String
    Const ISPROD As Boolean = True
#Region "Form Subs"
    Private Sub MDIParentMain_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            If DoAutoBackup Then
                Dim myProcess As New Process
                myProcess.StartInfo.FileName = Application.StartupPath & "\" & MY_BACKUP
                myProcess.StartInfo.Arguments = "/auto"
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                myProcess.Start()
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "Disposed", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub MDIParent2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LASTCONFIGEDVIEWED = 0
            MyLogFile = Application.StartupPath & "\err.log"
            Call CheckforHotFix()
            If LoginEnabled(UseMyPWD, UseMyUID, UseMyForgotWord, UseMyForgotPhrase) And Not IsLoggedIN Then
                frmLogin.Show()
                Me.Close()
            End If
            Dim Obj As New BSRegistry
            OwnerID = GetOwnerID()
            Call Obj.UpDateAppDetails()
            Call Obj.GetSettings(LastSucBackup, AlertOnBackUp, TrackHistoryDays, TrackHistory, DoAutoBackup, DoOriginalImage, UsePetLoads, cmbConfigSort.Text)
            If ISPROD Then Call DoRegistrationProcessForApp()
            If OwnerID = 0 Then
                Dim frmNew As New frmOptions
                frmNew.MdiParent = Me
                frmNew.Show()
            End If
            OwnerLoadName = Replace(GetLoadName(), "''", "'")
            If OwnerLoadName <> "My Loaders Log" Then Me.Text = OwnerLoadName & " Loaders Log"
            Call RefreshData()
            Call InitForm()
            Call InitLoaderType()
            Dim ObjFS As New BSFileSystem
            If ObjFS.FileExists(MY_HOTFIX_FILE) Then ReRunHotfixUpdatesToolStripMenuItem.Enabled = True
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub MDIParentMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Height > 0 Then
            lstCal.Height = TabControl1.Height - 90
            lstConfigSheets.Height = TabControl1.Height - 110
        End If
    End Sub
#End Region
#Region "Subs and Functions"
    Public Sub RefreshData()
        Call RefreshCalData()
        Call RefreshConfigData()
    End Sub
    Public Sub RefreshCalData()
        Try
            Me.List_CalibersTableAdapter.Fill(Me.MLLDataSet.List_Calibers)
        Catch ex As Exception
            Call LogError(Me.Name, "RefreshCalData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Public Sub RefreshConfigData()
        Try
            ConfigListNameBindingSource.ResetBindings(True)
            Dim SelectedView As String = cmbConfigSort.SelectedItem.ToString
            Select Case UCase(SelectedView)
                Case UCase("All")
                    Me.Config_List_NameTableAdapter.Fill(Me.MLLDataSet.Config_List_Name)
                Case UCase("Active Only")
                    Me.Config_List_NameTableAdapter.FillBy_Active(Me.MLLDataSet.Config_List_Name)
                Case UCase("Inactive Only")
                    Me.Config_List_NameTableAdapter.FillBy_Inactive(Me.MLLDataSet.Config_List_Name)
                Case UCase("All Favorites")
                    Me.Config_List_NameTableAdapter.FillBy_Fav(Me.MLLDataSet.Config_List_Name)
                Case UCase("Shotgun Loads")
                    Me.Config_List_NameTableAdapter.FillBy_Shotgun(Me.MLLDataSet.Config_List_Name)
                Case UCase("Rifle & Pistol Loads")
                    Me.Config_List_NameTableAdapter.FillBy_RiflePistol(Me.MLLDataSet.Config_List_Name)
                Case UCase("Personal Loads")
                    Me.Config_List_NameTableAdapter.FillBy_Personal(Me.MLLDataSet.Config_List_Name)
                Case UCase("Reffered Loads")
                    Me.Config_List_NameTableAdapter.FillBy_NonPersonal(Me.MLLDataSet.Config_List_Name)
                Case Else
                    Me.Config_List_NameTableAdapter.Fill(Me.MLLDataSet.Config_List_Name)
            End Select
            Me.lstConfigSheets.Refresh()
            If LASTCONFIGEDVIEWED > 0 Then Me.lstConfigSheets.SelectedValue = LASTCONFIGEDVIEWED
            Dim ObjR As New BSRegistry
            ObjR.SaveConfigSort(SelectedView)
        Catch ex As Exception
            Call LogError(Me.Name, "RefreshConfigData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
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
    Private Sub InitRegValues()
        Dim Objr As New BSRegistry
        Call Objr.UpDateAppDetails()
    End Sub
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
            Call LogError(Me.Name, "InitLoaderType", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub InitForm()
        Try
            Dim Obj As New BSMGC
            If Obj.MyGunCollectionIsInstalled Then
                tsslMGCEnabled.Enabled = True
                tsslMGCEnabled.Visible = True
                MGCPath = Obj.GetMGCPath
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
            Call LogError(Me.Name, "InitForm", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub DoBackup()
        Try
            DoAutoBackup = False
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = Application.StartupPath & "\" & MY_BACKUP
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            myProcess.Start()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "DoBackup", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub DoRestore()
        Try
            DoAutoBackup = False
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = Application.StartupPath & "\" & MY_RESTORE
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            myProcess.Start()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "DoRestore", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub CheckforHotFix()
        Dim Objf As New BSFileSystem
        If Objf.FileExists(Application.StartupPath & "\hotfix.ini") Then
            Dim myProcess As New Process
            Dim RunThiSApp As String = Application.StartupPath & "\" & MY_HOTFIX_FILE
            myProcess.StartInfo.FileName = RunThiSApp
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            DoAutoBackup = False
            myProcess.Start()
            Global.System.Windows.Forms.Application.Exit()
        End If
    End Sub
    Sub DoHelp()
        Try
            Help.ShowHelp(Me, MY_HELP_FILE)
        Catch ex As Exception
            Call LogError(Me.Name, "DoHelp", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub CheckForUpdates()
        Try
            DoAutoBackup = False
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = Application.StartupPath & "\" & MY_UPDATER
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            myProcess.Start()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "CheckForUpdates", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub DoRegistrationProcessForApp()
        Try
            Dim ObjReg As New RegistrationProcess
            Dim IsRegistered As Boolean = False
            Dim MyDaysLeft As String = ""
            Dim Has2Expired As Boolean = False
            Dim MyRegTo As String = ""
            Dim MyregPath As String = ObjReg.DefaultRegPath & "\Settings"
            Call ObjReg.StartRegistration(MyregPath, IsRegistered, MyDaysLeft, Has2Expired, MyRegTo)
            If Not IsRegistered Then
                If Not Has2Expired Then
                    Select Case MyDaysLeft
                        Case 0
                            DaysLeftToTry = "This is your Last Day to register!"
                            ToolStripStatusLabel.Text = "This is a shareware version that will expire today."
                        Case 1
                            DaysLeftToTry = "You have " & MyDaysLeft & " day left to register!"
                            ToolStripStatusLabel.Text = "This is a shareware version that will work for " & MyDaysLeft & " more day."
                        Case Else
                            DaysLeftToTry = "You have " & MyDaysLeft & " days left to register!"
                            ToolStripStatusLabel.Text = "This is a shareware version that will work for " & MyDaysLeft & " days."
                    End Select
                    ' DaysLeftToTry = "You have " & MyDaysLeft & " days left to register!"
                    ' ToolStripStatusLabel.Text = "This is a shareware version that will work for " & MyDaysLeft & " days."
                Else
                    BSRegistration.MainFormUnloaded = True
                    BSRegistration.StatusMessage = "This Application has Expired!"
                    BSRegistration.Show()
                    Me.Close()
                End If
            Else
                ToolStripStatusLabel.Text = "Registered To: " & MyRegTo
                RegisterToolStripMenuItem.Enabled = False
                RegisterToolStripMenuItem.Visible = False
                ToolStripSeparator4.Visible = False
                PurchaseToolStripMenuItem.Visible = False
            End If
            If Not Has2Expired Then Call CheckBackup()
        Catch ex As Exception
            Call LogError(Me.Name, "DoRegistrationProcessForApp", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub CheckBackup()
        Try
            Dim ObjR As New BSRegistry
            If Not AlertOnBackUp Then Exit Sub
            Dim MyLastDateDiff As Long = DateDiff(DateInterval.Day, CDate(LastSucBackup), DateTime.Now)
            Dim Obj As New MsgClass
            If MyLastDateDiff > TrackHistoryDays Then Obj.DoMessage("It has been " & MyLastDateDiff & " days since your last backup.", MgboxStyle.Inf_OK, MgBtnStyle.mb_Exclamantion, "Last Backup Notice", , True, "Backup Warning", False)
        Catch ex As Exception
            Call LogError(Me.Name, "CheckBackup", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub CopyConfigDetailsNSG(ByVal MyID As Long, ByVal ConfigID As Long)
        Try
            Dim SQL As String = "SELECT * from Config_List_Data_NSG where CLNID=" & ConfigID
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                SQL = "INSERT INTO Config_List_Data_NSG (CLNID,ATID,CALID,BID,PRID,CAID,Source) VALUES(" & _
                        MyID & "," & RS("ATID") & "," & RS("CALID") & "," & RS("BID") & "," & RS("PRID") & "," & RS("CAID") & ",'" & _
                        RS("Source") & "')"
                Obj.ConnExec(SQL)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "CopyConfigDetailsNSG", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub CopyConfigPowdersNSG(ByVal MyID As Long, ByVal ConfigID As Long)
        Try
            Dim SQL As String = "SELECT * from Config_List_Powder_Data_NSG where CLNID=" & ConfigID
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                SQL = "INSERT INTO Config_List_Powder_Data_NSG (CLNID,PID,Load_Min,Load_Mid,Load_Max," & _
                            "FPS_Min,FPS_MID,FPS_Max,CUPS_Min,CUPS_Mid,CUPS_Max,IsPref) VALUES(" & MyID & _
                            "," & RS("PID") & "," & RS("Load_Min") & "," & RS("Load_Mid") & "," & RS("Load_Max") & "," & _
                             RS("FPS_Min") & "," & RS("FPS_MID") & "," & RS("FPS_Max") & "," & RS("CUPS_Min") & "," & _
                              RS("CUPS_Mid") & "," & RS("CUPS_Max") & "," & RS("IsPref") & ")"
                Obj.ConnExec(SQL)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "CopyConfigPowdersNSG", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub CopyConfigDetailsSG(ByVal MyID As Long, ByVal ConfigID As Long)
        Try
            Dim SQL As String = "SELECT * from Config_List_Data_SG where CLNID=" & ConfigID
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                SQL = "INSERT INTO Config_List_Data_SG (CLNID,ATID,CALID,PRID,CAID,Source,SW,SS,Bushing,WAD,SCL,SW_t,GID,IsPersonal) VALUES(" & _
                        MyID & "," & RS("ATID") & "," & RS("CALID") & "," & RS("PRID") & "," & RS("CAID") & ",'" & _
                        RS("Source") & "'," & RS("SW") & "," & RS("SS") & "," & RS("Bushing") & "," & RS("WAD") & _
                        "," & RS("SCL") & ",'" & RS("SW_t") & "'," & RS("GID") & "," & RS("IsPersonal") & ")"
                Obj.ConnExec(SQL)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "CopyConfigDetailsSG", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub CopyConfigPowdersSG(ByVal MyID As Long, ByVal ConfigID As Long)
        Try
            Dim SQL As String = "SELECT * from Config_List_Powder_Data_SG where CLNID=" & ConfigID
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                SQL = "INSERT INTO Config_List_Powder_Data_SG (CLNID,PID,Load_Min,Load_Mid,Load_Max," & _
                            "FPS_Min,FPS_MID,FPS_Max,PSI_Min,PSI_Mid,PSI_Max,IsPref) VALUES(" & MyID & _
                            "," & RS("PID") & "," & RS("Load_Min") & "," & RS("Load_Mid") & "," & RS("Load_Max") & "," & _
                             RS("FPS_Min") & "," & RS("FPS_MID") & "," & RS("FPS_Max") & "," & RS("PSI_Min") & "," & _
                              RS("PSI_Mid") & "," & RS("PSI_Max") & "," & RS("IsPref") & ")"
                Obj.ConnExec(SQL)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "CopyConfigPowdersSG", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripButton.Click, NewWindowToolStripMenuItem.Click
        Call DoRestore()
    End Sub
    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click, OpenToolStripButton.Click
        Call DoBackup()
    End Sub
#End Region
#Region "Tool Bar And Misc. Components Subs"
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        frmImportFirearms.MdiParent = Me
        frmImportFirearms.Show()
    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        frmOptions.MdiParent = Me
        frmOptions.Show()
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Call CheckForUpdates()
    End Sub
    Private Sub HelpToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripButton.Click
        Call DoHelp()
    End Sub
    Private Sub btnAddCal2List_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCal2List.Click
        frmAddCaliberToCollection.MdiParent = Me
        frmAddCaliberToCollection.Show()
    End Sub
    Private Sub btnAddConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddConfig.Click
        frmConfig_Add_Wizard.MdiParent = Me
        frmConfig_Add_Wizard.Show()
    End Sub
    Sub ViewConfigs()
        Dim ObjGS As New GlobalFunctions
        Dim lngConfigID As Long = lstConfigSheets.SelectedValue
        Dim ConfigType As Boolean = ObjGS.IsShotGunCOnfig(lngConfigID)

        If Not ConfigType Then
            Dim frmNew As New frmView_Configuration_Sheet
            frmNew.ConfigID = lngConfigID
            frmNew.MdiParent = Me
            frmNew.Show()
        Else
            Dim frmNew As New frmView_Configuration_Shotgun_Sheet
            frmNew.ConfigID = lngConfigID
            frmNew.MdiParent = Me
            frmNew.Show()
        End If
        LASTCONFIGEDVIEWED = lngConfigID
        lstConfigSheets.SelectedItem = LASTCONFIGEDVIEWED
    End Sub
    Private Sub lstConfigSheets_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstConfigSheets.DoubleClick
        Call ViewConfigs()
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_List_Firearms.MdiParent = Me
        frmView_List_Firearms.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub lstCal_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstCal.DoubleClick
        Dim ObjGF As New GlobalFunctions
        Dim lngCalID As Long = lstCal.SelectedValue
        Dim IsNSG As Boolean = ObjGF.IsNotInShotgunConfigbyCal(lngCalID)
        If IsNSG Then
            Dim frmNew As New frmView_List_ConfigurationsByCal
            frmNew.CALID = lngCalID
            frmNew.MdiParent = Me
            frmNew.Show()
        Else
            Dim frmNewS As New frmView_List_ConfigurationsByCal_SG
            frmNewS.CALID = lngCalID
            frmNewS.MdiParent = Me
            frmNewS.Show()
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lngConfigID As Long = lstConfigSheets.SelectedValue
        Dim Obj As New BSDatabase
        Dim ObjG As New GlobalFunctions
        Dim strName As String = ObjG.GetName("SELECT * from Config_List_Name where ID=" & lngConfigID, "ConfigName")
        Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
        Dim SQL As String = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & lngConfigID
        If strAns = vbYes Then
            Obj.ConnExec(SQL)
            SQL = "DELETE from Config_List_Data_NSG where CLNID=" & lngConfigID
            Obj.ConnExec(SQL)
            SQL = "DELETE from Loaders_Log_Ammunition_Audit where CFID=" & lngConfigID
            Obj.ConnExec(SQL)
            SQL = "DELETE from Config_List_Name where ID=" & lngConfigID
            Obj.ConnExec(SQL)
            Call RefreshConfigData()
        End If
    End Sub
    Private Sub tsslErrorsFound_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsslErrorsFound.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MyLogFile
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        myProcess.Start()
    End Sub
    Private Sub tsslMGCEnabled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsslMGCEnabled.Click
        Dim Obj As New BSMGC
        Dim strPath As String = Obj.GetMGCEXEPath
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = strPath
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        myProcess.Start()
    End Sub
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_Loaded_Ammunition.MdiParent = Me
        frmView_Loaded_Ammunition.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub btnImportConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportConfig.Click
        frmImportConfiguration.MdiParent = Me
        frmImportConfiguration.Show()
    End Sub
    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        Me.Cursor = Cursors.WaitCursor
        frmSearchConfig_RiflePistol.MdiParent = Me
        frmSearchConfig_RiflePistol.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub cmbConfigSort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbConfigSort.SelectedIndexChanged
        Call RefreshConfigData()
    End Sub
#End Region
#Region "Menu Subs"
    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        frmImportFirearms.MdiParent = Me
        frmImportFirearms.Show()
    End Sub
    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub
    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        'Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub
    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub
    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub
    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub
    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub
    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub
    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.MdiParent = Me
        AboutBox1.Show()
    End Sub
    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.Click
        frmOptions.MdiParent = Me
        frmOptions.Show()
    End Sub
    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Call DoBackup()
    End Sub
    Private Sub RegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegisterToolStripMenuItem.Click
        BSRegistration.StatusMessage = ToolStripStatusLabel.Text
        BSRegistration.Show()
    End Sub
    Private Sub PurchaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseToolStripMenuItem.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MENU_SHOP
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        myProcess.Start()
    End Sub
    Private Sub TechnicalSupportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TechnicalSupportToolStripMenuItem.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MENU_SUPPORT
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        myProcess.Start()
    End Sub
    Private Sub ReportABugToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportABugToolStripMenuItem.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MENU_BUG
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        myProcess.Start()
    End Sub
    Private Sub KnowledgeBaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KnowledgeBaseToolStripMenuItem.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MENU_WIKI
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        myProcess.Start()
    End Sub
    Private Sub SupportForumToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupportForumToolStripMenuItem.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MENU_FORUM
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        myProcess.Start()
    End Sub
    Private Sub SearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchToolStripMenuItem.Click
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MENU_SITESEARCH
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        myProcess.Start()
    End Sub
    Private Sub IndexToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IndexToolStripMenuItem.Click
        Help.ShowHelpIndex(Me, MY_HELP_FILE)
    End Sub
    Private Sub ContentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContentsToolStripMenuItem.Click
        Call DoHelp()
    End Sub
    Private Sub CheckForUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click
        Call CheckForUpdates()
    End Sub
    Private Sub PowderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PowderToolStripMenuItem.Click
        frmAddPowder.MdiParent = Me
        frmAddPowder.Show()
    End Sub
    Private Sub PrimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrimerToolStripMenuItem.Click
        frmAddPrimer.MdiParent = Me
        frmAddPrimer.Show()
    End Sub
    Private Sub BulletToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BulletToolStripMenuItem.Click
        frmAddBullets.MdiParent = Me
        frmAddBullets.Show()
    End Sub
    Private Sub CaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CaseToolStripMenuItem.Click
        frmAddShells.MdiParent = Me
        frmAddShells.Show()
    End Sub
    Private Sub MyFirearmCollectionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyFirearmCollectionsToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_List_Firearms.MdiParent = Me
        frmView_List_Firearms.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub EquipmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EquipmentToolStripMenuItem.Click
        frmAddEquipment.MdiParent = Me
        frmAddEquipment.Show()
    End Sub
    Private Sub AddFirearmToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddFirearmToolStripMenuItem.Click
        frmAddFirearm.MdiParent = Me
        frmAddFirearm.Show()
    End Sub
    Private Sub EquipmentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EquipmentToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_List_Equipment.MdiParent = Me
        frmView_List_Equipment.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub CaseToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CaseToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_List_Shells.MdiParent = Me
        frmView_List_Shells.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub PrimerListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrimerListToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_List_Primer.MdiParent = Me
        frmView_List_Primer.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub PowderListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PowderListToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_List_Powder.MdiParent = Me
        frmView_List_Powder.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub BulletToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BulletToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_List_Bullets.MdiParent = Me
        frmView_List_Bullets.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub MakeReadyToUseAmmunitionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeReadyToUseAmmunitionToolStripMenuItem.Click
        Dim ConfigID As Long = lstConfigSheets.SelectedValue
        Dim ConfigName As String
        Dim frmNew As New frmLoadMakeReady_Details
        Dim ObjG As New GlobalFunctions
        ConfigName = ObjG.GetTitle(ConfigID)
        frmNew.ConfigID = ConfigID
        frmNew.ConfigName = ConfigName
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim lngConfigID As Long = lstConfigSheets.SelectedValue
        Dim Obj As New BSDatabase
        Dim ObjG As New GlobalFunctions
        Dim strName As String = ObjG.GetName("SELECT * from Config_List_Name where ID=" & lngConfigID, "ConfigName")
        Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
        Dim IsShotGun As Boolean = ObjG.IsShotGunCOnfig(lngConfigID)
        Dim SQL As String = ""
        If strAns = vbYes Then
            If Not IsShotGun Then
                SQL = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & lngConfigID
                Obj.ConnExec(SQL)
                SQL = "DELETE from Config_List_Data_NSG where CLNID=" & lngConfigID
                Obj.ConnExec(SQL)
                SQL = "DELETE from Config_List_Name where ID=" & lngConfigID
                Obj.ConnExec(SQL)
            Else
                SQL = "DELETE from Config_List_Powder_Data_SG where CLNID=" & lngConfigID
                Obj.ConnExec(SQL)
                SQL = "DELETE from Config_List_Data_SG where CLNID=" & lngConfigID
                Obj.ConnExec(SQL)
                SQL = "DELETE from Config_List_Name where ID=" & lngConfigID
                Obj.ConnExec(SQL)
            End If
            Call RefreshConfigData()
        End If
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Dim ConfigID As Long = lstConfigSheets.SelectedValue
        Dim ConfigName As String
        Dim ObjG As New GlobalFunctions
        ConfigName = ObjG.GetTitle(ConfigID)
        Dim sMsg As String = "Renaming " & ConfigName & " to:"
        Dim strNewName As String = Trim(FluffContent(InputBox(sMsg, "Rename Configuration Name", ConfigName)))
        If Len(strNewName) <> 0 And LCase(strNewName) <> LCase(ConfigName) Then
            Dim SQL As String = "UPDATE Config_List_Name set ConfigName='" & strNewName & "' where id=" & ConfigID
            Dim Obj As New BSDatabase
            Obj.ConnExec(SQL)
            Call RefreshConfigData()
        End If
    End Sub
    Private Sub ViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Call ViewConfigs()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Dim ConfigID As Long = lstConfigSheets.SelectedValue
        Dim ConfigName As String
        Dim ObjG As New GlobalFunctions
        ConfigName = ObjG.GetTitle(ConfigID)
        Dim IsShotGun As Boolean = False
        Dim sMsg As String = "What do you wish to call this new configuration?"
        Dim strNewName As String = Trim(FluffContent(InputBox(sMsg, "Copy Configuration", ConfigName)))
        If Len(strNewName) <> 0 And LCase(strNewName) <> LCase(ConfigName) Then
            Dim SQL As String = "SELECT * from Config_List_Name where ID=" & ConfigID
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read()
                If RS("IsShotGun") = 1 Then IsShotGun = True
                Dim strNotes As String = " "
                If Not IsDBNull(RS("notes")) Then strNotes = FluffContent(RS("Notes"))
                SQL = "INSERT INTO Config_List_Name(ConfigName,IsPersonal,IsShotGun,Notes,IsActive,IsFav) VALUES('" & _
                        strNewName & "'," & RS("IsPersonal") & "," & RS("IsShotGun") & ",'" & _
                        strNotes & "'," & RS("IsActive") & "," & RS("IsFav") & ")"
                Obj.ConnExec(SQL)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Dim MyID As Long = ObjG.GetID("SELECT * from Config_list_Name where ConfigName='" & strNewName & "'")
            If Not IsShotGun Then
                Call CopyConfigDetailsNSG(MyID, ConfigID)
                Call CopyConfigPowdersNSG(MyID, ConfigID)
            Else
                Call CopyConfigDetailsSG(MyID, ConfigID)
                Call CopyConfigPowdersSG(MyID, ConfigID)
            End If
            Call RefreshConfigData()
        End If
    End Sub
    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Dim frmNew As New frmEditConfig
        Dim ConfigID As Long = lstConfigSheets.SelectedValue
        Dim ConfigName As String
        Dim ObjG As New GlobalFunctions
        ConfigName = ObjG.GetTitle(ConfigID)
        frmNew.ConfigID = ConfigID
        frmNew.ConfigName = ConfigName
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
    Private Sub UseConfigurationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseConfigurationToolStripMenuItem.Click
        Dim frmNew As New frmAddDataSheet_RiflePistols_CFG
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
    Private Sub RiflePistolToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RiflePistolToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        Dim frmnew As New frmViewDataSheet_RiflePistols
        frmnew.MdiParent = Me
        frmnew.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub AmmunitionInventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AmmunitionInventoryToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_Loaded_Ammunition.MdiParent = Me
        frmReport_Loaded_Ammunition.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub EquipmentListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EquipmentListToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_List_Equipment.MdiParent = Me
        frmReport_List_Equipment.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub FirearmInventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FirearmInventoryToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_List_Firearms.MdiParent = Me
        frmReport_List_Firearms.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub PowderInventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PowderInventoryToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_PowderInventory.MdiParent = Me
        frmReport_PowderInventory.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub PrimerInventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrimerInventoryToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_PrimerInventory.MdiParent = Me
        frmReport_PrimerInventory.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub BulletInventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BulletInventoryToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_BulletInventory.MdiParent = Me
        frmReport_BulletInventory.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub CaseBrassInventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CaseBrassInventoryToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_CaseInventory.MdiParent = Me
        frmReport_CaseInventory.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub CaliberReloadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CaliberReloadToolStripMenuItem.Click
        frmAddCaliberToCollection.MdiParent = Me
        frmAddCaliberToCollection.Show()
    End Sub
    Private Sub PreLoadedCaliberListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreLoadedCaliberListToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_General_Calibers.MdiParent = Me
        frmView_General_Calibers.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub PrimerTypeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrimerTypeToolStripMenuItem.Click
        frmEdit_PrimerTypes.MdiParent = Me
        frmEdit_PrimerTypes.Show()
    End Sub
    Private Sub AmmunitionTypesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AmmunitionTypesToolStripMenuItem.Click
        frmEdit_AmmunitionTypes.MdiParent = Me
        frmEdit_AmmunitionTypes.Show()
    End Sub
    Private Sub ManuallyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManuallyToolStripMenuItem.Click
        frmAddDataSheet_RiflePistols_MAN.MdiParent = Me
        frmAddDataSheet_RiflePistols_MAN.Show()
    End Sub
    Private Sub SuggestedUsesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SuggestedUsesToolStripMenuItem.Click
        frmEdit_SuggestedUse.MdiParent = Me
        frmEdit_SuggestedUse.Show()
    End Sub
    Private Sub ShotgunToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShotgunToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        Dim frmNew As New frmViewDataSheet_Shotgun
        frmNew.MdiParent = Me
        frmNew.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub WADToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WADToolStripMenuItem.Click
        Dim frmNew As New frmAddWad
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
    Private Sub ShellToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShellToolStripMenuItem.Click
        Dim frmnew As New frmAddShell
        frmnew.MdiParent = Me
        frmnew.Show()
    End Sub
    Private Sub ShotgunGaugesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShotgunGaugesToolStripMenuItem.Click
        frmEditGuages.MdiParent = Me
        frmEditGuages.Show()
    End Sub
    Private Sub ShotWeightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShotWeightToolStripMenuItem.Click
        frmEdit_ShotWeight.MdiParent = Me
        frmEdit_ShotWeight.Show()
    End Sub
    Private Sub ShellListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShellListToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_List_ShellHulls.MdiParent = Me
        frmView_List_ShellHulls.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub WADListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WADListToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_List_WADS.MdiParent = Me
        frmView_List_WADS.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub ShellInventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShellInventoryToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_HullInventory.MdiParent = Me
        frmReport_HullInventory.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub WADInventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WADInventoryToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmReport_WADInventory.MdiParent = Me
        frmReport_WADInventory.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub RifleAndPistolsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RifleAndPistolsToolStripMenuItem.Click
        frmSearchConfig_RiflePistol.MdiParent = Me
        frmSearchConfig_RiflePistol.Show()
    End Sub
    Private Sub DeleteCaliberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteCaliberToolStripMenuItem.Click
        Try
            Dim lngCalID As Long = lstCal.SelectedValue
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strSQLTable As String = "List_Calibers"
            Dim strName As String = ObjG.GetName("SELECT * from " & strSQLTable & " where ID=" & lngCalID, "Cal")
            Dim COnfigCount As Long = ObjG.TotalConfigByCal(lngCalID)
            Dim strAns As String = ""
            Dim SQL As String = ""
            If COnfigCount = 0 Then
                strAns = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            Else
                strAns = MsgBox("Are you sure you want to delete " & strName & " and the " & COnfigCount & " configurations with it?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            End If
            If strAns = vbYes Then
                If COnfigCount = 0 Then
                    Me.Cursor = Cursors.WaitCursor
                    SQL = "DELETE from " & strSQLTable & " where ID=" & lngCalID
                    Obj.ConnExec(SQL)
                    Me.Cursor = Cursors.Arrow
                Else
                    SQL = "Select ID,IsShotGun from qry_ConfigCal_NSG where CalID=" & lngCalID
                    If ObjG.IsShotGunCOnfig(lngCalID) Then SQL = "Select ID,IsShotGun from qry_ConfigCal_SG where CalID=" & lngCalID
                    Obj.ConnectDB()
                    Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                    Dim RS As OdbcDataReader
                    RS = CMD.ExecuteReader
                    Dim ConfigID As Long = 0
                    Me.Cursor = Cursors.WaitCursor
                    While RS.Read
                        ConfigID = RS("CLNID")
                        If RS("IsShotGun") = 0 Then
                            SQL = "DELETE from Loaders_Log_Ammunition_Audit where CFID=" & ConfigID
                            Obj.ConnExec(SQL)
                            SQL = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & ConfigID
                            Obj.ConnExec(SQL)
                            SQL = "DELETE from Config_List_Data_NSG where CLNID=" & ConfigID
                            Obj.ConnExec(SQL)
                            SQL = "DELETE from Config_List_Name where ID=" & ConfigID
                            Obj.ConnExec(SQL)
                        Else
                            SQL = "DELETE from Loaders_Log_Ammunition_Audit where CFID=" & ConfigID
                            Obj.ConnExec(SQL)
                            SQL = "DELETE from Config_List_Powder_Data_SG where CLNID=" & ConfigID
                            Obj.ConnExec(SQL)
                            SQL = "DELETE from Config_List_Data_SG where CLNID=" & ConfigID
                            Obj.ConnExec(SQL)
                            SQL = "DELETE from Config_List_Name where ID=" & ConfigID
                            Obj.ConnExec(SQL)
                        End If
                    End While
                    RS.Close()
                    RS = Nothing
                    CMD = Nothing
                    SQL = "DELETE from " & strSQLTable & " where ID=" & lngCalID
                    Obj.ConnExec(SQL)
                    Call RefreshCalData()
                    Call RefreshConfigData()
                    Me.Cursor = Cursors.Arrow
                End If
                Call RefreshCalData()
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "DeleteCaliberToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub
    Private Sub ExportFirearmsToMGCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportFirearmsToMGCToolStripMenuItem.Click
        frmExportFirearms.MdiParent = Me
        frmExportFirearms.Show()
    End Sub
    Private Sub CleanUpDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CleanUpDatabaseToolStripMenuItem.Click
        frmDBCleanup.MdiParent = Me
        frmDBCleanup.Show()
    End Sub
    Private Sub LoadedAmmunitionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadedAmmunitionToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        frmView_Loaded_Ammunition.MdiParent = Me
        frmView_Loaded_Ammunition.Show()
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub ReRunHotfixUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReRunHotfixUpdatesToolStripMenuItem.Click
        DoAutoBackup = False
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = MY_HOTFIX_FILE
        myProcess.StartInfo.Arguments = "/redo"
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        myProcess.Start()
        Global.System.Windows.Forms.Application.Exit()
    End Sub
#End Region

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        Me.Cursor = Cursors.WaitCursor

        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub ShotToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShotToolStripMenuItem.Click
        frmAddShot.MdiParent = Me
        frmAddShot.FromView = False
        frmAddShot.Show()
    End Sub
    Private Sub SlugsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SlugsToolStripMenuItem.Click
        frmAddSlugs.MdiParent = Me
        frmAddSlugs.FromView = False
        frmAddSlugs.Show()
    End Sub
    Private Sub ShotListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShotListToolStripMenuItem.Click
        frmView_List_Shot.MdiParent = Me
        frmView_List_Shot.Show()
    End Sub
    Private Sub SlugListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SlugListToolStripMenuItem.Click
        frmView_List_Slug.MdiParent = Me
        frmView_List_Slug.Show()
    End Sub
    Private Sub SlugInventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SlugInventoryToolStripMenuItem.Click
        frmReport_SlugInventory.MdiParent = Me
        frmReport_SlugInventory.Show()
    End Sub
    Private Sub ShotInventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShotInventoryToolStripMenuItem.Click
        frmReport_ShotInventory.MdiParent = Me
        frmReport_ShotInventory.Show()
    End Sub
    Private Sub ShotgunsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShotgunsToolStripMenuItem.Click
        'TODO: Finish Search Option for Shotgun after you get the configs working
    End Sub

    Private Sub lstCal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCal.SelectedIndexChanged

    End Sub

    Private Sub lstConfigSheets_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstConfigSheets.SelectedIndexChanged

    End Sub

    Private Sub PowderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PowderToolStripMenuItem1.Click
        Dim frmNew As New frmAddBushing_Powder
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub

    Private Sub PowderToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PowderToolStripMenuItem2.Click
        Dim frmNew As New frmView_Bushings_Powder
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub

    Private Sub ShotToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShotToolStripMenuItem2.Click
        Dim frmNew As New frmView_Bushings_Shot
        frmNew.MdiParent = Me
        frmNew.Show()
    End Sub
End Class
