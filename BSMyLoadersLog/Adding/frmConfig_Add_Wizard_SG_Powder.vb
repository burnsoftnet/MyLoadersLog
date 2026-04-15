Imports System.Data
Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Helpers
''' <summary>
''' Class frmConfig_Add_Wizard_SG_Powder.
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class frmConfig_Add_Wizard_SG_Powder
    ''' <summary>
    ''' The configuration name
    ''' </summary>
    Public ConfigName As String
    ''' <summary>
    ''' The cal identifier
    ''' </summary>
    Public CalID As Long
    ''' <summary>
    ''' The cal name
    ''' </summary>
    Public CalName As String
    ''' <summary>
    ''' The gid
    ''' </summary>
    Public GID As Long
    ''' <summary>
    ''' The configuration identifier
    ''' </summary>
    Public ConfigID As Long
    ''' <summary>
    ''' From configuration wiz
    ''' </summary>
    Public FromConfigWiz As Boolean
    ''' <summary>
    ''' Handles the Load event of the frmConfig_Add_Wizard_SG_4 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub frmConfig_Add_Wizard_SG_4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
    ''' <summary>
    ''' Saves the data.
    ''' </summary>
    Sub SaveData()
        Try
            Dim dCharge As Double = GeneralHelpers.FluffContent(txtCharge.Text, 0)
            Dim PID As Long = cmbPowder.SelectedValue
            Dim dFPS As Double = GeneralHelpers.FluffContent(txtFPS.Text, 0)
            Dim dPIS As Double = GeneralHelpers.FluffContent(txtPSI.Text, 0)
            Dim SQL As String = ""
            Dim iPref As Long = 0
            Dim Obj As New BSDatabase
            If Not HasPerfPowder(ConfigID) Then iPref = 1

            If Not GeneralHelpers.IsRequired(dCharge, 0, "Load Charge", Me.Text) Then Exit Sub
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
    ''' <summary>
    ''' Determines whether [has perf powder] [the specified LNG identifier].
    ''' </summary>
    ''' <param name="lngID">The LNG identifier.</param>
    ''' <returns><c>true</c> if [has perf powder] [the specified LNG identifier]; otherwise, <c>false</c>.</returns>
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
    ''' <summary>
    ''' Loads the data.
    ''' </summary>
    Sub LoadData()
        Try
            Me.General_PowderTableAdapter.FillBy_Config_List(Me.MLLDataSet.General_Powder)
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnSave control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Call SaveData()
    End Sub
End Class