Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Imports BurnSoft.Applications.MLL.Helpers
Imports BSMyLoadersLog.Viewing

Public Class frmEditFirearm
    Public FID As Long
    Public FromView As Boolean
    Sub LoadData()
        Try
            Dim Obj As New BSDatabase
            Dim iExclude As Integer = 0
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT * from Loaders_Log_Firearms where ID=" & FID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                If Not IsDBNull(RS("Manu")) Then txtManu.Text = GeneralHelpers.UnFluffContent(RS("Manu"))
                If Not IsDBNull(RS("Model")) Then txtModel.Text = GeneralHelpers.UnFluffContent(RS("Model"))
                If Not IsDBNull(RS("SerialNo")) Then txtSerial.Text = GeneralHelpers.UnFluffContent(RS("SerialNo"))
                If Not IsDBNull(RS("Cal")) Then txtCal.Text = GeneralHelpers.UnFluffContent(RS("Cal"))
                If Not IsDBNull(RS("Barrel")) Then txtBarrel.Text = GeneralHelpers.UnFluffContent(RS("Barrel"))
                If Not IsDBNull(RS("GType")) Then txtType.Text = GeneralHelpers.UnFluffContent(RS("GType"))
                If Not IsDBNull(RS("exclude")) Then iExclude = RS("exclude")
                If iExclude = 1 Then chkExlude.Checked = True
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
            Dim strModel As String = GeneralHelpers.FluffContent(txtModel.Text)
            Dim strSerial As String = GeneralHelpers.FluffContent(txtSerial.Text)
            Dim strCal As String = GeneralHelpers.FluffContent(txtCal.Text)
            Dim strBarrel As String = GeneralHelpers.FluffContent(txtBarrel.Text)
            Dim strType As String = GeneralHelpers.FluffContent(txtType.Text)
            Dim MGCID As Integer = 0
            Dim iExclude As Integer = 0
            If chkExlude.Checked Then iExclude = 1

            If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strModel, "model", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strSerial, "Serial Number", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strCal, "Caliber", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strType, "Type", Me.Text) Then Exit Sub

            Dim strFullName As String = strManu & " " & strModel
            Dim Obj As New BSDatabase
            Dim SQL As String = "UPDATE Loaders_Log_Firearms set FullName='" & strFullName & "'," & _
                        "Manu='" & strManu & "',Model='" & strModel & "',Cal='" & strCal & "',Barrel='" & strBarrel & "'" & _
                        ",SerialNo='" & strSerial & "',GType='" & strType & "',exclude=" & iExclude & _
                        " where ID=" & FID
            Obj.ConnExec(SQL)
            If FromView Then Call FrmViewListFirearms.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmEditFirearm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Call SaveData()
    End Sub
End Class