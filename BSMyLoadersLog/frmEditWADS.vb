Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmEditWADS
    Public SID As Integer
    Public FromView As Boolean
    Sub Loaddata()
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT * from List_SG_WAD where ID=" & SID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            Dim TimesUsed As Long = 0
            Dim iQty As Integer = 0
            Dim eppo As Double = 0
            Dim dPrice As Double = 0
            Dim GID As Integer
            While RS.Read
                If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = UnFluffContent(RS("Manufacturer"))
                If Not IsDBNull(RS("WAD")) Then txtWAD.Text = UnFluffContent(RS("WAD"))
                If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                If Not IsDBNull(RS("Qty")) Then iQty = RS("Qty")
                If Not IsDBNull(RS("eppw")) Then eppo = RS("eppw")
                If Not IsDBNull(RS("load_t")) Then txtLoad.Text = UnFluffContent(RS("load_t"))
                If Not IsDBNull(RS("gid")) Then GID = RS("GID")
                cmdGauge.SelectedValue = GID
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
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim strManu As String = FluffContent(txtManu.Text)
            Dim strName As String = FluffContent(txtWAD.Text)
            Dim intQty As Integer = nudQty.Value
            Dim dbPrice As Double = FluffContent(txtPrice.Text, 0)
            Dim sLoad As String = FluffContent(txtLoad.Text, "0")
            Dim dLoad As Double = ConvertOZToDouble(sLoad)
            Dim GName As String = cmdGauge.Text
            Dim GID As Integer = cmdGauge.SelectedValue

            If Not IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(strName, "Name", Me.Text) Then Exit Sub
            Dim EstCostPerItem As Double = 0
            If dbPrice <> 0 And intQty > 0 Then
                EstCostPerItem = (dbPrice / intQty)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "UPDATE List_SG_WAD set Manufacturer='" & strManu & "',WAD='" & strName & "'" & _
                    ",Qty=" & intQty & ",Price=" & dbPrice & ",eppw=" & EstCostPerItem & _
                    ",gauge='" & GName & "',GID=" & GID & ",load_t='" & sLoad & _
                    "',load_d=" & dLoad & " where id=" & SID
            Obj.ConnExec(SQL)
            If FromView Then Call frmView_List_WADS.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmEditWADS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.List_SG_GaugeTableAdapter.Fill(Me.MLLDataSet.List_SG_Gauge)
        Call Loaddata()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
End Class