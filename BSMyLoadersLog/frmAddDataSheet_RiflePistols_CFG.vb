Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc

Public Class FrmAddDataSheetRiflePistolsCfg
    Public FromView As Boolean
    Public Fid As Long
    Sub LoadAutoFill()
        Try
            Dim objAf As New AutoFillCollections
            txtGroup.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_GroupSize
            txtCon.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_conditions
            txtLen.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_tl
        Catch ex As Exception
            Call LogError(Name, "LoadAutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim lngFid As Long = cmbFirearm.SelectedValue
            Dim strFireArm As String = cmbFirearm.Text
            Dim strDateTested As String = dtpTested.Value
            Dim strGroup As String = FluffContent(txtGroup.Text)
            Dim lngNumShots As Long = nudShots.Value
            Dim lngYards As Long = nudYards.Value
            Dim configId As Long = cmbConfig.SelectedValue
            Dim configName As String = cmbConfig.Text
            Dim strCond As String = FluffContent(txtCon.Text)
            Dim strLen As String = FluffContent(txtLen.Text)
            Dim strNotes As String = FluffContent(txtNotes.Text)
            Dim strBarLen As String = ""
            Dim powName As String = ""
            Dim powWei As Double = 0
            Dim powManu As String = ""
            Dim bulManu As String = ""
            Dim bulName As String = ""
            Dim bulWei As String = ""
            Dim priManu As String = ""
            Dim priName As String = ""
            Dim caseName As String = ""
            Dim caseManu As String = ""
            Dim caseStatus As String = ""
            Dim caliber As String = ""
            Dim obj As New BSDatabase
            Dim objIm As New InventoryMath
            Dim objGf As New GlobalFunctions
            Dim prefferedPowderId As Long = objIm.GetPrefNSGPowderID(configId, powWei)
            Call objGf.GetFirearmDetails(lngFid, 0, "", "", "", "", strBarLen)
            Dim sql As String = "SELECT * from Config_List_Data_NSG where CLNID=" & configId
            Call obj.ConnectDB()
            Dim cmd As New OdbcCommand(sql, obj.Conn)
            Dim rs As OdbcDataReader
            rs = cmd.ExecuteReader
            While rs.Read
                caliber = objIm.GetCaliber(rs("CALID"))
                Call objIm.LoadBulletInfo(rs("BID"), bulManu, bulName, "", _
                        bulWei)
                Call objIm.LoadPrimerInfo(rs("PRID"), priManu, priName)
                Call objIm.LoadCaseInfo(rs("CAID"), caseManu, caseName, "", caseStatus)
                Call objIm.GetPowderDetails(prefferedPowderId, powManu, powName)
            End While
            rs.Close()

            If CLng(caseStatus) = 0 Then
                caseStatus = "(NEW)"
            Else
                caseStatus = "(USED)"
            End If
            sql = "INSERT INTO Loaders_Log_NSG (fid,dt,yds,gs,ns,pwm,bullet," & _
                    "primer,case,conditions,tl,notes,ConfigName,FirearmName,Caliber,BarrelLen)" & _
                    " VALUES (" & lngFid & ",'" & strDateTested & "'," & lngYards & _
                    ",'" & strGroup & "'," & lngNumShots & ",'" & FluffContent(powName & " - " & powWei & _
                    " - " & powManu) & "','" & FluffContent(bulManu & " " & bulName) & " (" & bulWei & ")" & _
                    "','" & priManu & " " & priName & "','" & caseManu & " " & caseName & " " & _
                    caseStatus & "','" & strCond & "','" & strLen & "','" & strNotes & "','" & _
                    configName & "','" & strFireArm & "','" & caliber & "','" & strBarLen & "')"
            obj.ConnExec(sql)
            MsgBox("Information was saved to the Loaders Log!")
            If FromView Then Call frmViewDataSheet_RiflePistols.LoadDataCur()
            Close()
        Catch ex As Exception
            Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddDataSheet_RiflePistols_CFG_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            'Replaced to narrow down none shotgun vs shotgun
            'Loaders_Log_FirearmsTableAdapter.FillByFullName(MLLDataSet.Loaders_Log_Firearms)
            Loaders_Log_FirearmsTableAdapter.FillByFullNameNoneShotgun(MLLDataSet.Loaders_Log_Firearms)
            cmbFirearm.SelectedValue = Fid
            Call UpdateConfigList()
            Call LoadAutoFill()
        Catch ex As Exception
            Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
    Sub UpdateConfigList()
        Dim objGf As New GlobalFunctions
        Dim lngFid As Long = cmbFirearm.SelectedValue
        Dim strCal As String = ""
        Call objGf.GetFirearmDetails(lngFid, 0, "", "", "", strCal)
        Dim calId As Long = objGf.GetCaliberID(strCal)
        ConfigList_SimpleTableAdapter.FillBy_Caliber(MLLDataSet.ConfigList_Simple, calId)
    End Sub
    Private Sub cmbFirearm_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFirearm.SelectedIndexChanged
        Call UpdateConfigList()
    End Sub

    Private Sub txtGroup_TextChanged(sender As Object, e As EventArgs) Handles txtGroup.TextChanged

    End Sub
End Class