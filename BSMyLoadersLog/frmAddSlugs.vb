Imports BSMyLoadersLog.LoadersClass
Public Class frmAddSlugs
    Public FromView As Boolean
    Sub AutoFill()
        Try
            Dim ObjAF As New AutoFillCollections.ShotGun
            txtManu.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Manu
            txtName.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Name
            txtCal.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_CAL
            txtPounds.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_weight
            txtPrice.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Price
        Catch ex As Exception
            Call LogError(Me.Name, "AutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddSlugs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call AutoFill()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim Manu As String = FluffContent(txtManu.Text)
            Dim Name As String = FluffContent(txtName.Text)
            Dim CAL As String = FluffContent(txtCal.Text)
            Dim Qty As Integer = nudQty.Value
            Dim Weight As String = FluffContent(txtPounds.Text)
            Dim Cost As Double = FluffContent(txtPrice.Text, 0.0)
            Dim epps As Double = 0
            Dim SQL As String = ""
            Dim Obj As New BSDatabase

            If Not IsRequired(Manu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(Name, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(CAL, "Caliber", Me.Text) Then Exit Sub
            Dim EstCostPerItem As Double = 0
            If Cost <> 0 Then
                EstCostPerItem = (Cost / Qty)
            End If
            epps = EstCostPerItem
            SQL = "INSERT INTO List_SG_ShotType_Details(Manufacturer,Name,IsSlug,CAL,QTY,weight,epps,Price) VALUES('" & _
                    Manu & "','" & Name & "',1,'" & CAL & "'," & Qty & ",'" & Weight & "'," & epps & "," & Cost & ")"
            Obj.ConnExec(SQL)
            If FromView Then Call frmView_List_Slug.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class