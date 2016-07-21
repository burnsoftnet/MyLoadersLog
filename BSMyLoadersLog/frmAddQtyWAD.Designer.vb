<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddQtyWAD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddQtyWAD))
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnViewCalc = New System.Windows.Forms.Button
        Me.txtUPrice = New System.Windows.Forms.TextBox
        Me.txtUQty = New System.Windows.Forms.TextBox
        Me.txtUPPI = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtCPrice = New System.Windows.Forms.TextBox
        Me.txtCQty = New System.Windows.Forms.TextBox
        Me.txtCPPI = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(131, 226)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 17
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(14, 226)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 16
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnViewCalc)
        Me.GroupBox2.Controls.Add(Me.txtUPrice)
        Me.GroupBox2.Controls.Add(Me.txtUQty)
        Me.GroupBox2.Controls.Add(Me.txtUPPI)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 100)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(217, 120)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Update Status"
        '
        'btnViewCalc
        '
        Me.btnViewCalc.Location = New System.Drawing.Point(53, 88)
        Me.btnViewCalc.Name = "btnViewCalc"
        Me.btnViewCalc.Size = New System.Drawing.Size(110, 26)
        Me.btnViewCalc.TabIndex = 3
        Me.btnViewCalc.Text = "View Calculation"
        Me.btnViewCalc.UseVisualStyleBackColor = True
        '
        'txtUPrice
        '
        Me.txtUPrice.Location = New System.Drawing.Point(96, 40)
        Me.txtUPrice.Name = "txtUPrice"
        Me.txtUPrice.Size = New System.Drawing.Size(107, 20)
        Me.txtUPrice.TabIndex = 2
        Me.txtUPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtUQty
        '
        Me.txtUQty.Location = New System.Drawing.Point(96, 18)
        Me.txtUQty.Name = "txtUQty"
        Me.txtUQty.Size = New System.Drawing.Size(107, 20)
        Me.txtUQty.TabIndex = 1
        Me.txtUQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtUPPI
        '
        Me.txtUPPI.Enabled = False
        Me.txtUPPI.Location = New System.Drawing.Point(96, 62)
        Me.txtUPPI.Name = "txtUPPI"
        Me.txtUPPI.ReadOnly = True
        Me.txtUPPI.Size = New System.Drawing.Size(107, 20)
        Me.txtUPPI.TabIndex = 9
        Me.txtUPPI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Price Per Item:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Price:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(26, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Qty:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtCPrice)
        Me.GroupBox1.Controls.Add(Me.txtCQty)
        Me.GroupBox1.Controls.Add(Me.txtCPPI)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(217, 91)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Current Status"
        '
        'txtCPrice
        '
        Me.txtCPrice.Enabled = False
        Me.txtCPrice.Location = New System.Drawing.Point(91, 39)
        Me.txtCPrice.Name = "txtCPrice"
        Me.txtCPrice.ReadOnly = True
        Me.txtCPrice.Size = New System.Drawing.Size(107, 20)
        Me.txtCPrice.TabIndex = 5
        Me.txtCPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCQty
        '
        Me.txtCQty.Enabled = False
        Me.txtCQty.Location = New System.Drawing.Point(91, 17)
        Me.txtCQty.Name = "txtCQty"
        Me.txtCQty.ReadOnly = True
        Me.txtCQty.Size = New System.Drawing.Size(107, 20)
        Me.txtCQty.TabIndex = 4
        Me.txtCQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCPPI
        '
        Me.txtCPPI.Enabled = False
        Me.txtCPPI.Location = New System.Drawing.Point(91, 61)
        Me.txtCPPI.Name = "txtCPPI"
        Me.txtCPPI.ReadOnly = True
        Me.txtCPPI.Size = New System.Drawing.Size(107, 20)
        Me.txtCPPI.TabIndex = 3
        Me.txtCPPI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Price Per Item:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Price:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Qty:"
        '
        'frmAddQtyWAD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(227, 257)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddQtyWAD"
        Me.Text = "Update WAD Qty."
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnViewCalc As System.Windows.Forms.Button
    Friend WithEvents txtUPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtUQty As System.Windows.Forms.TextBox
    Friend WithEvents txtUPPI As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtCQty As System.Windows.Forms.TextBox
    Friend WithEvents txtCPPI As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
