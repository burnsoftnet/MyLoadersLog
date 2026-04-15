'Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory

Public Class frmAddSlugs
    ' TODO #20 Code Clean Up
    ''' <summary>
    ''' The error out
    ''' </summary>
    Dim _errOut As String
    ''' <summary>
    ''' From view
    ''' </summary>
    Public FromView As Boolean
    ''' <summary>
    ''' Automatics the fill.
    ''' </summary>
    ''' <exception cref="System.Exception"></exception>
    Sub AutoFill()
        Try
            'Dim ObjAF As New AutoFillCollections.ShotGun
            'txtManu.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Manu
            'txtName.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Name
            'txtCal.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_CAL
            'txtPounds.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_weight
            'txtPrice.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Price

            txtManu.AutoCompleteCustomSource = GeneralShotgun.TypeDetailsManufacturer(DatabasePath, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            txtName.AutoCompleteCustomSource = GeneralShotgun.TypeDetailsName(DatabasePath, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            txtCal.AutoCompleteCustomSource = GeneralShotgun.TypeDetailsCaliber(DatabasePath, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            txtPounds.AutoCompleteCustomSource = GeneralShotgun.TypeDetailsWeight(DatabasePath, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
            txtPrice.AutoCompleteCustomSource = GeneralShotgun.TypeDetailsPrice(DatabasePath, _errOut)
            If _errOut.Length > 0 Then Throw New Exception(_errOut)
        Catch ex As Exception
            Call LogError(Name, "AutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Load event of the frmAddSlugs control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub frmAddSlugs_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Call AutoFill()
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
            Dim manufacturer As String = GeneralHelpers.FluffContent(txtManu.Text)
            Dim itemName As String = GeneralHelpers.FluffContent(txtName.Text)
            Dim caliber As String = GeneralHelpers.FluffContent(txtCal.Text)
            Dim qty As Integer = nudQty.Value
            Dim weight As String = GeneralHelpers.FluffContent(txtPounds.Text)
            Dim cost As Double = GeneralHelpers.FluffContent(CDbl(txtPrice.Text), 0.0)
            'Dim epps As Double = 0
            'Dim SQL As String = ""
            'Dim Obj As New BSDatabase

            If Not GeneralHelpers.IsRequired(manufacturer, "Manufacturer", Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(itemName, "Name", Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(caliber, "Caliber", Text) Then Exit Sub

            If Not ShotgunShotTypeInventory.Add(DatabasePath, manufacturer, itemName, "", weight, True, "", caliber, qty, cost, _errOut) Then Throw New Exception(_errOut)
            'Dim EstCostPerItem As Double = 0
            'If cost <> 0 Then
            '    EstCostPerItem = (cost / qty)
            'End If
            'epps = EstCostPerItem
            'SQL = "INSERT INTO List_SG_ShotType_Details(Manufacturer,Name,IsSlug,CAL,QTY,weight,epps,Price) VALUES('" & _
            '        manufacturer & "','" & name & "',1,'" & caliber & "'," & qty & ",'" & weight & "'," & epps & "," & cost & ")"
            'Obj.ConnExec(SQL)
            If FromView Then Call FrmViewListSlug.LoadData()
            Close()
        Catch ex As Exception
            Call LogError(Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class