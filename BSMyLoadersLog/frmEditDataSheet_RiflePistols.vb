Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Public Class frmEditDataSheet_RiflePistols
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
            While RS.Read
                cmbFirearm.SelectedValue = RS("fid")
                dtpTested.Value = RS("dt")
                txtGroup.Text = UnFluffContent(RS("gs"))
                nudShots.Value = RS("ns")
                nudYards.Value = RS("yds")
                txtPowwtMFG.Text = UnFluffContent(RS("pwm"))
                txtBullet.Text = UnFluffContent(RS("bullet"))
                txtPrimer.Text = UnFluffContent(RS("primer"))
                txtCase.Text = UnFluffContent(RS("case"))
                txtCon.Text = UnFluffContent(RS("conditions"))
                txtLen.Text = UnFluffContent(RS("tl"))
                txtNotes.Text = UnFluffContent(RS("notes"))
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
            Dim strPowName As String = FluffContent(txtPowwtMFG.Text)
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
            Dim Sql As String = "UPDATE Loaders_Log_NSG set fid=" & lngFID & ",dt='" & strDateTested & _
                                "',yds=" & lngYards & ",gs='" & strGroup & "',ns=" & lngNumShots & _
                                ",pwm='" & strPowName & "',bullet='" & strBullet & "'," & _
                                "primer='" & strPrimer & "',case='" & strCase & "',conditions='" & _
                                strCond & "',tl='" & strLen & "',notes='" & strNotes & _
                                "',FirearmName='" & strFireArm & "',Caliber='" & Caliber & _
                                "',BarrelLen='" & strBarLen & "' where ID=" & CFGID
            Obj.ConnExec(Sql)
            MsgBox("Information was saved to the Loaders Log!")
            If FromView Then Call frmViewDataSheet_RiflePistols.LoadDataCur()
            Me.Close()
        Catch ex As Exception
            Call LogError(Me.Name, "SaveData", Err.Number, ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Call SaveData()
    End Sub

    Private Sub frmEditDataSheet_RiflePistols_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub
End Class