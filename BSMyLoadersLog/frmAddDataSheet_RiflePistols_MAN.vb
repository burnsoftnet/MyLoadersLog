Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmAddDataSheet_RiflePistols_MAN
    Public FromView As Boolean
    Public FID As Long
    Sub LoadAutoFill()
        Try
            Dim ObjAF As New AutoFillCollections
            txtGroup.AutoCompleteCustomSource = ObjAF.Loaders_Log_NSG_GroupSize
            txtCon.AutoCompleteCustomSource = ObjAF.Loaders_Log_NSG_conditions
            txtLen.AutoCompleteCustomSource = ObjAF.Loaders_Log_NSG_tl
            txtPowName.AutoCompleteCustomSource = ObjAF.General_Powder_Name
            txtPowManu.AutoCompleteCustomSource = ObjAF.General_Powder_Manufacturer
            txtBullet.AutoCompleteCustomSource = ObjAF.Loaders_Log_NSG_Bullet
            txtPrimer.AutoCompleteCustomSource = ObjAF.Loaders_Log_NSG_primer
            txtCase.AutoCompleteCustomSource = ObjAF.Loaders_Log_NSG_case
        Catch ex As Exception
            Call LogError(Me.Name, "LoadAutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadData()
        Try
            Me.Loaders_Log_FirearmsTableAdapter.Fill(Me.MLLDataSet.Loaders_Log_Firearms)
            cmbFirearm.SelectedValue = FID
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim lngFID As Long = cmbFirearm.SelectedValue
            Dim strFireArm As String = cmbFirearm.Text
            Dim strDateTested As String = dtpTested.Value
            Dim strGroup As String = FluffContent(txtGroup.Text)
            Dim lngNumShots As Long = nudShots.Value
            Dim lngYards As Long = nudYards.Value
            Dim strPowName As String = FluffContent(txtPowName.Text)
            Dim strPowWei As String = FluffContent(txtPowWei.Text)
            Dim strPowManu As String = FluffContent(txtPowManu.Text)
            Dim strBullet As String = FluffContent(txtBullet.Text)
            Dim strPrimer As String = FluffContent(txtPrimer.Text)
            Dim strCase As String = FluffContent(txtCase.Text)
            Dim strCond As String = FluffContent(txtCon.Text)
            Dim strLen As String = FluffContent(txtLen.Text)
            Dim strNotes As String = FluffContent(txtNotes.Text)
            Dim ConfigName As String = "N/A"
            Dim strBarLen As String = ""
            Dim Caliber As String = ""
            Dim Obj As New BSDatabase
            Dim ObjIM As New InventoryMath
            Dim ObjGF As New GlobalFunctions
            Call ObjGF.GetFirearmDetails(lngFID, 0, "", "", "", Caliber, strBarLen)
            Dim Sql As String = "INSERT INTO Loaders_Log_NSG (fid,dt,yds,gs,ns,pwm,bullet," & _
                    "primer,case,conditions,tl,notes,ConfigName,FirearmName,Caliber,BarrelLen)" & _
                    " VALUES (" & lngFID & ",'" & strDateTested & "'," & lngYards & _
                    ",'" & strGroup & "'," & lngNumShots & ",'" & strPowName & " - " & strPowWei & _
                    " - " & strPowManu & "','" & strBullet & _
                    "','" & strPrimer & "','" & strCase & _
                    "','" & strCond & "','" & strLen & "','" & strNotes & "','" & _
                    ConfigName & "','" & strFireArm & "','" & Caliber & "','" & strBarLen & "')"
            Obj.ConnExec(Sql)
            MsgBox("Information was saved to the Loaders Log!")
            If FromView Then Call frmViewDataSheet_RiflePistols.LoadDataCur()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddDataSheet_RiflePistols_MAN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
        Call LoadAutoFill()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
End Class