Imports System.Data.Odbc
Imports System.Web.UI.WebControls.Expressions
Imports BSMyLoadersLog.Adding
Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.LoadersLog
Imports BurnSoft.Applications.MLL.Types

Namespace Viewing

    Public Class FrmViewDataSheetRiflePistols
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        Dim FID As Long
        Dim FirearmName As String
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

                'Dim Obj As New BSDatabase
                'Dim SQL As String = "select * from Loaders_Log_Firearms where GType not like '%shotgun%' and exclude=0 order by FullName ASC"
                'Call Obj.ConnectDB()
                'Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                'Dim RS As OdbcDataReader
                'RS = CMD.ExecuteReader
                'ToolStripComboBox1.Items.Clear()
                'While RS.Read
                '    ToolStripComboBox1.Items.Add(RS("FullName"))
                '    If i = 0 Then ToolStripComboBox1.Text = RS("FullName")
                '    i += 1
                'End While
                'RS.Close()
                'RS = Nothing
                'CMD = Nothing
                'Obj.CloseDB()
            Catch ex As Exception
                Call LogError(Name, "LoadComboBox", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Sub UpdateLabels()
            Try
                'Dim Obj As New GlobalFunctions
                'FID = Obj.GetFirearmID(FirearmName)
                FID = Firearms.GetId(DatabasePath, FirearmName, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                'Dim sCal As String = ""
                'Dim sSerial As String = ""
                'Dim sBar As String = ""
                'Call Obj.GetFirearmDetails(FID, 0, "", "", "", sCal, sBar, sSerial)
                'tslCal.Text = "Caliber: " & sCal
                'tslBarrel.Text = "Barrel: " & sBar
                'tslSerialNo.Text = "Serial No.: " & sSerial

                Dim lst as List(Of FirearmCollection) = Firearms.GetDetails(DatabasePath,CInt(FID), _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As FirearmCollection In lst
                    tslCal.Text = $"Caliber: {o.Caliber}"
                    tslBarrel.Text = $"Barrel: {o.Barrel}"
                    tslSerialNo.Text = $"Serial No.: {o.SerialNo}"
                Next
            Catch ex As Exception
                Call LogError(Me.Name, "UpdateLabels", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Sub LoadDatabyID()
            Me.Loaders_Log_NSGTableAdapter.FillBy_FID(Me.MLLDataSet.Loaders_Log_NSG, FID)
        End Sub
        Sub LoadDatabyName()
            Me.Loaders_Log_NSGTableAdapter.FillBy_FirearmName(Me.MLLDataSet.Loaders_Log_NSG, FirearmName)
        End Sub
        Public Sub LoadData()

            Call LoadComboBox()
            If FID = 0 Then
                FirearmName = ToolStripComboBox1.Text
                Call LoadDatabyName()
            Else
                Call LoadDatabyID()
                ToolStripComboBox1.Text = FirearmName
            End If
            Call UpdateLabels()
        End Sub
        Public Sub LoadDataCur()
            FirearmName = ToolStripComboBox1.Text
            Call LoadDatabyName()
            Call UpdateLabels()
        End Sub
        Private Sub frmViewDataSheet_RiflePistols_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call LoadData()
        End Sub
        Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
            Call LoadDataCur()
        End Sub
        Private Sub frmViewDataSheet_RiflePistols_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
            If Height <> 0 Then
                DataGridView1.Height = Height - (65)
                DataGridView1.Width = Width - 15
            End If
        End Sub
        Private Sub UseConfigurationToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles UseConfigurationToolStripMenuItem.Click
            Dim frmNew As New FrmAddDataSheetRiflePistolsCfg
            frmNew.Fid = FID
            frmNew.MdiParent = MdiParent
            frmNew.FromView = True
            frmNew.Show()
        End Sub

        Private Sub ManuallyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ManuallyToolStripMenuItem.Click
            Dim frmNew As New FrmAddDataSheetRiflePistolsMan
            frmNew.Fid = FID
            frmNew.MdiParent = MdiParent
            frmNew.FromView = True
            frmNew.Show()
        End Sub

        Private Sub ToolStripButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton2.Click
            Call LoadDataCur()
        End Sub

        Private Sub WithConfigToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WithConfigToolStripMenuItem.Click
            Me.Cursor = Cursors.WaitCursor
            Dim frmNew As New frmReport_DataLoader_RiflePistol
            frmNew.FID = FID
            frmNew.FirearmName = FirearmName
            frmNew.MdiParent = MdiParent
            frmNew.Show()
            Me.Cursor = Cursors.Arrow
        End Sub

        Private Sub WithoutConfigToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WithoutConfigToolStripMenuItem.Click
            Me.Cursor = Cursors.WaitCursor
            Dim frmNew As New frmReport_DataLoader_RiflePistolWOC
            frmNew.FID = FID
            frmNew.FirearmName = FirearmName
            frmNew.MdiParent = MdiParent
            frmNew.Show()
            Me.Cursor = Cursors.Arrow
        End Sub
        Sub Delete_LogData()
            Try
                Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                'Dim Obj As New BSDatabase
                'Dim ObjG As New GlobalFunctions
                'Dim strSQLTable As String = "Loaders_Log_NSG"
                Dim strAns As String = MsgBox("Are you sure you want to delete this from the Log?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
                'If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadDataCur()

                If strAns = vbYes Then
                    If Not LoadersLogMetallic.Delete(DatabasePath, CLng(itemId), _errOut) Then Throw New Exception(_errOut)
                    Call LoadDataCur()
                End If
            Catch ex As Exception
                Dim strProcedure As String = "LoadData"
                Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Private Sub ToolStripButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton3.Click
            Call Delete_LogData()
        End Sub

        Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click
            Call Delete_LogData()
        End Sub

        Private Sub EditToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditToolStripMenuItem.Click
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim frmNew As New frmEditDataSheet_RiflePistols
            frmNew.CFGID = ItemID
            frmNew.FromView = True
            frmNew.MdiParent = Me.MdiParent
            frmNew.Show()
        End Sub

        Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
            Dim frmNew As New FrmCopyDataSheetRiflePistol
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            frmNew.ConfigId = ItemID
            frmNew.FromView = True
            frmNew.MdiParent = Me.MdiParent
            frmNew.Show()
        End Sub
    End Class
End NameSpace