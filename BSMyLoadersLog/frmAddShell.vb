Imports BSMyLoadersLog.LoadersClass
Public Class frmAddShell
    Sub AutoLoad()
        Try
            Dim ObjAF As New AutoFillCollections.ShotGun
            txtManu.AutoCompleteCustomSource = ObjAF.List_SG_Case_Manufacturer
            txtName.AutoCompleteCustomSource = ObjAF.List_SG_Case_Name
            txtLen.AutoCompleteCustomSource = ObjAF.List_SG_Case_Length
            txtPrice.AutoCompleteCustomSource = ObjAF.List_SG_Case_Price
            txtDRAM.AutoCompleteCustomSource = ObjAF.List_SG_Case_DRAM
        Catch ex As Exception
            Call LogError(Me.Name, "utoLoad", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim strManu As String = FluffContent(txtManu.Text)
            Dim strName As String = FluffContent(txtName.Text)
            Dim GID As Long = cmbGauge.SelectedValue
            Dim strGAName As String = cmbGauge.Text
            Dim strLen As String = FluffContent(txtLen.Text)
            Dim sDRAM As String = FluffContent(txtDRAM.Text)
            Dim iQty As Long = nudQty.Value
            Dim epps As Double = 0
            Dim dPrice As Double = FluffContent(txtPrice.Text, 0)

            If Not IsRequired(strManu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(strName, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(strLen, "Length", Me.Text) Then Exit Sub
            If dPrice <> 0 Then
                epps = (dPrice / iQty)
            End If
            Dim Obj As New BSDatabase
            Dim SQL As String = "INSERT INTO List_SG_Case (Manufacturer,Name,Gauge," & _
                                    "GID,Length,Qty,Price,epps,DRAM) VALUES('" & _
                                    strManu & "','" & strName & "','" & strGAName & _
                                    "'," & GID & ",'" & strLen & "'," & iQty & _
                                    "," & dPrice & "," & epps & ",'" & sDRAM & "')"
            Obj.ConnExec(SQL)
            Dim sAns As String = MsgBox(strManu & " " & strName & " was added to the database." & Chr(10) & "Do you wish to add another?", MsgBoxStyle.YesNo, Me.Text)
            If sAns = vbYes Then
                txtManu.Text = ""
                txtName.Text = ""
                txtLen.Text = ""
                nudQty.Value = 0
                txtPrice.Text = ""
            Else
                Me.Close()
            End If
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmAddShell_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.List_SG_GaugeTableAdapter.Fill(Me.MLLDataSet.List_SG_Gauge)
            Call AutoLoad()
        Catch ex As Exception
            Call LogError(Me.Name, "Load", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
End Class