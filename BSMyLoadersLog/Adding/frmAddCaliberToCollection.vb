Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
''' <summary>
''' Class FrmAddCaliberToCollection.
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class FrmAddCaliberToCollection
    ''' <summary>
    ''' The error out
    ''' </summary>
    Private errOut as String
    ''' <summary>
    ''' Handles the Load event of the frmAddCaliberToCollection control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    ''' <exception cref="System.Exception"></exception>
    Private Sub frmAddCaliberToCollection_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            ' TODO: #20 CLEAN UP CODE
            'Dim objAf As New AutoFillCollections
            'txtCal.AutoCompleteSource = AutoCompleteSource.CustomSource
            'txtCal.AutoCompleteCustomSource = objAf.General_Calibers
            'Dim objAf As New AutoFillCollections
            txtCal.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtCal.AutoCompleteCustomSource = Calibers.ShowAll(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
        Catch ex As Exception
            Call LogError(Name, "frmAddCaliberToCollection_Load", Err.Number, ex.Message.ToString)
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
            ' TODO: #20 CLEAN UP CODE
            'Dim strCal As String = FluffContent(txtCal.Text)
            'If Not IsRequired(strCal, "Caliber", Text) Then Exit Sub
            'Dim obj As New BSDatabase
            'Dim sql As String = "INSERT INTO List_Calibers(CAL) VALUES('" & strCal & "')"
            'obj.ConnExec(sql)
            'MDIParentMain.RefreshCalData()
            'If Not chkKeep.Checked Then
            '    Close()
            'Else
            '    txtCal.Text = ""
            'End If
            Dim strCal As String = GeneralHelpers.FluffContent(txtCal.Text, "  ")
            If Not GeneralHelpers.IsRequired(strCal, "Caliber", Text) Then Exit Sub
            If Not CaliberInventory.Add(DatabasePath, strCal, errOut) Then Throw New Exception(errOut)
            
            MDIParentMain.RefreshCalData()
            If Not chkKeep.Checked Then
                Close()
            Else
                txtCal.Text = ""
            End If
        Catch ex As Exception
            Call LogError(Name, "btnAdd_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class