Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Helpers
Imports BSMyLoadersLog.Viewing

Public Class frmEditShot
    Public BID As Long
    Public FromView As Boolean
    Sub LoadData()
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT * from List_SG_ShotType_Details where ID=" & BID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = RS("Manufacturer")
                If Not IsDBNull(RS("Name")) Then txtName.Text = RS("Name")
                If Not IsDBNull(RS("mat")) Then txtMat.Text = RS("mat")
                If Not IsDBNull(RS("ShotNo")) Then txtShotNo.Text = RS("ShotNo")
                If Not IsDBNull(RS("weight")) Then txtPounds.Text = RS("weight")
                If Not IsDBNull(RS("Price")) Then txtPrice.Text = RS("Price")
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Call Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim Manu As String = GeneralHelpers.FluffContent(txtManu.Text)
            Dim Name As String = GeneralHelpers.FluffContent(txtName.Text)
            Dim Mat As String = GeneralHelpers.FluffContent(txtMat.Text)
            Dim ShotNo As String = GeneralHelpers.FluffContent(txtShotNo.Text, "0")
            Dim Weight As String = GeneralHelpers.FluffContent(txtPounds.Text, "0")
            Dim Cost As Double = GeneralHelpers.FluffContent(txtPrice.Text, 0.0)
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            Dim ounces As Double = WeightValues.WEIGHT_OZ_1LBS * CDbl(Weight)
            Dim grams As Double = ounces * WeightValues.WEIGHT_GRAMS_OZ
            Dim epps As Double = 0
            If Cost > 0 Then epps = Cost / grams

            If Not GeneralHelpers.IsRequired(Manu, "Manufacturer", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(Name, "Name", Me.Text) Then Exit Sub

            SQL = "UPDATE List_SG_ShotType_Details set Manufacturer='" & Manu & "',Name='" & Name & _
                    "',mat='" & Mat & "',ShotNo='" & ShotNo & "',weight='" & Weight & "',Price=" & _
                    Cost & ",ounces=" & ounces & ",grams=" & grams & _
                    ",epps=" & epps & " where ID=" & BID
            Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmEditShot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Call SaveData()
        If FromView Then Call FrmViewListShot.LoadData()
        Me.Close()
    End Sub
End Class