Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.Global
Imports BurnSoft.Applications.MLL.Helpers

Namespace Adding

    Public Class FrmAddPowder
        Dim errOut As String
        Public FromView As Boolean
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
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
        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Try
                Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim dbWei As Double = GeneralHelpers.FluffContent(CDbl(txtwei.Text), 0)
                Dim dbWeiGrn As Double =Converters.ConvertWeight(dbWei, WeightValues.WeightType.Grains, 
                                                                 WeightValues.WeightType.Pounds, errOut)
                Dim dbPrice As Double = GeneralHelpers.FluffContent(CDbl(txtPrice.Text), 0)
                Dim strNotes As String = GeneralHelpers.FluffContent(txtNotes.Text)
                If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strName, "Name", Text) Then Exit Sub
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

        Private Sub frmAddPowder_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call AutoFill()
        End Sub
    End Class
End NameSpace