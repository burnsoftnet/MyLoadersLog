Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmAddBullets
    Public FromView As Boolean
    Public DoCopy As Boolean
    Public BID As Long
    Sub LoadData()
        Try
            Dim SQL As String = "SELECT * from List_Bullets where ID=" & BID
            Dim Obj As New BSDatabase
            Dim ObjIM As New InventoryMath
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            Dim iQty As Integer = 0
            Dim eppo As Double = 0
            Dim dPrice As Double = 0
            While RS.Read
                If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = UnFluffContent(RS("Manufacturer"))
                If Not IsDBNull(RS("Name")) Then txtName.Text = UnFluffContent(RS("Name"))
                If Not IsDBNull(RS("Diameter")) Then txtDia.Text = UnFluffContent(RS("Diameter"))
                If Not IsDBNull(RS("Weight")) Then txtWei.Text = UnFluffContent(RS("Weight"))
                If Not IsDBNull(RS("Sec_Den")) Then txtSecDia.Text = UnFluffContent(RS("Sec_Den"))
                If Not IsDBNull(RS("Part_number")) Then txtPartNo.Text = UnFluffContent(RS("Part_number"))
                If Not IsDBNull(RS("Ballistic_Coefficient")) Then txtBC.Text = UnFluffContent(RS("Ballistic_Coefficient"))
                If Not IsDBNull(RS("Bullet_Type")) Then
                    cmbBT.SelectedValue = RS("Bullet_Type")
                    cmbBT.Update()
                End If
                If Not IsDBNull(RS("CID")) Then
                    cmbCalList.SelectedValue = RS("CID")
                    cmbCalList.Update()
                End If
                If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                If Not IsDBNull(RS("Qty")) Then iQty = RS("Qty")
                If Not IsDBNull(RS("ePPB")) Then eppo = RS("ePPB")
                dPrice = eppo * iQty
                nudQty.Value = iQty
                txtPrice.Text = ObjIM.ConvertToDollars(dPrice)
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddBullets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.General_Ammunition_TypeTableAdapter.Fill(Me.MLLDataSet.General_Ammunition_Type)
        Try
            Me.List_CalibersTableAdapter.Fill(Me.MLLDataSet.List_Calibers)
            Call AutoFill()
            If DoCopy Then Call LoadData()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Sub AutoFill()
        Try
            Dim ObjAf As New AutoFillCollections
            txtManu.AutoCompleteCustomSource = ObjAf.List_Bullets_Manufacturer
            txtName.AutoCompleteCustomSource = ObjAf.List_Bullets_Name
            txtDia.AutoCompleteCustomSource = ObjAf.List_Bullets_Diameter
            txtWei.AutoCompleteCustomSource = ObjAf.List_Bullets_Weight
            txtSecDia.AutoCompleteCustomSource = ObjAf.List_Bullets_Sec_Den
            txtPartNo.AutoCompleteCustomSource = ObjAf.List_Bullets_Part_number
            txtBC.AutoCompleteCustomSource = ObjAf.List_Bullets_Ballistic_Coefficient
            txtPrice.AutoCompleteCustomSource = ObjAf.List_Bullets_Price
        Catch ex As Exception
            Call LogError(Me.Name, "AutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim strManu As String = FluffContent(txtManu.Text)
            Dim strName As String = FluffContent(txtName.Text)
            Dim strDia As String = FluffContent(txtDia.Text)
            Dim strWei As String = FluffContent(txtWei.Text)
            Dim strSecDia As String = FluffContent(txtSecDia.Text)
            Dim strPartNo As String = FluffContent(txtPartNo.Text)
            Dim strBC As String = FluffContent(txtBC.Text)
            Dim intBT As Integer = cmbBT.SelectedValue
            Dim ICal As Integer = cmbCalList.SelectedValue
            Dim strQty As Integer = nudQty.Value
            Dim dbPrice As Double = FluffContent(txtPrice.Text, 0)

            If Not IsRequired(strManu, "Manufacturers", Me.Text) Then Exit Sub
            If Not IsRequired(strManu, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(strDia, "Diameter", Me.Text) Then Exit Sub
            If Not IsRequired(strWei, "Weight", Me.Text) Then Exit Sub
            If Not IsRequired(strSecDia, "Sectional Density", Me.Text) Then Exit Sub
            If Not IsRequired(strBC, "Ballistic Coefficient", Me.Text) Then Exit Sub
            If Not IsRequired(intBT, "Caliber", Me.Text) Then Exit Sub
            Dim EstCostPerItem As Double = 0
            If dbPrice <> 0 Then
                EstCostPerItem = (dbPrice / strQty)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "INSERT INTO List_Bullets(Manufacturer,Name,Diameter," & _
                "Weight,Sec_Den,Part_number,Ballistic_Coefficient,Bullet_Type,Qty,Price,CID,eppb) VALUES" & _
                "('" & strManu & "','" & strName & "','" & strDia & "','" & strWei & "','" & _
                strSecDia & "','" & strPartNo & "','" & strBC & "'," & intBT & "," & strQty & _
                "," & dbPrice & "," & ICal & "," & EstCostPerItem & ")"
            Obj.ConnExec(SQL)
            If FromView Then Call frmView_List_Bullets.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class