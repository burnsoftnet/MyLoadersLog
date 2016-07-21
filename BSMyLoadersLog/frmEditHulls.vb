Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmEditHulls
    Public SID As Integer
    Public FromView As Boolean
    Sub AutoLoad()
        Try
            Dim ObjAF As New AutoFillCollections
            txtManu.AutoCompleteCustomSource = ObjAF.List_SG_Case_Manufacturer
            txtName.AutoCompleteCustomSource = ObjAF.List_SG_Case_Name
            txtLen.AutoCompleteCustomSource = ObjAF.List_SG_Case_Length
            txtPrice.AutoCompleteCustomSource = ObjAF.List_SG_Case_Price
            txtDRAM.AutoCompleteCustomSource = ObjAF.List_SG_Case_DRAM
        Catch ex As Exception
            Call LogError(Me.Name, "utoLoad", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub Loaddata()
        Try
            Me.List_SG_GaugeTableAdapter.Fill(Me.MLLDataSet.List_SG_Gauge)
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
                If Not IsDBNull(RS("GID")) Then
                    cmbGauge.SelectedValue = RS("GID")
                    cmbGauge.Update()
                End If
                If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = UnFluffContent(RS("Manufacturer"))
                If Not IsDBNull(RS("Name")) Then txtName.Text = UnFluffContent(RS("Name"))
                If Not IsDBNull(RS("Length")) Then txtLen.Text = UnFluffContent(RS("Length"))
                If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                If Not IsDBNull(RS("Qty")) Then iQty = RS("Qty")
                If Not IsDBNull(RS("epps")) Then eppo = RS("epps")
                If Not IsDBNull(RS("DRAM")) Then txtDRAM.Text = UnFluffContent(RS("DRAM"))
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
            Dim strName As String = FluffContent(txtName.Text)
            Dim strlen As String = FluffContent(txtLen.Text)
            Dim intQty As Integer = nudQty.Value
            Dim dbPrice As Double = FluffContent(txtPrice.Text, 0)
            Dim sDRAM As String = FluffContent(txtDRAM.Text)
            Dim LngGAID As Long = cmbGauge.SelectedValue
            Dim sGauge As String = cmbGauge.Text
            If Not IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(strName, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(strlen, "Length", Me.Text) Then Exit Sub
            Dim EstCostPerItem As Double = 0
            If dbPrice <> 0 And intQty > 0 Then
                EstCostPerItem = (dbPrice / intQty)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "UPDATE List_SG_Case set Manufacturer='" & strManu & "',Name='" & strName & "'" & _
                    ",Length='" & strlen & "',Gauge='" & sGauge & "', epps=" & EstCostPerItem & ", " & _
                    "Qty=" & intQty & ",Price=" & dbPrice & ",GID=" & LngGAID & _
                    ", DRAM='" & sDRAM & "' where id=" & SID
            Obj.ConnExec(SQL)
            If FromView Then Call frmView_List_ShellHulls.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmEditHulls_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call AutoLoad()
        Call Loaddata()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
End Class