Imports BSMyLoadersLog.LoadersClass
Public Class frmAddWad
    Sub AutoLoad()
        Try
            Dim ObjAF As New AutoFillCollections.ShotGun
            txtManu.AutoCompleteCustomSource = ObjAF.List_SG_WAD_Manufacturer
            txtWAD.AutoCompleteCustomSource = ObjAF.List_SG_WAD_WAD
            txtPrice.AutoCompleteCustomSource = ObjAF.List_SG_WAD_Price
        Catch ex As Exception
            Call LogError(Me.Name, "AutoLoad", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    
    Sub SaveData()
        Try
            Dim strManu As String = FluffContent(txtManu.Text)
            Dim strName As String = FluffContent(txtWAD.Text)
            Dim intQty As Integer = nudQty.Value
            Dim dPrice As Double = FluffContent(txtPrice.Text, 0)
            Dim sLoad As String = FluffContent(txtLoad.Text, "0")
            Dim dLoad As Double = ConvertOZToDouble(sLoad)
            Dim GName As String = cmdGauge.Text
            Dim GID As Integer = cmdGauge.SelectedValue
            Dim eppw As Double = 0
            If Not IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(strName, "Name", Me.Text) Then Exit Sub
            If dPrice <> 0 Then
                eppw = (dPrice / intQty)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "INSERT INTO List_SG_WAD (Manufacturer,WAD,Qty,Price,eppw,gauge,GID,load_t,load_d) VALUES('" & _
                            strManu & "','" & strName & "'," & intQty & "," & dPrice & "," & eppw & _
                            ",'" & GName & "'," & GID & ",'" & sLoad & "'," & dLoad & ")"
            Obj.ConnExec(SQL)
            Dim sAns As String = MsgBox(strManu & " " & strName & " was added to the database." & _
                                Chr(10) & "Do you wish to add another?", MsgBoxStyle.YesNo, Me.Text)
            If sAns = vbNo Then
                Me.Close()
            Else
                txtManu.Text = ""
                txtWAD.Text = ""
                txtLoad.Text = ""
                nudQty.Value = 0
                txtPrice.Text = ""
                Call AutoLoad()
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmAddWad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.List_SG_GaugeTableAdapter.Fill(Me.MLLDataSet.List_SG_Gauge)
        Call AutoLoad()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
End Class