Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmImportFirearms
    Dim iCount As Integer
    Private Sub frmImportFirearms_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Obj As New BSMGC
        iCount = Obj.CountFirearms
        If iCount = 1 Then
            Label1.Text = "You have " & iCount & " firearm in your collection, are you ready to import or update to the local database?"
        ElseIf iCount > 1 Then
            Label1.Text = "You have " & iCount & " firearms in your collection, are you ready to import or update to the local database?"
        ElseIf iCount = 0 Then
            Label1.Text = "You do not have any firearms listed to import!"
        End If
    End Sub
    Private Sub btnStartImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartImport.Click
        Try
            Dim i As Integer = 0
            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = iCount
            ProgressBar1.Value = 0
            Dim Obj As New BSMGC
            Dim ObjL As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Call Obj.ConnectDB()
            Dim SQL As String = "SELECT ID,FullName,ModelName,Brand,caliber,[Type],BarrelLength,Serialnumber from qryGunCollectionDetails where ItemSold=0"
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                ProgressBar1.Refresh()
                i = i + 1
                Me.Refresh()
                SQL = "INSERT INTO Loaders_Log_Firearms(MGCID,FullName,Manu,Model,Cal,Barrel,SerialNo,GType,exclude) VALUES(" & _
                        RS("ID") & ",'" & FluffContent(RS("FullName")) & "','" & FluffContent(RS("brand")) & "','" & FluffContent(RS("modelname")) & "','" & _
                        RS("caliber") & "','" & RS("barrellength") & "','" & RS("serialnumber") & "','" & RS("type") & "',0)"
                If Not ObjG.ObjectExistsinDB(CInt(RS("ID")), "MGCID", "Loaders_Log_Firearms") Then ObjL.ConnExec(SQL)
                ProgressBar1.Value = i
                ProgressBar1.Refresh()
                Me.Refresh()
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Call Obj.CloseDB()
            MsgBox("Import is Complete!")
            ProgressBar1.Visible = False
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnStartImport_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class