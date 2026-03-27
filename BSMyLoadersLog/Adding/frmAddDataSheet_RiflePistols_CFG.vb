Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.ConfigSheets
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.LoadersLog
Imports BurnSoft.Applications.MLL.Types

Namespace Adding
    ''' <summary>
    ''' Class FrmAddDataSheetRiflePistolsCfg.
    ''' Implements the <see cref="Form" />
    ''' </summary>
    ''' <seealso cref="Form" />
    Public Class FrmAddDataSheetRiflePistolsCfg
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean
        ''' <summary>
        ''' The fid
        ''' </summary>
        Public Fid As Long
        ''' <summary>
        ''' The error out
        ''' </summary>
        Private _errOut As String
        ''' <summary>
        ''' Loads the automatic fill.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub LoadAutoFill()
            Try
                txtGroup.AutoCompleteCustomSource = ConfigMetalic.GroupSize(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtCon.AutoCompleteCustomSource = ConfigMetalic.Conditions(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtLen.AutoCompleteCustomSource = ConfigMetalic.TotalLenght(DatabasePath, _errOut)
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
                Dim firearmId As Long = cmbFirearm.SelectedValue
                Dim firearmName As String = cmbFirearm.Text
                Dim dateCreated As String = dtpTested.Value
                Dim groupSize As String = GeneralHelpers.FluffContent(txtGroup.Text)
                Dim numberOfShots As Integer = nudShots.Value
                Dim yards As Integer = nudYards.Value
                Dim configId As Long = cmbConfig.SelectedValue
                Dim configName As String = cmbConfig.Text
                Dim condition As String = GeneralHelpers.FluffContent(txtCon.Text)
                Dim oal As String = GeneralHelpers.FluffContent(txtLen.Text)
                Dim notes As String = GeneralHelpers.FluffContent(txtNotes.Text)
                Dim barrelLenght As String = ""
                Dim powderName As String = ""
                Dim powderWeight As Double = 0
                Dim powderManufacturer As String = ""
                Dim bulletManufacturer As String = ""
                Dim bulletName As String = ""
                Dim bulletWeight As String = ""
                Dim primerManufacturer As String = ""
                Dim primerName As String = ""
                Dim caseName As String = ""
                Dim caseManu As String = ""
                Dim caseStatus As String = ""
                Dim caliber As String = ""
                Dim prefferedPowderId As Long = ConfigListDataPowder.GetDefaultPowderId(DatabasePath, configId, powderWeight, _errOut)

                Dim lst As List(Of FirearmCollection) = Firearms.GetDetails(DatabasePath, CInt(firearmId), _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As FirearmCollection In lst
                    barrelLenght = o.Barrel
                Next

                Dim configList As List(Of ConfigListDataMetalicData) = ConfigListDataMetalic.GetDetails(
                    DatabasePath, configId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                Dim bulletId As Long
                Dim primerId As Long
                Dim caseId As Long
                For Each o As ConfigListDataMetalicData In configList
                    caliber = CaliberInventory.GetName(DatabasePath, o.CaliberId, _errOut)
                    If _errOut.Length > 0 Then Throw New Exception(_errOut)
                    bulletId = o.BulletId
                    primerId = o.PrimerId
                    caseId = o.CaseId
                Next

                Dim bulletList As List(Of BulletListings) = BulletsInventory.GetDetails(DatabasePath, bulletId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As BulletListings In bulletList
                    bulletManufacturer = o.Manufacturer
                    bulletName = o.Name
                    bulletWeight = o.Weight
                Next

                Dim primerList As List(Of PrimerListings) = PrimerInventory.GetDetails(DatabasePath, primerId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As PrimerListings In primerList
                    primerManufacturer = o.Manufacturer
                    primerName = o.Name
                Next

                Dim caseList As List(Of CaseListings) = CaseInventory.GetDetails(DatabasePath, caseId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As CaseListings In caseList
                    caseManu = o.Manufacturer
                    caseName = o.Name
                    caseStatus = o.TimesUsed
                Next

                Dim powderList as List(Of PowderListing) = PowderInventory.GetDetails(DatabasePath, prefferedPowderId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As PowderListing In powderList
                    powderManufacturer = o.Manufacturer
                    powderName = o.Name
                Next

                If CLng(caseStatus) = 0 Then
                    caseStatus = "(NEW)"
                Else
                    caseStatus = "(USED)"
                End If
                Dim powderDetails As String = GeneralHelpers.FluffContent(powderName & " - " & powderWeight & " - " & powderManufacturer)
                Dim bulletDetails As String = GeneralHelpers.FluffContent(bulletManufacturer & " " & bulletName) & " (" & bulletWeight & ")"
                Dim primerDetails As String = primerManufacturer & " " & primerName
                Dim caseDetails As String = caseManu & " " & caseName & " " & caseStatus

                If Not LoadersLogMetallic.Add(DatabasePath, firearmId, dateCreated, yards, groupSize, numberOfShots,
                                              powderDetails, bulletDetails, primerDetails, caseDetails,
                                              condition, oal, notes, configName, firearmName,
                                              caliber,barrelLenght, _errOut) Then Throw New Exception(_errOut)

                MsgBox("Information was saved to the Loaders Log!")
                If FromView Then Call FrmViewDataSheetRiflePistols.LoadDataCur()
                Close()
            Catch ex As Exception
                Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddDataSheet_RiflePistols_CFG control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddDataSheet_RiflePistols_CFG_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                Loaders_Log_FirearmsTableAdapter.FillByFullNameNoneShotgun(MLLDataSet.Loaders_Log_Firearms)
                cmbFirearm.SelectedValue = Fid
                Call UpdateConfigList()
                Call LoadAutoFill()
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnAdd control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Call SaveData()
        End Sub
        ''' <summary>
        ''' Updates the configuration list.
        ''' </summary>
        Sub UpdateConfigList()
            Try
                Dim lngFid As Integer = cmbFirearm.SelectedValue
                Dim strCal As String = ""
                Dim values As List(Of FirearmCollection) = Firearms.GetDetails(DatabasePath, lngFid, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As FirearmCollection In values
                    strCal = o.Caliber
                Next
                Dim calId As Long = GeneralFunctions.GetCaliberID(DatabasePath, strCal, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                ConfigList_SimpleTableAdapter.FillBy_Caliber(MLLDataSet.ConfigList_Simple, calId)
            Catch ex As Exception
                Call LogError(Name, "UpdateConfigList", Err.Number,
                              ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the cmbFirearm control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub cmbFirearm_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFirearm.SelectedIndexChanged
            Call UpdateConfigList()
        End Sub
        ''' <summary>
        ''' Handles the TextChanged event of the txtGroup control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub txtGroup_TextChanged(sender As Object, e As EventArgs) Handles txtGroup.TextChanged

        End Sub
    End Class
End Namespace