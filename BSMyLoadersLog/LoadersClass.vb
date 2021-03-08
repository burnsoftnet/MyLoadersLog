Imports System
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Data
Imports System.Data.Odbc
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Configuration
Namespace LoadersClass
    Public Class BSRegistry
        Private _RegPath As String
        Private _DefaultDBName As String
#Region "Registry General Functions and Subs"
        Public Property DefaultRegPath() As String
            Get
                Return "Software\\BurnSoft\\BSMLL"
            End Get
            Set(ByVal value As String)
                _RegPath = value
            End Set
        End Property
        Public Property DefaultDBName() As String
            Get
                Return DATABASE_NAME
            End Get
            Set(ByVal value As String)
                _DefaultDBName = value
            End Set
        End Property
        Public Sub CreateSubKey(ByVal strValue As String)
            'Microsoft.Win32.Registry.LocalMachine.CreateSubKey(strValue)
            Microsoft.Win32.Registry.CurrentUser.CreateSubKey(strValue)
        End Sub
        Public Function RegSubKeyExists(ByVal strValue As String) As Boolean
            Dim bAns As Boolean = False
            Try
                Dim MyReg As RegistryKey
                'MyReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(strValue, True)
                MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strValue, True)
                If MyReg Is Nothing Then
                    bAns = False
                Else
                    bAns = True
                End If
            Catch ex As Exception
                bAns = False
            End Try
            Return bAns
        End Function
        Public Function GetRegSubKeyValue(ByVal strKey As String, ByVal strValue As String, ByVal strDefault As String) As String
            Dim sAns As String = ""
            Dim strMsg As String = ""
            Dim MyReg As RegistryKey
            Try
                If RegSubKeyExists(strKey) Then
                    'MyReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(strKey, True)
                    MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strKey, True)
                    If Len(MyReg.GetValue(strValue)) > 0 Then
                        sAns = MyReg.GetValue(strValue)
                    Else
                        MyReg.SetValue(strValue, strDefault)
                        sAns = strDefault
                    End If
                Else
                    Call CreateSubKey(strKey)
                    'MyReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(strKey, True)
                    MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strKey, True)
                    MyReg.SetValue(strValue, strDefault)
                    sAns = strDefault
                End If
            Catch ex As Exception
                sAns = strDefault
            End Try
            Return sAns
        End Function
        Public Function SettingsExists() As Boolean
            Dim bAns As Boolean = False
            Dim MyReg As RegistryKey
            Dim strValue As String = DefaultRegPath & "\Settings"
            On Error Resume Next
            'MyReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(strValue, True)
            MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strValue, True)
            If MyReg Is Nothing Then
                bAns = False
            Else
                bAns = True
            End If
            Return bAns
        End Function
        Public Sub UpDateAppDetails()
            Dim strValue As String = DefaultRegPath
            If Not RegSubKeyExists(strValue) Then Call CreateSubKey(strValue)
            Dim MyReg As RegistryKey
            'MyReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(strValue, True)
            MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strValue, True)
            MyReg.SetValue("Version", Application.ProductVersion)
            MyReg.SetValue("AppName", Application.ProductName)
            MyReg.SetValue("AppEXE", Application.ExecutablePath())
            MyReg.SetValue("Path", APPLICATION_PATH)
            MyReg.SetValue("LogPath", MyLogFile)
            MyReg.SetValue("DataBase", APPLICATION_PATH_DATA & "\" & DefaultDBName)
            MyReg.Close()
            'Dim BSAP As New BSAppConfig
            'BSAP.UpDateAppDetails()
        End Sub
#End Region
        Public Sub SetSettingDetails()

            If Not SettingsExists() Then
                Dim MyReg As RegistryKey
                Dim strValue As String = DefaultRegPath & "\Settings"
                'MyReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(strValue, True)
                MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strValue, True)

                'MyReg = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(strValue)
                MyReg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(strValue)
                MyReg.SetValue("Successful", Now)
                MyReg.SetValue("SetHistListtb", "")
                MyReg.SetValue("SetHistListdt", "")
                MyReg.SetValue("AlertOnBackUp", True)
                MyReg.SetValue("LastPath", "C:\")
                MyReg.SetValue("LastFile", DefaultDBName)
                MyReg.SetValue("BackupOnExit", False)
                MyReg.SetValue("UseOrgImage", True)
                MyReg.SetValue("LOADERTYPE_SHOTGUN", False)
                MyReg.SetValue("LOADERTYPE_NONSHOTGUN", True)
                MyReg.SetValue("DefaultList", "Caliber List")
                MyReg.SetValue("IndvReports", True)
                MyReg.SetValue("VIEW_FPS", True)
                MyReg.SetValue("VIEW_CUPS", True)
                MyReg.Close()
            End If
        End Sub
        Public Function GetViewSettings(ByVal sKey As String, Optional ByVal sDefault As String = "") As String
            Dim sAns As String = ""
            Dim strValue As String = DefaultRegPath & "\Settings"
            sAns = GetRegSubKeyValue(strValue, sKey, sDefault)
            Return sAns
        End Function
        Public Sub GetSettings(ByRef LastSucBackup As String, ByRef AlertOnBackUp As Boolean, ByRef TrackHistoryDays As Integer, ByRef TrackHistory As Boolean, ByRef AutoBackup As Boolean, ByRef UOIMG As Boolean, ByRef UseIPer As Boolean, Optional ByRef ConfigSort As String = "All")
            Dim NumberFormat As String
            Dim UseProxy As Boolean
            Dim AutoUpdate As Boolean
            Dim strValue As String = DefaultRegPath & "\Settings"
            Try
                TrackHistoryDays = CInt(GetRegSubKeyValue(strValue, "TrackHistoryDays", 15))
                TrackHistory = CBool(GetRegSubKeyValue(strValue, "TrackHistory", "False"))
                NumberFormat = GetRegSubKeyValue(strValue, "NumberFormat", "0000")
                AutoUpdate = CBool(GetRegSubKeyValue(strValue, "AutoUpdate", "False"))
                UseProxy = CBool(GetRegSubKeyValue(strValue, "UseProxy", "False"))
                LastSucBackup = GetRegSubKeyValue(strValue, "Successful", Now)
                AlertOnBackUp = CBool(GetRegSubKeyValue(strValue, "AlertOnBackUp", "True"))
                AutoBackup = CBool(GetRegSubKeyValue(strValue, "BackupOnExit", "False"))
                UOIMG = CBool(GetRegSubKeyValue(strValue, "UseOrgImage", "True"))
                LOADERTYPE_SHOTGUN = CBool(GetRegSubKeyValue(strValue, "LOADERTYPE_SHOTGUN", "False"))
                LOADERTYPE_NONSHOTGUN = CBool(GetRegSubKeyValue(strValue, "LOADERTYPE_NONSHOTGUN", "True"))
                VIEW_FPS = CBool(GetRegSubKeyValue(strValue, "VIEW_FPS", "True"))
                VIEW_CUPS = CBool(GetRegSubKeyValue(strValue, "VIEW_CUPS", "True"))
                DEFAULTLIST = GetRegSubKeyValue(strValue, "DefaultList", "Caliber List")
                UseIPer = CBool(GetRegSubKeyValue(strValue, "IndvReports", "True"))
                ConfigSort = GetRegSubKeyValue(strValue, "ConfigSort", "All")
            Catch ex As Exception
                Dim MyErr As Long = Err.Number
                If MyErr = 13 Then Call SetSettingDetails()
            End Try
        End Sub
        Public Sub SaveViewSettings(ByVal sKey As String, ByVal sValue As String)
            Dim strValue As String = DefaultRegPath & "\Settings"
            Dim MyReg As RegistryKey
            'MyReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(strValue, True)
            MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strValue, True)
            MyReg.SetValue(sKey, sValue)
            MyReg.Close()
        End Sub
        Public Sub SaveSettings(ByVal NumberFormat As String, ByVal TrackHistory As Boolean, ByVal TrackHistoryDays As Integer, ByVal AutoUpdate As Boolean, ByVal UseProxy As Boolean, ByVal AlertOnBackUp As Boolean, ByVal AutoBackup As Boolean, ByVal UOIMG As Boolean, ByVal UseSHOTGUN As Boolean, ByVal UseNONSHOTGUN As Boolean, ByVal UseDEFAULTLIST As String, ByVal UseIPer As Boolean, ByVal UseViewFPS As Boolean, ByVal UseViewCUPS As Boolean)
            Dim strValue As String = DefaultRegPath & "\Settings"
            If Not RegSubKeyExists(strValue) Then Call CreateSubKey(strValue)
            Dim MyReg As RegistryKey
            'MyReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(strValue, True)
            MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strValue, True)
            MyReg.SetValue("TrackHistoryDays", TrackHistoryDays)
            MyReg.SetValue("TrackHistory", TrackHistory)
            MyReg.SetValue("NumberFormat", NumberFormat)
            MyReg.SetValue("AutoUpdate", AutoUpdate)
            MyReg.SetValue("UseProxy", UseProxy)
            MyReg.SetValue("AlertOnBackUp", AlertOnBackUp)
            MyReg.SetValue("BackupOnExit", AutoBackup)
            MyReg.SetValue("UseOrgImage", UOIMG)
            MyReg.SetValue("LOADERTYPE_SHOTGUN", UseSHOTGUN)
            MyReg.SetValue("LOADERTYPE_NONSHOTGUN", UseNONSHOTGUN)
            MyReg.SetValue("DefaultList", UseDEFAULTLIST)
            MyReg.SetValue("IndvReports", UseIPer)
            MyReg.SetValue("VIEW_FPS", UseViewFPS)
            MyReg.SetValue("VIEW_CUPS", UseViewCUPS)
            MyReg.Close()
        End Sub
        Public Sub SaveLastWorkingDir(ByVal strPath As String)
            Dim MyReg As RegistryKey
            Dim strValue As String = DefaultRegPath & "\Settings"
            If Not RegSubKeyExists(strValue) Then Call CreateSubKey(strValue)
            MyReg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(strValue, RegistryKeyPermissionCheck.Default)
            MyReg.SetValue("LastWorkingPath", strPath)
            MyReg.Close()
        End Sub
        Public Sub SaveConfigSort(ByVal ConfigSort As String)
            Dim strValue As String = DefaultRegPath & "\Settings"
            If Not RegSubKeyExists(strValue) Then Call CreateSubKey(strValue)
            Dim MyReg As RegistryKey
            'MyReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(strValue, True)
            MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strValue, True)
            MyReg.SetValue("ConfigSort", ConfigSort)
            MyReg.Close()
        End Sub
        Public Function GetLastWorkingDir() As String
            Dim sAns As String = ""
            Dim strValue As String = DefaultRegPath & "\Settings"
            sAns = GetRegSubKeyValue(strValue, "LastWorkingPath", "C:\")
            Return sAns
        End Function
    End Class
    Public Class BSDatabase
        Public Conn As OdbcConnection
        ''' <summary>
        ''' This function will generate the connection string needed to connect to the load access database
        ''' <summary>
        Public Function sConnect() As String
            Dim sAns As String = ""
            Dim Obj As New BSRegistry
            sAns = "Driver={Microsoft Access Driver (*.mdb)};dbq=" & APPLICATION_PATH_DATA & "\" & Obj.DefaultDBName & ";Pwd=wtf.m@t3"
            Return sAns
        End Function
        ''' <summary>
        ''' This Sub will initialize the Conn ODBC Object with the connection string for database connection
        ''' </summary>
        Public Sub ConnectDB()
            Try
                Conn = New OdbcConnection(sConnect)
                Conn.Open()
            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "MLL.BSDatabase.ConnectDB" & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Sub
        Public Sub CloseDB()
            Try
                Conn.Close()
                Conn = Nothing
            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "MLL.BSDatabase.CloseDB" & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Sub
        Public Sub ConnExec(ByVal strSQL As String)
            Try
                Call ConnectDB()
                Dim CMD As New OdbcCommand
                CMD.Connection = Conn
                CMD.CommandText = strSQL
                CMD.ExecuteNonQuery()
                CMD.Connection.Close()
                CMD = Nothing
                Conn = Nothing
            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "MLL.BSDatabase.ConnExec" & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
                ObjFS.LogFile(MyLogFile, "ConnExec.strSQL=" & strSQL)
            End Try
        End Sub
        Public Function GetData(ByVal SQL As String) As DataTable
            Dim Table As New DataTable
            Try
                Table.Locale = System.Globalization.CultureInfo.InvariantCulture
                Call ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Conn)
                Dim RS As New OdbcDataAdapter
                RS.SelectCommand = CMD
                RS.Fill(Table)
            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "MLL.BSDatabase.GetData" & "::" & Err.Number & "::" & ex.Message.ToString() & Chr(10) & "SQL STATEMENT: " & SQL
                ObjFS.LogFile(MyLogFile, "GetData.SQL=" & SQL)
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return Table
        End Function
    End Class
    Public Class BSFileSystem
        Public Sub LogFile(ByVal strPath As String, ByVal strMessage As String)
            Dim SendMessage As String = DateTime.Now & vbTab & strMessage
            Call AppendToFile(strPath, SendMessage)
            MDIParentMain.tsslErrorsFound.Visible = True
            MDIParentMain.tsslErrorsFound.Enabled = True
        End Sub
        Public Sub DeleteFile(ByVal strPath As String)
            If File.Exists(strPath) Then
                File.Delete(strPath)
            End If
        End Sub
        Public Function FileExists(ByVal strPath As String)
            Return File.Exists(strPath)
        End Function
        Private Sub CreateFile(ByVal strPath As String)
            If File.Exists(strPath) = False Then
                Dim fs As New FileStream(strPath, FileMode.Append, FileAccess.Write, FileShare.Write)
                fs.Close()
            End If
        End Sub
        Private Sub AppendToFile(ByVal strPath As String, ByVal strNewLine As String)
            If File.Exists(strPath) = False Then Call CreateFile(strPath)
            Dim sw As New StreamWriter(strPath, True, Encoding.ASCII)
            sw.WriteLine(strNewLine)
            sw.Close()
        End Sub
        Public Sub OutPutToFile(ByVal strPath As String, ByVal strNewLine As String)
            Call AppendToFile(strPath, strNewLine)
        End Sub
        Public Sub MoveFile(ByVal strFrom As String, ByVal strTo As String)
            If File.Exists(strFrom) Then
                File.Move(strFrom, strTo)
            End If
        End Sub
        Public Sub CopyFile(ByVal strFrom As String, ByVal strTo As String)
            If File.Exists(strFrom) Then
                File.Copy(strFrom, strTo)
            End If
        End Sub
        Public Sub CreateDirectory(ByVal strPath As String)
            If Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
        End Sub
        Public Function DirectoryExists(ByVal strPath As String) As Boolean
            Return Directory.Exists(strPath)
        End Function
        Public Sub DeleteDirectory(ByVal strPath As String)
            If Directory.Exists(strPath) Then
                Directory.Delete(strPath)
            End If
        End Sub
        Public Sub MoveDirectory(ByVal strFrom As String, ByVal strTo As String)
            If Directory.Exists(strFrom) Then
                Directory.Move(strFrom, strTo)
            End If
        End Sub
        Public Sub RenameFile(ByVal strFrom As String, ByVal strTo As String)
            File.Move(strFrom, strTo)
        End Sub
        Public Function GetPathOfFile(ByVal strFile As String) As String
            Dim sAns As String = ""
            sAns = Path.GetDirectoryName(strFile)
            Return sAns
        End Function
        Public Function GetExtOfFile(ByVal strFile As String) As String
            Dim sAns As String = ""
            sAns = Path.GetExtension(strFile)
            Return sAns
        End Function
        Public Function GetNameOfFile(ByVal strFile As String) As String
            Dim sAns As String = ""
            sAns = Path.GetFileName(strFile)
            Return sAns
        End Function
        Public Function FileHasExtension(ByVal strFile As String) As Boolean
            Dim bAns As Boolean = False
            bAns = Path.HasExtension(strFile)
            Return bAns
        End Function
        Public Function GetNameOfFileWOExt(ByVal strFile As String) As String
            Dim sAns As String = ""
            sAns = Path.GetFileNameWithoutExtension(strFile)
            Return sAns
        End Function
    End Class
    Public Class AutoFillCollections
        Public Class ShotGun
            Private Function MainCollection(ByVal strColumn As String, ByVal strTable As String) As AutoCompleteStringCollection
                Dim iCol As New AutoCompleteStringCollection
                Dim ArrList As New ArrayList
                Dim SQL As String = "SELECT " & strColumn & " from " & strTable & " order by " & strColumn & " ASC"
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                iCol.Clear()
                If RS.HasRows Then
                    While (RS.Read())
                        If Not IsDBNull(RS(strColumn)) Then iCol.Add(RS(strColumn))
                    End While
                Else
                    iCol.Add("N/A")
                End If
                RS.Close()
                CMD = Nothing
                Call Obj.CloseDB()
                Return iCol
            End Function
            Private Function MainCollectionDistinct(ByVal strColumn As String, ByVal strTable As String) As AutoCompleteStringCollection
                Dim iCol As New AutoCompleteStringCollection
                Dim ArrList As New ArrayList
                Dim SQL As String = "SELECT distinct(" & strColumn & ") as Res from " & strTable & " order by " & strColumn & " ASC"
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                iCol.Clear()
                If RS.HasRows Then
                    While (RS.Read())
                        If Not IsDBNull(RS("Res")) Then iCol.Add(RS("Res"))
                    End While
                Else
                    iCol.Add("N/A")
                End If
                RS.Close()
                CMD = Nothing
                Call Obj.CloseDB()
                Return iCol
            End Function
            Public Function Config_Source_SG() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Source", "Config_List_Data_SG")
            End Function
            Public Function Config_LoadInOZ_SG() As AutoCompleteStringCollection
                Return MainCollectionDistinct("SW_t", "Config_List_Data_SG")
            End Function
            Public Function List_SG_WAD_Manufacturer() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Manufacturer", "List_SG_WAD")
            End Function
            Public Function List_SG_WAD_WAD() As AutoCompleteStringCollection
                Return MainCollectionDistinct("WAD", "List_SG_WAD")
            End Function
            Public Function List_SG_WAD_Price() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Price", "List_SG_WAD")
            End Function
            Public Function List_SG_Bushings_Powder_Manufacturer() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Manufacturer", "List_SG_Bushing_Powder")
            End Function
            Public Function List_SG_Bushings_Powder_Name() As AutoCompleteStringCollection
                Return MainCollectionDistinct("sName", "List_SG_Bushing_Powder")
            End Function
            Public Function List_SG_Bushings_Powder_sCharge() As AutoCompleteStringCollection
                Return MainCollectionDistinct("sCharge", "List_SG_Bushing_Powder")
            End Function
            Public Function List_SG_Bushings_Powder_Powder() As AutoCompleteStringCollection
                Return MainCollectionDistinct("name", "General_Powder")
            End Function
            Public Function List_SG_Bushings_Shot_Manufacturer() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Manufacturer", "List_SG_Bushing_Shot")
            End Function
            Public Function List_SG_Bushings_Shot_Name() As AutoCompleteStringCollection
                Return MainCollectionDistinct("sName", "List_SG_Bushing_Shot")
            End Function
            Public Function List_SG_Bushings_Shot_sCharge() As AutoCompleteStringCollection
                Return MainCollectionDistinct("sCharge", "List_SG_Bushing_Shot")
            End Function
            Public Function List_SG_Log_SG_Patterns() As AutoCompleteStringCollection
                Return MainCollectionDistinct("pd", "Loaders_Log_SG")
            End Function
            Public Function List_SG_Log_SG_ShotWt() As AutoCompleteStringCollection
                Return MainCollectionDistinct("shotwt", "Loaders_Log_SG")
            End Function
            Public Function List_SG_Log_SG_ShotSize() As AutoCompleteStringCollection
                Return MainCollectionDistinct("shotsize", "Loaders_Log_SG")
            End Function
            Public Function List_SG_Log_SG_Case() As AutoCompleteStringCollection
                Return MainCollectionDistinct("case", "Loaders_Log_SG")
            End Function
            Public Function List_SG_Log_SG_PowderBushing() As AutoCompleteStringCollection
                Return MainCollectionDistinct("pbm", "Loaders_Log_SG")
            End Function
            Public Function List_SG_Log_SG_Wad() As AutoCompleteStringCollection
                Return MainCollectionDistinct("wad", "Loaders_Log_SG")
            End Function
            Public Function List_SG_Log_SG_Primer() As AutoCompleteStringCollection
                Return MainCollectionDistinct("primer", "Loaders_Log_SG")
            End Function
            Public Function List_SG_Case_Manufacturer() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Manufacturer", "List_SG_Case")
            End Function
            Public Function List_SG_Case_Name() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Name", "List_SG_Case")
            End Function
            Public Function List_SG_Case_DRAM() As AutoCompleteStringCollection
                Return MainCollectionDistinct("DRAM", "List_SG_Case")
            End Function
            Public Function List_SG_Case_Gauge() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Gauge", "List_SG_Case")
            End Function
            Public Function List_SG_Case_Length() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Length", "List_SG_Case")
            End Function
            Public Function List_SG_Case_Price() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Price", "List_SG_Case")
            End Function
            Public Function List_SG_SHOTSLUG_Details_Manu() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Manufacturer", "List_SG_ShotType_Details")
            End Function
            Public Function List_SG_SHOTSLUG_Details_Name() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Name", "List_SG_ShotType_Details")
            End Function
            Public Function List_SG_SHOTSLUG_Details_mat() As AutoCompleteStringCollection
                Return MainCollectionDistinct("mat", "List_SG_ShotType_Details")
            End Function
            Public Function List_SG_SHOTSLUG_Details_ShotNo() As AutoCompleteStringCollection
                Return MainCollectionDistinct("ShotNo", "List_SG_ShotType_Details")
            End Function
            Public Function List_SG_SHOTSLUG_Details_weight() As AutoCompleteStringCollection
                Return MainCollectionDistinct("weight", "List_SG_ShotType_Details")
            End Function
            Public Function List_SG_SHOTSLUG_Details_CAL() As AutoCompleteStringCollection
                Return MainCollectionDistinct("CAL", "List_SG_ShotType_Details")
            End Function
            Public Function List_SG_SHOTSLUG_Details_Price() As AutoCompleteStringCollection
                Return MainCollectionDistinct("Price", "List_SG_ShotType_Details")
            End Function
        End Class
        Private Function MainCollection(ByVal strColumn As String, ByVal strTable As String) As AutoCompleteStringCollection
            Dim iCol As New AutoCompleteStringCollection
            Dim ArrList As New ArrayList
            Dim SQL As String = "SELECT " & strColumn & " from " & strTable & " order by " & strColumn & " ASC"
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            iCol.Clear()
            If RS.HasRows Then
                While (RS.Read())
                    If Not IsDBNull(RS(strColumn)) Then iCol.Add(RS(strColumn))
                End While
            Else
                iCol.Add("N/A")
            End If
            RS.Close()
            CMD = Nothing
            Call Obj.CloseDB()
            Return iCol
        End Function
        Private Function MainCollectionDistinct(ByVal strColumn As String, ByVal strTable As String) As AutoCompleteStringCollection
            Dim iCol As New AutoCompleteStringCollection
            Dim ArrList As New ArrayList
            Dim SQL As String = "SELECT distinct(" & strColumn & ") as Res from " & strTable & " order by " & strColumn & " ASC"
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            iCol.Clear()
            If RS.HasRows Then
                While (RS.Read())
                    If Not IsDBNull(RS("Res")) Then iCol.Add(RS("Res"))
                End While
            Else
                iCol.Add("N/A")
            End If
            RS.Close()
            CMD = Nothing
            Call Obj.CloseDB()
            Return iCol
        End Function
        Public Function General_Calibers() As AutoCompleteStringCollection
            Return MainCollection("Cal", "General_Calibers")
        End Function
        Public Function General_Primer_Type_ManuFacturers() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Manufacturer", "General_Primer")
        End Function
        Public Function General_Primer_Type_Name() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Name", "General_Primer")
        End Function
        Public Function General_Primer_Type_Price() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Price", "General_Primer")
        End Function
        Public Function General_Powder_Manufacturer() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Manufacturer", "General_Powder")
        End Function
        Public Function General_Powder_Name() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Name", "General_Powder")
        End Function
        Public Function General_Powder_WeightInPounds() As AutoCompleteStringCollection
            Return MainCollectionDistinct("weightlbs", "General_Powder")
        End Function
        Public Function General_Powder_Price() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Price", "General_Powder")
        End Function
        Public Function List_Bullets_Manufacturer() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Manufacturer", "List_Bullets")
        End Function
        Public Function List_Bullets_Name() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Name", "List_Bullets")
        End Function
        Public Function List_Bullets_Diameter() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Diameter", "List_Bullets")
        End Function
        Public Function List_Bullets_Sec_Den() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Sec_Den", "List_Bullets")
        End Function
        Public Function List_Bullets_Part_number() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Part_number", "List_Bullets")
        End Function
        Public Function List_Bullets_Ballistic_Coefficient() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Ballistic_Coefficient", "List_Bullets")
        End Function
        Public Function List_Bullets_Price() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Price", "List_Bullets")
        End Function
        Public Function List_Bullets_Weight() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Weight", "List_Bullets")
        End Function
        Public Function List_Case_Manufacturer() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Manufacturer", "List_Case")
        End Function
        Public Function List_Case_Name() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Name", "List_Case")
        End Function
        Public Function List_Case_Trim_to_length() As AutoCompleteStringCollection
            Return MainCollectionDistinct("ttl", "List_Case")
        End Function
        Public Function List_Case_Price() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Price", "List_Case")
        End Function
        Public Function General_Equipment_Manufacturer() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Manufacturer", "General_Equipment")
        End Function
        Public Function General_Equipment_Name() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Name", "General_Equipment")
        End Function
        Public Function General_Equipment_Use() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Use", "General_Equipment")
        End Function
        Public Function General_Equipment_Cost() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Cost", "General_Equipment")
        End Function
        Public Function ConfigName() As AutoCompleteStringCollection
            Return MainCollectionDistinct("ConfigName", "Config_List_Name")
        End Function
        Public Function Config_Source_NSG() As AutoCompleteStringCollection
            Return MainCollectionDistinct("Source", "Config_List_Data_NSG")
        End Function   
        Public Function Loaders_Log_NSG_GroupSize() As AutoCompleteStringCollection
            Return MainCollectionDistinct("gs", "Loaders_Log_NSG")
        End Function
        Public Function Loaders_Log_NSG_Powder() As AutoCompleteStringCollection
            Return MainCollectionDistinct("pwm", "Loaders_Log_NSG")
        End Function
        Public Function Loaders_Log_NSG_Bullet() As AutoCompleteStringCollection
            Return MainCollectionDistinct("bullet", "Loaders_Log_NSG")
        End Function
        Public Function Loaders_Log_NSG_primer() As AutoCompleteStringCollection
            Return MainCollectionDistinct("primer", "Loaders_Log_NSG")
        End Function
        Public Function Loaders_Log_NSG_case() As AutoCompleteStringCollection
            Return MainCollectionDistinct("case", "Loaders_Log_NSG")
        End Function
        Public Function Loaders_Log_NSG_conditions() As AutoCompleteStringCollection
            Return MainCollectionDistinct("conditions", "Loaders_Log_NSG")
        End Function
        Public Function Loaders_Log_NSG_tl() As AutoCompleteStringCollection
            Return MainCollectionDistinct("tl", "Loaders_Log_NSG")
        End Function
        Public Function Loaders_Log_NSG_notes() As AutoCompleteStringCollection
            Return MainCollectionDistinct("notes", "Loaders_Log_NSG")
        End Function
        Public Function Loaders_Log_NSG_ConfigName() As AutoCompleteStringCollection
            Return MainCollectionDistinct("ConfigName", "Loaders_Log_NSG")
        End Function
      
    End Class
    Public Class GlobalFunctions
        Public Function DatabaseVersion() As Double
            Dim dAns As Double = 0
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT top 1 dbver from DB_Version order by ID Desc"
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                If RS.HasRows Then
                    While RS.Read()
                        dAns = CDbl(RS("dbver"))
                    End While
                Else
                    dAns = 0
                End If
                RS.Close()
                RS = Nothing
                Obj.CloseDB()
                Obj = Nothing
            Catch ex As Exception
                Call LogError("LoadersClass.GlobablFunctions", "DatabaseVersion", Err.Number, ex.Message.ToString)
                dAns = 0
            End Try
            Return dAns
        End Function
        Public Function ObjectExistsinDB(ByVal strObject As String, ByVal strField As String, ByVal strTable As String) As Boolean
            Try
                Dim bAns As Boolean = False
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "SELECT " & strField & " from " & strTable & " where " & strField & "='" & strObject & "'"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                If RS.HasRows Then
                    bAns = True
                Else
                    bAns = False
                End If
                RS.Close()
                CMD = Nothing
                Call Obj.CloseDB()
                Return bAns
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "ObjectsExistinDB(String)"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Function
        Public Function ObjectExistsinDB(ByVal strObject As Integer, ByVal strField As String, ByVal strTable As String) As Boolean
            Try
                Dim bAns As Boolean = False
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "SELECT " & strField & " from " & strTable & " where " & strField & "=" & strObject & ""
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                If RS.HasRows Then
                    bAns = True
                Else
                    bAns = False
                End If
                RS.Close()
                CMD = Nothing
                Call Obj.CloseDB()
                Return bAns
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "ObjectsExistinDB(Intenger)"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Function
        Public Function GetID(ByVal SQL As String, Optional ByVal sField As String = "ID") As Long
            Try
                Dim sAns As Long = 0
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                If RS.HasRows Then
                    While (RS.Read())
                        sAns = CLng(RS(sField))
                    End While
                Else
                    sAns = 0
                End If
                RS.Close()
                CMD = Nothing
                Call Obj.CloseDB()
                Return sAns
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "GetID"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Function
        Public Function GetName(ByVal SQL As String, ByVal strValue As String) As String
            Dim sAns As String = "N/A"
            Try
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                If RS.HasRows Then
                    While (RS.Read())
                        If Not IsDBNull(RS(strValue)) Then
                            sAns = RS(strValue)
                        Else
                            sAns = 0
                        End If
                    End While
                Else
                    sAns = "N/A"
                End If
                RS.Close()
                CMD = Nothing
                Call Obj.CloseDB()
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "GetName"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return sAns
        End Function
        Public Function CountFirearms() As Integer
            Dim iAns As Integer = 0
            Try
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "SELECT Count(*) as Total from Loaders_Log_Firearms where MGCID=0"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    iAns = RS("Total")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Call Obj.CloseDB()
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "CountFirearms"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return iAns
        End Function
        Public Function CountReadyToUseAmmo() As Long
            Dim lAns As Long = 0
            Try
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "SELECT Sum(Qty) as Total from Loaders_Log_Ammunition"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    If Not IsDBNull(RS("Total")) Then lAns = CLng(RS("Total"))
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Call Obj.CloseDB()
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "CountReadyToUseAmmo"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return lAns
        End Function
        Public Function GetTitle(ByVal lngID As Long) As String
            Dim sAns As String = ""
            sAns = GetName("SELECT * from Config_List_Name where ID=" & lngID, "ConfigName")
            Return sAns
        End Function
        Public Function GetAmmoTypeID(ByVal sName) As Long
            Dim lAns As Long = 0
            lAns = GetID("SELECT ID from General_Ammunition_Type where FType='" & sName & "'")
            Return lAns
        End Function
        Public Function GetAmmoTypeIDSG(ByVal sName) As Long
            Dim lAns As Long = 0
            lAns = GetID("SELECT ID from List_SG_ShotCharge_Loads where Name='" & sName & "'")
            Return lAns
        End Function
        Public Function GetAmmoTypeName_SG(ByVal TID As Long) As String
            Return GetName("SELECT Name from List_SG_ShotCharge_Loads where ID=" & TID, "Name")
        End Function
        Public Function GetCaliberID(ByVal sName As String, Optional ByVal AutoAdd As Boolean = False) As Long
            Dim lans As Long = 0
            lans = GetID("SELECT ID from List_Calibers where cal='" & sName & "'")
            If AutoAdd Then
                Dim Obj As New BSDatabase
                Dim SQL As String = "INSERT INTO List_Calibers(Cal) VALUES('" & sName & "')"
                Obj.ConnExec(SQL)
                lans = GetID("SELECT ID from List_Calibers where cal='" & sName & "'")
            End If
            Return lans
        End Function
        Public Function TotalCost_Equipment() As String
            Dim sAns As String = "0.00"
            Try
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "Select Sum(Cost) as TC from General_Equipment"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    sAns = CStr(ConvToNum(RS("TC")))
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "TotalCost_Equipment"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return sAns
        End Function
        Public Sub GetFirearmDetails(ByVal FID As Long, Optional ByRef MGCID As Long = 0, _
                            Optional ByRef FullName As String = "", Optional ByRef Manu As String = "", _
                            Optional ByRef Model As String = "", Optional ByRef Cal As String = "", _
                            Optional ByRef Barrel As String = "", Optional ByRef SerialNo As String = "", _
                            Optional ByRef GType As String = "")
            Try
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "Select * from Loaders_Log_Firearms where ID=" & FID
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    If Not IsDBNull(RS("MGCID")) Then MGCID = RS("MGCID")
                    If Not IsDBNull(RS("FullName")) Then FullName = RS("FullName")
                    If Not IsDBNull(RS("Manu")) Then Manu = RS("Manu")
                    If Not IsDBNull(RS("Model")) Then Model = RS("Model")
                    If Not IsDBNull(RS("Cal")) Then Cal = RS("Cal")
                    If Not IsDBNull(RS("Barrel")) Then Barrel = RS("Barrel")
                    If Not IsDBNull(RS("SerialNo")) Then SerialNo = RS("SerialNo")
                    If Not IsDBNull(RS("GType")) Then GType = RS("GType")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "GetFirearmDetails"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Sub
        Public Function GetFirearmID(ByVal Fullname As String) As Long
            Dim lAns As Long = 0
            Try
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "Select ID from Loaders_Log_Firearms where FullName='" & Fullname & "'"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    lAns = RS("id")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "GetFirearmID"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return lAns
        End Function
        Public Function GetGaugeID(ByVal Fullname As String) As Long
            Dim lAns As Long = 0
            Try
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "Select ID from List_SG_Gauge where ga='" & Fullname & "'"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    lAns = RS("id")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "GetGaugeID"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return lAns
        End Function
        Public Function TotalConfigByCal(ByVal lCalID As Long) As Long
            Dim iAns As Integer = 0
            Try
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "SELECT Count(*) as Total from qry_CFG_SR_PowderList where MyCalID=" & lCalID
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    iAns = RS("Total")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Call Obj.CloseDB()
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "TotalConfigByCal"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return iAns
        End Function
        Function IsShotGunCOnfig(ByVal iCal As Long) As Boolean
            Dim bAns As Boolean = False
            bAns = ObjectExistsinDB(CInt(iCal), "Id", "qry_ConfigCal_SG")
            Return bAns
        End Function
        Function FormatForXML(ByVal sValue As String) As String
            Dim sAns As String = ""
            sAns = Replace(sValue, "&", "&amp;")
            Return sAns
        End Function
        Function FormatFromXML(ByVal sValue As String) As String
            Dim sAns As String = ""
            sAns = Replace(sValue, "&amp;", "&")
            sAns = Replace(sAns, "'", "''")
            Return sAns
        End Function
        Public Function IsSlugConfig(ByVal BID As String) As Boolean
            Dim bAns As Boolean = False
            Try
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "Select IsSlug from List_SG_ShotType_Details where ID=" & BID
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    If RS("IsSlug") = 1 Then bAns = True
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "IsSlugConfig"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return bAns
        End Function
        Private Function InSG(ByVal lCALID As Long) As Boolean
            Dim bAns As Boolean = False
            Try
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "SELECT Config_List_Name.ID,Config_List_Name.ConfigName,Config_List_Name.IsPersonal,Config_List_Name.IsActive,Config_List_Name.IsFav,Config_List_Name.IsShotgun, Config_List_Data_SG.ATID,Config_List_Data_SG.CALID,Config_List_Data_SG.PRID,Config_List_Data_SG.CAID,Config_List_Data_SG.SW,Config_List_Data_SG.SS,Config_List_Data_SG.WAD,Config_List_Data_SG.SCL,Config_List_Data_SG.GID,Config_List_Data_SG.LTID from Config_List_Name INNER JOIN Config_List_Data_SG on Config_List_Data_SG.CLNID=Config_List_Name.ID " & _
                                    "where Config_List_Data_SG.ATID=" & lCALID & " order by Config_List_Name.ConfigName ASC"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                bAns = RS.HasRows
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "InSG"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return bAns
        End Function
        Private Function InRP(ByVal lCALID As Long) As Boolean
            Dim bAns As Boolean = False
            Try
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                Dim SQL As String = "SELECT Config_List_Name.ID,Config_List_Name.ConfigName,Config_List_Name.IsPersonal,Config_List_Name.IsActive,Config_List_Name.IsFav,Config_List_Name.IsShotgun, " & _
                                    "Config_List_Data_NSG.ATID, Config_List_Data_NSG.CALID,Config_List_Data_NSG.CAID, Config_List_Data_NSG.BID, " & _
                                    "Config_List_Data_NSG.PRID from Config_List_Name INNER JOIN Config_List_Data_NSG on " & _
                                    "Config_List_Data_NSG.CLNID=Config_List_Name.ID  where Config_List_Data_NSG.CALID=" & lCALID & " order by Config_List_Name.ConfigName ASC"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                bAns = RS.HasRows
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "InRP"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return bAns
        End Function
        Public Function IsNotInShotgunConfigbyCal(ByVal lCALID As Long) As Boolean
            Dim bAns As Boolean = False
            Try
                If Not InRP(lCALID) Then
                    If InSG(lCALID) Then
                        bAns = False
                    Else
                        bAns = True
                    End If
                Else
                    bAns = True
                End If
            Catch ex As Exception
                Dim strform As String = "LoadersClass.GlobalFunctions"
                Dim strProcedure As String = "IsNotInShotgunConfigbyCal"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
            Return bAns
        End Function
    End Class
    Public Class BSMGC
        Private _RegPath As String
        Public Conn As OdbcConnection
        Public Property DefaultRegPath() As String
            Get
                Return "Software\\BurnSoft\\BSMGC"
            End Get
            Set(ByVal value As String)
                _RegPath = value
            End Set
        End Property
        Public Function MyGunCollectionIsInstalled() As Boolean
            Dim bAns As Boolean = False
            Dim MyReg As RegistryKey
            Dim strValue As String = DefaultRegPath
            On Error Resume Next
            MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strValue, True)
            If MyReg Is Nothing Then
                bAns = False
            Else
                bAns = True
            End If
            Return bAns
            Return bAns
        End Function
        Public Function sConnect() As String
            Dim sAns As String = ""
            sAns = "Driver={Microsoft Access Driver (*.mdb)};dbq=" & MGCPath & ";Pwd=14un0t2n0"
            Return sAns
        End Function
        Public Sub ConnectDB()
            Try
                Conn = New OdbcConnection(sConnect)
                Conn.Open()
            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "LoadersClass.BSMGC.ConnectDB" & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Sub
        Public Sub CloseDB()
            Try
                Conn.Close()
                'Conn = Nothing
            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "MGC.BSDatabase.CloseDB" & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Sub
        Public Sub ConnExec(ByVal strSQL As String)
            Try
                Call ConnectDB()
                Dim CMD As New OdbcCommand
                CMD.Connection = Conn
                CMD.CommandText = strSQL
                CMD.ExecuteNonQuery()
                CMD.Connection.Close()
                CMD = Nothing
                'Conn = Nothing
            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "MGC.BSDatabase.ConnExec" & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Sub
        Public Function GetMGCPath() As String
            Dim sAns As String = ""
            Dim MyReg As RegistryKey
            Dim strValue As String = DefaultRegPath
            MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strValue, True)
            If MyReg Is Nothing Then
                MyReg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(strValue)
            End If
            sAns = MyReg.GetValue("DataBase", "")
            MyReg.Close()
            Return sAns
        End Function
        Public Function GetMGCEXEPath() As String
            Dim sAns As String = ""
            Dim MyReg As RegistryKey
            Dim strValue As String = DefaultRegPath
            MyReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strValue, True)
            If MyReg Is Nothing Then
                MyReg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(strValue)
            End If
            sAns = MyReg.GetValue("AppEXE", "")
            MyReg.Close()
            Return sAns
        End Function
        Public Function CountFirearms() As Integer
            Dim iAns As Integer = 0
            Call ConnectDB()
            Dim SQL As String = "SELECT Count(*) as Total from Gun_Collection where ItemSold=0"
            Dim CMD As New OdbcCommand(SQL, Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                iAns = RS("Total")
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Call CloseDB()
            Return ians
        End Function
        Public Function ObjectExistsinDB(ByVal strObject As String, ByVal strField As String, ByVal strTable As String) As Boolean
            Try
                Dim bAns As Boolean = False
                Call ConnectDB()
                Dim SQL As String = "SELECT " & strField & " from " & strTable & " where " & strField & "='" & strObject & "'"
                Dim CMD As New OdbcCommand(SQL, Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                If RS.HasRows Then
                    bAns = True
                Else
                    bAns = False
                End If
                RS.Close()
                CMD = Nothing
                Call CloseDB()
                Return bAns
            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "LoadersClass.BSMGC.ObjectExistsinDB" & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Function
        Public Function GetID(ByVal SQL As String) As Long
            Try
                Dim sAns As Long = 0
                Call ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                If RS.HasRows Then
                    While (RS.Read())
                        sAns = CLng(RS("ID"))
                    End While
                Else
                    sAns = 0
                End If
                RS.Close()
                CMD = Nothing
                Call CloseDB()
                Return sAns
            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "LoadersClass.BSMGC.GetID" & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Function
        Private Function GetName(ByVal SQL As String, ByVal strValue As String) As String
            Dim sAns As String = "N/A"
            Call ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            If RS.HasRows Then
                While (RS.Read())
                    If Not IsDBNull(RS(strValue)) Then
                        sAns = RS(strValue)
                    Else
                        sAns = 0
                    End If
                End While
            Else
                sAns = "N/A"
            End If
            RS.Close()
            CMD = Nothing
            Call CloseDB()
            Return sAns
        End Function
        Public Function GetManufacturersID(ByVal strValue As String) As Long
            Dim SQL As String = "SELECT ID from Gun_Manufacturer where Brand='" & strValue & "'"
            Dim iAns As Long = GetID(SQL)
            If iAns = 0 Then
                ConnExec("INSERT INTO Gun_Manufacturer(Brand) VALUES('" & strValue & "')")
                iAns = GetID(SQL)
            End If
            Return iAns
        End Function
        Public Function GetManufacturersName(ByVal strValue As String) As String
            Dim SQL As String = "SELECT Brand from Gun_Manufacturer where ID=" & strValue
            Dim sAns As String = GetName(SQL, "Brand")
            Return sAns
        End Function
        Public Function GetModelID(ByVal strValue As String, ByVal StrValueID As Long) As Long
            Dim SQL As String = "SELECT ID from Gun_Model where Model='" & strValue & "' and GMID=" & StrValueID
            Dim iAns As Long = GetID(SQL)
            If iAns = 0 Then
                ConnExec("INSERT INTO Gun_Model(Model,GMID) VALUES('" & strValue & "'," & StrValueID & ")")
                iAns = GetID(SQL)
            End If
            Return iAns
        End Function
        Public Function GetNationalityID(ByVal strValue As String)
            Dim SQL As String = "SELECT ID from Gun_Nationality where Country='" & strValue & "'"
            Dim iAns As Long = GetID(SQL)
            If iAns = 0 Then
                ConnExec("INSERT INTO Gun_Nationality(Country) VALUES('" & strValue & "')")
                iAns = GetID(SQL)
            End If
            Return iAns
        End Function
        Public Function GetGripID(ByVal strValue As String) As Long
            Dim SQL As String = "SELECT ID from Gun_GripType where grip='" & strValue & "'"
            Dim iAns As Long = GetID(SQL)
            If iAns = 0 Then
                ConnExec("INSERT INTO Gun_GripType(grip) VALUES('" & strValue & "')")
                iAns = GetID(SQL)
            End If
            Return iAns
        End Function
        Public Function GetGunShopID(ByVal strValue As String) As Long
            Try
                Dim SQL As String = "SELECT ID from Gun_Shop_Details where Name='" & strValue & "'"
                Dim iAns As Long = GetID(SQL)
                If iAns = 0 Then
                    ConnExec("INSERT INTO Gun_Shop_Details(Name) VALUES('" & strValue & "')")
                    iAns = GetID(SQL)
                End If
                Return iAns

            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "LoadersClass.BSMGC.GetGunShopID" & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Function
        Public Function GetLastFirearmID() As Long
            Try
                Dim SQL As String = "SELECT Top 1 ID from Gun_Collection order by ID DESC" '"SELECT MAX(ID) as ID from Gun_Collection"
                Dim iAns As Long = GetID(SQL)
                Return iAns
            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "LoadersClass.BSMGC.GetLastFirearmID" & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Function
        Public Sub UpdateGunType(ByVal strType As String)
            Try
                If Not ObjectExistsinDB(strType, "Type", "Gun_Type") Then
                    Dim SQL As String = "INSERT INTO Gun_Type(Type) VALUES('" & strType & "')"
                    ConnExec(SQL)
                End If
            Catch ex As Exception
                Dim ObjFS As New BSFileSystem
                Dim sMessage As String = "LoadersClass.BSMGC.UpdateGunType" & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Sub
        Public Function CaliberExists(ByVal strCaliber As String) As Boolean
            Return ObjectExistsinDB(strCaliber, "Cal", "Gun_Cal")
        End Function
        Public Sub AddFirearmToMGC(ByVal FullName As String, ByVal Manu As String, ByVal Model As String, ByVal Cal As String, ByVal Barrel As String, ByVal SerialNo As String, ByVal GType As String, Optional ByRef MGCID As Long = 0)
            Dim lngManID As Long = GetManufacturersID(Manu)
            Dim lngModelID As Long = GetModelID(Model, lngManID)
            Dim lngNationalityID As Long = GetNationalityID("N/A")
            Dim lngGripID As Long = GetGripID("N/A")
            Dim intHasAss As Integer = 0
            Dim strPurchasedFrom As String = "My Loaders Log Import"
            Dim SQL As String = "INSERT INTO Gun_Collection(MID,FullName,ModelName," & _
                                "ModelID,SerialNumber,Type,Caliber,NatID,GripID," & _
                                "BarrelLength,PurchasedFrom,Condition,AppraisalDate) VALUES(" & lngManID & ",'" & FullName & _
                                "','" & Model & "'," & lngModelID & ",'" & SerialNo & _
                                "','" & GType & "','" & Cal & "'," & lngNationalityID & _
                                "," & lngGripID & ",'" & Barrel & "','" & strPurchasedFrom & "','New',' ')"
            Call ConnExec(SQL)
            If Not ObjectExistsinDB(strPurchasedFrom, "Name", "Gun_Shop_Details") Then
                SQL = "INSERT INTO Gun_Shop_Details(Name,Address1,City,State,Zip) VALUES('" & strPurchasedFrom & "','N/A','N/A','N/A','N/A')"
                Call ConnExec(SQL)
            End If
            Dim GSID As Long = GetGunShopID(strPurchasedFrom)
            Dim ItemID As Long = GetLastFirearmID()
            MGCID = ItemID
            SQL = "UPDATE Gun_Collection set SID=" & GSID & " where ID=" & ItemID
            Call ConnExec(SQL)
            If Not CaliberExists(Cal) Then ConnExec("INSERT INTO Gun_Cal (Cal) VALUES('" & Cal & "')")
        End Sub
        Public Function AmmoIsAlreadyListed(ByVal Manufacturer As String, ByVal Name As String, _
       ByVal Cal As String, ByVal Grain As String, ByVal Jacket As String, Optional ByRef Qty As Long = 0, Optional ByRef MID As Long = 0)
            Dim bAns As Boolean = False
            Call ConnectDB()
            Dim SQL As String = "SELECT * from Gun_Collection_Ammo where Manufacturer='" & _
                        Manufacturer & "' and Name='" & Name & "' and Cal='" & Cal & _
                        "' and Grain='" & Grain & "' and Jacket='" & Jacket & "'"
            Dim CMD As New OdbcCommand(SQL, Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            bAns = RS.HasRows
            While RS.Read
                Qty = RS("qty")
                MID = RS("ID")
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            CloseDB()
            Return bAns
        End Function
    End Class
    Public Class InventoryMath
        ''' <summary>
        ''' This is a the local Private Sub for Logging Errors in the Error Log
        ''' </summary>
        Private Sub LogError(ByVal sProcedure As String, ByVal iErrNo As Long, ByVal sErrorDesc As String)
            Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
            Dim sForm As String = "LoadersClass.InventoryMath"
            Dim sMessage As String = sForm & "." & sProcedure & "::" & iErrNo & "::" & sErrorDesc
            ObjFS.LogFile(MyLogFile, sMessage)
        End Sub
        ''' <summary>
        ''' Gets the Powder ID ( PID ) from Config_List_Powder_Data_NSG Table for Non-Shotguns
        ''' and returns the ID alonf with the Default Powder Load ( byref DefaultPowderLoad ) value.
        ''' Just pass the Configuration ID to this function
        ''' </summary>
        ''' <returns></returns>
        Function GetPrefNSGPowderID(ByVal ConfigID As Long, Optional ByRef DefaultPowderLoad As Double = 0, Optional ByRef DefaultFPS As Double = 0) As Long
            Dim iAns As Integer = 0
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT PID,Load_Mid,FPS_MID from Config_List_Powder_Data_NSG where IsPref=1 and CLNID=" & ConfigID
                Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    iAns = RS("PID")
                    DefaultPowderLoad = RS("Load_Mid")
                    DefaultFPS = RS("FPS_MID")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
            Catch ex As Exception
                Call LogError("GetPrefNSGPowderID", Err.Number, ex.Message.ToString())
            End Try
            Return iAns
        End Function
        ''' <summary>
        ''' Get the Powder ID ( OID ) from the Config_List_Powder_Data_SG Table for Shotguns
        ''' and returns the ID along with the Default Powder Load ( byref DefaultPowderLoad ) value.
        ''' Just pass the configuration ID to this function
        ''' </summary>
        ''' <returns></returns>
        Function GetPrefSGPowderID(ByVal ConfigID As Long, Optional ByRef DefaultPowderLoad As Double = 0, Optional ByRef DefaultFPS As Double = 0) As Long
            Dim iAns As Integer = 0
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT PID,Load_Mid,FPS_MID from Config_List_Powder_Data_SG where IsPref=1 and CLNID=" & ConfigID
                Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    iAns = RS("PID")
                    DefaultPowderLoad = RS("Load_Mid")
                    DefaultFPS = RS("FPS_MID")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
            Catch ex As Exception
                Call LogError("GetPrefNSGPowderID", Err.Number, ex.Message.ToString())
            End Try
            Return iAns
        End Function
        ''' <summary>
        ''' Get the preffered Powder ID and Measurement by passing the Configuration ID For Pistol/Rifle Configs
        ''' </summary>
        ''' <returns></returns>
        Function GetPrefNSGPowderIDID(ByVal ConfigID As Long, Optional ByRef DefaultPowderLoad As Double = 0) As Long
            Dim iAns As Integer = 0
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT ID,PID,Load_Mid from Config_List_Powder_Data_NSG where IsPref=1 and CLNID=" & ConfigID
                Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    iAns = RS("ID")
                    DefaultPowderLoad = RS("Load_Mid")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
            Catch ex As Exception
                Call LogError("GetPrefNSGPowderIDID", Err.Number, ex.Message.ToString())
            End Try
            Return iAns
        End Function
        ''' <summary>
        ''' Get the preffered Powder ID and Measurement by passing the Conifuration ID for Shotgun Configs
        ''' </summary>
        ''' <returns></returns>
        Function GetPrefSGPowderIDID(ByVal ConfigID As Long, Optional ByRef DefaultPowderLoad As Double = 0) As Long
            Dim iAns As Integer = 0
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT ID,PID,Load_Mid from Config_List_Powder_Data_SG where IsPref=1 and CLNID=" & ConfigID
                Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    iAns = RS("ID")
                    DefaultPowderLoad = RS("Load_Mid")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
            Catch ex As Exception
                Call LogError("GetPrefSGPowderIDID", Err.Number, ex.Message.ToString())
            End Try
            Return iAns
        End Function
        ''' <summary>
        '''  Get the Powder weight in grains by passing the Powder ID from the General_Powder Table
        ''' </summary>
        ''' <returns></returns>
        Function GetQTYPerPowder(ByVal lngPID As Long) As Double
            Dim dAns As Double = 0
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT weightgn,weightlbs from General_Powder where ID=" & lngPID
                Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    dAns = RS("weightgn")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
            Catch ex As Exception
                Call LogError("GetQTYPerPowder", Err.Number, ex.Message.ToString())
            End Try
            Return dAns
        End Function
        ''' <summary>
        '''  Get the Price Per Powder from the General_Powder Table by Passing the powder ID
        ''' </summary>
        ''' <returns></returns>
        Function GetPricePerPowder(ByVal lngPID As Long) As Double
            Dim dAns As Double = 0
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT ePPP from General_Powder where ID=" & lngPID
                Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    dAns = RS("ePPP")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
            Catch ex As Exception
                Call LogError("GetPricePerPowder", Err.Number, ex.Message.ToString())
            End Try
            Return dAns
        End Function
        ''' <summary>
        '''  Get the Ammunition Type from the General_Ammunition_Type table by passing the ID
        ''' </summary>
        ''' <returns></returns>
        Function GetAmmoType(ByVal lngID As Long) As String
            Dim sAns As String = "N/A"
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT * from General_Ammunition_Type where ID=" & lngID
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    sAns = RS("FType")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Call LogError("GetAmmoType", Err.Number, ex.Message.ToString())
            End Try
            Return sAns
        End Function
        ''' <summary>
        ''' Get the Primer Type String Value from the General_Primer_Type table by passing the Primer ID
        ''' </summary>
        ''' <returns></returns>
        Function GetPrimerType(ByVal lngID As Long) As String
            Dim sAns As String = "N/A"
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT * from General_Primer_Type where ID=" & lngID
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    sAns = RS("Name")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Call LogError("GetPrimerType", Err.Number, ex.Message.ToString())
            End Try
            Return sAns
        End Function
        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Function GetCaliber(ByVal lngID As Long) As String
            Dim sAns As String = "N/A"
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT * from List_Calibers where ID=" & lngID
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    sAns = RS("Cal")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Call LogError("GetCaliber", Err.Number, ex.Message.ToString())
            End Try
            Return sAns
        End Function
        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Public Sub LoadBulletInfo(ByVal lngBID As Long, Optional ByRef Manufacturer As String = "", _
                    Optional ByRef Name As String = "", Optional ByRef Diameter As String = "", _
                    Optional ByRef Weight As String = "", Optional ByRef SectionalDensity As String = "", _
                    Optional ByRef PartNumber As String = "", Optional ByRef Ballistic_Coefficient As String = "", _
                    Optional ByRef lQty As Long = 0, Optional ByRef bullet_type As String = "", _
                    Optional ByRef ePPB As Double = 0)
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT * from List_Bullets where ID=" & lngBID
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    If Not IsDBNull(RS("Manufacturer")) Then Manufacturer = RS("Manufacturer")
                    If Not IsDBNull(RS("Name")) Then Name = RS("Name")
                    If Not IsDBNull(RS("Diameter")) Then Diameter = RS("Diameter")
                    If Not IsDBNull(RS("Weight")) Then Weight = RS("Weight")
                    If Not IsDBNull(RS("Sec_Den")) Then SectionalDensity = RS("Sec_Den")
                    If Not IsDBNull(RS("Part_number")) Then PartNumber = RS("Part_number")
                    If Not IsDBNull(RS("Ballistic_Coefficient")) Then Ballistic_Coefficient = RS("Ballistic_Coefficient")
                    lQty = RS("Qty")
                    bullet_type = GetAmmoType(RS("bullet_type"))
                    ePPB = CDbl(RS("ePPB"))
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Call LogError("LoadBulletInfo", Err.Number, ex.Message.ToString())
            End Try
        End Sub
        ''' <summary>
        '''  Get Primer Information from the PrimerID, to optional get the Manufacturer, Name
        '''  Primer Type, Estimated Price Per Primer and current Quantity.
        ''' </summary>
        ''' <returns></returns>
        Public Sub LoadPrimerInfo(ByVal lngID As Long, Optional ByRef Manufacturer As String = "", _
                    Optional ByRef Name As String = "", Optional ByRef Primer_Type As String = "", _
                    Optional ByRef ePPP As Double = 0, Optional ByRef lQty As Long = 0)
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT * from General_Primer where ID=" & lngID
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    If Not IsDBNull(RS("Manufacturer")) Then Manufacturer = RS("Manufacturer")
                    If Not IsDBNull(RS("Name")) Then Name = RS("Name")
                    Primer_Type = GetPrimerType(RS("Primer_Type"))
                    ePPP = CDbl(RS("ePPP"))
                    lQty = RS("Qty")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Call LogError("LoadPrimerInfo", Err.Number, ex.Message.ToString())
            End Try
        End Sub
        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Public Sub LoadCaseInfo(ByVal lngID As Long, Optional ByRef Manufacturer As String = "", _
                                Optional ByRef Name As String = "", Optional ByRef Trim2length As String = "", _
                                Optional ByRef TimesUsed As String = "0", Optional ByRef lQty As Long = 0, _
                                Optional ByRef ePPC As Double = 0)
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT * from List_Case where ID=" & lngID
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                Dim intNew As Integer = 0
                While RS.Read
                    If Not IsDBNull(RS("Manufacturer")) Then Manufacturer = RS("Manufacturer")
                    If Not IsDBNull(RS("Name")) Then Name = RS("Name")
                    If Not IsDBNull(RS("ttl")) Then Trim2length = RS("ttl")
                    If Not IsDBNull(RS("TimesUsed")) Then TimesUsed = RS("TimesUsed")
                    lQty = RS("Qty")
                    ePPC = CDbl(RS("ePPC"))
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Call LogError("LoadCaseInfo", Err.Number, ex.Message.ToString())
            End Try
        End Sub
        ''' <summary>
        '''  Get the Shull/shotgun Case information by passing the Case ID and optionally getting the Manufacturer,
        '''  Name, Trim to Length, Current Qty, Estimated Price Per Case, and DRAM
        ''' </summary>
        ''' <returns></returns>
        Public Sub LoadHullInfo(ByVal lngID As Long, Optional ByRef Manufacturer As String = "", _
                               Optional ByRef Name As String = "", Optional ByRef Trim2length As String = "", _
                               Optional ByRef lQty As Long = 0, _
                               Optional ByRef ePPC As Double = 0, _
                               Optional ByRef DRAM As String = "")
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT * from List_SG_Case where ID=" & lngID
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                Dim intNew As Integer = 0
                While RS.Read
                    If Not IsDBNull(RS("Manufacturer")) Then Manufacturer = RS("Manufacturer")
                    If Not IsDBNull(RS("Name")) Then Name = RS("Name")
                    If Not IsDBNull(RS("Length")) Then Trim2length = RS("Length")
                    If Not IsDBNull(RS("DRAM")) Then DRAM = RS("DRAM")
                    lQty = RS("Qty")
                    ePPC = CDbl(RS("epps"))
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Call LogError("LoadHullInfo", Err.Number, ex.Message.ToString())
            End Try
        End Sub
        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Public Sub LoadWADInfo(ByVal lngID As Long, Optional ByRef Manufacturer As String = "", _
                               Optional ByRef Name As String = "", Optional ByRef MaxLoad As String = "", _
                                Optional ByRef dMaxLoad As Double = 0, Optional ByRef lQty As Long = 0, _
                               Optional ByRef ePPC As Double = 0)
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT * from List_SG_WAD where ID=" & lngID
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                Dim intNew As Integer = 0
                While RS.Read
                    If Not IsDBNull(RS("Manufacturer")) Then Manufacturer = RS("Manufacturer")
                    If Not IsDBNull(RS("WAD")) Then Name = RS("WAD")
                    If Not IsDBNull(RS("Load_t")) Then MaxLoad = RS("Load_t")
                    If Not IsDBNull(RS("Load_d")) Then dMaxLoad = RS("Load_d")
                    lQty = RS("Qty")
                    ePPC = CDbl(RS("eppw"))
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Call LogError("LoadWADInfo", Err.Number, ex.Message.ToString())
            End Try
        End Sub
        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Sub LoadSG_ShotType_Details(ByVal lngID As Long, Optional ByRef Manufacturer As String = "", _
                               Optional ByRef Name As String = "", Optional ByRef IsSlug As Boolean = False, _
                                Optional ByRef material As String = "", Optional ByRef ShotNo As String = "", _
                               Optional ByRef weight As String = "", Optional ByRef CAL As String = "", _
                               Optional ByRef Qty As Double = 0, Optional ByRef epps As Double = 0, _
                               Optional ByRef Price As Double = 0, Optional ByRef ounces As Double = 0, _
                               Optional ByRef grams As Double = 0)
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT * from List_SG_ShotType_Details where ID=" & lngID
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                Dim intNew As Integer = 0
                Dim iSlug As Integer = 0
                While RS.Read
                    If Not IsDBNull(RS("Manufacturer")) Then Manufacturer = RS("Manufacturer")
                    If Not IsDBNull(RS("Name")) Then Name = RS("Name")
                    If Not IsDBNull(RS("IsSlug")) Then iSlug = RS("IsSlug")
                    If iSlug = 1 Then
                        IsSlug = True
                    Else
                        IsSlug = False
                    End If
                    If Not IsDBNull(RS("mat")) Then material = RS("mat")
                    If Not IsDBNull(RS("ShotNo")) Then ShotNo = RS("ShotNo")
                    If Not IsDBNull(RS("mat")) Then material = RS("mat")
                    If Not IsDBNull(RS("weight")) Then weight = RS("weight")
                    If Not IsDBNull(RS("CAL")) Then CAL = RS("CAL")
                    If Not IsDBNull(RS("Price")) Then Price = RS("Price")
                    If Not IsDBNull(RS("ounces")) Then ounces = RS("ounces")
                    'If Not IsDBNull(RS("grams")) Then grams = RS("grams")
                    'TODO: Error Occured on the list above, check to see if this is needed.
                    If IsSlug Then If Not IsDBNull(RS("Qty")) Then Qty = RS("Qty")
                    If Not IsDBNull(RS("epps")) Then
                        epps = CDbl(RS("epps"))
                    Else
                        epps = 0
                    End If

                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Call LogError("LoadSG_ShotType_Details", Err.Number, ex.Message.ToString())
            End Try
        End Sub
        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Sub LoadConfig(ByVal configid As Long, Optional ByRef IsPersonal As Boolean = False, _
                                Optional ByRef IsShotGun As Boolean = False, Optional ByRef strNotes As String = "", _
                                Optional ByRef IsActive As Boolean = False, Optional ByRef IsFav As Boolean = False)
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT * from Config_List_Name where ID=" & configid
                Call Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    If RS("IsPersonal") = 1 Then IsPersonal = True
                    If RS("IsShotGun") = 1 Then IsShotGun = True
                    If RS("IsActive") = 1 Then IsActive = True
                    If RS("IsFav") = 1 Then IsFav = True
                    If Not IsDBNull(RS("Notes")) Then strNotes = Trim(RS("Notes"))
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
            Catch ex As Exception
                Call LogError("LoadConfig", Err.Number, ex.Message.ToString())
            End Try
        End Sub
        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Public Function ConvertToDollars(ByVal dValue As Double) As Double
            Dim dAns As Double = 0
            dAns = Math.Round(dValue, 2)
            Return dAns
        End Function
        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Public Sub ARUNSG_UpdateInventoryQty(ByVal lQty As Long, ByVal lQBullet As Long, ByVal BID As Long, ByVal lQPrimer As Long, _
                    ByVal PRID As Long, ByVal lQCase As Long, ByVal CID As Long, ByVal dQPowder As Double, ByVal PPID As Long, ByVal dGrainsUses As Double)
            Try
                Dim lNewBullet As Long = lQBullet - lQty
                Dim lNewCase As Long = lQCase - lQty
                Dim lNewPrimer As Long = lQPrimer - lQty
                Dim lNewPowder As Double = dQPowder - (dGrainsUses * lQty)
                Dim dPounds As Double = Math.Round(lNewPowder / WEIGHT_GRAINS_1LBS, 3)
                Dim Obj As New BSDatabase
                Dim SQL As String = "UPDATE List_Bullets set Qty=" & lNewBullet & " where ID=" & BID
                Obj.ConnExec(SQL)
                SQL = "UPDATE List_Case set Qty=" & lNewCase & " where id=" & CID
                Obj.ConnExec(SQL)
                SQL = "UPDATE General_Primer set Qty=" & lNewPrimer & " where id=" & PRID
                Obj.ConnExec(SQL)
                SQL = "UPDATE General_Powder set weightgn=" & lNewPowder & ",weightlbs=" & dPounds & " where id=" & PPID
                Obj.ConnExec(SQL)
            Catch ex As Exception
                Call LogError("ARUNSG_UpdateInventoryQty", Err.Number, ex.Message.ToString())
            End Try
        End Sub
        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Public Sub ARUSG_UpdateInventoryQty(ByVal lQty As Long, ByVal lQBullet As Long, ByVal BID As Long, ByVal lQPrimer As Long, _
                    ByVal PRID As Long, ByVal lQCase As Long, ByVal CID As Long, ByVal dQPowder As Double, ByVal PPID As Long, _
                    ByVal dGrainsUses As Double, ByVal lQWADS As Long, ByVal WID As Long, ByVal IsSlug As Boolean, _
                    ByVal lQSHOT_OZ As Double, ByVal lQSHOT_GR As Double, ByVal dQPrefLoad As Double)
            Try
                Dim lNewBullet As Long = 0
                Dim lNewCase As Long = lQCase - lQty
                Dim lNewPrimer As Long = lQPrimer - lQty
                Dim lNewWad As Long = lQWADS - lQty
                Dim lNewPowder As Double = dQPowder - (dGrainsUses * lQty)
                Dim dPounds As Double = Math.Round(lNewPowder / WEIGHT_GRAINS_1LBS, 3)
                Dim dNewShotGrans As Double = 0
                Dim dNewShotOz As Double = 0
                Dim dNewShotLBS As Double = 0
                Dim Obj As New BSDatabase
                Dim SQL As String = ""
                If IsSlug Then
                    lNewBullet = lQBullet - lQty
                    SQL = "UPDATE List_SG_ShotType_Details set Qty=" & lNewBullet & " where ID=" & BID
                Else
                    dNewShotOz = lQSHOT_OZ - (dQPrefLoad * lQty)
                    dNewShotGrans = dNewShotOz * WEIGHT_GRAMS_OZ
                    dNewShotLBS = dNewShotOz / WEIGHT_OZ_1LBS
                    SQL = "UPDATE List_SG_ShotType_Details set weight=" & dNewShotLBS & _
                            ", ounces=" & dNewShotOz & ", grams=" & dNewShotGrans & " where ID=" & BID
                End If
                Obj.ConnExec(SQL)
                SQL = "UPDATE List_SG_WAD set Qty=" & lNewWad & " where id=" & WID
                Obj.ConnExec(SQL)
                SQL = "UPDATE List_SG_Case set Qty=" & lNewCase & " where id=" & CID
                Obj.ConnExec(SQL)
                SQL = "UPDATE General_Primer set Qty=" & lNewPrimer & " where id=" & PRID
                Obj.ConnExec(SQL)
                SQL = "UPDATE General_Powder set weightgn=" & lNewPowder & ",weightlbs=" & dPounds & " where id=" & PPID
                Obj.ConnExec(SQL)
            Catch ex As Exception
                Call LogError("ARUSG_UpdateInventoryQty", Err.Number, ex.Message.ToString())
            End Try
        End Sub
        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Public Function IsAlreadyListed(ByVal Manufacturer As String, ByVal Name As String, _
        ByVal Cal As String, ByVal Grain As String, ByVal Jacket As String, Optional ByRef Qty As Long = 0, Optional ByRef MID As Long = 0)
            Dim bAns As Boolean = False
            Try
                Dim Obj As New BSDatabase
                Obj.ConnectDB()
                Dim SQL As String = "SELECT * from Loaders_Log_Ammunition where Manufacturer='" & _
                            Manufacturer & "' and Name='" & Name & "' and Cal='" & Cal & _
                            "' and Grain='" & Grain & "' and Jacket='" & Jacket & "'"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                bAns = RS.HasRows
                While RS.Read
                    Qty = RS("qty")
                    MID = RS("ID")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
            Catch ex As Exception
                Call LogError("IsAlreadyListed", Err.Number, ex.Message.ToString())
            End Try
            Return bAns
        End Function
        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Public Sub GetPowderDetails(ByVal PID As Long, Optional ByRef Manu As String = "", _
                    Optional ByRef sName As String = "", Optional ByVal weightlbs As Double = 0, _
                    Optional ByRef weightgn As Double = 0, Optional ByRef Price As Double = 0, _
                    Optional ByRef sNotes As String = "", Optional ByRef ePPP As Double = 0)
            Try
                Dim Obj As New BSDatabase
                Dim SQL As String = "SELECT * from General_Powder where ID=" & PID
                Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    If Not IsDBNull(RS("Manufacturer")) Then Manu = RS("Manufacturer")
                    If Not IsDBNull(RS("Name")) Then sName = RS("Name")
                    If Not IsDBNull(RS("weightlbs")) Then weightlbs = RS("weightlbs")
                    If Not IsDBNull(RS("weightgn")) Then weightgn = RS("weightgn")
                    If Not IsDBNull(RS("Price")) Then Price = RS("Price")
                    If Not IsDBNull(RS("Notes")) Then sNotes = RS("Notes")
                    If Not IsDBNull(RS("ePPP")) Then ePPP = RS("ePPP")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
            Catch ex As Exception
                Call LogError("GetPowderDetails", Err.Number, ex.Message.ToString())
            End Try
        End Sub
    End Class
    Public Class BSAppConfig
        Private _DefaultDBName As String
        Private _DefaultUserConfig As System.Configuration.ConfigurationUserLevel
        Public Property DefaultDBName() As String
            Get
                Return DATABASE_NAME
            End Get
            Set(ByVal value As String)
                _DefaultDBName = value
            End Set
        End Property
        Public Property DefaultUserConfig() As System.Configuration.ConfigurationUserLevel
            Get
                Return ConfigurationUserLevel.None
            End Get
            Set(ByVal value As System.Configuration.ConfigurationUserLevel)
                _DefaultUserConfig = value
            End Set
        End Property
        Sub RemoveAppSetting(ByVal sKey As String)
            Try
                Dim Config As Configuration = ConfigurationManager.OpenExeConfiguration(DefaultUserConfig)
                Config.AppSettings.Settings.Remove(sKey)
                Config.Save(ConfigurationSaveMode.Minimal)
            Catch ex As Exception
                Dim strform As String = "BSAppConfig"
                Dim strProcedure As String = "RemoveAppSetting"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Sub
        Sub SaveAppSetting(ByVal sKey As String, ByVal sValue As String)
            Try
                Dim Config As Configuration = ConfigurationManager.OpenExeConfiguration(DefaultUserConfig)
                Config.AppSettings.Settings.Add(sKey, sValue)
                Config.Save(ConfigurationSaveMode.Minimal)
                ConfigurationManager.RefreshSection("AppSettings")
            Catch ex As Exception
                Dim strform As String = "BSAppConfig"
                Dim strProcedure As String = "SaveAppSetting"
                Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
                Dim sMessage As String = strform & "." & strProcedure & "::" & Err.Number & "::" & ex.Message.ToString()
                ObjFS.LogFile(MyLogFile, sMessage)
            End Try
        End Sub
        Function GetAppSetting(ByVal sKey As String) As String
            Dim sAns As String = ""
            sAns = System.Configuration.ConfigurationManager.AppSettings(sKey)
            Return sAns
        End Function
        Sub UpdateAppSetting(ByVal sKey As String, ByVal sValue As String)
            Call RemoveAppSetting(sKey)
            Call SaveAppSetting(sKey, sValue)
        End Sub
        Public Sub UpDateAppDetails()
            Dim playsettings As String = GetAppSetting("appname")
            Call UpdateAppSetting("Version", Application.ProductVersion)
            Dim sversion As String = GetAppSetting("Version")
            Call UpdateAppSetting("AppName", Application.ProductName)
            Call UpdateAppSetting("AppEXE", Application.ExecutablePath())
            Call UpdateAppSetting("Path", APPLICATION_PATH)
            Call UpdateAppSetting("LogPath", MyLogFile)
            Call UpdateAppSetting("DataBase", APPLICATION_PATH_DATA & "\" & DefaultDBName)
        End Sub
    End Class
    Public Class ViewSizeSettings
        Sub LoadView_Configuration_Shotgun_Sheet(ByRef height As Long, ByRef width As Long, ByVal location As System.Drawing.Point)
            If My.Settings.View_Configuration_Shotgun_Sheet_Width.Length > 0 And My.Settings.View_Configuration_Shotgun_Sheet_Height.Length > 0 Then
                height = My.Settings.View_Configuration_Shotgun_Sheet_Height
                width = My.Settings.View_Configuration_Shotgun_Sheet_Width
            End If
            If My.Settings.View_Configuration_Shotgun_Sheet_X.Length > 0 And My.Settings.View_Configuration_Shotgun_Sheet_Y.Length > 0 Then
                location = New System.Drawing.Point(My.Settings.View_Configuration_Shotgun_Sheet_X, My.Settings.View_Configuration_Shotgun_Sheet_Y)
            End If
        End Sub

        Sub SaveView_Configuration_Shotgun_Sheet(ByVal height As Long, ByVal width As Long, ByVal x As Long, ByVal y As Long)
            My.Settings.View_Configuration_Shotgun_Sheet_Height = height
            My.Settings.View_Configuration_Shotgun_Sheet_Width = width
            My.Settings.View_Configuration_Shotgun_Sheet_X = x
            My.Settings.View_Configuration_Shotgun_Sheet_Y = y
            My.Settings.Save()
        End Sub

        Sub SaveView_Configuration_Sheet(ByVal height As Long, ByVal width As Long, ByVal x As Long, ByVal y As Long)
            My.Settings.View_Configuration_Sheet_Height = height
            My.Settings.View_Configuration_Sheet_Width = width
            My.Settings.View_Configuration_Sheet_X = x
            My.Settings.View_Configuration_Sheet_Y = y
            My.Settings.Save()
        End Sub
        Sub LoadView_Configuration_Sheet(ByRef height As Long, ByRef width As Long, ByVal location As System.Drawing.Point)
            If My.Settings.View_Configuration_Sheet_Width.Length > 0 And My.Settings.View_Configuration_Sheet_Height.Length > 0 Then
                height = My.Settings.View_Configuration_Sheet_Height
                width = My.Settings.View_Configuration_Sheet_Width
            End If
            If My.Settings.View_Configuration_Sheet_X.Length > 0 And My.Settings.View_Configuration_Sheet_Y.Length > 0 Then
                location = New System.Drawing.Point(My.Settings.View_Configuration_Sheet_X, My.Settings.View_Configuration_Sheet_Y)
            End If
        End Sub
        Sub SaveBushings_Powder_List(ByVal height As Long, ByVal width As Long, ByVal x As Long, ByVal y As Long)
            My.Settings.View_Bushings_Powder_List_Height = height
            My.Settings.View_Bushings_Powder_List_Width = width
            My.Settings.View_Bushings_Powder_List_X = x
            My.Settings.View_Bushings_Powder_List_Y = y
            My.Settings.Save()
        End Sub
        Sub LoadBushings_Powder_List(ByRef height As Long, ByRef width As Long, ByVal location As System.Drawing.Point)
            If My.Settings.View_Bushings_Powder_List_Width.Length > 0 And My.Settings.View_Bushings_Powder_List_Height.Length > 0 Then
                height = My.Settings.View_Bushings_Powder_List_Height
                width = My.Settings.View_Bushings_Powder_List_Width
            End If
            If My.Settings.View_Bushings_Powder_List_X.Length > 0 And My.Settings.View_Bushings_Powder_List_Y.Length > 0 Then
                location = New System.Drawing.Point(My.Settings.View_Bushings_Powder_List_X, My.Settings.View_Bushings_Powder_List_Y)
            End If
        End Sub
        'View_Bushings_Powder
        Sub SaveBushings_Shot_List(ByVal height As Long, ByVal width As Long, ByVal x As Long, ByVal y As Long)
            My.Settings.View_Bushings_Shot_List_Height = height
            My.Settings.View_Bushings_Shot_List_Width = width
            My.Settings.View_Bushings_Shot_List_X = x
            My.Settings.View_Bushings_Shot_List_Y = y
            My.Settings.Save()
        End Sub
        Sub LoadBushings_Shot_List(ByRef height As Long, ByRef width As Long, ByVal location As System.Drawing.Point)
            If My.Settings.View_Bushings_Shot_List_Width.Length > 0 And My.Settings.View_Bushings_Shot_List_Height.Length > 0 Then
                height = My.Settings.View_Bushings_Shot_List_Height
                width = My.Settings.View_Bushings_Shot_List_Width
            End If
            If My.Settings.View_Bushings_Shot_List_X.Length > 0 And My.Settings.View_Bushings_Shot_List_Y.Length > 0 Then
                location = New System.Drawing.Point(My.Settings.View_Bushings_Shot_List_X, My.Settings.View_Bushings_Shot_List_Y)
            End If
        End Sub
    End Class
    Public Class Inventory_Export_Import
        Private Function GetXMLNode(ByVal instance As XmlNode) As String
            Dim sAns As String = ""
            On Error Resume Next
            sAns = instance.InnerText
            Return sAns
        End Function
        Sub XML_Generate_Powder_Import(ByVal sPath As String)
            Dim doc As New XmlDocument
            Dim ObjGF As New GlobalFunctions
            Dim obj As New BSDatabase
            Dim i As Integer = 0
            doc.Load(sPath)
            Dim sNodeName As String = "General_Powder"
            Dim SQL As String = ""
            Dim Manufacturer As String = ""
            Dim sName As String = ""
            Dim weightlbs As Double = 0
            Dim weightgn As Double = 0
            Dim price As Double = 0
            Dim notes As String = ""
            Dim eppp As Double = 0
            Dim elemlist As XmlNodeList = doc.GetElementsByTagName(sNodeName)
            For i = 0 To elemlist.Count - 1
                Manufacturer = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("Manufacturer")))
                sName = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("Name")))
                weightlbs = CDbl(ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("weightlbs"))))
                weightgn = CDbl(ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("weightgn"))))
                price = CDbl(ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("price"))))
                notes = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("Notes")))
                eppp = CDbl(ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("ePPP"))))
                SQL = "INSERT INTO General_Powder (Manufacturer,Name,weightlbs,weightgn,Price,Notes,ePPP,sync_lastupdate) VALUES('" & _
                        Manufacturer & "','" & sName & "'," & weightlbs & "," & weightgn & "," & _
                        price & ",'" & notes & "'," & eppp & ",Now())"
                If Not ObjGF.ObjectExistsinDB(Manufacturer, "Manufacturer", sNodeName) Or Not ObjGF.ObjectExistsinDB(sName, "Name", sNodeName) Then
                    obj.ConnExec(SQL)
                End If
            Next
            elemlist = Nothing
            doc = Nothing
        End Sub
        Sub XML_Generate_Powder_Export(ByVal strPath As String)
            Try
                Dim sAns As String = ""
                Dim NL As String = Chr(10) & Chr(13)
                Dim Obj As New BSDatabase
                Obj.ConnectDB()
                Dim SQL As String = "SELECT Manufacturer,Name,weightlbs,weightgn,Price,Notes,ePPP from General_Powder"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                sAns = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
                sAns &= "<Inventory>" & NL
                While RS.Read
                    sAns &= "   <General_Powder>" & NL
                    sAns &= "       <Manufacturer>" & RS("Manufacturer") & "</Manufacturer>" & NL
                    sAns &= "       <Name>" & RS("Name") & "</Name>" & NL
                    sAns &= "       <weightlbs>" & RS("weightlbs") & "</weightlbs>" & NL
                    sAns &= "       <weightgn>" & RS("weightgn") & "</weightgn>" & NL
                    sAns &= "       <Price>" & RS("Price") & "</Price>" & NL
                    sAns &= "       <Notes>" & RS("Notes") & "</Notes>" & NL
                    sAns &= "       <ePPP>" & RS("ePPP") & "</ePPP>" & NL
                    sAns &= "   </General_Powder>" & NL
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
                Obj = Nothing
                sAns &= "</Inventory>" & NL
                sAns = Replace(sAns, "&", "&amp;")
                Dim ObjFS As New BSFileSystem
                ObjFS.DeleteFile(strPath)
                ObjFS.OutPutToFile(strPath, sAns)
                MsgBox("Config was exported to " & Chr(10) & strPath)
            Catch ex As Exception
                Call LogError("LoadersClass.Inventory_Export_Import", "XML_Generate_Powder_Export", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Sub XML_Generate_Bushings_Powder_Import(ByVal sPath As String)
            Dim doc As New XmlDocument
            Dim ObjGF As New GlobalFunctions
            Dim obj As New BSDatabase
            Dim i As Integer = 0
            doc.Load(sPath)
            Dim sNodeName As String = "List_SG_Bushing_Powder"
            Dim SQL As String = ""
            Dim Manufacturer As String = ""
            Dim sName As String = ""
            Dim sCharge As String = ""
            Dim sType As String = ""
            Dim PowderName As String = ""
            Dim elemlist As XmlNodeList = doc.GetElementsByTagName(sNodeName)
            For i = 0 To elemlist.Count - 1
                Manufacturer = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("Manufacturer")))
                sName = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("sName")))
                sCharge = ObjGF.FormatForXML(GetXMLNode(elemlist(i).Item("sCharge")))
                sType = ObjGF.FormatForXML(GetXMLNode(elemlist(i).Item("sType")))
                PowderName = ObjGF.FormatForXML(GetXMLNode(elemlist(i).Item("PowderName")))
                SQL = "INSERT INTO List_SG_Bushing_Powder (Manufacturer,sName,sCharge,sType,PowderName,sync_lastupdate) VALUES('" & _
                        Manufacturer & "','" & sName & "','" & sCharge & "','" & sType & "','" & _
                        PowderName & "',Now())"
                If Not ObjGF.ObjectExistsinDB(Manufacturer, "Manufacturer", sNodeName) _
                    Or Not ObjGF.ObjectExistsinDB(sName, "sName", sNodeName) _
                    Or Not ObjGF.ObjectExistsinDB(sCharge, "sCharge", sNodeName) _
                    Or Not ObjGF.ObjectExistsinDB(PowderName, "PowderName", sNodeName) Then
                    obj.ConnExec(SQL)
                End If
            Next
            elemlist = Nothing
            doc = Nothing
        End Sub
        Sub XML_Generate_Bushings_Powder_Export(ByVal strPath As String)
            Try
                Dim sAns As String = ""
                Dim NL As String = Chr(10) & Chr(13)
                Dim Obj As New BSDatabase
                Obj.ConnectDB()
                Dim SQL As String = "SELECT Manufacturer,sName,sCharge,sType,PowderName from List_SG_Bushing_Powder"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                sAns = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
                sAns &= "<Inventory>" & NL
                While RS.Read
                    sAns &= "   <List_SG_Bushing_Powder>" & NL
                    sAns &= "       <Manufacturer>" & RS("Manufacturer") & "</Manufacturer>" & NL
                    sAns &= "       <sName>" & RS("sName") & "</sName>" & NL
                    sAns &= "       <sCharge>" & RS("sCharge") & "</sCharge>" & NL
                    sAns &= "       <sType>" & RS("sType") & "</sType>" & NL
                    sAns &= "       <PowderName>" & RS("PowderName") & "</PowderName>" & NL
                    sAns &= "   </List_SG_Bushing_Powder>" & NL
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
                Obj = Nothing
                sAns &= "</Inventory>" & NL
                sAns = Replace(sAns, "&", "&amp;")
                Dim ObjFS As New BSFileSystem
                ObjFS.DeleteFile(strPath)
                ObjFS.OutPutToFile(strPath, sAns)
                MsgBox("Config was exported to " & Chr(10) & strPath)
            Catch ex As Exception
                Call LogError("LoadersClass.Inventory_Export_Import", "XML_Generate_Bushings_Powder_Export", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Sub XML_Generate_Bushings_Shot_Import(ByVal sPath As String)
            Dim doc As New XmlDocument
            Dim ObjGF As New GlobalFunctions
            Dim obj As New BSDatabase
            Dim i As Integer = 0
            doc.Load(sPath)
            Dim sNodeName As String = "List_SG_Bushing_Shot"
            Dim SQL As String = ""
            Dim Manufacturer As String = ""
            Dim sName As String = ""
            Dim sCharge As String = ""
            Dim sType As String = ""
            Dim elemlist As XmlNodeList = doc.GetElementsByTagName(sNodeName)
            For i = 0 To elemlist.Count - 1
                Manufacturer = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("Manufacturer")))
                sName = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("sName")))
                sCharge = ObjGF.FormatForXML(GetXMLNode(elemlist(i).Item("sCharge")))
                sType = ObjGF.FormatForXML(GetXMLNode(elemlist(i).Item("sType")))
                SQL = "INSERT INTO List_SG_Bushing_Shot (Manufacturer,sName,sCharge,sType,sync_lastupdate) VALUES('" & _
                        Manufacturer & "','" & sName & "','" & sCharge & "','" & sType & "',Now())"
                If Not ObjGF.ObjectExistsinDB(Manufacturer, "Manufacturer", sNodeName) _
                    Or Not ObjGF.ObjectExistsinDB(sName, "sName", sNodeName) _
                    Or Not ObjGF.ObjectExistsinDB(sCharge, "sCharge", sNodeName) Then
                    obj.ConnExec(SQL)
                End If
            Next
            elemlist = Nothing
            doc = Nothing
        End Sub
        Sub XML_Generate_Bushings_Shot_Export(ByVal strPath As String)
            Try
                Dim sAns As String = ""
                Dim NL As String = Chr(10) & Chr(13)
                Dim Obj As New BSDatabase
                Obj.ConnectDB()
                Dim SQL As String = "SELECT Manufacturer,sName,sCharge,sType from List_SG_Bushing_Shot"
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                sAns = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
                sAns &= "<Inventory>" & NL
                While RS.Read
                    sAns &= "   <List_SG_Bushing_Shot>" & NL
                    sAns &= "       <Manufacturer>" & RS("Manufacturer") & "</Manufacturer>" & NL
                    sAns &= "       <sName>" & RS("sName") & "</sName>" & NL
                    sAns &= "       <sCharge>" & RS("sCharge") & "</sCharge>" & NL
                    sAns &= "       <sType>" & RS("sType") & "</sType>" & NL
                    sAns &= "   </List_SG_Bushing_Shot>" & NL
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
                Obj = Nothing
                sAns &= "</Inventory>" & NL
                sAns = Replace(sAns, "&", "&amp;")
                Dim ObjFS As New BSFileSystem
                ObjFS.DeleteFile(strPath)
                ObjFS.OutPutToFile(strPath, sAns)
                MsgBox("Config was exported to " & Chr(10) & strPath)
            Catch ex As Exception
                Call LogError("LoadersClass.Inventory_Export_Import", "XML_Generate_Bushings_Shot_Export", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End Namespace
