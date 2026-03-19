Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory

Namespace Adding

    ''' <summary>
    ''' Class FrmAddBushingPowder.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmAddBushingPowder
        ''' <summary>
        ''' The error out
        ''' </summary>
        Private errOut as String
        ''' <summary>
        ''' Preloads the data.
        ''' </summary>
        Sub PreloadData()
            Try
                ' TODO: #20 CLEAN UP CODE
                'Dim objaf As New AutoFillCollections.ShotGun
                'txtManu.AutoCompleteCustomSource = objaf.List_SG_Bushings_Powder_Manufacturer
                'txtName.AutoCompleteCustomSource = objaf.List_SG_Bushings_Powder_Name
                'txtCharge.AutoCompleteCustomSource = objaf.List_SG_Bushings_Powder_sCharge
                'txtPowderName.AutoCompleteCustomSource = objaf.List_SG_Bushings_Powder_Powder
                Dim objaf As New AutoFillCollections.ShotGun
                txtManu.AutoCompleteCustomSource = ConfigShotgun.BushingPowderManufacturer(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
                txtName.AutoCompleteCustomSource = ConfigShotgun.BushingPowderName(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
                txtCharge.AutoCompleteCustomSource = ConfigShotgun.BushingPowderCharge(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
                txtPowderName.AutoCompleteCustomSource = Powder.Name(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
            Catch ex As Exception
                Call LogError(Name, "PreloadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddChargeBushing control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddChargeBushing_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
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
        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancel.Click
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
                ' TODO: #20 CLEAN UP CODE
                'Dim manu As String = FluffContent(txtManu.Text)
                'Dim sName As String = FluffContent(txtName.Text)
                'Dim sCharge As String = FluffContent(txtCharge.Text)
                'Dim sType As String = cmbType.Text
                'Dim powderName As String = FluffContent(txtPowderName.Text)
            
                'If Not IsRequired(manu, "Manufacturer", Text) Then Exit Sub
                'If Not IsRequired(sName, "Name", Text) Then Exit Sub
                'If Not IsRequired(sCharge, "Charge Amount", Text) Then Exit Sub
                'Dim sql As String = "INSERT INTO List_SG_Bushing_Powder(Manufacturer,sName,sCharge,sType,PowderName) VALUES('" & _
                '                    manu & "','" & sName & "','" & sCharge & "','" & sType & "','" & powderName & "')"
                'Dim objDb As New BSDatabase
                'objDb.ConnExec(sql)
                'Call ClearFields()
                Dim manu As String = GeneralHelpers.FluffContent(txtManu.Text, "  ")
                Dim sName As String = GeneralHelpers.FluffContent(txtName.Text, "  ")
                Dim sCharge As String = GeneralHelpers.FluffContent(txtCharge.Text, "  ")
                Dim sType As String = cmbType.Text
                Dim powderName As String = GeneralHelpers.FluffContent(txtPowderName.Text, "  ")
            
                If Not GeneralHelpers.IsRequired(manu, "Manufacturer", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(sName, "Name", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(sCharge, "Charge Amount", Text) Then Exit Sub

                If Not ShotgunPowderInventory.Add(DatabasePath, manu, sName, sCharge, sType, 
                                                  powderName, errOut) Then Throw new Exception(errOut)

                'Dim sql As String = "INSERT INTO List_SG_Bushing_Powder(Manufacturer,sName,sCharge,sType,PowderName) VALUES('" & _
                '                    manu & "','" & sName & "','" & sCharge & "','" & sType & "','" & powderName & "')"
                'Dim objDb As New BSDatabase
                'objDb.ConnExec(sql)
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
        Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAdd.Click
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
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub txtName_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles txtName.Enter
            txtName.SelectAll()
        End Sub
        ''' <summary>
        ''' Handles the Enter event of the txtPowderName control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub txtPowderName_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles txtPowderName.Enter
            txtPowderName.SelectAll()
        End Sub
    End Class
End NameSpace