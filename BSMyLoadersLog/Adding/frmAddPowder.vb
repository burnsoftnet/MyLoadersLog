Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory

Namespace Adding
    ''' <summary>
    ''' Class FrmAddPowder.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmAddPowder
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean
        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub
        ''' <summary>
        ''' Automatics the fill.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub AutoFill()
            Try
                txtManu.AutoCompleteCustomSource = Powder.Manufacturer(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtName.AutoCompleteCustomSource = Powder.Name(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtPrice.AutoCompleteCustomSource = Powder.Price(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtwei.AutoCompleteCustomSource = Powder.Weightlbs(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "AutoFill", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnAdd control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Try
                Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim dbWei As Double = GeneralHelpers.FluffContent(txtwei.Text, 0)
                Dim dbPrice As Double = GeneralHelpers.FluffContent(txtPrice.Text, 0)
                Dim strNotes As String = GeneralHelpers.FluffContent(txtNotes.Text)
                If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strName, "Name", Text) Then Exit Sub
                
                If Not PowderInventory.Add(DatabasePath, strManu, strName, dbWei, 
                                           dbPrice, strNotes, _errOut) Then Throw New Exception(_errOut)

                If FromView Then Call FrmViewListPowder.LoadData()
                Close()
            Catch ex As Exception
                Call LogError(Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddPowder control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddPowder_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call AutoFill()
        End Sub
    End Class
End NameSpace