Imports System.Data
Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers

Namespace Adding
    ''' <summary>
    ''' Class frmConfig_Add_Wizard_SG_2.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmConfigAddWizardSg2
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim errOut as String
        ''' <summary>
        ''' The configuration name
        ''' </summary>
        Public ConfigName As String
        ''' <summary>
        ''' The cal identifier
        ''' </summary>
        Public CalID As Long
        ''' <summary>
        ''' The cal name
        ''' </summary>
        Public CalName As String
        ''' <summary>
        ''' The gid
        ''' </summary>
        Public GID As Long
        ''' <summary>
        ''' The configuration identifier
        ''' </summary>
        Public ConfigID As Long
        ''' <summary>
        ''' Handles the Load event of the frmConfig_Add_Wizard_SG_2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmConfig_Add_Wizard_SG_2_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                List_SG_ShotType_DetailsTableAdapter.FillBy_CFG_List(MLLDataSet.List_SG_ShotType_Details)
                List_SG_CaseTableAdapter.FillBy_CFG_List(MLLDataSet.List_SG_Case, GID)
                List_SG_WADTableAdapter.FillBy_CFG_WADList(MLLDataSet.List_SG_WAD, GID)
                ViewPrimerListTableAdapter.FillBy_CFG_List(MLLDataSet.viewPrimerList)
                Dim WID As Long = cmdWAD.SelectedValue
                txtShotCharge.Text = GetMaxWADCharge(WID)
                List_SG_ShotCharge_LoadsTableAdapter.Fill(MLLDataSet.List_SG_ShotCharge_Loads)
                'Dim ObjAF As New AutoFillCollections.ShotGun
                'txtSource.AutoCompleteCustomSource = ObjAF.Config_Source_SG
                'txtShotCharge.AutoCompleteCustomSource = ObjAF.Config_LoadInOZ_SG
                'Dim ObjAF As New AutoFillCollections.ShotGun
                txtSource.AutoCompleteCustomSource = ConfigShotgun.Source(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
                txtShotCharge.AutoCompleteCustomSource = ConfigShotgun.LoadInOunces(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Gets the maximum wad charge.
        ''' </summary>
        ''' <param name="iID">The i identifier.</param>
        ''' <returns>System.String.</returns>
        Function GetMaxWADCharge(ByVal iID As Long) As String
            Dim sAns As String = ""
            Try
                Dim SQL As String = "SELECT * from List_SG_WAD where ID=" & iID
                Dim Obj As New BSDatabase
                Obj.ConnectDB()
                Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                Dim RS As OdbcDataReader
                RS = CMD.ExecuteReader
                While RS.Read
                    sAns = RS("load_t")
                End While
                RS.Close()
                RS = Nothing
                CMD = Nothing
                Obj.CloseDB()
            Catch ex As Exception
                Call LogError(Name, "GetMaxWADCharge", Err.Number, ex.Message.ToString)
            End Try
            Return sAns
        End Function
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the cmdWAD control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub cmdWAD_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmdWAD.SelectedIndexChanged
            Dim WID As Long = cmdWAD.SelectedValue
            txtShotCharge.Text = GetMaxWADCharge(WID)
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnNext control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
            Try
                Dim PID As Long = cmbPrimer.SelectedValue
                Dim HID As Long = cmdHull.SelectedValue
                Dim WID As Long = cmdWAD.SelectedValue
                Dim sLoad As String = txtShotCharge.Text
                Dim dLoad As Double = Converters.ConvertOZToDouble(sLoad, errOut)
                Dim SLTID As Long = cmdShotType.SelectedValue
                Dim LTID As Long = cmbLoadType.SelectedValue
                Dim sSource As String = GeneralHelpers.FluffContent(txtSource.Text)
                Dim IsPersonal As Boolean = chkPersonal.Checked
                Dim iPersonal As Integer = 1
                Dim SQL As String = ""
                Dim Obj As New BSDatabase
                If Not IsPersonal Then iPersonal = 0
                SQL = "INSERT INTO Config_List_Data_SG (CLNID,ATID,CALID,PRID,CAID,SW,SW_t,WAD" & _
                      ",SCL,Source,GID,IsPersonal,LTID) VALUES(" & ConfigID & "," & CalID & "," & GID & "," & PID & _
                      "," & HID & "," & dLoad & ",'" & sLoad & "'," & WID & "," & SLTID & ",'" & _
                      sSource & "'," & GID & "," & iPersonal & "," & LTID & ")"
                Obj.ConnExec(SQL)
                SQL = "UPDATE Config_List_Name set IsPersonal=" & iPersonal & " where id=" & ConfigID
                Obj.ConnExec(SQL)
                frmConfig_Add_Wizard_SG_Powder.MdiParent = MdiParent
                frmConfig_Add_Wizard_SG_Powder.ConfigID = ConfigID
                frmConfig_Add_Wizard_SG_Powder.ConfigName = ConfigName
                frmConfig_Add_Wizard_SG_Powder.CalName = CalName
                frmConfig_Add_Wizard_SG_Powder.FromConfigWiz = True
                frmConfig_Add_Wizard_SG_Powder.CalID = CalID
                frmConfig_Add_Wizard_SG_Powder.GID = GID
                frmConfig_Add_Wizard_SG_Powder.Show()
                Close()
            Catch ex As Exception
                Call LogError(Name, "btnNext.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the CheckedChanged event of the chkPersonal control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub chkPersonal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPersonal.CheckedChanged
            If chkPersonal.Checked Then
                txtSource.ReadOnly = True
            Else
                txtSource.ReadOnly = False
            End If
        End Sub
    End Class
End NameSpace