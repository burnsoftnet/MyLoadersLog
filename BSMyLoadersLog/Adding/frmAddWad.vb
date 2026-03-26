Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers

Public Class frmAddWad
    ''' <summary>
    ''' The error out
    ''' </summary>
    Dim errOut as String
    Sub AutoLoad()
        Try
            'Dim ObjAF As New AutoFillCollections.ShotGun
            'txtManu.AutoCompleteCustomSource = ObjAF.List_SG_WAD_Manufacturer
            'txtWAD.AutoCompleteCustomSource = ObjAF.List_SG_WAD_WAD
            'txtPrice.AutoCompleteCustomSource = ObjAF.List_SG_WAD_Price
            txtManu.AutoCompleteCustomSource = ConfigShotgun.WadManufacturer(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtWAD.AutoCompleteCustomSource = ConfigShotgun.Wads(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtPrice.AutoCompleteCustomSource = ConfigShotgun.WadPrice(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
        Catch ex As Exception
            Call LogError(Me.Name, "AutoLoad", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    
    Sub SaveData()
        Try
            Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
            Dim strName As String = GeneralHelpers.FluffContent(txtWAD.Text)
            Dim intQty As Integer = nudQty.Value
            Dim dPrice As Double = GeneralHelpers.FluffContent(CDbl(txtPrice.Text), 0)
            Dim sLoad As String = GeneralHelpers.FluffContent(txtLoad.Text, "0")
            Dim dLoad As Double = Converters.ConvertOZToDouble(sLoad, errOut)
            Dim GName As String = cmdGauge.Text
            Dim GID As Integer = cmdGauge.SelectedValue
            Dim eppw As Double = 0
            If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strName, "Name", Me.Text) Then Exit Sub
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