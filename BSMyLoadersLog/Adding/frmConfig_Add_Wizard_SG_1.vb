'Imports System.Data
Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass

Namespace Adding
    'TODO: #19 This section is missing for table List_SG_Gauge
    ''' <summary>
    ''' Class frmConfig_Add_Wizard_SG_1.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmConfigAddWizardSg1
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
                If bShot Then
                    frmConfig_Add_Wizard_SG_2.ConfigID = ConfigId
                    frmConfig_Add_Wizard_SG_2.CalID = CaliberId
                    frmConfig_Add_Wizard_SG_2.CalName = CaliberName
                    frmConfig_Add_Wizard_SG_2.GID = GenerateGaugeId(CaliberName)
                    frmConfig_Add_Wizard_SG_2.ConfigName = ConfigName
                    frmConfig_Add_Wizard_SG_2.MdiParent = MdiParent
                    frmConfig_Add_Wizard_SG_2.Show()
                    Close()
                End If
                If bSlug Then
                    frmConfig_Add_Wizard_SG_3.ConfigID = ConfigId
                    frmConfig_Add_Wizard_SG_3.CalID = CaliberId
                    frmConfig_Add_Wizard_SG_3.CalName = CaliberName
                    frmConfig_Add_Wizard_SG_3.GID = GenerateGaugeId(CaliberName)
                    frmConfig_Add_Wizard_SG_3.ConfigName = ConfigName
                    frmConfig_Add_Wizard_SG_3.MdiParent = MdiParent
                    frmConfig_Add_Wizard_SG_3.Show()
                    Close()
                End If
            Catch ex As Exception
                Call LogError(Name, "btnCon_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Generates the gauge identifier.
        ''' </summary>
        ''' <param name="sCalName">Name of the s cal.</param>
        ''' <returns>System.Int64.</returns>
        Function GenerateGaugeId(ByVal sCalName As String) As Long
            Dim lAns As Long = 0
            Dim obj As New BSDatabase
            lAns = GetGaugeId(sCalName)
            If lAns = 0 Then
                obj.ConnExec("INSERT INTO List_SG_Gauge(ga) VALUES('" & sCalName & "')")
                lAns = GetGaugeId(sCalName)
            End If
            Return lAns
        End Function
        ''' <summary>
        ''' Gets the gauge identifier.
        ''' </summary>
        ''' <param name="sCalName">Name of the s cal.</param>
        ''' <returns>System.Int64.</returns>
        Function GetGaugeId(ByVal sCalName As String) As Long
            Dim lAns As Long = 0
            Try
                Dim sql As String = "SELECT ID from List_SG_Gauge where ga='" & sCalName & "'"
                Dim obj As New BSDatabase
                Call obj.ConnectDB()
                Dim cmd As New OdbcCommand(sql, obj.Conn)
                Dim rs As OdbcDataReader
                rs = cmd.ExecuteReader
                While rs.Read
                    lAns = rs("ID")
                End While
                rs.Close()
                rs = Nothing
                cmd = Nothing
                obj.CloseDB()
            Catch ex As Exception
                Call LogError(Name, "GetGaugeID", Err.Number, ex.Message.ToString)
            End Try
            Return lAns
        End Function
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