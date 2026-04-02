'Imports BSMyLoadersLog.LoadersClass
'Imports System.Data.Odbc
'Imports BurnSoft.Applications.MGC
Imports BurnSoft.Applications.MLL.ConfigSheets
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.LoadersLog
Imports BurnSoft.Applications.MLL.Types

''' <summary>
''' Class frmLoadMakeReady_Details.
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class FrmLoadMakeReadyDetails
    ''' <summary>
    ''' The error out
    ''' </summary>
    Dim _errOut As string
    ''' <summary>
    ''' The configuration name
    ''' </summary>
    Public ConfigName As String
    ''' <summary>
    ''' The configuration identifier
    ''' </summary>
    Public ConfigID As Long
    ''' <summary>
    ''' The is personal
    ''' </summary>
    Dim IsPersonal As Boolean
    ''' <summary>
    ''' The is shot gun'
    ''' </summary>
    Dim IsShotGun As Boolean
    ''' <summary>
    ''' The cost bullet
    ''' </summary>
    Dim COST_BULLET As Double
    ''' <summary>
    ''' The cost primer
    ''' </summary>
    Dim COST_PRIMER As Double
    ''' <summary>
    ''' The cost case
    ''' </summary>
    Dim COST_CASE As Double
    ''' <summary>
    ''' The cost powder
    ''' </summary>
    Dim COST_POWDER As Double
    ''' <summary>
    ''' The cost shot
    ''' </summary>
    Dim COST_SHOT As Double
    ''' <summary>
    ''' The cost slug
    ''' </summary>
    Dim COST_SLUG As Double
    ''' <summary>
    ''' The mid powder
    ''' </summary>
    Dim MID_POWDER As Double
    ''' <summary>
    ''' The l makeable rounds
    ''' </summary>
    Dim lMakeableRounds As Long
    ''' <summary>
    ''' The instock bullet
    ''' </summary>
    Dim INSTOCK_BULLET As Long
    ''' <summary>
    ''' The instock primer
    ''' </summary>
    Dim INSTOCK_PRIMER As Long
    ''' <summary>
    ''' The instock case
    ''' </summary>
    Dim INSTOCK_CASE As Long
    ''' <summary>
    ''' The instock powder
    ''' </summary>
    Dim INSTOCK_POWDER As Double
    ''' <summary>
    ''' The instock shot
    ''' </summary>
    Dim INSTOCK_SHOT As Double
    ''' <summary>
    ''' The instock shot oz
    ''' </summary>
    Dim INSTOCK_SHOT_OZ As Double
    ''' <summary>
    ''' The shot prefload
    ''' </summary>
    Dim SHOT_PREFLOAD As Double
    ''' <summary>
    ''' The instock slug
    ''' </summary>
    Dim INSTOCK_SLUG As Double
    ''' <summary>
    ''' The preffered powder identifier
    ''' </summary>
    Dim PrefferedPowderID As Long
    ''' <summary>
    ''' The wad maxload
    ''' </summary>
    Dim WAD_MAXLOAD As Double
    ''' <summary>
    ''' The instock wad
    ''' </summary>
    Dim INSTOCK_WAD As Double
    ''' <summary>
    ''' The shot details gr
    ''' </summary>
    Dim ShotDetails_GR As Double
    ''' <summary>
    ''' The cost wad
    ''' </summary>
    Dim COST_WAD As Double
    ''' <summary>
    ''' The FPS mid
    ''' </summary>
    Dim FPS_MID As Double
    ''' <summary>
    ''' The is slug
    ''' </summary>
    Dim IsSlug As Boolean
    ''' <summary>
    ''' The bid
    ''' </summary>
    Dim BID As Long
    ''' <summary>
    ''' The prid
    ''' </summary>
    Dim PRID As Long
    ''' <summary>
    ''' The cid
    ''' </summary>
    Dim CID As Long
    ''' <summary>
    ''' The Shot/SlugID  
    ''' </summary>
    Dim SID As Long  
    ''' <summary>
    ''' The Hull ID 
    ''' </summary>
    Dim HID As Long   
    ''' <summary>
    ''' The WAD ID  
    ''' </summary>
    Dim WID As Long  
    ''' <summary>
    ''' The d c1 ra
    ''' </summary>
    Dim dC1RA As Double
#Region "General Subs and Functions"
    ''' <summary>
    ''' Loads the costs.
    ''' </summary>
    Sub LoadCosts()
        Dim lnmr As Long = 0
        Dim dPowPerB As Double = 0
        If Not IsShotGun Then
            dC1RA = Converters.CostOfRoundsOfAmmoMetalic(COST_PRIMER, COST_CASE, COST_BULLET, COST_POWDER, 
                                                         MID_POWDER)
            lnmr = INSTOCK_BULLET
            If lnmr < INSTOCK_CASE Then
                lnmr = INSTOCK_BULLET
            ElseIf lnmr > INSTOCK_CASE Then
                lnmr = INSTOCK_CASE
            End If
            dPowPerB = (INSTOCK_POWDER / MID_POWDER)
            If lnmr > INSTOCK_PRIMER Then lnmr = INSTOCK_PRIMER
            If lnmr > dPowPerB Then lnmr = CLng(dPowPerB)
        Else
            If Not IsSlug Then
                COST_BULLET = COST_SHOT * (SHOT_PREFLOAD * WeightValues.WEIGHT_GRAMS_OZ) ' * COST_SHOT
            Else
                COST_BULLET = COST_SLUG
            End If
            dC1RA = Converters.CostOfRoundsOfAmmoShotGun(COST_PRIMER, COST_CASE, COST_BULLET, 
                                                         COST_POWDER, MID_POWDER, COST_WAD)

            If IsSlug Then
                lnmr = INSTOCK_SLUG
                If lnmr < INSTOCK_CASE Then
                    lnmr = INSTOCK_SLUG
                ElseIf lnmr > INSTOCK_CASE Then
                    lnmr = INSTOCK_CASE
                End If
                If lnmr > INSTOCK_WAD Then lnmr = INSTOCK_WAD
                dPowPerB = (INSTOCK_POWDER / MID_POWDER)
                If lnmr > INSTOCK_PRIMER Then lnmr = INSTOCK_PRIMER
                If lnmr > dPowPerB Then lnmr = CLng(dPowPerB)
            Else
                Dim countMakeAble As Double = INSTOCK_SHOT_OZ / SHOT_PREFLOAD
                lnmr = countMakeAble
                If lnmr < INSTOCK_CASE Then
                    lnmr = countMakeAble
                ElseIf lnmr > INSTOCK_CASE Then
                    lnmr = INSTOCK_CASE
                End If
                If lnmr > INSTOCK_WAD Then lnmr = INSTOCK_WAD
                dPowPerB = (INSTOCK_POWDER / MID_POWDER)
                If lnmr > INSTOCK_PRIMER Then lnmr = INSTOCK_PRIMER
                If lnmr > dPowPerB Then lnmr = CLng(dPowPerB)
            End If
        End If
        lMakeableRounds = lnmr
    End Sub
    ''' <summary>
    ''' Loads the data.
    ''' </summary>
    Sub LoadData()
        Try
            IsShotGun = False
            IsPersonal = False
            'Dim Obj As New InventoryMath
            'Call Obj.LoadConfig(ConfigID, IsPersonal, IsShotGun, "")
            Dim lst as List(Of ConfigNameList) = ConfigListDataName.GetDetails(DatabasePath, ConfigID, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As ConfigNameList In lst
                IsPersonal = o.IsPersonal
                IsShotGun = o.IsShotGun
            Next
            If Not IsShotGun Then
                PrefferedPowderID = ConfigListDataPowder.GetDefaultPowderId(DatabasePath, ConfigID, MID_POWDER, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                COST_POWDER = PowderInventory.GetPricePerPowder(DatabasePath, PrefferedPowderID, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                INSTOCK_POWDER = PowderInventory.GetQtyPerPowder(DatabasePath, PrefferedPowderID, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                Call LoadConfig_RiflePistol()
            Else

                PrefferedPowderID = ConfigListDataPowderShotGun.GetDefaultPowderId(DatabasePath, 
                                                                                   CInt(ConfigID), MID_POWDER, 
                                                                                   FPS_MID, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                COST_POWDER = PowderInventory.GetPricePerPowder(DatabasePath, PrefferedPowderID, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                INSTOCK_POWDER = PowderInventory.GetQtyPerPowder(DatabasePath, PrefferedPowderID, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                LoadConfig_ShotGun()
            End If
            Call LoadCosts()
        Catch ex As Exception
            Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Loads the configuration rifle pistol.
    ''' </summary>
    Private Sub LoadConfig_RiflePistol()
        Try
            Dim lst as List(Of ConfigListDataMetalicData) = ConfigListDataMetalic.GetDetails(DatabasePath, ConfigID, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            if lst.Count > 0 Then
                For Each o As ConfigListDataMetalicData In lst
                    txtManu.Text = OwnerLoadName
                    txtName.Text = ConfigName
                    txtCal.Text = CaliberInventory.GetName(DatabasePath, o.CaliberId, _errOut)
                    BID = o.BulletId
                    PRID = o.PrimerId
                    CID = o.CaliberId
                Next
                Dim bulletList as List(Of BulletListings) = BulletsInventory.GetDetails(DatabasePath, BID, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As BulletListings In bulletList
                    txtJacket.Text = o.Name
                    txtGrains.Text = o.Weight
                    INSTOCK_BULLET = o.Qty
                    COST_BULLET = o.EsitmatedPricePerBullet
                Next
                Dim primerList As List(Of PrimerListings) = PrimerInventory.GetDetails(DatabasePath, PRID, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As PrimerListings In primerList
                    COST_PRIMER = o.PricePerPrimer
                    INSTOCK_PRIMER = o.Qty
                Next
                Dim caseList As List(Of CaseListings) = CaseInventory.GetDetails(DatabasePath, CID, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As CaseListings In caseList
                    COST_CASE = o.EstimatedPricePerCase
                    INSTOCK_CASE = o.Qty
                Next
            End If
            
        Catch ex As Exception
            Call LogError(Name, "LoadConfig_RiflePistol", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Loads the configuration shot gun.
    ''' </summary>
    Private Sub LoadConfig_ShotGun()
        Try
            'Dim ObjIM As New InventoryMath

            'Dim ShotDetails_Manu As String = ""
            'Dim ShotDetails_Name As String = ""
            'Dim ShotDetails_QTY As Double = 0
            'Dim ShotDetails_EPPS As Double = 0
            'Dim ShotDetails_ShotMat As String = ""
            'Dim ShotDetails_ShotNo As String = ""
            'Dim ShotDetails_SlugWeight As String = ""

            Dim lst As List(Of ConfigListDataShotgunData) = ConfigListDataShotgun.GetDetails(DatabasePath, ConfigID, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As ConfigListDataShotgunData In lst
                'txtCal.Text = CaliberInventory.GetName(DatabasePath, o.AmmoTypeId, _errOut)
                txtCal.Text = CaliberInventory.GetName(DatabasePath, o.CaliberId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                SID = o.ShotChargeLoad
                PRID = o.PrimerId
                HID = o.CaseId
                WID = o.Wad
                txtManu.Text = OwnerLoadName
                txtName.Text = ConfigName
                txtGrains.Text = $"{o.ShotWeightText} oz. shot"
                SHOT_PREFLOAD = o.ShotWeight
            Next

            'Call ObjIM.LoadSG_ShotType_Details(SID, ShotDetails_Manu, ShotDetails_Name, IsSlug, _
            '                                   ShotDetails_ShotMat, ShotDetails_ShotNo, 
            '                                   ShotDetails_SlugWeight, "", ShotDetails_QTY, ShotDetails_EPPS, _
            '                                   0, INSTOCK_SHOT_OZ, ShotDetails_GR)
            Dim shotList As List(Of ShotgunShotTypeData) = ShotgunShotTypeInventory.GetDetails(DatabasePath, SID, 
                                                                                               errOut := _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As ShotgunShotTypeData In shotList
                'ShotDetails_Manu = o.Manufacturer
                'ShotDetails_Name = o.Name
                IsSlug = o.IsSlug
                'ShotDetails_ShotMat = o.MaterialUsed
                'ShotDetails_ShotNo = o.ShotNumber
                'ShotDetails_SlugWeight = O.Weight
                'ShotDetails_QTY = O.Qty
                'ShotDetails_EPPS = O.EstimatedPricePerItem
                'INSTOCK_SHOT_OZ = o.Ounces
                'ShotDetails_GR = o.Grams
                txtJacket.Text = $"{ o.ShotNumber} Shot"
                If Not IsSlug Then
                    COST_SHOT = O.EstimatedPricePerItem
                    INSTOCK_SHOT = o.Grams
                Else
                    COST_SLUG = O.EstimatedPricePerItem
                    INSTOCK_SLUG = O.Qty
                End If
            Next
            'txtJacket.Text = $"{ShotDetails_ShotNo} Shot"
            'If Not IsSlug Then
            '    COST_SHOT = ShotDetails_EPPS
            '    INSTOCK_SHOT = ShotDetails_GR
            'Else
            '    COST_SLUG = ShotDetails_EPPS
            '    INSTOCK_SLUG = ShotDetails_QTY
            'End If

            'Call ObjIM.LoadWADInfo(WID, "", "", "", WAD_MAXLOAD,
            '                       INSTOCK_WAD, COST_WAD)
            Dim wadList as List(Of WadData) = WadInventory.GetDetails(DatabasePath, WID, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As WadData In wadList
                WAD_MAXLOAD = o.LoadInOz
                INSTOCK_WAD = o.Qty
                COST_WAD = o.Price
            Next

            Dim primerList As List(Of PrimerListings) = PrimerInventory.GetDetails(DatabasePath, PRID, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As PrimerListings In primerList
                COST_PRIMER = o.PricePerPrimer
                INSTOCK_PRIMER = o.Qty
            Next

            'Call ObjIM.LoadHullInfo(HID, "", "", "", INSTOCK_CASE, COST_CASE)
            Dim hullList As List(Of ShotgunHullData) = ShotgunHullInventory.GetDetails(DatabasePath, HID, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As ShotgunHullData In hullList
                INSTOCK_CASE = o.Qty
                COST_CASE = O.Price
            Next

        Catch ex As Exception
            Call LogError(Name, "LoadConfig_RiflePistol", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Saves the audit.
    ''' </summary>
    ''' <param name="qty">The qty.</param>
    Sub SaveAudit(ByVal qty As Long)
        Try
            If Not LoadersLogAmmunitionAudit.Add(DatabasePath, ConfigID, Now, qty, 
                                                 Converters.ConvertToDollars(qty * dC1RA), 
                                                 Converters.ConvertToDollars(dC1RA), 
                                                 _errOut) Then Throw New Exception(_errOut)
        Catch ex As Exception
            Call LogError(Name, "SaveAudit", Err.Number, ex.Message.ToString)
        End Try
    End Sub
#End Region
#Region "Form Subs"
    ''' <summary>
    ''' Handles the Load event of the frmLoadMakeReady_Details control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub frmLoadMakeReady_Details_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Call LoadData()
        lblInv.Text = $"NOTE: Inventory states that you have enough to make {lMakeableRounds} rounds."
        nudQty.Maximum = lMakeableRounds
    End Sub
    ''' <summary>
    ''' Handles the Click event of the Cancel control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub Cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel.Click
        Close()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnMake control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    ''' <exception cref="System.Exception"></exception>
    Private Sub btnMake_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMake.Click
        Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
        Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
        Dim strCaliber As String = GeneralHelpers.FluffContent(txtCal.Text)
        Dim strGrains As String = GeneralHelpers.FluffContent(txtGrains.Text)
        Dim strJacket As String = GeneralHelpers.FluffContent(txtJacket.Text)
        'Dim dcal As Double = ConvToNum(strGrains)
        'Converters
        'Dim dcal As Double = Converters.ConvToNum(strGrains, _errOut)
        'If _errOut.Length > 0 Then Throw New  Exception(_errOut)
        Dim iQty As Long = nudQty.Value
        Dim cQty As Long = 0
        'Dim ObjIM As New InventoryMath
        'Dim Obj As New BSDatabase
        'Dim SQL As String = ""
        Dim MID As Long = 0

        If LoadersLogAmmunition.IsAlreadyListed(DatabasePath, strManu, strName, strCaliber, 
                                                strGrains, strJacket, _errOut, cQty, MID) Then
            If Not LoadersLogAmmunition.UpdateQty(DatabasePath, MID, (cQty + iQty), 
                                                  _errOut) then throw New Exception(_errOut)
        Else 
            if not LoadersLogAmmunition.Add(DatabasePath, strManu, strName, strCaliber, 
                                            strGrains, strJacket, iQty, FPS_MID, 
                                            _errOut) then Throw new Exception(_errOut)
        End If

        'If ObjIM.IsAlreadyListed(strManu, strName, strCaliber, strGrains, strJacket, cQty, MID) Then
        '    SQL = "UPDATE Loaders_Log_Ammunition set Qty='" & (cQty + iQty) & "' where id=" & MID
        '    Obj.ConnExec(SQL)
        'Else
        '    SQL = "INSERT INTO Loaders_Log_Ammunition(Manufacturer,Name,Cal,Grain,Jacket,Qty,dcal,Vel) VALUES('" & _
        '            strManu & "','" & strName & "','" & strCaliber & "','" & strGrains & "','" & _
        '            strJacket & "'," & iQty & "," & dcal & "," & FPS_MID & ")"
        '    Obj.ConnExec(SQL)
        'End If
        If Not IsShotGun Then
            'Call ObjIM.ARUNSG_UpdateInventoryQty(iQty, INSTOCK_BULLET, BID, INSTOCK_PRIMER, PRID, INSTOCK_CASE, CID, _
            '            INSTOCK_POWDER, PrefferedPowderID, MID_POWDER)
            If Not InventoryUpdate.MetallicUpdate(DatabasePath, iQty, INSTOCK_BULLET, BID, 
                                                  INSTOCK_PRIMER, PRID, INSTOCK_CASE, 
                                                  CID, INSTOCK_POWDER,PrefferedPowderID,
                                                  MID_POWDER, _errOut) Then Throw New Exception(_errOut)
        Else
            'Call ObjIM.ARUSG_UpdateInventoryQty(iQty, INSTOCK_SLUG, SID, INSTOCK_PRIMER, PRID, INSTOCK_CASE, HID, _
            '            INSTOCK_POWDER, PrefferedPowderID, MID_POWDER, INSTOCK_WAD, WID, IsSlug, INSTOCK_SHOT_OZ, ShotDetails_GR, SHOT_PREFLOAD)
            If not InventoryUpdate.ShotgunUpdate(DatabasePath, iQty, SID, INSTOCK_SLUG, 
                                                 IsSlug, INSTOCK_SHOT_OZ,ShotDetails_GR, 
                                                 SHOT_PREFLOAD, INSTOCK_WAD, WID, 
                                                 INSTOCK_PRIMER, PRID, INSTOCK_CASE, CID, 
                                                 INSTOCK_POWDER, PrefferedPowderID, 
                                                 FPS_MID, _errOut) Then Throw new Exception(_errOut)
        End If
        Call SaveAudit(iQty)
        Close()
    End Sub
#End Region
End Class