Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Security.RegularEncryption.SHA
Public Class FrmOptions
    Dim _recId As Integer
    Function SaveData() As Integer
        Try
            Dim strLoadName As String = FluffContent(txtLoadName.Text)
            Dim strName As String = FluffContent(txtName.Text)
            Dim strAddress As String = FluffContent(txtAddress.Text)
            Dim strCity As String = FluffContent(txtCity.Text)
            Dim strState As String = FluffContent(txtState.Text)
            Dim strZip As String = FluffContent(txtZip.Text)
            Dim strPhone As String = FluffContent(txtPhone.Text)
            Dim strLic As String = One.Encrypt(FluffContent(txtLic.Text))
            Dim bRiflePistol As Boolean = chkRiflePistol.Checked
            Dim bShotGun As Boolean = chkShotGun.Checked
            Dim bSec As Boolean = chkSec.Checked
            Dim strUid As String = FluffContent(txtUID.Text)
            Dim strPwd As String = One.Encrypt(FluffContent(txtPWD.Text))
            Dim strCpwd As String = One.Encrypt(FluffContent(txtCPWD.Text))
            Dim strDefaultList As String = cmbDefaultList.Text
            Dim strPhrase As String = One.Encrypt(FluffContent(txtPhrase.Text))
            Dim strWord As String = One.Encrypt(FluffContent(txtWord.Text))
            Dim iUsePassword As Integer = 0
            If Len(strUid) = 0 Then strUid = "admin"
            strUid = One.Encrypt(FluffContent(strUid))
' ReSharper disable once VbUnreachableCode
            If Not IsRequired(strName, "Name", Text) Then Return 1 : Exit Function
            If bSec Then
                If Not IsRequired(txtUID.Text, "User Name", Text) Then Return 1 : Exit Function
                If Not IsRequired(txtPWD.Text, "Password", Text) Then Return 1 : Exit Function
                If Not IsRequired(txtPhrase.Text, "Forgot Phrase", Text) Then Return 1 : Exit Function
                If Not IsRequired(txtWord.Text, "Forgot Key Word", Text) Then Return 1 : Exit Function
                If InStr(strPwd, strCpwd, CompareMethod.Text) = 0 Then
                    MsgBox("Passwords do not match!", MsgBoxStyle.Critical, Text)
                    Return 1
                    Exit Function
                End If
            End If
            If bSec Then iUsePassword = 1
            Dim sql As String = ""
            Dim obj As New BSDatabase
            Dim objR As New BSRegistry
            If OwnerID = 0 Then
                sql = "INSERT INTO Personal_Information(Load_Name,Name,Address," & _
                            "City,State,ZipCode,Phone,Lic,UseLock,UserName,Password,Password_Forgot," & _
                            "Password_Forgot_word) VALUES('" & strLoadName & "','" & _
                            strName & "','" & strAddress & "','" & strCity & "','" & strState & "','" & strZip & "','" & _
                            strPhone & "','" & strLic & "'," & iUsePassword & ",'" & strUid & "','" & strPwd & "','" & _
                            strPhrase & "','" & strWord & "')"
            Else
                sql = "UPDATE Personal_Information set Load_Name='" & strLoadName & "',Name='" & strName & "',Address='" & strAddress & "'" & _
                        ",City='" & strCity & "',State='" & strState & "',ZipCode='" & strZip & "', Phone='" & strPhone & "',Lic='" & strLic & _
                        "',UseLock=" & iUsePassword & ",UserName='" & strUid & "',Password='" & strPwd & "'," & _
                        "Password_forgot='" & strPhrase & "',Password_Forgot_word='" & strWord & "' where ID=" & OwnerID
            End If
            obj.ConnExec(sql)
            objR.SaveSettings("0000", chkBAKCleanup.Checked, nudDays.Value, False, False, chkAOBU.Checked, chkBackupOnExit.Checked, chkDoOriginalImage.Checked, bShotGun, bRiflePistol, strDefaultList, chkIPer.Checked, chkViewFPS.Checked, chkViewCUPS.Checked)
            LOADERTYPE_SHOTGUN = bShotGun
            OwnerLoadName = Replace(strLoadName, "''", "'")
            LOADERTYPE_NONSHOTGUN = bRiflePistol
            VIEW_FPS = chkViewFPS.Checked
            VIEW_CUPS = chkViewCUPS.Checked
            DEFAULTLIST = strDefaultList
            Call MDIParentMain.InitLoaderType()
            Return 0
        Catch ex As Exception
            Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Function
    Sub GetRegData()
        Dim objR As New BSRegistry
        Call objR.GetSettings(lblLastSuc.Text, chkAOBU.Checked, nudDays.Value, chkBAKCleanup.Checked, chkBackupOnExit.Checked, chkDoOriginalImage.Checked, chkIPer.Checked)
        chkShotGun.Checked = LOADERTYPE_SHOTGUN
        chkRiflePistol.Checked = LOADERTYPE_NONSHOTGUN
        cmbDefaultList.Text = DEFAULTLIST
        chkViewFPS.Checked = VIEW_FPS
        chkViewCUPS.Checked = VIEW_CUPS
    End Sub
    Sub GetDbData()
        Try
            Dim obj As New BSDatabase
            Dim intUsePass As Integer
            Call obj.ConnectDB()
            Dim sql As String = "SELECT TOP 1 * from Personal_Information"
            Dim cmd As New Odbc.OdbcCommand(sql, obj.Conn)
            Dim rs As Odbc.OdbcDataReader
            rs = cmd.ExecuteReader
            If rs.HasRows Then
                rs.Read()
                _recId = CInt(rs("ID"))
                OwnerID = _recId
                txtLoadName.Text = Trim(rs("load_name"))
                txtName.Text = Trim(rs("name")) 'oEncrypt.DecryptSHA(RS("name"))
                txtAddress.Text = Trim(rs("address"))
                txtCity.Text = Trim(rs("City"))
                txtState.Text = Trim(rs("State"))
                txtZip.Text = Trim(rs("ZipCode"))
                txtPhone.Text = Trim(rs("Phone")) ' oEncrypt.DecryptSHA(RS("Phone"))
                txtLic.Text = One.Decrypt(rs("LIC"))
                intUsePass = CInt(rs("UseLock"))
                If intUsePass = 1 Then
                    txtPWD.Text = One.Decrypt(rs("Password"))
                    txtCPWD.Text = txtPWD.Text
                    chkSec.Checked = True
                    txtUID.Text = One.Decrypt(rs("UserName"))
                    txtPhrase.Text = One.Decrypt(rs("Password_Forgot"))
                    txtWord.Text = One.Decrypt(rs("Password_Forgot_word"))
                Else
                    chkSec.Checked = False
                End If
            Else
                chkSec.Checked = False
                _recId = 0
            End If
            Call SetSecurity()
            rs.Close()
            cmd = Nothing
            rs = Nothing
            obj.CloseDB()
        Catch ex As Exception
            Call LogError(Name, "GetDBData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadData()
        Call GetRegData()
        Call GetDbData()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSave.Click
        Call SaveData()
        Close()
    End Sub
    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnApply.Click
        Call SaveData()
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
    Private Sub frmOptions_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Call LoadData()
        chkShotGun.Enabled = USE_SHOTGUN
    End Sub
    Sub SetSecurity()
        txtPWD.Enabled = chkSec.Checked
        txtCPWD.Enabled = chkSec.Checked
        txtUID.Enabled = chkSec.Checked
        txtPhrase.Enabled = chkSec.Checked
        txtWord.Enabled = chkSec.Checked
    End Sub
    Private Sub chkSec_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkSec.CheckedChanged
        Call SetSecurity()
    End Sub
End Class