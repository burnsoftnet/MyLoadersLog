<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewDataSheet_Shotgun
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewDataSheet_Shotgun))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripDropDownButton
        Me.ManuallyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UseConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton
        Me.WithConfigToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WithoutConfigToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.tslSerialNo = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tslCal = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.tslBarrel = New System.Windows.Forms.ToolStripLabel
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBox1, Me.ToolStripButton1, Me.ToolStripSplitButton1, Me.ToolStripButton2, Me.tslSerialNo, Me.ToolStripSeparator1, Me.tslCal, Me.ToolStripSeparator2, Me.tslBarrel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(648, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ToolStripComboBox1.DropDownWidth = 150
        Me.ToolStripComboBox1.MaxDropDownItems = 100
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(150, 25)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ManuallyToolStripMenuItem, Me.UseConfigurationToolStripMenuItem})
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(29, 22)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        Me.ToolStripButton1.ToolTipText = "Add to Log"
        '
        'ManuallyToolStripMenuItem
        '
        Me.ManuallyToolStripMenuItem.Name = "ManuallyToolStripMenuItem"
        Me.ManuallyToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ManuallyToolStripMenuItem.Text = "Manually"
        '
        'UseConfigurationToolStripMenuItem
        '
        Me.UseConfigurationToolStripMenuItem.Name = "UseConfigurationToolStripMenuItem"
        Me.UseConfigurationToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.UseConfigurationToolStripMenuItem.Text = "Use Configuration"
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WithConfigToolStripMenuItem, Me.WithoutConfigToolStripMenuItem})
        Me.ToolStripSplitButton1.Image = CType(resources.GetObject("ToolStripSplitButton1.Image"), System.Drawing.Image)
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(32, 22)
        Me.ToolStripSplitButton1.Text = "tsbPrint"
        Me.ToolStripSplitButton1.ToolTipText = "View and Print Reports"
        '
        'WithConfigToolStripMenuItem
        '
        Me.WithConfigToolStripMenuItem.Name = "WithConfigToolStripMenuItem"
        Me.WithConfigToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.WithConfigToolStripMenuItem.Text = "With Config"
        '
        'WithoutConfigToolStripMenuItem
        '
        Me.WithoutConfigToolStripMenuItem.Name = "WithoutConfigToolStripMenuItem"
        Me.WithoutConfigToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.WithoutConfigToolStripMenuItem.Text = "Without Config"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "Refresh List"
        '
        'tslSerialNo
        '
        Me.tslSerialNo.Name = "tslSerialNo"
        Me.tslSerialNo.Size = New System.Drawing.Size(57, 22)
        Me.tslSerialNo.Text = "tslSerialNo"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tslCal
        '
        Me.tslCal.Name = "tslCal"
        Me.tslCal.Size = New System.Drawing.Size(33, 22)
        Me.tslCal.Text = "tslCal"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tslBarrel
        '
        Me.tslBarrel.Name = "tslBarrel"
        Me.tslBarrel.Size = New System.Drawing.Size(46, 22)
        Me.tslBarrel.Text = "tslBarrel"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 25)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(648, 241)
        Me.DataGridView1.TabIndex = 2
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'frmViewDataSheet_Shotgun
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 266)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewDataSheet_Shotgun"
        Me.Text = "Loaders Log - Shotgun"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripComboBox1 As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ManuallyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UseConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents WithConfigToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WithoutConfigToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tslSerialNo As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tslCal As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tslBarrel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
End Class
