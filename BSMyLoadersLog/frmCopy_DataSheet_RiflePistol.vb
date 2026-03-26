'Imports BSMyLoadersLog.LoadersClass
'Imports System.Data.Odbc
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Types
Imports BurnSoft.Applications.MLL.LoadersLog
''' <summary>
''' Class FrmCopyDataSheetRiflePistol.
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class FrmCopyDataSheetRiflePistol
    ''' <summary>
    ''' The error out
    ''' </summary>
    Dim _errOut As String
    ''' <summary>
    ''' The configuration identifier
    ''' </summary>
    Public ConfigId As Long
    ''' <summary>
    ''' From view
    ''' </summary>
    Public FromView As Boolean
    ''' <summary>
    ''' Loads the data.
    ''' </summary>
    ''' <exception cref="System.Exception"></exception>
    Sub LoadData()
        Try
            Loaders_Log_FirearmsTableAdapter.Fill(MLLDataSet.Loaders_Log_Firearms)
            Dim strSplit() As String
            Dim lst As List(Of LoadersLogMetallicData) = LoadersLogMetallic.GetDetails(DatabasePath, ConfigId, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As LoadersLogMetallicData In lst
                cmbFirearm.SelectedValue = o.FirearmId
                dtpTested.Value = o.DateCreated
                txtGroup.Text = o.GroupSize
                nudShots.Value = o.NumberOfShots
                nudYards.Value = o.Yards
                strSplit = Split(o.PowderDetails, " - ")
                txtPowName.Text = Trim(strSplit(0))
                txtPowWei.Text = Trim(strSplit(1))
                txtPowManu.Text = Trim(strSplit(2))
                txtBullet.Text = o.BulletDetails
                txtPrimer.Text = o.PrimerDetails
                txtCase.Text = o.CaseDetails
                txtCon.Text = o.Conditions
                txtLen.Text = o.TotalLenght
                txtNotes.Text = o.Notes
            Next
            'Dim SQL As String = "SELECT * from Loaders_Log_NSG where ID=" & ConfigId
            'Dim Obj As New BSDatabase
            'Call Obj.ConnectDB()
            'Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            'Dim RS As OdbcDataReader
            'RS = CMD.ExecuteReader
            ''Dim strSplit() As String
            'While RS.Read
            '    cmbFirearm.SelectedValue = RS("fid")
            '    dtpTested.Value = RS("dt")
            '    txtGroup.Text = RS("gs")
            '    nudShots.Value = RS("ns")
            '    nudYards.Value = RS("yds")
            '    strSplit = Split(RS("pwm"), " - ")
            '    txtPowName.Text = Trim(strSplit(0))
            '    txtPowWei.Text = Trim(strSplit(1))
            '    txtPowManu.Text = Trim(strSplit(2))
            '    'txtPowwtMFG.Text = RS("pwm")
            '    txtBullet.Text = RS("bullet")
            '    txtPrimer.Text = RS("primer")
            '    txtCase.Text = RS("case")
            '    txtCon.Text = RS("conditions")
            '    txtLen.Text = RS("tl")
            '    txtNotes.Text = RS("notes")
            'End While
            'RS.Close()
            'RS = Nothing
            'CMD = Nothing
            'Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Saves the data.
    ''' </summary>
    ''' <exception cref="System.Exception"></exception>
    Sub SaveData()
        Try
            Dim firearmId As Long = cmbFirearm.SelectedValue
            Dim firearmName As String = cmbFirearm.Text
            Dim dateCreated As String = dtpTested.Value
            Dim groupSize As String = GeneralHelpers.FluffContent(txtGroup.Text)
            Dim numShots As Long = nudShots.Value
            Dim yards As Long = nudYards.Value
            Dim powderName As String = GeneralHelpers.FluffContent(txtPowName.Text)
            Dim powderWeight As String = GeneralHelpers.FluffContent(txtPowWei.Text)
            Dim powderManufacturer As String = GeneralHelpers.FluffContent(txtPowManu.Text)
            Dim bulletDetails As String = GeneralHelpers.FluffContent(txtBullet.Text)
            Dim primerDetails As String = GeneralHelpers.FluffContent(txtPrimer.Text)
            Dim caseDetails As String = GeneralHelpers.FluffContent(txtCase.Text)
            Dim condition As String = GeneralHelpers.FluffContent(txtCon.Text)
            Dim oal As String = GeneralHelpers.FluffContent(txtLen.Text)
            Dim notes As String = GeneralHelpers.FluffContent(txtNotes.Text)
            Dim configName As String = "N/A"
            Dim barrelLenght As String = ""
            Dim caliber As String = ""
            'Dim Obj As New BSDatabase
            'Dim ObjIM As New InventoryMath
            'Dim ObjGF As New GlobalFunctions
            Dim lst As List(Of FirearmCollection) = Firearms.GetDetails(DatabasePath, CInt(firearmId), _errOut)
            For Each o As FirearmCollection In lst
                caliber = o.Caliber
                barrelLenght = o.Barrel
            Next
            'Call ObjGF.GetFirearmDetails(lngFID, 0, "", "", "", Caliber, strBarLen)
            If Not LoadersLogMetallic.Add(DatabasePath, firearmId, dateCreated, CInt(yards), groupSize, 
                                          CInt(numShots), $"{powderName} - {powderWeight} - {powderManufacturer}", 
                                          bulletDetails, primerDetails, caseDetails, condition, oal, notes,
                                          configName, firearmName, caliber, barrelLenght, 
                                          _errOut) Then Throw New Exception(_errOut)
            'Dim Sql As String = "INSERT INTO Loaders_Log_NSG (fid,dt,yds,gs,ns,pwm,bullet," & _
            '        "primer,case,conditions,tl,notes,ConfigName,FirearmName,Caliber,BarrelLen)" & _
            '        " VALUES (" & lngFID & ",'" & strDateTested & "'," & lngYards & _
            '        ",'" & strGroup & "'," & lngNumShots & ",'" & strPowName & " - " & strPowWei & _
            '        " - " & strPowManu & "','" & strBullet & _
            '        "','" & strPrimer & "','" & strCase & _
            '        "','" & strCond & "','" & strLen & "','" & strNotes & "','" & _
            '        ConfigName & "','" & strFireArm & "','" & Caliber & "','" & strBarLen & "')"
            'Obj.ConnExec(Sql)
            MsgBox("Information was saved to the Loaders Log!")
            If FromView Then Call frmViewDataSheet_RiflePistols.LoadDataCur()
            Close()
        Catch ex As Exception
            Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Load event of the frmCopy_DataSheet_RiflePistol control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub frmCopy_DataSheet_RiflePistol_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnAdd control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnCancel control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub
End Class