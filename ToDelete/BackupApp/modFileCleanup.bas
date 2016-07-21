Attribute VB_Name = "modFileCleanup"
Public BSReg As Registry
Const RegRoot = "HKLM"
Const RegKey = "Software\BurnSoft\BSMLL\"
Dim strFile As String         '-Working File
Dim strDateCreated  '-Used to get the Last Time a File was modified /Created
Dim strDateOld      '-Used to get the value of todaydate minus tthe DaysOld Constant
Dim CurrentFile     '-Working File
Dim MyFolderList    '-Used to Split the folders in an array
Dim x               '-Count Folder List array
Dim strWorkingDir   '-Current Working Directory
Dim strDateCount    '- To Count files that arex days old.  MOstly used for reporting
Dim Days2Old        '-Used to Convert the Constant Daysold into a negative
Dim strFileType     '-Grab the Array from DayOld
Dim strFileArr      '-File Type Array
Dim ArrayFiles      '-Array of Files Deleted
Dim SplitArray
Dim strNetIQ
Dim IsEnabled As Boolean
Public DaysOld As Integer
Public RootDirectory As String
Const FileType = "bak"
Const DeleteAllFiles = "n" 'DeleteAllFiles without DateCheck
Const MAXFILESLEFT = 6
Const SIMULATE = False
Const DoMSG = False
Private Function ShowFileInfo(strFile)
    Dim fso, f
    Dim strOld
    Set fso = CreateObject("Scripting.FileSystemObject")
    Set f = fso.GetFile(strFile)
    strDateCreated = FormatDateTime(f.DateLastModified)
End Function
Private Function DeleteOldFile(strFile)
    Dim bAns As Boolean: bAns = False
    Dim fso, f
    Dim strDeletedFile
    Set fso = CreateObject("Scripting.FileSystemObject")
    Set strDeletedFile = fso.GetFile(strFile)
    DeleteOldFile = bAns
    If FileCount <= MAXFILESLEFT Then Exit Function
    If Not SIMULATE Then strDeletedFile.Delete
    bAns = True
    DeleteOldFile = bAns
End Function
Private Function GetCreatedDate(strFileCreated)
    strFile = RootDirectory & strFileCreated
    Call ShowFileInfo(strFile)
    GetCurrentDate = FormatDateTime(strDateCreated, 2)
    strDateCreated = GetCurrentDate
    GetCurrentDate = Date
    Days2Old = 0 - DaysOld
    strDateOld = DateAdd("d", Days2Old, GetCurrentDate)
    CurrentMonth = DateDiff("d", strDateCreated, GetCurrentDate)
     If CurrentMonth >= DaysOld Then
        If DeleteOldFile(strFile) Then
            strDateCount = strDateCount + 1
            ArrayFiles = ArrayFiles + "," + strFile
        End If
    End If
End Function
Private Function GetFileList()
On Error GoTo ErrHandler
    Dim fsof, fi, flf, sf, fc
    Dim strFileSplit
    
    Set fsof = CreateObject("Scripting.FileSystemObject")
    Set fi = fsof.GetFolder(RootDirectory)
    Set fc = fi.Files
    For Each flf In fc
        sf = sf & flf.Name
        strFileSplit = Split(sf, ".")
        If strFileSplit(1) = strFileType Then
            CurrentFile = sf
            Call GetCreatedDate(CurrentFile)
        End If
        sf = ""
    Next
    Exit Function
ErrHandler:
        Dim intErr As Integer
        intErr = Err.Number
        Call UpdateLog(Err.Number & "::" & Err.Description, "modfilecleanup", "GetFileList")
        Select Case intErr
        Case 76
            MsgBox ("Unable to Find Target Path, Please try again!.")
            DoOver = True
            Exit Function
        Case Else
            MsgBox ("Unknown error has occured. Please check your path to make sure it is vaild!")
            DoOver = True
            Exit Function
        End Select
        Resume Next
End Function
Private Function FileCount() As Integer
   Dim fsof, fi, flf, sf, fc
    Dim strFileSplit
    Dim i: i = 0
    Set fsof = CreateObject("Scripting.FileSystemObject")
    Set fi = fsof.GetFolder(RootDirectory)
    Set fc = fi.Files
    For Each flf In fc
        sf = sf & flf.Name
        strFileSplit = Split(sf, ".")
        If strFileSplit(1) = strFileType Then i = i + 1
        sf = ""
    Next
    FileCount = i
End Function
Private Sub GetSettings()
Set BSReg = New Registry
RootDirectory = BSReg.GetString(RegRoot, RegKey & "Settings", "LastPath")
DaysOld = CInt(BSReg.GetDWord(RegRoot, RegKey & "Settings", "TrackHistoryDays"))
IsEnabled = CBool(BSReg.GetString(RegRoot, RegKey & "Settings", "TrackHistory"))
End Sub
Public Sub DoDelete()
strFileArr = Split(FileType, ",")
strFileType = UBound(strFileArr)
Call GetSettings
If Not IsEnabled Then Exit Sub
strDateCount = 0
strFileType = FileType
Call GetFileList
If DoMSG Then
    If strDateCount = 0 Then
        MsgBox "0 Files were deleted!"
    Else
        SplitArray = Replace(ArrayFiles, ",", Chr(10) & Chr(13))
        strNetIQ = strDateCount & " Files were deleted!" & Chr(10) & Chr(13) & "The Following Files Where Deleted:" & Chr(10) & Chr(13) & SplitArray
        MsgBox strNetIQ
    End If
End If
End Sub
