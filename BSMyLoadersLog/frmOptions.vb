Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.oEncrypt
Public Class frmOptions
    Dim RecID As Integer
    Function SaveData() As Integer
        Try
            Dim strLoadName As String = FluffContent(txtLoadName.Text)
            Dim strName As String = FluffContent(txtName.Text)
            Dim strAddress As String = FluffContent(txtAddress.Text)
            Dim strCity As String = FluffContent(txtCity.Text)
            Dim strState As String = FluffContent(txtState.Text)
            Dim strZip As String = FluffContent(txtZip.Text)
            Dim strPhone As String = FluffContent(txtPhone.Text)
            Dim strLic As String = oEncrypt.EncryptSHA(FluffContent(txtLic.Text))
            Dim bRiflePistol As Boolean = chkRiflePistol.Checked
            Dim bShotGun As Boolean = chkShotGun.Checked
            Dim bSec As Boolean = chkSec.Checked
            Dim strUID As String = FluffContent(txtUID.Text)
            Dim strPWD As String = oEncrypt.EncryptSHA(FluffContent(txtPWD.Text))
            Dim strCPWD As String = oEncrypt.EncryptSHA(FluffContent(txtCPWD.Text))
            Dim strDefaultList As String = cmbDefaultList.Text
            Dim strPhrase As String = oEncrypt.EncryptSHA(FluffContent(txtPhrase.Text))
            Dim strWord As String = oEncrypt.EncryptSHA(FluffContent(txtWord.Text))
            Dim iUsePassword As Integer = 0
            If Len(strUID) = 0 Then strUID = "admin"
            strUID = oEncrypt.EncryptSHA(FluffContent(strUID))
            If Not IsRequired(strName, "Name", Me.Text) Then Return 1 : Exit Function
            If bSec Then
                If Not IsRequired(txtUID.Text, "User Name", Me.Text) Then Return 1 : Exit Function
                If Not IsRequired(txtPWD.Text, "Password", Me.Text) Then Return 1 : Exit Function
                If Not IsRequired(txtPhrase.Text, "Forgot Phrase", Me.Text) Then Return 1 : Exit Function
                If Not IsRequired(txtWord.Text, "Forgot Key Word", Me.Text) Then Return 1 : Exit Function
                If InStr(strPWD, strCPWD, CompareMethod.Text) = 0 Then
                    MsgBox("Passwords do not match!", MsgBoxStyle.Critical, Me.Text)
                    Return 1
                    Exit Function
                End If
            End If
            If bSec Then iUsePassword = 1
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            Dim ObjR As New BSRegistry
            If OwnerID = 0 Then
                SQL = "INSERT INTO Personal_Information(Load_Name,Name,Address," & _
                            "City,State,ZipCode,Phone,Lic,UseLock,UserName,Password,Password_Forgot," & _
                            "Password_Forgot_word) VALUES('" & strLoadName & "','" & _
                            strName & "','" & strAddress & "','" & strCity & "','" & strState & "','" & strZip & "','" & _
                            strPhone & "','" & strLic & "'," & iUsePassword & ",'" & strUID & "','" & strPWD & "','" & _
                            strPhrase & "','" & strWord & "')"
            Else
                SQL = "UPDATE Personal_Information set Load_Name='" & strLoadName & "',Name='" & strName & "',Address='" & strAddress & "'" & _
                        ",City='" & strCity & "',State='" & strState & "',ZipCode='" & strZip & "', Phone='" & strPhone & "',Lic='" & strLic & _
                        "',UseLock=" & iUsePassword & ",UserName='" & strUID & "',Password='" & strPWD & "'," & _
                        "Password_forgot='" & strPhrase & "',Password_Forgot_word='" & strWord & "' where ID=" & OwnerID
            End If
            Obj.ConnExec(SQL)
            ObjR.SaveSettings("0000", chkBAKCleanup.Checked, nudDays.Value, False, False, chkAOBU.Checked, chkBackupOnExit.Checked, chkDoOriginalImage.Checked, bShotGun, bRiflePistol, strDefaultList, chkIPer.Checked, chkViewFPS.Checked, chkViewCUPS.Checked)
            LOADERTYPE_SHOTGUN = bShotGun
            OwnerLoadName = Replace(strLoadName, "''", "'")
            LOADERTYPE_NONSHOTGUN = bRiflePistol
            VIEW_FPS = chkViewFPS.Checked
            VIEW_CUPS = chkViewCUPS.Checked
            DEFAULTLIST = strDefaultList
            Call MDIParentMain.InitLoaderType()
            Obj = Nothing
            Return 0
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Function
    Sub GetRegData()
        Dim ObjR As New BSRegistry
        Call ObjR.GetSettings(lblLastSuc.Text, chkAOBU.Checked, nudDays.Value, chkBAKCleanup.Checked, chkBackupOnExit.Checked, chkDoOriginalImage.Checked, chkIPer.Checked)
        chkShotGun.Checked = LOADERTYPE_SHOTGUN
        chkRiflePistol.Checked = LOADERTYPE_NONSHOTGUN
        cmbDefaultList.Text = DEFAULTLIST
        chkViewFPS.Checked = VIEW_FPS
        chkViewCUPS.Checked = VIEW_CUPS
    End Sub
    Sub GetDBData()
        Try
            Dim Obj As New BSDatabase
            Dim intUsePass As Integer
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT TOP 1 * from Personal_Information"
            Dim CMD As New Odbc.OdbcCommand(SQL, Obj.Conn)
            Dim RS As Odbc.OdbcDataReader
            RS = CMD.ExecuteReader
            If RS.HasRows Then
                RS.Read()
                RecID = CInt(RS("ID"))
                OwnerID = RecID
                txtLoadName.Text = Trim(RS("load_name"))
                txtName.Text = Trim(RS("name")) 'oEncrypt.DecryptSHA(RS("name"))
                txtAddress.Text = Trim(RS("address"))
                txtCity.Text = Trim(RS("City"))
                txtState.Text = Trim(RS("State"))
                txtZip.Text = Trim(RS("ZipCode"))
                txtPhone.Text = Trim(RS("Phone")) ' oEncrypt.DecryptSHA(RS("Phone"))
                txtLic.Text = oEncrypt.DecryptSHA(RS("LIC"))
                intUsePass = CInt(RS("UseLock"))
                If intUsePass = 1 Then
                    txtPWD.Text = oEncrypt.DecryptSHA(RS("Password"))
                    txtCPWD.Text = txtPWD.Text
                    chkSec.Checked = True
                    txtUID.Text = oEncrypt.DecryptSHA(RS("UserName"))
                    txtPhrase.Text = oEncrypt.DecryptSHA(RS("Password_Forgot"))
                    txtWord.Text = oEncrypt.DecryptSHA(RS("Password_Forgot_word"))
                Else
                    chkSec.Checked = False
                End If
            Else
                chkSec.Checked = False
                RecID = 0
            End If
            Call SetSecurity()
            RS.Close()
            CMD = Nothing
            RS = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "GetDBData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadData()
        Call GetRegData()
        Call GetDBData()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Call SaveData()
        Me.Close()
    End Sub
    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Call SaveData()
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub frmOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
    Sub SetSecurity()
        txtPWD.Enabled = chkSec.Checked
        txtCPWD.Enabled = chkSec.Checked
        txtUID.Enabled = chkSec.Checked
        txtPhrase.Enabled = chkSec.Checked
        txtWord.Enabled = chkSec.Checked
    End Sub
    Private Sub chkSec_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSec.CheckedChanged
        Call SetSecurity()
    End Sub
End Class