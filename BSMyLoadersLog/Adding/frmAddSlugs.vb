Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers

Public Class frmAddSlugs
    ''' <summary>
    ''' The error out
    ''' </summary>
    Dim errOut As String
    Public FromView As Boolean
    Sub AutoFill()
        Try
            'Dim ObjAF As New AutoFillCollections.ShotGun
            'txtManu.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Manu
            'txtName.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Name
            'txtCal.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_CAL
            'txtPounds.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_weight
            'txtPrice.AutoCompleteCustomSource = ObjAF.List_SG_SHOTSLUG_Details_Price

            txtManu.AutoCompleteCustomSource = GeneralShotgun.TypeDetailsManufacturer(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtName.AutoCompleteCustomSource = GeneralShotgun.TypeDetailsName(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtCal.AutoCompleteCustomSource = GeneralShotgun.TypeDetailsCaliber(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtPounds.AutoCompleteCustomSource = GeneralShotgun.TypeDetailsWeight(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtPrice.AutoCompleteCustomSource = GeneralShotgun.TypeDetailsPrice(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
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
            Dim Manu As String = GeneralHelpers.FluffContent(txtManu.Text)
            Dim Name As String = GeneralHelpers.FluffContent(txtName.Text)
            Dim CAL As String = GeneralHelpers.FluffContent(txtCal.Text)
            Dim Qty As Integer = nudQty.Value
            Dim Weight As String = GeneralHelpers.FluffContent(txtPounds.Text)
            Dim Cost As Double = GeneralHelpers.FluffContent(CDbl(txtPrice.Text), 0.0)
            Dim epps As Double = 0
            Dim SQL As String = ""
            Dim Obj As New BSDatabase

            If Not GeneralHelpers.IsRequired(Manu, "Manufacturer", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(Name, "Name", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(CAL, "Caliber", Me.Text) Then Exit Sub
            Dim EstCostPerItem As Double = 0
            If Cost <> 0 Then
                EstCostPerItem = (Cost / Qty)
            End If
            epps = EstCostPerItem
            SQL = "INSERT INTO List_SG_ShotType_Details(Manufacturer,Name,IsSlug,CAL,QTY,weight,epps,Price) VALUES('" & _
                    Manu & "','" & Name & "',1,'" & CAL & "'," & Qty & ",'" & Weight & "'," & epps & "," & Cost & ")"
            Obj.ConnExec(SQL)
            If FromView Then Call FrmViewListSlug.LoadData()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class