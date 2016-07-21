Imports BSMyLoadersLog.LoadersClass
Imports Microsoft.Reporting.WinForms
Public Class frmReport_Configuration_Sheet_SG_Slug
    Public Config_ID As Long
    Public Config_Name As String
    Public Config_AT As String
    Public Config_Cal As String
    Public Config_Notes As String
    Public Pro_Manu As String
    Public Pro_Name As String
    Public Pro_Weight As String
    Public WAD_Manu As String
    Public WAD_Name As String
    Public WAD_MaxLoad As String
    Public Pri_Manu As String
    Public Pri_Name As String
    Public Pri_PT As String
    Public Case_Manu As String
    Public Case_Name As String
    Public Case_TTL As String
    Public Case_DRAM As String
    Dim Config_PersonalLoad As String
    Public Config_ISPersonal As Boolean
    Public Config_Fav As Boolean
    Public Config_Ref As String
    Sub LoadData()
        Try
            Me.Config_List_Powder_Data_SG_ViewTableAdapter.FillBy_ConfigID(Me.MLLDataSet.Config_List_Powder_Data_SG_View, Config_ID)
            Dim parmList As New Generic.List(Of ReportParameter)
            parmList.Add(New ReportParameter("Config_Name", Config_Name))
            parmList.Add(New ReportParameter("Config_AT", Config_AT))
            parmList.Add(New ReportParameter("Config_Cal", Config_Cal))
            parmList.Add(New ReportParameter("Config_Notes", Config_Notes))

            parmList.Add(New ReportParameter("Pro_Manu", Pro_Manu))
            parmList.Add(New ReportParameter("Pro_Name", Pro_Name))
            parmList.Add(New ReportParameter("Pro_Weight", Pro_Weight))
            parmList.Add(New ReportParameter("WAD_Manu", WAD_Manu))
            parmList.Add(New ReportParameter("WAD_Name", WAD_Name))
            parmList.Add(New ReportParameter("WAD_MaxLoad", WAD_MaxLoad))
            parmList.Add(New ReportParameter("Pri_Manu", Pri_Manu))
            parmList.Add(New ReportParameter("Pri_Name", Pri_Name))
            parmList.Add(New ReportParameter("Pri_PT", Pri_PT))
            parmList.Add(New ReportParameter("Case_Manu", Case_Manu))
            parmList.Add(New ReportParameter("Case_Name", Case_Name))
            parmList.Add(New ReportParameter("Case_TTL", Case_TTL))
            parmList.Add(New ReportParameter("Case_DRAM", Case_DRAM))
            If Config_ISPersonal Then
                Config_PersonalLoad = "Yes"
            Else
                Config_PersonalLoad = "No (" & Config_Ref & ")"
            End If
            parmList.Add(New ReportParameter("Config_PersonalLoad", Config_PersonalLoad))
            Me.ReportViewer1.LocalReport.SetParameters(parmList)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmReport_Configuration_Sheet_SG_Slug_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
End Class