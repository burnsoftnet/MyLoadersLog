'Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory

Namespace Adding

    ''' <summary>
    ''' Class frmAddShells.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmAddShells
        ' TODO: #20 clean up code
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut as String
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean
        Sub AutoFill()
            Try
                'Dim ObjAf As New AutoFillCollections
                'txtManu.AutoCompleteCustomSource = ObjAf.List_Case_Manufacturer
                'txtName.AutoCompleteCustomSource = ObjAf.List_Case_Name
                'txtTTL.AutoCompleteCustomSource = ObjAf.List_Case_Trim_to_length
                'txtPrice.AutoCompleteCustomSource = ObjAf.List_Case_Price
                txtManu.AutoCompleteCustomSource = Cases.Manufacturer(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtName.AutoCompleteCustomSource = Cases.Name(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtTTL.AutoCompleteCustomSource = Cases.TrimToLength(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtPrice.AutoCompleteCustomSource = Cases.Price(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                'Dim ObjFS As New BSFileSystem
                'Dim sMessage As String = "frmAddShells.AutoFill" & "::" & Err.Number & "::" & ex.Message.ToString()
                'ObjFS.LogFile(MyLogFile, sMessage)
                Call LogError(Name, "AutoFill", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddShells control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddShells_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            List_CalibersTableAdapter.Fill(MLLDataSet.List_Calibers)
            Call AutoFill()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnAdd control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Try
                Dim manufacturer As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim itemName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim trimToLength As Double = GeneralHelpers.FluffContent(CDbl(txtTTL.Text), 0)
                'Dim intNew As Integer = 0
                Dim used As Integer = nudUsed.Value
                Dim qty As Integer = nudQty.Value
                Dim price As Double = GeneralHelpers.FluffContent(CDbl(txtPrice.Text), 0)
                Dim caliberId As Long = cmbCal.SelectedValue
                If Not GeneralHelpers.IsRequired(manufacturer, "Manufacturer", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(itemName, "Name", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(trimToLength, "Trim to Lenght", Text) Then Exit Sub

                If Not CaseInventory.Add(DatabasePath, manufacturer, itemName, trimToLength, chkNew.Checked, used, 
                                         qty, price, caliberId, _errOut) Then Throw new Exception(_errOut)

                'If chkNew.Checked Then intNew = 1
                'Dim EstCostPerItem As Double = 0
                'If price <> 0 Then
                '    EstCostPerItem = (price / qty)
                'End If
                'Dim Obj As New BSDatabase
                'Dim SQL As String = "INSERT INTO List_Case (Manufacturer,Name,ttl," & _
                '    "IsNew,Qty,Price,CID, ePPC,TimesUsed) VALUES('" & manufacturer & "','" & itemName & "','" & trimToLength & _
                '    "'," & intNew & "," & qty & "," & price & "," & caliberId & "," & EstCostPerItem & "," & used & ")"
                'Obj.ConnExec(SQL)
                If FromView Then Call FrmViewListShells.LoadData()
                Close()
            Catch ex As Exception
                'Dim ObjFS As New BSFileSystem
                'Dim sMessage As String = "frmAddShells.btnAdd.Click" & "::" & Err.Number & "::" & ex.Message.ToString()
                'ObjFS.LogFile(MyLogFile, sMessage)
                Call LogError(Name, "btnAdd_Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace