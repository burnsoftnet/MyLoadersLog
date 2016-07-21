Imports System.Data
Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Public Class frmConfig_Add_Wizard_SG_Powder
    Public ConfigName As String
    Public CalID As Long
    Public CalName As String
    Public GID As Long
    Public ConfigID As Long
    Public FromConfigWiz As Boolean
    Private Sub frmConfig_Add_Wizard_SG_4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
    Sub SaveData()
        Try
            Dim dCharge As Double = FluffContent(txtCharge.Text, 0)
            Dim PID As Long = cmbPowder.SelectedValue
            Dim dFPS As Double = FluffContent(txtFPS.Text, 0)
            Dim dPIS As Double = FluffContent(txtPSI.Text, 0)
            Dim SQL As String = ""
            Dim iPref As Long = 0
            Dim Obj As New BSDatabase
            If Not HasPerfPowder(ConfigID) Then iPref = 1

            If Not IsRequired(dCharge, 0, "Load Charge", Me.Text) Then Exit Sub
            SQL = "INSERT INTO Config_List_Powder_Data_SG (CLNID,PID,Load_Mid," & _
                    "FPS_MID,PSI_Mid,IsPref) VALUES(" & ConfigID & "," & PID & _
                    "," & dCharge & "," & dFPS & "," & dPIS & "," & iPref & ")"
            Obj.ConnExec(SQL)
            If FromConfigWiz Then Call MDIParentMain.RefreshConfigData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Function HasPerfPowder(ByVal lngID As Long) As Boolean
        Dim bAns As Boolean = False
        Try
            Dim Obj As New BSDatabase
            Dim SQL As String = "SELECT * from Config_List_Powder_Data_SG where CLNID=" & _
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
    Sub LoadData()
        Try
            Me.General_PowderTableAdapter.FillBy_Config_List(Me.MLLDataSet.General_Powder)
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Call SaveData()
    End Sub
End Class