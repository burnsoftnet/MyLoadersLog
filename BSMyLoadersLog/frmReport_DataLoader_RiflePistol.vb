Imports BSMyLoadersLog.LoadersClass
Imports Microsoft.Reporting.WinForms
Public Class frmReport_DataLoader_RiflePistol
    Public FID As Long
    Public FirearmName As String
    Public BySorting As String
    Dim Barlen As String
    Dim Cal As String
    Dim SerialNo As String
    Private Sub DoFit()
        If Me.Height <> 0 Then
            Me.ReportViewer1.Height = Me.Height - 36
            Me.ReportViewer1.Width = Me.Width - 5
        End If
    End Sub
    Sub LoadData()
        Try
            Dim ObjGF As New GlobalFunctions
            Call ObjGF.GetFirearmDetails(FID, 0, "", "", "", Cal, Barlen, SerialNo)
            Me.Loaders_Log_NSGTableAdapter.FillBy_FID(Me.MLLDataSet.Loaders_Log_NSG, FID)
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
    Private Sub frmReport_DataLoader_RiflePistol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Private Sub frmReport_DataLoader_RiflePistol_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Call DoFit()
    End Sub
End Class