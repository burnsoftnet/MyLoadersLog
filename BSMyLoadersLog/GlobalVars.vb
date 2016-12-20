Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Module GlobalVars
    Public OwnerID As String
    Public OwnerName As String
    Public OwnerLoadName As String
    Public UseLogin As Boolean
    Public UseMyPWD As String
    Public UseMyUID As String
    Public UseMyForgotWord As String
    Public UseMyForgotPhrase As String
    Public IsLoggedIN As Boolean
    Public MyLogFile As String
    Public DoAutoBackup As Boolean
    Public DoOriginalImage As Boolean
    Public UsePetLoads As Boolean
    Public PersonalMark As Boolean
    Public LOADERTYPE_SHOTGUN As Boolean
    Public LOADERTYPE_NONSHOTGUN As Boolean
    Public DEFAULTLIST As String
    Public MGCPath As String
    Public LastSucBackup As String
    Public AlertOnBackUp As Boolean
    Public TrackHistoryDays As Integer
    Public TrackHistory As Boolean
    Public VIEW_CUPS As Boolean
    Public VIEW_FPS As Boolean
    Public LASTCONFIGEDVIEWED As Long
    Public APPLICATION_PATH As String
    Public APPLICATION_PATH_DATA As String
    Public Const MY_HELP_FILE = "my_loaders_log_help.chm"
    Public Const MY_HOTFIX_FILE = "BSMLL_HotFixes.exe"
    'Public Const MY_UPDATER = "BSApplicationUpdater.exe"
    Public Const MY_BACKUP = "DBBackup.exe"
    Public Const MY_RESTORE = "DBRestore.exe"
    'Public Const MENU_FORUM = "http://mllforum.burnsoft.net"
    Public Const MENU_WIKI = "http://wiki.burnsoft.net/AllPages.aspx?Cat=My%20Loaders%20Log"
    Public Const MENU_SHOP = "http://shopping.burnsoft.net"
    Public Const MENU_BUG = "http://bugreport.burnsoft.net"
    Public Const MENU_SUPPORT = "http://support.burnsoft.net"
    Public Const MENU_SITESEARCH = "http://www.burnsoft.net/Search_Site.aspx"
    Public Const MENU_LINKS = "http://wiki.burnsoft.net/Links_Firearm_reloading.ashx"
    Public Const WEIGHT_GRAINS_1LBS = 6999.99
    Public Const WEIGHT_GRAINS_1GM = 15.4323
    Public Const WEIGHT_GRAMS_1LBS = 453.592
    Public Const WEIGHT_OZ_1LBS = 16
    Public Const WEIGHT_GRAMS_OZ = 28.35
    Public Const DATABASE_NAME = "MLL.mdb"
    Public Const USE_SHOTGUN As Boolean = True
    Public Enum WeightType
        Pounds
        Grams
        Grains
    End Enum
    Public Function ConvToNum(ByVal strValue As String) As Double
        Dim dAns As Double = 0
        Try
            Dim intChar As Integer = Len(strValue)
            Dim i As Integer = 0
            Dim CurValue As String = ""
            Dim NewValue As String = ""
            Dim LastValue As String = ""
            Dim NeedDiv As Boolean = False
            For i = 1 To intChar
                CurValue = Mid(strValue, i, 1)
                If CurValue = " " Then Exit For
                If IsNumeric(CurValue) Then
                    If Len(LastValue) <> 0 Then
                        LastValue = Mid(NewValue, Len(NewValue), 1)
                    Else
                        LastValue = CurValue
                    End If
                    If Not NeedDiv Then
                        NewValue &= CurValue
                    Else
                        NewValue = CInt(CurValue) / CInt(LastValue)
                    End If
                    NeedDiv = False
                Else
                    Select Case CurValue
                        Case "."
                            NewValue &= CurValue
                            NeedDiv = False
                        Case "/"
                            NeedDiv = True
                    End Select
                End If
            Next
            dAns = CDbl(NewValue)
        Catch ex As Exception
            Dim strform As String = "GlobalVars"
            Dim strProcedure As String = "ConvertToNum"
            Call LogError(strform, strProcedure, Err.Number, ex.Message.ToString)
        End Try
        Return dAns
    End Function
    Public Function ConvertOZToDouble(ByVal sValue As String) As Double
        Dim dAns As Double = 0
        Try
            Dim char_count As Integer = Len(sValue)
            Dim i As Integer = 0
            Dim CurValue As String = ""
            Dim NewValue As String = ""
            Dim LastValue As String = ""
            Dim EndValue As String = "0"
            Dim NeedDiv As Boolean = False
            Dim isFraction As Boolean = False
            Dim IsDec As Boolean = False
            For i = 1 To char_count
                CurValue = Mid(sValue, i, 1)
                isFraction = False
                If IsNumeric(CurValue) Then
                    If Not NeedDiv Then
                        NewValue = CurValue
                        IsDec = False
                    Else
                        NewValue = CInt(LastValue) / CInt(CurValue)
                        IsDec = True
                    End If
                    NeedDiv = False
                    LastValue = CurValue
                Else
                    Select Case CurValue
                        Case "."
                            NewValue &= CurValue
                            NeedDiv = False
                        Case "/"
                            NeedDiv = True
                        Case Else
                            NewValue = ""
                    End Select
                End If
                If Mid(sValue, i + 1, 1) = "/" Then isFraction = True
                If Not isFraction And Not NeedDiv Then
                    If Not IsDec Then
                        EndValue &= NewValue
                    Else
                        EndValue = CDbl(EndValue) + CDbl(NewValue)
                    End If
                End If
            Next
            dAns = CDbl(EndValue)
        Catch ex As Exception
            Dim strform As String = "GlobalVars"
            Dim strProcedure As String = "ConvertOZToDouble"
            Call LogError(strform, strProcedure, Err.Number, ex.Message.ToString)
        End Try
        Return dAns
    End Function
    Public Function UnFluffContent(ByVal strContent As String) As String
        Dim sAns As String = ""
        sAns = Trim(Replace(strContent, "''", "'"))
        If Len(sAns) = 0 Then
            sAns = ""
        End If
        Return sAns
    End Function
    Public Function FluffContent(ByVal strContent As String, Optional ByVal default_value As String = "   ") As String
        Dim sAns As String = ""
        sAns = Trim(Replace(strContent, "'", "''"))
        If Len(sAns) = 0 Then
            sAns = default_value
        End If
        Return sAns
    End Function
    Public Function FluffContent(ByVal strContent As String, ByVal lDefault As Double) As Double
        Dim sAns As Double = 0
        If Len(strContent) = 0 Then
            sAns = lDefault
        Else
            sAns = CDbl(strContent)
        End If
        Return sAns
    End Function
    Public Function IsRequired(ByVal strValue As String, ByVal strField As String, ByVal StrTitle As String) As Boolean
        Dim bAns As Boolean = False
        If Len(Trim(strValue)) = 0 Then
            bAns = False
        Else
            bAns = True
        End If
        If bAns = False Then MsgBox("Please put in a value for " & strField & "!", MsgBoxStyle.Critical, StrTitle)
        Return bAns
    End Function
    Public Function IsRequired(ByVal lValue As Long, ByVal lDefault As Long, ByVal strField As String, ByVal StrTitle As String) As Boolean
        Dim bAns As Boolean = False
        If lValue = lDefault Then
            bAns = False
        Else
            bAns = True
        End If
        If bAns = False Then MsgBox("Please put in a value for " & strField & "!", MsgBoxStyle.Critical, StrTitle)
        Return bAns
    End Function
    Public Function IsRequired(ByVal lValue As Double, ByVal lDefault As Double, ByVal strField As String, ByVal StrTitle As String) As Boolean
        Dim bAns As Boolean = False
        If lValue = lDefault Then
            bAns = False
        Else
            bAns = True
        End If
        If bAns = False Then MsgBox("Please put in a value for " & strField & "!", MsgBoxStyle.Critical, StrTitle)
        Return bAns
    End Function
    Public Function ConvertWeight(ByVal Value As Double, ByVal ConvertTo As WeightType, ByVal ConvertFrom As WeightType) As Double
        Dim dAns As Double = 0
        Select Case ConvertTo
            Case WeightType.Pounds
                If ConvertFrom = WeightType.Grains Then
                    dAns = Value / WEIGHT_GRAINS_1LBS
                ElseIf ConvertFrom = WeightType.Grams Then
                    dAns = Value / WEIGHT_GRAMS_1LBS
                End If
            Case WeightType.Grams
                If ConvertFrom = WeightType.Pounds Then
                    dAns = Value * WEIGHT_GRAMS_1LBS
                ElseIf ConvertFrom = WeightType.Grains Then
                    dAns = Value / WEIGHT_GRAINS_1GM
                End If
            Case WeightType.Grains
                If ConvertFrom = WeightType.Pounds Then
                    dAns = Value * WEIGHT_GRAINS_1LBS
                ElseIf ConvertFrom = WeightType.Grams Then
                    dAns = Value * WEIGHT_GRAINS_1GM
                End If
        End Select
        Return FormatNumber(dAns, 6)
    End Function
    Sub CheckforHotFix()
        Dim Objf As New BSFileSystem
        If Objf.FileExists(Application.StartupPath & "\hotfix.ini") Then
            Dim myProcess As New Process
            Dim RunThiSApp As String = Application.StartupPath & "\BSMLL_HotFixes.exe"
            myProcess.StartInfo.FileName = RunThiSApp
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            DoAutoBackup = False
            myProcess.Start()
            Global.System.Windows.Forms.Application.Exit()
        End If
    End Sub
    Public Function LoginEnabled(ByRef PWD As String, ByRef UID As String, ByRef FW As String, ByRef FP As String) As Boolean
        Dim bAns As Boolean = False
        Try
            Dim Obj As New BSDatabase
            Obj.ConnectDB()
            Dim SQL = "SELECT UseLock,Password,UserName,Password_Forgot_word,Password_Forgot from Personal_Information"
            Dim CMD As New Odbc.OdbcCommand(SQL, Obj.Conn)
            Dim RS As Odbc.OdbcDataReader
            RS = CMD.ExecuteReader
            If RS.HasRows Then
                Dim intUsePWD As Integer = CInt(RS("UseLock"))
                If intUsePWD = 1 Then
                    If Not IsDBNull(RS("Password")) Then
                        PWD = oEncrypt.DecryptSHA(RS("Password"))
                    Else
                        PWD = ""
                    End If
                    If Not IsDBNull(RS("UserName")) Then
                        UID = oEncrypt.DecryptSHA(RS("UserName"))
                    Else
                        UID = "admin"
                    End If
                    If Not IsDBNull(RS("Password_Forgot_word")) And Len(RS("Password_Forgot_word")) > 0 Then
                        FW = oEncrypt.DecryptSHA(RS("Password_Forgot_word"))
                    Else
                        FW = "burnsoft"
                    End If
                    If Not IsDBNull(RS("Password_Forgot")) And Len(RS("Password_Forgot")) > 0 Then
                        FP = oEncrypt.DecryptSHA(RS("Password_Forgot"))
                    Else
                        FP = "The Company that made this App"
                    End If
                    bAns = True
                Else
                    bAns = False
                    PWD = ""
                    UID = "admin"
                    FP = "The Company that made this App"
                    FW = "burnsoft"
                End If
            Else
                bAns = False
                PWD = ""
                UID = "admin"
                FP = "The Company that made this App"
                FW = "burnsoft"
            End If
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
            Obj = Nothing
        Catch ex As Exception
            Dim strform As String = "GlobalVars"
            Dim strProcedure As String = "LoginEnabled"
            Call LogError(strform, strProcedure, Err.Number, ex.Message.ToString)
        End Try
        Return bAns
    End Function
    Public Function GetOwnerID() As Integer
        Dim iAns As Integer = 0
        Try
            Dim Obj As New BSDatabase
            Obj.ConnectDB()
            Dim SQL As String = "SELECT Top 1 ID,Name from Personal_Information"
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            If RS.HasRows Then
                While (RS.Read)
                    iAns = RS("ID")
                    OwnerName = RS("Name")
                End While
            Else
                iAns = 0
                OwnerName = "Trial User"
            End If
            RS.Close()
            RS = Nothing
            Obj.CloseDB()
            Obj = Nothing
        Catch ex As Exception
            Dim strform As String = "GlobalVars"
            Dim strProcedure As String = "GetOwnerID"
            Call LogError(strform, strProcedure, Err.Number, ex.Message.ToString)
        End Try
        Return iAns
    End Function
    Public Function GetLoadName() As String
        Dim sAns As String = "My Loaders Log"
        Try
            If Len(OwnerLoadName) > 0 Then sAns = OwnerLoadName
            Dim Obj As New BSDatabase
            Dim SQL As String = "SELECT Load_Name from Personal_Information where ID=" & OwnerID
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                sAns = RS("Load_Name")
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Dim strform As String = "GlobalVars"
            Dim strProcedure As String = "GetLoadName"
            Call LogError(strform, strProcedure, Err.Number, ex.Message.ToString)
        End Try
        Return sAns
    End Function
    Public Function CostOf1RndOfAmmo(ByVal dPrimer As Double, ByVal dCase As Double, _
                                        ByVal dBullet As Double, ByVal dPowder As Double, _
                                        ByVal midPowder As Double) As Double
        Dim dAns As Double = 0
        Dim Obj As New InventoryMath
        dAns = ((Obj.ConvertToDollars(dPowder * midPowder)) + Obj.ConvertToDollars(dCase) + Obj.ConvertToDollars(dPrimer) + Obj.ConvertToDollars(dBullet))
        Return Obj.ConvertToDollars(dAns)
    End Function
    Public Function CostOf1RndOfAmmoSG(ByVal dPrimer As Double, ByVal dCase As Double, _
                                        ByVal dBullet As Double, ByVal dPowder As Double, _
                                        ByVal midPowder As Double, ByVal WAD As Double) As Double
        Dim dAns As Double = 0
        Dim Obj As New InventoryMath
        dAns = ((Obj.ConvertToDollars(dPowder * midPowder)) + Obj.ConvertToDollars(dCase) + Obj.ConvertToDollars(dPrimer) + Obj.ConvertToDollars(dBullet) + Obj.ConvertToDollars(WAD))
        Return Obj.ConvertToDollars(dAns)
    End Function
    Public Sub LogError(ByVal sForm As String, ByVal sProcedure As String, ByVal iErrNo As Long, ByVal sErrorDesc As String)
        Dim ObjFS As New BSMyLoadersLog.LoadersClass.BSFileSystem
        Dim sMessage As String = sForm & "." & sProcedure & "::" & iErrNo & "::" & sErrorDesc
        ObjFS.LogFile(MyLogFile, sMessage)
    End Sub

End Module
