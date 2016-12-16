Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmViewDataSheet_Shotgun
    Public FID As Long
    Public FirearmName As String
    Sub LoadComboBox()
        Try
            Dim Obj As New BSDatabase
            Dim SQL As String = "SELECT * from Loaders_Log_Firearms where GType like '%shotgun%' order by FullName ASC"
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            Dim FullName As String = ""

            While RS.Read
                ToolStripComboBox1.Items.Add(RS("FullName"))
                'If Len(ToolStripComboBox1.Text) = 0 Then FullName = RS("FullName")
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
            'ToolStripComboBox1.Text = FullName
            ToolStripComboBox1.SelectedIndex = 0
        Catch ex As Exception
            Call LogError(Me.Name, "LoadComboBox", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub UpdateLabels()
        Try
            Dim Obj As New GlobalFunctions
            FID = Obj.GetFirearmID(FirearmName)
            Dim sCal As String = ""
            Dim sSerial As String = ""
            Dim sBar As String = ""
            Call Obj.GetFirearmDetails(FID, 0, "", "", "", sCal, sBar, sSerial)
            tslCal.Text = "Caliber: " & sCal
            tslBarrel.Text = "Barrel: " & sBar
            tslSerialNo.Text = "Serial No. " & sSerial
        Catch ex As Exception
            Call LogError(Me.Name, "UpdateLabels", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub WithConfigToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithConfigToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Dim frmNew As New frmReport_DataLoader_Shotgun
        frmNew.FID = FID
        frmNew.FirearmName = FirearmName
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub WithoutConfigToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithoutConfigToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Dim frmNew As New frmReport_DataLoader_ShotgunWOC
        frmNew.FID = FID
        frmNew.FirearmName = FirearmName
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
        Me.Cursor = Cursors.Arrow
    End Sub

    Sub LoadDatabyID()
        Try
            Me.Loaders_Log_SGTableAdapter.FillBy_FID(Me.MLLDataSet.Loaders_Log_SG, FID)
        Catch ex As Exception
            Call LogError(Me.Name, "LoadDatabyID", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub LoadDatabyName()
        Try
            'Me.Loaders_Log_SGTableAdapter.FillBy_FirearmName(Me.MLLDataSet.Loaders_Log_SG, FirearmName)
            Me.Loaders_Log_SGTableAdapter.FillBy_FirearmName(Me.MLLDataSet.Loaders_Log_SG, FirearmName)
        Catch ex As Exception
            Call LogError(Me.Name, "LoadDatabyName", Err.Number, ex.Message.ToString)
        End Try
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
    Private Sub frmViewDataSheet_Shotgun_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Private Sub ManuallyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManuallyToolStripMenuItem.Click
        Dim frmNew As New frmAddDataSheet_ShotGun_MAN
        frmNew.FID = FID
        frmNew.MdiParent = Me.MdiParent
        frmNew.FromView = True
        frmNew.Show()
    End Sub

    Private Sub UseConfigurationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseConfigurationToolStripMenuItem.Click
        Dim frmNew As New frmAddDataSheet_ShotGun_CFG
        frmNew.FID = FID
        frmNew.MdiParent = Me.MdiParent
        frmNew.FromView = True
        frmNew.Show()
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Call LoadDataCur()
    End Sub

    Private Sub frmViewDataSheet_Shotgun_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        If Me.Height <> 0 Then
            Me.DataGridView1.Height = Me.Height - (65)
            Me.DataGridView1.Width = Me.Width - 15
        End If
    End Sub
    Sub Delete_LogData()
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strSQLTable As String = "Loaders_Log_SG"
            Dim strAns As String = MsgBox("Are you sure you want to delete this from the Log?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
            If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadDataCur()
        Catch ex As Exception
            Dim strProcedure As String = "LoadData"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As System.EventArgs) Handles ToolStripButton3.Click
        Call Delete_LogData()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Call Delete_LogData()
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        Dim frmNew As New frmEditDataSheet_ShotGun
        frmNew.CFGID = ItemID
        frmNew.FromView = True
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Dim frmNew As New frmCopy_DataSheet_Copy_ShotGun
        Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
        frmNew.CFGID = ItemID
        frmNew.FromView = True
        frmNew.MdiParent = Me.MdiParent
        frmNew.Show()
    End Sub
End Class