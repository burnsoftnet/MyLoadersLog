<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAddDataSheetShotGunCfg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAddDataSheetShotGunCfg))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbFirearm = New System.Windows.Forms.ComboBox()
        Me.LoadersLogFirearmsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet()
        Me.Loaders_Log_FirearmsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.Loaders_Log_FirearmsTableAdapter()
        Me.dtpTested = New System.Windows.Forms.DateTimePicker()
        Me.nudYards = New System.Windows.Forms.NumericUpDown()
        Me.cmbConfiguration = New System.Windows.Forms.ComboBox()
        Me.ConfigListSimpleSGBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.txtPattern = New System.Windows.Forms.TextBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.ConfigList_Simple_SGTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.ConfigList_Simple_SGTableAdapter()
        CType(Me.LoadersLogFirearmsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudYards, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ConfigListSimpleSGBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Firearm tested with:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Date tested:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Yards tested at:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Configuration Used:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 129)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Pattern Density:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 151)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Notes:"
        '
        'cmbFirearm
        '
        Me.cmbFirearm.DataSource = Me.LoadersLogFirearmsBindingSource
        Me.cmbFirearm.DisplayMember = "FullName"
        Me.cmbFirearm.FormattingEnabled = True
        Me.cmbFirearm.Location = New System.Drawing.Point(115, 20)
        Me.cmbFirearm.Name = "cmbFirearm"
        Me.cmbFirearm.Size = New System.Drawing.Size(213, 21)
        Me.cmbFirearm.TabIndex = 6
        Me.cmbFirearm.ValueMember = "ID"
        '
        'LoadersLogFirearmsBindingSource
        '
        Me.LoadersLogFirearmsBindingSource.DataMember = "Loaders_Log_Firearms"
        Me.LoadersLogFirearmsBindingSource.DataSource = Me.MLLDataSet
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Loaders_Log_FirearmsTableAdapter
        '
        Me.Loaders_Log_FirearmsTableAdapter.ClearBeforeFill = True
        '
        'dtpTested
        '
        Me.dtpTested.Location = New System.Drawing.Point(115, 47)
        Me.dtpTested.Name = "dtpTested"
        Me.dtpTested.Size = New System.Drawing.Size(213, 20)
        Me.dtpTested.TabIndex = 7
        '
        'nudYards
        '
        Me.nudYards.Location = New System.Drawing.Point(115, 73)
        Me.nudYards.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nudYards.Name = "nudYards"
        Me.nudYards.Size = New System.Drawing.Size(213, 20)
        Me.nudYards.TabIndex = 8
        Me.nudYards.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbConfiguration
        '
        Me.cmbConfiguration.DataSource = Me.ConfigListSimpleSGBindingSource
        Me.cmbConfiguration.DisplayMember = "ConfigName"
        Me.cmbConfiguration.FormattingEnabled = True
        Me.cmbConfiguration.Location = New System.Drawing.Point(115, 99)
        Me.cmbConfiguration.Name = "cmbConfiguration"
        Me.cmbConfiguration.Size = New System.Drawing.Size(213, 21)
        Me.cmbConfiguration.TabIndex = 9
        Me.cmbConfiguration.ValueMember = "ID"
        '
        'ConfigListSimpleSGBindingSource
        '
        Me.ConfigListSimpleSGBindingSource.DataMember = "ConfigList_Simple_SG"
        Me.ConfigListSimpleSGBindingSource.DataSource = Me.MLLDataSet
        '
        'txtPattern
        '
        Me.txtPattern.Location = New System.Drawing.Point(115, 126)
        Me.txtPattern.Name = "txtPattern"
        Me.txtPattern.Size = New System.Drawing.Size(213, 20)
        Me.txtPattern.TabIndex = 10
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(13, 168)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(315, 102)
        Me.txtNotes.TabIndex = 11
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(13, 277)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 12
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(252, 278)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'ConfigList_Simple_SGTableAdapter
        '
        Me.ConfigList_Simple_SGTableAdapter.ClearBeforeFill = True
        '
        'frmAddDataSheet_ShotGun_CFG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 313)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.txtPattern)
        Me.Controls.Add(Me.cmbConfiguration)
        Me.Controls.Add(Me.nudYards)
        Me.Controls.Add(Me.dtpTested)
        Me.Controls.Add(Me.cmbFirearm)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAddDataSheetShotGunCfg"
        Me.Text = "Add to Loaders Log Using a Configuration"
        CType(Me.LoadersLogFirearmsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudYards, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ConfigListSimpleSGBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbFirearm As System.Windows.Forms.ComboBox
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents LoadersLogFirearmsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Loaders_Log_FirearmsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.Loaders_Log_FirearmsTableAdapter
    Friend WithEvents dtpTested As System.Windows.Forms.DateTimePicker
    Friend WithEvents nudYards As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbConfiguration As System.Windows.Forms.ComboBox
    Friend WithEvents txtPattern As System.Windows.Forms.TextBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents ConfigListSimpleSGBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ConfigList_Simple_SGTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.ConfigList_Simple_SGTableAdapter
End Class
