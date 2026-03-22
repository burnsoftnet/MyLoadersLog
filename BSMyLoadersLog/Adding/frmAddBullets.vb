Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.Types
Imports BSMyLoadersLog.Viewing

Namespace Adding

    ''' <summary>
    ''' Class FrmAddBullets.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmAddBullets
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean
        ''' <summary>
        ''' The do copy
        ''' </summary>
        Public DoCopy As Boolean
        ''' <summary>
        ''' The bullet id
        ''' </summary>
        Public Bid As Long
        ''' <summary>
        ''' The error out
        ''' </summary>
        Private _errOut as String
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Sub LoadData()
            Try
                Dim values As List(Of BulletListings) = BulletsInventory.GetDetails(DatabasePath, 
                                                                                    Bid, _errOut)
                if _errOut.Length > 0 Then Throw New Exception(_errOut)
                For Each o As BulletListings In values
                    txtManu.Text = o.Manufacturer
                    txtName.Text = o.Name
                    txtDia.Text = o.Diameter
                    txtWei.Text = o.Weight
                    txtSecDia.Text = o.SectionDensity
                    txtPartNo.Text = o.PartNumber
                    txtBC.Text = o.BallisticCoeffcient
                    cmbBT.SelectedValue = o.BulletType
                    cmbBT.Update()
                    cmbCalList.SelectedValue = o.CaliberId
                    cmbCalList.Update()
                    nudQty.Value = o.Qty
                    Dim newValue = Converters.ConvertToDollars(o.EsitmatedPricePerBullet * o.Qty)
                    txtPrice.Text = newValue
                Next
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddBullets control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddBullets_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            General_Ammunition_TypeTableAdapter.Fill(MLLDataSet.General_Ammunition_Type)
            Try
                List_CalibersTableAdapter.Fill(MLLDataSet.List_Calibers)
                Call AutoFill()
                If DoCopy Then Call LoadData()
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
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
        ''' Automatics the fill.
        ''' </summary>
        Sub AutoFill()
            Try
                txtManu.AutoCompleteCustomSource = Bullets.Manufacturer(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtName.AutoCompleteCustomSource = Bullets.Name(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtDia.AutoCompleteCustomSource = Bullets.Diameter(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtWei.AutoCompleteCustomSource = Bullets.Weight(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtSecDia.AutoCompleteCustomSource = Bullets.SectionalDensity(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtPartNo.AutoCompleteCustomSource = Bullets.PartNumber(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtBC.AutoCompleteCustomSource = Bullets.BallisticCoefficient(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtPrice.AutoCompleteCustomSource = Bullets.Price(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "AutoFill", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnAdd control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAdd.Click
            Try
                Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim strDia As String = GeneralHelpers.FluffContent(txtDia.Text)
                Dim strWei As String = GeneralHelpers.FluffContent(txtWei.Text)
                Dim strSecDia As String = GeneralHelpers.FluffContent(txtSecDia.Text)
                Dim strPartNo As String = GeneralHelpers.FluffContent(txtPartNo.Text)
                Dim strBc As String = GeneralHelpers.FluffContent(txtBC.Text)
                Dim intBt As Integer = cmbBT.SelectedValue
                Dim cal As Integer = cmbCalList.SelectedValue
                Dim strQty As Integer = nudQty.Value
                Dim dbPrice As Double = GeneralHelpers.FluffContent(txtPrice.Text, 0)

                If Not GeneralHelpers.IsRequired(strManu, "Manufacturers", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strName, "Name", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strDia, "Diameter", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strWei, "Weight", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strSecDia, "Sectional Density", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strBc, "Ballistic Coefficient", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(intBt, "Caliber", Text) Then Exit Sub
                
                If Not BulletsInventory.Add(DatabasePath, strManu, strName, strDia, strWei, 
                                            strSecDia, strPartNo, strBc, intBt, strQty, dbPrice, cal, _errOut) Then
                    Throw new Exception(_errOut)
                End If
                If FromView Then Call frmView_List_Bullets.LoadData()
                Close()
            Catch ex As Exception
                Call LogError(Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace