Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmAddDataSheet_ShotGun_CFG
    Public FID As Long
    Public FromView As Boolean
    Sub LoadAutoFill()
        Try
            Dim ObjAF As New AutoFillCollections
            'Put in things that you want to autofill in this box
            ObjAF = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "LoadAutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim lngFID As Long = cmbFirearm.SelectedValue
            Dim strFireArm As String = cmbFirearm.Text
            Dim strdateTested As String = dtpTested.Value
            Dim strPattern As String = txtPattern.Text
            Dim lngYards As Long = nudYards.Value
            Dim ConfigID As Long = cmbConfiguration.SelectedValue
            Dim ConfigName As String = cmbConfiguration.Text
            Dim strNotes As String = txtNotes.Text
            Dim strBarLen As String = ""
            Dim PowName As String = ""
            Dim PowWei As Double = 0
            Dim PowManu As String = ""
            Dim ShotWt As String = ""
            Dim ShotSizeNo As String = ""
            Dim HullName As String = ""
            Dim HullManufacturer As String = ""
            Dim HullLength As String = ""
            Dim HullDram As String = ""
            Dim pbm As String = ""
            Dim wad As String = ""
            Dim PrimerName As String = ""
            Dim PriManu As String = ""
            Dim pd As String = ""
            Dim Caliber As String = ""
            Dim Obj As New BSDatabase
            Dim ObjIM As New InventoryMath
            Dim ObjGF As New GlobalFunctions

            Dim PrefferedPowderID As Long = ObjIM.GetPrefSGPowderID(ConfigID, PowWei)
            Call ObjGF.GetFirearmDetails(lngFID, 0, "", "", "", "", strBarLen)
            Call Obj.ConnectDB()

            Dim SQL As String = "SELECT * from Config_List_Data_SG where CLNID=" & ConfigID
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                'LEFTOFF - adding config data to log
                'Subs are normally in the InvetoryMathSection
                Caliber = ObjIM.GetCaliber(RS("CALID"))
                Call ObjIM.LoadPrimerInfo(RS("PRID"), PriManu, PrimerName)
                Call ObjIM.LoadHullInfo(RS("CAID"), HullManufacturer, HullName, HullLength, 0, 0, HullDram)
                'THE SCL column is used for both Slug and Shot
                'load shot or slug info into vars
                'load powder details into vars
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing


        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub UpdateConfigList()
        Try
            Dim ObjGF As New GlobalFunctions
            Dim lngFID As Long = cmbFirearm.SelectedValue
            Dim strFireArm As String = ""
            Dim strCal As String = ""
            Call ObjGF.GetFirearmDetails(lngFID, 0, "", "", "", strCal)
            Dim CalID As Long = ObjGF.GetCaliberID(strCal)
            Me.ConfigList_Simple_SGTableAdapter.FillBy_Caliber(Me.MLLDataSet.ConfigList_Simple_SG, CalID)
        Catch ex As Exception
            Call LogError(Me.Name, "UpdateConfigList", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddDataSheet_ShotGun_CFG_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Loaders_Log_FirearmsTableAdapter.FillByFullNameShotgunOnly(Me.MLLDataSet.Loaders_Log_Firearms)
        cmbFirearm.SelectedValue = FID
        Call UpdateConfigList()
        Call LoadAutoFill()
    End Sub

    Private Sub cmbFirearm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbFirearm.SelectedIndexChanged
        Call UpdateConfigList()
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
End Class