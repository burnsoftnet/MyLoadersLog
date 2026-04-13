'Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.Types

Namespace Adding

    ''' <summary>
    ''' Class frmAddQtyShot.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmAddQtyShot
        ' TODO: #20 clean up code
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' The shot identifier
        ''' </summary>
        Public ShotId As Long
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean
        ''' <summary>
        ''' The current price per item
        ''' </summary>
        Dim currentPricePerItem as Double
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
                Dim lst as List(Of ShotgunShotTypeData) = ShotgunShotTypeInventory.GetDetails(DatabasePath, ShotId, _errOut)
                if _errOut.Length > 0 then Throw new Exception(_errOut)
                For Each o As ShotgunShotTypeData In lst
                    txtCQty.Text = o.Qty
                    txtCPrice.Text = Converters.ConvertToDollars(o.Price)
                    currentPricePerItem = o.EstimatedPricePerItem
                Next
                'Dim Obj As New BSDatabase
                'Call Obj.ConnectDB()
                'Dim SQL As String = "SELECT * from List_SG_ShotType_Details where ID=" & ShotId
                'Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                'Dim RS As OdbcDataReader
                'RS = CMD.ExecuteReader
                'Dim TimesUsed As Long = 0
                'Dim iQty As Integer = 0
                'Dim eppo As Double = 0
                'Dim dPrice As Double = 0
                'While RS.Read
                '    If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                '    If Not IsDBNull(RS("weight")) Then iQty = RS("weight")
                '    txtCQty.Text = iQty
                '    If iQty = 0 Then dPrice = 0
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
        ''' Handles the Load event of the frmAddQtyShot control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddQtyShot_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
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
        ''' Saves the data.
        ''' </summary>
        Sub SaveData()
            Try
                '93.8 pellets per oz
                'BPI Nickel Plated Lead Shot #2 11 lb Box
                Dim currentQty As Long = CLng(GeneralHelpers.FluffContent(txtCQty.Text, 0))
                Dim currentPrice As Double = CDbl(GeneralHelpers.FluffContent(txtCPrice.Text, 0))
                Dim newQty As Long = CDbl(GeneralHelpers.FluffContent(txtUQty.Text, 0))
                Dim newPrice As Double = CDbl(GeneralHelpers.FluffContent(txtUPrice.Text, 0))
                'Dim currentPricePerItem as Double = 0
                'If Not ShotgunShotTypeInventory.UpdateQty(DatabasePath, ShotId, currentQty, currentPrice, currentPricePerItem, newQty, newPrice, errOut) Then throw new exception(errOut)
                Dim sql As String = ""
                Dim obj As New BSDatabase
                Dim nQty As Long = currentQty + newQty
                Dim nPrice As Double = currentPrice + newPrice
                Dim ounces As Double = WeightValues.WEIGHT_OZ_1LBS * nQty
                sql = "Update List_SG_ShotType_Details set Price=" & nPrice & ", weight='" & nQty & _
                      "',ounces=" & ounces & " where ID=" & ShotId
                obj.ConnExec(sql)
            Catch ex As Exception
                Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnUpdate control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
            Call SaveData()
            If FromView Then Call FrmViewListShot.LoadData()
            Close()
        End Sub
    End Class
End NameSpace