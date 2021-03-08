Imports BSMyLoadersLog.LoadersClass
''' <summary>
''' Class FrmAddBushingPowder.
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class FrmAddBushingPowder
    ''' <summary>
    ''' Preloads the data.
    ''' </summary>
    Sub PreloadData()
        Try
            Dim objaf As New AutoFillCollections.ShotGun
            txtManu.AutoCompleteCustomSource = objaf.List_SG_Bushings_Powder_Manufacturer
            txtName.AutoCompleteCustomSource = objaf.List_SG_Bushings_Powder_Name
            txtCharge.AutoCompleteCustomSource = objaf.List_SG_Bushings_Powder_sCharge
            txtPowderName.AutoCompleteCustomSource = objaf.List_SG_Bushings_Powder_Powder
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
            Dim manu As String = FluffContent(txtManu.Text)
            Dim sName As String = FluffContent(txtName.Text)
            Dim sCharge As String = FluffContent(txtCharge.Text)
            Dim sType As String = cmbType.Text
            Dim powderName As String = FluffContent(txtPowderName.Text)
            
            If Not IsRequired(manu, "Manufacturer", Text) Then Exit Sub
            If Not IsRequired(sName, "Name", Text) Then Exit Sub
            If Not IsRequired(sCharge, "Charge Amount", Text) Then Exit Sub
            Dim sql As String = "INSERT INTO List_SG_Bushing_Powder(Manufacturer,sName,sCharge,sType,PowderName) VALUES('" & _
                                manu & "','" & sName & "','" & sCharge & "','" & sType & "','" & powderName & "')"
            Dim objDb As New BSDatabase
            objDb.ConnExec(sql)
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