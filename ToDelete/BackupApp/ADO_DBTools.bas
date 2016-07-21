Attribute VB_Name = "ADO_DBTools"
' Module     : ADO_DBTools
' Description:
' Procedures : g_blnCompactDB(p_strMDBFile As String, Optional p_blnBackup As Boolean, Optional p_blnMsg As Boolean)
'              g_blnDB_BackUp(p_strMDBFile As String, Optional p_blnMsg As Boolean)
'              g_blnOutput_CSV(p_strDbFile As String)
'              GetDataPath(p_strFile As String)
'
' Modified   : 04/12/05 TPM
' --------------------------------------------------
' This Module Requires references to the following:
'
' 1) Microsoft ActiveX Data Objects Library (Minimum Version 2.5)
' 2) Microsoft Jet and Replication Objects Library (Minimum Version 2.5)
' 3) Microsoft ADO Ext. for DDL and Security (Minimum Version 2.5)

Option Explicit

Private Const m_cstrMOD_TITLE As String = "ADO Database Tools"

Public Function g_blnCompactDB(p_strMDBFile As String, Optional p_blnBackup As Boolean, Optional p_blnMsg As Boolean) As Boolean
    ' Comments  :
    ' Parameters: p_strMDBFile = Filename with fully qualified path
    '             p_blnBackup -
    '             p_blnMsg -
    ' Returns   : Boolean -
    ' Modified  : 04/12/2005 TPM
    '
    ' --------------------------------------------------

    On Error GoTo PROC_ERR

    Dim pJetEngine      As New JRO.JetEngine
    Dim strMsg          As String
    Dim strTempDB       As String
    Dim strPath         As String

    Dim blnReturn       As Boolean

    strTempDB = "Temp.mdb"

    strPath = GetDataPath(p_strMDBFile)

    ' xx Assume Success
    g_blnCompactDB = True

    ' xx Make Backup copy of Database
    If p_blnBackup = True Then
        blnReturn = g_blnDB_BackUp(p_strMDBFile, p_blnMsg)
    End If

    ' xx Delete compacted copy if it still exists
    If Dir(strPath & strTempDB) <> "" Then
        Kill strPath & strTempDB
    End If


    ' xx Compacts database without encryption.
    pJetEngine.CompactDatabase _
    "Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & p_strMDBFile, _
    "Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & strPath & strTempDB & ";" & _
    "Jet OLEDB:Encrypt Database=False"

    ' xx Delete the fragmented original
    Kill p_strMDBFile

    ' xx Rename the compacted file with the original name
    Name strPath & strTempDB As p_strMDBFile

    If p_blnMsg Then
        strMsg = "Successfully Compacted Database:" & vbCrLf & vbCrLf & p_strMDBFile
        MsgBox strMsg, vbInformation, m_cstrMOD_TITLE
    End If


PROC_EXIT:
    ' xx Clean Up
    Set pJetEngine = Nothing
    Exit Function

PROC_ERR:
    g_blnCompactDB = False
    strMsg = Err.Description & " " & Err.Number
    MsgBox strMsg, vbExclamation, m_cstrMOD_TITLE
    Resume PROC_EXIT


End Function

Public Function g_blnDB_BackUp(p_strMDBFile As String, Optional p_blnMsg As Boolean) As Boolean
    ' Comments  :
    ' Parameters: p_strMDBFile = Filename with fully qualified path
    '             p_blnMsg -
    ' Returns   : Boolean -
    '
    ' Modified  : 04/12/2005 TPM
    ' --------------------------------------------------

    On Error GoTo PROC_ERR

    Dim strMsg          As String
    Dim strPath         As String
    Dim strFileName     As String

    strPath = GetDataPath(p_strMDBFile)


    'Assume Success
    g_blnDB_BackUp = True

    strFileName = Replace(p_strMDBFile, strPath, vbNullString, , , vbTextCompare)

    FileCopy strPath & strFileName, strPath & strFileName & ".bak"

    If p_blnMsg Then
        strMsg = "Database Backup Successful!" & vbCrLf & vbCrLf & _
             strPath & strFileName & ".bak" & vbCrLf

        MsgBox strMsg, vbInformation, m_cstrMOD_TITLE

    End If


PROC_EXIT:
    Exit Function

PROC_ERR:
    g_blnDB_BackUp = False
    strMsg = Err.Description & " " & Err.Number
    MsgBox strMsg, vbExclamation, m_cstrMOD_TITLE
    Resume PROC_EXIT


End Function

Public Function g_blnOutput_CSV(p_strDbFile As String) As Boolean
    ' Comments  :
    ' Parameters: p_strDbFile = Filename with fully qualified path
    '             p_strTblName -
    '
    ' Modified  : 04/12/2005 TPM
    ' --------------------------------------------------
    On Error GoTo PROC_ERR

    Dim pconDataBase        As New ADODB.Connection
    Dim pcmdDataBase        As New ADODB.Command
    Dim prstDataBase        As New ADODB.Recordset
    Dim objADOX             As New ADOX.Catalog

    Dim objTable            As ADOX.Table
    Dim objTables           As ADOX.Tables

    Dim flds                As Fields
    Dim fld                 As Field

    Dim lngI                As Long
    Dim lngN                As Long
    Dim lngR                As Long
    Dim lngS                As Long
    Dim lngFile             As Long

    Dim strCSV              As String
    Dim strPath             As String
    Dim strMsg              As String

    Dim strFields           As String


    ' xx Assume Success
    g_blnOutput_CSV = True

    strPath = GetDataPath(p_strDbFile)

    pconDataBase.Mode = adModeShareDenyNone
    pconDataBase.CursorLocation = adUseClient
    pconDataBase.Provider = "Microsoft.Jet.OLEDB.4.0"
    pconDataBase.ConnectionString = "Data Source=" & p_strDbFile & ";Persist Security Info=False"
    pconDataBase.Open

    Set pcmdDataBase.ActiveConnection = pconDataBase
    Set objADOX.ActiveConnection = pconDataBase

    Set objTables = objADOX.Tables

        For Each objTable In objTables

            If InStr(1, objTable.Name, "MSys", vbTextCompare) <= 0 Then

                    strCSV = vbNullString

                    pcmdDataBase.CommandType = adCmdTable
                    pcmdDataBase.CommandText = objTable.Name
                    prstDataBase.Open pcmdDataBase

                    If Not prstDataBase.RecordCount >= 1 Then

                        Set objADOX = Nothing
                        Set objTables = Nothing
                        Set flds = Nothing
                        Set fld = Nothing
                        Set pconDataBase = Nothing
                        Set pcmdDataBase = Nothing
                        Set prstDataBase = Nothing
                        Exit Function
                    End If

                    If Not prstDataBase.EOF Then
                        prstDataBase.MoveFirst
                    End If


                    lngN = 0
                    Set flds = prstDataBase.Fields
                    lngI = prstDataBase.Fields.Count - 1

                    For lngN = 0 To lngI

                        Set fld = flds.Item(lngN)

                        If Not lngN = lngI Then
                            strFields = strFields & fld.Name & ","
                        Else
                            strFields = strFields & fld.Name

                        End If

                    Next

                    'Add the field names
                    strCSV = strFields & vbCrLf

                    strFields = vbNullString
                    lngR = prstDataBase.RecordCount
                    lngI = prstDataBase.Fields.Count - 1
                    lngN = 1
                    lngS = 0

                    'Add the records
                    For lngN = 1 To lngR

                        For lngS = 0 To lngI
                            Set fld = flds.Item(lngS)

                            If Not lngS = lngI Then
                                strFields = strFields & fld.Value & ","
                            Else
                                strFields = strFields & fld.Value

                            End If

                        Next

                        strCSV = strCSV & strFields & vbCrLf

                        strFields = vbNullString
                        prstDataBase.MoveNext

                    Next


                    'Create the file
                    lngFile = FreeFile

                    Open strPath & objTable.Name & ".csv" For Output As #lngFile

                        Print #lngFile, strCSV

                    Close #lngFile

                    MsgBox "Successfully Output CSV File for Table: " & objTable.Name, vbInformation, m_cstrMOD_TITLE

                    prstDataBase.Close


            End If

        Next

    pconDataBase.Close


PROC_EXIT:
    'Clean up
    Set objADOX = Nothing
    Set objTables = Nothing
    Set flds = Nothing
    Set fld = Nothing
    Set pconDataBase = Nothing
    Set pcmdDataBase = Nothing
    Set prstDataBase = Nothing
    Exit Function

PROC_ERR:
    pconDataBase.Close
    prstDataBase.Close
    g_blnOutput_CSV = False
    strMsg = Err.Description & " " & Err.Number
    MsgBox strMsg, vbExclamation, m_cstrMOD_TITLE
    Resume PROC_EXIT

End Function

Function GetDataPath(p_strFile As String) As String
    ' Comments  :
    ' Parameters: p_strFile -
    ' Returns   : String -
    '
    ' Modified  : 04/12/2005 TPM
    '
    ' --------------------------------------------------

    On Error GoTo PROC_ERR

    Dim lngL            As Long
    Dim lngN            As Long
    Dim lngI            As Long

    Dim strMsg          As String

    lngL = Len(p_strFile)

    For lngN = 1 To lngL

        If Mid(p_strFile, lngN, 1) = "\" Then
            lngI = lngN
        End If

    Next

    GetDataPath = Left(p_strFile, lngI)

PROC_EXIT:
    Exit Function

PROC_ERR:
    GetDataPath = "C:\"
    strMsg = Err.Description & " " & Err.Number
    MsgBox strMsg, vbExclamation, m_cstrMOD_TITLE
    Resume PROC_EXIT

End Function


