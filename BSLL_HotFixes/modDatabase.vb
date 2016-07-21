Imports System
Imports System.Data
Imports System.Data.Odbc
Module modDatabase
    Public Sub AddPassword()
        Dim Conn As ADODB.Connection
        Conn = New ADODB.Connection
        Try
            With Conn
                .Provider = "Microsoft.Jet.OLEDB.4.0"
                .ConnectionString = "Data Source=" & strDBPath
                .Mode = ADODB.ConnectModeEnum.adModeShareExclusive
                .Open()
            End With
            Dim SQL As String
            SQL = "ALTER DATABASE PASSWORD " & DATABASEPASSWORD & " NULL"
            Conn.Execute(SQL)
            Conn.Close()
            Conn = Nothing
        Catch ex As Exception
            Call DebugLog("AddPassword error")
            Call DebugLog(Err.Number & " - " & Err.Description)
            If Conn.State <> 0 Then
                Conn.Close()
                Conn = Nothing
            End If
        End Try
    End Sub
    Public Sub RemovePassword()
        Dim Conn As ADODB.Connection
        Conn = New ADODB.Connection
        Try
            With Conn
                .Provider = "Microsoft.Jet.OLEDB.4.0"
                .ConnectionString = "Data Source=" & strDBPath
                .Properties("Jet OLEDB:Database Password").Value = DATABASEPASSWORD
                .Mode = ADODB.ConnectModeEnum.adModeShareExclusive
                .Open()
            End With
            Dim SQL As String
            SQL = "ALTER DATABASE PASSWORD NULL " & DATABASEPASSWORD
            Conn.Execute(SQL)
            Conn.Close()
            Conn = Nothing
        Catch ex As Exception
            Call DebugLog("RemovePassword error")
            Call DebugLog(Err.Number & " - " & Err.Description)
            If Conn.State <> 0 Then
                Conn.Close()
                Conn = Nothing
            End If
        End Try
    End Sub
    Sub ChangePassword()
        Call RemovePassword()
        Call AddPassword()
        Call DebugLog("ChangePassword")
    End Sub
    Public Function TestDBWithNoPWD() As Boolean
        Dim bAns As Boolean = False
        Dim Conn As ADODB.Connection
        Conn = New ADODB.Connection
        Try
            With Conn
                .Provider = "Microsoft.Jet.OLEDB.4.0"
                .ConnectionString = "Data Source=" & strDBPath
                .Mode = ADODB.ConnectModeEnum.adModeShareExclusive
                .Open()
            End With
            bAns = True
            Conn.Close()
            Conn = Nothing
        Catch ex As Exception
            Call DebugLog("TestDBWithNoPWD")
            Call DebugLog(Err.Number & " - " & Err.Description)
            If Conn.State <> 0 Then
                Conn.Close()
                Conn = Nothing
            End If
        End Try
        Return bAns
    End Function
    Function TestDBwithPWD() As Boolean
        Dim bAns As Boolean = False
        Dim Conn As ADODB.Connection
        Conn = New ADODB.Connection
        Try
            With Conn
                .Provider = "Microsoft.Jet.OLEDB.4.0"
                .ConnectionString = "Data Source=" & strDBPath
                .Mode = ADODB.ConnectModeEnum.adModeShareExclusive
                .Properties("Jet OLEDB:Database Password").Value = DATABASEPASSWORD
                .Open()
            End With
            bAns = True
            Conn.Close()
            Conn = Nothing
        Catch ex As Exception
            Call DebugLog("TestDBWithPWD")
            Call DebugLog(Err.Number & " - " & Err.Description)
            If Conn.State <> 0 Then
                Conn.Close()
                Conn = Nothing
            End If
        End Try
        Return bAns
    End Function
    Sub RunSQL(ByRef SQL As String)
        Dim Conn As ADODB.Connection
        Conn = New ADODB.Connection
        Try
            With Conn
                .Provider = "Microsoft.Jet.OLEDB.4.0"
                .ConnectionString = "Data Source=" & strDBPath
                .Mode = ADODB.ConnectModeEnum.adModeShareExclusive
                .Properties("Jet OLEDB:Database Password").Value = DATABASEPASSWORD
                .Open()
            End With
            Conn.Execute(SQL)
            Conn.Close()
            Conn = Nothing
        Catch ex As Exception
            Call DebugLog("RunSQL")
            Call DebugLog(Err.Number & " - " & Err.Description)
            If Conn.State <> 0 Then
                Conn.Close()
                Conn = Nothing
            End If
        End Try
    End Sub
    Sub AddColumn(ByRef strName As String, ByRef strTable As String, ByRef strDefaultValue As String, ByRef StrType As String)
        Dim MySQL As String
        MySQL = "ALTER TABLE " & strTable & " ADD COLUMN " & strName & " " & StrType & ";"
        Call RunSQL(MySQL)
    End Sub
    Sub AddColumnD(ByRef strName As String, ByRef strTable As String, ByRef strDefaultValue As String, ByRef StrType As String)
        Dim MySQL As String
        MySQL = "ALTER TABLE " & strTable & " ADD COLUMN " & strName & " " & StrType & " [""" & strDefaultValue & """];"
        Call RunSQL(MySQL)
    End Sub
    Sub CreateView(ByVal vName As String, ByVal SQL As String)
        Dim MySQL As String = ""
        MySQL = "CREATE VIEW " & vName & " AS " & SQL & ""
        Call RunSQL(MySQL)
    End Sub
    Sub AlterColumn(ByRef strName As String, ByRef strTable As String, ByRef strDefaultValue As String, ByRef StrType As String)
        Dim MySQL As String
        MySQL = "ALTER TABLE " & strTable & " ALTER COLUMN " & strName & " " & StrType & ";"
        Call RunSQL(MySQL)
    End Sub
    Sub DropTable(ByRef strTable As String)
        Dim MySQL As String
        MySQL = "DROP TABLE " & strTable & ";"
        Call RunSQL(MySQL)
    End Sub
    Function ValueDoesExist(ByRef strTable As String, ByRef strCol As String, ByRef strValue As String) As Boolean
        Dim bAns As Boolean = False
        Dim Conn As ADODB.Connection
        Conn = New ADODB.Connection
        Try
            Dim RS As ADODB.Recordset
            RS = New ADODB.Recordset
            With Conn
                .Provider = "Microsoft.Jet.OLEDB.4.0"
                .ConnectionString = "Data Source=" & strDBPath
                .Mode = ADODB.ConnectModeEnum.adModeShareExclusive
                .Properties("Jet OLEDB:Database Password").Value = DATABASEPASSWORD
                .Open()
            End With
            Dim SQL As String
            SQL = "SELECT * from " & strTable & " where " & strCol & "='" & strValue & "'"
            RS.Open(SQL, Conn, 1, 3)
            If Not RS.BOF And Not RS.EOF Then bAns = True
            RS.Close()
            RS = Nothing
            Conn.Close()
            Conn = Nothing
        Catch ex As Exception
            Call DebugLog("ValueDoesExist error")
            Call DebugLog(Err.Number & " - " & Err.Description)
            If Conn.State <> 0 Then
                Conn.Close()
                Conn = Nothing
            End If
        End Try
        Return bAns
    End Function
    Sub ConvertAmmoGrainsToNum()
        Dim Conn As ADODB.Connection
        Conn = New ADODB.Connection
        Try
            Dim RS As ADODB.Recordset
            RS = New ADODB.Recordset
            With Conn
                .Provider = "Microsoft.Jet.OLEDB.4.0"
                .ConnectionString = "Data Source=" & strDBPath
                .Mode = ADODB.ConnectModeEnum.adModeShareExclusive
                .Properties("Jet OLEDB:Database Password").Value = DATABASEPASSWORD
                .Open()
            End With
            Dim SQL As String
            Dim MySQL As String
            SQL = "SELECT ID,Grain FROM Gun_Collection_Ammo"
            RS.Open(SQL, Conn, 1, 3)
            Dim intID As Integer : intID = 0
            Dim dValue As Double : dValue = 0
            If Not RS.BOF And Not RS.EOF Then
                RS.MoveFirst()
                Do Until RS.EOF
                    intID = RS.Fields("ID").Value
                    dValue = ConvToNum(RS.Fields("Grain").Value)
                    MySQL = "UPDATE Gun_Collection_Ammo set dcal=" & dValue & " where ID=" & intID
                    Conn.Execute(MySQL)
                    RS.MoveNext()
                Loop
            End If
            RS.Close()
            RS = Nothing
            Conn.Close()
            Conn = Nothing
        Catch ex As Exception
            Call DebugLog("ConvertAmmoGrainsToNum error")
            Call DebugLog(Err.Number & " - " & Err.Description)
            If Conn.State <> 0 Then
                Conn.Close()
                Conn = Nothing
            End If
        End Try
    End Sub
    Public Sub ConnExec(ByVal strSQL As String)
        Try
            Dim Conn As OdbcConnection
            Conn = New OdbcConnection("Driver={Microsoft Access Driver (*.mdb)};dbq=" & strDBPath & ";Pwd=" & DATABASEPASSWORD)
            Conn.Open()
            Dim CMD As New OdbcCommand
            CMD.Connection = Conn
            CMD.CommandText = strSQL
            CMD.ExecuteNonQuery()
            CMD.Connection.Close()
            CMD = Nothing
            Conn.Close()
            Conn = Nothing
        Catch ex As Exception
            Call DebugLog("ConnExec", Err.Number & " - " & Err.Description)
        End Try
    End Sub
    Public Sub SaveDatabaseVersion(ByVal newVer As String)
        Try
            Dim SQL As String = "INSERT INTO DB_Version (dbver,dta) VALUES('" & _
                newVer & "',DATE())"
            Call ConnExec(SQL)
        Catch ex As Exception
            Call DebugLog("SaveDatabaseVersion", Err.Number & " - " & Err.Description)
        End Try
    End Sub
    Public Sub AddSyncToTable(ByVal sNewTableName As String, Optional ByVal UpdateField As Boolean = False, Optional ByVal SyncTableName As String = "sync_tables")
        Try
            If Not ValueDoesExist(SyncTableName, "tblname", sNewTableName) Then Call ConnExec("INSERT INTO " & SyncTableName & " (tblname) VALUES('" & sNewTableName & "')")
            Call AddColumn("sync_lastupdate", sNewTableName, "Now()", "DATETIME")
            If UpdateField Then Call ConnExec("UPDATE " & sNewTableName & " set sync_lastupdate=Now()")
        Catch ex As Exception
            Call DebugLog("AddSyncToTable", Err.Number & " - " & Err.Description)
        End Try
    End Sub
End Module
