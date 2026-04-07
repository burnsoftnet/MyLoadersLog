'Imports System.Data.Odbc
'Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.Types

Namespace Editing
    ''' <summary>
    ''' Class FrmEditPrimer.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmEditPrimer
        ' TODO: #20 clean up code
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' The primer identifier
        ''' </summary>
        Public PrimerId As Long
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Sub LoadData()
            Try
                Dim lst As List(Of PrimerListings) = PrimerInventory.GetDetails(DatabasePath, PrimerId, _errOut)
                If _errOut.Length > 0 then Throw New Exception(_errOut)
                For Each o As PrimerListings In lst
                    nudQty.Value = o.Qty
                    txtManu.Text = o.Manufacturer
                    txtName.Text = o.Name
                    cmbPriType.SelectedValue = o.PrimerType
                    cmbPriType.Update()
                    txtPrice.Text = Converters.ConvertToDollars(o.Qty * o.PricePerPrimer)
                Next
                'Dim Obj As New BSDatabase
                'Call Obj.ConnectDB()
                'Dim SQL As String = "SELECT * from General_Primer where ID=" & PrimerId
                'Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                'Dim RS As OdbcDataReader
                'RS = CMD.ExecuteReader
                'Dim iQty As Integer = 0
                'Dim eppo As Double = 0
                'Dim newPrice As Double = 0
                'While RS.Read
                '    If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = GeneralHelpers.UnFluffContent(RS("Manufacturer"))
                '    If Not IsDBNull(RS("Name")) Then txtName.Text = GeneralHelpers.UnFluffContent(RS("Name"))
                '    If Not IsDBNull(RS("Primer_Type")) Then
                '        cmbPriType.SelectedValue = RS("Primer_Type")
                '        cmbPriType.Update()
                '    End If
                '    If Not IsDBNull(RS("Price")) Then newPrice = RS("Price")
                '    If Not IsDBNull(RS("Qty")) Then iQty = RS("Qty")
                '    If Not IsDBNull(RS("eppp")) Then eppo = RS("eppp")
                '    newPrice = eppo * iQty
                '    nudQty.Value = iQty
                '    Dim ObjIM As New InventoryMath
                '    txtPrice.Text = Converters.ConvertToDollars(newPrice)
                'End While
                'RS.Close()
                'RS = Nothing
                'CMD = Nothing
                'Obj.CloseDB()
            Catch ex As Exception
                Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Saves the data.
        ''' </summary>
        Sub SaveData()
            Try
                Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim intPriType As Integer = cmbPriType.SelectedValue
                Dim intQty As Integer = nudQty.Value
                Dim dbPrice As Double = GeneralHelpers.FluffContent(txtPrice.Text, 0)

                If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strName, "Name", Text) Then Exit Sub
                If Not PrimerInventory.Update(DatabasePath, PrimerId, strManu, strName, intPriType, 
                                              dbPrice, intQty, _errOut) then Throw new Exception(_errOut)
                'Dim EstCostPerItem As Double = 0
                'If dbPrice <> 0 And intQty > 0 Then
                '    EstCostPerItem = (dbPrice / intQty)
                'End If
                'Dim Obj As New BSDatabase
                'Dim SQL As String = "UPDATE General_Primer set Manufacturer='" & strManu & "',Name='" & strName & "'" & _
                '                    ", Primer_Type=" & intPriType & ",Qty=" & intQty & ",Price=" & dbPrice & _
                '                    ",eppp=" & EstCostPerItem & " where ID=" & PrimerId
                'Obj.ConnExec(SQL)
                'Close()
            Catch ex As Exception
                Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmEditPrimer control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmEditPrimer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            General_Primer_TypeTableAdapter.Fill(MLLDataSet.General_Primer_Type)
            General_PrimerTableAdapter.Fill(MLLDataSet.General_Primer)
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnUpdate control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
            Call SaveData()
            Call FrmViewListPrimer.LoadData()
        End Sub
    End Class
End NameSpace