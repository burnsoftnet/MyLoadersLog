Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmAddQtyPowder
    Public PID As Long
    Public FromView As Boolean
    Function PricePerItem(ByVal lValue As Long, ByVal dPrice As Double, ByVal sType As String) As Double
        Dim dAns As Double = 0
        Dim ObjIM As New InventoryMath
        Dim lNewValue As Long = 0
        Select Case sType
            Case "Grains (grs)"
                lNewValue = lValue
            Case "Pounds (lbs)"
                lNewValue = lValue * WEIGHT_GRAINS_1LBS
        End Select
        If lValue > 0 Then
            dAns = dPrice / lNewValue
        End If
        ObjIM.ConvertToDollars(dAns)
        Return dAns
    End Function
    Sub LoadData()
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim SQL As String = "Select * from General_Powder where ID=" & PID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            Dim iQty As Integer = 0
            Dim eppo As Double = 0
            Dim dPrice As Double = 0
            While RS.Read
                If Not IsDBNull(RS("weightlbs")) Then txtCQty.Text = RS("weightlbs")
                If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                If Not IsDBNull(RS("weightgn")) Then iQty = RS("weightgn")
                If Not IsDBNull(RS("eppp")) Then eppo = RS("eppp")
                txtCQty2.Text = iQty
                txtCPPI.Text = eppo
                Dim ObjIM As New InventoryMath
                txtCPrice.Text = ObjIM.ConvertToDollars(dPrice)
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
            Dim CQty As Double = CDbl(FluffContent(txtCQty.Text, 0))
            Dim CGrains As Double = CDbl(FluffContent(txtCQty2.Text, 0))
            Dim CPrice As Double = CDbl(FluffContent(txtCPrice.Text, 0))
            Dim CPPI As Double = CDbl(FluffContent(txtCPPI.Text, 0))
            Dim UQty As Double = CDbl(FluffContent(txtUQty.Text, 0))
            Dim UPrice As Double = CDbl(FluffContent(txtUPrice.Text, 0))
            Dim sType As String = cmbWei.Text
            Dim UPPI As Double = PricePerItem(UQty, UPrice, sType)
            Dim UGrains As Double = 0
            Dim UPounds As Double = 0
            txtUPPI.Text = UPPI
            If Not IsRequired(UQty, "New Weight", Me.Text) Then Exit Sub
            If Not IsRequired(UPrice, "Update Price", Me.Text) Then Exit Sub

            If LCase(sType) = "grains (grs)" Then
                UGrains = CDbl(UQty)
                UPounds = CDbl(ConvertWeight(UQty, WeightType.Pounds, WeightType.Grains))
            Else
                UPounds = CDbl(UQty)
                UGrains = ConvertWeight(UQty, WeightType.Grains, WeightType.Pounds)
            End If

            Dim NGrains As Double = CGrains + UGrains
            Dim NPounds As Double = CQty + UPounds
            Dim NPrice As Double = (CGrains * CPPI) + UPrice
            Dim NPPI As Double = NPrice / NGrains
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            If CPPI = UPPI Then
                SQL = "UPDATE General_Powder set weightlbs=" & NPounds & ", weightgn=" & NGrains & ", Price=" & NPrice & " where ID=" & PID
            ElseIf UPrice = 0 And UQty = 0 Then
                SQL = "UPDATE General_Powder set weightlbs=0,weightgn=0, Price=0, eppp=0 where ID=" & PID
            Else
                SQL = "UPDATE General_Powder set weightlbs=" & NPounds & ", weightgn=" & NGrains & ", eppp=" & NPPI & ", Price=" & NPrice & " where ID=" & PID
            End If
            Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Call SaveData()
        If FromView Then Call frmView_List_Powder.LoadData()
        Me.Close()
    End Sub
    Private Sub btnViewCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewCalc.Click
        txtUPPI.Text = PricePerItem(CLng(txtUQty.Text), CDbl(txtUPrice.Text), cmbWei.Text)
    End Sub

    Private Sub frmAddQtyPowder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
End Class