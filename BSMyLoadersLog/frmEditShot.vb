Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
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
            Dim Manu As String = FluffContent(txtManu.Text)
            Dim Name As String = FluffContent(txtName.Text)
            Dim Mat As String = FluffContent(txtMat.Text)
            Dim ShotNo As String = FluffContent(txtShotNo.Text, "0")
            Dim Weight As String = FluffContent(txtPounds.Text, "0")
            Dim Cost As Double = FluffContent(txtPrice.Text, 0.0)
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            Dim ounces As Double = WEIGHT_OZ_1LBS * CDbl(Weight)
            Dim grams As Double = ounces * WEIGHT_GRAMS_OZ
            Dim epps As Double = 0
            If Cost > 0 Then epps = Cost / grams

            If Not IsRequired(Manu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(Name, "Name", Me.Text) Then Exit Sub

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
        If FromView Then Call frmView_List_Shot.LoadData()
        Me.Close()
    End Sub
End Class