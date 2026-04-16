'Imports System.Data
'Imports System.Data.Odbc
'Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Inventory

Namespace Adding
    ''' <summary>
    ''' Class frmConfig_Add_Wizard_SG_1.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmConfigAddWizardSg1
        ' TODO: #20 Code Cleanup
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut as String
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
        ''' The configuration identifier
        ''' </summary>
        Public ConfigId As Long
        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnCon control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub btnCon_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCon.Click
            Try
                Dim bShot As Boolean = chkShot.Checked
                Dim bSlug As Boolean = chkSlug.Checked
                Dim gaugeId as Long = ShotgunGauges.GenerateGaugeId(DatabasePath, CaliberName, _errOut)
                If _errOut.Length > 0 then Throw New Exception(_errOut)

                If bShot Then
                    FrmConfigAddWizardSg2.ConfigId = ConfigId
                    FrmConfigAddWizardSg2.CaliberId = CaliberId
                    FrmConfigAddWizardSg2.CaliberName = CaliberName
                    FrmConfigAddWizardSg2.GaugeId = gaugeId
                    FrmConfigAddWizardSg2.ConfigName = ConfigName
                    FrmConfigAddWizardSg2.MdiParent = MdiParent
                    FrmConfigAddWizardSg2.Show()
                    Close()
                End If
                If bSlug Then
                    FrmConfigAddWizardSg3.ConfigId = ConfigId
                    FrmConfigAddWizardSg3.CaliberId = CaliberId
                    FrmConfigAddWizardSg3.CaliberName = CaliberName
                    FrmConfigAddWizardSg3.GaugeId = gaugeId
                    FrmConfigAddWizardSg3.ConfigName = ConfigName
                    FrmConfigAddWizardSg3.MdiParent = MdiParent
                    FrmConfigAddWizardSg3.Show()
                    Close()
                End If
            Catch ex As Exception
                Call LogError(Name, "btnCon_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
 
        'Function GenerateGaugeId(ByVal sCalName As String) As Long
        '    Dim lAns As Long = 0
        '    Dim obj As New BSDatabase
        '    lAns = GetGaugeId(sCalName)
        '    If lAns = 0 Then
        '        obj.ConnExec("INSERT INTO List_SG_Gauge(ga) VALUES('" & sCalName & "')")
        '        lAns = GetGaugeId(sCalName)
        '    End If
        '    Return lAns
        'End Function
        
        'Function GetGaugeId(ByVal sCalName As String) As Long
        '    Dim lAns As Long = 0
        '    Try
        '        Dim sql As String = "SELECT ID from List_SG_Gauge where ga='" & sCalName & "'"
        '        Dim obj As New BSDatabase
        '        Call obj.ConnectDB()
        '        Dim cmd As New OdbcCommand(sql, obj.Conn)
        '        Dim rs As OdbcDataReader
        '        rs = cmd.ExecuteReader
        '        While rs.Read
        '            lAns = rs("ID")
        '        End While
        '        rs.Close()
        '        rs = Nothing
        '        cmd = Nothing
        '        obj.CloseDB()
        '    Catch ex As Exception
        '        Call LogError(Name, "GetGaugeID", Err.Number, ex.Message.ToString)
        '    End Try
        '    Return lAns
        'End Function
        ''' <summary>
        ''' Handles the CheckedChanged event of the chkShot control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub chkShot_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShot.CheckedChanged
            If chkShot.Checked Then chkSlug.Checked = False
        End Sub
        ''' <summary>
        ''' Handles the CheckedChanged event of the chkSlug control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub chkSlug_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkSlug.CheckedChanged
            If chkSlug.Checked Then chkShot.Checked = False
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmConfig_Add_Wizard_SG_1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmConfig_Add_Wizard_SG_1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        End Sub
    End Class
End NameSpace