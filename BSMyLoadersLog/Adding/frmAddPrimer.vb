'Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory

Namespace Adding
    ''' <summary>
    ''' Class FrmAddPrimer.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmAddPrimer
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean
        ''' <summary>
        ''' Loads the data.
        ''' </summary>
        Sub LoadData()
            Try
                Call AutoFill()
                General_Primer_TypeTableAdapter.Fill(MLLDataSet.General_Primer_Type)
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddPrimer control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddPrimer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call LoadData()
        End Sub
        ''' <summary>
        ''' Automatics the fill.
        ''' </summary>
        Sub AutoFill()
            Try
                ' TODO: @20 Removed Unused Code
                'Dim ObjAF As New AutoFillCollections
                'txtManu.AutoCompleteCustomSource = ObjAF.General_Primer_Type_ManuFacturers
                'txtName.AutoCompleteCustomSource = ObjAF.General_Primer_Type_Name
                'txtPrice.AutoCompleteCustomSource = ObjAF.General_Primer_Type_Price
                'Dim ObjAF As New AutoFillCollections
                txtManu.AutoCompleteCustomSource = Primers.Manufacturer(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtName.AutoCompleteCustomSource = Primers.Name(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtPrice.AutoCompleteCustomSource = Primers.Price(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "AutoFill", Err.Number, ex.Message.ToString)
            End Try
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
                Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim intPriType As Integer = cmbPriType.SelectedValue
                Dim intQty As Integer = nudQty.Value
                Dim dbPrice As Double = GeneralHelpers.FluffContent(txtPrice.Text, 0)

                If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", 
                                                 Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strName, "Name", 
                                                 Text) Then Exit Sub
                If Not PrimerInventory.Add(DatabasePath, strManu, strName, intPriType, 
                                           dbPrice, intQty, _errOut) Then Throw New Exception(_errOut)
                'Dim EstCostPerItem As Double = 0
                'If dbPrice <> 0 Then
                '    EstCostPerItem = (dbPrice / intQty)
                'End If
                'Dim Obj As New BSDatabase
                'Dim SQL As String = "INSERT INTO General_Primer(Manufacturer,Name,Primer_Type," & _
                '                    "Qty,Price, ePPP) VALUES('" & strManu & "','" & strName & "'," & intPriType & "," & _
                '                    intQty & "," & dbPrice & "," & EstCostPerItem & ")"
                'Obj.ConnExec(SQL)
                If FromView Then Call frmView_List_Primer.LoadData()
                Close()
            Catch ex As Exception
                Call LogError(Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace