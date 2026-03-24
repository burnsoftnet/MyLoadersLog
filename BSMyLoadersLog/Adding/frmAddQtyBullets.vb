'Imports System.Data.Odbc
Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.Types

Namespace Adding
    ''' <summary>
    ''' Class FrmAddQtyBullets.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmAddQtyBullets
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' The bullet identifier
        ''' </summary>
        Public BulletId As Long
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
        '    ObjIM.ConvertToDollars(dAns)
        '    Return dAns
        'End Function        
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub LoadData()
            Try
                Dim lst as List(Of BulletListings) = BulletsInventory.GetDetails(DatabasePath, BulletId, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As BulletListings In lst
                    txtCQty.Text = o.Qty
                    txtCPrice.Text = Converters.ConvertToDollars(o.Price)
                    txtCPPI.Text = Converters.ConvertToDollars(o.EsitmatedPricePerBullet)
                Next

                ' TODO: #20 Removed Unused Code
                'Dim SQL As String = "SELECT * from List_Bullets where ID=" & BID
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
                '    If Not IsDBNull(RS("ePPB")) Then eppo = RS("ePPB")
                '    txtCQty.Text = iQty
                '    txtCPrice.Text = ObjIM.ConvertToDollars(dPrice)
                '    txtCPPI.Text = ObjIM.ConvertToDollars(eppo)
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
        ''' <exception cref="System.Exception"></exception>
        Sub SaveData()
            Try
                Dim currentQty As Long = CLng(GeneralHelpers.FluffContent(txtCQty.Text, 0))
                Dim currentPrice As Double = CDbl(GeneralHelpers.FluffContent(txtCPrice.Text, 0))
                Dim currentPricePerItem As Double = CDbl(GeneralHelpers.FluffContent(txtCPPI.Text, 0))
                Dim newQty As Long = CLng(GeneralHelpers.FluffContent(txtUQty.Text, 0))
                Dim newPrice As Double = CDbl(GeneralHelpers.FluffContent(txtUPrice.Text, 0))
                'Dim UPPI As Double = PricePerItem(UQty, UPrice)
                'txtUPPI.Text = UPPI
                If Not GeneralHelpers.IsRequired(newQty, "Update Qty", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(newPrice, "Update Price", Text) Then Exit Sub

                If Not BulletsInventory.UpdateQty(DatabasePath, BulletId, currentQty, currentPrice, currentPricePerItem, 
                                                  newQty, newPrice, _errOut) Then Throw New Exception(_errOut)

                'Dim NQty As Long = CQty + UQty
                'Dim NPrice As Double = (CQty * CPPI) + UPrice
                'Dim NPPI As Double = PricePerItem(NQty, NPrice)
                'Dim SQL As String = ""
                'Dim Obj As New BSDatabase
                'If CPPI = UPPI Then
                '    SQL = "UPDATE List_Bullets set QTY=" & NQty & ", Price=" & NPrice & " where ID=" & BID
                'ElseIf UPrice = 0 And UQty = 0 Then
                '    SQL = "UPDATE List_Bullets set QTY=0, Price=0, eppb=0 where ID=" & BID
                'Else
                '    SQL = "UPDATE List_Bullets set QTY=" & NQty & ", Price=" & NPrice & ", eppb=" & NPPI & " where ID=" & BID
                'End If
                'Obj.ConnExec(SQL)
            Catch ex As Exception
                Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddQtyBullets control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddQtyBullets_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
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
        ''' Handles the Click event of the btnViewCalc control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnViewCalc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnViewCalc.Click
            txtUPPI.Text = CDbl(txtUPrice.Text) / CLng(txtUQty.Text)
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnUpdate control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
            Try
                Call SaveData()
                If FromView Then Call frmView_List_Bullets.LoadData()
                Close()
            Catch ex As Exception
                Call LogError(Name, "btnUpdate.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace