VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "Registry"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False

Friend Function GetKeyType(strType As String) As Long
Select Case strType
    Case "HKCR"
        GetKeyType = HKEY_CLASSES_ROOT
    Case "HKCU"
        GetKeyType = HKEY_CURRENT_USER
    Case "HKLM"
        GetKeyType = HKEY_LOCAL_MACHINE
    Case "HKU"
        GetKeyType = HKEY_USERS
    Case "HKPD"
        GetKeyType = HKEY_PERFORMANCE_DATA
End Select
End Function
Friend Sub SaveString(hkey As String, strPath As String, strValue As String, strdata As String)
Call fSaveString(GetKeyType(hkey), strPath, strValue, strdata)
'If MyError = 0 Then
'    SaveString = "The Value was added Successfully!"
'Else
'    SaveString = "There was a problem inserting the data: " & Err.Description & ":" & Err.Number
'End If
End Sub

Friend Function GetString(hkey As String, strPath As String, strValue As String)
GetString = fgetstring(GetKeyType(hkey), strPath, strValue)
'GetString = fgetstring(HKEY_LOCAL_MACHINE, strPath, strValue)
End Function
Friend Function DeleteKey(hkey As String, strKey As String)
Call fDeleteKey(GetKeyType(hkey), strKey)
If MyError = 0 Then
    DeleteKey = hkey & "\" & strKey & " was deleted."
Else
    DeleteKey = "There was a problem deleting the Registry Key"
End If

End Function

Friend Function DeleteValue(hkey As String, strKey As String, strValue As String)
DeleteValue = fDeleteValue(GetKeyType(hkey), strKey, strValue)
If MyError = 0 Then
    DeleteValue = "Value Deleted"
Else
    DeleteValue = "There was a problem deleting the value."
End If
End Function
Friend Function SaveKey(hkey As String, strKey As String) As Boolean
Call fSaveKey(GetKeyType(hkey), strKey)

If MyError = 0 Then
    SaveKey = True
Else
    SaveKey = False
End If
End Function

Friend Function SaveDWord(hkey As String, strKey As String, strValue As String, intData As Long)
Dim MyMessage

MyMessage = fSaveDword(GetKeyType(hkey), strKey, strValue, intData)

If MyError = 0 Then
    SaveDWord = "The DWord Value was saved."
Else
    SaveDWord = "THere was a problem inserting the value in the registy."
End If
End Function

Friend Function GetDWord(hkey As String, strKey As String, strValue As String)
Dim MyMessage
MyMessage = fGetDWord(GetKeyType(hkey), strKey, strValue)

If MyError = 0 Then
    GetDWord = MyMessage
Else
    GetDWord = "There was a problem read the Dword Value."
End If
End Function
Friend Function EnumRegistryKeys(ByVal hkey As String, ByVal KeyName As String) As _
                Collection
    Dim handle As Long
    Dim length As Long
    Dim index As Long
    Dim subkeyName As String
    Dim fFiletime As FILETIME
         ' initialize the result collection
         Set EnumRegistryKeys = New Collection
         
         ' Open the key, exit if not found
         If Len(KeyName) Then
             If RegOpenKeyEx(GetKeyType(hkey), KeyName, 0, KEY_READ, handle) Then Exit Function
             ' in all case the subsequent functions use hKey
             hkey = handle
         End If
         
         Do
             ' this is the max length for a key name
             length = 260
             subkeyName = Space$(length)
             ' get the N-th key, exit the loop if not found
             If RegEnumKeyEx(hkey, index, subkeyName, length, 0, "", vbNull, fFiletime) = ERROR_NO_MORE_ITEMS Then Exit Do
             ' add to the result collection
             subkeyName = Left$(subkeyName, InStr(subkeyName, vbNullChar) - 1)
             EnumRegistryKeys.Add subkeyName, subkeyName
             ' prepare to query for next key
             index = index + 1
         Loop
        
         ' Close the key, if it was actually opened
         If handle Then RegCloseKey handle
        
End Function


