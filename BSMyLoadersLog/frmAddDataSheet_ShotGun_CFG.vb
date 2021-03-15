Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class FrmAddDataSheetShotGunCfg
    Public Fid As Long
    Public FromView As Boolean
    Sub LoadAutoFill()
        Try

            Dim objAf As New AutoFillCollections.ShotGun
            'Put in things that you want to autofill in this box
            txtPattern.AutoCompleteCustomSource = objAf.List_SG_Log_SG_Patterns()

        Catch ex As Exception
            Call LogError(Name, "LoadAutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim lngFid As Long = cmbFirearm.SelectedValue
            Dim strFireArm As String = cmbFirearm.Text
            Dim strdateTested As String = dtpTested.Value
            Dim strPattern As String = txtPattern.Text
            Dim lngYards As Long = nudYards.Value
            Dim configId As Long = cmbConfiguration.SelectedValue
            Dim configName As String = cmbConfiguration.Text
            Dim strNotes As String = txtNotes.Text
            Dim strBarLen As String = ""
            Dim powName As String = ""
            Dim powWei As Double = 0
            Dim powManu As String = ""
            Dim shotWt As String = ""
            Dim shotSizeNo As String = ""
            Dim hullName As String = ""
            Dim hullManufacturer As String = ""
            Dim hullLength As String = ""
            Dim hullDram As String = ""
            Dim pbm As String = ""
            Dim wad As String = ""
            Dim primerName As String = ""
            Dim priManu As String = ""
            Dim pd As String = ""
            Dim caliber As String = ""
            Dim obj As New BSDatabase
            Dim objIm As New InventoryMath
            Dim objGf As New GlobalFunctions

            Dim shotDetailsManu As String = ""
            Dim shotDetailsName As String = ""
            Dim shotDetailsQty As Double
            Dim shotDetailsEpps As Double
            Dim shotDetailsGr As Double
            Dim isSlug As Boolean = False
            Dim shotMaterial As String = ""
            Dim shotShotNo As String = ""
            Dim slugWeight As String = ""
            Dim instockShotOz As Double
            Dim prefLoad As String
            Dim shotPrefload As Double
            Dim costSlug As Double
            Dim costShot As Double
            Dim instockShot As Double
            Dim instockSlug As Double
            Dim wadManu As String = ""
            Dim wadName As String = ""
            Dim powderId As Long = 0
            Dim powderDefault As Double
            Dim powderManu As String = ""
            Dim powderName As String = ""

            Dim shotSize As String = ""
            Dim caseDetails As String = ""
            Dim primerDetails As String = ""

            Dim prefferedPowderId As Long = objIm.GetPrefSGPowderID(configId, powWei)
            Call objGf.GetFirearmDetails(lngFid, 0, "", "", "", "", strBarLen)
            Call obj.ConnectDB()

            Dim sql As String = "SELECT * from Config_List_Data_SG where CLNID=" & configId
            Dim cmd As New OdbcCommand(sql, obj.Conn)
            Dim rs As OdbcDataReader
            rs = cmd.ExecuteReader
            While rs.Read
                'LEFTOFF - adding config data to log
                'Subs are normally in the InvetoryMathSection
                caliber = objIm.GetCaliber(rs("CALID"))

                Call objIm.LoadPrimerInfo(rs("PRID"), priManu, primerName)
                primerDetails = priManu & " " & primerName

                Call objIm.LoadHullInfo(rs("CAID"), hullManufacturer, hullName, hullLength, 0, 0, hullDram)
                caseDetails = "DRAM: " & hullDram & " - " & hullManufacturer & " " & hullName

                Call objIm.LoadSG_ShotType_Details(rs("SCL"), shotDetailsManu, shotDetailsName, isSlug, _
                                    shotMaterial, shotShotNo, slugWeight, "", shotDetailsQty, shotDetailsEpps, _
                                    0, instockShotOz, shotDetailsGr)
                If Not IsDBNull(rs("SW_t")) Then prefLoad = rs("SW_t")
                shotPrefload = rs("SW")
                If Not isSlug Then
                    costShot = shotDetailsEpps
                    instockShot = shotDetailsGr
                    shotSize = "SLUG - " & shotDetailsManu & " - " & shotDetailsName
                Else
                    costSlug = shotDetailsEpps
                    instockSlug = shotDetailsQty
                    shotSize = shotSizeNo & " - " & shotDetailsManu & " - " & shotDetailsName
                End If

                'TODO: FINISHS THIS!!!
                'Loaders_Log_SG
                'THE SCL column is used for both Slug and Shot
                'load wad information
                Call objIm.LoadWADInfo(rs("WAD"), wadManu, wadName, "", 0, 0, 0)
                wad = wadManu & " " & wadName
                'load powder details into vars
                powderId = objIm.GetPrefSGPowderIDID(configId, powderDefault)
                Call objIm.GetPowderDetails(powderId, powderManu, powderName, 0, 0, 0, "", 0)
                pbm = powderDefault & " - " & "Bushing" & powderManu & " " & powderName

            End While

            rs.Close()
            rs = Nothing
            cmd = Nothing
            sql = "INSERT INTO Loaders_Log_SG(fid,FirearmName,Caliber,BarrelLen,ConfigName,Shotwt,ShotSize,case,pbm,wad,primer,pd,yds,notes) VALUES(" & _
                lngFid & ",'" & strFireArm & "','" & caliber & "','" & strBarLen & "','" & configName & "','" & shotWt & "','" & shotSize & _
                "','" & caseDetails & "','" & pbm & "','" & wad & "','" & primerDetails & "','" & pd & "'," & lngYards & ",'" & strNotes & "')"


        Catch ex As Exception
            Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub UpdateConfigList()
        Try
            Dim objGf As New GlobalFunctions
            Dim lngFid As Long = cmbFirearm.SelectedValue
            Dim strFireArm As String = ""
            Dim strCal As String = ""
            Call objGf.GetFirearmDetails(lngFid, 0, "", "", "", strCal)
            Dim calId As Long = objGf.GetCaliberID(strCal)
            ConfigList_Simple_SGTableAdapter.FillBy_Caliber(MLLDataSet.ConfigList_Simple_SG, calId)
        Catch ex As Exception
            Call LogError(Name, "UpdateConfigList", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddDataSheet_ShotGun_CFG_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Loaders_Log_FirearmsTableAdapter.FillByFullNameShotgunOnly(MLLDataSet.Loaders_Log_Firearms)
        cmbFirearm.SelectedValue = Fid
        Call UpdateConfigList()
        Call LoadAutoFill()
    End Sub

    Private Sub cmbFirearm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbFirearm.SelectedIndexChanged
        Call UpdateConfigList()
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
End Class