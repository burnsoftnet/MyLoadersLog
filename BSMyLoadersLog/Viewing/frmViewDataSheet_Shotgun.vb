'Imports System.Data.Odbc
Imports BSMyLoadersLog.Adding
'Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.LoadersLog
Imports BurnSoft.Applications.MLL.Types

Namespace Viewing
    ''' <summary>
    ''' Class FrmViewDataSheetShotgun.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmViewDataSheetShotgun
        'TODO: #20 Clean Up code
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut as String
        ''' <summary>
        ''' The firearm identifier
        ''' </summary>
        Public FirearmId As Long
        ''' <summary>
        ''' The firearm name
        ''' </summary>
        Public FirearmName As String
        ''' <summary>
        ''' Loads the ComboBox.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub LoadComboBox()
            Try
                Dim lst as List(Of FirearmCollection) = Firearms.GetAll(DatabasePath, _errOut)
                If _errOut.Length > 0 Then throw New Exception(_errOut)
                For Each o As FirearmCollection In lst
                    ToolStripComboBox1.Items.Add(o.FullName)
                Next
                'Dim Obj As New BSDatabase
                'Dim SQL As String = "SELECT * from Loaders_Log_Firearms where GType like '%shotgun%' and exclude=0 order by FullName ASC"
                'Call Obj.ConnectDB()
                'Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                'Dim RS As OdbcDataReader
                'RS = CMD.ExecuteReader
                'Dim FullName As String = ""

                'While RS.Read
                '    ToolStripComboBox1.Items.Add(RS("FullName"))
                '    'If Len(ToolStripComboBox1.Text) = 0 Then FullName = RS("FullName")
                'End While
                'RS.Close()
                'RS = Nothing
                'CMD = Nothing
                'Obj.CloseDB()
                'ToolStripComboBox1.Text = FullName
                ToolStripComboBox1.SelectedIndex = 0
            Catch ex As Exception
                Call LogError(Name, "LoadComboBox", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Updates the labels.
        ''' </summary>
        Sub UpdateLabels()
            Try
                'Dim Obj As New GlobalFunctions
                'FirearmId = Obj.GetFirearmID(FirearmName)
                FirearmId = Firearms.GetId(DatabasePath, FirearmName, _errOut)
                If _errOut.Length > 0 Then throw New Exception(_errOut)
                'Dim sCal As String = ""
                'Dim sSerial As String = ""
                'Dim sBar As String = ""
                'Call Obj.GetFirearmDetails(FirearmId, 0, "", "", "", sCal, sBar, sSerial)
                'tslCal.Text = "Caliber: " & sCal
                'tslBarrel.Text = "Barrel: " & sBar
                'tslSerialNo.Text = "Serial No. " & sSerial

                Dim lst as List(Of FirearmCollection) = Firearms.GetDetails(DatabasePath, CInt(FirearmId), _errOut)
                If _errOut.Length > 0 Then throw New Exception(_errOut)
                For Each o As FirearmCollection In lst
                    tslCal.Text = $"Caliber: {o.Caliber}"
                    tslBarrel.Text = $"Barrel: {o.Barrel}"
                    tslSerialNo.Text = $"Serial No. {o.SerialNo}"
                Next
            Catch ex As Exception
                Call LogError(Name, "UpdateLabels", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the WithConfigToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub WithConfigToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WithConfigToolStripMenuItem.Click
            Cursor = Cursors.WaitCursor
            Try
                Dim frmNew As New frmReport_DataLoader_Shotgun
                frmNew.FID = FirearmId
                frmNew.FirearmName = FirearmName
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "WithConfigToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
            Cursor = Cursors.Arrow
        End Sub
        ''' <summary>
        ''' Handles the Click event of the WithoutConfigToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub WithoutConfigToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WithoutConfigToolStripMenuItem.Click
            Cursor = Cursors.WaitCursor
            Try
                Dim frmNew As New frmReport_DataLoader_ShotgunWOC
                frmNew.FID = FirearmId
                frmNew.FirearmName = FirearmName
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "WithoutConfigToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
            Cursor = Cursors.Arrow
        End Sub
        ''' <summary>
        ''' Loads the databy identifier.
        ''' </summary>
        Sub LoadDatabyId()
            Try
                Loaders_Log_SGTableAdapter.FillBy_FID(MLLDataSet.Loaders_Log_SG, FirearmId)
                'Loaders_Log_SGTableAdapter.FillBy_FID(MLLDataSet.Loaders_Log_SG, FID)
            Catch ex As Exception
                Call LogError(Name, "LoadDatabyID", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Loads the name of the databy.
        ''' </summary>
        Sub LoadDatabyName()
            Try
                Loaders_Log_SGTableAdapter.FillBy_FirearmName(MLLDataSet.Loaders_Log_SG, FirearmName)
            Catch ex As Exception
                Call LogError(Name, "LoadDatabyName", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            Call LoadComboBox()
            If FirearmId = 0 Then
                FirearmName = ToolStripComboBox1.Text
                Call LoadDatabyName()
            Else
                Call LoadDatabyId()
                ToolStripComboBox1.Text = FirearmName
            End If
            Call UpdateLabels()
        End Sub
        ''' <summary>
        ''' Loads the data current.
        ''' </summary>
        Public Sub LoadDataCur()
            FirearmName = ToolStripComboBox1.Text
            Call LoadDatabyName()
            Call UpdateLabels()
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmViewDataSheet_Shotgun control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmViewDataSheet_Shotgun_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ManuallyToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ManuallyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ManuallyToolStripMenuItem.Click
            Try
                Dim frmNew As New frmAddDataSheet_ShotGun_MAN
                frmNew.FID = FirearmId
                frmNew.MdiParent = MdiParent
                frmNew.FromView = True
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "ManuallyToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the UseConfigurationToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub UseConfigurationToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles UseConfigurationToolStripMenuItem.Click
            Try
                Dim frmNew As New FrmAddDataSheetShotGunCfg
                frmNew.Fid = FirearmId
                frmNew.MdiParent = MdiParent
                frmNew.FromView = True
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "UseConfigurationToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the ToolStripComboBox1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
            Call LoadDataCur()
        End Sub
        ''' <summary>
        ''' Handles the Resize event of the frmViewDataSheet_Shotgun control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmViewDataSheet_Shotgun_Resize(sender As Object, e As EventArgs) Handles Me.Resize
            Try
                If Height <> 0 Then
                    DataGridView1.Height = Height - (65)
                    DataGridView1.Width = Width - 15
                End If
            Catch ex As Exception
                Call LogError(Name, "frmViewDataSheet_Shotgun_Resize", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Deletes the log data.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub Delete_LogData()
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                'Dim obj As New BSDatabase
                'Dim objG As New GlobalFunctions
                'Dim strSQLTable As String = "Loaders_Log_SG"
                Dim strAns As String = MsgBox("Are you sure you want to delete this from the Log?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & itemId
                'If strAns = vbYes Then obj.ConnExec(SQL) : Call LoadDataCur()

                If strAns = vbYes Then 
                    If Not LoadersLogShotgun.Delete(DatabasePath, itemId, _errOut) Then Throw New Exception(_errOut)
                    Call LoadDataCur()
                End If
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton3 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
            Call Delete_LogData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the DeleteToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
            Call Delete_LogData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the EditToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim frmNew As New frmEditDataSheet_ShotGun
                frmNew.CFGID = itemId
                frmNew.FromView = True
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "EditToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the CopyToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
            Try
                Dim frmNew As New FrmCopyDataSheetCopyShotGun
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                frmNew.CFGID = itemId
                frmNew.FromView = True
                frmNew.MdiParent = MdiParent
                frmNew.Show()
            Catch ex As Exception
                Call LogError(Name, "CopyToolStripMenuItem_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace