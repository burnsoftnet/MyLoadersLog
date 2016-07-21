Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmEditSlug
    Public FromView As Boolean
    Public SID As Long
    Sub LoadData()
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT * from List_SG_ShotType_Details where ID=" & SID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = RS("Manufacturer")
                If Not IsDBNull(RS("Name")) Then txtName.Text = RS("Name")
                If Not IsDBNull(RS("cal")) Then txtCal.Text = RS("cal")
                If Not IsDBNull(RS("Qty")) Then nudQty.Value = RS("Qty")
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
            Dim CAL As String = FluffContent(txtCal.Text)
            Dim Qty As Integer = nudQty.Value
            Dim Weight As String = FluffContent(txtPounds.Text)
            Dim Cost As Double = FluffContent(txtPrice.Text, 0.0)
            Dim epps As Double = 0
            Dim SQL As String = ""
            Dim Obj As New BSDatabase

            If Not IsRequired(Manu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(Name, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(CAL, "Caliber", Me.Text) Then Exit Sub
            Dim EstCostPerItem As Double = 0
            If Cost <> 0 Then
                EstCostPerItem = (Cost / Qty)
            End If
            epps = EstCostPerItem
            SQL = "UPDATE List_SG_ShotType_Details set Manufacturer='" & Manu & _
                    "',Name='" & Name & "',CAL='" & CAL & "',QTY=" & Qty & _
                    ",weight='" & Weight & "',epps=" & epps & ",Price=" & _
                    Cost & " where ID=" & SID
            Obj.ConnExec(SQL)
            If FromView Then Call frmView_List_Slug.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmEditSlug_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Call SaveData()
    End Sub
End Class