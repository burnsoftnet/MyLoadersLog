'Imports System.Data.Odbc
'Imports BSMyLoadersLog.LoadersClass
Imports BSMyLoadersLog.Viewing
Imports BurnSoft.Applications.MLL.AutoFill
Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.Inventory
Imports BurnSoft.Applications.MLL.Types

Namespace Editing
    ' TODO: #20 clean up code
    ''' <summary>
    ''' Class frmEditHulls.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmEditHulls
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut As String
        ''' <summary>
        ''' The hull identifier
        ''' </summary>
        Public HullId As Integer
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean
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
                Call LogError(Name, "AutoLoad", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Loaddatas this instance.
        ''' </summary>
        Sub Loaddata()
            Try
                List_SG_GaugeTableAdapter.Fill(MLLDataSet.List_SG_Gauge)
                Dim lst As List(Of ShotgunHullData) = ShotgunHullInventory.GetDetails(DatabasePath, HullId, _errOut)
                If _errOut.Length > 0 Then Throw new Exception(_errOut)
                For Each o As ShotgunHullData In lst
                    cmbGauge.SelectedValue = o.GunId
                    cmbGauge.Update()
                    txtManu.Text = GeneralHelpers.UnFluffContent(o.Manufacturer)
                    txtName.Text = GeneralHelpers.UnFluffContent(o.Name)
                    txtLen.Text = GeneralHelpers.UnFluffContent(o.Length)
                    txtDRAM.Text = GeneralHelpers.UnFluffContent(o.DRAM)
                    nudQty.Value = o.Qty
                    txtPrice.Text = Converters.ConvertToDollars(o.Qty * o.EstimatedPricePerItem)
                Next
                'Dim Obj As New BSDatabase
                'Call Obj.ConnectDB()
                'Dim SQL As String = "SELECT * from List_SG_Case where ID=" & HullId
                'Dim CMD As New OdbcCommand(SQL, Obj.Conn)
                'Dim RS As OdbcDataReader
                'RS = CMD.ExecuteReader
                'Dim TimesUsed As Long = 0
                'Dim iQty As Integer = 0
                'Dim eppo As Double = 0
                'Dim dPrice As Double = 0
                'While RS.Read
                '    If Not IsDBNull(RS("GID")) Then
                '        cmbGauge.SelectedValue = RS("GID")
                '        cmbGauge.Update()
                '    End If
                '    If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = GeneralHelpers.UnFluffContent(RS("Manufacturer"))
                '    If Not IsDBNull(RS("Name")) Then txtName.Text = GeneralHelpers.UnFluffContent(RS("Name"))
                '    If Not IsDBNull(RS("Length")) Then txtLen.Text = GeneralHelpers.UnFluffContent(RS("Length"))
                '    If Not IsDBNull(RS("Price")) Then dPrice = RS("Price")
                '    If Not IsDBNull(RS("Qty")) Then iQty = RS("Qty")
                '    If Not IsDBNull(RS("epps")) Then eppo = RS("epps")
                '    If Not IsDBNull(RS("DRAM")) Then txtDRAM.Text = GeneralHelpers.UnFluffContent(RS("DRAM"))
                '    dPrice = eppo * iQty
                '    nudQty.Value = iQty
                '    Dim ObjIM As New InventoryMath
                '    txtPrice.Text = Converters.ConvertToDollars(dPrice)
                'End While
                'RS.Close()
                'RS = Nothing
                'CMD = Nothing
                'Call Obj.CloseDB()
            Catch ex As Exception
                Call LogError(Name, "LoadData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Saves the data.
        ''' </summary>
        Sub SaveData()
            Try
                Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim strName As String = GeneralHelpers.FluffContent(txtName.Text)
                Dim strlen As String = GeneralHelpers.FluffContent(txtLen.Text)
                Dim intQty As Integer = nudQty.Value
                Dim dbPrice As Double = GeneralHelpers.FluffContent(txtPrice.Text, 0)
                Dim sDram As String = GeneralHelpers.FluffContent(txtDRAM.Text)
                'Dim LngGAID As Long = cmbGauge.SelectedValue
                Dim sGauge As String = cmbGauge.Text
                If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strName, "Name", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strlen, "Length", Text) Then Exit Sub
                If Not ShotgunHullInventory.Add(DatabasePath, HullId, strManu, strName, 
                                                sGauge, strlen, intQty, dbPrice, sDram, _errOut) then Throw new Exception(_errOut)

                'Dim EstCostPerItem As Double = 0
                'If dbPrice <> 0 And intQty > 0 Then
                '    EstCostPerItem = (dbPrice / intQty)
                'End If
                'Dim Obj As New BSDatabase
                'Dim SQL As String = "UPDATE List_SG_Case set Manufacturer='" & strManu & "',Name='" & strName & "'" & _
                '                    ",Length='" & strlen & "',Gauge='" & sGauge & "', epps=" & EstCostPerItem & ", " & _
                '                    "Qty=" & intQty & ",Price=" & dbPrice & ",GID=" & LngGAID & _
                '                    ", DRAM='" & sDRAM & "' where id=" & HullId
                'Obj.ConnExec(SQL)
                If FromView Then Call FrmViewListShellHulls.LoadData()
                Close()
            Catch ex As Exception
                Call LogError(Name, "SaveData", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the Load event of the frmEditHulls control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub frmEditHulls_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Call AutoLoad()
            Call Loaddata()
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