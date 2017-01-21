﻿Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmAddQtyHulls
    Public SID As Long
    Public FromView As Boolean
    Function PricePerItem(ByVal lQty As Long, ByVal dPrice As Double) As Double
        Dim dAns As Double = 0
        Dim ObjIM As New InventoryMath
        If lQty > 0 Then
            dAns = dPrice / lQty
        End If
        ObjIM.ConvertToDollars(dAns)
        Return dAns
    End Function
    Sub LoadData()
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT * from List_SG_Case where ID=" & SID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            Dim TimesUsed As Long = 0
            Dim iQty As Integer = 0
            Dim eppo As Double = 0
            Dim dPrice As Double = 0
            While RS.Read
                If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                If Not IsDBNull(RS("Qty")) Then iQty = RS("Qty")
                If Not IsDBNull(RS("ePPs")) Then eppo = RS("ePPs")
                txtCQty.Text = iQty
                txtCPPI.Text = eppo
                Dim ObjIM As New InventoryMath
                txtCPrice.Text = ObjIM.ConvertToDollars(dPrice)
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
            Dim CQty As Long = CLng(FluffContent(txtCQty.Text, 0))
            Dim CPrice As Double = CDbl(FluffContent(txtCPrice.Text, 0))
            Dim CPPI As Double = CDbl(FluffContent(txtCPPI.Text, 0))
            Dim UQty As Long = CLng(FluffContent(txtUQty.Text, 0))
            Dim UPrice As Double = CDbl(FluffContent(txtUPrice.Text, 0))
            Dim UPPI As Double = PricePerItem(UQty, UPrice)
            txtUPPI.Text = UPPI
            If Not IsRequired(UQty, "Update Qty", Me.Text) Then Exit Sub
            If Not IsRequired(UPrice, "Update Price", Me.Text) Then Exit Sub

            Dim NQty As Long = CQty + UQty
            Dim NPrice As Double = (CQty * CPPI) + UPrice
            Dim NPPI As Double = PricePerItem(NQty, NPrice)
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            If CPPI = UPPI Then
                SQL = "UPDATE List_SG_Case set QTY=" & NQty & ", Price=" & NPrice & " where ID=" & SID
            ElseIf UPrice = 0 And UQty = 0 Then
                SQL = "UPDATE List_SG_Case set QTY=0, Price=0, epps=0 where ID=" & SID
            Else
                SQL = "UPDATE List_SG_Case set QTY=" & NQty & ", Price=" & NPrice & ", epps=" & NPPI & " where ID=" & SID
            End If
            Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddQtyHulls_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Private Sub btnViewCalc_Click(sender As System.Object, e As System.EventArgs) Handles btnViewCalc.Click
        txtUPPI.Text = PricePerItem(CLng(FluffContent(txtUQty.Text, 0)), CDbl(FluffContent(txtUPrice.Text, 0)))
    End Sub

    Private Sub btnUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdate.Click
        Try
            Call SaveData()
            If FromView Then Call frmView_List_Shells.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnUpdate.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class