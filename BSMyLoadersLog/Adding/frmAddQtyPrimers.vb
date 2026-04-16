'Imports System.Data.Odbc
'Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.Types

Namespace Adding
    ' TODO: #20 clean up code
    ''' <summary>
    ''' Class frmAddQtyPrimers.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmAddQtyPrimers
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut as string
        ''' <summary>
        ''' The primer identifier
        ''' </summary>
        Public PrimerId As Long
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean

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
        Sub LoadData()
            Try
                Dim lst As List(Of PrimerListings) = PrimerInventory.GetDetails(DatabasePath, PrimerId, _errOut)
                If _errOut.Length > 0 then Throw New Exception(_errOut)
                For Each o As PrimerListings In lst
                    txtCQty.Text = o.Qty
                    txtCPPI.Text = o.PricePerPrimer
                    txtCPrice.Text = Converters.ConvertToDollars(o.Price)
                Next
                'Dim Obj As New BSDatabase
                'Call Obj.ConnectDB()
                'Dim SQL As String = "SELECT * from General_Primer where ID=" & PrimerId
                'Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                'Dim RS As OdbcDataReader
                'RS = CMD.ExecuteReader
                'Dim iQty As Integer = 0
                'Dim eppo As Double = 0
                'Dim dPrice As Double = 0
                'While RS.Read
                '    If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                '    If Not IsDBNull(RS("Qty")) Then iQty = RS("Qty")
                '    If Not IsDBNull(RS("eppp")) Then eppo = RS("eppp")
                '    txtCQty.Text = iQty
                '    txtCPPI.Text = eppo
                '    Dim ObjIM As New InventoryMath
                '    txtCPrice.Text = Converters.ConvertToDollars(dPrice)
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
                Dim currentQty As Long = GeneralHelpers.FluffContent(CLng(txtCQty.Text), 0)
                Dim currentPrice As Double = GeneralHelpers.FluffContent(CDbl(txtCPrice.Text), 0)
                Dim currentPricePerItem As Double = GeneralHelpers.FluffContent(CDbl(txtCPPI.Text), 0)
                Dim newQty As Long = GeneralHelpers.FluffContent(CLng(txtUQty.Text), 0)
                Dim newPrice As Double = GeneralHelpers.FluffContent(CDbl(txtUPrice.Text), 0)
                'Dim UPPI As Double = PricePerItem(newQty, newPrice)
                'txtUPPI.Text = UPPI
                If Not GeneralHelpers.IsRequired(newQty, "Update Qty", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(newPrice, "Update Price", Text) Then Exit Sub
                If Not PrimerInventory.UpdateQty(DatabasePath, PrimerID, currentQty, currentPrice, currentPricePerItem, 
                                                 newQty, newPrice, _errOut) Then throw new Exception(_errOut)
                'Dim NQty As Long = currentQty + newQty
                'Dim NPrice As Double = (currentQty * currentPricePerItem) + newPrice
                'Dim NPPI As Double = PricePerItem(NQty, NPrice)
                'Dim SQL As String = ""
                'Dim Obj As New BSDatabase
                'If currentPricePerItem = UPPI Then
                '    SQL = "UPDATE General_Primer set QTY=" & NQty & ", Price=" & NPrice & " where ID=" & PrimerId
                'ElseIf newPrice = 0 And newQty = 0 Then
                '    SQL = "UPDATE General_Primer set QTY=0, Price=0, eppp=0 where ID=" & PrimerId
                'Else
                '    SQL = "UPDATE General_Primer set QTY=" & NQty & ", Price=" & NPrice & ", eppp=" & NPPI & " where ID=" & PrimerId
                'End If
                'Obj.ConnExec(SQL)
            Catch ex As Exception
                Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnUpdate control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
            Try
                Call SaveData()
                If FromView Then Call FrmViewListPrimer.LoadData()
                Close()
            Catch ex As Exception
                Call LogError(Name, "btnUpdate.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddQtyPrimers control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddQtyPrimers_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnViewCalc control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnViewCalc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnViewCalc.Click
            try
                'txtUPPI.Text = PricePerItem(CLng(GeneralHelpers.FluffContent(CDbl(txtUQty.Text), 0)),
                '                            CDbl(GeneralHelpers.FluffContent(CDbl(txtUPrice.Text), 0)))
                txtUPPI.Text = PrimerInventory.CalculatePricePerItem(CLng(GeneralHelpers.FluffContent(CDbl(txtUQty.Text), 0)),
                                                                     CDbl(GeneralHelpers.FluffContent(CDbl(txtUPrice.Text), 0)))
            Catch ex As Exception
                Call LogError(Name, "btnViewCalc_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace