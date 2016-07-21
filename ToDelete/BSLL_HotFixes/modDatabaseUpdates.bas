Attribute VB_Name = "modDatabaseUpdates"
Public BSReg As Registry
Public Const DATABASEPASSWORD = "wtf.m@t3"
Public strDBPath As String
Const RegRoot = "HKLM"
Const RegKey = "Software\BurnSoft\BSMLL\"
Function GetDatabasePath() As String
    Set BSReg = New Registry
    GetDatabasePath = BSReg.GetString(RegRoot, RegKey, "DataBase")
End Function
Public Sub AddPassword()
    On Error GoTo ErrHandler
    Dim Conn As ADODB.Connection
    Set Conn = New ADODB.Connection
    With Conn
        .Provider = "Microsoft.Jet.OLEDB.4.0"
        .ConnectionString = "Data Source=" & strDBPath
        .Mode = adModeShareExclusive
        .Open
    End With
    Dim SQL As String
    SQL = "ALTER DATABASE PASSWORD " & DATABASEPASSWORD & " NULL"
    Conn.Execute SQL
    Conn.Close
    Set Conn = Nothing
    Exit Sub
ErrHandler:
    Debug.Print Err.Number & " - " & Err.Description
End Sub
Public Sub RemovePassword()
    On Error GoTo ErrHandler
    Dim Conn As ADODB.Connection
    Set Conn = New ADODB.Connection
    With Conn
        .Provider = "Microsoft.Jet.OLEDB.4.0"
        .ConnectionString = "Data Source=" & strDBPath
        .Properties("Jet OLEDB:Database Password") = DATABASEPASSWORD
        .Mode = adModeShareExclusive
        .Open
    End With
    Dim SQL As String
    SQL = "ALTER DATABASE PASSWORD NULL " & DATABASEPASSWORD
    Conn.Execute SQL
    Conn.Close
    Set Conn = Nothing
    Exit Sub
ErrHandler:
    Debug.Print Err.Number & " - " & Err.Description
End Sub
Sub ChangePassword()
    Call RemovePassword
    Call AddPassword
    Exit Sub
ErrHandler:
    Debug.Print Err.Number & " - " & Err.Description

End Sub

Public Function TestDBWithNoPWD() As Boolean
    On Error GoTo ErrHandler
    Dim bAns As Boolean: bAns = False
    Dim Conn As ADODB.Connection
    Set Conn = New ADODB.Connection
    With Conn
        .Provider = "Microsoft.Jet.OLEDB.4.0"
        .ConnectionString = "Data Source=" & strDBPath
        .Mode = adModeShareExclusive
        .Open
    End With
    bAns = True
    Conn.Close
    Set Conn = Nothing
    GoTo Ending
ErrHandler:
    Debug.Print Err.Number & " - " & Err.Description
    bAns = False
    GoTo Ending
Ending:
    Set Conn = Nothing
    TestDBWithNoPWD = bAns
End Function

Function TestDBwithPWD()
    On Error GoTo ErrHandler
    Dim bAns As Boolean: bAns = False
    Dim Conn As ADODB.Connection
    Set Conn = New ADODB.Connection
    With Conn
        .Provider = "Microsoft.Jet.OLEDB.4.0"
        .ConnectionString = "Data Source=" & strDBPath
        .Mode = adModeShareExclusive
        .Properties("Jet OLEDB:Database Password") = DATABASEPASSWORD
        .Open
    End With
    bAns = True
    Conn.Close
    Set Conn = Nothing
    GoTo Ending
ErrHandler:
    Debug.Print Err.Number & " - " & Err.Description
    bAns = False
    GoTo Ending
Ending:
    Set Conn = Nothing
    TestDBwithPWD = bAns
End Function
Sub AddColumn(strName As String, strTable As String, strDefaultValue As String, StrType As String)
    Dim MySQL As String
    MySQL = "ALTER TABLE " & strTable & " ADD COLUMN " & strName & " " & StrType & ";"
    Call RunSQL(MySQL)
End Sub
Sub AddColumnD(strName As String, strTable As String, strDefaultValue As String, StrType As String)
    Dim MySQL As String
    MySQL = "ALTER TABLE " & strTable & " ADD COLUMN " & strName & " " & StrType & " [""" & strDefaultValue & """];"
    Call RunSQL(MySQL)
End Sub
Sub AlterColumn(strName As String, strTable As String, strDefaultValue As String, StrType As String)
    Dim MySQL As String
    MySQL = "ALTER TABLE " & strTable & " ALTER COLUMN " & strName & " " & StrType & ";"
    Call RunSQL(MySQL)
End Sub
Sub DropTable(strTable As String)
    Dim MySQL As String
    MySQL = "DROP TABLE " & strTable & ";"
    Call RunSQL(MySQL)
End Sub
Sub RunSQL(SQL As String, Optional eMsg As String = "")
     On Error GoTo ErrHandler
    Dim Conn As ADODB.Connection
    Set Conn = New ADODB.Connection
    With Conn
        .Provider = "Microsoft.Jet.OLEDB.4.0"
        .ConnectionString = "Data Source=" & strDBPath
        .Mode = adModeShareExclusive
        .Properties("Jet OLEDB:Database Password") = DATABASEPASSWORD
        .Open
    End With
    Conn.Execute SQL
    Conn.Close
    Set Conn = Nothing
    Exit Sub
ErrHandler:
    eMsg = Err.Number & " - " & Err.Description
    Debug.Print eMsg
End Sub
Function ValueDoesExist(strTable As String, strCol As String, strValue As String) As Boolean
    Dim bAns As Boolean: bAns = False
     On Error GoTo ErrHandler
    Dim Conn As ADODB.Connection
    Dim RS As ADODB.Recordset
    Set Conn = New ADODB.Connection
    Set RS = New ADODB.Recordset
    With Conn
        .Provider = "Microsoft.Jet.OLEDB.4.0"
        .ConnectionString = "Data Source=" & strDBPath
        .Mode = adModeShareExclusive
        .Properties("Jet OLEDB:Database Password") = DATABASEPASSWORD
        .Open
    End With
    Dim SQL As String
    SQL = "SELECT * from " & strTable & " where " & strCol & "='" & strValue & "'"
    RS.Open SQL, Conn, 1, 3
    If Not RS.BOF And Not RS.EOF Then bAns = True
    'Conn.Execute SQL
    RS.Close
    Set RS = Nothing
    Conn.Close
    Set Conn = Nothing
    ValueDoesExist = bAns
    Exit Function
ErrHandler:
    Debug.Print Err.Number & " - " & Err.Description
    ValueDoesExist = False
End Function

Sub SwapGunColPurchaseValues(strTable As String, strSourceCol As String, strDestCol As String)
     On Error GoTo ErrHandler
    Dim Conn As ADODB.Connection
    Dim RS As ADODB.Recordset
    Set Conn = New ADODB.Connection
    Set RS = New ADODB.Recordset
    With Conn
        .Provider = "Microsoft.Jet.OLEDB.4.0"
        .ConnectionString = "Data Source=" & strDBPath
        .Mode = adModeShareExclusive
        .Properties("Jet OLEDB:Database Password") = DATABASEPASSWORD
        .Open
    End With
    Dim SQL As String
    Dim MySQL As String
    SQL = "SELECT ID," & strSourceCol & ", " & strDestCol & " from " & strTable
    RS.Open SQL, Conn, 1, 3
    If Not RS.BOF And Not RS.EOF Then
        RS.MoveFirst
        Do Until RS.EOF
            If IsNull(RS(strDestCol)) Then
                MySQL = "UPDATE " & strTable & " set " & strDestCol & "='" & RS(strSourceCol) & "' where id=" & RS("ID")
                Conn.Execute MySQL
            End If
            RS.MoveNext
        Loop
    End If
    RS.Close
    Set RS = Nothing
    Conn.Close
    Set Conn = Nothing
    Exit Sub
ErrHandler:
    Debug.Print Err.Number & " - " & Err.Description
End Sub
Function GetMainPictureFirstListing(strCID As String) As Long
    Dim lAns As Long: lAns = 0
     On Error GoTo ErrHandler
    Dim Conn As ADODB.Connection
    Dim RS As ADODB.Recordset
    Set Conn = New ADODB.Connection
    Set RS = New ADODB.Recordset
    With Conn
        .Provider = "Microsoft.Jet.OLEDB.4.0"
        .ConnectionString = "Data Source=" & strDBPath
        .Mode = adModeShareExclusive
        .Properties("Jet OLEDB:Database Password") = DATABASEPASSWORD
        .Open
    End With
    Dim SQL As String
    Dim MySQL As String
    SQL = "SELECT TOP 1 ID FROM Gun_Collection_Pictures where CID=" & strCID & " order by ID ASC"
    RS.Open SQL, Conn, 1, 3
    If Not RS.BOF And Not RS.EOF Then
        RS.MoveFirst
        Do Until RS.EOF
            lAns = CLng(RS("ID"))
            RS.MoveNext
        Loop
    End If
    RS.Close
    Set RS = Nothing
    Conn.Close
    Set Conn = Nothing
    GetMainPictureFirstListing = lAns
    Exit Function
ErrHandler:
    Debug.Print Err.Number & " - " & Err.Description
    GetMainPictureFirstListing = lAns
End Function
Function HasDefaultPictureSet(strCID As String) As Boolean
   Dim bAns As Boolean: bAns = False
     On Error GoTo ErrHandler
    Dim Conn As ADODB.Connection
    Dim RS As ADODB.Recordset
    Set Conn = New ADODB.Connection
    Set RS = New ADODB.Recordset
    With Conn
        .Provider = "Microsoft.Jet.OLEDB.4.0"
        .ConnectionString = "Data Source=" & strDBPath
        .Mode = adModeShareExclusive
        .Properties("Jet OLEDB:Database Password") = DATABASEPASSWORD
        .Open
    End With
    Dim SQL As String
    Dim MySQL As String
    SQL = "SELECT * FROM Gun_Collection_Pictures where CID=" & strCID & " and ISMAIN=1"
    RS.Open SQL, Conn, 1, 3
    If Not RS.BOF And Not RS.EOF Then
        bAns = True
    Else
        bAns = False
    End If
    RS.Close
    Set RS = Nothing
    Conn.Close
    Set Conn = Nothing
    HasDefaultPictureSet = bAns
    Exit Function
ErrHandler:
    Debug.Print Err.Number & " - " & Err.Description
    HasDefaultPictureSet = bAns
End Function
Sub SetMainPictures()
     On Error GoTo ErrHandler
    Dim Conn As ADODB.Connection
    Dim RS As ADODB.Recordset
    Set Conn = New ADODB.Connection
    Set RS = New ADODB.Recordset
    With Conn
        .Provider = "Microsoft.Jet.OLEDB.4.0"
        .ConnectionString = "Data Source=" & strDBPath
        .Mode = adModeShareExclusive
        .Properties("Jet OLEDB:Database Password") = DATABASEPASSWORD
        .Open
    End With
    Dim SQL As String
    Dim MySQL As String
    SQL = "SELECT DISTINCT(Gun_Collection_Pictures.CID) as CID FROM Gun_Collection_Pictures"
    RS.Open SQL, Conn, 1, 3
    Dim LID As Long: LID = 0
    If Not RS.BOF And Not RS.EOF Then
        RS.MoveFirst
        Do Until RS.EOF
            If Not HasDefaultPictureSet(CStr(RS("CID"))) Then
                LID = GetMainPictureFirstListing(CStr(RS("CID")))
                If LID <> 0 Then
                    MySQL = "UPDATE Gun_Collection_Pictures set ISMAIN=1 where ID=" & LID
                    Conn.Execute MySQL
                End If
            End If
            RS.MoveNext
        Loop
    End If
    RS.Close
    Set RS = Nothing
    Conn.Close
    Set Conn = Nothing
    Exit Sub
ErrHandler:
    Debug.Print Err.Number & " - " & Err.Description
End Sub

Sub ConvertAmmoGrainsToNum()
     On Error GoTo ErrHandler
    Dim Conn As ADODB.Connection
    Dim RS As ADODB.Recordset
    Set Conn = New ADODB.Connection
    Set RS = New ADODB.Recordset
    With Conn
        .Provider = "Microsoft.Jet.OLEDB.4.0"
        .ConnectionString = "Data Source=" & strDBPath
        .Mode = adModeShareExclusive
        .Properties("Jet OLEDB:Database Password") = DATABASEPASSWORD
        .Open
    End With
    Dim SQL As String
    Dim MySQL As String
    SQL = "SELECT ID,Grain FROM Gun_Collection_Ammo"
    RS.Open SQL, Conn, 1, 3
    Dim intID As Long: intID = 0
    Dim dValue As Double: dValue = 0
    If Not RS.BOF And Not RS.EOF Then
        RS.MoveFirst
        Do Until RS.EOF
            intID = RS("ID")
            dValue = ConvToNum(RS("Grain"))
            MySQL = "UPDATE Gun_Collection_Ammo set dcal=" & dValue & " where ID=" & intID
            Conn.Execute MySQL
            RS.MoveNext
        Loop
    End If
    RS.Close
    Set RS = Nothing
    Conn.Close
    Set Conn = Nothing
    Exit Sub
ErrHandler:
    Debug.Print Err.Number & " - " & Err.Description

End Sub
