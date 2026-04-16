'Imports System.Data
'Imports System.Data.Odbc
'Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.ConfigSheets
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.Types

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
        Dim _errOut As String
        ''' <summary>
        ''' The configuration name
        ''' </summary>
        Public ConfigName As String
        ''' <summary>
        ''' The cal identifier
        ''' </summary>
        Public CaliberId As Long
        ''' <summary>
        ''' The cal name
        ''' </summary>
        Public CaliberName As String
        ''' <summary>
        ''' The gauge id
        ''' </summary>
        Public GaugeId As Long
        ''' <summary>
        ''' The configuration identifier
        ''' </summary>
        Public ConfigId As Long
        ''' <summary>
        ''' Handles the Load event of the frmConfig_Add_Wizard_SG_2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmConfig_Add_Wizard_SG_2_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                List_SG_ShotType_DetailsTableAdapter.FillBy_CFG_List(MLLDataSet.List_SG_ShotType_Details)
                List_SG_CaseTableAdapter.FillBy_CFG_List(MLLDataSet.List_SG_Case, GaugeId)
                List_SG_WADTableAdapter.FillBy_CFG_WADList(MLLDataSet.List_SG_WAD, GaugeId)
                ViewPrimerListTableAdapter.FillBy_CFG_List(MLLDataSet.viewPrimerList)
                Dim wadId As Long = cmdWAD.SelectedValue
                txtShotCharge.Text = GetMaxWADCharge(wadId)
                List_SG_ShotCharge_LoadsTableAdapter.Fill(MLLDataSet.List_SG_ShotCharge_Loads)
                'Dim ObjAF As New AutoFillCollections.ShotGun
                'txtSource.AutoCompleteCustomSource = ObjAF.Config_Source_SG
                'txtShotCharge.AutoCompleteCustomSource = ObjAF.Config_LoadInOZ_SG
                'Dim ObjAF As New AutoFillCollections.ShotGun
                txtSource.AutoCompleteCustomSource = ConfigShotgun.Source(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtShotCharge.AutoCompleteCustomSource = ConfigShotgun.LoadInOunces(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Gets the maximum wad charge.
        ''' </summary>
        ''' <param name="iID">The i identifier.</param>
        ''' <returns>System.String.</returns>
        <Obsolete("Replaced with WadInventory.GetMaxCharge in 3.0.3.80-beta")>
        Function GetMaxWADCharge(ByVal iID As Long) As String
            Dim sAns As String = ""
            Try
                Dim lst As List(Of WadData) = WadInventory.GetDetails(DatabasePath, iID, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As WadData In lst
                    sAns = o.LoadInOzText
                Next
                'Dim SQL As String = "SELECT * from List_SG_WAD where ID=" & iID
                'Dim Obj As New BSDatabase
                'Obj.ConnectDB()
                'Dim CMD As New OdbcCommand(Sql, Obj.Conn)
                'Dim RS As OdbcDataReader
                'RS = CMD.ExecuteReader
                'While RS.Read
                '    sAns = RS("load_t")
                'End While
                'RS.Close()
                'RS = Nothing
                'CMD = Nothing
                'Obj.CloseDB()
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
            Try
                Dim wadId As Long = cmdWAD.SelectedValue
                txtShotCharge.Text = GetMaxWADCharge(wadId)
            Catch ex As Exception
                Call LogError(Name, "cmdWAD_SelectedIndexChanged", Err.Number, ex.Message.ToString)
            End Try
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnNext control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
            Try
                Dim primerId As Long = cmbPrimer.SelectedValue
                Dim hullId As Long = cmdHull.SelectedValue
                Dim wadId As Long = cmdWAD.SelectedValue
                Dim shotCharge As String = txtShotCharge.Text
                Dim shotChargeNumeric As Double = Converters.ConvertOzToDouble(shotCharge, _errOut)
                Dim shotTypeId As Long = cmdShotType.SelectedValue
                Dim loadTypeId As Long = cmbLoadType.SelectedValue
                Dim source As String = GeneralHelpers.FluffContent(txtSource.Text)
                ' TODO: Double check this
                If Not ConfigListDataShotgun.Add(DatabasePath, ConfigId, CaliberId, 
                                                 GaugeId, primerId, hullId, shotChargeNumeric, 
                                                 shotCharge, shotTypeId, 0, wadId, 
                                                 loadTypeId, source, GaugeId, chkPersonal.Checked, 
                                                 loadTypeId, 0, 0, 
                                                 _errOut) Then Throw new Exception(_errOut)
                If Not ConfigListDataName.SetPersonal(DatabaseName, ConfigId, chkPersonal.Checked, 
                                                      _errOut) Then Throw New Exception(_errOut)
                    'Dim isPersonal As Boolean = chkPersonal.Checked
                    'Dim iPersonal As Integer = 1
                    'Dim sql As String = ""
                    'Dim obj As New BSDatabase
                    'If Not isPersonal Then iPersonal = 0
                    'sql = "INSERT INTO Config_List_Data_SG (CLNID,ATID,CALID,PRID,CAID,SW,SW_t,WAD" & _
                    '      ",SCL,Source,GID,IsPersonal,LTID) VALUES(" & ConfigId & "," & CaliberId & "," & GaugeId & "," & primerId & _
                    '      "," & hullId & "," & shotChargeNumeric & ",'" & shotCharge & "'," & wadId & "," & shotTypeId & ",'" & _
                    '      source & "'," & GaugeId & "," & iPersonal & "," & loadTypeId & ")"
                    'obj.ConnExec(sql)
                    'sql = "UPDATE Config_List_Name set IsPersonal=" & iPersonal & " where id=" & ConfigId
                    'obj.ConnExec(sql)
                frmConfig_Add_Wizard_SG_Powder.MdiParent = MdiParent
                frmConfig_Add_Wizard_SG_Powder.ConfigID = ConfigId
                frmConfig_Add_Wizard_SG_Powder.ConfigName = ConfigName
                frmConfig_Add_Wizard_SG_Powder.CalName = CaliberName
                frmConfig_Add_Wizard_SG_Powder.FromConfigWiz = True
                frmConfig_Add_Wizard_SG_Powder.CalID = CaliberId
                frmConfig_Add_Wizard_SG_Powder.GID = GaugeId
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
End Namespace