Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmAddDataSheet_RiflePistols_CFG
    Public FromView As Boolean
    Public FID As Long
    Sub LoadAutoFill()
        Try
            Dim ObjAF As New AutoFillCollections
            txtGroup.AutoCompleteCustomSource = ObjAF.Loaders_Log_NSG_GroupSize
            txtCon.AutoCompleteCustomSource = ObjAF.Loaders_Log_NSG_conditions
            txtLen.AutoCompleteCustomSource = ObjAF.Loaders_Log_NSG_tl
        Catch ex As Exception
            Call LogError(Me.Name, "LoadAutoFill", Err.Number, ex.Message.ToString)
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
            Dim ConfigID As Long = cmbConfig.SelectedValue
            Dim ConfigName As String = cmbConfig.Text
            Dim strCond As String = FluffContent(txtCon.Text)
            Dim strLen As String = FluffContent(txtLen.Text)
            Dim strNotes As String = FluffContent(txtNotes.Text)
            Dim strBarLen As String = ""
            Dim PowName As String = ""
            Dim PowWei As Double = 0
            Dim PowManu As String = ""
            Dim BulManu As String = ""
            Dim BulName As String = ""
            Dim BulWei As String = ""
            Dim PriManu As String = ""
            Dim PriName As String = ""
            Dim CaseName As String = ""
            Dim CaseManu As String = ""
            Dim CaseStatus As String = ""
            Dim Caliber As String = ""
            Dim Obj As New BSDatabase
            Dim ObjIM As New InventoryMath
            Dim ObjGF As New GlobalFunctions
            Dim PrefferedPowderID As Long = ObjIM.GetPrefNSGPowderID(ConfigID, PowWei)
            Call ObjGF.GetFirearmDetails(lngFID, 0, "", "", "", "", strBarLen)
            Dim SQL As String = "SELECT * from Config_List_Data_NSG where CLNID=" & ConfigID
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                Caliber = ObjIM.GetCaliber(RS("CALID"))
                Call ObjIM.LoadBulletInfo(RS("BID"), BulManu, BulName, "", _
                        BulWei)
                Call ObjIM.LoadPrimerInfo(RS("PRID"), PriManu, PriName)
                Call ObjIM.LoadCaseInfo(RS("CAID"), CaseManu, CaseName, "", CaseStatus)
                Call ObjIM.GetPowderDetails(PrefferedPowderID, PowManu, PowName)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            If CLng(CaseStatus) = 0 Then
                CaseStatus = "(NEW)"
            Else
                CaseStatus = "(USED)"
            End If
            SQL = "INSERT INTO Loaders_Log_NSG (fid,dt,yds,gs,ns,pwm,bullet," & _
                    "primer,case,conditions,tl,notes,ConfigName,FirearmName,Caliber,BarrelLen)" & _
                    " VALUES (" & lngFID & ",'" & strDateTested & "'," & lngYards & _
                    ",'" & strGroup & "'," & lngNumShots & ",'" & FluffContent(PowName & " - " & PowWei & _
                    " - " & PowManu) & "','" & FluffContent(BulManu & " " & BulName) & " (" & BulWei & ")" & _
                    "','" & PriManu & " " & PriName & "','" & CaseManu & " " & CaseName & " " & _
                    CaseStatus & "','" & strCond & "','" & strLen & "','" & strNotes & "','" & _
                    ConfigName & "','" & strFireArm & "','" & Caliber & "','" & strBarLen & "')"
            Obj.ConnExec(SQL)
            MsgBox("Information was saved to the Loaders Log!")
            If FromView Then Call frmViewDataSheet_RiflePistols.LoadDataCur()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddDataSheet_RiflePistols_CFG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Loaders_Log_FirearmsTableAdapter.FillByFullName(Me.MLLDataSet.Loaders_Log_Firearms)
            cmbFirearm.SelectedValue = FID
            Call UpdateConfigList()
            Call LoadAutoFill()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
    Sub UpdateConfigList()
        Dim ObjGF As New GlobalFunctions
        Dim lngFID As Long = cmbFirearm.SelectedValue
        Dim strFireArm As String = ""
        Dim strCal As String = ""
        Call ObjGF.GetFirearmDetails(lngFID, 0, "", "", "", strCal)
        Dim CalID As Long = ObjGF.GetCaliberID(strCal)
        Me.ConfigList_SimpleTableAdapter.FillBy_Caliber(Me.MLLDataSet.ConfigList_Simple, CalID)
    End Sub
    Private Sub cmbFirearm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFirearm.SelectedIndexChanged
        Call UpdateConfigList()
    End Sub
End Class