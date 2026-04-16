'Imports System.Data.Odbc
'Imports System.Web.UI.WebControls.Expressions
'Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MGC.LoadersLog
Imports BurnSoft.Applications.MGC.Types
Imports BurnSoft.Applications.MLL.Global
'Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.LoadersLog
Imports BurnSoft.Applications.MLL.Types

Namespace Viewing
    ' TODO: #20 Clean up Code
    ''' <summary>
    ''' Class FrmViewLoadedAmmunition.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmViewLoadedAmmunition
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Sub LoadData()
            Try
                Loaders_Log_AmmunitionTableAdapter.Fill(MLLDataSet.Loaders_Log_Ammunition)
                ToolStripButton1.Enabled = MdiParentMain.tsslMGCEnabled.Enabled
                'Dim ObjGF As New GlobalFunctions
                'ToolStripLabel2.Text = ObjGF.CountReadyToUseAmmo
                ToolStripLabel2.Text = GeneralFunctions.CountReadyToUseAmmo(DatabasePath, _errOut)
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmView_Loaded_Ammunition control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_Loaded_Ammunition_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Exports to MGC.
        ''' </summary>
        Sub ExportToMgc()
            Try
                Dim loadedList As List(Of LoadersLogAmmunitionData) = LoadersLogAmmunition.GetAll(DatabasePath, _errOut)
                If _errOut.Length > 0 Then throw New Exception(_errOut)
                Dim newList as List(Of Ammunition) = New List(Of Ammunition)()
                For Each o As LoadersLogAmmunitionData In loadedList
                    newList = AmmoHelper.AddedToAmmoList(newList, o.Manufacturer, o.Name, o.Caliber, o.Grain, 
                                                         o.Jacket, o.Qty, o.Velocity, _errOut)
                    If _errOut.Length > 0 Then throw New Exception(_errOut)
                Next

                If Not AmmoHelper.ImportAmmoMade(newList, _errOut) Then Throw New Exception(_errOut)

                'Dim Obj As New BSDatabase
                'Call Obj.ConnectDB()
                'Dim ObjMGC As New BSMGC
                'Dim iQty As Long = 0
                'Dim AID As Long = 0
                'Dim MID As Long = 0
                'Dim cQty As Long = 0
                'Dim strManu As String = ""
                'Dim strName As String = ""
                'Dim strCaliber As String = ""
                'Dim strGrains As String = ""
                'Dim strJacket As String = ""
                'Dim sVelocity As String = ""
                'Dim dcal As Double = 0
                'Dim SQL As String = "SELECT * from Loaders_Log_Ammunition"
                'Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                'Dim RS As OdbcDataReader
                'RS = CMD.ExecuteReader
                'While RS.Read
                '    cQty = RS("Qty")
                '    MID = RS("ID")
                '    strManu = GeneralHelpers.FluffContent(RS("Manufacturer"))
                '    strName = GeneralHelpers.FluffContent(RS("Name"))
                '    strCaliber = GeneralHelpers.FluffContent(RS("Cal"))
                '    strGrains = GeneralHelpers.FluffContent(RS("Grain"))
                '    strJacket = GeneralHelpers.FluffContent(RS("Jacket"))
                '    sVelocity = GeneralHelpers.FluffContent(RS("Vel"))
                '    dcal = RS("dcal")
                '    If ObjMGC.AmmoIsAlreadyListed(strManu, strName, strCaliber, _
                '                                  strGrains, strJacket, iQty, AID) Then
                '        SQL = "UPDATE Gun_Collection_Ammo set Qty='" & (cQty + iQty) & "' where id=" & AID
                '        ObjMGC.ConnExec(SQL)
                '    Else
                '        SQL = "INSERT INTO Gun_Collection_Ammo(Manufacturer,Name,Cal,Grain,Jacket,Qty,dcal,vel_n) VALUES('" & _
                '              strManu & "','" & strName & "','" & strCaliber & "','" & strGrains & "','" & _
                '              strJacket & "'," & cQty & "," & dcal & "," & sVelocity & ")"
                '        ObjMGC.ConnExec(SQL)
                '    End If
                '    Obj.ConnExec("DELETE from Loaders_Log_Ammunition where ID=" & MID)
                'End While
                'RS.Close()
                'RS = Nothing
                'CMD = Nothing
            Catch ex As Exception
                Call LogError(Name, "ExportToMGC", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Resize event of the frmView_Loaded_Ammunition control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_Loaded_Ammunition_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
            Try
                If Height <> 0 Then
                    DataGridView1.Height = Height - (65)
                    DataGridView1.Width = Width - 15
                End If
            Catch ex As Exception
                Call LogError(Name, "frmView_Loaded_Ammunition_Resize", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton1.Click
            Try
                Dim NL As String = Chr(10)
                Dim sMsg As String = "Exporting will Add your Ready to Use Ammunition to " & NL & "My Gun Collection and delete it from the local database." & NL & "Do you wish to continue?"
                Dim sAns As String = MsgBox(sMsg, MsgBoxStyle.YesNo, Text)
                If sAns = vbYes Then
                    Call ExportToMgc()
                    MsgBox("Export of Ammunition to My Gun Collection is complete!")
                    Call LoadData()
                End If
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton1_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton4 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton4.Click
            Close()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton3 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton3.Click
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the ToolStripButton2 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub ToolStripButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripButton2.Click
            Try
                Dim itemId As Long = DataGridView1.SelectedRows.Item(0).Cells.Item(0).Value
                'Dim obj As New BSDatabase
                'Dim objG As New GlobalFunctions
                'Dim strSqlTable As String = "Loaders_Log_Ammunition"
                'Dim strName As String = objG.GetName("SELECT * from " & strSqlTable & " where ID=" & itemId, "Name")
                'Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")
                'Dim sql As String = "DELETE from " & strSqlTable & " where ID=" & itemId
                'If strAns = vbYes Then obj.ConnExec(sql) : Call LoadData()
                Dim strName As String = LoadersLogAmmunition.GetName(DatabasePath, itemId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                Dim strAns As String = MsgBox("Are you sure you want to delete " & strName & "?", MsgBoxStyle.YesNo, "Delete Item from the Database.")

                If strAns = vbYes Then
                    If Not LoadersLogAmmunition.Delete(DatabasePath, itemId, _errOut) Then Throw New Exception(_errOut)
                    Call LoadData()
                End If
            Catch ex As Exception
                Call LogError(Name, "ToolStripButton2_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the BindingContextChanged event of the DataGridView1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub DataGridView1_BindingContextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DataGridView1.BindingContextChanged
            Try
                If DataGridView1.DataSource Is Nothing Then
                    Return
                End If
                DataGridView1.AutoResizeColumns()
            Catch ex As Exception
                Call LogError(Name, "DataGridView1_BindingContextChanged", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End Namespace