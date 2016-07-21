Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmEditShells
    Public SID As Long
    Public FromView As Boolean
    Sub LoadData()
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT * from List_Case where ID=" & SID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            Dim TimesUsed As Long = 0
            Dim iQty As Integer = 0
            Dim eppo As Double = 0
            Dim dPrice As Double = 0
            While RS.Read
                If Not IsDBNull(RS("cid")) Then
                    cmbCal.SelectedValue = RS("cid")
                    cmbCal.Update()
                End If
                If RS("isnew") = 1 Then
                    chkNew.Checked = True
                    TimesUsed = 0
                Else
                    chkNew.Checked = False
                    TimesUsed = 0
                End If
                If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = UnFluffContent(RS("Manufacturer"))
                If Not IsDBNull(RS("Name")) Then txtName.Text = UnFluffContent(RS("Name"))
                If Not IsDBNull(RS("ttl")) Then txtTTL.Text = UnFluffContent(RS("ttl"))
                If Not IsDBNull(RS("TimesUsed")) Then nudUsed.Value = RS("TimesUsed")
                If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                If Not IsDBNull(RS("Qty")) Then iQty = RS("Qty")
                If Not IsDBNull(RS("ePPC")) Then eppo = RS("ePPC")
                dPrice = eppo * iQty
                nudQty.Value = iQty
                Dim ObjIM As New InventoryMath
                txtPrice.Text = ObjIM.ConvertToDollars(dPrice)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Call Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "loadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim strManu As String = FluffContent(txtManu.Text)
            Dim strName As String = FluffContent(txtName.Text)
            Dim strTrim As Double = FluffContent(txtTTL.Text, 0)
            Dim intNew As Integer = 0
            Dim intUsed As Integer = nudUsed.Value
            Dim intQty As Integer = nudQty.Value
            Dim dbPrice As Double = FluffContent(txtPrice.Text, 0)
            Dim LngCalID As Long = cmbCal.SelectedValue
            If Not IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(strName, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(strTrim, "Trim to Lenght", Me.Text) Then Exit Sub
            If chkNew.Checked Then intNew = 1
            Dim EstCostPerItem As Double = 0
            If dbPrice <> 0 And intQty > 0 Then
                EstCostPerItem = (dbPrice / intQty)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "UPDATE List_Case set Manufacturer='" & strManu & "',Name='" & strName & "'" & _
                    ",ttl='" & strTrim & "',IsNew=" & intNew & ", TimesUsed=" & intUsed & ", ePPC=" & EstCostPerItem & ", " & _
                    "Qty=" & intQty & ",Price=" & dbPrice & ",CID=" & LngCalID & _
                    " where id=" & SID
            Obj.ConnExec(SQL)
            If FromView Then Call frmView_List_Shells.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmEditShells_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.List_CalibersTableAdapter.Fill(Me.MLLDataSet.List_Calibers)
        Call LoadData()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Call SaveData()
    End Sub
End Class