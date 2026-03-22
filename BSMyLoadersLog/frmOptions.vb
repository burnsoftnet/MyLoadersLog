'TODO #20 Clean Up Code
'Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.PeopleAndPlaces
Imports BurnSoft.Applications.MLL.Types
Imports BurnSoft.Security.RegularEncryption.SHA
''' <summary>
''' Class FrmOptions Options or settings window code
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class FrmOptions
    ''' <summary>
    ''' The record identifier
    ''' </summary>
    Dim _recId As Integer
    ''' <summary>
    ''' The error out
    ''' </summary>
    Private _errOut as String
    ''' <summary>
    ''' Saves the data.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="System.Exception"></exception>
    Function SaveData() As Integer
        Try
            Dim strLoadName As String = GeneralHelpers.FluffContent(txtLoadName.Text)
            Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
            Dim strAddress As String = GeneralHelpers.FluffContent(txtAddress.Text)
            Dim strCity As String = GeneralHelpers.FluffContent(txtCity.Text)
            Dim strState As String = GeneralHelpers.FluffContent(txtState.Text)
            Dim strZip As String = GeneralHelpers.FluffContent(txtZip.Text)
            Dim strPhone As String = GeneralHelpers.FluffContent(txtPhone.Text)
            Dim strLic As String = One.Encrypt(GeneralHelpers.FluffContent(txtLic.Text))
            Dim bRiflePistol As Boolean = chkRiflePistol.Checked
            Dim bShotGun As Boolean = chkShotGun.Checked
            Dim bSec As Boolean = chkSec.Checked
            Dim strUid As String = GeneralHelpers.FluffContent(txtUID.Text)
            Dim strPwd As String = One.Encrypt(GeneralHelpers.FluffContent(txtPWD.Text))
            Dim strCpwd As String = One.Encrypt(GeneralHelpers.FluffContent(txtCPWD.Text))
            Dim strDefaultList As String = cmbDefaultList.Text
            Dim strPhrase As String = One.Encrypt(GeneralHelpers.FluffContent(txtPhrase.Text))
            Dim strWord As String = One.Encrypt(GeneralHelpers.FluffContent(txtWord.Text))
            'Dim iUsePassword As Integer = 0
            If Len(strUid) = 0 Then strUid = "admin"
            strUid = One.Encrypt(GeneralHelpers.FluffContent(strUid))
' ReSharper disable once VbUnreachableCode
            If Not GeneralHelpers.IsRequired(strName, "Name", Text) Then Return 1 ': Exit Function
            If bSec Then
                If Not GeneralHelpers.IsRequired(txtUID.Text, "User Name", Text) Then Return 1 ': Exit Function
                If Not GeneralHelpers.IsRequired(txtPWD.Text, "Password", Text) Then Return 1 ': Exit Function
                If Not GeneralHelpers.IsRequired(txtPhrase.Text, "Forgot Phrase", Text) Then Return 1 ': Exit Function
                If Not GeneralHelpers.IsRequired(txtWord.Text, "Forgot Key Word", Text) Then Return 1 ': Exit Function
                If InStr(strPwd, strCpwd, CompareMethod.Text) = 0 Then
                    MsgBox("Passwords do not match!", MsgBoxStyle.Critical, Text)
                    Return 1
                    'Exit Function
                End If
            End If
            'If bSec Then iUsePassword = 1
            'Dim sql As String = ""
            'Dim obj As New BSDatabase
            'Dim objR As New BSRegistry
            If OwnerId = 0 Then
                If Not  OwnerInformation.Add(DatabasePath, strName, strLoadName, strAddress, 
                                             strCity, strState, strZip, strPhone, strLic, bSec, 
                                             strUid, strPwd, strPhrase, strWord, 
                                             _errOut) Then Throw New Exception(_errOut)
                'sql = "INSERT INTO Personal_Information(Load_Name,Name,Address," & _
                '            "City,State,ZipCode,Phone,Lic,UseLock,UserName,Password,Password_Forgot," & _
                '            "Password_Forgot_word) VALUES('" & strLoadName & "','" & _
                '            strName & "','" & strAddress & "','" & strCity & "','" & strState & "','" & strZip & "','" & _
                '            strPhone & "','" & strLic & "'," & iUsePassword & ",'" & strUid & "','" & strPwd & "','" & _
                '            strPhrase & "','" & strWord & "')"
            Else
                'sql = "UPDATE Personal_Information set Load_Name='" & strLoadName & "',Name='" & strName & "',Address='" & strAddress & "'" & _
                '        ",City='" & strCity & "',State='" & strState & "',ZipCode='" & strZip & "', Phone='" & strPhone & "',Lic='" & strLic & _
                '        "',UseLock=" & iUsePassword & ",UserName='" & strUid & "',Password='" & strPwd & "'," & _
                '        "Password_forgot='" & strPhrase & "',Password_Forgot_word='" & strWord & "' where ID=" & OwnerID
                If Not OwnerInformation.Update(DatabasePath, OwnerId, strName, strLoadName, strAddress, 
                                               strCity, strState, strZip, strPhone, strLic, bSec, 
                                               strUid, strPwd, strPhrase, strWord, 
                                               _errOut) Then Throw New Exception(_errOut)
            End If
            'obj.ConnExec(sql)
            'objR.SaveSettings("0000", chkBAKCleanup.Checked, nudDays.Value,
            '                  False, False, chkAOBU.Checked, chkBackupOnExit.Checked,
            '                  chkDoOriginalImage.Checked, bShotGun, bRiflePistol,
            '                  strDefaultList, chkIPer.Checked, chkViewFPS.Checked, chkViewCUPS.Checked)
            Dim mySettings As List(Of RegistrySettings) = MyRegistry.BuildRegistry(
                AlertOnBackUp := chkAOBU.Checked, BackupOnExit := chkBackupOnExit.Checked, 
                UseOrgImage := chkDoOriginalImage.Checked, LOADERTYPE_SHOTGUN := bShotGun, 
                LOADERTYPE_NONSHOTGUN := bRiflePistol, DefaultList := strDefaultList, IndvReports := chkIPer.Checked, 
                VIEW_FPS := chkViewFPS.Checked, VIEW_CUPS := chkViewCUPS.Checked, 
                TrackHistory:=chkBAKCleanup.Checked, TrackHistoryDays := nudDays.Value)
            If Not MyRegistry.SaveSettings(mySettings, _errOut) Then Throw New Exception(_errOut)

            LoadertypeShotgun = bShotGun
            OwnerLoadName = Replace(strLoadName, "''", "'")
            LoadertypeNonshotgun = bRiflePistol
            ViewFps = chkViewFPS.Checked
            ViewCups = chkViewCUPS.Checked
            Defaultlist = strDefaultList
            Call MDIParentMain.InitLoaderType()
            Return 0
        Catch ex As Exception
            Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Function
    ''' <summary>
    ''' Gets the reg data.
    ''' </summary>
    ''' <exception cref="System.Exception"></exception>
    Sub GetRegData()
        'Dim objR As New BSRegistry
        'Call objR.GetSettings(lblLastSuc.Text, chkAOBU.Checked, nudDays.Value, chkBAKCleanup.Checked, 
        '                      chkBackupOnExit.Checked, chkDoOriginalImage.Checked, chkIPer.Checked)
        
        Dim regSettings As List(Of RegistrySettings) = MyRegistry.GetSettings(_errOut)
        If _errOut.Length > 0 Then Throw New Exception(_errOut)
        For Each o As RegistrySettings In regSettings
            lblLastSuc.Text = o.LastSucBackup
            chkAOBU.Checked = o.AlertOnBackUp
            nudDays.Value = o.TrackHistoryDays
            chkBAKCleanup.Checked = o.TrackHistory
            chkBackupOnExit.Checked = o.AutoBackup
            chkDoOriginalImage.Checked = o.UseOrgImage
            chkIPer.Checked = o.IndvReports
        Next

        chkShotGun.Checked = LoadertypeShotgun
        chkRiflePistol.Checked = LoadertypeNonshotgun
        cmbDefaultList.Text = Defaultlist
        chkViewFPS.Checked = ViewFps
        chkViewCUPS.Checked = ViewCups
    End Sub
    ''' <summary>
    ''' Gets the database data.
    ''' </summary>
    ''' <exception cref="System.Exception"></exception>
    Sub GetDbData()
        Try
            'Dim obj As New BSDatabase
            'Dim intUsePass As Integer
            'Call obj.ConnectDB()
            'Dim sql As String = "SELECT TOP 1 * from Personal_Information"
            'Dim cmd As New Odbc.OdbcCommand(sql, obj.Conn)
            'Dim rs As Odbc.OdbcDataReader
            'rs = cmd.ExecuteReader
            'If rs.HasRows Then
            '    rs.Read()
            '    _recId = CInt(rs("ID"))
            '    OwnerID = _recId
            '    txtLoadName.Text = Trim(rs("load_name"))
            '    txtName.Text = Trim(rs("name")) 'oEncrypt.DecryptSHA(RS("name"))
            '    txtAddress.Text = Trim(rs("address"))
            '    txtCity.Text = Trim(rs("City"))
            '    txtState.Text = Trim(rs("State"))
            '    txtZip.Text = Trim(rs("ZipCode"))
            '    txtPhone.Text = Trim(rs("Phone")) ' oEncrypt.DecryptSHA(RS("Phone"))
            '    txtLic.Text = One.Decrypt(rs("LIC"))
            '    intUsePass = CInt(rs("UseLock"))
            '    If intUsePass = 1 Then
            '        txtPWD.Text = One.Decrypt(rs("Password"))
            '        txtCPWD.Text = txtPWD.Text
            '        chkSec.Checked = True
            '        txtUID.Text = One.Decrypt(rs("UserName"))
            '        txtPhrase.Text = One.Decrypt(rs("Password_Forgot"))
            '        txtWord.Text = One.Decrypt(rs("Password_Forgot_word"))
            '    Else
            '        chkSec.Checked = False
            '    End If
            'Else
            '    chkSec.Checked = False
            '    _recId = 0
            'End If
            'Call SetSecurity()
            'rs.Close()
            'cmd = Nothing
            'rs = Nothing
            'obj.CloseDB()
            Dim value As List(Of PersonalInformation) = OwnerInformation.GetAllData(DatabasePath, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            if value.Count > 0 Then
                For Each o As PersonalInformation In value
                    _recId = o.Id
                    OwnerId = _recId
                    txtLoadName.Text = Trim(o.LoadName)
                    txtName.Text = Trim(o.Name) 'oEncrypt.DecryptSHA(RS("name"))
                    txtAddress.Text = Trim(o.Address)
                    txtCity.Text = Trim(o.City)
                    txtState.Text = Trim(o.State)
                    txtZip.Text = Trim(o.ZipCode)
                    txtPhone.Text = Trim(o.Phone) ' oEncrypt.DecryptSHA(RS("Phone"))
                    txtLic.Text = One.Decrypt(o.License)
                    chkSec.Checked = o.UseLock
                    If o.UseLock Then
                        txtPWD.Text = One.Decrypt(o.Password)
                        txtCPWD.Text = txtPWD.Text
                        txtUID.Text = One.Decrypt(o.UserName)
                        txtPhrase.Text = One.Decrypt(o.ForgetPhrase)
                        txtWord.Text = One.Decrypt(o.Forgot)
                    End If
                Next
            Else
                chkSec.Checked = False
                _recId = 0
            End If
            Call SetSecurity()
        Catch ex As Exception
            Call LogError(Name, "GetDBData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Loads the data.
    ''' </summary>
    Sub LoadData()
        Call GetRegData()
        Call GetDbData()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnSave control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSave.Click
        Call SaveData()
        Close()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnApply control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnApply.Click
        Call SaveData()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnExit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
    ''' <summary>
    ''' Handles the Load event of the frmOptions control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub frmOptions_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Call LoadData()
        chkShotGun.Enabled = UseShotgun
    End Sub
    ''' <summary>
    ''' Sets the security.
    ''' </summary>
    Sub SetSecurity()
        txtPWD.Enabled = chkSec.Checked
        txtCPWD.Enabled = chkSec.Checked
        txtUID.Enabled = chkSec.Checked
        txtPhrase.Enabled = chkSec.Checked
        txtWord.Enabled = chkSec.Checked
    End Sub
    ''' <summary>
    ''' Handles the CheckedChanged event of the chkSec control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub chkSec_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkSec.CheckedChanged
        Call SetSecurity()
    End Sub
End Class