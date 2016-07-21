Imports BSMyLoadersLog.LoadersClass
Public Class frmConfig_Add_Wizard_RP_1
    Public CalID As Long
    Public ConfigName As String
    Public ConfigID As Long
    Dim isPersonal As Boolean
    Private Sub frmConfig_Add_Wizard_RP_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.General_Ammunition_TypeTableAdapter.Fill(Me.MLLDataSet.General_Ammunition_Type)
            isPersonal = True
            Me.List_CaseTableAdapter.FillBy_CALID(Me.MLLDataSet.List_Case, CalID)
            Me.General_PrimerTableAdapter.Fill(Me.MLLDataSet.General_Primer)
            Me.List_BulletsTableAdapter.FillBy_CALID(Me.MLLDataSet.List_Bullets, CalID)
            If isPersonal Then
                txtLoad.Enabled = False
                chkPersonal.Checked = True
            End If
            Dim ObjAF As New AutoFillCollections
            txtLoad.AutoCompleteCustomSource = ObjAF.Config_Source_NSG
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub chkPersonal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPersonal.CheckedChanged
        If chkPersonal.Checked Then
            txtLoad.Enabled = False
            chkBook.Checked = False
        End If
    End Sub

    Private Sub chkBook_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBook.CheckedChanged
        If chkBook.Checked Then
            txtLoad.Enabled = True
            chkPersonal.Checked = False
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim lngBullet As Long = cmbBullet.SelectedValue
            Dim lngPrimer As Long = cmbPrimer.SelectedValue
            Dim lngCase As Long = cmbCase.SelectedValue
            Dim lngAmmoType As Long = cmbAmmo.SelectedValue
            Dim bPersonal As Boolean = chkPersonal.Checked
            Dim bOther As Boolean = chkBook.Checked
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            Dim strSource As String = FluffContent(txtLoad.Text)
            If bOther Then
                SQL = "UPDATE Config_List_Name set IsPersonal=0 where id=" & ConfigID
                Obj.ConnExec(SQL)
            End If

            SQL = "INSERT INTO Config_List_Data_NSG(CLNID,ATID,CALID,BID,PRID,CAID,Source) VALUES (" & _
                                            ConfigID & "," & lngAmmoType & "," & CalID & "," & lngBullet & "," & _
                                            lngPrimer & "," & lngCase & ",'" & strSource & "')"
            Obj.ConnExec(SQL)
            frmConfig_Add_Wizard_Powder.ConfigID = ConfigID
            frmConfig_Add_Wizard_Powder.ConfigName = ConfigName
            frmConfig_Add_Wizard_Powder.FromConfigWiz = True
            frmConfig_Add_Wizard_Powder.MdiParent = Me.MdiParent
            frmConfig_Add_Wizard_Powder.Show()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class