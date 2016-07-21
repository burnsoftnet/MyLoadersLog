Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmSearchConfig_RiflePistol
    Function ValuesStored() As Boolean
        Dim bAns As Boolean = False
        If lstQueue.Items.Count > 0 Or Len(txtString.Text) > 0 Then bAns = True
        Return bAns
    End Function
    Function ColumnsSelected() As Boolean
        Dim bans As Boolean = False
        If CheckedListBox1.CheckedItems.Count > 0 Then bans = True
        Return bans
    End Function
    Function GetColumnName(ByVal DN As String) As String
        Dim sAns As String = ""
        Try
            Dim Obj As New BSDatabase
            Dim SQL As String = "SELECT colname from Search_Fields where Dis='" & DN & "'"
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                sAns = RS("colname")
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "GetColumnName", Err.Number, ex.Message.ToString)
        End Try
        Return sAns
    End Function
    Function GenerateSQL() As String
        Dim sAns As String = ""
        If Not ValuesStored() Then MsgBox("Please type in something to search for!") : Return "" : Exit Function
        If Not ColumnsSelected() Then MsgBox("Please select the Columns that you wish to view!") : Return "" : Exit Function
        Try
            sAns = "SELECT "
            Dim DisplayName As String = ""
            Dim strColumn As String
            Dim i As Integer = 0
            For i = 0 To CheckedListBox1.CheckedItems.Count - 1
                DisplayName = CheckedListBox1.CheckedItems(i)
                strColumn = GetColumnName(DisplayName)
                If i = 0 Then
                    If DisplayName = strColumn Then
                        sAns &= strColumn & " "
                    Else
                        sAns &= strColumn & " as [" & DisplayName & "] "
                    End If
                Else
                    If DisplayName = strColumn Then
                        sAns &= "," & strColumn & " "
                    Else
                        sAns &= "," & strColumn & " as [" & DisplayName & "] "
                    End If
                End If
            Next i
            If i = 0 Then sAns &= "* "
            sAns &= " from qry_CFG_SR_PowderList where"
            If lstQueue.Items.Count = 0 Then
                If Len(txtString.Text) > 0 Then
                    Dim ColID As Long = cmbCol.SelectedValue
                    Dim sValue As String = txtString.Text
                    sAns &= " " & GenerateDetails(ColID, sValue)
                End If
            Else
                If Len(txtString.Text) > 0 Then
                    Dim ColID As Long = cmbCol.SelectedValue
                    Dim sValue As String = txtString.Text
                    lstQueue.Items.Add(GenerateDetails(ColID, sValue))
                End If
                For i = 0 To lstQueue.Items.Count - 1
                    sAns &= " " & lstQueue.Items.Item(i).ToString
                Next
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "GenerateSQL", Err.Number, ex.Message.ToString)
        End Try
        Return sAns
    End Function

    Private Sub frmSearchConfig_RiflePistol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Search_FieldsTableAdapter.Fill(Me.MLLDataSet.Search_Fields)
        Me.Height = 289
        Try
            Dim Obj As New BSDatabase
            Dim SQL As String = "Select * from Search_Fields order by Dis ASC"
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                CheckedListBox1.Items.Add(RS("Dis"))
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub GetColumnDetails(ByVal CID As Long, ByRef ColName As String, ByRef colType As String)
        Try
            Dim Obj As New BSDatabase
            Dim SQL As String = "Select * from Search_Fields where ID=" & CID
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                ColName = RS("colname")
                colType = RS("ctype")
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "GetColumnDetails", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Function GenerateDetails(ByVal ColID As Long, ByVal svalue As String) As String
        Dim sType As String = ""
        Dim colName As String = ""
        Dim sAdd As String = ""
        Call GetColumnDetails(ColID, colName, sType)
        Select Case LCase(sType)
            Case "double"
                If Not IsNumeric(svalue) Then MsgBox("Value needs to be a number!") : Return "" : Exit Function
                If lstQueue.Items.Count = 0 Then
                    sAdd = colName & "=" & svalue
                Else
                    sAdd = cmbAddOpt.Text & " " & colName & "=" & svalue
                End If
            Case "string"
                If lstQueue.Items.Count = 0 Then
                    sAdd = colName & " like '%" & svalue & "%'"
                Else
                    sAdd = cmbAddOpt.Text & " " & colName & " like '%" & svalue & "%'"
                End If
            Case "number"
                If Not IsNumeric(svalue) Then MsgBox("Value needs to be a number!") : Return "" : Exit Function
                If lstQueue.Items.Count = 0 Then
                    sAdd = colName & "=" & svalue
                Else
                    sAdd = cmbAddOpt.Text & " " & colName & "=" & svalue
                End If
        End Select
        Return sAdd
    End Function
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim ColID As Long = cmbCol.SelectedValue
            Dim sValue As String = txtString.Text
            lstQueue.Items.Add(GenerateDetails(ColID, sValue))
            cmbAddOpt.Visible = ValuesStored()
            txtString.Text = ""
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnShowSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowSQL.Click
        Me.Height = 433
        btnShowSQL.Visible = False
        btnHideSQL.Visible = True
    End Sub
    Private Sub btnHideSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHideSQL.Click
        Me.Height = 289
        btnShowSQL.Visible = True
        btnHideSQL.Visible = False
    End Sub

    Private Sub btnResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResults.Click
        If Len(txtSQL.Text) = 0 Then txtSQL.Text = GenerateSQL()
        frmReport_Custom.SQL = txtSQL.Text
        frmReport_Custom.ReportName = "Search Results"
        frmReport_Custom.FormTitle = "Search Results"
        frmReport_Custom.MdiParent = Me.MdiParent
        frmReport_Custom.Show()
    End Sub

    Private Sub btnSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSQL.Click
        txtSQL.Text = GenerateSQL()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        lstQueue.Items.Clear()
    End Sub
End Class