Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmView_Loaded_Ammunition
    Sub LoadData()
        Try
            Me.Loaders_Log_AmmunitionTableAdapter.Fill(Me.MLLDataSet.Loaders_Log_Ammunition)
            ToolStripButton1.Enabled = MDIParentMain.tsslMGCEnabled.Enabled
            Dim ObjGF As New GlobalFunctions
            ToolStripLabel2.Text = ObjGF.CountReadyToUseAmmo
        Catch ex As Exception
            Dim strProcedure As String = "LoadData"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmView_Loaded_Ammunition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
    Sub ExportToMGC()
        Try
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim ObjMGC As New BSMGC
            Dim iQty As Long = 0
            Dim AID As Long = 0
            Dim MID As Long = 0
            Dim cQty As Long = 0
            Dim strManu As String = ""
            Dim strName As String = ""
            Dim strCaliber As String = ""
            Dim strGrains As String = ""
            Dim strJacket As String = ""
            Dim sVelocity As String = ""
            Dim dcal As Double = 0
            Dim SQL As String = "SELECT * from Loaders_Log_Ammunition"
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            While RS.Read
                cQty = RS("Qty")
                MID = RS("ID")
                strManu = FluffContent(RS("Manufacturer"))
                strName = FluffContent(RS("Name"))
                strCaliber = FluffContent(RS("Cal"))
                strGrains = FluffContent(RS("Grain"))
                strJacket = FluffContent(RS("Jacket"))
                sVelocity = FluffContent(RS("Vel"))
                dcal = RS("dcal")
                If ObjMGC.AmmoIsAlreadyListed(strManu, strName, strCaliber, _
                        strGrains, strJacket, iQty, AID) Then
                    SQL = "UPDATE Gun_Collection_Ammo set Qty='" & (cQty + iQty) & "' where id=" & AID
                    ObjMGC.ConnExec(SQL)
                Else
                    SQL = "INSERT INTO Gun_Collection_Ammo(Manufacturer,Name,Cal,Grain,Jacket,Qty,dcal,vel_n) VALUES('" & _
                            strManu & "','" & strName & "','" & strCaliber & "','" & strGrains & "','" & _
                            strJacket & "'," & cQty & "," & dcal & "," & sVelocity & ")"
                    ObjMGC.ConnExec(SQL)
                End If
                Obj.ConnExec("DELETE from Loaders_Log_Ammunition where ID=" & MID)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
        Catch ex As Exception
            Dim strProcedure As String = "ExportToMGC"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmView_Loaded_Ammunition_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Height <> 0 Then
            Me.DataGridView1.Height = Me.Height - (65)
            Me.DataGridView1.Width = Me.Width - 15
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            Dim NL As String = Chr(10)
            Dim sMsg As String = "Exporting will Add your Ready to Use Ammunition to " & NL & "My Gun Collection and delete it from the local database." & NL & "Do you wish to continue?"
            Dim sAns As String = MsgBox(sMsg, MsgBoxStyle.YesNo, Me.Text)
            If sAns = vbYes Then
                Call ExportToMGC()
                MsgBox("Export of Ammunition to My Gun Collection is complete!")
                Call LoadData()
            End If
        Catch ex As Exception
            Dim strProcedure As String = "ToolStripButton1_Click"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Me.Close()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Call LoadData()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Dim ItemID As String = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
            Dim Obj As New BSDatabase
            Dim ObjG As New GlobalFunctions
            Dim strSQLTable As String = "Loaders_Log_Ammunition"
            Dim strName As String = ObjG.GetName("SELECT * from " & strSQLTable & " where ID=" & ItemID, "Name")
            Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
            Dim SQL As String = "DELETE from " & strSQLTable & " where ID=" & ItemID
            If strAns = vbYes Then Obj.ConnExec(SQL) : Call LoadData()
        Catch ex As Exception
            Dim strProcedure As String = "ToolStripButton2_Click"
            Call LogError(Me.Name, strProcedure, Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.BindingContextChanged
        If DataGridView1.DataSource Is Nothing Then
            Return
        End If
        DataGridView1.AutoResizeColumns()
    End Sub
End Class