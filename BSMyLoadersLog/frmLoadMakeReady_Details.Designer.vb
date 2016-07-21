<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoadMakeReady_Details
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoadMakeReady_Details))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtManu = New System.Windows.Forms.TextBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtCal = New System.Windows.Forms.TextBox
        Me.txtGrains = New System.Windows.Forms.TextBox
        Me.txtJacket = New System.Windows.Forms.TextBox
        Me.lblInv = New System.Windows.Forms.Label
        Me.btnMake = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.nudQty = New System.Windows.Forms.NumericUpDown
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        CType(Me.nudQty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Manufacturer:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Caliber:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Grains:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Jacket:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 140)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(26, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Qty:"
        '
        'txtManu
        '
        Me.txtManu.Location = New System.Drawing.Point(82, 6)
        Me.txtManu.Name = "txtManu"
        Me.txtManu.Size = New System.Drawing.Size(180, 20)
        Me.txtManu.TabIndex = 1
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(82, 33)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(180, 20)
        Me.txtName.TabIndex = 2
        '
        'txtCal
        '
        Me.txtCal.Location = New System.Drawing.Point(82, 59)
        Me.txtCal.Name = "txtCal"
        Me.txtCal.Size = New System.Drawing.Size(180, 20)
        Me.txtCal.TabIndex = 3
        '
        'txtGrains
        '
        Me.txtGrains.Location = New System.Drawing.Point(82, 85)
        Me.txtGrains.Name = "txtGrains"
        Me.txtGrains.Size = New System.Drawing.Size(180, 20)
        Me.txtGrains.TabIndex = 4
        '
        'txtJacket
        '
        Me.txtJacket.Location = New System.Drawing.Point(82, 111)
        Me.txtJacket.Name = "txtJacket"
        Me.txtJacket.Size = New System.Drawing.Size(180, 20)
        Me.txtJacket.TabIndex = 5
        '
        'lblInv
        '
        Me.lblInv.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInv.Location = New System.Drawing.Point(6, 166)
        Me.lblInv.Name = "lblInv"
        Me.lblInv.Size = New System.Drawing.Size(256, 42)
        Me.lblInv.TabIndex = 12
        '
        'btnMake
        '
        Me.btnMake.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnMake.Location = New System.Drawing.Point(24, 223)
        Me.btnMake.Name = "btnMake"
        Me.btnMake.Size = New System.Drawing.Size(92, 21)
        Me.btnMake.TabIndex = 7
        Me.btnMake.Text = "Make"
        Me.btnMake.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(154, 223)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(92, 21)
        Me.Cancel.TabIndex = 8
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'nudQty
        '
        Me.nudQty.Location = New System.Drawing.Point(82, 138)
        Me.nudQty.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.nudQty.Name = "nudQty"
        Me.nudQty.Size = New System.Drawing.Size(178, 20)
        Me.nudQty.TabIndex = 6
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'frmLoadMakeReady_Details
        '
        Me.AcceptButton = Me.btnMake
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(272, 256)
        Me.Controls.Add(Me.nudQty)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.btnMake)
        Me.Controls.Add(Me.lblInv)
        Me.Controls.Add(Me.txtJacket)
        Me.Controls.Add(Me.txtGrains)
        Me.Controls.Add(Me.txtCal)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtManu)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.HelpProvider1.SetHelpKeyword(Me, "Make Live Ammunition for this Configuration Load")
        Me.HelpProvider1.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.KeywordIndex)
        Me.HelpProvider1.SetHelpString(Me, "Make Live Ammunition for this Configuration Load")
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLoadMakeReady_Details"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.Text = "Ammunition Ready to Use"
        CType(Me.nudQty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtManu As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtCal As System.Windows.Forms.TextBox
    Friend WithEvents txtGrains As System.Windows.Forms.TextBox
    Friend WithEvents txtJacket As System.Windows.Forms.TextBox
    Friend WithEvents lblInv As System.Windows.Forms.Label
    Friend WithEvents btnMake As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents nudQty As System.Windows.Forms.NumericUpDown
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
End Class
