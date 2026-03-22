Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
'Imports BurnSoft.Applications.MLL.Global
'Imports BSMyLoadersLog.LoadersClass

Namespace Adding

    Public Class FrmAddPowder
        Dim errOut As String
        Public FromView As Boolean
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub
        Sub AutoFill()
            Try
                'TODO: #20 Clean Up Code
                'Dim ObjAF As New AutoFillCollections
                'txtManu.AutoCompleteCustomSource = ObjAF.General_Powder_Manufacturer
                'txtName.AutoCompleteCustomSource = ObjAF.General_Powder_Name
                'txtPrice.AutoCompleteCustomSource = ObjAF.General_Powder_Price
                'txtwei.AutoCompleteCustomSource = ObjAF.General_Powder_WeightInPounds
                'Dim ObjAF As New AutoFillCollections
                txtManu.AutoCompleteCustomSource = Powder.Manufacturer(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
                txtName.AutoCompleteCustomSource = Powder.Name(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
                txtPrice.AutoCompleteCustomSource = Powder.Price(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
                txtwei.AutoCompleteCustomSource = Powder.Weightlbs(DatabasePath, errOut)
                If errOut.Length > 0 Then Throw New Exception(errOut)
            Catch ex As Exception
                Call LogError(Me.Name, "AutoFill", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Try
                Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim dbWei As Double = GeneralHelpers.FluffContent(txtwei.Text, 0)
                'Dim dbWeiGrn As Double =Converters.ConvertWeight(dbWei, WeightValues.WeightType.Grains, 
                '                                                 WeightValues.WeightType.Pounds, errOut)
                Dim dbPrice As Double = GeneralHelpers.FluffContent(txtPrice.Text, 0)
                Dim strNotes As String = GeneralHelpers.FluffContent(txtNotes.Text)
                If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strName, "Name", Text) Then Exit Sub
                
                If Not PowderInventory.Add(DatabasePath, strManu, strName, dbWei, 
                                           dbPrice, strNotes, errOut) Then Throw New Exception(errOut)
                'TODO: #20 Clean Up Code
                'Dim EstCostPerItem As Double = 0
                'If dbPrice <> 0 Then
                '    EstCostPerItem = (dbPrice / dbWeiGrn)
                'End If
                'Dim Obj As New BSDatabase
                'Dim SQL As String = "INSERT INTO General_Powder(Manufacturer,Name," & _
                '                    "weightlbs,weightgn,Price,Notes,ePPP) VALUES ('" & strManu & "','" & _
                '                    strName & "'," & dbWei & "," & dbWeiGrn & "," & dbPrice & ",'" & _
                '                    strNotes & "'," & EstCostPerItem & ")"
                'Obj.ConnExec(SQL)
                If FromView Then Call frmView_List_Powder.LoadData()
                Close()
            Catch ex As Exception
                Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub

        Private Sub frmAddPowder_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call AutoFill()
        End Sub
    End Class
End NameSpace