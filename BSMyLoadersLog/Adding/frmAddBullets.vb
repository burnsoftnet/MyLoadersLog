Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Universal
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.Types

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
    Private errOut as String
    ''' <summary>
    ''' Loads the data.
    ''' </summary>
    Sub LoadData()
        Try
            ' TODO: #20 CLEAN UP CODE
'            Dim sql As String = "SELECT * from List_Bullets where ID=" & Bid
'            Dim obj As New BSDatabase
'            Dim objIm As New InventoryMath
'            Call obj.ConnectDB()
'            Dim cmd As New OdbcCommand(sql, obj.Conn)
'            Dim rs As OdbcDataReader
'            rs = cmd.ExecuteReader
'            Dim iQty As Integer = 0
'            Dim eppo As Double = 0
'' ReSharper disable RedundantAssignment
'            Dim dPrice As Double = 0
'' ReSharper restore RedundantAssignment
'            While rs.Read
'                If Not IsDBNull(rs("Manufacturer")) Then txtManu.Text = UnFluffContent(rs("Manufacturer"))
'                If Not IsDBNull(rs("Name")) Then txtName.Text = UnFluffContent(rs("Name"))
'                If Not IsDBNull(rs("Diameter")) Then txtDia.Text = UnFluffContent(rs("Diameter"))
'                If Not IsDBNull(rs("Weight")) Then txtWei.Text = UnFluffContent(rs("Weight"))
'                If Not IsDBNull(rs("Sec_Den")) Then txtSecDia.Text = UnFluffContent(rs("Sec_Den"))
'                If Not IsDBNull(rs("Part_number")) Then txtPartNo.Text = UnFluffContent(rs("Part_number"))
'                If Not IsDBNull(rs("Ballistic_Coefficient")) Then txtBC.Text = UnFluffContent(rs("Ballistic_Coefficient"))
'                If Not IsDBNull(rs("Bullet_Type")) Then
'                    cmbBT.SelectedValue = rs("Bullet_Type")
'                    cmbBT.Update()
'                End If
'                If Not IsDBNull(rs("CID")) Then
'                    cmbCalList.SelectedValue = rs("CID")
'                    cmbCalList.Update()
'                End If
'' ReSharper disable RedundantAssignment
'                If Not IsDBNull(rs("Price")) Then dPrice = rs("Price")
'' ReSharper restore RedundantAssignment
'                If Not IsDBNull(rs("Qty")) Then iQty = rs("Qty")
'                If Not IsDBNull(rs("ePPB")) Then eppo = rs("ePPB")
'                dPrice = eppo * iQty
'                nudQty.Value = iQty
'                txtPrice.Text = objIm.ConvertToDollars(dPrice)
'            End While
'            rs.Close()
'            ' ReSharper disable RedundantAssignment
'            rs = Nothing
'            cmd = Nothing
'' ReSharper restore RedundantAssignment
'            obj.CloseDB()
            Dim values As List(Of BulletListings) = BulletsInventory.GetDetails(DatabasePath, Bid, errOut)
            if errOut.Length > 0 Then Throw New Exception(errOut)
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
            txtManu.AutoCompleteCustomSource = Bullets.Manufacturer(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtName.AutoCompleteCustomSource = Bullets.Name(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtDia.AutoCompleteCustomSource = Bullets.Diameter(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtWei.AutoCompleteCustomSource = Bullets.Weight(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtSecDia.AutoCompleteCustomSource = Bullets.SectionalDensity(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtPartNo.AutoCompleteCustomSource = Bullets.PartNumber(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtBC.AutoCompleteCustomSource = Bullets.BallisticCoefficient(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtPrice.AutoCompleteCustomSource = Bullets.Price(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
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
            Dim o As BSOtherObjects = New BSOtherObjects()
            Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text, "  ")
            Dim strName As String = GeneralHelpers.FluffContent(txtName.Text, "  ")
            Dim strDia As String = GeneralHelpers.FluffContent(txtDia.Text, "  ")
            Dim strWei As String = GeneralHelpers.FluffContent(txtWei.Text, "  ")
            Dim strSecDia As String = GeneralHelpers.FluffContent(txtSecDia.Text, "  ")
            Dim strPartNo As String = GeneralHelpers.FluffContent(txtPartNo.Text, "  ")
            Dim strBc As String = GeneralHelpers.FluffContent(txtBC.Text, "  ")
            Dim intBt As Integer = cmbBT.SelectedValue
            Dim cal As Integer = cmbCalList.SelectedValue
            Dim strQty As Integer = nudQty.Value
            Dim dbPrice As Double = o.FC(txtPrice.Text, 0)

            If Not GeneralHelpers.IsRequired(strManu, "Manufacturers", Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strName, "Name", Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strDia, "Diameter", Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strWei, "Weight", Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strSecDia, "Sectional Density", Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strBc, "Ballistic Coefficient", Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(intBt, "Caliber", Text) Then Exit Sub
            ' TODO: #20 CLEAN UP CODE
'            Dim estCostPerItem As Double = 0
'' ReSharper disable CompareOfFloatsByEqualityOperator
'            If dbPrice <> 0 Then
'' ReSharper restore CompareOfFloatsByEqualityOperator
'                estCostPerItem = (dbPrice / strQty)
'            End If
'            Dim obj As New BSDatabase
'            Dim sql As String = "INSERT INTO List_Bullets(Manufacturer,Name,Diameter," & _
'                "Weight,Sec_Den,Part_number,Ballistic_Coefficient,Bullet_Type,Qty,Price,CID,eppb) VALUES" & _
'                "('" & strManu & "','" & strName & "','" & strDia & "','" & strWei & "','" & _
'                strSecDia & "','" & strPartNo & "','" & strBc & "'," & intBt & "," & strQty & _
'                "," & dbPrice & "," & cal & "," & estCostPerItem & ")"
'            obj.ConnExec(sql)
            If Not BulletsInventory.Add(DatabasePath, strManu, strName, strDia, strWei, 
                                    strSecDia, strPartNo, strBc, intBt, strQty, dbPrice, cal, errOut) Then
                Throw new Exception(errOut)
            End If
            If FromView Then Call frmView_List_Bullets.LoadData()
            Close()
        Catch ex As Exception
            Call LogError(Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class