Imports BSMyLoadersLog.LoadersClass
''' <summary>
''' Add Data sheet for Rifle or Pistol, aka metallic reloading data sheet
''' </summary>
Public Class FrmAddDataSheetRiflePistolsMan
    ''' <summary>
    ''' This is from a view
    ''' </summary>
    Public FromView As Boolean
    ''' <summary>
    ''' Firearm Id
    ''' </summary>
    Public Fid As Long
    ''' <summary>
    ''' Load the Auto Fill Fields
    ''' </summary>
    Sub LoadAutoFill()
        Try
            Dim objAf As New AutoFillCollections
            txtGroup.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_GroupSize
            txtCon.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_conditions
            txtLen.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_tl
            txtPowName.AutoCompleteCustomSource = objAf.General_Powder_Name
            txtPowManu.AutoCompleteCustomSource = objAf.General_Powder_Manufacturer
            txtBullet.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_Bullet
            txtPrimer.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_primer
            txtCase.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_case
        Catch ex As Exception
            Call LogError(Name, "LoadAutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Will the data table with durrent log information
    ''' </summary>
    Sub LoadData()
        Try
            Loaders_Log_FirearmsTableAdapter.Fill(MLLDataSet.Loaders_Log_Firearms)
            cmbFirearm.SelectedValue = Fid
        Catch ex As Exception
            Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Save the informamtion to the database
    ''' </summary>
    Sub SaveData()
        Try
            Dim lngFid As Long = cmbFirearm.SelectedValue
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
            Dim configName As String = "N/A"
            Dim strBarLen As String = ""
            Dim caliber As String = ""
            Dim obj As New BSDatabase
            Dim objGf As New GlobalFunctions
            Call objGf.GetFirearmDetails(lngFid, 0, "", "", "", caliber, strBarLen)
            Dim sql As String = "INSERT INTO Loaders_Log_NSG (fid,dt,yds,gs,ns,pwm,bullet," & _
                    "primer,case,conditions,tl,notes,ConfigName,FirearmName,Caliber,BarrelLen)" & _
                    " VALUES (" & lngFid & ",'" & strDateTested & "'," & lngYards & _
                    ",'" & strGroup & "'," & lngNumShots & ",'" & strPowName & " - " & strPowWei & _
                    " - " & strPowManu & "','" & strBullet & _
                    "','" & strPrimer & "','" & strCase & _
                    "','" & strCond & "','" & strLen & "','" & strNotes & "','" & _
                    configName & "','" & strFireArm & "','" & caliber & "','" & strBarLen & "')"
            obj.ConnExec(sql)
            MsgBox("Information was saved to the Loaders Log!")
            If FromView Then Call frmViewDataSheet_RiflePistols.LoadDataCur()
            Close()
        Catch ex As Exception
            Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Manual Load view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmAddDataSheet_RiflePistols_MAN_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Call LoadData()
        Call LoadAutoFill()
    End Sub
    ''' <summary>
    ''' Canel button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub
    ''' <summary>
    ''' Add Button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
End Class