Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers

Public Class frmAddShells
    ''' <summary>
    ''' The error out
    ''' </summary>
    Dim errOut as String
    Public FromView As Boolean
    Sub AutoFill()
        Try
            'Dim ObjAf As New AutoFillCollections
            'txtManu.AutoCompleteCustomSource = ObjAf.List_Case_Manufacturer
            'txtName.AutoCompleteCustomSource = ObjAf.List_Case_Name
            'txtTTL.AutoCompleteCustomSource = ObjAf.List_Case_Trim_to_length
            'txtPrice.AutoCompleteCustomSource = ObjAf.List_Case_Price
            txtManu.AutoCompleteCustomSource = Cases.Manufacturer(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtName.AutoCompleteCustomSource = Cases.Name(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtTTL.AutoCompleteCustomSource = Cases.TrimToLength(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
            txtPrice.AutoCompleteCustomSource = Cases.Price(DatabasePath, errOut)
            If errOut.Length > 0 Then Throw New Exception(errOut)
        Catch ex As Exception
            'Dim ObjFS As New BSFileSystem
            'Dim sMessage As String = "frmAddShells.AutoFill" & "::" & Err.Number & "::" & ex.Message.ToString()
            'ObjFS.LogFile(MyLogFile, sMessage)
            Call LogError(Name, "AutoFill", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddShells_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.List_CalibersTableAdapter.Fill(Me.MLLDataSet.List_Calibers)
        Call AutoFill()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
            Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
            Dim strTrim As Double = GeneralHelpers.FluffContent(CDbl(txtTTL.Text), 0)
            Dim intNew As Integer = 0
            Dim intUsed As Integer = nudUsed.Value
            Dim intQty As Integer = nudQty.Value
            Dim dbPrice As Double = GeneralHelpers.FluffContent(CDbl(txtPrice.Text), 0)
            Dim LngCalID As Long = cmbCal.SelectedValue
            If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strName, "Name", Me.Text) Then Exit Sub
            If Not GeneralHelpers.IsRequired(strTrim, "Trim to Lenght", Me.Text) Then Exit Sub
            If chkNew.Checked Then intNew = 1
            Dim EstCostPerItem As Double = 0
            If dbPrice <> 0 Then
                EstCostPerItem = (dbPrice / intQty)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "INSERT INTO List_Case (Manufacturer,Name,ttl," & _
                "IsNew,Qty,Price,CID, ePPC,TimesUsed) VALUES('" & strManu & "','" & strName & "','" & strTrim & _
                "'," & intNew & "," & intQty & "," & dbPrice & "," & LngCalID & "," & EstCostPerItem & "," & intUsed & ")"
            Obj.ConnExec(SQL)
            If FromView Then Call FrmViewListShells.LoadData()
            Me.Close()
        Catch ex As Exception
            'Dim ObjFS As New BSFileSystem
            'Dim sMessage As String = "frmAddShells.btnAdd.Click" & "::" & Err.Number & "::" & ex.Message.ToString()
            'ObjFS.LogFile(MyLogFile, sMessage)
            Call LogError(Name, "btnAdd_Click", Err.Number, ex.Message.ToString)
        End Try
    End Sub
End Class