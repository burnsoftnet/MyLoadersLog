Imports BSMyLoadersLog.LoadersClass
Imports Microsoft.Reporting.WinForms
''' <summary>
''' Class frmReport_Configuration_Sheet.
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class frmReport_Configuration_Sheet
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
    ''' The bullet manufacturer
    ''' </summary>
    Public BulletManufacturer As String
    ''' <summary>
    ''' The bullet name
    ''' </summary>
    Public BulletName As String
    ''' <summary>
    ''' The bullet diameter
    ''' </summary>
    Public BulletDiameter As String
    ''' <summary>
    ''' The bullet weight
    ''' </summary>
    Public BulletWeight As String
    ''' <summary>
    ''' The bullet sectional density
    ''' </summary>
    Public BulletSectionalDensity As String
    ''' <summary>
    ''' The bullet part number
    ''' </summary>
    Public BulletPartNumber As String
    ''' <summary>
    ''' The bullet ballistic coeffcient
    ''' </summary>
    Public BulletBallisticCoeffcient As String
    ''' <summary>
    ''' The bullet type
    ''' </summary>
    Public BulletType As String
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
    ''' The case times used
    ''' </summary>
    Public CaseTimesUsed As String
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
    ''' The configuration referance
    ''' </summary>
    Public ConfigReferance As String
    ''' <summary>
    ''' Loads the data.
    ''' </summary>
    Sub LoadData()
        Try
            Me.Config_List_Powder_Data_NSG_ViewTableAdapter.FillBy_ConfigID(Me.MLLDataSet.Config_List_Powder_Data_NSG_View, ConfigId)
            Dim parmList As New List(Of ReportParameter)
            parmList.Add(New ReportParameter("Config_Name", ConfigName))
            parmList.Add(New ReportParameter("Config_AT", ConfigAmmoType))
            parmList.Add(New ReportParameter("Config_Cal", ConfigCaliber))
            parmList.Add(New ReportParameter("Config_Notes", ConfigNotes))
            parmList.Add(New ReportParameter("Bul_Manu", BulletManufacturer))
            parmList.Add(New ReportParameter("Bul_Name", BulletName))
            parmList.Add(New ReportParameter("Bul_Dia", BulletDiameter))
            parmList.Add(New ReportParameter("Bul_Wei", BulletWeight))
            parmList.Add(New ReportParameter("Bul_SD", BulletSectionalDensity))
            parmList.Add(New ReportParameter("Bul_PN", BulletPartNumber))
            parmList.Add(New ReportParameter("Bul_BC", BulletBallisticCoeffcient))
            parmList.Add(New ReportParameter("Bul_BT", BulletType))
            parmList.Add(New ReportParameter("Pri_Manu", PrimerManufacturer))
            parmList.Add(New ReportParameter("Pri_Name", PrimerName))
            parmList.Add(New ReportParameter("Pri_PT", PrimerType))
            parmList.Add(New ReportParameter("Case_Manu", CaseManufacturer))
            parmList.Add(New ReportParameter("Case_Name", CaseName))
            parmList.Add(New ReportParameter("Case_TTL", CaseTrimToLength))
            parmList.Add(New ReportParameter("Case_TU", CaseTimesUsed))
            If ConfigIsPersonal Then
                _configPersonalLoad = "Yes"
            Else
                _configPersonalLoad = "No (" & ConfigReferance & ")"
            End If
            parmList.Add(New ReportParameter("Config_PersonalLoad", _configPersonalLoad))
            Me.ReportViewer1.LocalReport.SetParameters(parmList)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Load event of the frmReport_Configuration_Sheet control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub frmReport_Configuration_Sheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
End Class