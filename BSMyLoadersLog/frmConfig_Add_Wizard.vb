Imports BSMyLoadersLog.LoadersClass
Public Class frmConfig_Add_Wizard
    Private Sub frmConfig_Add_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.List_CalibersTableAdapter.Fill(Me.MLLDataSet.List_Calibers)
            Dim Obj As New AutoFillCollections
            txtConfigID.AutoCompleteCustomSource = Obj.ConfigName
            If LOADERTYPE_SHOTGUN And LOADERTYPE_NONSHOTGUN Then
                chkShotgun.Checked = False
                chkRP.Checked = False
            ElseIf LOADERTYPE_NONSHOTGUN And Not LOADERTYPE_SHOTGUN Then
                chkShotgun.Checked = False
                chkRP.Checked = True
                chkShotgun.Enabled = False
            ElseIf Not LOADERTYPE_NONSHOTGUN And LOADERTYPE_SHOTGUN Then
                chkShotgun.Checked = True
                chkRP.Checked = False
                chkRP.Enabled = False
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            Dim strConfigName As String = FluffContent(txtConfigID.Text)
            Dim bRP As Boolean = chkRP.Checked
            Dim bSG As Boolean = chkShotgun.Checked
            Dim lngCal As Long = cmbCal.SelectedValue
            Dim LoadType As Integer = 0
            If bSG Then LoadType = 1
            If Not IsRequired(strConfigName, "Configuration ID", Me.Text) Then Exit Sub
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim SQL As String = ""
            Dim MyID As Long = 0
            If ObjG.ObjectExistsinDB(strConfigName, "ConfigName", "Config_List_Name") Then
                Dim sAns As String = MsgBox(strConfigName & " already exists!" & Chr(10) & "Do you wish to overwrite?", MsgBoxStyle.YesNo)
                If sAns = vbYes Then
                    MyID = ObjG.GetID("SELECT * from Config_list_Name where ConfigName='" & strConfigName & "'")
                    SQL = "DELETE from Config_List_Powder_Data_NSG where CLNID=" & MyID
                    Obj.ConnExec(SQL)
                    SQL = "DELETE from Config_List_Data_NSG where CLNID=" & MyID
                    Obj.ConnExec(SQL)
                Else
                    Exit Sub
                End If
            Else
                SQL = "INSERT INTO Config_List_Name(ConfigName,IsPersonal,IsShotGun) VALUES('" & _
                            strConfigName & "',1," & LoadType & ")"
                Obj.ConnExec(SQL)
                MyID = ObjG.GetID("SELECT * from Config_list_Name where ConfigName='" & strConfigName & "'")
            End If
            If bRP Then
                frmConfig_Add_Wizard_RP_1.ConfigName = strConfigName
                frmConfig_Add_Wizard_RP_1.CalID = lngCal
                frmConfig_Add_Wizard_RP_1.ConfigID = MyID
                frmConfig_Add_Wizard_RP_1.MdiParent = Me.MdiParent
                frmConfig_Add_Wizard_RP_1.Show()
                Me.Close()
            End If
            If bSG Then
                frmConfig_Add_Wizard_SG_1.ConfigName = strConfigName
                frmConfig_Add_Wizard_SG_1.CalID = lngCal
                frmConfig_Add_Wizard_SG_1.ConfigID = MyID
                frmConfig_Add_Wizard_SG_1.CalName = cmbCal.Text
                frmConfig_Add_Wizard_SG_1.MdiParent = Me.MdiParent
                frmConfig_Add_Wizard_SG_1.Show()
                Me.Close()
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "btnNext", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub chkRP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRP.CheckedChanged
        If chkShotgun.Checked Then chkShotgun.Checked = False
    End Sub

    Private Sub chkShotgun_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShotgun.CheckedChanged
        If chkRP.Checked Then chkRP.Checked = False
    End Sub
End Class