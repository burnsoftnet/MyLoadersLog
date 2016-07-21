Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmEditBushing_Shot
    Public CID As Long
    Sub LoadData()
        Dim Obj As New BSDatabase
        Obj.ConnectDB()
        Dim SQL As String = "SELECT Manufacturer,sName,sCharge,sType from List_SG_Bushing_Shot where ID=" & CID
        Dim CMD As New OdbcCommand(SQL, Obj.Conn)
        Dim RS As OdbcDataReader
        RS = CMD.ExecuteReader
        While RS.Read
            If Not IsDBNull(RS("Manufacturer")) Then txtManu.Text = RS("Manufacturer")
            If Not IsDBNull(RS("sName")) Then txtName.Text = RS("sName")
            If Not IsDBNull(RS("sCharge")) Then txtCharge.Text = RS("sCharge")
            If Not IsDBNull(RS("sType")) Then cmbType.Text = RS("sType")
        End While
        RS.Close()
        RS = Nothing
        CMD = Nothing
        Obj.CloseDB()
        Obj = Nothing
    End Sub
    Sub PreLoadData()
        Try
            Dim ObjAF As New AutoFillCollections
            txtCharge.AutoCompleteCustomSource = ObjAF.List_SG_Bushings_Shot_sCharge
            txtManu.AutoCompleteCustomSource = ObjAF.List_SG_Bushings_Shot_Manufacturer
            txtName.AutoCompleteCustomSource = ObjAF.List_SG_Bushings_Shot_Name
        Catch ex As Exception
            Call LogError(Me.Name, "PreLoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub
    Private Sub txtCharge_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCharge.Enter
        txtCharge.SelectAll()
    End Sub

    Private Sub txtManu_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtManu.Enter
        txtManu.SelectAll()
    End Sub

    Private Sub txtName_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.Enter
        txtName.SelectAll()
    End Sub
    Sub SaveData()
        Try
            Dim Manu As String = FluffContent(txtManu.Text)
            Dim sName As String = FluffContent(txtName.Text)
            Dim sCharge As String = FluffContent(txtCharge.Text)
            Dim sType As String = cmbType.Text

            If Not IsRequired(Manu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(sName, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(sCharge, "Charge Amount", Me.Text) Then Exit Sub
            Dim SQL As String = "UPDATE List_SG_Bushing_Shot set Manufacturer='" & Manu & "',sName='" & sName & "',sCharge='" & sCharge & "',sType='" & sType & "' where ID=" & CID
            Dim ObjDb As New BSDatabase
            ObjDb.ConnExec(SQL)
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub

    Private Sub frmEditBushing_Shot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call PreLoadData()
        Call LoadData()
    End Sub
End Class