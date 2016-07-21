Imports BSMLL_HotFixes
Module ModMain
    Sub INIT()
        Try
            SUPPRESS_CONSOLE_ERRORS = True
            Dim TempBool As Boolean = False
            TempBool = GetCommand("redo", "bool", ReDo)
            TempBool = GetCommand("debug", "bool", BUGGERME)
            SUPPRESS_CONSOLE_ERRORS = CBool(GetCommand("showerrors", "bool", TempBool))
            If Not TempBool Then
                SUPPRESS_CONSOLE_ERRORS = True
            Else
                SUPPRESS_CONSOLE_ERRORS = False
            End If
            Call DebugLog("INIT", "Redo=" & ReDo)
            Call DebugLog("INIT", "debug=" & BUGGERME)
            Call DebugLog("INIT", "showerrors=" & SUPPRESS_CONSOLE_ERRORS)
        Catch ex As Exception
            Call LogError("ModMain", "INIT", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub RunApp()
        Try
            Dim sAns As String = vbYes
            Dim UpdateMsg As String = ""
            Dim ObjRS As New BSRegistry
            Call DebugLog("RunApp", "Getting DB Path")
            strDBPath = ObjRS.GetDatabasePath
            Call DebugLog("RunApp", "DBPATH=" & strDBPath)
            Call DebugLog("RunApp", "Getting Main App Path")
            MainApp = ObjRS.MainApplication()
            Call DebugLog("RunApp", "MAINAPPPATH=" & MainApp)
            Call DebugLog("RunApp", "Getting Current Path")
            CurPath = ObjRS.CurrentAppPath()
            Call DebugLog("RunApp", "CURPATH=" & CurPath)
            Call RemovePassword()
            Call RegHotfixExists()
            Call DoUpdates()
            Call DeleteUpdateFile()
            Call AddPassword()
            If HotFixApplied Then
                Console.WriteLine("Updates were applied!")
                sAns = "y"
            Else
                Console.WriteLine("No updates where applied.  You are currently up-to-date!")
                Console.WriteLine("Do you wish to start the " & ProductName & " Application Now (y/n)?")
                sAns = LCase(Console.ReadLine())
            End If
            Dim i As Integer
            If sAns = "y" Or sAns = "yes" Then
                i = Shell(MainApp, AppWinStyle.MaximizedFocus)
            End If
        Catch ex As Exception
            Call LogError("ModMain", "RunApp", Err.Number, ex.Message.ToString)
            Console.Read()
        End Try
    End Sub
    Sub Banner()
        Console.WriteLine("BurnSoft " & ProductName & " HotFix/Upgrade Assistant")
        Console.WriteLine("Version: " & HFVer & vbTab & "Copyright 1997-" & Now.Year)
        Console.WriteLine("Support and Updates at http://www.burnsoft.net")
        Console.WriteLine("")
    End Sub
    Sub Main()
        Try
            Call Banner()
            Call INIT()
            Dim bsreg As New BSRegistry
            Call bsreg.MoveSettingKeys()
            If ReDo Then Call RedoAll()
            IsRunning = False
            HotFixApplied = False
            Call RunApp()
        Catch ex As Exception
            Call LogError("ModMain", "Main", Err.Number, ex.Message.ToString)
            Console.Read()
        End Try
    End Sub

End Module
