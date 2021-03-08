Imports BSMyLoadersLog.LoadersClass
''' <summary>
''' Class frmAddBushing_Shot.
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class FrmAddBushingShot
    ''' <summary>
    ''' Pres the load data.
    ''' </summary>
    Sub PreLoadData()
        Try
            Dim objAf As New AutoFillCollections.ShotGun
            txtCharge.AutoCompleteCustomSource = objAf.List_SG_Bushings_Shot_sCharge
            txtManu.AutoCompleteCustomSource = objAf.List_SG_Bushings_Shot_Manufacturer
            txtName.AutoCompleteCustomSource = objAf.List_SG_Bushings_Shot_Name
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
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancel.Click
        Close()
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

            If Not IsRequired(manu, "Manufacturer", Text) Then Exit Sub
            If Not IsRequired(sName, "Name", Text) Then Exit Sub
            If Not IsRequired(sCharge, "Charge Amount", Text) Then Exit Sub
            Dim sql As String = "INSERT INTO List_SG_Bushing_Shot(Manufacturer,sName,sCharge,sType) VALUES('" & _
                                manu & "','" & sName & "','" & sCharge & "','" & sType & "')"
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
    ''' Handles the Load event of the frmAddBushing_Shot control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub frmAddBushing_Shot_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Call PreLoadData()
    End Sub
End Class