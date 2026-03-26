Imports BSMyLoadersLog.Adding
Imports BurnSoft.Applications.MLL.LoadersLog
Imports BurnSoft.Applications.MLL.Types

Namespace Viewing
    ''' <summary>
    ''' Class FrmViewDataSheetRiflePistols.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmViewDataSheetRiflePistols
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' The firearm identifier
        ''' </summary>
        Dim _firearmId As Long
        ''' <summary>
        ''' The firearm name
        ''' </summary>
        Dim _firearmName As String
        ''' <summary>
        ''' Loads the ComboBox.
        ''' </summary>
        Sub LoadComboBox()
            Try
                Dim i As Integer = 0
                Dim lst as List(Of FirearmCollection) = Firearms.GetAll(DatabasePath, _errOut)

                For Each o As FirearmCollection In lst
                    If Not o.GunType.ToLower().Contains("shotgun") Then
                        If Not o.Exclude Then
                            ToolStripComboBox1.Items.Add(o.FullName)
                            If i = 0 Then ToolStripComboBox1.Text = o.FullName
                            i += 1
                        End If
                    End If 
                Next
            Catch ex As Exception
                Call LogError(Name, "LoadComboBox", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Updates the labels.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub UpdateLabels()
            Try
                _firearmId = Firearms.GetId(DatabasePath, _firearmName, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)

                Dim lst as List(Of FirearmCollection) = Firearms.GetDetails(DatabasePath,CInt(_firearmId), _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As FirearmCollection In lst
                    tslCal.Text = $"Caliber: {o.Caliber}"
                    tslBarrel.Text = $"Barrel: {o.Barrel}"
                    tslSerialNo.Text = $"Serial No.: {o.SerialNo}"
                Next
            Catch ex As Exception
                Call LogError(Name, "UpdateLabels", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Loads the databy identifier.
        ''' </summary>
        Sub LoadDatabyId()
            Loaders_Log_NSGTableAdapter.FillBy_FID(MLLDataSet.Loaders_Log_NSG, _firearmId)
        End Sub
        ''' <summary>
        ''' Loads the name of the databy.
        ''' </summary>
        Sub LoadDatabyName()
            Loaders_Log_NSGTableAdapter.FillBy_FirearmName(MLLDataSet.Loaders_Log_NSG, _firearmName)
        End Sub
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Public Sub LoadData()
            Call LoadComboBox()
            If _firearmId = 0 Then
                _firearmName = ToolStripComboBox1.Text
                Call LoadDatabyName()
            Else
                Call LoadDatabyId()
                ToolStripComboBox1.Text = _firearmName
            End If
            Call UpdateLabels()
        End Sub
        ''' <summary>
        ''' Loads the data current.
        ''' </summary>
        Public Sub LoadDataCur()
            _firearmName = ToolStripComboBox1.Text
            Call LoadDatabyName()
            Call UpdateLabels()
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmViewDataSheet_RiflePistols control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmViewDataSheet_RiflePistols_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the ToolStripComboBox1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
            Call LoadDataCur()
        End Sub
        ''' <summary>
        ''' Handles the Resize event of the frmViewDataSheet_RiflePistols control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmViewDataSheet_RiflePistols_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
            If Height <> 0 Then
                DataGridView1.Height = Height - (65)
                DataGridView1.Width = Width - 15
            End If
        End Sub
        ''' <summary>
        ''' Handles the Click event of the UseConfigurationToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub UseConfigurationToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles UseConfigurationToolStripMenuItem.Click
            Dim frmNew As New FrmAddDataSheetRiflePistolsCfg
            frmNew.Fid = _firearmId
            frmNew.MdiParent = MdiParent
            frmNew.FromView = True
            frmNew.Show()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ManuallyToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ManuallyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ManuallyToolStripMenuItem.Click
            Dim frmNew As New FrmAddDataSheetRiflePistolsMan
            frmNew.Fid = _firearmId
            frmNew.MdiParent = MdiParent
            frmNew.FromView = True
            frmNew.Show()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton2.Click
            Call LoadDataCur()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the WithConfigToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub WithConfigToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WithConfigToolStripMenuItem.Click
            Cursor = Cursors.WaitCursor
            Dim frmNew As New frmReport_DataLoader_RiflePistol
            frmNew.FID = _firearmId
            frmNew.FirearmName = _firearmName
            frmNew.MdiParent = MdiParent
            frmNew.Show()
            Cursor = Cursors.Arrow
        End Sub
        ''' <summary>
        ''' Handles the Click event of the WithoutConfigToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub WithoutConfigToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WithoutConfigToolStripMenuItem.Click
            Cursor = Cursors.WaitCursor
            Dim frmNew As New frmReport_DataLoader_RiflePistolWOC
            frmNew.FID = _firearmId
            frmNew.FirearmName = _firearmName
            frmNew.MdiParent = MdiParent
            frmNew.Show()
            Cursor = Cursors.Arrow
        End Sub
        ''' <summary>
        ''' Deletes the log data.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub Delete_LogData()
            Try
                Dim itemId As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                Dim strAns As String = MsgBox("Are you sure you want to delete this from the Log?", MsgBoxStyle.YesNo, "Delete Item from the Database.")

                If strAns = vbYes Then
                    If Not LoadersLogMetallic.Delete(DatabasePath, CLng(itemId), _errOut) Then Throw New Exception(_errOut)
                    Call LoadDataCur()
                End If
            Catch ex As Exception
                Dim strProcedure As String = "LoadData"
                Call LogError(Name, strProcedure, Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton3 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton3.Click
            Call Delete_LogData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the DeleteToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
            Call Delete_LogData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the EditToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub EditToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditToolStripMenuItem.Click
            Dim itemId As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim frmNew As New frmEditDataSheet_RiflePistols
            frmNew.CFGID = itemId
            frmNew.FromView = True
            frmNew.MdiParent = MdiParent
            frmNew.Show()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the CopyToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
            Dim frmNew As New FrmCopyDataSheetRiflePistol
            Dim itemId As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            frmNew.ConfigId = itemId
            frmNew.FromView = True
            frmNew.MdiParent = MdiParent
            frmNew.Show()
        End Sub
    End Class
End NameSpace