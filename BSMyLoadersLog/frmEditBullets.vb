Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmEditBullets
    Public BID As Long
    Public FromView As Boolean
    Sub LoadData()
        Try
            Me.General_Ammunition_TypeTableAdapter.Fill(Me.MLLDataSet.General_Ammunition_Type)
            Me.List_CalibersTableAdapter.Fill(Me.MLLDataSet.List_Calibers)
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
    Sub SaveData()
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
            If dbPrice <> 0 And strQty > 0 Then
                EstCostPerItem = (dbPrice / strQty)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "UPDATE List_Bullets set Manufacturer='" & strManu & "', Name='" & strName & "', " & _
                                "Diameter='" & strDia & "'" & ", Weight='" & strWei & "'" & ", Sec_Den='" & strSecDia & "', " & _
                                "Part_number='" & strPartNo & "', Ballistic_Coefficient='" & strBC & "', " & _
                                "Bullet_Type=" & intBT & ", Qty=" & strQty & ", Price=" & dbPrice & _
                                ", CID=" & ICal & ", ePPB=" & EstCostPerItem & " where" & _
                                " ID=" & BID
            Obj.ConnExec(SQL)
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmEditBullets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Call LoadData()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Call SaveData()
            If FromView Then Call frmView_List_Bullets.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class