Imports BSMyLoadersLog.LoadersClass
Imports Microsoft.Reporting.WinForms
Public Class frmReport_DataLoader_Shotgun
    Public FID As Long
    Public FirearmName As String
    Public BySorting As String
    Dim Barlen As String
    Dim Cal As String
    Dim SerialNo As String
    Sub LoadData()
        Try
            Dim ObjGF As New GlobalFunctions
            Call ObjGF.GetFirearmDetails(FID, 0, "", "", "", Cal, Barlen, SerialNo)
            'Me.Loaders_Log_NSGTableAdapter.FillBy_FID(Me.MLLDataSet.Loaders_Log_NSG, FID)
            Dim parmList As New Generic.List(Of ReportParameter)
            parmList.Add(New ReportParameter("Firearm", FirearmName))
            parmList.Add(New ReportParameter("SerialNo", SerialNo))
            parmList.Add(New ReportParameter("Cal", Cal))
            parmList.Add(New ReportParameter("Barlen", Barlen))
            Me.ReportViewer1.LocalReport.SetParameters(parmList)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmReport_DataLoader_Shotgun_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Loaders_Log_SGTableAdapter.Fill(Me.MLLDataSet.Loaders_Log_SG)
        Call LoadData()
    End Sub
End Class