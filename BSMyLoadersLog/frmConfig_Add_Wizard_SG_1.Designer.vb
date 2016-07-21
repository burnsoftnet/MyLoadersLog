<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig_Add_Wizard_SG_1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig_Add_Wizard_SG_1))
        Me.chkShot = New System.Windows.Forms.CheckBox
        Me.chkSlug = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCon = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'chkShot
        '
        Me.chkShot.AutoSize = True
        Me.chkShot.Location = New System.Drawing.Point(16, 55)
        Me.chkShot.Name = "chkShot"
        Me.chkShot.Size = New System.Drawing.Size(187, 17)
        Me.chkShot.TabIndex = 0
        Me.chkShot.Text = "Shot (#9,8,7,6,5,4,3,0,00,000,etc)"
        Me.chkShot.UseVisualStyleBackColor = True
        '
        'chkSlug
        '
        Me.chkSlug.AutoSize = True
        Me.chkSlug.Location = New System.Drawing.Point(16, 78)
        Me.chkSlug.Name = "chkSlug"
        Me.chkSlug.Size = New System.Drawing.Size(86, 17)
        Me.chkSlug.TabIndex = 1
        Me.chkSlug.Text = "Slug/Sabbot"
        Me.chkSlug.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(240, 28)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Please select the type of shotgun ammunition that you want to build."
        '
        'btnCon
        '
        Me.btnCon.Location = New System.Drawing.Point(16, 110)
        Me.btnCon.Name = "btnCon"
        Me.btnCon.Size = New System.Drawing.Size(75, 23)
        Me.btnCon.TabIndex = 3
        Me.btnCon.Text = "&Continue"
        Me.btnCon.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(159, 110)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmConfig_Add_Wizard_SG_1
        '
        Me.AcceptButton = Me.btnCon
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(257, 144)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnCon)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkSlug)
        Me.Controls.Add(Me.chkShot)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfig_Add_Wizard_SG_1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Shotgun Ammunition Type"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkShot As System.Windows.Forms.CheckBox
    Friend WithEvents chkSlug As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCon As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
