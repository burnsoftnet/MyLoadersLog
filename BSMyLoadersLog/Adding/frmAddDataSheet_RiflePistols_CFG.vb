Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.LoadersLog
Imports BurnSoft.Applications.MLL.Types

Namespace Adding
    ''' <summary>
    ''' Class FrmAddDataSheetRiflePistolsCfg.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
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
                Dim lngFid As Long = cmbFirearm.SelectedValue
                Dim strFireArm As String = cmbFirearm.Text
                Dim strDateTested As String = dtpTested.Value
                Dim strGroup As String = GeneralHelpers.FluffContent(txtGroup.Text)
                Dim lngNumShots As Integer = nudShots.Value
                Dim lngYards As Integer = nudYards.Value
                Dim configId As Long = cmbConfig.SelectedValue
                Dim configName As String = cmbConfig.Text
                Dim strCond As String = GeneralHelpers.FluffContent(txtCon.Text)
                Dim strLen As String = GeneralHelpers.FluffContent(txtLen.Text)
                Dim strNotes As String = GeneralHelpers.FluffContent(txtNotes.Text)
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
                Dim powderDetails As String = GeneralHelpers.FluffContent(powName & " - " & powWei & " - " & powManu)
                Dim bulletDetails As String = GeneralHelpers.FluffContent(bulManu & " " & bulName) & " (" & bulWei & ")"
                Dim primerDetails As String = priManu & " " & priName
                Dim caseDetails As String = caseManu & " " & caseName & " " & caseStatus

                If Not LoadersLogMetallic.Add(DatabasePath, firearmId:=lngFid, dateCreated:=strDateTested,
                                              yards:=lngYards, groupSize:=strGroup, numberOfShots:=lngNumShots,
                                              powderDetails:=powderDetails, bulletDetails:=bulletDetails,
                                              primerDetails:=primerDetails, caseDetails:=caseDetails,
                                              condition:=strCond, oal:=strLen, notes:=strNotes,
                                              configName:=configName, FirearmName:=strFireArm,
                                              caliber:=caliber, BarrelLenght:=strBarLen, _errOut) Then
                    Throw New Exception(_errOut)
                End If

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