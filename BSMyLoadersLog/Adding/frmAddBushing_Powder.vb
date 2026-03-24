Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory

Namespace Adding

    ''' <summary>
    ''' Class FrmAddBushingPowder.
    ''' Implements the <see cref="Form" />
    ''' </summary>
    ''' <seealso cref="Form" />
    Public Class FrmAddBushingPowder
        ''' <summary>
        ''' The error out
        ''' </summary>
        Private _errOut as String
        ''' <summary>
        ''' Preloads the data.
        ''' </summary>
        Sub PreloadData()
            Try
                txtManu.AutoCompleteCustomSource = ConfigShotgun.BushingPowderManufacturer(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtName.AutoCompleteCustomSource = ConfigShotgun.BushingPowderName(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtCharge.AutoCompleteCustomSource = ConfigShotgun.BushingPowderCharge(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtPowderName.AutoCompleteCustomSource = Powder.Name(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "PreloadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddChargeBushing control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddChargeBushing_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                Call PreloadData()
            Catch ex As Exception
                Call LogError(Name, "load", Err.Number, ex.Message.ToString)
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
        ''' Saves the data.
        ''' </summary>
        Sub SaveData()
            Try
                Dim manu As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim sName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim sCharge As String = GeneralHelpers.FluffContent(txtCharge.Text)
                Dim sType As String = cmbType.Text
                Dim powderName As String = GeneralHelpers.FluffContent(txtPowderName.Text)
            
                If Not GeneralHelpers.IsRequired(manu, "Manufacturer", 
                                                 Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(sName, "Name",
                                                 Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(sCharge, "Charge Amount",
                                                 Text) Then Exit Sub

                If Not ShotgunPowderInventory.Add(DatabasePath, manu, sName, sCharge, sType, 
                                                  powderName, _errOut) Then Throw new Exception(_errOut)

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
        ''' Handles the Enter event of the txtPowderName control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub txtPowderName_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles txtPowderName.Enter
            txtPowderName.SelectAll()
        End Sub
    End Class
End NameSpace