'Imports System.Data.Odbc
'Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.ConfigSheets
Imports BurnSoft.Applications.MLL.Helpers

Namespace Adding
    ' TODO #20 Code Clean Up
    ''' <summary>
    ''' Class frmConfig_Add_Wizard_Powder.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmConfigAddWizardPowder
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' The configuration name
        ''' </summary>
        Public ConfigName As String
        ''' <summary>
        ''' The configuration identifier
        ''' </summary>
        Public ConfigId As Long
        ''' <summary>
        ''' From configuration wiz
        ''' </summary>
        Public FromConfigWiz As Boolean
        ''' <summary>
        ''' Handles the Load event of the frmConfig_Add_Wizard_Powder control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub frmConfig_Add_Wizard_Powder_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                General_PowderTableAdapter.Fill(MLLDataSet.General_Powder)
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub

        '<Obsolete("Replaced by BurnSoft.Applications.MLL.ConfigSheets.ConfigListDataPowder.HasDefaultPowder")>
        'Private Function HasPerfPowder(ByVal lngID As Long) As Boolean
        '    Dim bAns As Boolean = False
        '    Try
        '        Dim Obj As New BSDatabase
        '        Dim SQL As String = "SELECT * from Config_List_Powder_Data_NSG where CLNID=" & _
        '                            lngID & " and IsPref=1"
        '        Obj.ConnectDB()
        '        Dim CMD As New OdbcCommand(SQL, Obj.Conn)
        '        Dim RS As OdbcDataReader
        '        RS = CMD.ExecuteReader
        '        bAns = RS.HasRows
        '        RS.Close()
        '        RS = Nothing
        '        CMD = Nothing
        '        Obj.CloseDB()
        '    Catch ex As Exception
        '        Call LogError(Name, "HasPerfPowder", Err.Number, ex.Message.ToString)
        '    End Try
        '    Return bAns
        'End Function
        ''' <summary>
        ''' Handles the Click event of the btnAdd control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Try
                Dim powderId As Long = cmbPowder.SelectedValue
                Dim loadMin As Double = GeneralHelpers.FluffContent(txtLMin.Text, 0)
                Dim loadMid As Double = GeneralHelpers.FluffContent(txtLMid.Text, 0)
                Dim loadMax As Double = GeneralHelpers.FluffContent(txtLMax.Text, 0)
                Dim fpsMin As Double = GeneralHelpers.FluffContent(txtMVMin.Text, 0)
                Dim fpsMid As Double = GeneralHelpers.FluffContent(txtMVMid.Text, 0)
                Dim fpsMax As Double = GeneralHelpers.FluffContent(txtMVMax.Text, 0)
                Dim cupsMin As Double = GeneralHelpers.FluffContent(txtCUPSMin.Text, 0)
                Dim cupsMid As Double = GeneralHelpers.FluffContent(txtCUPSMid.Text, 0)
                Dim cupsMax As Double = GeneralHelpers.FluffContent(txtCUPSMax.Text, 0)

                If Not GeneralHelpers.IsRequired(loadMid, 0, "Mid Load/Preferred Load", Text) Then Exit Sub

                Dim setAsDefault As Boolean = False
                ' TODO: #19 UnComment Code when you update is applied
                'If Not ConfigListDataPowder.HasDefaultPowder(DatabasePath, ConfigId, _errOut) Then setAsDefault = True
                if _errOut.Length > 0 then Throw new Exception(_errOut)
                If Not ConfigListDataPowder.Add(DatabasePath, ConfigId, powderId, loadMin, 
                                                loadMid, loadMax, fpsMin, fpsMid, fpsMax, cupsMin, 
                                                cupsMid, cupsMax, setAsDefault, _errOut) then Throw new Exception(_errOut)
                'Dim intPerf As Integer = 0
                'If Not HasPerfPowder(ConfigId) Then intPerf = 1
                'Dim Obj As New BSDatabase
                'Dim SQL As String = "INSERT INTO Config_List_Powder_Data_NSG(CLNID,PID," & _
                '                    "Load_Min,Load_Mid,Load_Max,FPS_Min,FPS_MID,FPS_Max," & _
                '                    "CUPS_Min,CUPS_Mid,CUPS_Max,IsPref) VALUES (" & ConfigId & "," & _
                '                    powderId & "," & loadMin & "," & loadMid & "," & loadMax & "," & _
                '                    fpsMin & "," & fpsMid & "," & fpsMax & "," & cupsMin & "," & _
                '                    cupsMid & "," & cupsMax & "," & intPerf & ")"
                'Obj.ConnExec(SQL)
                If FromConfigWiz Then Call MdiParentMain.RefreshConfigData()
                Close()
            Catch ex As Exception
                Call LogError(Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub

    End Class
End Namespace