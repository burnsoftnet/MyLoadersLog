Imports System.Data
Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Public Class frmConfig_Add_Wizard_SG_1
    Public ConfigName As String
    Public CalID As Long
    Public CalName As String
    Public ConfigID As Long
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnCon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCon.Click
        Try
            Dim bShot As Boolean = chkShot.Checked
            Dim bSlug As Boolean = chkSlug.Checked
            If bShot Then
                frmConfig_Add_Wizard_SG_2.ConfigID = ConfigID
                frmConfig_Add_Wizard_SG_2.CalID = CalID
                frmConfig_Add_Wizard_SG_2.CalName = CalName
                frmConfig_Add_Wizard_SG_2.GID = GenerateGaugeID(CalName)
                frmConfig_Add_Wizard_SG_2.ConfigName = ConfigName
                frmConfig_Add_Wizard_SG_2.MdiParent = Me.MdiParent
                frmConfig_Add_Wizard_SG_2.Show()
                Me.Close()
            End If
            If bSlug Then
                frmConfig_Add_Wizard_SG_3.ConfigID = ConfigID
                frmConfig_Add_Wizard_SG_3.CalID = CalID
                frmConfig_Add_Wizard_SG_3.CalName = CalName
                frmConfig_Add_Wizard_SG_3.GID = GenerateGaugeID(CalName)
                frmConfig_Add_Wizard_SG_3.ConfigName = ConfigName
                frmConfig_Add_Wizard_SG_3.MdiParent = Me.MdiParent
                frmConfig_Add_Wizard_SG_3.Show()
                Me.Close()
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "btnCon_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Function GenerateGaugeID(ByVal sCalName As String) As Long
        Dim lAns As Long = 0
        Dim Obj As New BSDatabase
        lAns = GetGaugeID(sCalName)
        If lAns = 0 Then
            Obj.ConnExec("INSERT INTO List_SG_Gauge(ga) VALUES('" & sCalName & "')")
            lAns = GetGaugeID(sCalName)
        End If
        Return lAns
    End Function
    Function GetGaugeID(ByVal sCalName As String) As Long
        Dim lAns As Long = 0
        Try
            Dim SQL As String = "SELECT ID from List_SG_Gauge where ga='" & sCalName & "'"
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                lAns = RS("ID")
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "GetGaugeID", Err.Number, ex.Message.ToString)
        End Try
        Return lAns
    End Function
    Private Sub chkShot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShot.CheckedChanged
        If chkShot.Checked Then chkSlug.Checked = False
    End Sub

    Private Sub chkSlug_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSlug.CheckedChanged
        If chkSlug.Checked Then chkShot.Checked = False
    End Sub

    Private Sub frmConfig_Add_Wizard_SG_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class