Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.Helpers

Public Class frmAddEquipment
    Public FromView As Boolean
    Sub AutoFill()
        Try
            Dim ObjAF As New AutoFillCollections
            txtManu.AutoCompleteCustomSource = ObjAF.General_Equipment_Manufacturer
            txtName.AutoCompleteCustomSource = ObjAF.General_Equipment_Name
            txtUse.AutoCompleteCustomSource = ObjAF.General_Equipment_Use
            txtPrice.AutoCompleteCustomSource = ObjAF.General_Equipment_Cost
        Catch ex As Exception
            Call LogError(Me.Name, "AutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub frmAddEquipment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call AutoFill()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
            Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
            Dim strUse As String = GeneralHelpers.FluffContent(txtUse.Text)
            Dim strPrice As Double = GeneralHelpers.FluffContent(CDbl(txtPrice.Text), 0)
            Dim strNotes As String = GeneralHelpers.FluffContent(txtNotes.Text)

            If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strName, "Name", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strUse, "Use", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strPrice, 0, "Price", Me.Text) Then Exit Sub

            Dim Obj As New BSDatabase
            Dim SQL As String = "INSERT INTO General_Equipment(Manufacturer,Name,Use,Cost,Notes) VALUES('" & _
                        strManu & "','" & strName & "','" & strUse & "'," & strPrice & ",'" & strNotes & "')"
            Obj.ConnExec(SQL)
            Obj = Nothing
            Dim sAns As String = MsgBox("Equipment was added to the database, do you with to add another?", MsgBoxStyle.YesNo, Me.Text)
            If FromView Then Call frmView_List_Equipment.LoadData()
            If sAns = vbYes Then
                txtManu.Text = ""
                txtName.Text = ""
                txtUse.Text = ""
                txtPrice.Text = ""
                txtNotes.Text = ""
            Else
                Me.Close()
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class