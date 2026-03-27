Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.LoadersLog
Imports BurnSoft.Applications.MLL.Types

Namespace Adding
    ''' <summary>
    ''' Add Data sheet for Rifle or Pistol, aka metallic reloading data sheet
    ''' </summary>
    Public Class FrmAddDataSheetRiflePistolsMan
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
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
                txtGroup.AutoCompleteCustomSource = ConfigMetalic.GroupSize(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtCon.AutoCompleteCustomSource = ConfigMetalic.Conditions(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtLen.AutoCompleteCustomSource = ConfigMetalic.TotalLenght(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtPowName.AutoCompleteCustomSource = Powder.Name(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtPowManu.AutoCompleteCustomSource = Powder.Manufacturer(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtBullet.AutoCompleteCustomSource = ConfigMetalic.Bullet(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtPrimer.AutoCompleteCustomSource = ConfigMetalic.Primer(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtCase.AutoCompleteCustomSource = ConfigMetalic.Case(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
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
                Dim firearmId As Long = cmbFirearm.SelectedValue
                Dim firearmName As String = cmbFirearm.Text
                Dim dateCreated As String = dtpTested.Value
                Dim groupSize As String = GeneralHelpers.FluffContent(txtGroup.Text)
                Dim numShots As Long = nudShots.Value
                Dim yards As Long = nudYards.Value
                Dim powderName As String = GeneralHelpers.FluffContent(txtPowName.Text)
                Dim powderWeight As String = GeneralHelpers.FluffContent(txtPowWei.Text)
                Dim powderManufacturer As String = GeneralHelpers.FluffContent(txtPowManu.Text)
                Dim bulletDetails As String = GeneralHelpers.FluffContent(txtBullet.Text)
                Dim primerDetails As String = GeneralHelpers.FluffContent(txtPrimer.Text)
                Dim caseDetails As String = GeneralHelpers.FluffContent(txtCase.Text)
                Dim condition As String = GeneralHelpers.FluffContent(txtCon.Text)
                Dim oal As String = GeneralHelpers.FluffContent(txtLen.Text)
                Dim notes As String = GeneralHelpers.FluffContent(txtNotes.Text)
                Dim configName As String = "N/A"
                Dim barrelLenght As String = ""
                Dim caliber As String = ""

                Dim lst As List(Of FirearmCollection) = Firearms.GetDetails(DatabasePath, CInt(firearmId), _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As FirearmCollection In lst
                    caliber = o.Caliber
                    barrelLenght = o.Barrel
                Next

                If Not LoadersLogMetallic.Add(DatabasePath, firearmId, dateCreated, CInt(yards), groupSize, 
                                              CInt(numShots), $"{powderName} - {powderWeight} - {powderManufacturer}", 
                                              bulletDetails, primerDetails, caseDetails, condition, oal, notes,
                                              configName, firearmName, caliber, barrelLenght, 
                                              _errOut) Then Throw New Exception(_errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                MsgBox("Information was saved to the Loaders Log!")
                If FromView Then Call FrmViewDataSheetRiflePistols.LoadDataCur()
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
End NameSpace