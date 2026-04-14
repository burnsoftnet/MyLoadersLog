Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Types
Imports BurnSoft.Applications.MLL.Inventory

''' <summary>
''' Class frmAddQtyShellcase.
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class frmAddQtyShellcase
    ' TODO: #20 clean up code
    ''' <summary>
    ''' The error out
    ''' </summary>
    Dim _errOut As String
    ''' <summary>
    ''' The case identifier
    ''' </summary>
    Public CaseId As Long
    ''' <summary>
    ''' From view
    ''' </summary>
    Public FromView As Boolean
    Function PricePerItem(ByVal lQty As Long, ByVal dPrice As Double) As Double
        Dim dAns As Double = 0
        Dim ObjIM As New InventoryMath
        If lQty > 0 Then
            dAns = dPrice / lQty
        End If
        Converters.ConvertToDollars(dAns)
        Return dAns
    End Function
    ''' <summary>
    ''' Loads the data.
    ''' </summary>
    Sub LoadData()
        Try
            Dim lst As List(Of CaseListings) = CaseInventory.GetDetails(DatabasePath, CaseId, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            For Each o As CaseListings In lst
                txtCQty.Text = o.Qty
                txtCPrice.Text = Converters.ConvertToDollars(o.Price)
                txtCPPI.Text = Converters.ConvertToDollars(o.EstimatedPricePerCase)
            Next
            'Dim Obj As New BSDatabase
            'Call Obj.ConnectDB()
            'Dim SQL As String = "SELECT * from List_Case where ID=" & CaseId
            'Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            'Dim RS As OdbcDataReader
            'RS = CMD.ExecuteReader
            'Dim TimesUsed As Long = 0
            'Dim iQty As Integer = 0
            'Dim eppo As Double = 0
            'Dim dPrice As Double = 0
            'While RS.Read
            '    If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
            '    If Not IsDBNull(RS("Qty")) Then iQty = RS("Qty")
            '    If Not IsDBNull(RS("ePPC")) Then eppo = RS("ePPC")
            '    txtCQty.Text = iQty
            '    txtCPPI.Text = eppo
            '    Dim ObjIM As New InventoryMath
            '    txtCPrice.Text = Converters.ConvertToDollars(dPrice)
            'End While
            'RS.Close()
            'RS = Nothing
            'CMD = Nothing
            'Call Obj.CloseDB()
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
            'Dim UPPI As Double = PricePerItem(newQty, newPrice)
            'txtUPPI.Text = UPPI
            If Not GeneralHelpers.IsRequired(newQty, "Update Qty", Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(newPrice, "Update Price", Text) Then Exit Sub

            If Not CaseInventory.UpdateQty(DatabasePath, CaseId, currentQty, currentPrice, 
                                           currentPricePerItem, newQty, newPrice, 
                                           _errOut) Then Throw New Exception(_errOut)

            'Dim NQty As Long = currentQty + newQty
            'Dim NPrice As Double = (currentQty * currentPricePerItem) + newPrice
            'Dim NPPI As Double = PricePerItem(NQty, NPrice)
            'Dim SQL As String = ""
            'Dim Obj As New BSDatabase
            'If currentPricePerItem = UPPI Then
            '    SQL = "UPDATE List_Case set QTY=" & NQty & ", Price=" & NPrice & " where ID=" & CaseId
            'ElseIf newPrice = 0 And newQty = 0 Then
            '    SQL = "UPDATE List_Case set QTY=0, Price=0, eppc=0 where ID=" & CaseId
            'Else
            '    SQL = "UPDATE List_Case set QTY=" & NQty & ", Price=" & NPrice & ", eppc=" & NPPI & " where ID=" & CaseId
            'End If
            'Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnViewCalc control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnViewCalc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnViewCalc.Click
        txtUPPI.Text = PricePerItem(CLng(GeneralHelpers.FluffContent(txtUQty.Text, 0)),
                                    CDbl(GeneralHelpers.FluffContent(txtUPrice.Text, 0)))
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
        Try
            Call SaveData()
            If FromView Then Call FrmViewListShells.LoadData()
            Close()
        Catch ex As Exception
            Call LogError(Name, "btnUpdate.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Load event of the frmAddQtyShellcase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub frmAddQtyShellcase_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
End Class