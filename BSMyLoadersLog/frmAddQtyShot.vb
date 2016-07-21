Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmAddQtyShot
    Public BID As Long
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
            Dim SQL As String = "SELECT * from List_SG_ShotType_Details where ID=" & BID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            Dim TimesUsed As Long = 0
            Dim iQty As Integer = 0
            Dim eppo As Double = 0
            Dim dPrice As Double = 0
            While RS.Read
                If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                If Not IsDBNull(RS("weight")) Then iQty = RS("weight")
                txtCQty.Text = iQty
                If iQty = 0 Then dPrice = 0
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
    Private Sub frmAddQtyShot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Sub SaveData()
        Try
            '93.8 pellets per oz
            'BPI Nickel Plated Lead Shot #2 11 lb Box
            Dim CQty As Long = CLng(FluffContent(txtCQty.Text, 0))
            Dim CPrice As Double = CDbl(FluffContent(txtCPrice.Text, 0))
            Dim UQty As Long = CDbl(FluffContent(txtUQty.Text, 0))
            Dim UPrice As Double = CDbl(FluffContent(txtUPrice.Text, 0))
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            Dim NQty As Long = CQty + UQty
            Dim NPrice As Double = CPrice + UPrice
            Dim ounces As Double = WEIGHT_OZ_1LBS * NQty
            SQL = "Update List_SG_ShotType_Details set Price=" & NPrice & ", weight='" & NQty & _
                "',ounces=" & ounces & " where ID=" & BID
            Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Call SaveData()
        If FromView Then Call frmView_List_Shot.LoadData()
        Me.Close()
    End Sub
End Class