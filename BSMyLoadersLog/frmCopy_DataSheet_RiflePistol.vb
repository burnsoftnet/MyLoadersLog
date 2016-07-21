Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmCopy_DataSheet_RiflePistol
    Public CFGID As Long
    Public FromView As Boolean
    Sub LoadData()
        Try
            Me.Loaders_Log_FirearmsTableAdapter.Fill(Me.MLLDataSet.Loaders_Log_Firearms)
            Dim SQL As String = "SELECT * from Loaders_Log_NSG where ID=" & CFGID
            Dim Obj As New BSDatabase
            Call Obj.ConnectDB()
            Dim CMD As New OdbcCommand(SQL, Obj.Conn)
            Dim RS As OdbcDataReader
            RS = CMD.ExecuteReader
            Dim strSplit() As String
            While RS.Read
                cmbFirearm.SelectedValue = RS("fid")
                dtpTested.Value = RS("dt")
                txtGroup.Text = RS("gs")
                nudShots.Value = RS("ns")
                nudYards.Value = RS("yds")
                strSplit = Split(RS("pwm"), " - ")
                txtPowName.Text = Trim(strSplit(0))
                txtPowWei.Text = Trim(strSplit(1))
                txtPowManu.Text = Trim(strSplit(2))
                'txtPowwtMFG.Text = RS("pwm")
                txtBullet.Text = RS("bullet")
                txtPrimer.Text = RS("primer")
                txtCase.Text = RS("case")
                txtCon.Text = RS("conditions")
                txtLen.Text = RS("tl")
                txtNotes.Text = RS("notes")
            End While
            RS.Close()
            RS = Nothing
            CMD = Nothing
            Obj.CloseDB()
        Catch ex As Exception
            Call LogError(Me.Name, "LoadData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Sub SaveData()
        Try
            Dim lngFID As Long = cmbFirearm.SelectedValue
            Dim strFireArm As String = cmbFirearm.Text
            Dim strDateTested As String = dtpTested.Value
            Dim strGroup As String = FluffContent(txtGroup.Text)
            Dim lngNumShots As Long = nudShots.Value
            Dim lngYards As Long = nudYards.Value
            Dim strPowName As String = FluffContent(txtPowName.Text)
            Dim strPowWei As String = FluffContent(txtPowWei.Text)
            Dim strPowManu As String = FluffContent(txtPowManu.Text)
            Dim strBullet As String = FluffContent(txtBullet.Text)
            Dim strPrimer As String = FluffContent(txtPrimer.Text)
            Dim strCase As String = FluffContent(txtCase.Text)
            Dim strCond As String = FluffContent(txtCon.Text)
            Dim strLen As String = FluffContent(txtLen.Text)
            Dim strNotes As String = FluffContent(txtNotes.Text)
            Dim ConfigName As String = "N/A"
            Dim strBarLen As String = ""
            Dim Caliber As String = ""
            Dim Obj As New BSDatabase
            Dim ObjIM As New InventoryMath
            Dim ObjGF As New GlobalFunctions
            Call ObjGF.GetFirearmDetails(lngFID, 0, "", "", "", Caliber, strBarLen)
            Dim Sql As String = "INSERT INTO Loaders_Log_NSG (fid,dt,yds,gs,ns,pwm,bullet," & _
                    "primer,case,conditions,tl,notes,ConfigName,FirearmName,Caliber,BarrelLen)" & _
                    " VALUES (" & lngFID & ",'" & strDateTested & "'," & lngYards & _
                    ",'" & strGroup & "'," & lngNumShots & ",'" & strPowName & " - " & strPowWei & _
                    " - " & strPowManu & "','" & strBullet & _
                    "','" & strPrimer & "','" & strCase & _
                    "','" & strCond & "','" & strLen & "','" & strNotes & "','" & _
                    ConfigName & "','" & strFireArm & "','" & Caliber & "','" & strBarLen & "')"
            Obj.ConnExec(Sql)
            MsgBox("Information was saved to the Loaders Log!")
            If FromView Then Call frmViewDataSheet_RiflePistols.LoadDataCur()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmCopy_DataSheet_RiflePistol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call SaveData()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class