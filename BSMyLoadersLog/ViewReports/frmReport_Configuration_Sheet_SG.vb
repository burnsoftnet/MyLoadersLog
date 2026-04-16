Imports Microsoft.Reporting.WinForms

Namespace ViewReports
    ''' <summary>
    ''' Class frmReport_Configuration_Sheet_SG.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmReportConfigurationSheetSg
        ''' <summary>
        ''' The configuration identifier
        ''' </summary>
        Public ConfigId As Long
        ''' <summary>
        ''' The configuration name
        ''' </summary>
        Public ConfigName As String
        ''' <summary>
        ''' The configuration ammo type
        ''' </summary>
        Public ConfigAmmoType As String
        ''' <summary>
        ''' The configuration caliber
        ''' </summary>
        Public ConfigCaliber As String
        ''' <summary>
        ''' The configuration notes
        ''' </summary>
        Public ConfigNotes As String
        ''' <summary>
        ''' The projectile manufacturer
        ''' </summary>
        Public ProjectileManufacturer As String
        ''' <summary>
        ''' The projectile name
        ''' </summary>
        Public ProjectileName As String
        ''' <summary>
        ''' The projectile shot no
        ''' </summary>
        Public ProjectileShotNo As String
        ''' <summary>
        ''' The projectile material
        ''' </summary>
        Public ProjectileMaterial As String
        ''' <summary>
        ''' The projectile selected load
        ''' </summary>
        Public ProjectileSelectedLoad As String
        ''' <summary>
        ''' The wad manufacturer
        ''' </summary>
        Public WadManufacturer As String
        ''' <summary>
        ''' The wad name
        ''' </summary>
        Public WadName As String
        ''' <summary>
        ''' The wad maximum load
        ''' </summary>
        Public WadMaxLoad As String
        ''' <summary>
        ''' The primer manufacturer
        ''' </summary>
        Public PrimerManufacturer As String
        ''' <summary>
        ''' The primer name
        ''' </summary>
        Public PrimerName As String
        ''' <summary>
        ''' The primer type
        ''' </summary>
        Public PrimerType As String
        ''' <summary>
        ''' The case manufacturer
        ''' </summary>
        Public CaseManufacturer As String
        ''' <summary>
        ''' The case name
        ''' </summary>
        Public CaseName As String
        ''' <summary>
        ''' The case trim to length
        ''' </summary>
        Public CaseTrimToLength As String
        ''' <summary>
        ''' The case dram
        ''' </summary>
        Public CaseDram As String
        ''' <summary>
        ''' The configuration personal load
        ''' </summary>
        Dim _configPersonalLoad As String
        ''' <summary>
        ''' The configuration is personal
        ''' </summary>
        Public ConfigIsPersonal As Boolean
        ''' <summary>
        ''' The configuration favorite
        ''' </summary>
        Public ConfigFavorite As Boolean
        ''' <summary>
        ''' The configuration refference
        ''' </summary>
        Public ConfigRefference As String
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Sub LoadData()
            Try
                Config_List_Powder_Data_SG_ViewTableAdapter.FillBy_ConfigID(MLLDataSet.Config_List_Powder_Data_SG_View, ConfigId)
                Dim parmList As New List(Of ReportParameter)
                parmList.Add(New ReportParameter("Config_Name", ConfigName))
                parmList.Add(New ReportParameter("Config_AT", ConfigAmmoType))
                parmList.Add(New ReportParameter("Config_Cal", ConfigCaliber))
                parmList.Add(New ReportParameter("Config_Notes", ConfigNotes))
                parmList.Add(New ReportParameter("Pro_Manu", ProjectileManufacturer))
                parmList.Add(New ReportParameter("Pro_Name", ProjectileName))
                parmList.Add(New ReportParameter("Pro_Material", ProjectileMaterial))
                parmList.Add(New ReportParameter("Pro_ShotNo", ProjectileShotNo))
                parmList.Add(New ReportParameter("Pro_SelectedLoad", ProjectileSelectedLoad))
                parmList.Add(New ReportParameter("WAD_Manu", WadManufacturer))
                parmList.Add(New ReportParameter("WAD_Name", WadName))
                parmList.Add(New ReportParameter("WAD_MaxLoad", WadMaxLoad))
                parmList.Add(New ReportParameter("Pri_Manu", PrimerManufacturer))
                parmList.Add(New ReportParameter("Pri_Name", PrimerName))
                parmList.Add(New ReportParameter("Pri_PT", PrimerType))
                parmList.Add(New ReportParameter("Case_Manu", CaseManufacturer))
                parmList.Add(New ReportParameter("Case_Name", CaseName))
                parmList.Add(New ReportParameter("Case_TTL", CaseTrimToLength))
                parmList.Add(New ReportParameter("Case_DRAM", CaseDram))
                If ConfigIsPersonal Then
                    _configPersonalLoad = "Yes"
                Else
                    _configPersonalLoad = "No (" & ConfigRefference & ")"
                End If
                parmList.Add(New ReportParameter("Config_PersonalLoad", _configPersonalLoad))
                ReportViewer1.LocalReport.SetParameters(parmList)
                ReportViewer1.RefreshReport()
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmReport_Configuration_Sheet_SG control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmReport_Configuration_Sheet_SG_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call LoadData()
        End Sub
    End Class
End NameSpace