Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmEditBushing_Powder
    Public CID As Long
    Sub LoadData()
        Try
            Dim SQL As String = "SELECT * from List_SG_Bushing_Powder where ID=" & CID
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim rs As OdbcDataReader
            rs = CMD.ExecuteReader
            While rs.Read
                If Not IsDBNull(rs("Manufacturer")) Then txtManu.Text = rs("Manufacturer")
                If Not IsDBNull(rs("sName")) Then txtName.Text = rs("sName")
                If Not IsDBNull(rs("sCharge")) Then txtCharge.Text = rs("sCharge")
                If Not IsDBNull(rs("PowderName")) Then txtPowderName.Text = rs("PowderName")
                cmbType.Text = rs("sType")
            End While
            rs.Close()
            rs = Nothing
            CMD = Nothing
            Obj.CloseDB()
            Obj = Nothing
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub PreloadData()
        Try
            Dim Objaf As New AutoFillCollections.ShotGun
            txtManu.AutoCompleteCustomSource = Objaf.List_SG_Bushings_Powder_Manufacturer
            txtName.AutoCompleteCustomSource = Objaf.List_SG_Bushings_Powder_Name
            txtCharge.AutoCompleteCustomSource = Objaf.List_SG_Bushings_Powder_sCharge
        Catch ex As Exception
            Call LogError(Me.Name, "PreloadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Call SaveData()
    End Sub
    Sub SaveData()
        Try
            Dim Manu As String = FluffContent(txtManu.Text)
            Dim sName As String = FluffContent(txtName.Text)
            Dim sCharge As String = FluffContent(txtCharge.Text)
            Dim PowderName As String = FluffContent(txtPowderName.Text)
            Dim sType As String = cmbType.Text
            If Not IsRequired(Manu, "Manufacturer", Me.Text) Then Exit Sub
            If Not IsRequired(sName, "Name", Me.Text) Then Exit Sub
            If Not IsRequired(sCharge, "Charge Amount", Me.Text) Then Exit Sub
            Dim SQL As String = "UPDATE List_SG_Bushing_Powder set Manufacturer='" & Manu & "',sName='" & _
                               sName & "',sCharge='" & sCharge & "',PowderName='" & PowderName & _
                               "',sType='" & sType & "', sync_lastupdate=Now() where ID=" & CID
            Dim ObjDb As New BSDatabase
            ObjDb.ConnExec(SQL)
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmEditChargeBushing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Call PreloadData()
            Call LoadData()
        Catch ex As Exception
            Call LogError(Me.Name, "load", Err.Number, ex.Message.ToString)
        End Try
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

    Private Sub txtPowderName_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPowderName.Enter
        txtPowderName.SelectAll()
    End Sub
End Class