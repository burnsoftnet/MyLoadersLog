<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddDataSheet_RiflePistols_CFG
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddDataSheet_RiflePistols_CFG))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbFirearm = New System.Windows.Forms.ComboBox()
        Me.LoadersLogFirearmsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet()
        Me.dtpTested = New System.Windows.Forms.DateTimePicker()
        Me.txtGroup = New System.Windows.Forms.TextBox()
        Me.txtCon = New System.Windows.Forms.TextBox()
        Me.txtLen = New System.Windows.Forms.TextBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Loaders_Log_FirearmsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.Loaders_Log_FirearmsTableAdapter()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.nudShots = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.nudYards = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbConfig = New System.Windows.Forms.ComboBox()
        Me.ConfigListSimpleBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.ConfigList_SimpleTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.ConfigList_SimpleTableAdapter()
        CType(Me.LoadersLogFirearmsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudShots, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudYards, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ConfigListSimpleBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Firearm tested with:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Date tested:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Group size or Score:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 176)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Conditions:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 202)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Total Length:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 224)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Notes"
        '
        'cmbFirearm
        '
        Me.cmbFirearm.DataSource = Me.LoadersLogFirearmsBindingSource
        Me.cmbFirearm.DisplayMember = "FullName"
        Me.cmbFirearm.FormattingEnabled = True
        Me.cmbFirearm.Location = New System.Drawing.Point(136, 12)
        Me.cmbFirearm.Name = "cmbFirearm"
        Me.cmbFirearm.Size = New System.Drawing.Size(200, 21)
        Me.cmbFirearm.TabIndex = 1
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
        'dtpTested
        '
        Me.dtpTested.Location = New System.Drawing.Point(136, 39)
        Me.dtpTested.Name = "dtpTested"
        Me.dtpTested.Size = New System.Drawing.Size(200, 20)
        Me.dtpTested.TabIndex = 2
        '
        'txtGroup
        '
        Me.txtGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtGroup.Location = New System.Drawing.Point(136, 92)
        Me.txtGroup.Name = "txtGroup"
        Me.txtGroup.Size = New System.Drawing.Size(200, 20)
        Me.txtGroup.TabIndex = 4
        '
        'txtCon
        '
        Me.txtCon.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtCon.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtCon.Location = New System.Drawing.Point(136, 173)
        Me.txtCon.Name = "txtCon"
        Me.txtCon.Size = New System.Drawing.Size(200, 20)
        Me.txtCon.TabIndex = 7
        '
        'txtLen
        '
        Me.txtLen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtLen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtLen.Location = New System.Drawing.Point(136, 199)
        Me.txtLen.Name = "txtLen"
        Me.txtLen.Size = New System.Drawing.Size(200, 20)
        Me.txtLen.TabIndex = 8
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(19, 240)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(317, 84)
        Me.txtNotes.TabIndex = 9
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(19, 332)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 10
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(261, 332)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Loaders_Log_FirearmsTableAdapter
        '
        Me.Loaders_Log_FirearmsTableAdapter.ClearBeforeFill = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 122)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(89, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Number of Shots:"
        '
        'nudShots
        '
        Me.nudShots.Location = New System.Drawing.Point(136, 120)
        Me.nudShots.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudShots.Name = "nudShots"
        Me.nudShots.Size = New System.Drawing.Size(200, 20)
        Me.nudShots.TabIndex = 5
        Me.nudShots.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(16, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Yards tested at:"
        '
        'nudYards
        '
        Me.nudYards.Location = New System.Drawing.Point(136, 66)
        Me.nudYards.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nudYards.Name = "nudYards"
        Me.nudYards.Size = New System.Drawing.Size(200, 20)
        Me.nudYards.TabIndex = 3
        Me.nudYards.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 149)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Configuration Used"
        '
        'cmbConfig
        '
        Me.cmbConfig.DataSource = Me.ConfigListSimpleBindingSource
        Me.cmbConfig.DisplayMember = "ConfigName"
        Me.cmbConfig.FormattingEnabled = True
        Me.cmbConfig.Location = New System.Drawing.Point(136, 146)
        Me.cmbConfig.Name = "cmbConfig"
        Me.cmbConfig.Size = New System.Drawing.Size(200, 21)
        Me.cmbConfig.TabIndex = 6
        Me.cmbConfig.ValueMember = "ID"
        '
        'ConfigListSimpleBindingSource
        '
        Me.ConfigListSimpleBindingSource.DataMember = "ConfigList_Simple"
        Me.ConfigListSimpleBindingSource.DataSource = Me.MLLDataSet
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'ConfigList_SimpleTableAdapter
        '
        Me.ConfigList_SimpleTableAdapter.ClearBeforeFill = True
        '
        'frmAddDataSheet_RiflePistols_CFG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(355, 359)
        Me.Controls.Add(Me.nudYards)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.nudShots)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.txtLen)
        Me.Controls.Add(Me.txtCon)
        Me.Controls.Add(Me.cmbConfig)
        Me.Controls.Add(Me.txtGroup)
        Me.Controls.Add(Me.dtpTested)
        Me.Controls.Add(Me.cmbFirearm)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.HelpProvider1.SetHelpKeyword(Me, "Add to Loaders Log - Rifle & Pistol - Using Configuration")
        Me.HelpProvider1.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.KeywordIndex)
        Me.HelpProvider1.SetHelpString(Me, "Add to Loaders Log - Rifle & Pistol - Using Configuration")
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddDataSheet_RiflePistols_CFG"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.Text = "Add To Loaders Log using a Configuration"
        CType(Me.LoadersLogFirearmsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudShots, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudYards, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ConfigListSimpleBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbFirearm As System.Windows.Forms.ComboBox
    Friend WithEvents dtpTested As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtGroup As System.Windows.Forms.TextBox
    Friend WithEvents txtCon As System.Windows.Forms.TextBox
    Friend WithEvents txtLen As System.Windows.Forms.TextBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents LoadersLogFirearmsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Loaders_Log_FirearmsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.Loaders_Log_FirearmsTableAdapter
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents nudShots As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents nudYards As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbConfig As System.Windows.Forms.ComboBox
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents ConfigListSimpleBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ConfigList_SimpleTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.ConfigList_SimpleTableAdapter
End Class
