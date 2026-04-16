
''' <summary>
''' Class FrmForgotPassword.
''' Implements the <see cref="System.Windows.Forms.Form" />
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class FrmForgotPassword
    ''' <summary>
    ''' Handles the Click event of the btnAnswer control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnAnswer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAnswer.Click
        Dim strword As String = LCase(Trim(txtWord.Text))
        If strword = UseMyForgotWord Then
            Dim sMsg As String = "Your password is " & UseMyPwd
            txtWord.Text = ""
            MsgBox(sMsg)
            frmLogin.TopMost = True
            Close()
        Else
            Dim sMsg As String = "That is incorrect!"
            txtWord.Text = ""
            MsgBox(sMsg)
        End If
    End Sub
    ''' <summary>
    ''' Handles the Load event of the frmForgotPassword control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub frmForgotPassword_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        lblPhrase.Text = UseMyForgotPhrase
        TopMost = True
    End Sub
End Class