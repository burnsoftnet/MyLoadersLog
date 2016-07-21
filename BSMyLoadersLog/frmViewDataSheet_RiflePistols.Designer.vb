<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewDataSheet_RiflePistols
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewDataSheet_RiflePistols))
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
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.IdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DtDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.YdsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PwmDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BulletDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PrimerDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CaseDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ConditionsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TlDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ConfigNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NotesDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadersLogNSGBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.Loaders_Log_NSGTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.Loaders_Log_NSGTableAdapter
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.LoadersLogNSGBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBox1, Me.ToolStripButton1, Me.ToolStripSplitButton1, Me.ToolStripButton2, Me.tslSerialNo, Me.ToolStripSeparator1, Me.tslCal, Me.ToolStripSeparator2, Me.tslBarrel, Me.ToolStripButton3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(589, 25)
        Me.ToolStrip1.TabIndex = 0
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
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton3.Text = "Delete From Log"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdDataGridViewTextBoxColumn, Me.DtDataGridViewTextBoxColumn, Me.YdsDataGridViewTextBoxColumn, Me.GsDataGridViewTextBoxColumn, Me.NsDataGridViewTextBoxColumn, Me.PwmDataGridViewTextBoxColumn, Me.BulletDataGridViewTextBoxColumn, Me.PrimerDataGridViewTextBoxColumn, Me.CaseDataGridViewTextBoxColumn, Me.ConditionsDataGridViewTextBoxColumn, Me.TlDataGridViewTextBoxColumn, Me.ConfigNameDataGridViewTextBoxColumn, Me.NotesDataGridViewTextBoxColumn})
        Me.DataGridView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DataGridView1.DataSource = Me.LoadersLogNSGBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 25)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(589, 300)
        Me.DataGridView1.TabIndex = 1
        '
        'IdDataGridViewTextBoxColumn
        '
        Me.IdDataGridViewTextBoxColumn.DataPropertyName = "id"
        Me.IdDataGridViewTextBoxColumn.HeaderText = "id"
        Me.IdDataGridViewTextBoxColumn.Name = "IdDataGridViewTextBoxColumn"
        Me.IdDataGridViewTextBoxColumn.Visible = False
        '
        'DtDataGridViewTextBoxColumn
        '
        Me.DtDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DtDataGridViewTextBoxColumn.DataPropertyName = "dt"
        Me.DtDataGridViewTextBoxColumn.HeaderText = "Date"
        Me.DtDataGridViewTextBoxColumn.Name = "DtDataGridViewTextBoxColumn"
        Me.DtDataGridViewTextBoxColumn.Width = 55
        '
        'YdsDataGridViewTextBoxColumn
        '
        Me.YdsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.YdsDataGridViewTextBoxColumn.DataPropertyName = "yds"
        Me.YdsDataGridViewTextBoxColumn.HeaderText = "Yards"
        Me.YdsDataGridViewTextBoxColumn.Name = "YdsDataGridViewTextBoxColumn"
        Me.YdsDataGridViewTextBoxColumn.Width = 59
        '
        'GsDataGridViewTextBoxColumn
        '
        Me.GsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.GsDataGridViewTextBoxColumn.DataPropertyName = "gs"
        Me.GsDataGridViewTextBoxColumn.HeaderText = "Group Size or Score"
        Me.GsDataGridViewTextBoxColumn.Name = "GsDataGridViewTextBoxColumn"
        Me.GsDataGridViewTextBoxColumn.Width = 91
        '
        'NsDataGridViewTextBoxColumn
        '
        Me.NsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.NsDataGridViewTextBoxColumn.DataPropertyName = "ns"
        Me.NsDataGridViewTextBoxColumn.HeaderText = "No. of Shots"
        Me.NsDataGridViewTextBoxColumn.Name = "NsDataGridViewTextBoxColumn"
        Me.NsDataGridViewTextBoxColumn.Width = 84
        '
        'PwmDataGridViewTextBoxColumn
        '
        Me.PwmDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.PwmDataGridViewTextBoxColumn.DataPropertyName = "pwm"
        Me.PwmDataGridViewTextBoxColumn.HeaderText = "Powder - Wt. - MFG."
        Me.PwmDataGridViewTextBoxColumn.Name = "PwmDataGridViewTextBoxColumn"
        Me.PwmDataGridViewTextBoxColumn.Width = 87
        '
        'BulletDataGridViewTextBoxColumn
        '
        Me.BulletDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.BulletDataGridViewTextBoxColumn.DataPropertyName = "bullet"
        Me.BulletDataGridViewTextBoxColumn.HeaderText = "Bullet"
        Me.BulletDataGridViewTextBoxColumn.Name = "BulletDataGridViewTextBoxColumn"
        Me.BulletDataGridViewTextBoxColumn.Width = 58
        '
        'PrimerDataGridViewTextBoxColumn
        '
        Me.PrimerDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.PrimerDataGridViewTextBoxColumn.DataPropertyName = "primer"
        Me.PrimerDataGridViewTextBoxColumn.HeaderText = "Primer"
        Me.PrimerDataGridViewTextBoxColumn.Name = "PrimerDataGridViewTextBoxColumn"
        Me.PrimerDataGridViewTextBoxColumn.Width = 61
        '
        'CaseDataGridViewTextBoxColumn
        '
        Me.CaseDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.CaseDataGridViewTextBoxColumn.DataPropertyName = "case"
        Me.CaseDataGridViewTextBoxColumn.HeaderText = "Case"
        Me.CaseDataGridViewTextBoxColumn.Name = "CaseDataGridViewTextBoxColumn"
        Me.CaseDataGridViewTextBoxColumn.Width = 56
        '
        'ConditionsDataGridViewTextBoxColumn
        '
        Me.ConditionsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ConditionsDataGridViewTextBoxColumn.DataPropertyName = "conditions"
        Me.ConditionsDataGridViewTextBoxColumn.HeaderText = "Conditions"
        Me.ConditionsDataGridViewTextBoxColumn.Name = "ConditionsDataGridViewTextBoxColumn"
        Me.ConditionsDataGridViewTextBoxColumn.Width = 81
        '
        'TlDataGridViewTextBoxColumn
        '
        Me.TlDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.TlDataGridViewTextBoxColumn.DataPropertyName = "tl"
        Me.TlDataGridViewTextBoxColumn.HeaderText = "Total Length"
        Me.TlDataGridViewTextBoxColumn.Name = "TlDataGridViewTextBoxColumn"
        Me.TlDataGridViewTextBoxColumn.Width = 85
        '
        'ConfigNameDataGridViewTextBoxColumn
        '
        Me.ConfigNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ConfigNameDataGridViewTextBoxColumn.DataPropertyName = "ConfigName"
        Me.ConfigNameDataGridViewTextBoxColumn.HeaderText = "Config Name"
        Me.ConfigNameDataGridViewTextBoxColumn.Name = "ConfigNameDataGridViewTextBoxColumn"
        Me.ConfigNameDataGridViewTextBoxColumn.Width = 86
        '
        'NotesDataGridViewTextBoxColumn
        '
        Me.NotesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.NotesDataGridViewTextBoxColumn.DataPropertyName = "notes"
        Me.NotesDataGridViewTextBoxColumn.HeaderText = "Notes"
        Me.NotesDataGridViewTextBoxColumn.Name = "NotesDataGridViewTextBoxColumn"
        Me.NotesDataGridViewTextBoxColumn.Width = 60
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.DeleteToolStripMenuItem, Me.CopyToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(117, 70)
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Image = CType(resources.GetObject("EditToolStripMenuItem.Image"), System.Drawing.Image)
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = CType(resources.GetObject("DeleteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Image = CType(resources.GetObject("CopyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        '
        'LoadersLogNSGBindingSource
        '
        Me.LoadersLogNSGBindingSource.DataMember = "Loaders_Log_NSG"
        Me.LoadersLogNSGBindingSource.DataSource = Me.MLLDataSet
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Loaders_Log_NSGTableAdapter
        '
        Me.Loaders_Log_NSGTableAdapter.ClearBeforeFill = True
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'frmViewDataSheet_RiflePistols
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(589, 325)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.HelpProvider1.SetHelpKeyword(Me, "View Loaders Log - Rifle & Pistol")
        Me.HelpProvider1.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.KeywordIndex)
        Me.HelpProvider1.SetHelpString(Me, "View Loaders Log - Rifle & Pistol")
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewDataSheet_RiflePistols"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.Text = "Loaders Log - Rifles & Pistols"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.LoadersLogNSGBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripComboBox1 As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents LoadersLogNSGBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Loaders_Log_NSGTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.Loaders_Log_NSGTableAdapter
    Friend WithEvents IdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents YdsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PwmDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BulletDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrimerDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CaseDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConditionsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TlDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConfigNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NotesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ManuallyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UseConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tslSerialNo As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tslCal As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tslBarrel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents WithConfigToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WithoutConfigToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
