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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewDataSheet_Shotgun))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ManuallyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.WithConfigToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WithoutConfigToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.tslSerialNo = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tslCal = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tslBarrel = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShotwtDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShotSizeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CaseDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PbmDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrimerDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.YdsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ConfigNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NotesDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LoadersLogSGBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.Loaders_Log_SGTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.Loaders_Log_SGTableAdapter()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoadersLogSGBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBox1, Me.ToolStripButton1, Me.ToolStripSplitButton1, Me.ToolStripButton2, Me.tslSerialNo, Me.ToolStripSeparator1, Me.tslCal, Me.ToolStripSeparator2, Me.tslBarrel, Me.ToolStripButton3})
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
        Me.ManuallyToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ManuallyToolStripMenuItem.Text = "Manually"
        '
        'UseConfigurationToolStripMenuItem
        '
        Me.UseConfigurationToolStripMenuItem.Name = "UseConfigurationToolStripMenuItem"
        Me.UseConfigurationToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
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
        Me.WithConfigToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.WithConfigToolStripMenuItem.Text = "With Config"
        '
        'WithoutConfigToolStripMenuItem
        '
        Me.WithoutConfigToolStripMenuItem.Name = "WithoutConfigToolStripMenuItem"
        Me.WithoutConfigToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
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
        Me.tslSerialNo.Size = New System.Drawing.Size(63, 22)
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
        Me.tslCal.Size = New System.Drawing.Size(36, 22)
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
        Me.tslBarrel.Size = New System.Drawing.Size(49, 22)
        Me.tslBarrel.Text = "tslBarrel"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton3.Text = "ToolStripButton3"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.DtDataGridViewTextBoxColumn, Me.ShotwtDataGridViewTextBoxColumn, Me.ShotSizeDataGridViewTextBoxColumn, Me.CaseDataGridViewTextBoxColumn, Me.PbmDataGridViewTextBoxColumn, Me.WadDataGridViewTextBoxColumn, Me.PrimerDataGridViewTextBoxColumn, Me.PdDataGridViewTextBoxColumn, Me.YdsDataGridViewTextBoxColumn, Me.ConfigNameDataGridViewTextBoxColumn, Me.NotesDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.LoadersLogSGBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 25)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(648, 241)
        Me.DataGridView1.TabIndex = 2
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        Me.IDDataGridViewTextBoxColumn.Visible = False
        '
        'DtDataGridViewTextBoxColumn
        '
        Me.DtDataGridViewTextBoxColumn.DataPropertyName = "dt"
        Me.DtDataGridViewTextBoxColumn.HeaderText = "Date & Time"
        Me.DtDataGridViewTextBoxColumn.Name = "DtDataGridViewTextBoxColumn"
        Me.DtDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ShotwtDataGridViewTextBoxColumn
        '
        Me.ShotwtDataGridViewTextBoxColumn.DataPropertyName = "Shotwt"
        Me.ShotwtDataGridViewTextBoxColumn.HeaderText = "Shot Weight"
        Me.ShotwtDataGridViewTextBoxColumn.Name = "ShotwtDataGridViewTextBoxColumn"
        Me.ShotwtDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ShotSizeDataGridViewTextBoxColumn
        '
        Me.ShotSizeDataGridViewTextBoxColumn.DataPropertyName = "ShotSize"
        Me.ShotSizeDataGridViewTextBoxColumn.HeaderText = "Shot Size No. - MFG"
        Me.ShotSizeDataGridViewTextBoxColumn.Name = "ShotSizeDataGridViewTextBoxColumn"
        Me.ShotSizeDataGridViewTextBoxColumn.ReadOnly = True
        '
        'CaseDataGridViewTextBoxColumn
        '
        Me.CaseDataGridViewTextBoxColumn.DataPropertyName = "case"
        Me.CaseDataGridViewTextBoxColumn.HeaderText = "Case Length - MFG"
        Me.CaseDataGridViewTextBoxColumn.Name = "CaseDataGridViewTextBoxColumn"
        Me.CaseDataGridViewTextBoxColumn.ReadOnly = True
        '
        'PbmDataGridViewTextBoxColumn
        '
        Me.PbmDataGridViewTextBoxColumn.DataPropertyName = "pbm"
        Me.PbmDataGridViewTextBoxColumn.HeaderText = "Powder - bushing - MFG"
        Me.PbmDataGridViewTextBoxColumn.Name = "PbmDataGridViewTextBoxColumn"
        Me.PbmDataGridViewTextBoxColumn.ReadOnly = True
        '
        'WadDataGridViewTextBoxColumn
        '
        Me.WadDataGridViewTextBoxColumn.DataPropertyName = "wad"
        Me.WadDataGridViewTextBoxColumn.HeaderText = "WAD"
        Me.WadDataGridViewTextBoxColumn.Name = "WadDataGridViewTextBoxColumn"
        Me.WadDataGridViewTextBoxColumn.ReadOnly = True
        '
        'PrimerDataGridViewTextBoxColumn
        '
        Me.PrimerDataGridViewTextBoxColumn.DataPropertyName = "primer"
        Me.PrimerDataGridViewTextBoxColumn.HeaderText = "Primer"
        Me.PrimerDataGridViewTextBoxColumn.Name = "PrimerDataGridViewTextBoxColumn"
        Me.PrimerDataGridViewTextBoxColumn.ReadOnly = True
        '
        'PdDataGridViewTextBoxColumn
        '
        Me.PdDataGridViewTextBoxColumn.DataPropertyName = "pd"
        Me.PdDataGridViewTextBoxColumn.HeaderText = "Pattern Density"
        Me.PdDataGridViewTextBoxColumn.Name = "PdDataGridViewTextBoxColumn"
        Me.PdDataGridViewTextBoxColumn.ReadOnly = True
        '
        'YdsDataGridViewTextBoxColumn
        '
        Me.YdsDataGridViewTextBoxColumn.DataPropertyName = "yds"
        Me.YdsDataGridViewTextBoxColumn.HeaderText = "Yards"
        Me.YdsDataGridViewTextBoxColumn.Name = "YdsDataGridViewTextBoxColumn"
        Me.YdsDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ConfigNameDataGridViewTextBoxColumn
        '
        Me.ConfigNameDataGridViewTextBoxColumn.DataPropertyName = "ConfigName"
        Me.ConfigNameDataGridViewTextBoxColumn.HeaderText = "ConfigName"
        Me.ConfigNameDataGridViewTextBoxColumn.Name = "ConfigNameDataGridViewTextBoxColumn"
        Me.ConfigNameDataGridViewTextBoxColumn.ReadOnly = True
        '
        'NotesDataGridViewTextBoxColumn
        '
        Me.NotesDataGridViewTextBoxColumn.DataPropertyName = "notes"
        Me.NotesDataGridViewTextBoxColumn.HeaderText = "Notes"
        Me.NotesDataGridViewTextBoxColumn.Name = "NotesDataGridViewTextBoxColumn"
        Me.NotesDataGridViewTextBoxColumn.ReadOnly = True
        '
        'LoadersLogSGBindingSource
        '
        Me.LoadersLogSGBindingSource.DataMember = "Loaders_Log_SG"
        Me.LoadersLogSGBindingSource.DataSource = Me.MLLDataSet
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'Loaders_Log_SGTableAdapter
        '
        Me.Loaders_Log_SGTableAdapter.ClearBeforeFill = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.DeleteToolStripMenuItem, Me.CopyToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 92)
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Image = CType(resources.GetObject("EditToolStripMenuItem.Image"), System.Drawing.Image)
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = CType(resources.GetObject("DeleteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Image = CType(resources.GetObject("CopyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
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
        CType(Me.LoadersLogSGBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
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
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents LoadersLogSGBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Loaders_Log_SGTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.Loaders_Log_SGTableAdapter
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ShotwtDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ShotSizeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CaseDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PbmDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrimerDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents YdsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConfigNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NotesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
