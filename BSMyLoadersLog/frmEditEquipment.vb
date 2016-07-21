Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmEditEquipment
    Public EID As Long
    Public FromView As Boolean
    Sub LoadData()
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT * from General_Equipment where ID=" & EID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = UnFluffContent(RS("Manufacturer"))
                If Not IsDBNull(RS("Name")) Then txtName.Text = UnFluffContent(RS("Name"))
                If Not IsDBNull(RS("Use")) Then txtUse.Text = UnFluffContent(RS("Use"))
                If Not IsDBNull(RS("Cost")) Then txtPrice.Text = RS("Cost")
                If Not IsDBNull(RS("Notes")) Then txtNotes.Text = UnFluffContent(RS("Notes"))
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim strManu As String = FluffContent(txtManu.Text)
            Dim strName As String = FluffContent(txtName.Text)
            Dim strUse As String = FluffContent(txtUse.Text)
            Dim strPrice As Double = FluffContent(txtPrice.Text, 0)
            Dim strNotes As String = FluffContent(txtNotes.Text)

            If Not IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(strName, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(strUse, "Use", Me.Text) Then Exit Sub
            If Not IsRequired(strPrice, 0, "Price", Me.Text) Then Exit Sub

            Dim Obj As New BSDatabase
            Dim SQL As String = "UPDATE General_Equipment set Manufacturer='" & strManu & "', Name='" & strName & "', " & _
                                "Use='" & strUse & "', Cost=" & strPrice & ", Notes='" & strNotes & "' where ID=" & EID
            Obj.ConnExec(SQL)
            Obj = Nothing
            If FromView Then Call frmView_List_Equipment.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Call SaveData()
    End Sub

    Private Sub frmEditEquipment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
End Class