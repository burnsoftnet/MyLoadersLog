Imports BSMyLoadersLog.LoadersClass
Public Class frmAddPowder
    Public FromView As Boolean
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Sub AutoFill()
        Try
            Dim ObjAF As New AutoFillCollections
            txtManu.AutoCompleteCustomSource = ObjAF.General_Powder_Manufacturer
            txtName.AutoCompleteCustomSource = ObjAF.General_Powder_Name
            txtPrice.AutoCompleteCustomSource = ObjAF.General_Powder_Price
            txtwei.AutoCompleteCustomSource = ObjAF.General_Powder_WeightInPounds
        Catch ex As Exception
            Call LogError(Me.Name, "AutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim strManu As String = FluffContent(txtManu.Text)
            Dim strName As String = FluffContent(txtName.Text)
            Dim dbWei As Double = FluffContent(txtwei.Text, 0)
            Dim dbWeiGrn As Double = ConvertWeight(dbWei, WeightType.Grains, WeightType.Pounds)
            Dim dbPrice As Double = FluffContent(txtPrice.Text, 0)
            Dim strNotes As String = FluffContent(txtNotes.Text)
            If Not IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(strName, "Name", Me.Text) Then Exit Sub
            Dim EstCostPerItem As Double = 0
            If dbPrice <> 0 Then
                EstCostPerItem = (dbPrice / dbWeiGrn)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "INSERT INTO General_Powder(Manufacturer,Name," & _
                "weightlbs,weightgn,Price,Notes,ePPP) VALUES ('" & strManu & "','" & _
                strName & "'," & dbWei & "," & dbWeiGrn & "," & dbPrice & ",'" & _
                strNotes & "'," & EstCostPerItem & ")"
            Obj.ConnExec(SQL)
            If FromView Then Call frmView_List_Powder.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub frmAddPowder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call AutoFill()
    End Sub
End Class