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
                If Not GeneralHelpers.IsRequired(newQty, "Update Qty", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(newPrice, "Update Price", Text) Then Exit Sub

                If Not BulletsInventory.UpdateQty(DatabasePath, BulletId, currentQty, currentPrice, currentPricePerItem, 
                                                  newQty, newPrice, _errOut) Then Throw New Exception(_errOut)
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