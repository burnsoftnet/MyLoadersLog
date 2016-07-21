Imports BSMyLoadersLog.LoadersClass
Public Class frmAddShot
    Public FromView As Boolean
    Sub AutoFill()
        Try
            Dim ObjAF As New AutoFillCollections
            txtManu.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Manu
            txtName.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Name
            txtMat.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_mat
            txtShotNo.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_ShotNo
            txtPounds.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_weight
            txtPrice.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Price
        Catch ex As Exception
            Call LogError(Me.Name, "AutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddShot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call AutoFill()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim Manu As String = FluffContent(txtManu.Text)
            Dim Name As String = FluffContent(txtName.Text)
            Dim Mat As String = FluffContent(txtMat.Text, "LEAD")
            Dim ShotNo As String = FluffContent(txtShotNo.Text, "0")
            Dim Weight As String = FluffContent(txtPounds.Text, "0")
            Dim Cost As Double = FluffContent(txtPrice.Text, 0.0)
            Dim SQL As String = ""
            Dim Obj As New BSDatabase
            Dim ounces As Double = WEIGHT_OZ_1LBS * CDbl(Weight)
            Dim grams As Double = ounces * WEIGHT_GRAMS_OZ
            Dim epps As Double = 0
            If Cost > 0 Then epps = Cost / grams

            If Not IsRequired(Manu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(Name, "Name", Me.Text) Then Exit Sub

            SQL = "INSERT INTO List_SG_ShotType_Details(Manufacturer,Name,mat,ShotNo,weight,Price,ounces,grams,epps,IsSlug) VALUES('" & _
                    Manu & "','" & Name & "','" & Mat & "','" & ShotNo & "','" & Weight & "'," & Cost & "," & ounces & _
                    "," & grams & "," & epps & ",0)"
            Obj.ConnExec(SQL)
            If FromView Then Call frmView_List_Shot.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class