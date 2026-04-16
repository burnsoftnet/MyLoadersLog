'Imports System.Data.Odbc
'Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.ConfigSheets
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.LoadersLog
Imports BurnSoft.Applications.MLL.Types

Namespace Adding
    ''' <summary>
    ''' Class FrmAddDataSheetShotGunCfg.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmAddDataSheetShotGunCfg
        'TODO: #20 Clean up Code
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' The firearm identifier
        ''' </summary>
        Public FirearmId As Long
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean
        ''' <summary>
        ''' Loads the automatic fill.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub LoadAutoFill()
            Try

                'Dim objAf As New AutoFillCollections.ShotGun
                ''Put in things that you want to autofill in this box
                'txtPattern.AutoCompleteCustomSource = objAf.List_SG_Log_SG_Patterns()
                txtPattern.AutoCompleteCustomSource = ConfigShotgun.LogPattern(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "LoadAutoFill", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Saves the data.
        ''' </summary>
        Sub SaveData()
            Try
                FirearmId = cmbFirearm.SelectedValue
                Dim firearmName As String = cmbFirearm.Text
                Dim dateCreated As String = dtpTested.Value
                Dim pattern As String = txtPattern.Text
                Dim yards As Long = nudYards.Value
                Dim configId As Long = cmbConfiguration.SelectedValue
                Dim configName As String = cmbConfiguration.Text
                Dim notes As String = txtNotes.Text
                Dim barrelLenght As String = ""
                'Dim powderName As String = ""
                Dim powderWeight As Double = 0
                'Dim powderManufacturer As String = ""
                Dim shotWt As String = ""
                Dim shotSizeNo As String = ""
                'Dim hullName As String = ""
                'Dim hullManufacturer As String = ""
                'Dim hullLength As String = ""
                'Dim hullDram As String = ""
                Dim hullId as Long = 0
                Dim powderDetails As String = ""
                Dim wad As String = ""
                Dim primerId as Long = 0
                'Dim primerName As String = ""
                'Dim primerManufacturer As String = ""
                'Dim pd As String = ""
                Dim caliber As String = ""
                'Dim obj As New BSDatabase
                'Dim objIm As New InventoryMath
                'Dim objGf As New GlobalFunctions

                'Dim shotDetailsManu As String = ""
                'Dim shotDetailsName As String = ""
                'Dim shotDetailsQty As Double
                'Dim shotDetailsEpps As Double
                'Dim shotDetailsGr As Double
                'Dim isSlug As Boolean = False
                'Dim shotMaterial As String = ""
                'Dim shotShotNo As String = ""
                'Dim slugWeight As String = ""
                'Dim instockShotOz As Double
                'Dim prefLoad As String
                'Dim shotPrefload As Double
                'Dim costSlug As Double
                'Dim costShot As Double
                'Dim instockShot As Double
                'Dim instockSlug As Double
                'Dim wadManu As String = ""
                'Dim wadName As String = ""
                'Dim powderId As Long = 0
                Dim powderDefault As Double
                'Dim powderManu As String = ""
                'Dim powderName As String = ""
                'Dim powderWeight As Double = 0
                Dim shotSize As String = ""
                Dim caseDetails As String = ""
                Dim primerDetails As String = ""
                Dim shotTypeId as Long = 0
                Dim wadId As Long = 0

                'Dim prefferedPowderId As Long = objIm.GetPrefSGPowderID(configId, powWei)
                Dim prefferedPowderId As Long = ConfigListDataPowder.GetDefaultPowderId(DatabasePath, configId, powderWeight, _errOut)
                Dim lst As List(Of FirearmCollection) = Firearms.GetDetails(DatabasePath, CInt(FirearmId), _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As FirearmCollection In lst
                    barrelLenght = o.Barrel
                Next


                ConfigListDataPowderShotGun.GetDefaultPowderId(DatabasePath, configId, powderDefault, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                Dim powderList as List(Of PowderListing) = PowderInventory.GetDetails(DatabasePath, prefferedPowderId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As PowderListing In powderList
                    powderDetails = $"{powderDefault} - {o.Manufacturer} {o.Name}"
                Next

                Dim configList As List(Of ConfigListDataShotgunData) = ConfigListDataShotgun.GetDetails(DatabasePath,
                                                                                                        CInt(FirearmId), _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As ConfigListDataShotgunData In configList
                    caliber = CaliberInventory.GetName(DatabasePath, o.CaliberId, _errOut)
                    If _errOut.Length > 0 Then Throw New Exception(_errOut)
                    'shotPrefload = o.ShotWeight
                    'prefLoad = o.ShotWeightText
                    primerId = o.PrimerId
                    hullId = o.CaseId
                    shotTypeId = o.ShotChargeLoad
                    wadId = o.Wad
                Next

                Dim primerList As List(Of PrimerListings) = PrimerInventory.GetDetails(DatabasePath, primerId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each p As PrimerListings In primerList
                    primerDetails = $"{p.Manufacturer} {p.Name}"
                Next

                Dim hullList As List(Of ShotgunHullData) = ShotgunHullInventory.GetDetails(DatabasePath, hullId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As ShotgunHullData In hullList
                    caseDetails = $"DRAM: {o.DRAM} - {o.Manufacturer} - {o.Name}"
                Next

                Dim shotType as List(Of ShotgunShotTypeData) = ShotgunShotTypeInventory.GetDetails(DatabasePath, shotTypeId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As ShotgunShotTypeData In shotType
                    If Not o.IsSlug Then
                        'costShot = o.EstimatedPricePerItem
                        'instockShot = o.Grams
                        shotSize = $"SLUG - {o.Manufacturer} - {o.Name}"
                    Else
                        'costSlug = o.EstimatedPricePerItem
                        'instockSlug = o.Qty
                        shotSize = shotSizeNo & $"{o.ShotNumber} - {o.Manufacturer} {o.Name}"
                    End If
                Next

                Dim wadList As List(Of WadData) = WadInventory.GetDetails(DatabasePath, wadId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As WadData In wadList
                    wad = $"{o.Manufacturer} {o.Name}"
                Next

               
                'Call objGf.GetFirearmDetails(lngFid, 0, "", "", "", "", strBarLen)
                'Call obj.ConnectDB()

                'Dim sql As String = "SELECT * from Config_List_Data_SG where CLNID=" & configId
                'Dim cmd As New OdbcCommand(sql, obj.Conn)
                'Dim rs As OdbcDataReader
                'rs = cmd.ExecuteReader
                'While rs.Read
                '    'LEFTOFF - adding config data to log
                '    'Subs are normally in the InvetoryMathSection
                '    'caliber = objIm.GetCaliber(rs("CALID"))

                '    'Call objIm.LoadPrimerInfo(rs("PRID"), primerManufacturer, primerName)
                '    'primerDetails = primerManufacturer & " " & primerName

                '    'Call objIm.LoadHullInfo(rs("CAID"), hullManufacturer, hullName, hullLength, 0, 0, hullDram)
                '    'caseDetails = "DRAM: " & hullDram & " - " & hullManufacturer & " " & hullName

                '    'Call objIm.LoadSG_ShotType_Details(rs("SCL"), shotDetailsManu, shotDetailsName, isSlug, _
                '    '                                   shotMaterial, shotShotNo, slugWeight, "", shotDetailsQty, shotDetailsEpps, _
                '    '                                   0, instockShotOz, shotDetailsGr)
                '    'If Not IsDBNull(rs("SW_t")) Then prefLoad = rs("SW_t")
                '    'shotPrefload = rs("SW")
                '    'If Not isSlug Then
                '    '    costShot = shotDetailsEpps
                '    '    instockShot = shotDetailsGr
                '    '    shotSize = "SLUG - " & shotDetailsManu & " - " & shotDetailsName
                '    'Else
                '    '    costSlug = shotDetailsEpps
                '    '    instockSlug = shotDetailsQty
                '    '    shotSize = shotSizeNo & " - " & shotDetailsManu & " - " & shotDetailsName
                '    'End If

                '    'TODO: FINISHS THIS!!!
                '    'Loaders_Log_SG
                '    'THE SCL column is used for both Slug and Shot
                '    'load wad information
                '    'Call objIm.LoadWADInfo(rs("WAD"), wadManu, wadName, "", 0, 0, 0)
                '    'wad = wadManu & " " & wadName
                '    'load powder details into vars
                '    powderId = objIm.GetPrefSGPowderIDID(configId, powderDefault)
                '    Call objIm.GetPowderDetails(powderId, powderManu, powderName, 0, 0, 0, "", 0)
                '    pbm = powderDefault & " - " & "Bushing" & powderManu & " " & powderName

                'End While

                'rs.Close()
                'rs = Nothing
                'cmd = Nothing
                'Dim sql = "INSERT INTO Loaders_Log_SG(fid,FirearmName,Caliber,BarrelLen,ConfigName,Shotwt,ShotSize,case,pbm,wad,primer,pd,yds,notes) VALUES(" & _
                '      FirearmId & ",'" & firearmName & "','" & caliber & "','" & barrelLenght & "','" & configName & "','" & shotWt & "','" & shotSize & _
                '      "','" & caseDetails & "','" & powderDetails & "','" & wad & "','" & primerDetails & "','" & pd & "'," & yards & ",'" & notes & "')"
                If Not LoadersLogShotgun.Add(DatabasePath, FirearmId, firearmName, caliber, barrelLenght,
                                             configName, dateCreated,shotWt,shotSize, caseDetails, 
                                             powderDetails,wad, primerDetails, pattern, yards, 
                                             notes, _errOut) Then Throw New Exception(_errOut)

            Catch ex As Exception
                Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Updates the configuration list.
        ''' </summary>
        Sub UpdateConfigList()
            Try
                'Dim objGf As New GlobalFunctions
                Dim lngFid As Long = cmbFirearm.SelectedValue
                'Dim strFireArm As String = ""
                'Dim strCal As String = ""
                'Call objGf.GetFirearmDetails(lngFid, 0, "", "", "", strCal)
                Dim calId As Long = 0
                Dim lst As List(Of FirearmCollection) = Firearms.GetDetails(DatabasePath, CInt(lngFid), _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As FirearmCollection In lst
                    calId = GeneralFunctions.GetCaliberID(DatabasePath, o.Caliber, _errOut)
                    If _errOut.Length > 0 Then Throw New Exception(_errOut)
                Next
                ConfigList_Simple_SGTableAdapter.FillBy_Caliber(MLLDataSet.ConfigList_Simple_SG, calId)
            Catch ex As Exception
                Call LogError(Name, "UpdateConfigList", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddDataSheet_ShotGun_CFG control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddDataSheet_ShotGun_CFG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                Loaders_Log_FirearmsTableAdapter.FillByFullNameShotgunOnly(MLLDataSet.Loaders_Log_Firearms)
                cmbFirearm.SelectedValue = FirearmId
                Call UpdateConfigList()
                Call LoadAutoFill()
            Catch ex As Exception
                Call LogError(Name, "frmAddDataSheet_ShotGun_CFG_Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the cmbFirearm control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub cmbFirearm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFirearm.SelectedIndexChanged
            Call UpdateConfigList()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnAdd control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
            Call SaveData()
        End Sub
    End Class
End Namespace