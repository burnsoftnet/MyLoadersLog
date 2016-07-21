Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmExportFirearms
    Dim iCount As Integer
    Private Sub frmExportFirearms_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Obj As New GlobalFunctions
        iCount = Obj.CountFirearms
        If iCount = 1 Then
            Label1.Text = "You have " & iCount & " firearm in your collection, are you ready to export this firearm to the My Gun Collection application?"
        ElseIf iCount > 1 Then
            Label1.Text = "You have " & iCount & " firearms in your collection, are you ready to export these firearms to the My Gun Collection application?"
        ElseIf iCount = 0 Then
            Label1.Text = "You do not have any firearms listed to export that is not already listed in the My Gun Collection Application!"
            btnExport.Enabled = False
        End If
    End Sub
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Dim i As Integer = 0
            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = iCount
            ProgressBar1.Value = 0
            Dim Obj As New BSMGC
            Dim ObjL As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Call ObjL.ConnectDB()
            Dim SQL As String = "SELECT * from Loaders_Log_Firearms where MGCID=0"
            Dim CMD As New OdbcCommand(SQL, ObjL.Conn)
            Dim RS As OdbcDataReader
            Dim MGCID As Long = 0
            RS = CMD.ExecuteReader
            While RS.Read
                ProgressBar1.Refresh()
                Me.Refresh()
                Call Obj.AddFirearmToMGC(RS("fullname"), RS("manu"), RS("model"), RS("cal"), RS("Barrel"), RS("SerialNo"), RS("GType"), MGCID)
                If MGCID <> 0 Then
                    SQL = "UPDATE Loaders_Log_Firearms set MGCID=" & MGCID & " where id=" & RS("ID")
                    Call ObjL.ConnExec(SQL)
                End If
                i = i + 1
                MGCID = 0
                ProgressBar1.Value = i
                ProgressBar1.Refresh()
                Me.Refresh()
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Call Obj.CloseDB()
            MsgBox("Export is Complete!")
            ProgressBar1.Visible = False
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnStartImport_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class