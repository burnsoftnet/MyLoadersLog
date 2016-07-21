Imports BSMyLoadersLog.LoadersClass
Public Class frmAddPrimer
    Public FromView As Boolean
    Sub LoadData()
        Try
            Call AutoFill()
            Me.General_Primer_TypeTableAdapter.Fill(Me.MLLDataSet.General_Primer_Type)
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddPrimer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
    Sub AutoFill()
        Try
            Dim ObjAF As New AutoFillCollections
            txtManu.AutoCompleteCustomSource = ObjAF.General_Primer_Type_ManuFacturers
            txtName.AutoCompleteCustomSource = ObjAF.General_Primer_Type_Name
            txtPrice.AutoCompleteCustomSource = ObjAF.General_Primer_Type_Price
        Catch ex As Exception
            Call LogError(Me.Name, "AutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim strManu As String = FluffContent(txtManu.Text)
            Dim strName As String = FluffContent(txtName.Text)
            Dim intPriType As Integer = cmbPriType.SelectedValue
            Dim intQty As Integer = nudQty.Value
            Dim dbPrice As Double = FluffContent(txtPrice.Text, 0)

            If Not IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(strName, "Name", Me.Text) Then Exit Sub
            Dim EstCostPerItem As Double = 0
            If dbPrice <> 0 Then
                EstCostPerItem = (dbPrice / intQty)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "INSERT INTO General_Primer(Manufacturer,Name,Primer_Type," & _
                "Qty,Price, ePPP) VALUES('" & strManu & "','" & strName & "'," & intPriType & "," & _
                intQty & "," & dbPrice & "," & EstCostPerItem & ")"
            Obj.ConnExec(SQL)
            If FromView Then Call frmView_List_Primer.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class