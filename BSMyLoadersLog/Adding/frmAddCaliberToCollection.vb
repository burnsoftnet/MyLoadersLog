Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory

Namespace Adding
    ''' <summary>
    ''' Class FrmAddCaliberToCollection.
    ''' Implements the <see cref="Form" />
    ''' </summary>
    ''' <seealso cref="Form" />
    Public Class FrmAddCaliberToCollection
        ''' <summary>
        ''' The error out
        ''' </summary>
        Private _errOut as String
        ''' <summary>
        ''' Handles the Load event of the frmAddCaliberToCollection control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub frmAddCaliberToCollection_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                txtCal.AutoCompleteSource = AutoCompleteSource.CustomSource
                txtCal.AutoCompleteCustomSource = Calibers.ShowAll(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "frmAddCaliberToCollection_Load", 
                              Err.Number, ex.Message.ToString)
            End Try
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
        ''' Handles the Click event of the btnAdd control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Try
                Dim strCal As String = GeneralHelpers.FluffContent(txtCal.Text, "  ")
                If Not GeneralHelpers.IsRequired(strCal, "Caliber", 
                                                 Text) Then Exit Sub
                If Not CaliberInventory.Add(DatabasePath, strCal, _errOut) Then Throw New Exception(_errOut)
            
                MDIParentMain.RefreshCalData()
                If Not chkKeep.Checked Then
                    Close()
                Else
                    txtCal.Text = ""
                End If
            Catch ex As Exception
                Call LogError(Name, "btnAdd_Click", Err.Number, 
                              ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace