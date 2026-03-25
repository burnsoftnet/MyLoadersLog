Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Helpers

Public Class frmEditPowder
    Public PID As Long
    Public FromView As Boolean
    Sub LoadData()
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim SQL As String = "Select * from General_Powder where ID=" & PID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            Dim iQty As Double = 0
            Dim eppo As Double = 0
            Dim dPrice As Double = 0
            While RS.Read
                If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = GeneralHelpers.UnFluffContent(RS("Manufacturer"))
                If Not IsDBNull(RS("Name")) Then txtName.Text = GeneralHelpers.UnFluffContent(RS("Name"))
                If Not IsDBNull(RS("Notes")) Then txtNotes.Text = GeneralHelpers.UnFluffContent(RS("Notes"))
                If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                If Not IsDBNull(RS("weightgn")) Then iQty = RS("weightgn")
                If Not IsDBNull(RS("eppp")) Then eppo = RS("eppp")
                'If Not IsDBNull(RS("weightlbs")) Then txtwei.Text = RS("weightlbs")
                txtwei.Text = Math.Round(iQty / WeightValues.WEIGHT_GRAINS_1LBS, 3)
                dPrice = eppo * iQty
                txtGrains.Text = iQty
                Dim ObjIM As New InventoryMath
                txtPrice.Text = ObjIM.ConvertToDollars(dPrice)
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
            Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
            Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
            Dim strWei As String = GeneralHelpers.FluffContent(txtwei.Text)
            Dim dbWeiGrn As Double = GeneralHelpers.FluffContent(txtGrains.Text, 0) 'ConvertWeight(CDbl(strWei), WeightType.Grains, WeightType.Pounds)
            Dim dbPrice As Double = GeneralHelpers.FluffContent(txtPrice.Text, 0)
            Dim strNotes As String = GeneralHelpers.FluffContent(txtNotes.Text)
            If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strName, "Name", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strWei, "Weight", Me.Text) Then Exit Sub
            Dim EstCostPerItem As Double = 0
            If dbPrice <> 0 And dbWeiGrn > 0 Then
                EstCostPerItem = (dbPrice / dbWeiGrn)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "UPDATE General_Powder set Manufacturer='" & strManu & "',Name='" & strName & "'," & _
                                "weightlbs=" & strWei & ",weightgn=" & dbWeiGrn & "," & _
                                "Price=" & dbPrice & ",Notes='" & strNotes & "',eppp=" & EstCostPerItem & " where ID=" & PID
            Obj.ConnExec(SQL)
            If FromView Then Call frmView_List_Powder.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmEditPowder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
End Class