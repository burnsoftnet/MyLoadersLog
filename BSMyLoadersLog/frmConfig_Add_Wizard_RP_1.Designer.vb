<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig_Add_Wizard_RP_1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig_Add_Wizard_RP_1))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbBullet = New System.Windows.Forms.ComboBox
        Me.ListBulletsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.cmbPrimer = New System.Windows.Forms.ComboBox
        Me.GeneralPrimerBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cmbCase = New System.Windows.Forms.ComboBox
        Me.ListCaseBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.chkPersonal = New System.Windows.Forms.CheckBox
        Me.chkBook = New System.Windows.Forms.CheckBox
        Me.txtLoad = New System.Windows.Forms.TextBox
        Me.List_BulletsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_BulletsTableAdapter
        Me.General_PrimerTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.General_PrimerTableAdapter
        Me.List_CaseTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_CaseTableAdapter
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.cmbAmmo = New System.Windows.Forms.ComboBox
        Me.GeneralAmmunitionTypeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label6 = New System.Windows.Forms.Label
        Me.General_Ammunition_TypeTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.General_Ammunition_TypeTableAdapter
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        CType(Me.ListBulletsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GeneralPrimerBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListCaseBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GeneralAmmunitionTypeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bullet:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Primer:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Case:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Load Source:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 119)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Load Type:"
        '
        'cmbBullet
        '
        Me.cmbBullet.DataSource = Me.ListBulletsBindingSource
        Me.cmbBullet.DisplayMember = "Name"
        Me.cmbBullet.FormattingEnabled = True
        Me.cmbBullet.Location = New System.Drawing.Point(103, 36)
        Me.cmbBullet.Name = "cmbBullet"
        Me.cmbBullet.Size = New System.Drawing.Size(172, 21)
        Me.cmbBullet.TabIndex = 5
        Me.cmbBullet.ValueMember = "ID"
        '
        'ListBulletsBindingSource
        '
        Me.ListBulletsBindingSource.DataMember = "List_Bullets"
        Me.ListBulletsBindingSource.DataSource = Me.MLLDataSet
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cmbPrimer
        '
        Me.cmbPrimer.DataSource = Me.GeneralPrimerBindingSource
        Me.cmbPrimer.DisplayMember = "Name"
        Me.cmbPrimer.FormattingEnabled = True
        Me.cmbPrimer.Location = New System.Drawing.Point(103, 63)
        Me.cmbPrimer.Name = "cmbPrimer"
        Me.cmbPrimer.Size = New System.Drawing.Size(172, 21)
        Me.cmbPrimer.TabIndex = 6
        Me.cmbPrimer.ValueMember = "ID"
        '
        'GeneralPrimerBindingSource
        '
        Me.GeneralPrimerBindingSource.DataMember = "General_Primer"
        Me.GeneralPrimerBindingSource.DataSource = Me.MLLDataSet
        '
        'cmbCase
        '
        Me.cmbCase.DataSource = Me.ListCaseBindingSource
        Me.cmbCase.DisplayMember = "Name"
        Me.cmbCase.FormattingEnabled = True
        Me.cmbCase.Location = New System.Drawing.Point(103, 90)
        Me.cmbCase.Name = "cmbCase"
        Me.cmbCase.Size = New System.Drawing.Size(172, 21)
        Me.cmbCase.TabIndex = 7
        Me.cmbCase.ValueMember = "ID"
        '
        'ListCaseBindingSource
        '
        Me.ListCaseBindingSource.DataMember = "List_Case"
        Me.ListCaseBindingSource.DataSource = Me.MLLDataSet
        '
        'chkPersonal
        '
        Me.chkPersonal.AutoSize = True
        Me.chkPersonal.Location = New System.Drawing.Point(103, 118)
        Me.chkPersonal.Name = "chkPersonal"
        Me.chkPersonal.Size = New System.Drawing.Size(67, 17)
        Me.chkPersonal.TabIndex = 8
        Me.chkPersonal.Text = "Personal"
        Me.chkPersonal.UseVisualStyleBackColor = True
        '
        'chkBook
        '
        Me.chkBook.AutoSize = True
        Me.chkBook.Location = New System.Drawing.Point(188, 118)
        Me.chkBook.Name = "chkBook"
        Me.chkBook.Size = New System.Drawing.Size(86, 17)
        Me.chkBook.TabIndex = 9
        Me.chkBook.Text = "From a Book"
        Me.chkBook.UseVisualStyleBackColor = True
        '
        'txtLoad
        '
        Me.txtLoad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtLoad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtLoad.Location = New System.Drawing.Point(103, 142)
        Me.txtLoad.Name = "txtLoad"
        Me.txtLoad.Size = New System.Drawing.Size(171, 20)
        Me.txtLoad.TabIndex = 10
        '
        'List_BulletsTableAdapter
        '
        Me.List_BulletsTableAdapter.ClearBeforeFill = True
        '
        'General_PrimerTableAdapter
        '
        Me.General_PrimerTableAdapter.ClearBeforeFill = True
        '
        'List_CaseTableAdapter
        '
        Me.List_CaseTableAdapter.ClearBeforeFill = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(44, 180)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 11
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(188, 180)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'cmbAmmo
        '
        Me.cmbAmmo.DataSource = Me.GeneralAmmunitionTypeBindingSource
        Me.cmbAmmo.DisplayMember = "FType"
        Me.cmbAmmo.FormattingEnabled = True
        Me.cmbAmmo.Location = New System.Drawing.Point(103, 9)
        Me.cmbAmmo.Name = "cmbAmmo"
        Me.cmbAmmo.Size = New System.Drawing.Size(171, 21)
        Me.cmbAmmo.TabIndex = 13
        Me.cmbAmmo.ValueMember = "ID"
        '
        'GeneralAmmunitionTypeBindingSource
        '
        Me.GeneralAmmunitionTypeBindingSource.DataMember = "General_Ammunition_Type"
        Me.GeneralAmmunitionTypeBindingSource.DataSource = Me.MLLDataSet
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Ammo Type:"
        '
        'General_Ammunition_TypeTableAdapter
        '
        Me.General_Ammunition_TypeTableAdapter.ClearBeforeFill = True
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'frmConfig_Add_Wizard_RP_1
        '
        Me.AcceptButton = Me.btnAdd
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(291, 214)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbAmmo)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtLoad)
        Me.Controls.Add(Me.chkBook)
        Me.Controls.Add(Me.chkPersonal)
        Me.Controls.Add(Me.cmbCase)
        Me.Controls.Add(Me.cmbPrimer)
        Me.Controls.Add(Me.cmbBullet)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.HelpProvider1.SetHelpKeyword(Me, "Adding a New Configuration")
        Me.HelpProvider1.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.KeywordIndex)
        Me.HelpProvider1.SetHelpString(Me, "Adding a New Configuration")
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfig_Add_Wizard_RP_1"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Configuration for Pistols and Rifles"
        CType(Me.ListBulletsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GeneralPrimerBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListCaseBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GeneralAmmunitionTypeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbBullet As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPrimer As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCase As System.Windows.Forms.ComboBox
    Friend WithEvents chkPersonal As System.Windows.Forms.CheckBox
    Friend WithEvents chkBook As System.Windows.Forms.CheckBox
    Friend WithEvents txtLoad As System.Windows.Forms.TextBox
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents ListBulletsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_BulletsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_BulletsTableAdapter
    Friend WithEvents GeneralPrimerBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents General_PrimerTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.General_PrimerTableAdapter
    Friend WithEvents ListCaseBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_CaseTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_CaseTableAdapter
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cmbAmmo As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GeneralAmmunitionTypeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents General_Ammunition_TypeTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.General_Ammunition_TypeTableAdapter
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
End Class
