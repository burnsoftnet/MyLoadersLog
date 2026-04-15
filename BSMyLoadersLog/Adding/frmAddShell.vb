'Imports BSMyLoadersLog.LoadersClass
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory

Namespace Adding
    ''' <summary>
    ''' Class FrmAddShell.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmAddShell
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' Automatics the load.
        ''' </summary>
        ''' <exception cref="System.Exception"></exception>
        Sub AutoLoad()
            Try
                'Dim ObjAF As New AutoFillCollections.ShotGun
                'txtManu.AutoCompleteCustomSource = ObjAF.List_SG_Case_Manufacturer
                'txtName.AutoCompleteCustomSource = ObjAF.List_SG_Case_Name
                'txtLen.AutoCompleteCustomSource = ObjAF.List_SG_Case_Length
                'txtPrice.AutoCompleteCustomSource = ObjAF.List_SG_Case_Price
                'txtDRAM.AutoCompleteCustomSource = ObjAF.List_SG_Case_DRAM
                txtManu.AutoCompleteCustomSource = GeneralShotgun.CaseManufacturer(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtName.AutoCompleteCustomSource = GeneralShotgun.CaseName(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtLen.AutoCompleteCustomSource = GeneralShotgun.Length(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtPrice.AutoCompleteCustomSource = GeneralShotgun.Price(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
                txtDRAM.AutoCompleteCustomSource = GeneralShotgun.Dram(DatabasePath, _errOut)
                If _errOut.Length > 0 Then Throw New Exception(_errOut)
            Catch ex As Exception
                Call LogError(Name, "utoLoad", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Saves the data.
        ''' </summary>
        Sub SaveData()
            Try
                Dim manufacturer As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim itemName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim gaugeId As Long = cmbGauge.SelectedValue
                Dim gaugeName As String = cmbGauge.Text
                Dim length As String = GeneralHelpers.FluffContent(txtLen.Text)
                Dim dram As String = GeneralHelpers.FluffContent(txtDRAM.Text)
                Dim qty As Long = nudQty.Value
                'Dim esitematedPricePerItem As Double = 0
                Dim price As Double = GeneralHelpers.FluffContent(CDbl(txtPrice.Text), 0)

                If Not GeneralHelpers.IsRequired(manufacturer, "Manufacturer", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(itemName, "Name", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(length, "Length", Text) Then Exit Sub

                If Not ShotgunHullInventory.Add(DatabasePath, manufacturer, itemName, 
                                                gaugeName, gaugeId, length, qty, price, 
                                                dram, _errOut) Then Throw New Exception(_errOut)
                'If price <> 0 Then
                '    esitematedPricePerItem = (price / qty)
                'End If
                'Dim Obj As New BSDatabase
                'Dim SQL As String = "INSERT INTO List_SG_Case (Manufacturer,Name,Gauge," & _
                '                    "GID,Length,Qty,Price,epps,DRAM) VALUES('" & _
                '                    manufacturer & "','" & itemName & "','" & gaugeName & _
                '                    "'," & gaugeId & ",'" & length & "'," & qty & _
                '                    "," & price & "," & esitematedPricePerItem & ",'" & dram & "')"
                'Obj.ConnExec(SQL)
                Dim sAns As String = MsgBox($"{manufacturer} {itemName} was added to the database.
{Environment.NewLine} Do you wish to add another?", 
                                            MsgBoxStyle.YesNo, Text)
                If sAns = vbYes Then
                    txtManu.Text = ""
                    txtName.Text = ""
                    txtLen.Text = ""
                    nudQty.Value = 0
                    txtPrice.Text = ""
                Else
                    Close()
                End If
            Catch ex As Exception
                Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmAddShell control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmAddShell_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                List_SG_GaugeTableAdapter.Fill(MLLDataSet.List_SG_Gauge)
                Call AutoLoad()
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
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
            Call SaveData()
        End Sub
    End Class
End Namespace