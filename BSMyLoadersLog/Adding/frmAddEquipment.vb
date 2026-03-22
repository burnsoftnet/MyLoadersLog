'Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory

Namespace Adding
    ''' <summary>
    ''' Class frmAddEquipment.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class frmAddEquipment
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut as String
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean
        ''' <summary>
        ''' Automatics the fill.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub AutoFill()
            Try
                'TODO #20 Delete Old Code
                'Dim ObjAF As New AutoFillCollections
                'txtManu.AutoCompleteCustomSource = ObjAF.General_Equipment_Manufacturer
                'txtName.AutoCompleteCustomSource = ObjAF.General_Equipment_Name
                'txtUse.AutoCompleteCustomSource = ObjAF.General_Equipment_Use
                'txtPrice.AutoCompleteCustomSource = ObjAF.General_Equipment_Cost
                'Dim ObjAF As New AutoFillCollections
                txtManu.AutoCompleteCustomSource = Equipment.Manufacturer(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtName.AutoCompleteCustomSource = Equipment.Name(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtUse.AutoCompleteCustomSource = Equipment.Use(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtPrice.AutoCompleteCustomSource = Equipment.Cost(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Me.Name, "AutoFill", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddEquipment control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddEquipment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Call AutoFill()
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.Close()
        End Sub
        ''' <summary>
        ''' Clears the fields.
        ''' </summary>
        Private Sub ClearFields()
            txtManu.Text = ""
            txtName.Text = ""
            txtUse.Text = ""
            txtPrice.Text = ""
            txtNotes.Text = ""
        End Sub
        ''' <summary>
        ''' Handles the Click event of the btnAdd control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ''' <exception cref="System.Exception"></exception>
        Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            Try
                Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim strUse As String = GeneralHelpers.FluffContent(txtUse.Text)
                Dim strPrice As Double = GeneralHelpers.FluffContent(txtPrice.Text, 0)
                Dim strNotes As String = GeneralHelpers.FluffContent(txtNotes.Text)

                If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strName, "Name", Me.Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strUse, "Use", Me.Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strPrice, 0, "Price", Me.Text) Then Exit Sub

                'TODO #20 Delete Old Code
                'Dim Obj As New BSDatabase
                'Dim SQL As String = "INSERT INTO General_Equipment(Manufacturer,Name,Use,Cost,Notes) VALUES('" & _
                '                    strManu & "','" & strName & "','" & strUse & "'," & strPrice & ",'" & strNotes & "')"
                'Obj.ConnExec(SQL)
                'Obj = Nothing

                If Not EquipmentInventory.Add(DatabasePath, strManu, strName, strUse, strPrice, 
                                              strNotes, _errOut) Then Throw New Exception(_errOut)

                Dim sAns As String = MsgBox("Equipment was added to the database, do you with to add another?", MsgBoxStyle.YesNo, Me.Text)
                If FromView Then Call frmView_List_Equipment.LoadData()
                If sAns = vbYes Then
                    ClearFields()
                Else
                    Me.Close()
                End If
            Catch ex As Exception
                Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace