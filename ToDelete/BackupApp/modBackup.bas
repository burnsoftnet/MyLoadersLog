Attribute VB_Name = "modBackup"
Private Declare Function CopyFile Lib "kernel32" Alias _
"CopyFileA" (ByVal lpExistingFileName As String, ByVal _
lpNewFileName As String, ByVal bFailIfExists As Long) As Long
Public LogFile As String
Public LF As Integer
Public Sub Copy()
Dim strSource As String
Dim strTarget As String
Dim lngRetVal As Long
Dim strPath As String

strSource = App.Path & "\MLL.mdb"
strTarget = frmBackup1Disk.lblPath & "MLL_" & Year(Now) & "-" & Month(Now) & "-" & Day(Now) & ".bak"

strPath = frmBackup1Disk.lblPath & "*.bak"

'// Copy File
lngRetVal = CopyFile(Trim$(strSource), Trim(strTarget), False)
If lngRetVal Then
    MsgBox "Database Backup Successful!", vbInformation, frmBackup1Disk.Caption
Else
    MsgBox "Error!Database Backup Failed!", vbCritical, frmBackup1Disk.Caption
End If

End Sub

Public Sub UpdateLog(strErr As String, errForm As String, MyFunc As String)
If BuggerMe = True Then
    MsgBox Now & "  " & strErr & "  at form " & errForm & "  in function " & MyFunc
Else
    LogFile = App.Path & "\Backup_Err.log"
    LF = FreeFile
    Open LogFile For Append As #LF
    Print #LF, Now & "  " & strErr & "  at form " & errForm & "  in function " & MyFunc & vbTab & Err.Number & "@" & Err.Description
    Call CloseLog
End If
Err.Clear
End Sub
Public Sub CloseLog()
Close #LF
End Sub
Public Sub CreatelogFile(strLogFile As String)
LogFile = strLogFile
LF = FreeFile
Open strLogFile For Output As #LF

Print #LF, Now & "  The Application has started!"
Call CloseLog
End Sub
