Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmViewDataSheet_Shotgun
    Public FID As Long
    Public FirearmName As String
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

    Private Sub frmViewDataSheet_Shotgun_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO:  Finish frmViewDataSheet_Shotgun_Load
    End Sub

    Private Sub ManuallyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManuallyToolStripMenuItem.Click
        'TODO: Add Data to data sheet for Manual
    End Sub

    Private Sub UseConfigurationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseConfigurationToolStripMenuItem.Click
        'TODO: Add Data to data sheet for Config Fillout
    End Sub
End Class