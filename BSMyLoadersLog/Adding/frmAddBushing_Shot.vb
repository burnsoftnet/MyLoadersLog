Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory

Namespace Adding

    ''' <summary>
    ''' Class frmAddBushing_Shot.
    ''' Implements the <see cref="Form" />
    ''' </summary>
    ''' <seealso cref="Form" />
    Public Class FrmAddBushingShot
        ''' <summary>
        ''' The error out
        ''' </summary>
        Private _errOut as String
        ''' <summary>
        ''' Pres the load data.
        ''' </summary>
        Sub PreLoadData()
            Try
                txtCharge.AutoCompleteCustomSource = ConfigShotgun.BushingShotCharge(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtManu.AutoCompleteCustomSource = ConfigShotgun.BushingShotManufacturer(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtName.AutoCompleteCustomSource = ConfigShotgun.BushingShotName(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "PreLoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Clears the fields.
        ''' </summary>
        Sub ClearFields()
            If chkKeepOpen.Checked Then
                Call PreloadData()
            Else
                Close()
            End If
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
        ''' Saves the data.
        ''' </summary>
        Sub SaveData()
            Try
                Dim manu As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim sName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim sCharge As String = GeneralHelpers.FluffContent(txtCharge.Text)
                Dim sType As String = cmbType.Text

                If Not GeneralHelpers.IsRequired(manu, "Manufacturer", 
                                                 Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(sName, "Name", 
                                                 Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(sCharge, "Charge Amount", 
                                                 Text) Then Exit Sub
                If Not ShotgunShotInventory.Add(DatabasePath, manu, sName, sCharge, 
                                                sType, _errOut) Then Throw New Exception(_errOut)
                Call ClearFields()
            Catch ex As Exception
                Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnAdd control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Call SaveData()
        End Sub
        ''' <summary>
        ''' Handles the Enter event of the txtCharge control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub txtCharge_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles txtCharge.Enter
            txtCharge.SelectAll()
        End Sub
        ''' <summary>
        ''' Handles the Enter event of the txtManu control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub txtManu_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles txtManu.Enter
            txtManu.SelectAll()
        End Sub
        ''' <summary>
        ''' Handles the Enter event of the txtName control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub txtName_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles txtName.Enter
            txtName.SelectAll()
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddBushing_Shot control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddBushing_Shot_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call PreLoadData()
        End Sub
    End Class
End NameSpace