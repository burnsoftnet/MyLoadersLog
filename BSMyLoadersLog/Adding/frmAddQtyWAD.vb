'Imports BSMyLoadersLog.LoadersClass
'Imports System.Data.Odbc
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.Types

Namespace Adding

    Public Class FrmAddQtyWad
        ' TODO: #20 clean up code
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean
        ''' <summary>
        ''' The wad identifier
        ''' </summary>
        Public WadId As Long
        'Function PricePerItem(ByVal lQty As Long, ByVal dPrice As Double) As Double
        '    Dim dAns As Double = 0
        '    Dim ObjIM As New InventoryMath
        '    If lQty > 0 Then
        '        dAns = dPrice / lQty
        '    End If
        '    Converters.ConvertToDollars(dAns)
        '    Return dAns
        'End Function
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub LoadData()
            Try
                Dim lst As List(Of WadData) = WadInventory.GetDetails(DatabasePath, WadId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As WadData In lst
                    txtCQty.Text = o.Qty
                    txtCPrice.Text = Converters.ConvertToDollars(o.Price)
                    txtCPPI.Text = Converters.ConvertToDollars(o.EstimatedPricePerItem)
                Next

                'Dim SQL As String = "SELECT * from List_SG_WAD where ID=" & WadId
                'Dim Obj As New BSDatabase
                'Dim ObjIM As New InventoryMath
                'Call Obj.ConnectDB()
                'Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                'Dim RS As OdbcDataReader
                'RS = CMD.ExecuteReader
                'Dim iQty As Integer = 0
                'Dim eppo As Double = 0
                'Dim dPrice As Double = 0
                'While RS.Read
                '    If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                '    If Not IsDBNull(RS("Qty")) Then iQty = RS("Qty")
                '    If Not IsDBNull(RS("eppw")) Then eppo = RS("eppw")
                '    txtCQty.Text = iQty
                '    txtCPrice.Text = Converters.ConvertToDollars(dPrice)
                '    txtCPPI.Text = Converters.ConvertToDollars(eppo)
                'End While
                'RS.Close()
                'RS = Nothing
                'CMD = Nothing
                'Obj.CloseDB()
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Saves the data.
        ''' </summary>
        Sub SaveData()
            Try
                Dim currentQty As Long = CLng(GeneralHelpers.FluffContent(txtCQty.Text, 0))
                Dim currentPrice As Double = CDbl(GeneralHelpers.FluffContent(txtCPrice.Text, 0))
                Dim currentPricePerItem As Double = CDbl(GeneralHelpers.FluffContent(txtCPPI.Text, 0))
                Dim newQty As Long = CLng(GeneralHelpers.FluffContent(txtUQty.Text, 0))
                Dim newPrice As Double = CDbl(GeneralHelpers.FluffContent(txtUPrice.Text, 0))
                'Dim newPricePerItem As Double = PricePerItem(newQty, newPrice)
                'txtUPPI.Text = newPricePerItem
                If Not GeneralHelpers.IsRequired(newQty, "Update Qty", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(newPrice, "Update Price", Text) Then Exit Sub

                If Not WadInventory.UpdateQty(DatabasePath, WadId, currentQty, currentPrice, currentPricePerItem,
                                              newQty, newPrice, _errOut) Then Throw New Exception(_errOut)

                'Dim NQty As Long = currentQty + newQty
                'Dim NPrice As Double = (currentQty * currentPricePerItem) + newPrice
                'Dim NPPI As Double = PricePerItem(NQty, NPrice)
                'Dim SQL As String = ""
                'Dim Obj As New BSDatabase
                'If currentPricePerItem = newPricePerItem Then
                '    SQL = "UPDATE List_SG_WAD set QTY=" & NQty & ", Price=" & NPrice & " where ID=" & WadId
                'ElseIf newPrice = 0 And newQty = 0 Then
                '    SQL = "UPDATE List_SG_WAD set QTY=0, Price=0, eppw=0 where ID=" & WadId
                'Else
                '    SQL = "UPDATE List_SG_WAD set QTY=" & NQty & ", Price=" & NPrice & ", eppw=" & NPPI & " where ID=" & WadId
                'End If
                'Obj.ConnExec(SQL)
            Catch ex As Exception
                Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddQtyWAD control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddQtyWAD_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
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
            If FromView Then Call FrmViewListWads.LoadData()
            Close()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnViewCalc control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnViewCalc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnViewCalc.Click
            'txtUPPI.Text = PricePerItem(CLng(GeneralHelpers.FluffContent(txtUQty.Text, 0)),
            '                            CDbl(GeneralHelpers.FluffContent(txtUPrice.Text, 0)))
            Try
                If CLng(GeneralHelpers.FluffContent(txtUQty.Text, 0)) > 0 Then
                    txtUPPI.Text = Converters.ConvertToDollars(
                        CDbl(GeneralHelpers.FluffContent(txtUPrice.Text, 0)) /
                        CLng(GeneralHelpers.FluffContent(txtUQty.Text, 0)))
                End If
            Catch ex As Exception
                Call LogError(Name, "btnViewCalc_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace