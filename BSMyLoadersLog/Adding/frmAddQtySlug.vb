'Imports BSMyLoadersLog.LoadersClass
'Imports System.Data.Odbc
Imports BurnSoft.Applications.MLL.Helpers
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.Types

''' <summary>
''' Class frmAddQtySlug.
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class frmAddQtySlug
    ' TODO: #20 clean up code
    ''' <summary>
    ''' The error out
    ''' </summary>
    Dim _errOut As String
    ''' <summary>
    ''' The slug identifier
    ''' </summary>
    Public SlugId As Long
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
    ''' <exception cref="System.Exception"></exception>
    Public Sub LoadData()
        Try
            Dim lst as List(Of ShotgunShotTypeData) = ShotgunShotTypeInventory.GetDetails(DatabasePath, SlugId, _errOut)
            if _errOut.Length > 0 then Throw new Exception(_errOut)
            For Each o As ShotgunShotTypeData In lst
                txtCQty.Text = o.Qty
                txtCPrice.Text = Converters.ConvertToDollars(o.Price)
                txtCPPI.Text = Converters.ConvertToDollars(o.EstimatedPricePerItem)
            Next
            'Dim SQL As String = "SELECT * from List_SG_ShotType_Details where ID=" & SlugId
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
            '    If Not IsDBNull(RS("epps")) Then eppo = RS("epps")
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
    Public Sub SaveData()
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
            ' TODO: #19 Remove comment below on net Library Update
            'If Not ShotgunShotTypeInventory.UpdateQtySlug(DatabasePath, SlugId, currentQty, currentPrice, 
            '                                          currentPricePerItem, newQty, newPrice, 
            '                                          _errOut) Then Throw new Exception(_errOut)

            'Dim NQty As Long = currentQty + newQty
            'Dim NPrice As Double = (currentQty * currentPricePerItem) + newPrice
            'Dim NPPI As Double = PricePerItem(NQty, NPrice)
            'Dim SQL As String = ""
            'Dim Obj As New BSDatabase
            'If currentPricePerItem = newPricePerItem Then
            '    SQL = "UPDATE List_SG_ShotType_Details set QTY=" & NQty & ", Price=" & NPrice & " where ID=" & SlugId
            'ElseIf newPrice = 0 And newQty = 0 Then
            '    SQL = "UPDATE List_SG_ShotType_Details set QTY=0, Price=0, epps=0 where ID=" & SlugId
            'Else
            '    SQL = "UPDATE List_SG_ShotType_Details set QTY=" & NQty & ", Price=" & NPrice & ", epps=" & NPPI & " where ID=" & SlugId
            'End If
            'Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Load event of the frmAddQtySlug control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub frmAddQtySlug_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Call LoadData()
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
        Call SaveData()
        If FromView Then Call FrmViewListSlug.LoadData()
        Close()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the btnViewCalc control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnViewCalc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnViewCalc.Click
        If CLng(txtUQty.Text) > 0 Then
            txtUPPI.Text = Converters.ConvertToDollars(CDbl(txtUPrice.Text) / CLng(txtUQty.Text))
        End If
    End Sub
End Class