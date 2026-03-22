Imports BurnSoft.Applications.MLL.Helpers
Imports BurnSoft.Applications.MLL.LoadersLog
'Imports BSMyLoadersLog.LoadersClass
'Imports BurnSoft.Applications.MLL.Types

Namespace Adding
    ''' <summary>
    ''' Class FrmAddFirearm.
    ''' Implements the <see cref="System.Windows.Forms.Form" />
    ''' </summary>
    ''' <seealso cref="System.Windows.Forms.Form" />
    Public Class FrmAddFirearm
        ''' <summary>
        ''' The error out
        ''' </summary>
        Dim _errOut as String
        ''' <summary>
        ''' From view
        ''' </summary>
        Public FromView As Boolean
        'TODO: #20 Clean Up Code
        'Sub AutoFill()
        '    Try
        '        'Dim ObjAF As New AutoFillCollections
        '    Catch ex As Exception
        '        Call LogError(Me.Name, "AutoFill", Err.Number, ex.Message.ToString)
        '    End Try
        'End Sub        
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
        ''' <exception cref="System.Exception"></exception>
        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Try
                Dim strManu As String = GeneralHelpers.FluffContent(txtManu.Text)
                Dim strModel As String = GeneralHelpers.FluffContent(txtModel.Text)
                Dim strSerial As String = GeneralHelpers.FluffContent(txtSerial.Text)
                Dim strCal As String = GeneralHelpers.FluffContent(txtCal.Text)
                Dim strBarrel As String = GeneralHelpers.FluffContent(txtBarrel.Text)
                Dim strType As String = GeneralHelpers.FluffContent(txtType.Text)
                'TODO: #20 Clean Up Code
                'Dim MGCID As Integer = 0
                Dim iExclude As Integer = 0
                If chkExlude.Checked Then iExclude = 1

                If Not GeneralHelpers.IsRequired(strManu, "Manufacturer", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strModel, "model", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strSerial, "Serial Number", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strCal, "Caliber", Text) Then Exit Sub
                If Not GeneralHelpers.IsRequired(strType, "Type", Text) Then Exit Sub

                'Dim strFullName As String = strManu & " " & strModel
                If Not Firearms.Add(DatabasePath, strManu, strModel, strSerial, strCal, 
                                    strType, strBarrel, _errOut) Then Throw New Exception(_errOut)
                'TODO: #20 Clean Up Code
                'Dim Obj As New BSDatabase
                'Dim SQL As String = "INSERT INTO Loaders_Log_Firearms (MGCID,FullName,Manu,Model,Cal,Barrel,SerialNo,GType,exclude)" & _
                '                    " VALUES (" & MGCID & ",'" & strFullName & "','" & strManu & "','" & _
                '                    strModel & "','" & strCal & "','" & strBarrel & "','" & strSerial & _
                '                    "','" & strType & "'" & iExclude & ")"
                'Obj.ConnExec(SQL)
                MsgBox(strManu & " " & strModel & " was added to the database!")
                Close()
            Catch ex As Exception
                Call LogError(Me.Name, "btnAdd.Click", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace