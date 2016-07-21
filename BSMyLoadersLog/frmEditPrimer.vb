Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmEditPrimer
    Public PID As Long
    Sub LoadData()
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT * from General_Primer where ID=" & PID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            Dim iQty As Integer = 0
            Dim eppo As Double = 0
            Dim dPrice As Double = 0
            While RS.Read
                If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = UnFluffContent(RS("Manufacturer"))
                If Not IsDBNull(RS("Name")) Then txtName.Text = UnFluffContent(RS("Name"))
                If Not IsDBNull(RS("Primer_Type")) Then
                    cmbPriType.SelectedValue = RS("Primer_Type")
                    cmbPriType.Update()
                End If
                If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                If Not IsDBNull(RS("Qty")) Then iQty = RS("Qty")
                If Not IsDBNull(RS("eppp")) Then eppo = RS("eppp")
                dPrice = eppo * iQty
                nudQty.Value = iQty
                Dim ObjIM As New InventoryMath
                txtPrice.Text = ObjIM.ConvertToDollars(dPrice)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim strManu As String = FluffContent(txtManu.Text)
            Dim strName As String = FluffContent(txtName.Text)
            Dim intPriType As Integer = cmbPriType.SelectedValue
            Dim intQty As Integer = nudQty.Value
            Dim dbPrice As Double = FluffContent(txtPrice.Text, 0)

            If Not IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(strName, "Name", Me.Text) Then Exit Sub
            Dim EstCostPerItem As Double = 0
            If dbPrice <> 0 And intQty > 0 Then
                EstCostPerItem = (dbPrice / intQty)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "UPDATE General_Primer set Manufacturer='" & strManu & "',Name='" & strName & "'" & _
                        ", Primer_Type=" & intPriType & ",Qty=" & intQty & ",Price=" & dbPrice & _
                        ",eppp=" & EstCostPerItem & " where ID=" & PID
            Obj.ConnExec(SQL)
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmEditPrimer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.General_Primer_TypeTableAdapter.Fill(Me.MLLDataSet.General_Primer_Type)
        Me.General_PrimerTableAdapter.Fill(Me.MLLDataSet.General_Primer)
        Call LoadData()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Call SaveData()
        Call frmView_List_Primer.LoadData()
    End Sub
End Class