
'Imports BSMyLoadersLog.LoadersClass
'Imports System.Data.Odbc
'Imports System.Diagnostics.Eventing.Reader
Imports System.IO
Imports BurnSoft.MsgBox
Imports BSMyLoadersLog.Adding
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.PeopleAndPlaces
Imports BurnSoft.Applications.MLL.Types
Imports BSMyLoadersLog.Viewing
Imports BSMyLoadersLog.ViewReports
'Imports BurnSoft.Applications.MGC
Imports BurnSoft.Applications.MGC.LoadersLog
Imports BurnSoft.Applications.MLL.ConfigSheets
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Universal

''' <summary>
''' Class MdiParentMain.
''' Implements the <see cref="Form" />
''' </summary>
''' <seealso cref="Form" />
Public Class MdiParentMain
    ''' <summary>
    ''' The error out
    ''' </summary>
    Private _errOut as String
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
                myProcess.StartInfo.FileName = Application.StartupPath & "\" & GeneralSettings.MY_BACKUP
                myProcess.StartInfo.Arguments = "/auto"
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                myProcess.Start()
            End If
        Catch ex As Exception
            Call LogError(Name, "Disposed", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Checks the login.
    ''' </summary>
    ''' <exception cref="System.Exception"></exception>
    Private Sub CheckLogin()
        Dim loginInfo as List(Of LoginInformationOnly) = OwnerInformation.LoginEnabled(DatabasePath, _errOut)
        If _errOut.Length > 0 Then Throw New Exception(_errOut)
        Dim requiredLogin as Boolean = False
        For Each o As LoginInformationOnly In loginInfo
            requiredLogin = o.UseLock
            UseMyPwd = o.Password
            UseMyUid = o.UserName
            UseMyForgotWord = o.Forgot
            UseMyForgotPhrase = o.ForgetPhrase
        Next

        If requiredLogin And Not IsLoggedIn Then
            frmLogin.Show()
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Load event of the MDIParent2 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MDIParent2_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            Lastconfigedviewed = 0
            'MyLogFile = Application.StartupPath & "\err.log"
            Call CheckforHotFix()
            Call CheckLogin()
            'If LoginEnabled(UseMyPWD, UseMyUID, UseMyForgotWord, UseMyForgotPhrase) And Not IsLoggedIN Then
            '    frmLogin.Show()
            '    Close()
            'End If
            'Dim loginInfo as List(Of LoginInformationOnly) = OwnerInformation.LoginEnabled(DatabasePath, errOut)
            'If errOut.Length > 0 Then Throw New Exception(errOut)
            'Dim requiredLogin as Boolean = False
            'For Each o As LoginInformationOnly In loginInfo
            '    requiredLogin = o.UseLock
            '    UseMyPwd = o.Password
            '    UseMyUid = o.UserName
            '    UseMyForgotWord = o.Forgot
            '    UseMyForgotPhrase = o.ForgetPhrase
            'Next

            'If requiredLogin And Not IsLoggedIn Then
            '    frmLogin.Show()
            '    Close()
            'End If
            'Dim obj As New LoadersClass.BSRegistry
            'OwnerID = GetOwnerID()
            OwnerId = OwnerInformation.GetOwnerID(DatabasePath, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
           
            'Call obj.UpDateAppDetails()
            If Not MyRegistry.UpdateAppDetails(Application.ProductVersion, Application.ProductName, 
                                        Application.ExecutablePath(), ApplicationPath, 
                                        MyLogFile, DatabasePath, ApplicationPathData, 
                                               _errOut) Then Throw New Exception(_errOut)

            Dim regSettings As List(Of RegistrySettings) = MyRegistry.GetSettings(_errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As RegistrySettings In regSettings
                LastSucBackup = o.LastSucBackup
                AlertOnBackUp = o.AlertOnBackUp
                TrackHistoryDays = o.TrackHistoryDays
                TrackHistory = o.TrackHistory
                DoAutoBackup = o.AutoBackup
                DoOriginalImage = o.UseOrgImage
                UseIndividualReports = o.IndvReports
                cmbConfigSort.Text = o.ConfigSort
                LoaderTypeShotGun = o.LoaderTypeShotGun
                LoaderTypeMetalic = o.LoaderTypeMetalic
            Next
            
            'Call obj.GetSettings(LastSucBackup, AlertOnBackUp, TrackHistoryDays, TrackHistory, DoAutoBackup, DoOriginalImage,
            '                     UseIndividualReports, cmbConfigSort.Text)

            ToolStripStatusLabel.Text = ""
            ToolStripSeparator4.Visible = False

            If OwnerId = 0 Then
                Dim frmNew As New FrmOptions
                frmNew.MdiParent = Me
                frmNew.Show()
            End If
            'OwnerLoadName = Replace(GetLoadName(), "''", "'")
            OwnerLoadName = Replace(OwnerInformation.GetLoadName(DatabasePath, _errOut), "''", "'")
            If OwnerLoadName <> "My Loaders Log" Then Text = $"{OwnerLoadName} Loaders Log"
            Call RefreshData()
            Call InitForm()
            Call InitLoaderType()
            Dim objFs As New FileIO
            If objFs.FileExists(GeneralSettings.MY_HOTFIX_FILE) Then ReRunHotfixUpdatesToolStripMenuItem.Enabled = True
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
            If Lastconfigedviewed > 0 Then lstConfigSheets.SelectedValue = Lastconfigedviewed
            'Dim objR As New BSRegistry
            'objR.SaveConfigSort(selectedView)
            If Not MyRegistry.SaveConfigSort(selectedView, _errOut) Then Throw New Exception(_errOut)
        Catch ex As Exception
            Call LogError(Name, "RefreshConfigData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    '''' <summary>
    '''' Deinits the type of the loader.
    '''' </summary>
    'Private Sub DeinitLoaderType()
    '    BulletToolStripMenuItem.Visible = False
    '    CaseToolStripMenuItem.Visible = False
    '    WADToolStripMenuItem.Visible = False
    '    ShellToolStripMenuItem.Visible = False
    '    BulletToolStripMenuItem1.Visible = False
    '    CaseToolStripMenuItem1.Visible = False
    '    WADListToolStripMenuItem.Visible = False
    '    ShellListToolStripMenuItem.Visible = False
    '    ToolStripSeparator7.Visible = False
    '    ToolStripSeparator10.Visible = False
    '    ToolStripSeparator13.Visible = False
    '    ToolStripSeparator14.Visible = False
    '    ShotgunToolStripMenuItem.Visible = False
    '    ShotgunToolStripMenuItem1.Visible = False
    '    RiflePistolToolStripMenuItem.Visible = False
    '    RiflePistolToolStripMenuItem1.Visible = False
    '    WADInventoryToolStripMenuItem.Visible = False
    '    ShellInventoryToolStripMenuItem.Visible = False
    '    CaseBrassInventoryToolStripMenuItem.Visible = False
    '    BulletInventoryToolStripMenuItem.Visible = False
    '    ShotgunGaugesToolStripMenuItem.Visible = False
    '    ShotWeightToolStripMenuItem.Visible = False
    '    ShotgunsToolStripMenuItem.Visible = False
    '    RifleAndPistolsToolStripMenuItem.Visible = False
    '    ToolStripButton6.Visible = False
    '    ToolStripButton5.Visible = False
    '    SlugsToolStripMenuItem.Visible = False
    '    ShotToolStripMenuItem.Visible = False
    '    SlugListToolStripMenuItem.Visible = False
    '    ShotListToolStripMenuItem.Visible = False
    '    ShotInventoryToolStripMenuItem.Visible = False
    '    SlugInventoryToolStripMenuItem.Visible = False
    '    PowderBushingsToolStripMenuItem.Visible = False
    '    BushingsChargeBarToolStripMenuItem.Visible = False
    'End Sub
    '''' <summary>
    '''' Initializes the reg values.
    '''' </summary>
    'Private Sub InitRegValues()
    '    'Dim objr As New BSRegistry
    '    'Call objr.UpDateAppDetails()
    '    Try
    '        If Not MyRegistry.UpdateAppDetails(Application.ProductVersion, Application.ProductName, 
    '                                           Application.ExecutablePath(), ApplicationPath, 
    '                                           MyLogFile, DatabasePath, ApplicationPathData, 
    '                                           errOut) Then Throw New Exception(errOut)
    '    Catch ex As Exception
    '        Call LogError(Name, "InitRegValues", Err.Number, ex.Message.ToString)
    '    End Try
    'End Sub
    ''' <summary>
    ''' Toggles the shotgun views visible or hidden
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    Sub ToggleShotgunViews(status As Boolean)
        If LoaderTypeShotGun Then
            ToolStripSeparator7.Visible = status
            ToolStripSeparator14.Visible = status
            WADToolStripMenuItem.Visible = status
            ShellToolStripMenuItem.Visible = status
            WADListToolStripMenuItem.Visible = status
            ShellListToolStripMenuItem.Visible = status
            ShotgunToolStripMenuItem.Visible = status
            ShotgunToolStripMenuItem1.Visible = status
            WADInventoryToolStripMenuItem.Visible = status
            ShellInventoryToolStripMenuItem.Visible = status
            ShotgunGaugesToolStripMenuItem.Visible = status
            ShotWeightToolStripMenuItem.Visible = status
            ShotgunsToolStripMenuItem.Visible = status
            ToolStripButton6.Visible = status
            SlugsToolStripMenuItem.Visible = status
            ShotToolStripMenuItem.Visible = status
            SlugListToolStripMenuItem.Visible = status
            ShotListToolStripMenuItem.Visible = status
            ShotInventoryToolStripMenuItem.Visible = status
            SlugInventoryToolStripMenuItem.Visible = status
            PowderBushingsToolStripMenuItem.Visible = status
            BushingsChargeBarToolStripMenuItem.Visible = status
        End If
    End Sub
    ''' <summary>
    ''' Toggles the metalic views visible or hidden
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    Sub ToggleMetalicViews(status As Boolean)
        If LoaderTypeMetalic Then
            ToolStripSeparator13.Visible = status
            ToolStripSeparator10.Visible = status
            BulletToolStripMenuItem.Visible = status
            CaseToolStripMenuItem.Visible = status
            BulletToolStripMenuItem1.Visible = status
            CaseToolStripMenuItem1.Visible = status
            RiflePistolToolStripMenuItem.Visible = status
            RiflePistolToolStripMenuItem1.Visible = status
            CaseBrassInventoryToolStripMenuItem.Visible = status
            BulletInventoryToolStripMenuItem.Visible = status
            RifleAndPistolsToolStripMenuItem.Visible = status
            ToolStripButton5.Visible = status
        End If
    End Sub
    ''' <summary>
    ''' Initializes the type of the loader.
    ''' </summary>
    Public Sub InitLoaderType()
        Try
            Call ToggleShotgunViews(False)
            Call ToggleMetalicViews(False)
            Call ToggleShotgunViews(True)
            Call ToggleMetalicViews(True)
            'Call DeinitLoaderType()

            'If LoaderTypeShotGun Then
            '    ToolStripSeparator7.Visible = True
            '    ToolStripSeparator14.Visible = True
            '    WADToolStripMenuItem.Visible = True
            '    ShellToolStripMenuItem.Visible = True
            '    WADListToolStripMenuItem.Visible = True
            '    ShellListToolStripMenuItem.Visible = True
            '    ShotgunToolStripMenuItem.Visible = True
            '    ShotgunToolStripMenuItem1.Visible = True
            '    WADInventoryToolStripMenuItem.Visible = True
            '    ShellInventoryToolStripMenuItem.Visible = True
            '    ShotgunGaugesToolStripMenuItem.Visible = True
            '    ShotWeightToolStripMenuItem.Visible = True
            '    ShotgunsToolStripMenuItem.Visible = True
            '    ToolStripButton6.Visible = True
            '    SlugsToolStripMenuItem.Visible = True
            '    ShotToolStripMenuItem.Visible = True
            '    SlugListToolStripMenuItem.Visible = True
            '    ShotListToolStripMenuItem.Visible = True
            '    ShotInventoryToolStripMenuItem.Visible = True
            '    SlugInventoryToolStripMenuItem.Visible = True
            '    PowderBushingsToolStripMenuItem.Visible = True
            '    BushingsChargeBarToolStripMenuItem.Visible = True
            'End If
            'If LoaderTypeMetalic Then
            '    ToolStripSeparator13.Visible = True
            '    ToolStripSeparator10.Visible = True
            '    BulletToolStripMenuItem.Visible = True
            '    CaseToolStripMenuItem.Visible = True
            '    BulletToolStripMenuItem1.Visible = True
            '    CaseToolStripMenuItem1.Visible = True
            '    RiflePistolToolStripMenuItem.Visible = True
            '    RiflePistolToolStripMenuItem1.Visible = True
            '    CaseBrassInventoryToolStripMenuItem.Visible = True
            '    BulletInventoryToolStripMenuItem.Visible = True
            '    RifleAndPistolsToolStripMenuItem.Visible = True
            '    ToolStripButton5.Visible = True
            'End If
        Catch ex As Exception
            Call LogError(Name, "InitLoaderType", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Initializes the form.
    ''' </summary>
    Sub InitForm()
        Try
            'Dim obj As New BSMGC
            If RegistryHelpers.MyGunCollectionIsInstalled(_errOut) Then
                tsslMGCEnabled.Enabled = True
                tsslMGCEnabled.Visible = True
                MgcPath = RegistryHelpers.GetMgcExePath(_errOut)
                if _errOut.Length > 0 Then Throw new Exception(_errOut)
                SaveAsToolStripMenuItem.Enabled = True
                ToolStripButton1.Enabled = True
                ExportFirearmsToMGCToolStripMenuItem.Enabled = True
            Else
                SaveAsToolStripMenuItem.Enabled = False
                ToolStripButton1.Enabled = False
                ExportFirearmsToMGCToolStripMenuItem.Enabled = False
            End If
            Select Case Defaultlist
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
            myProcess.StartInfo.FileName = Application.StartupPath & "\" & GeneralSettings.MY_BACKUP
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
            myProcess.StartInfo.FileName = Application.StartupPath & "\" & GeneralSettings.MY_RESTORE
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
        Dim objf As New FileIO
        If objf.FileExists(Application.StartupPath & "\hotfix.ini") Then
            Dim myProcess As New Process
            Dim runThiSApp As String = Application.StartupPath & "\" & GeneralSettings.MY_HOTFIX_FILE
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
            Help.ShowHelp(Me, GeneralSettings.MY_HELP_FILE)
        Catch ex As Exception
            Call LogError(Name, "DoHelp", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Checks the backup.
    ''' </summary>
    Sub CheckBackup()
        Try
            ' TODO: See if this is needed or was replaced.
            'Dim objR As New LoadersClass.BSRegistry
            If Not AlertOnBackUp Then Exit Sub
            Dim myLastDateDiff As Long = DateDiff(DateInterval.Day, CDate(LastSucBackup), DateTime.Now)
            Dim obj As New MsgClass
            If myLastDateDiff > TrackHistoryDays Then obj.DoMessage($"It has been {myLastDateDiff} days since your last backup.",
                                                                    MgboxStyle.Inf_OK, MgBtnStyle.mb_Exclamantion,
                                                                    "Last Backup Notice", , True, "Backup Warning",
                                                                    False)
        Catch ex As Exception
            Call LogError(Name, "CheckBackup", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    '<Obsolete("Replaced by BurnSoft.Applications.MLL.ConfigSheets.ConfigListDataMetalic.CopyConfig")>
    'Private Sub CopyConfigDetailsNsg(ByVal myId As Long, ByVal configId As Long)
    '    Try
    '        Dim sql As String = "SELECT * from Config_List_Data_NSG where CLNID=" & configId
    '        Dim obj As New BSDatabase
    '        Call obj.ConnectDB()
    '        Dim cmd As New OdbcCommand(sql, obj.Conn)
    '        Dim rs As OdbcDataReader
    '        rs = cmd.ExecuteReader
    '        While rs.Read
    '            sql = "INSERT INTO Config_List_Data_NSG (CLNID,ATID,CALID,BID,PRID,CAID,Source) VALUES(" & _
    '                    myId & "," & rs("ATID") & "," & rs("CALID") & "," & rs("BID") & "," & rs("PRID") & "," & rs("CAID") & ",'" & _
    '                    rs("Source") & "')"
    '            obj.ConnExec(sql)
    '        End While
    '        rs.Close()

    '    Catch ex As Exception
    '        Call LogError(Name, "CopyConfigDetailsNSG", Err.Number, ex.Message.ToString)
    '    End Try
    'End Sub

    '<Obsolete("Replaced by BurnSoft.Applications.MLL.ConfigSheets.ConfigListDataPowders.CopyConfig")>
    'Private Sub CopyConfigPowdersNsg(ByVal myId As Long, ByVal configId As Long)
    '    Try
    '        Dim sql As String = "SELECT * from Config_List_Powder_Data_NSG where CLNID=" & configId
    '        Dim obj As New BSDatabase
    '        Call obj.ConnectDB()
    '        Dim cmd As New OdbcCommand(sql, obj.Conn)
    '        Dim rs As OdbcDataReader
    '        rs = cmd.ExecuteReader
    '        While rs.Read
    '            sql = "INSERT INTO Config_List_Powder_Data_NSG (CLNID,PID,Load_Min,Load_Mid,Load_Max," & _
    '                        "FPS_Min,FPS_MID,FPS_Max,CUPS_Min,CUPS_Mid,CUPS_Max,IsPref) VALUES(" & myId & _
    '                        "," & rs("PID") & "," & rs("Load_Min") & "," & rs("Load_Mid") & "," & rs("Load_Max") & "," & _
    '                         rs("FPS_Min") & "," & rs("FPS_MID") & "," & rs("FPS_Max") & "," & rs("CUPS_Min") & "," & _
    '                          rs("CUPS_Mid") & "," & rs("CUPS_Max") & "," & rs("IsPref") & ")"
    '            obj.ConnExec(sql)
    '        End While
    '        rs.Close()
    '    Catch ex As Exception
    '        Call LogError(Name, "CopyConfigPowdersNSG", Err.Number, ex.Message.ToString)
    '    End Try
    'End Sub

    '<Obsolete("Replaced by BurnSoft.Applications.MLL.ConfigSheets.ConfigListDataShotgun.CopyConfig")>
    'Private Sub CopyConfigDetailsSg(ByVal myId As Long, ByVal configId As Long)
    '    Try
    '        Dim sql As String = "SELECT * from Config_List_Data_SG where CLNID=" & configId
    '        Dim obj As New BSDatabase
    '        Call obj.ConnectDB()
    '        Dim cmd As New OdbcCommand(sql, obj.Conn)
    '        Dim rs As OdbcDataReader
    '        rs = cmd.ExecuteReader
    '        While rs.Read
    '            sql = "INSERT INTO Config_List_Data_SG (CLNID,ATID,CALID,PRID,CAID,Source,SW,SS,Bushing,WAD,SCL,SW_t,GID,IsPersonal) VALUES(" & _
    '                    myId & "," & rs("ATID") & "," & rs("CALID") & "," & rs("PRID") & "," & rs("CAID") & ",'" & _
    '                    rs("Source") & "'," & rs("SW") & "," & rs("SS") & "," & rs("Bushing") & "," & rs("WAD") & _
    '                    "," & rs("SCL") & ",'" & rs("SW_t") & "'," & rs("GID") & "," & rs("IsPersonal") & ")"
    '            obj.ConnExec(sql)
    '        End While
    '        rs.Close()
    '        rs = Nothing
    '        cmd = Nothing
    '    Catch ex As Exception
    '        Call LogError(Name, "CopyConfigDetailsSG", Err.Number, ex.Message.ToString)
    '    End Try
    'End Sub

    '<Obsolete("Replaced by BurnSoft.Applications.MLL.ConfigSheets.ConfigListDataPowdersShotgun.CopyConfig")>
    'Private Sub CopyConfigPowdersSg(ByVal myId As Long, ByVal configId As Long)
    '    Try
    '        Dim sql As String = "SELECT * from Config_List_Powder_Data_SG where CLNID=" & configId
    '        Dim obj As New BSDatabase
    '        Call obj.ConnectDB()
    '        Dim cmd As New OdbcCommand(sql, obj.Conn)
    '        Dim rs As OdbcDataReader
    '        rs = cmd.ExecuteReader
    '        While rs.Read
    '            sql = "INSERT INTO Config_List_Powder_Data_SG (CLNID,PID,Load_Min,Load_Mid,Load_Max," & _
    '                        "FPS_Min,FPS_MID,FPS_Max,PSI_Min,PSI_Mid,PSI_Max,IsPref) VALUES(" & myId & _
    '                        "," & rs("PID") & "," & rs("Load_Min") & "," & rs("Load_Mid") & "," & rs("Load_Max") & "," & _
    '                         rs("FPS_Min") & "," & rs("FPS_MID") & "," & rs("FPS_Max") & "," & rs("PSI_Min") & "," & _
    '                          rs("PSI_Mid") & "," & rs("PSI_Max") & "," & rs("IsPref") & ")"
    '            obj.ConnExec(sql)
    '        End While
    '        rs.Close()
    '        rs = Nothing
    '        cmd = Nothing
    '    Catch ex As Exception
    '        Call LogError(Name, "CopyConfigPowdersSG", Err.Number, ex.Message.ToString)
    '    End Try
    'End Sub
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
        FrmOptions.MdiParent = Me
        FrmOptions.Show()
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
        FrmAddCaliberToCollection.MdiParent = Me
        FrmAddCaliberToCollection.Show()
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
        'Dim objGs As New GlobalFunctions
        Dim lngConfigId As Long = lstConfigSheets.SelectedValue
        'Dim configType As Boolean = objGs.IsShotGunCOnfig(lngConfigId)
        Dim configType As Boolean = ConfigListGeneral.IsShotgunConfig(DatabasePath, lngConfigId, _errOut)
        if _errOut.Length > 0 Then Throw New Exception(_errOut)

        If Not configType Then
            Dim frmNew As New FrmViewConfigurationSheet
            frmNew.ConfigId = lngConfigId
            frmNew.MdiParent = Me
            frmNew.Show()
        Else
            Dim frmNew As New frmView_Configuration_Shotgun_Sheet
            frmNew.ConfigID = lngConfigId
            frmNew.MdiParent = Me
            frmNew.Show()
        End If
        Lastconfigedviewed = lngConfigId
        lstConfigSheets.SelectedItem = Lastconfigedviewed
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
        FrmViewListFirearms.MdiParent = Me
        FrmViewListFirearms.Show()
        Cursor = Cursors.Arrow
    End Sub
    ''' <summary>
    ''' Handles the DoubleClick event of the lstCal control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub lstCal_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstCal.DoubleClick
        Try
            'Dim objGf As New GlobalFunctions
            Dim lngCalId As Long = lstCal.SelectedValue
            'Dim isNsg As Boolean = objGf.IsNotInShotgunConfigbyCal(lngCalId)
            Dim isNsg As Boolean = ConfigListGeneral.IsNotInShotgunConfigByCaliber(DatabasePath, lngCalId, _errOut)
            If _errOut.Length > 0 Then Throw new Exception(_errOut)
            If isNsg Then
                Dim frmNew As New FrmViewListConfigurationsByCal
                frmNew.CaliberId = lngCalId
                frmNew.MdiParent = Me
                frmNew.Show()
            Else
                Dim frmNewS As New FrmViewListConfigurationsByCalSg
                frmNewS.CaliberId = lngCalId
                frmNewS.MdiParent = Me
                frmNewS.Show()
            End If
        Catch ex As Exception
            Call LogError(Name, "lstCal_DoubleClick", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnDelete control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Dim lngConfigId As Long = lstConfigSheets.SelectedValue
        'Dim obj As New BSDatabase
        'Dim objG As New GlobalFunctions
        'Dim strName As String = objG.GetName("SELECT * from Config_List_Name where ID=" & lngConfigId, "ConfigName")
        Dim strName As String = ConfigListDataName.GetName(DatabasePath, lngConfigId ,_errOut)
        If _errOut.Length > 0 then Throw New Exception(_errOut)
        Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
        'Dim sql As String = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & lngConfigId
        If strAns = vbYes Then
            if Not ConfigListDataName.Delete(DatabasePath, lngConfigId ,_errOut) then Throw New Exception(_errOut)
            'obj.ConnExec(sql)
            'sql = "DELETE from Config_List_Data_NSG where CLNID=" & lngConfigId
            'obj.ConnExec(sql)
            'sql = "DELETE from Loaders_Log_Ammunition_Audit where CFID=" & lngConfigId
            'obj.ConnExec(sql)
            'sql = "DELETE from Config_List_Name where ID=" & lngConfigId
            'obj.ConnExec(sql)
            Call RefreshConfigData()
        End If
    End Sub
    ''' <summary>
    ''' Handles the Click event of the tsslErrorsFound control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub tsslErrorsFound_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsslErrorsFound.Click
        Try
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = MyLogFile
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            myProcess.Start()
        Catch ex As Exception
            Call LogError(Name, "tsslErrorsFound_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the tsslMGCEnabled control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub tsslMGCEnabled_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsslMGCEnabled.Click
        Try
            'Dim obj As New BSMGC
            'Dim strPath As String = obj.GetMGCEXEPath
            Dim strPath As String = RegistryHelpers.GetMgcExePath(_errOut)
            if _errOut.Length > 0 then Throw new Exception(_errOut)
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = strPath
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            myProcess.Start()
        Catch ex As Exception
            Call LogError(Name, "tsslMGCEnabled_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ToolStripButton4 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ToolStripButton4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton4.Click
        Try
            Cursor = Cursors.WaitCursor
            frmView_Loaded_Ammunition.MdiParent = Me
            frmView_Loaded_Ammunition.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "tsslMGCEnabled_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnImportConfig control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnImportConfig_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImportConfig.Click
        Try
            frmImportConfiguration.MdiParent = Me
            frmImportConfiguration.Show()
        Catch ex As Exception
            Call LogError(Name, "btnImportConfig_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ToolStripButton5 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ToolStripButton5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton5.Click
        Try
            Cursor = Cursors.WaitCursor
            frmSearchConfig_RiflePistol.MdiParent = Me
            frmSearchConfig_RiflePistol.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "ToolStripButton5_Click", Err.Number, ex.Message.ToString)
        End Try
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
        Try
            FrmOptions.MdiParent = Me
            FrmOptions.Show()
        Catch ex As Exception
            Call LogError(Name, "TechnicalSupportToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the SaveToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub SaveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click
        Call DoBackup()
    End Sub
    '''' <summary>
    '''' Handles the Click event of the PurchaseToolStripMenuItem control.
    '''' </summary>
    '''' <param name="sender">The source of the event.</param>
    '''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    'Private Sub PurchaseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim myProcess As New Process
    '    myProcess.StartInfo.FileName = MenuShop
    '    myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
    '    myProcess.Start()
    'End Sub
    ''' <summary>
    ''' Handles the Click event of the TechnicalSupportToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub TechnicalSupportToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TechnicalSupportToolStripMenuItem.Click
        Try
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = GeneralSettings.MENU_SUPPORT
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
            myProcess.Start()
        Catch ex As Exception
            Call LogError(Name, "TechnicalSupportToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ReportABugToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ReportABugToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ReportABugToolStripMenuItem.Click
        Try
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = GeneralSettings.MENU_BUG
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
            myProcess.Start()
        Catch ex As Exception
            Call LogError(Name, "ReportABugToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the KnowledgeBaseToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub KnowledgeBaseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles KnowledgeBaseToolStripMenuItem.Click
        Try
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = GeneralSettings.MENU_WIKI
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
            myProcess.Start()
        Catch ex As Exception
            Call LogError(Name, "KnowledgeBaseToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the SearchToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub SearchToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchToolStripMenuItem.Click
        Try
            Dim myProcess As New Process
            myProcess.StartInfo.FileName = GeneralSettings.MENU_SITESEARCH
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
            myProcess.Start()
        Catch ex As Exception
            Call LogError(Name, "SearchToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the IndexToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndexToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles IndexToolStripMenuItem.Click
        Try
            Help.ShowHelpIndex(Me, GeneralSettings.MY_HELP_FILE)
        Catch ex As Exception
            Call LogError(Name, "IndexToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
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
        Try
            frmAddPowder.MdiParent = Me
            frmAddPowder.Show()
        Catch ex As Exception
            Call LogError(Name, "PowderToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PrimerToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PrimerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PrimerToolStripMenuItem.Click
        Try
            frmAddPrimer.MdiParent = Me
            frmAddPrimer.Show()
        Catch ex As Exception
            Call LogError(Name, "PrimerToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the BulletToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BulletToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BulletToolStripMenuItem.Click
        Try
            FrmAddBullets.MdiParent = Me
            FrmAddBullets.Show()
        Catch ex As Exception
            Call LogError(Name, "BulletToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CaseToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CaseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CaseToolStripMenuItem.Click
        Try
            frmAddShells.MdiParent = Me
            frmAddShells.Show()
        Catch ex As Exception
            Call LogError(Name, "CaseToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the MyFirearmCollectionsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MyFirearmCollectionsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MyFirearmCollectionsToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            FrmViewListFirearms.MdiParent = Me
            FrmViewListFirearms.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Call LogError(Name, "MyFirearmCollectionsToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the EquipmentToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub EquipmentToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EquipmentToolStripMenuItem.Click
        Try
            frmAddEquipment.MdiParent = Me
            frmAddEquipment.Show()
        Catch ex As Exception
            Call LogError(Name, "EquipmentToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the AddFirearmToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub AddFirearmToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddFirearmToolStripMenuItem.Click
        Try
            frmAddFirearm.MdiParent = Me
            frmAddFirearm.Show()
        Catch ex As Exception
            Call LogError(Name, "AddFirearmToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the EquipmentToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub EquipmentToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EquipmentToolStripMenuItem1.Click
        Try
            Cursor = Cursors.WaitCursor
            FrmViewListEquipment.MdiParent = Me
            FrmViewListEquipment.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "EquipmentToolStripMenuItem1_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CaseToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CaseToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CaseToolStripMenuItem1.Click
        Try
            Cursor = Cursors.WaitCursor
            FrmViewListShells.MdiParent = Me
            FrmViewListShells.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "CaseToolStripMenuItem1_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PrimerListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PrimerListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PrimerListToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            FrmViewListPrimer.MdiParent = Me
            FrmViewListPrimer.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "PrimerListToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PowderListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PowderListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PowderListToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            FrmViewListPowder.MdiParent = Me
            FrmViewListPowder.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "PowderListToolStripMenuItem_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the BulletToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BulletToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BulletToolStripMenuItem1.Click
        Try
            Cursor = Cursors.WaitCursor
            FrmViewListBullets.MdiParent = Me
            FrmViewListBullets.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "BulletToolStripMenuItem1_Click", Err.Number, 
                          ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the MakeReadyToUseAmmunitionToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MakeReadyToUseAmmunitionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MakeReadyToUseAmmunitionToolStripMenuItem.Click
        Try
            Dim configId As Long = lstConfigSheets.SelectedValue
            Dim configName As String = GeneralFunctions.GetTitle(DatabasePath, configId, _errOut)
            if _errOut.Length > 0 Then Throw new Exception(_errOut)
            'Dim objG As New GlobalFunctions
            'configName = objG.GetTitle(configId)
            Dim frmNew As New FrmLoadMakeReadyDetails With {
                .ConfigId = configId,
                .ConfigName = configName,
                .MdiParent = Me
            }
            frmNew.Show()
        Catch ex As Exception
            Call LogError(Name, "MakeReadyToUseAmmunitionToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the DeleteToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            Dim lngConfigId As Long = lstConfigSheets.SelectedValue
            'Dim obj As New BSDatabase
            'Dim objG As New GlobalFunctions
            'Dim strName As String = objG.GetName("SELECT * from Config_List_Name where ID=" & lngConfigId, "ConfigName")
            Dim sql As String = $"SELECT * from Config_List_Name where ID={lngConfigId}"
            Dim strName As String = BurnSoft.Applications.MLL.Database.GetName(DatabasePath, sql, "ConfigName", _errOut)
            if _errOut.Length > 0 Then Throw New Exception(_errOut)
            Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            'Dim isShotGun As Boolean = objG.IsShotGunCOnfig(lngConfigId)
            'Dim isShotGun As Boolean = ConfigListGeneral.IsShotGunCOnfig(DatabasePath, lngConfigId, errOut)
            if _errOut.Length > 0 Then Throw New Exception(_errOut)
            'Dim sql As String = ""
            If strAns = vbYes Then
                If Not ConfigListDataName.Delete(DatabasePath, lngConfigId, _errOut) Then Throw new Exception(_errOut)
                'If Not isShotGun Then
                '    sql = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & lngConfigId
                '    obj.ConnExec(sql)
                '    sql = "DELETE from Config_List_Data_NSG where CLNID=" & lngConfigId
                '    obj.ConnExec(sql)
                '    sql = "DELETE from Config_List_Name where ID=" & lngConfigId
                '    obj.ConnExec(sql)
                'Else
                '    sql = "DELETE from Config_List_Powder_Data_SG where CLNID=" & lngConfigId
                '    obj.ConnExec(sql)
                '    sql = "DELETE from Config_List_Data_SG where CLNID=" & lngConfigId
                '    obj.ConnExec(sql)
                '    sql = "DELETE from Config_List_Name where ID=" & lngConfigId
                '    obj.ConnExec(sql)
                'End If
                Call RefreshConfigData()
            End If
        Catch ex As Exception
            Call LogError(Name, "DeleteToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripMenuItem1.Click
        try
            Dim configId As Long = lstConfigSheets.SelectedValue
            Dim configName As String = GeneralFunctions.GetTitle(DatabasePath, configId, _errOut)
            'Dim objG As New GlobalFunctions
            'configName = objG.GetTitle(configId)
            Dim sMsg As String = "Renaming " & configName & " to:"
            Dim strNewName As String = Trim(GeneralHelpers.FluffContent(InputBox(sMsg, "Rename Configuration Name", configName)))
            If Len(strNewName) <> 0 And LCase(strNewName) <> LCase(configName) Then
                if Not ConfigListDataName.Rename(DatabasePath, configId, strNewName, _errOut) Then Throw new Exception(_errOut)
                'Dim sql As String = "UPDATE Config_List_Name set ConfigName='" & strNewName & "' where id=" & configId
                'Dim obj As New BSDatabase
                'obj.ConnExec(sql)
                Call RefreshConfigData()
            End If
        Catch ex As Exception
            Call LogError(Name, "ToolStripMenuItem1_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the ViewToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ViewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewToolStripMenuItem.Click
        try
            Cursor = Cursors.WaitCursor
            Call ViewConfigs()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "ViewToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CopyToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
        Try
            Dim configId As Long = lstConfigSheets.SelectedValue
        Dim configName As String = GeneralFunctions.GetTitle(DatabasePath, configId, _errOut)
        'Dim objG As New GlobalFunctions
        'configName = objG.GetTitle(configId)
        'Dim isShotGun As Boolean = False
        Dim sMsg As String = "What do you wish to call this new configuration?"
        Dim strNewName As String = Trim(GeneralHelpers.FluffContent(InputBox(sMsg, "Copy Configuration", configName)))
        If Len(strNewName) <> 0 And LCase(strNewName) <> LCase(configName) Then
            if Not ConfigListDataName.CopyConfig(DatabasePath, strNewName, configId, _errOut) Then Throw new Exception(_errOut)

            'Dim sql As String = "SELECT * from Config_List_Name where ID=" & configId
            'Dim obj As New BSDatabase
            'Call obj.ConnectDB()
            'Dim cmd As New OdbcCommand(sql, obj.Conn)
            'Dim rs As OdbcDataReader
            'rs = cmd.ExecuteReader
            'While rs.Read()
            '    If rs("IsShotGun") = 1 Then isShotGun = True
            '    Dim strNotes As String = " "
            '    If Not IsDBNull(rs("notes")) Then strNotes = GeneralHelpers.FluffContent(rs("Notes"))
            '    sql = "INSERT INTO Config_List_Name(ConfigName,IsPersonal,IsShotGun,Notes,IsActive,IsFav) VALUES('" & _
            '            strNewName & "'," & rs("IsPersonal") & "," & rs("IsShotGun") & ",'" & _
            '            strNotes & "'," & rs("IsActive") & "," & rs("IsFav") & ")"
            '    obj.ConnExec(sql)
            'End While
            'rs.Close()
            'rs = Nothing
            'cmd = Nothing
            ''Dim myId As Long = objG.GetID("SELECT * from Config_list_Name where ConfigName='" & strNewName & "'")
            'Dim myId As Long = ConfigListDataName.GetId(DatabasePath, strNewName, errOut)
            'if errOut.Length > 0 then Throw New Exception(errOut)
            'If Not isShotGun Then
            '    Call CopyConfigDetailsNsg(myId, configId)
            '    Call CopyConfigPowdersNsg(myId, configId)
            'Else
            '    Call CopyConfigDetailsSg(myId, configId)
            '    Call CopyConfigPowdersSg(myId, configId)
            'End If
            Call RefreshConfigData()
        End If
        Catch ex As Exception
            Call LogError(Name, "CopyToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the EditToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub EditToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditToolStripMenuItem.Click
        Try
            Dim frmNew As New frmEditConfig
            Dim configId As Long = lstConfigSheets.SelectedValue
            Dim configName As String
            'Dim objG As New GlobalFunctions
            'configName = objG.GetTitle(configId)
            configName = GeneralFunctions.GetTitle(DatabasePath, configId, _errOut)
            frmNew.ConfigID = configId
            frmNew.ConfigName = configName
            frmNew.MdiParent = Me
            frmNew.Show()
        Catch ex As Exception
            Call LogError(Name, "EditToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the UseConfigurationToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub UseConfigurationToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles UseConfigurationToolStripMenuItem.Click
        Try
            Dim frmNew As New FrmAddDataSheetRiflePistolsCfg
            frmNew.MdiParent = Me
            frmNew.Show()
        Catch ex As Exception
            Call LogError(Name, "UseConfigurationToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the RiflePistolToolStripMenuItem1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RiflePistolToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RiflePistolToolStripMenuItem1.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim frmnew As New FrmViewDataSheetRiflePistols
            frmnew.MdiParent = Me
            frmnew.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "RiflePistolToolStripMenuItem1_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the AmmunitionInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub AmmunitionInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AmmunitionInventoryToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            frmReport_Loaded_Ammunition.MdiParent = Me
            frmReport_Loaded_Ammunition.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "AmmunitionInventoryToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the EquipmentListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub EquipmentListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EquipmentListToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            frmReport_List_Equipment.MdiParent = Me
            frmReport_List_Equipment.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "EquipmentListToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the FirearmInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FirearmInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles FirearmInventoryToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            frmReport_List_Firearms.MdiParent = Me
            frmReport_List_Firearms.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "FirearmInventoryToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PowderInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PowderInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PowderInventoryToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            frmReport_PowderInventory.MdiParent = Me
            frmReport_PowderInventory.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "PowderInventoryToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PrimerInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PrimerInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PrimerInventoryToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            frmReport_PrimerInventory.MdiParent = Me
            frmReport_PrimerInventory.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "PrimerInventoryToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the BulletInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BulletInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BulletInventoryToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            FrmReportBulletInventory.MdiParent = Me
            FrmReportBulletInventory.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "BulletInventoryToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CaseBrassInventoryToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CaseBrassInventoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CaseBrassInventoryToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            FrmReportCaseInventory.MdiParent = Me
            FrmReportCaseInventory.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "CaseBrassInventoryToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the CaliberReloadToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CaliberReloadToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CaliberReloadToolStripMenuItem.Click
        Try
            FrmAddCaliberToCollection.MdiParent = Me
            FrmAddCaliberToCollection.Show()
        Catch ex As Exception
            Call LogError(Name, "CaliberReloadToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PreLoadedCaliberListToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PreLoadedCaliberListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PreLoadedCaliberListToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            FrmViewGeneralCalibers.MdiParent = Me
            FrmViewGeneralCalibers.Show()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Cursor = Cursors.Arrow
            Call LogError(Name, "PreLoadedCaliberListToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the PrimerTypeToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PrimerTypeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PrimerTypeToolStripMenuItem.Click
        Try
            frmEdit_PrimerTypes.MdiParent = Me
            frmEdit_PrimerTypes.Show()
        Catch ex As Exception
            Call LogError(Name, "PrimerTypeToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
        End Try
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
        FrmViewListShellHulls.MdiParent = Me
        FrmViewListShellHulls.Show()
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
            'Dim obj As New BSDatabase
            'Dim objG As New GlobalFunctions
            'Dim strSqlTable As String = "List_Calibers"
            'Dim strName As String = objG.GetName("SELECT * from " & strSqlTable & " where ID=" & lngCalId, "Cal")
            'Dim cOnfigCount As Long = objG.TotalConfigByCal(lngCalId)
            Dim cOnfigCount As Long = CaliberInventory.TotalConfigurationUsedByCaliber(DatabasePath, lngCalId, _errOut)
            if _errOut.Length > 0 Then Throw New Exception(_errOut)
            Dim strName As String = CaliberInventory.GetName(DatabasePath, lngCalId, _errOut)
            if _errOut.Length > 0 Then Throw New Exception(_errOut)
            'Dim cOnfigCount As Long = 
            Dim strAns As String
            'Dim sql As String = ""
            If cOnfigCount = 0 Then
                strAns = MsgBox("Are you sure you want to delete " & strName & "?", 
                                MsgBoxStyle.YesNo, "Delete Item from the Database.")
            Else
                strAns = MsgBox("Are you sure you want to delete " & strName & " and the " & 
                                cOnfigCount & " configurations with it?", MsgBoxStyle.YesNo, 
                                "Delete Item from the Database.")
            End If
            If strAns = vbYes Then
                Cursor = Cursors.WaitCursor
                If cOnfigCount = 0 Then
                    'Cursor = Cursors.WaitCursor
                    'sql = "DELETE from " & strSqlTable & " where ID=" & lngCalId
                    'obj.ConnExec(sql)
                    if not CaliberInventory.Delete(DatabasePath, lngCalId, _errOut) then throw new Exception(_errOut)
                    'Cursor = Cursors.Arrow
                Else
                    Dim lst as List(Of QueryConfigCaliberData)
                    Dim isShotgunConfig as Boolean  = ConfigListGeneral.IsShotgunConfig(DatabasePath, lngCalId, _errOut)
                    if _errOut.Length > 0 Then Throw New Exception(_errOut)
                    if isShotgunConfig Then
                        lst = QueryConfigCaliberShotgun.GetDetailsByCaliberId(DatabasePath, lngCalId, _errOut)
                        if _errOut.Length > 0 Then Throw New Exception(_errOut)
                    Else 
                        lst = QueryConfigCaliberMetallic.GetDetailsByCaliberId(DatabasePath, lngCalId, _errOut)
                        if _errOut.Length > 0 Then Throw New Exception(_errOut)
                    End If

                    For Each o As QueryConfigCaliberData In lst
                        If Not ConfigListDataName.Delete(DatabasePath, o.Id, _errOut ) Then Throw New Exception(_errOut)
                    Next


                    'sql = "Select ID,IsShotGun from qry_ConfigCal_NSG where CalID=" & lngCalId
                    'If ConfigListGeneral.IsShotgunConfig(DatabasePath, lngCalId, errOut) Then sql = "Select ID,IsShotGun from qry_ConfigCal_SG where CalID=" & lngCalId
                    'obj.ConnectDB()
                    'Dim cmd As New OdbcCommand(sql, obj.Conn)
                    'Dim rs As OdbcDataReader
                    'rs = cmd.ExecuteReader
                    'Dim configId As Long = 0
                    'Cursor = Cursors.WaitCursor
                    'While rs.Read
                    '    configId = rs("CLNID")
                    '    If rs("IsShotGun") = 0 Then
                    '        sql = "DELETE from Loaders_Log_Ammunition_Audit where CFID=" & configId
                    '        obj.ConnExec(sql)
                    '        sql = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & configId
                    '        obj.ConnExec(sql)
                    '        sql = "DELETE from Config_List_Data_NSG where CLNID=" & configId
                    '        obj.ConnExec(sql)
                    '        sql = "DELETE from Config_List_Name where ID=" & configId
                    '        obj.ConnExec(sql)
                    '    Else
                    '        sql = "DELETE from Loaders_Log_Ammunition_Audit where CFID=" & configId
                    '        obj.ConnExec(sql)
                    '        sql = "DELETE from Config_List_Powder_Data_SG where CLNID=" & configId
                    '        obj.ConnExec(sql)
                    '        sql = "DELETE from Config_List_Data_SG where CLNID=" & configId
                    '        obj.ConnExec(sql)
                    '        sql = "DELETE from Config_List_Name where ID=" & configId
                    '        obj.ConnExec(sql)
                    '    End If
                    'End While
                    'rs.Close()
                    'rs = Nothing
                    'cmd = Nothing
                    'sql = "DELETE from " & strSqlTable & " where ID=" & lngCalId
                    'obj.ConnExec(sql)
                    if not CaliberInventory.Delete(DatabasePath, lngCalId, _errOut) then throw new Exception(_errOut)
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
        myProcess.StartInfo.FileName = GeneralSettings.MY_HOTFIX_FILE
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
        FrmViewListShot.MdiParent = Me
        FrmViewListShot.Show()
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

    Private Sub NewConfigurationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewConfigurationToolStripMenuItem.Click

    End Sub
End Class
