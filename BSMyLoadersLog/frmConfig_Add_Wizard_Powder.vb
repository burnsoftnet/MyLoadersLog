Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmConfig_Add_Wizard_Powder
    Public ConfigName As String
    Public ConfigID As Long
    Public FromConfigWiz As Boolean
    Private Sub frmConfig_Add_Wizard_Powder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.General_PowderTableAdapter.Fill(Me.MLLDataSet.General_Powder)
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Function HasPerfPowder(ByVal lngID As Long) As Boolean
        Dim bAns As Boolean = False
        Try
            Dim Obj As New BSDatabase
            Dim SQL As String = "SELECT * from Config_List_Powder_Data_NSG where CLNID=" & _
                                    lngID & " and IsPref=1"
            Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            bAns = RS.HasRows
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "HasPerfPowder", Err.Number, ex.Message.ToString)
        End Try
        Return bAns
    End Function
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim lngPowderID As Long = cmbPowder.SelectedValue
            Dim LMin As Double = FluffContent(txtLMin.Text, 0)
            Dim LMid As Double = FluffContent(txtLMid.Text, 0)
            Dim LMax As Double = FluffContent(txtLMax.Text, 0)
            Dim MVMin As Double = FluffContent(txtMVMin.Text, 0)
            Dim MVMid As Double = FluffContent(txtMVMid.Text, 0)
            Dim MVMax As Double = FluffContent(txtMVMax.Text, 0)
            Dim CUPSMin As Double = FluffContent(txtCUPSMin.Text, 0)
            Dim CUPSMid As Double = FluffContent(txtCUPSMid.Text, 0)
            Dim CUPSMax As Double = FluffContent(txtCUPSMax.Text, 0)
            Dim intPerf As Integer = 0
            If Not HasPerfPowder(ConfigID) Then intPerf = 1
            If Not IsRequired(LMid, 0, "Mid Load/Preferred Load", Me.Text) Then Exit Sub
            Dim Obj As New BSDatabase
            Dim SQL As String = "INSERT INTO Config_List_Powder_Data_NSG(CLNID,PID," & _
                                    "Load_Min,Load_Mid,Load_Max,FPS_Min,FPS_MID,FPS_Max," & _
                                    "CUPS_Min,CUPS_Mid,CUPS_Max,IsPref) VALUES (" & ConfigID & "," & _
                                    lngPowderID & "," & LMin & "," & LMid & "," & LMax & "," & _
                                    MVMin & "," & MVMid & "," & MVMax & "," & CUPSMin & "," & _
                                    CUPSMid & "," & CUPSMax & "," & intPerf & ")"
            Obj.ConnExec(SQL)
            If FromConfigWiz Then Call MDIParentMain.RefreshConfigData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub

End Class