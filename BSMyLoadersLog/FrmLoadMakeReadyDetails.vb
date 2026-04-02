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
#Region "Variables"
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
    Public ConfigId As Long
    '''' <summary>
    '''' The is personal
    '''' </summary>
    'Dim IsPersonal As Boolean
    ''' <summary>
    ''' The is shot gun'
    ''' </summary>
    Dim _isShotGun As Boolean
    ''' <summary>
    ''' The cost bullet
    ''' </summary>
    Dim _bulletPrice As Double
    ''' <summary>
    ''' The cost primer
    ''' </summary>
    Dim _primerPrice As Double
    ''' <summary>
    ''' The cost case
    ''' </summary>
    Dim _casePrice As Double
    ''' <summary>
    ''' The cost powder
    ''' </summary>
    Dim _powderPrice As Double
    ''' <summary>
    ''' The cost shot
    ''' </summary>
    Dim _shotPrice As Double
    ''' <summary>
    ''' The cost slug
    ''' </summary>
    Dim _slugPrice As Double
    ''' <summary>
    ''' The mid powder
    ''' </summary>
    Dim _powderMidRangeLoad As Double
    ''' <summary>
    ''' The l makeable rounds
    ''' </summary>
    Dim _roundsAbleToMake As Long
    ''' <summary>
    ''' The instock bullet
    ''' </summary>
    Dim _bulletQty As Long
    ''' <summary>
    ''' The instock primer
    ''' </summary>
    Dim _primerQty As Long
    ''' <summary>
    ''' The instock case
    ''' </summary>
    Dim _caseQty As Long
    ''' <summary>
    ''' The instock powder
    ''' </summary>
    Dim _powderQty As Double

    ''' <summary>
    ''' The instock shot oz
    ''' </summary>
    Dim _shotOzQty As Double
    ''' <summary>
    ''' The shot prefload
    ''' </summary>
    Dim _shotPrefferedLoad As Double
    ''' <summary>
    ''' The instock slug
    ''' </summary>
    Dim _slugQty As Double
    ''' <summary>
    ''' The preffered powder identifier
    ''' </summary>
    Dim _prefferedPowderId As Long

    ''' <summary>
    ''' The instock wad
    ''' </summary>
    Dim _wadQty As Double
    ''' <summary>
    ''' The shot details gr
    ''' </summary>
    Dim _shotDetailsInGrains As Double
    ''' <summary>
    ''' The cost wad
    ''' </summary>
    Dim _wadPrice As Double
    ''' <summary>
    ''' The FPS mid
    ''' </summary>
    Dim _midRangeFps As Double
    ''' <summary>
    ''' The is slug
    ''' </summary>
    Dim _isSlug As Boolean
    ''' <summary>
    ''' The bid
    ''' </summary>
    Dim _bulletId As Long
    ''' <summary>
    ''' The prid
    ''' </summary>
    Dim _primerId As Long
    ''' <summary>
    ''' The cid
    ''' </summary>
    Dim _caseId As Long
    ''' <summary>
    ''' The Shot/SlugID  
    ''' </summary>
    Dim _shotSlugId As Long  
    ''' <summary>
    ''' The Hull ID 
    ''' </summary>
    Dim _hullId As Long   
    ''' <summary>
    ''' The WAD ID  
    ''' </summary>
    Dim _wadId As Long
    ''' <summary>
    ''' The cost to make rounds
    ''' </summary>
    Dim _costToMakeRounds As Double

    ''' <summary>
    ''' The instock shot
    ''' </summary>
    Public Property ShotQty as Double

    ''' <summary>
    ''' The wad maxload
    ''' </summary>
    Public Property WadMaxLoad as Double
#End Region
#Region "General Subs and Functions"
    ''' <summary>
    ''' Loads the costs.
    ''' </summary>
    Sub LoadCosts()
        Dim lnmr As Long
        'Dim dPowPerB As Double
        If Not _isShotGun Then
            _costToMakeRounds = Converters.CostOfRoundsOfAmmoMetalic(_primerPrice, _casePrice, _bulletPrice, _powderPrice, 
                                                         _powderMidRangeLoad)
            lnmr = GeneralCalculations.CalculateMetallicRoundsToMake(_bulletQty, _caseQty, _primerQty, 
                                                                     _powderQty, _powderMidRangeLoad, _errOut)
            If _errOut.Length > 0 Then throw New Exception(_errOut)
            'lnmr = _bulletQty
            'If lnmr < _caseQty Then
            '    lnmr = _bulletQty
            'ElseIf lnmr > _caseQty Then
            '    lnmr = _caseQty
            'End If
            'dPowPerB = (_powderQty / _powderMidRangeLoad)
            'If lnmr > _primerQty Then lnmr = _primerQty
            'If lnmr > dPowPerB Then lnmr = CLng(dPowPerB)
        Else
            If Not _isSlug Then
                _bulletPrice = _shotPrice * (_shotPrefferedLoad * WeightValues.WEIGHT_GRAMS_OZ) ' * _shotPrice
            Else
                _bulletPrice = _slugPrice
            End If
            _costToMakeRounds = Converters.CostOfRoundsOfAmmoShotGun(_primerPrice, _casePrice, _bulletPrice, 
                                                         _powderPrice, _powderMidRangeLoad, _wadPrice)

            If _isSlug Then
                lnmr = GeneralCalculations.CalculateShotgunSlugRoundsToMake(_slugQty, _caseQty, _wadQty, 
                                                                            _powderQty, _powderMidRangeLoad, 
                                                                            _primerQty, _errOut)
                If _errOut.Length > 0 Then throw New Exception(_errOut)
                'lnmr = _slugQty
                'If lnmr < _caseQty Then
                '    lnmr = _slugQty
                'ElseIf lnmr > _caseQty Then
                '    lnmr = _caseQty
                'End If
                'If lnmr > _wadQty Then lnmr = _wadQty
                'dPowPerB = (_powderQty / _powderMidRangeLoad)
                'If lnmr > _primerQty Then lnmr = _primerQty
                'If lnmr > dPowPerB Then lnmr = CLng(dPowPerB)
            Else
                lnmr = GeneralCalculations.CalculateShotgunRoundsToMake(_shotOzQty, _shotPrefferedLoad, _caseQty, _wadQty, 
                                                                            _powderQty, _powderMidRangeLoad, 
                                                                            _primerQty, _errOut)
                If _errOut.Length > 0 Then throw New Exception(_errOut)
                'Dim countMakeAble As Double = _shotOzQty / _shotPrefferedLoad
                'lnmr = countMakeAble
                'If lnmr < _caseQty Then
                '    lnmr = countMakeAble
                'ElseIf lnmr > _caseQty Then
                '    lnmr = _caseQty
                'End If
                'If lnmr > _wadQty Then lnmr = _wadQty
                'dPowPerB = (_powderQty / _powderMidRangeLoad)
                'If lnmr > _primerQty Then lnmr = _primerQty
                'If lnmr > dPowPerB Then lnmr = CLng(dPowPerB)
            End If
        End If
        _roundsAbleToMake = lnmr
    End Sub
    ''' <summary>
    ''' Loads the data.
    ''' </summary>
    Sub LoadData()
        Try
            _isShotGun = False
            'IsPersonal = False
            'Dim Obj As New InventoryMath
            'Call Obj.LoadConfig(ConfigID, IsPersonal, IsShotGun, "")
            Dim lst as List(Of ConfigNameList) = ConfigListDataName.GetDetails(DatabasePath, ConfigId, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As ConfigNameList In lst
                'IsPersonal = o.IsPersonal
                _isShotGun = o.IsShotGun
            Next
            If Not _isShotGun Then
                _prefferedPowderId = ConfigListDataPowder.GetDefaultPowderId(DatabasePath, ConfigId, _powderMidRangeLoad, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                _powderPrice = PowderInventory.GetPricePerPowder(DatabasePath, _prefferedPowderId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                _powderQty = PowderInventory.GetQtyPerPowder(DatabasePath, _prefferedPowderId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                Call LoadConfig_RiflePistol()
            Else
                _prefferedPowderId = ConfigListDataPowderShotGun.GetDefaultPowderId(DatabasePath, 
                                                                                   CInt(ConfigId), _powderMidRangeLoad, 
                                                                                   _midRangeFps, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                _powderPrice = PowderInventory.GetPricePerPowder(DatabasePath, _prefferedPowderId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                _powderQty = PowderInventory.GetQtyPerPowder(DatabasePath, _prefferedPowderId, _errOut)
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
            Dim lst as List(Of ConfigListDataMetalicData) = ConfigListDataMetalic.GetDetails(DatabasePath, ConfigId, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            if lst.Count > 0 Then
                For Each o As ConfigListDataMetalicData In lst
                    txtManu.Text = OwnerLoadName
                    txtName.Text = ConfigName
                    txtCal.Text = CaliberInventory.GetName(DatabasePath, o.CaliberId, _errOut)
                    _bulletId = o.BulletId
                    _primerId = o.PrimerId
                    _caseId = o.CaseId
                Next
                Dim bulletList as List(Of BulletListings) = BulletsInventory.GetDetails(DatabasePath, _bulletId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As BulletListings In bulletList
                    txtJacket.Text = o.Name
                    txtGrains.Text = o.Weight
                    _bulletQty = o.Qty
                    _bulletPrice = o.EsitmatedPricePerBullet
                Next
                Dim primerList As List(Of PrimerListings) = PrimerInventory.GetDetails(DatabasePath, _primerId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As PrimerListings In primerList
                    _primerPrice = o.PricePerPrimer
                    _primerQty = o.Qty
                Next
                Dim caseList As List(Of CaseListings) = CaseInventory.GetDetails(DatabasePath, _caseId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As CaseListings In caseList
                    _casePrice = o.EstimatedPricePerCase
                    _caseQty = o.Qty
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

            Dim lst As List(Of ConfigListDataShotgunData) = ConfigListDataShotgun.GetDetails(DatabasePath, ConfigId, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As ConfigListDataShotgunData In lst
                'txtCal.Text = CaliberInventory.GetName(DatabasePath, o.AmmoTypeId, _errOut)
                txtCal.Text = CaliberInventory.GetName(DatabasePath, o.CaliberId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                _shotSlugId = o.ShotChargeLoad
                _primerId = o.PrimerId
                _hullId = o.CaseId
                _wadId = o.Wad
                txtManu.Text = OwnerLoadName
                txtName.Text = ConfigName
                txtGrains.Text = $"{o.ShotWeightText} oz. shot"
                _shotPrefferedLoad = o.ShotWeight
            Next

            'Call ObjIM.LoadSG_ShotType_Details(_shotSlugId, ShotDetails_Manu, ShotDetails_Name, IsSlug, _
            '                                   ShotDetails_ShotMat, ShotDetails_ShotNo, 
            '                                   ShotDetails_SlugWeight, "", ShotDetails_QTY, ShotDetails_EPPS, _
            '                                   0, _shotOzQty, _shotDetailsInGrains)
            Dim shotList As List(Of ShotgunShotTypeData) = ShotgunShotTypeInventory.GetDetails(DatabasePath, _shotSlugId, 
                                                                                               errOut := _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As ShotgunShotTypeData In shotList
                'ShotDetails_Manu = o.Manufacturer
                'ShotDetails_Name = o.Name
                _isSlug = o.IsSlug
                'ShotDetails_ShotMat = o.MaterialUsed
                'ShotDetails_ShotNo = o.ShotNumber
                'ShotDetails_SlugWeight = O.Weight
                'ShotDetails_QTY = O.Qty
                'ShotDetails_EPPS = O.EstimatedPricePerItem
                _shotOzQty = o.Ounces
                _shotDetailsInGrains = o.Grams
                txtJacket.Text = $"{ o.ShotNumber} Shot"
                If Not _isSlug Then
                    _shotPrice = O.EstimatedPricePerItem
                    ShotQty = o.Grams
                Else
                    _slugPrice = O.EstimatedPricePerItem
                    _slugQty = O.Qty
                End If
            Next
            'txtJacket.Text = $"{ShotDetails_ShotNo} Shot"
            'If Not IsSlug Then
            '    _shotPrice = ShotDetails_EPPS
            '    _shotQty = _shotDetailsInGrains
            'Else
            '    _slugPrice = ShotDetails_EPPS
            '    _slugQty = ShotDetails_QTY
            'End If

            'Call ObjIM.LoadWADInfo(_wadId, "", "", "", _wadMaxLoad,
            '                       _wadQty, _wadPrice)
            Dim wadList as List(Of WadData) = WadInventory.GetDetails(DatabasePath, _wadId, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As WadData In wadList
                WadMaxLoad = o.LoadInOz
                _wadQty = o.Qty
                _wadPrice = o.Price
            Next

            Dim primerList As List(Of PrimerListings) = PrimerInventory.GetDetails(DatabasePath, _primerId, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As PrimerListings In primerList
                _primerPrice = o.PricePerPrimer
                _primerQty = o.Qty
            Next

            'Call ObjIM.LoadHullInfo(_hullId, "", "", "", _caseQty, _casePrice)
            Dim hullList As List(Of ShotgunHullData) = ShotgunHullInventory.GetDetails(DatabasePath, _hullId, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As ShotgunHullData In hullList
                _caseQty = o.Qty
                _casePrice = O.Price
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
            If Not LoadersLogAmmunitionAudit.Add(DatabasePath, ConfigId, Now, qty, 
                                                 Converters.ConvertToDollars(qty * _costToMakeRounds), 
                                                 Converters.ConvertToDollars(_costToMakeRounds), 
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
        lblInv.Text = $"NOTE: Inventory states that you have enough to make {_roundsAbleToMake} rounds."
        nudQty.Maximum = _roundsAbleToMake
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
        Dim ammoId As Long = 0

        If LoadersLogAmmunition.IsAlreadyListed(DatabasePath, strManu, strName, strCaliber, 
                                                strGrains, strJacket, _errOut, cQty, ammoId) Then
            If Not LoadersLogAmmunition.UpdateQty(DatabasePath, ammoId, (cQty + iQty), 
                                                  _errOut) then throw New Exception(_errOut)
        Else 
            if not LoadersLogAmmunition.Add(DatabasePath, strManu, strName, strCaliber, 
                                            strGrains, strJacket, iQty, _midRangeFps, 
                                            _errOut) then Throw new Exception(_errOut)
        End If

        'If ObjIM.IsAlreadyListed(strManu, strName, strCaliber, strGrains, strJacket, cQty, MID) Then
        '    SQL = "UPDATE Loaders_Log_Ammunition set Qty='" & (cQty + iQty) & "' where id=" & MID
        '    Obj.ConnExec(SQL)
        'Else
        '    SQL = "INSERT INTO Loaders_Log_Ammunition(Manufacturer,Name,Cal,Grain,Jacket,Qty,dcal,Vel) VALUES('" & _
        '            strManu & "','" & strName & "','" & strCaliber & "','" & strGrains & "','" & _
        '            strJacket & "'," & iQty & "," & dcal & "," & _midRangeFps & ")"
        '    Obj.ConnExec(SQL)
        'End If
        If Not _isShotGun Then
            'Call ObjIM.ARUNSG_UpdateInventoryQty(iQty, _bulletQty, _bulletId, _primerQty, _primerId, _caseQty, _CaseId, _
            '            _powderQty, PrefferedPowderID, _powderMidRangeLoad)
            If Not InventoryUpdate.MetallicUpdate(DatabasePath, iQty, _bulletQty, _bulletId, 
                                                  _primerQty, _primerId, _caseQty, 
                                                  _caseId, _powderQty,_prefferedPowderId,
                                                  _powderMidRangeLoad, _errOut) Then Throw New Exception(_errOut)
        Else
            'Call ObjIM.ARUSG_UpdateInventoryQty(iQty, _slugQty, _shotSlugId, _primerQty, _primerId, _caseQty, _hullId, _
            '            _powderQty, PrefferedPowderID, _powderMidRangeLoad, _wadQty, _wadId, IsSlug, _shotOzQty, _shotDetailsInGrains, _shotPrefferedLoad)
            If not InventoryUpdate.ShotgunUpdate(DatabasePath, iQty, _shotSlugId, _slugQty, 
                                                 _isSlug, _shotOzQty,_shotDetailsInGrains, 
                                                 _shotPrefferedLoad, _wadQty, _wadId, 
                                                 _primerQty, _primerId, _caseQty, _caseId, 
                                                 _powderQty, _prefferedPowderId, 
                                                 _midRangeFps, _errOut) Then Throw new Exception(_errOut)
        End If
        Call SaveAudit(iQty)
        Close()
    End Sub
#End Region
End Class