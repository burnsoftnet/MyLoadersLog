Attribute VB_Name = "modRestore"
Private Declare Function CopyFile Lib "kernel32" Alias _
"CopyFileA" (ByVal lpExistingFileName As String, ByVal _
lpNewFileName As String, ByVal bFailIfExists As Long) As Long

Public Sub Import()
Dim strSource As String
Dim strTarget As String
Dim lngRetVal As Long
Dim strPath As String

strSource = "" 'frmImportDB.Drive1.Drive & "\db.bsa"
strTarget = App.Path & "\MLL.mdb"

strPath = App.Path & "\MLL.mdb"

Kill strPath
'// Copy File
lngRetVal = CopyFile(Trim$(strSource), Trim(strTarget), False)
If lngRetVal Then
MsgBox "File copied!"
Else
MsgBox "Error. File not Copied!"
End If

'// Move File
'lngRetVal = MoveFile(Trim$(strSource), Trim(strTarget))
'If lngRetVal Then
'MsgBox "File moved!"
'Else
'MsgBox "Error. File not moved!"
'End If
End Sub

