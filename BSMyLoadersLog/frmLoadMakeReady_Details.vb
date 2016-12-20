Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmLoadMakeReady_Details
    Public ConfigName As String
    Public ConfigID As Long
    Dim IsPersonal As Boolean
    Dim IsShotGun As Boolean
    Dim COST_BULLET As Double
    Dim COST_PRIMER As Double
    Dim COST_CASE As Double
    Dim COST_POWDER As Double
    Dim COST_SHOT As Double
    Dim COST_SLUG As Double
    Dim MID_POWDER As Double
    Dim lMakeableRounds As Long
    Dim INSTOCK_BULLET As Long
    Dim INSTOCK_PRIMER As Long
    Dim INSTOCK_CASE As Long
    Dim INSTOCK_POWDER As Double
    Dim INSTOCK_SHOT As Double
    Dim INSTOCK_SHOT_OZ As Double
    Dim SHOT_PREFLOAD As Double
    Dim INSTOCK_SLUG As Double
    Dim PrefferedPowderID As Long
    Dim WAD_MAXLOAD As Double
    Dim INSTOCK_WAD As Double
    Dim ShotDetails_GR As Double
    Dim COST_WAD As Double
    Dim FPS_MID As Double
    Dim IsSlug As Boolean
    Dim BID As Long
    Dim PRID As Long
    Dim CID As Long
    Dim SID As Long  'Shot/SlugID
    Dim HID As Long  'Hull ID
    Dim WID As Long  'WAD ID
    Dim dC1RA As Double
#Region "General Subs and Functions"
    Sub LoadCosts()
        Dim lnmr As Long = 0
        Dim dPowPerB As Double = 0
        If Not IsShotGun Then
            dC1RA = CostOf1RndOfAmmo(COST_PRIMER, COST_CASE, COST_BULLET, COST_POWDER, MID_POWDER)
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
                COST_BULLET = COST_SHOT * (SHOT_PREFLOAD * WEIGHT_GRAMS_OZ) ' * COST_SHOT
            Else
                COST_BULLET = COST_SLUG
            End If
            dC1RA = CostOf1RndOfAmmoSG(COST_PRIMER, COST_CASE, COST_BULLET, COST_POWDER, MID_POWDER, COST_WAD)

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
                Dim CountMakeAble As Double = INSTOCK_SHOT_OZ / SHOT_PREFLOAD
                lnmr = CountMakeAble
                If lnmr < INSTOCK_CASE Then
                    lnmr = CountMakeAble
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
    Sub LoadData()
        Try
            IsShotGun = False
            IsPersonal = False
            Dim Obj As New InventoryMath
            Call Obj.LoadConfig(ConfigID, IsPersonal, IsShotGun, "")
            If Not IsShotGun Then
                PrefferedPowderID = Obj.GetPrefNSGPowderID(ConfigID, MID_POWDER, FPS_MID)
                COST_POWDER = Obj.GetPricePerPowder(PrefferedPowderID)
                INSTOCK_POWDER = Obj.GetQTYPerPowder(PrefferedPowderID)
                Call LoadConfig_RiflePistol()
            Else
                PrefferedPowderID = Obj.GetPrefSGPowderID(ConfigID, MID_POWDER, FPS_MID)
                COST_POWDER = Obj.GetPricePerPowder(PrefferedPowderID)
                INSTOCK_POWDER = Obj.GetQTYPerPowder(PrefferedPowderID)
                LoadConfig_ShotGun()
            End If
            Call LoadCosts()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub LoadConfig_RiflePistol()
        Try
            Dim Obj As New BSDatabase
            Dim ObjIM As New InventoryMath
            Dim SQL As String = "SELECT * from Config_List_Data_NSG where CLNID=" & ConfigID
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                txtManu.Text = OwnerLoadName
                txtName.Text = ConfigName
                txtCal.Text = ObjIM.GetCaliber(RS("CALID"))
                BID = RS("BID")
                PRID = RS("PRID")
                CID = RS("CAID")
                Call ObjIM.LoadBulletInfo(BID, "", txtJacket.Text, "", txtGrains.Text, _
                        "", "", "", INSTOCK_BULLET, "", COST_BULLET)
                Call ObjIM.LoadPrimerInfo(PRID, "", "", "", COST_PRIMER, INSTOCK_PRIMER)
                Call ObjIM.LoadCaseInfo(CID, "", "", "", "", INSTOCK_CASE, COST_CASE)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "LoadConfig_RiflePistol", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub LoadConfig_ShotGun()
        Try
            Dim Obj As New BSDatabase
            Dim ObjIM As New InventoryMath
            Dim SQL As String = "SELECT * from Config_List_Data_SG where CLNID=" & ConfigID
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            Dim ShotDetails_Manu As String = ""
            Dim ShotDetails_Name As String = ""
            Dim ShotDetails_QTY As Double = 0
            Dim ShotDetails_EPPS As Double = 0
            Dim ShotDetails_ShotMat As String = ""
            Dim ShotDetails_ShotNo As String = ""
            Dim ShotDetails_SlugWeight As String = ""
            RS = CMD.ExecuteReader
            While RS.Read
                txtCal.Text = ObjIM.GetCaliber(RS("ATID"))
                SID = RS("SCL")
                PRID = RS("PRID")
                HID = RS("CAID")
                WID = RS("WAD")
                Call ObjIM.LoadSG_ShotType_Details(SID, ShotDetails_Manu, ShotDetails_Name, IsSlug, _
                                    ShotDetails_ShotMat, ShotDetails_ShotNo, ShotDetails_SlugWeight, "", ShotDetails_QTY, ShotDetails_EPPS, _
                                    0, INSTOCK_SHOT_OZ, ShotDetails_GR)
                txtManu.Text = OwnerLoadName
                txtName.Text = ConfigName
                txtJacket.Text = ShotDetails_ShotNo & " Shot"
                If Not IsDBNull(RS("SW_t")) Then txtGrains.Text = RS("SW_t") & " oz. shot"
                SHOT_PREFLOAD = RS("SW")
                If Not IsSlug Then
                    COST_SHOT = ShotDetails_EPPS
                    INSTOCK_SHOT = ShotDetails_GR
                Else
                    COST_SLUG = ShotDetails_EPPS
                    INSTOCK_SLUG = ShotDetails_QTY
                End If
                Call ObjIM.LoadWADInfo(WID, "", "", "", WAD_MAXLOAD, INSTOCK_WAD, COST_WAD)
                Call ObjIM.LoadPrimerInfo(PRID, "", "", "", COST_PRIMER, INSTOCK_PRIMER)
                Call ObjIM.LoadHullInfo(HID, "", "", "", INSTOCK_CASE, COST_CASE)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "LoadConfig_RiflePistol", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveAudit(ByVal qty As Long)
        Dim Obj As New BSDatabase
        Dim ObjIM As New InventoryMath
        Dim SQL As String = "INSERT INTO Loaders_Log_Ammunition_Audit (CFID,dtc,qty,ec,ecpr) VALUES(" & _
            ConfigID & ",'" & Now & "'," & qty & "," & ObjIM.ConvertToDollars((qty * dC1RA)) & "," & ObjIM.ConvertToDollars(dC1RA) & ")"
        Obj.ConnExec(SQL)
    End Sub
#End Region
#Region "Form Subs"
    Private Sub frmLoadMakeReady_Details_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
        lblInv.Text = "NOTE: Inventory states that you have enough to make " & lMakeableRounds & " rounds."
        nudQty.Maximum = lMakeableRounds
    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub
    Private Sub btnMake_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMake.Click
        Dim strManu As String = FluffContent(txtManu.Text)
        Dim strName As String = FluffContent(txtName.Text)
        Dim strCaliber As String = FluffContent(txtCal.Text)
        Dim strGrains As String = FluffContent(txtGrains.Text)
        Dim strJacket As String = FluffContent(txtJacket.Text)
        Dim dcal As Double = ConvToNum(strGrains)
        Dim iQty As Long = nudQty.Value
        Dim cQty As Long = 0
        Dim ObjIM As New InventoryMath
        Dim Obj As New BSDatabase
        Dim SQL As String = ""
        Dim MID As Long = 0

        If ObjIM.IsAlreadyListed(strManu, strName, strCaliber, strGrains, strJacket, cQty, MID) Then
            SQL = "UPDATE Loaders_Log_Ammunition set Qty='" & (cQty + iQty) & "' where id=" & MID
            Obj.ConnExec(SQL)
        Else
            SQL = "INSERT INTO Loaders_Log_Ammunition(Manufacturer,Name,Cal,Grain,Jacket,Qty,dcal,Vel) VALUES('" & _
                    strManu & "','" & strName & "','" & strCaliber & "','" & strGrains & "','" & _
                    strJacket & "'," & iQty & "," & dcal & "," & FPS_MID & ")"
            Obj.ConnExec(SQL)
        End If
        If Not IsShotGun Then
            Call ObjIM.ARUNSG_UpdateInventoryQty(iQty, INSTOCK_BULLET, BID, INSTOCK_PRIMER, PRID, INSTOCK_CASE, CID, _
                        INSTOCK_POWDER, PrefferedPowderID, MID_POWDER)
        Else
            Call ObjIM.ARUSG_UpdateInventoryQty(iQty, INSTOCK_SLUG, SID, INSTOCK_PRIMER, PRID, INSTOCK_CASE, HID, _
                        INSTOCK_POWDER, PrefferedPowderID, MID_POWDER, INSTOCK_WAD, WID, IsSlug, INSTOCK_SHOT_OZ, ShotDetails_GR, SHOT_PREFLOAD)
        End If
        Call SaveAudit(iQty)
        Me.Close()
    End Sub
#End Region
End Class