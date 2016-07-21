<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddDataSheet_RiflePistols_MAN
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddDataSheet_RiflePistols_MAN))
        Me.nudYards = New System.Windows.Forms.NumericUpDown
        Me.Label9 = New System.Windows.Forms.Label
        Me.nudShots = New System.Windows.Forms.NumericUpDown
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.txtNotes = New System.Windows.Forms.TextBox
        Me.txtLen = New System.Windows.Forms.TextBox
        Me.txtCon = New System.Windows.Forms.TextBox
        Me.txtGroup = New System.Windows.Forms.TextBox
        Me.dtpTested = New System.Windows.Forms.DateTimePicker
        Me.cmbFirearm = New System.Windows.Forms.ComboBox
        Me.LoadersLogFirearmsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Loaders_Log_FirearmsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.Loaders_Log_FirearmsTableAdapter
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtPowName = New System.Windows.Forms.TextBox
        Me.txtPowWei = New System.Windows.Forms.TextBox
        Me.txtPowManu = New System.Windows.Forms.TextBox
        Me.txtBullet = New System.Windows.Forms.TextBox
        Me.txtPrimer = New System.Windows.Forms.TextBox
        Me.txtCase = New System.Windows.Forms.TextBox
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        CType(Me.nudYards, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudShots, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoadersLogFirearmsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'nudYards
        '
        Me.nudYards.Location = New System.Drawing.Point(132, 69)
        Me.nudYards.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nudYards.Name = "nudYards"
        Me.nudYards.Size = New System.Drawing.Size(200, 20)
        Me.nudYards.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 71)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "Yards tested at:"
        '
        'nudShots
        '
        Me.nudShots.Location = New System.Drawing.Point(132, 123)
        Me.nudShots.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudShots.Name = "nudShots"
        Me.nudShots.Size = New System.Drawing.Size(200, 20)
        Me.nudShots.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 125)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(89, 13)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "Number of Shots:"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(257, 463)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 16
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(15, 463)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 15
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(15, 371)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(317, 84)
        Me.txtNotes.TabIndex = 14
        '
        'txtLen
        '
        Me.txtLen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtLen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtLen.Location = New System.Drawing.Point(132, 330)
        Me.txtLen.Name = "txtLen"
        Me.txtLen.Size = New System.Drawing.Size(200, 20)
        Me.txtLen.TabIndex = 13
        '
        'txtCon
        '
        Me.txtCon.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtCon.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtCon.Location = New System.Drawing.Point(132, 304)
        Me.txtCon.Name = "txtCon"
        Me.txtCon.Size = New System.Drawing.Size(200, 20)
        Me.txtCon.TabIndex = 12
        '
        'txtGroup
        '
        Me.txtGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtGroup.Location = New System.Drawing.Point(132, 95)
        Me.txtGroup.Name = "txtGroup"
        Me.txtGroup.Size = New System.Drawing.Size(200, 20)
        Me.txtGroup.TabIndex = 4
        '
        'dtpTested
        '
        Me.dtpTested.Location = New System.Drawing.Point(132, 42)
        Me.dtpTested.Name = "dtpTested"
        Me.dtpTested.Size = New System.Drawing.Size(200, 20)
        Me.dtpTested.TabIndex = 2
        '
        'cmbFirearm
        '
        Me.cmbFirearm.DataSource = Me.LoadersLogFirearmsBindingSource
        Me.cmbFirearm.DisplayMember = "FullName"
        Me.cmbFirearm.FormattingEnabled = True
        Me.cmbFirearm.Location = New System.Drawing.Point(132, 15)
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
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 355)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Notes"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 333)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Total Length:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 307)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Conditions:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 13)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Group size or Score:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Date tested:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Firearm tested with:"
        '
        'Loaders_Log_FirearmsTableAdapter
        '
        Me.Loaders_Log_FirearmsTableAdapter.ClearBeforeFill = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Powder Name:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 177)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(123, 13)
        Me.Label10.TabIndex = 41
        Me.Label10.Text = "Weight of Powder Used:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 203)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(112, 13)
        Me.Label11.TabIndex = 42
        Me.Label11.Text = "Powder Manufacturer:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 229)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(36, 13)
        Me.Label12.TabIndex = 43
        Me.Label12.Text = "Bullet:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(12, 255)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(39, 13)
        Me.Label13.TabIndex = 44
        Me.Label13.Text = "Primer:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(12, 281)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(34, 13)
        Me.Label14.TabIndex = 45
        Me.Label14.Text = "Case:"
        '
        'txtPowName
        '
        Me.txtPowName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtPowName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtPowName.Location = New System.Drawing.Point(132, 149)
        Me.txtPowName.Name = "txtPowName"
        Me.txtPowName.Size = New System.Drawing.Size(200, 20)
        Me.txtPowName.TabIndex = 6
        '
        'txtPowWei
        '
        Me.txtPowWei.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtPowWei.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtPowWei.Location = New System.Drawing.Point(132, 174)
        Me.txtPowWei.Name = "txtPowWei"
        Me.txtPowWei.Size = New System.Drawing.Size(200, 20)
        Me.txtPowWei.TabIndex = 7
        '
        'txtPowManu
        '
        Me.txtPowManu.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtPowManu.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtPowManu.Location = New System.Drawing.Point(132, 200)
        Me.txtPowManu.Name = "txtPowManu"
        Me.txtPowManu.Size = New System.Drawing.Size(200, 20)
        Me.txtPowManu.TabIndex = 8
        '
        'txtBullet
        '
        Me.txtBullet.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtBullet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtBullet.Location = New System.Drawing.Point(132, 226)
        Me.txtBullet.Name = "txtBullet"
        Me.txtBullet.Size = New System.Drawing.Size(200, 20)
        Me.txtBullet.TabIndex = 9
        '
        'txtPrimer
        '
        Me.txtPrimer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtPrimer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtPrimer.Location = New System.Drawing.Point(132, 252)
        Me.txtPrimer.Name = "txtPrimer"
        Me.txtPrimer.Size = New System.Drawing.Size(200, 20)
        Me.txtPrimer.TabIndex = 10
        '
        'txtCase
        '
        Me.txtCase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtCase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtCase.Location = New System.Drawing.Point(132, 278)
        Me.txtCase.Name = "txtCase"
        Me.txtCase.Size = New System.Drawing.Size(200, 20)
        Me.txtCase.TabIndex = 11
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'frmAddDataSheet_RiflePistols_MAN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(349, 495)
        Me.Controls.Add(Me.txtCase)
        Me.Controls.Add(Me.txtPrimer)
        Me.Controls.Add(Me.txtBullet)
        Me.Controls.Add(Me.txtPowManu)
        Me.Controls.Add(Me.txtPowWei)
        Me.Controls.Add(Me.txtPowName)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.nudYards)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.nudShots)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.txtLen)
        Me.Controls.Add(Me.txtCon)
        Me.Controls.Add(Me.txtGroup)
        Me.Controls.Add(Me.dtpTested)
        Me.Controls.Add(Me.cmbFirearm)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.HelpProvider1.SetHelpKeyword(Me, "Add to Loaders Log - Rifle & Pistol - Manual")
        Me.HelpProvider1.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.KeywordIndex)
        Me.HelpProvider1.SetHelpString(Me, "Add to Loaders Log - Rifle & Pistol - Manual")
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddDataSheet_RiflePistols_MAN"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.Text = "Add To Loaders Log "
        CType(Me.nudYards, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudShots, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoadersLogFirearmsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents nudYards As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents nudShots As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents txtLen As System.Windows.Forms.TextBox
    Friend WithEvents txtCon As System.Windows.Forms.TextBox
    Friend WithEvents txtGroup As System.Windows.Forms.TextBox
    Friend WithEvents dtpTested As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbFirearm As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents LoadersLogFirearmsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Loaders_Log_FirearmsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.Loaders_Log_FirearmsTableAdapter
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtPowName As System.Windows.Forms.TextBox
    Friend WithEvents txtPowWei As System.Windows.Forms.TextBox
    Friend WithEvents txtPowManu As System.Windows.Forms.TextBox
    Friend WithEvents txtBullet As System.Windows.Forms.TextBox
    Friend WithEvents txtPrimer As System.Windows.Forms.TextBox
    Friend WithEvents txtCase As System.Windows.Forms.TextBox
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
End Class
