Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers

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
        Private errOut as String
        ''' <summary>
        ''' Loads the automatic fill.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub LoadAutoFill()
            Try
                ' TODO: #20 CLEAN UP CODE
                'Dim objAf As New AutoFillCollections
                'txtGroup.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_GroupSize
                'txtCon.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_conditions
                'txtLen.AutoCompleteCustomSource = objAf.Loaders_Log_NSG_tl
                Dim objAf As New AutoFillCollections
                txtGroup.AutoCompleteCustomSource = ConfigMetalic.GroupSize(DatabasePath, errOut)
                if errOut.Length > 0 Then Throw New Exception(errOut)
                txtCon.AutoCompleteCustomSource = ConfigMetalic.Conditions(DatabasePath, errOut)
                if errOut.Length > 0 Then Throw New Exception(errOut)
                txtLen.AutoCompleteCustomSource = ConfigMetalic.TotalLenght(DatabasePath, errOut)
                if errOut.Length > 0 Then Throw New Exception(errOut)
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
                Dim lngNumShots As Long = nudShots.Value
                Dim lngYards As Long = nudYards.Value
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
                sql = "INSERT INTO Loaders_Log_NSG (fid,dt,yds,gs,ns,pwm,bullet," & _
                      "primer,case,conditions,tl,notes,ConfigName,FirearmName,Caliber,BarrelLen)" & _
                      " VALUES (" & lngFid & ",'" & strDateTested & "'," & lngYards & _
                      ",'" & strGroup & "'," & lngNumShots & ",'" & GeneralHelpers.FluffContent(powName & " - " & powWei & _
                                                                                                " - " & powManu) & "','" & GeneralHelpers.FluffContent(bulManu & " " & bulName) & " (" & bulWei & ")" & _
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
        ''' <summary>
        ''' Handles the Load event of the frmAddDataSheet_RiflePistols_CFG control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
            Dim objGf As New GlobalFunctions
            Dim lngFid As Long = cmbFirearm.SelectedValue
            Dim strCal As String = ""
            Call objGf.GetFirearmDetails(lngFid, 0, "", "", "", strCal)
            Dim calId As Long = objGf.GetCaliberID(strCal)
            ConfigList_SimpleTableAdapter.FillBy_Caliber(MLLDataSet.ConfigList_Simple, calId)
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
End NameSpace