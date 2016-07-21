Attribute VB_Name = "modOtherUpdates"
Public BSReg As Registry
Const RegRoot = "HKLM"
Const RegKey = "Software\BurnSoft\BSMLL\"
Public MainApp As String
Public CurPath As String
Function CurrentVersion() As String
    Set BSReg = New Registry
    CurrentVersion = BSReg.GetString(RegRoot, RegKey, "Version")
End Function
Function CurrentAppPath() As String
    Set BSReg = New Registry
    CurrentAppPath = BSReg.GetString(RegRoot, RegKey, "Path")
End Function

Function MainApplication() As String
    Set BSReg = New Registry
    MainApplication = BSReg.GetString(RegRoot, RegKey, "AppEXE")
End Function
Sub RegHotfixExists()
    Set BSReg = New Registry
    Dim MyRead As String
    MyRead = BSReg.GetString(RegRoot, RegKey & "HotFix", "LastUpdate")
    If MyRead = "There was a problem reading the registry." Then
        Call BSReg.SaveKey(RegRoot, RegKey & "\HotFix")
    End If
End Sub
Function HotFixExists(strID As String) As Boolean
    Dim bAns As Boolean: bAns = False
    Set BSReg = New Registry
    Dim MyRead As String
    MyRead = BSReg.GetString(RegRoot, RegKey & "\HotFix", strID)
    If MyRead = "There was a problem reading the registry." Then
        bAns = False
    Else
        bAns = True
    End If
    HotFixExists = bAns
End Function
Sub UpdateLastUpdate(strUpdate As String)
    Set BSReg = New Registry
    Call BSReg.SaveString(RegRoot, RegKey & "\HotFix", "LastUpdate", strUpdate)
End Sub
Sub AppliedUpdates(strUpdate As String)
    Set BSReg = New Registry
    Call BSReg.SaveString(RegRoot, RegKey & "\HotFix", strUpdate, Now)
End Sub
Sub SaveRegValue(strKey As String, strValue As String, strParameter As String)
    Set BSReg = New Registry
    Call BSReg.SaveString(RegRoot, RegKey & "\" & strKey, strValue, strParameter)
End Sub
Sub DeleteUpdateFile()
On Error Resume Next
Kill CurPath & "\hotfix.ini"
End Sub
Sub DelRegValue(strKey As String, strValue As String, strParameter As String)
    Set BSReg = New Registry
    Call BSReg.DeleteValue(RegRoot, RegKey & "\" & strKey, strValue)
End Sub
Public Function ConvToNum(ByVal strValue As String) As Double
        Dim dAns As Double: dAns = 0
        Dim intChar As Integer: intChar = Len(strValue)
        Dim i As Integer: i = 0
        Dim CurValue As String: CurValue = ""
        Dim NewValue As String: NewValue = ""
        Dim LastValue As String: LastValue = ""
        Dim NeedDiv As Boolean: NeedDiv = False
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
                    NewValue = NewValue & CurValue
                Else
                    NewValue = CInt(CurValue) / CInt(LastValue)
                End If
                NeedDiv = False
            Else
                Select Case CurValue
                    Case "."
                        NewValue = NewValue & CurValue
                        NeedDiv = False
                    Case "/"
                        NeedDiv = True
                End Select
            End If
        Next
        dAns = CDbl(NewValue)
        ConvToNum = dAns
    End Function
