Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Imports System.Data
Public Class frmDBCleanup
    Public i As Integer = 0
    Const PROGRESSBAR_MAX = 15
#Region "General Subs and Functions"
    Private Sub cbActionList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbActionList.SelectedIndexChanged
        If cbActionList.SelectedItem <> Nothing Then
            Dim strList As String = cbActionList.SelectedItem.ToString
            If Len(strList) > 0 Then
                btnStart.Enabled = True
            Else
                btnStart.Enabled = False
            End If
        End If
    End Sub
    Function DataIsRelated(ByVal strSQL As String) As Boolean
        Dim bAns As Boolean = False
        Dim Obj As New BSDatabase
        Call Obj.ConnectDB()
        Dim CMD As New OdbcCommand(strSQL, Obj.Conn)
        Dim RS As OdbcDataReader
        RS = CMD.ExecuteReader
        bAns = RS.HasRows
        Return bAns
    End Function
    Sub KillData(ByVal strTable As String)
        Dim Obj As New BSDatabase
        Dim SQL As String = "DELETE from " & strTable
        Call Obj.ConnExec(SQL)
    End Sub
    Sub DELETE_RECORD(ByVal strTable As String, ByVal strID As String)
        Dim SQL As String = "DELETE from " & strTable & " where ID=" & strID
        Dim Obj As New BSDatabase
        Obj.ConnExec(SQL)
    End Sub
    Sub AddToProgBar(ByVal strMsg As String)
        Try
            lblStatus.Text = strMsg
            lblStatus.Refresh()
            i = i + 1
            ProgressBar1.Value = i
            ProgressBar1.Refresh()
        Catch ex As Exception
            Call LogError(Me.Name, "AddToProgBar", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Function ExistsinCollection(ByVal strID As String, ByVal strColumn As String, ByVal strTable As String) As Boolean
        Dim bAns As Boolean = False
        Dim SQL As String = "SELECT * from " & strTable & " where " & strColumn & "=" & strID
        Dim Obj As New BSDatabase
        Call Obj.ConnectDB()
        Dim CMD As New OdbcCommand(SQL, Obj.Conn)
        Dim RS As OdbcDataReader
        RS = CMD.ExecuteReader
        bAns = RS.HasRows
        RS.Close()
        RS = Nothing
        CMD = Nothing
        Obj.CloseDB()
        Obj = Nothing
        Return bAns
    End Function
    Sub ProcessRecordDeletes(ByVal strTable As String, ByVal strCollectionID As String, ByVal strCollectionTable As String)
        Dim Obj As New BSDatabase
        Dim strID As String = ""
        Call Obj.ConnectDB()
        Dim SQL As String = "SELECT ID from " & strTable
        Dim CMD As New OdbcCommand(SQL, Obj.Conn)
        Dim RS As OdbcDataReader
        RS = CMD.ExecuteReader
        While RS.Read()
            strID = CStr(RS("ID"))
            If Not ExistsinCollection(strID, strCollectionID, strCollectionTable) Then
                Call DELETE_RECORD(strTable, strID)
            End If
        End While
        RS.Close()
        RS = Nothing
        Obj.CloseDB()
    End Sub
#End Region
#Region "Non Dependant Clear Subs"
    Sub Clear_GeneralCaliberList()
        Call KillData("General_Calibers")
    End Sub
    Sub Clear_AmmunitionList()
        Call KillData("Loaders_Log_Ammunition")
    End Sub
    Sub Clear_ConfigLists()
        Call KillData("Loaders_Log_Ammunition_Audit")
        Call KillData("Config_List_Powder_Data_SG")
        Call KillData("Config_List_Powder_Data_NSG")
        Call KillData("Config_List_Data_SG")
        Call KillData("Config_List_Data_NSG")
        Call KillData("Config_List_Name")
        Call MDIParentMain.RefreshConfigData()
    End Sub
    Sub Clear_Firearms()
        Call KillData("Loaders_Log_Firearms")
    End Sub
    Sub Clear_EquipmentList()
        Call KillData("General_Equipment")
    End Sub
    Sub Clear_WishList()
        Call KillData("Wishlist")
    End Sub
    Sub Clear_Loaders_Log_NSG()
        Call KillData("Loaders_Log_NSG")
    End Sub
    Sub Clear_Loaders_Log_SG()
        Call KillData("Loaders_Log_SG")
    End Sub
#End Region
#Region "Dependant Clear Subs"
    Sub Clear_Primers()
        Try
            Dim strTable As String = "General_Primer"
            Dim strCollectionID As String = "PRID"
            Dim strCollectionTable As String = "Config_List_Data_NSG"
            Dim strCollectionTable2 As String = "Config_List_Data_SG"
            Dim strID As String = ""
            Dim SQL As String = "SELECT " & strTable & ".*  from " & strTable & " Inner Join " & strCollectionTable & " on " & strCollectionTable & "." & strCollectionID & " =" & strTable & ".ID"
            Dim SQL2 As String = "SELECT " & strTable & ".*  from " & strTable & " Inner Join " & strCollectionTable2 & " on " & strCollectionTable2 & "." & strCollectionID & " =" & strTable & ".ID"
            If DataIsRelated(SQL) Or DataIsRelated(SQL2) Then
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                SQL = "SELECT ID from " & strTable
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read()
                    strID = CStr(RS("ID"))
                    If Not ExistsinCollection(strID, strCollectionID, strCollectionTable) And Not ExistsinCollection(strID, strCollectionID, strCollectionTable2) Then Call DELETE_RECORD(strTable, strID)
                End While
                RS.Close()
                RS = Nothing
                Obj.CloseDB()
            Else
                Call KillData(strTable)
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "Clear_Primers", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub Clear_Bullets()
        Try
            Dim strTable As String = "List_Bullets"
            Dim strCollectionID As String = "BID"
            Dim strCollectionTable As String = "Config_List_Data_NSG"
            Dim strID As String = ""
            Dim SQL As String = "SELECT " & strTable & ".*  from " & strTable & " Inner Join " & strCollectionTable & " on " & strCollectionTable & "." & strCollectionID & " =" & strTable & ".ID"
            If DataIsRelated(SQL) Then
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                SQL = "SELECT ID from " & strTable
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read()
                    strID = CStr(RS("ID"))
                    If Not ExistsinCollection(strID, strCollectionID, strCollectionTable) Then Call DELETE_RECORD(strTable, strID)
                End While
                RS.Close()
                RS = Nothing
                Obj.CloseDB()
            Else
                Call KillData(strTable)
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "Clear_Bullets", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub Clear_MyCalLists()
        Try
            Dim strTable As String = "List_Calibers"
            Dim strCollectionID As String = "CALID"
            Dim strCollectionID2 As String = "CID"
            Dim strCollectionTable As String = "Config_List_Data_NSG"
            Dim strCollectionTable2 As String = "List_Bullets"
            Dim strCollectionTable3 As String = "List_Case"
            Dim strID As String = ""
            Dim SQL As String = "SELECT " & strTable & ".*  from " & strTable & " Inner Join " & strCollectionTable & " on " & strCollectionTable & "." & strCollectionID & " =" & strTable & ".ID"
            Dim SQL2 As String = "SELECT " & strTable & ".*  from " & strTable & " Inner Join " & strCollectionTable2 & " on " & strCollectionTable2 & "." & strCollectionID2 & " =" & strTable & ".ID"
            Dim SQL3 As String = "SELECT " & strTable & ".*  from " & strTable & " Inner Join " & strCollectionTable3 & " on " & strCollectionTable3 & "." & strCollectionID2 & " =" & strTable & ".ID"
            If DataIsRelated(SQL) Or DataIsRelated(SQL2) Or DataIsRelated(SQL3) Then
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                SQL = "SELECT ID from " & strTable
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read()
                    strID = CStr(RS("ID"))
                    If Not ExistsinCollection(strID, strCollectionID, strCollectionTable) And Not ExistsinCollection(strID, strCollectionID2, strCollectionTable2) _
                        And Not ExistsinCollection(strID, strCollectionID2, strCollectionTable3) Then Call DELETE_RECORD(strTable, strID)
                End While
                RS.Close()
                RS = Nothing
                Obj.CloseDB()
            Else
                Call KillData(strTable)
            End If
            Call MDIParentMain.RefreshCalData()
        Catch ex As Exception
            Call LogError(Me.Name, "Clear_Bullets", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub Clear_Cases()
        Try
            Dim strTable As String = "List_Case"
            Dim strCollectionID As String = "CAID"
            Dim strCollectionTable As String = "Config_List_Data_NSG"
            Dim strID As String = ""
            Dim SQL As String = "SELECT " & strTable & ".*  from " & strTable & " Inner Join " & strCollectionTable & " on " & strCollectionTable & "." & strCollectionID & " =" & strTable & ".ID"
            If DataIsRelated(SQL) Then
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                SQL = "SELECT ID from " & strTable
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read()
                    strID = CStr(RS("ID"))
                    If Not ExistsinCollection(strID, strCollectionID, strCollectionTable) Then Call DELETE_RECORD(strTable, strID)
                End While
                RS.Close()
                RS = Nothing
                Obj.CloseDB()
            Else
                Call KillData(strTable)
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "Clear_Cases", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub Clear_WADS()
        Try
            Dim strTable As String = "List_SG_WAD"
            Dim strCollectionID As String = "WAD"
            Dim strCollectionTable As String = "Config_List_Data_SG"
            Dim strID As String = ""
            Dim SQL As String = "SELECT " & strTable & ".*  from " & strTable & " Inner Join " & strCollectionTable & " on " & strCollectionTable & "." & strCollectionID & " =" & strTable & ".ID"
            If DataIsRelated(SQL) Then
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                SQL = "SELECT ID from " & strTable
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read()
                    strID = CStr(RS("ID"))
                    If Not ExistsinCollection(strID, strCollectionID, strCollectionTable) Then Call DELETE_RECORD(strTable, strID)
                End While
                RS.Close()
                RS = Nothing
                Obj.CloseDB()
            Else
                Call KillData(strTable)
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "Clear_WADS", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub Clear_Shells()
        Try
            Dim strTable As String = "List_SG_Case"
            Dim strCollectionID As String = "CAID"
            Dim strCollectionTable As String = "Config_List_Data_SG"
            Dim strID As String = ""
            Dim SQL As String = "SELECT " & strTable & ".*  from " & strTable & " Inner Join " & strCollectionTable & " on " & strCollectionTable & "." & strCollectionID & " =" & strTable & ".ID"
            If DataIsRelated(SQL) Then
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                SQL = "SELECT ID from " & strTable
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read()
                    strID = CStr(RS("ID"))
                    If Not ExistsinCollection(strID, strCollectionID, strCollectionTable) Then Call DELETE_RECORD(strTable, strID)
                End While
                RS.Close()
                RS = Nothing
                Obj.CloseDB()
            Else
                Call KillData(strTable)
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "Clear_Shells", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub Clear_Powder()
        Try
            Dim strTable As String = "General_Powder"
            Dim strCollectionID As String = "PID"
            Dim strCollectionTable As String = "Config_List_Powder_Data_NSG"
            Dim strCollectionTable2 As String = "Config_List_Powder_Data_SG"
            Dim strID As String = ""
            Dim SQL As String = "SELECT " & strTable & ".*  from " & strTable & " Inner Join " & strCollectionTable & " on " & strCollectionTable & "." & strCollectionID & " =" & strTable & ".ID"
            Dim SQL2 As String = "SELECT " & strTable & ".*  from " & strTable & " Inner Join " & strCollectionTable2 & " on " & strCollectionTable2 & "." & strCollectionID & " =" & strTable & ".ID"
            If DataIsRelated(SQL) Or DataIsRelated(SQL2) Then
                Dim Obj As New BSDatabase
                Call Obj.ConnectDB()
                SQL = "SELECT ID from " & strTable
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read()
                    strID = CStr(RS("ID"))
                    If Not ExistsinCollection(strID, strCollectionID, strCollectionTable) And Not ExistsinCollection(strID, strCollectionID, strCollectionTable2) Then Call DELETE_RECORD(strTable, strID)
                End While
                RS.Close()
                RS = Nothing
                Obj.CloseDB()
            Else
                Call KillData(strTable)
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "Clear_Powder", Err.Number, ex.Message.ToString)
        End Try
    End Sub
#End Region
    Sub ClearAll()
        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = PROGRESSBAR_MAX
        i = 0
        Call AddToProgBar("Clearing General Calibers")
        Call Clear_GeneralCaliberList()
        Call AddToProgBar("Clear Config Lists")
        Call Clear_ConfigLists()
        Call AddToProgBar("Clearing Ammunition Lists")
        Call Clear_AmmunitionList()
        Call AddToProgBar("Clearing Primers")
        Call Clear_Primers()
        Call AddToProgBar("Clearing Bullets")
        Call Clear_Bullets()
        Call AddToProgBar("Clearing Cases")
        Call Clear_Cases()
        Call AddToProgBar("Clearing Powders")
        Call Clear_Powder()
        Call AddToProgBar("Clearing WADS")
        Call Clear_WADS()
        Call AddToProgBar("Clearing Shells")
        Call Clear_Shells()
        Call AddToProgBar("Clearing Equipment List")
        Call Clear_EquipmentList()
        Call AddToProgBar("Clearing Firearms")
        Call Clear_Firearms()
        Call AddToProgBar("Clearing Wish List")
        Call Clear_WishList()
        Call AddToProgBar("Clearing My Calibers")
        Call Clear_MyCalLists()
        Call AddToProgBar("Clearing Loaders Log - Shotguns")
        Call Clear_Loaders_Log_SG()
        Call AddToProgBar("Clearing Loaders Log - Rifle & Pistols")
        Call Clear_Loaders_Log_NSG()
        ProgressBar1.Visible = False
        lblStatus.Text = ""
        lblStatus.Refresh()
    End Sub
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Me.Cursor = Cursors.WaitCursor
        Dim strSelected As String = cbActionList.SelectedItem.ToString
        btnStart.Enabled = False
        Select Case strSelected
            Case "Remove All Data"
                Dim sAns As String = MsgBox("Are you sure you wish to remove all data from the database?", MsgBoxStyle.YesNo, "Remove All Data")
                If sAns = vbYes Then Call ClearAll()
            Case "Clear General Caliber List"
                Call Clear_GeneralCaliberList()
            Case "Clear My Caliber List"
                Call Clear_MyCalLists()
            Case "Clear Configuration Data"
                Call Clear_ConfigLists()
            Case "Clear Ammunition List"
                Call Clear_AmmunitionList()
            Case "Clear Primers"
                Call Clear_Primers
            Case "Clear Bullets"
                Call Clear_Bullets()
            Case "Clear Cases"
                Call Clear_Cases()
            Case "Clear Powder"
                Call Clear_Powder()
            Case "Clear WADS"
                Call Clear_WADS()
            Case "Clear Shells"
                Call Clear_Shells()
            Case "Clear Equipment"
                Call Clear_EquipmentList()
            Case "Clear Firearm Collection"
                Call Clear_Firearms()
            Case "Clear WishList"
                Call Clear_WishList()
            Case "Clearing Loaders Log - Rifle & Pistols"
                Call Clear_Loaders_Log_NSG()
            Case "Clearing Loaders Log - Shotguns"
                Call Clear_Loaders_Log_SG()
        End Select
        MsgBox("Database clean up to " & strSelected & " is complete!")
        btnStart.Enabled = True
        Me.Cursor = Cursors.Arrow
    End Sub
End Class