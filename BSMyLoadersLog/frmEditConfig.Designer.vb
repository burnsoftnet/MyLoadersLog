<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditConfig))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btnSaveGD = New System.Windows.Forms.Button
        Me.cmbCal = New System.Windows.Forms.ComboBox
        Me.ListCalibersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.chkShotgun = New System.Windows.Forms.CheckBox
        Me.chkRP = New System.Windows.Forms.CheckBox
        Me.txtConfigID = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btnSaveRPDetails = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmbAmmo = New System.Windows.Forms.ComboBox
        Me.GeneralAmmunitionTypeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.txtLoad = New System.Windows.Forms.TextBox
        Me.chkBook = New System.Windows.Forms.CheckBox
        Me.chkPersonal = New System.Windows.Forms.CheckBox
        Me.cmbCase = New System.Windows.Forms.ComboBox
        Me.ListCaseBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cmbPrimer = New System.Windows.Forms.ComboBox
        Me.GeneralPrimerBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cmbBullet = New System.Windows.Forms.ComboBox
        Me.ListBulletsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.cmbSlug = New System.Windows.Forms.ComboBox
        Me.ListSGShotTypeDetailsBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label29 = New System.Windows.Forms.Label
        Me.cmdShotType = New System.Windows.Forms.ComboBox
        Me.ListSGShotTypeDetailsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtShotCharge = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.cmbLoadType = New System.Windows.Forms.ComboBox
        Me.ListSGShotChargeLoadsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label26 = New System.Windows.Forms.Label
        Me.cmdHull = New System.Windows.Forms.ComboBox
        Me.ListSGCaseBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cmdWAD = New System.Windows.Forms.ComboBox
        Me.ListSGWADBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cmbSGPrimer = New System.Windows.Forms.ComboBox
        Me.ViewPrimerListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtSourceSG = New System.Windows.Forms.TextBox
        Me.chkBookSG = New System.Windows.Forms.CheckBox
        Me.chkPersonalSG = New System.Windows.Forms.CheckBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.btnSavePowder = New System.Windows.Forms.Button
        Me.chkPrefPowder = New System.Windows.Forms.CheckBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtCUPSMax = New System.Windows.Forms.TextBox
        Me.txtCUPSMid = New System.Windows.Forms.TextBox
        Me.txtCUPSMin = New System.Windows.Forms.TextBox
        Me.txtMVMax = New System.Windows.Forms.TextBox
        Me.txtMVMid = New System.Windows.Forms.TextBox
        Me.txtMVMin = New System.Windows.Forms.TextBox
        Me.txtLMax = New System.Windows.Forms.TextBox
        Me.txtLMid = New System.Windows.Forms.TextBox
        Me.txtLMin = New System.Windows.Forms.TextBox
        Me.cmbPowder = New System.Windows.Forms.ComboBox
        Me.ConfigListPowderDataNSGViewBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.btnSavePowderSG = New System.Windows.Forms.Button
        Me.chkDefaultPowderSG = New System.Windows.Forms.CheckBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.txtPSI = New System.Windows.Forms.TextBox
        Me.txtFPS = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtCharge = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.cmbPowderSG = New System.Windows.Forms.ComboBox
        Me.ConfigListPowderDataSGViewBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnSaveAll = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        Me.GeneralPrimerBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.List_CalibersTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_CalibersTableAdapter
        Me.General_Ammunition_TypeTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.General_Ammunition_TypeTableAdapter
        Me.List_BulletsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_BulletsTableAdapter
        Me.General_PrimerTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.General_PrimerTableAdapter
        Me.List_CaseTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_CaseTableAdapter
        Me.Config_List_Powder_Data_NSG_ViewTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.Config_List_Powder_Data_NSG_ViewTableAdapter
        Me.List_SG_CaseTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_CaseTableAdapter
        Me.List_SG_WADTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_WADTableAdapter
        Me.List_SG_ShotCharge_LoadsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotCharge_LoadsTableAdapter
        Me.Config_List_Powder_Data_SG_ViewTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.Config_List_Powder_Data_SG_ViewTableAdapter
        Me.List_SG_ShotType_DetailsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotType_DetailsTableAdapter
        Me.ViewPrimerListTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.viewPrimerListTableAdapter
        Me.btnSaveShotgunDetails = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.ListCalibersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.GeneralAmmunitionTypeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListCaseBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GeneralPrimerBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListBulletsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.ListSGShotTypeDetailsBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListSGShotTypeDetailsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListSGShotChargeLoadsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListSGCaseBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListSGWADBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewPrimerListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.ConfigListPowderDataNSGViewBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.ConfigListPowderDataSGViewBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GeneralPrimerBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Location = New System.Drawing.Point(1, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(358, 352)
        Me.TabControl1.TabIndex = 15
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnSaveGD)
        Me.TabPage1.Controls.Add(Me.cmbCal)
        Me.TabPage1.Controls.Add(Me.chkShotgun)
        Me.TabPage1.Controls.Add(Me.chkRP)
        Me.TabPage1.Controls.Add(Me.txtConfigID)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(350, 326)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General Details"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnSaveGD
        '
        Me.btnSaveGD.Location = New System.Drawing.Point(110, 122)
        Me.btnSaveGD.Name = "btnSaveGD"
        Me.btnSaveGD.Size = New System.Drawing.Size(118, 23)
        Me.btnSaveGD.TabIndex = 22
        Me.btnSaveGD.Text = "Save General Details"
        Me.btnSaveGD.UseVisualStyleBackColor = True
        '
        'cmbCal
        '
        Me.cmbCal.DataSource = Me.ListCalibersBindingSource
        Me.cmbCal.DisplayMember = "Cal"
        Me.cmbCal.FormattingEnabled = True
        Me.cmbCal.Location = New System.Drawing.Point(99, 70)
        Me.cmbCal.Name = "cmbCal"
        Me.cmbCal.Size = New System.Drawing.Size(195, 21)
        Me.cmbCal.TabIndex = 21
        Me.cmbCal.ValueMember = "ID"
        '
        'ListCalibersBindingSource
        '
        Me.ListCalibersBindingSource.DataMember = "List_Calibers"
        Me.ListCalibersBindingSource.DataSource = Me.MLLDataSet
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'chkShotgun
        '
        Me.chkShotgun.AutoSize = True
        Me.chkShotgun.Location = New System.Drawing.Point(195, 47)
        Me.chkShotgun.Name = "chkShotgun"
        Me.chkShotgun.Size = New System.Drawing.Size(66, 17)
        Me.chkShotgun.TabIndex = 20
        Me.chkShotgun.Text = "Shotgun"
        Me.chkShotgun.UseVisualStyleBackColor = True
        '
        'chkRP
        '
        Me.chkRP.AutoSize = True
        Me.chkRP.Location = New System.Drawing.Point(99, 47)
        Me.chkRP.Name = "chkRP"
        Me.chkRP.Size = New System.Drawing.Size(77, 17)
        Me.chkRP.TabIndex = 19
        Me.chkRP.Text = "Rifle/Pistol"
        Me.chkRP.UseVisualStyleBackColor = True
        '
        'txtConfigID
        '
        Me.txtConfigID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtConfigID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtConfigID.Location = New System.Drawing.Point(99, 20)
        Me.txtConfigID.Name = "txtConfigID"
        Me.txtConfigID.Size = New System.Drawing.Size(195, 20)
        Me.txtConfigID.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Caliber:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Type:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Configuration ID:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnSaveRPDetails)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.cmbAmmo)
        Me.TabPage2.Controls.Add(Me.txtLoad)
        Me.TabPage2.Controls.Add(Me.chkBook)
        Me.TabPage2.Controls.Add(Me.chkPersonal)
        Me.TabPage2.Controls.Add(Me.cmbCase)
        Me.TabPage2.Controls.Add(Me.cmbPrimer)
        Me.TabPage2.Controls.Add(Me.cmbBullet)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(350, 326)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Rifle and Pistol Details"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnSaveRPDetails
        '
        Me.btnSaveRPDetails.Location = New System.Drawing.Point(98, 192)
        Me.btnSaveRPDetails.Name = "btnSaveRPDetails"
        Me.btnSaveRPDetails.Size = New System.Drawing.Size(146, 23)
        Me.btnSaveRPDetails.TabIndex = 28
        Me.btnSaveRPDetails.Text = "Save Rifle/Pistol Details"
        Me.btnSaveRPDetails.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "Ammo Type:"
        '
        'cmbAmmo
        '
        Me.cmbAmmo.DataSource = Me.GeneralAmmunitionTypeBindingSource
        Me.cmbAmmo.DisplayMember = "FType"
        Me.cmbAmmo.FormattingEnabled = True
        Me.cmbAmmo.Location = New System.Drawing.Point(98, 15)
        Me.cmbAmmo.Name = "cmbAmmo"
        Me.cmbAmmo.Size = New System.Drawing.Size(171, 21)
        Me.cmbAmmo.TabIndex = 26
        Me.cmbAmmo.ValueMember = "ID"
        '
        'GeneralAmmunitionTypeBindingSource
        '
        Me.GeneralAmmunitionTypeBindingSource.DataMember = "General_Ammunition_Type"
        Me.GeneralAmmunitionTypeBindingSource.DataSource = Me.MLLDataSet
        '
        'txtLoad
        '
        Me.txtLoad.Location = New System.Drawing.Point(98, 148)
        Me.txtLoad.Name = "txtLoad"
        Me.txtLoad.Size = New System.Drawing.Size(171, 20)
        Me.txtLoad.TabIndex = 25
        '
        'chkBook
        '
        Me.chkBook.AutoSize = True
        Me.chkBook.Location = New System.Drawing.Point(183, 124)
        Me.chkBook.Name = "chkBook"
        Me.chkBook.Size = New System.Drawing.Size(86, 17)
        Me.chkBook.TabIndex = 24
        Me.chkBook.Text = "From a Book"
        Me.chkBook.UseVisualStyleBackColor = True
        '
        'chkPersonal
        '
        Me.chkPersonal.AutoSize = True
        Me.chkPersonal.Location = New System.Drawing.Point(98, 124)
        Me.chkPersonal.Name = "chkPersonal"
        Me.chkPersonal.Size = New System.Drawing.Size(67, 17)
        Me.chkPersonal.TabIndex = 23
        Me.chkPersonal.Text = "Personal"
        Me.chkPersonal.UseVisualStyleBackColor = True
        '
        'cmbCase
        '
        Me.cmbCase.DataSource = Me.ListCaseBindingSource
        Me.cmbCase.DisplayMember = "Name"
        Me.cmbCase.FormattingEnabled = True
        Me.cmbCase.Location = New System.Drawing.Point(98, 96)
        Me.cmbCase.Name = "cmbCase"
        Me.cmbCase.Size = New System.Drawing.Size(172, 21)
        Me.cmbCase.TabIndex = 22
        Me.cmbCase.ValueMember = "ID"
        '
        'ListCaseBindingSource
        '
        Me.ListCaseBindingSource.DataMember = "List_Case"
        Me.ListCaseBindingSource.DataSource = Me.MLLDataSet
        '
        'cmbPrimer
        '
        Me.cmbPrimer.DataSource = Me.GeneralPrimerBindingSource
        Me.cmbPrimer.DisplayMember = "Name"
        Me.cmbPrimer.FormattingEnabled = True
        Me.cmbPrimer.Location = New System.Drawing.Point(98, 69)
        Me.cmbPrimer.Name = "cmbPrimer"
        Me.cmbPrimer.Size = New System.Drawing.Size(172, 21)
        Me.cmbPrimer.TabIndex = 21
        Me.cmbPrimer.ValueMember = "ID"
        '
        'GeneralPrimerBindingSource
        '
        Me.GeneralPrimerBindingSource.DataMember = "General_Primer"
        Me.GeneralPrimerBindingSource.DataSource = Me.MLLDataSet
        '
        'cmbBullet
        '
        Me.cmbBullet.DataSource = Me.ListBulletsBindingSource
        Me.cmbBullet.DisplayMember = "Name"
        Me.cmbBullet.FormattingEnabled = True
        Me.cmbBullet.Location = New System.Drawing.Point(98, 42)
        Me.cmbBullet.Name = "cmbBullet"
        Me.cmbBullet.Size = New System.Drawing.Size(172, 21)
        Me.cmbBullet.TabIndex = 20
        Me.cmbBullet.ValueMember = "ID"
        '
        'ListBulletsBindingSource
        '
        Me.ListBulletsBindingSource.DataMember = "List_Bullets"
        Me.ListBulletsBindingSource.DataSource = Me.MLLDataSet
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 125)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Load Type:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 151)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Load Source:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 99)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Case:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Primer:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 45)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(36, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Bullet:"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btnSaveShotgunDetails)
        Me.TabPage3.Controls.Add(Me.cmbSlug)
        Me.TabPage3.Controls.Add(Me.Label29)
        Me.TabPage3.Controls.Add(Me.cmdShotType)
        Me.TabPage3.Controls.Add(Me.Label27)
        Me.TabPage3.Controls.Add(Me.txtShotCharge)
        Me.TabPage3.Controls.Add(Me.Label28)
        Me.TabPage3.Controls.Add(Me.cmbLoadType)
        Me.TabPage3.Controls.Add(Me.Label26)
        Me.TabPage3.Controls.Add(Me.cmdHull)
        Me.TabPage3.Controls.Add(Me.cmdWAD)
        Me.TabPage3.Controls.Add(Me.cmbSGPrimer)
        Me.TabPage3.Controls.Add(Me.Label23)
        Me.TabPage3.Controls.Add(Me.Label24)
        Me.TabPage3.Controls.Add(Me.Label25)
        Me.TabPage3.Controls.Add(Me.txtSourceSG)
        Me.TabPage3.Controls.Add(Me.chkBookSG)
        Me.TabPage3.Controls.Add(Me.chkPersonalSG)
        Me.TabPage3.Controls.Add(Me.Label21)
        Me.TabPage3.Controls.Add(Me.Label22)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(350, 326)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Shotgun Details"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'cmbSlug
        '
        Me.cmbSlug.DataSource = Me.ListSGShotTypeDetailsBindingSource1
        Me.cmbSlug.DisplayMember = "Name"
        Me.cmbSlug.FormattingEnabled = True
        Me.cmbSlug.Location = New System.Drawing.Point(99, 228)
        Me.cmbSlug.Name = "cmbSlug"
        Me.cmbSlug.Size = New System.Drawing.Size(235, 21)
        Me.cmbSlug.TabIndex = 47
        Me.cmbSlug.ValueMember = "ID"
        '
        'ListSGShotTypeDetailsBindingSource1
        '
        Me.ListSGShotTypeDetailsBindingSource1.DataMember = "List_SG_ShotType_Details"
        Me.ListSGShotTypeDetailsBindingSource1.DataSource = Me.MLLDataSet
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(7, 232)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(58, 13)
        Me.Label29.TabIndex = 46
        Me.Label29.Text = "Slug Type:"
        '
        'cmdShotType
        '
        Me.cmdShotType.DataSource = Me.ListSGShotTypeDetailsBindingSource
        Me.cmdShotType.DisplayMember = "Name"
        Me.cmdShotType.FormattingEnabled = True
        Me.cmdShotType.Location = New System.Drawing.Point(99, 175)
        Me.cmdShotType.Name = "cmdShotType"
        Me.cmdShotType.Size = New System.Drawing.Size(235, 21)
        Me.cmdShotType.TabIndex = 45
        Me.cmdShotType.ValueMember = "ID"
        '
        'ListSGShotTypeDetailsBindingSource
        '
        Me.ListSGShotTypeDetailsBindingSource.DataMember = "List_SG_ShotType_Details"
        Me.ListSGShotTypeDetailsBindingSource.DataSource = Me.MLLDataSet
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(7, 178)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(59, 13)
        Me.Label27.TabIndex = 44
        Me.Label27.Text = "Shot Type:"
        '
        'txtShotCharge
        '
        Me.txtShotCharge.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtShotCharge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtShotCharge.Location = New System.Drawing.Point(99, 202)
        Me.txtShotCharge.Name = "txtShotCharge"
        Me.txtShotCharge.Size = New System.Drawing.Size(235, 20)
        Me.txtShotCharge.TabIndex = 40
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(7, 205)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(89, 13)
        Me.Label28.TabIndex = 39
        Me.Label28.Text = "Shot Charge (oz):"
        '
        'cmbLoadType
        '
        Me.cmbLoadType.DataSource = Me.ListSGShotChargeLoadsBindingSource
        Me.cmbLoadType.DisplayMember = "Name"
        Me.cmbLoadType.FormattingEnabled = True
        Me.cmbLoadType.Location = New System.Drawing.Point(99, 148)
        Me.cmbLoadType.Name = "cmbLoadType"
        Me.cmbLoadType.Size = New System.Drawing.Size(235, 21)
        Me.cmbLoadType.TabIndex = 38
        Me.cmbLoadType.ValueMember = "ID"
        '
        'ListSGShotChargeLoadsBindingSource
        '
        Me.ListSGShotChargeLoadsBindingSource.DataMember = "List_SG_ShotCharge_Loads"
        Me.ListSGShotChargeLoadsBindingSource.DataSource = Me.MLLDataSet
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(7, 151)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(61, 13)
        Me.Label26.TabIndex = 37
        Me.Label26.Text = "Load Type:"
        '
        'cmdHull
        '
        Me.cmdHull.DataSource = Me.ListSGCaseBindingSource
        Me.cmdHull.DisplayMember = "Name"
        Me.cmdHull.FormattingEnabled = True
        Me.cmdHull.Location = New System.Drawing.Point(99, 94)
        Me.cmdHull.Name = "cmdHull"
        Me.cmdHull.Size = New System.Drawing.Size(235, 21)
        Me.cmdHull.TabIndex = 36
        Me.cmdHull.ValueMember = "ID"
        '
        'ListSGCaseBindingSource
        '
        Me.ListSGCaseBindingSource.DataMember = "List_SG_Case"
        Me.ListSGCaseBindingSource.DataSource = Me.MLLDataSet
        '
        'cmdWAD
        '
        Me.cmdWAD.DataSource = Me.ListSGWADBindingSource
        Me.cmdWAD.DisplayMember = "WAD"
        Me.cmdWAD.FormattingEnabled = True
        Me.cmdWAD.Location = New System.Drawing.Point(99, 121)
        Me.cmdWAD.Name = "cmdWAD"
        Me.cmdWAD.Size = New System.Drawing.Size(235, 21)
        Me.cmdWAD.TabIndex = 35
        Me.cmdWAD.ValueMember = "ID"
        '
        'ListSGWADBindingSource
        '
        Me.ListSGWADBindingSource.DataMember = "List_SG_WAD"
        Me.ListSGWADBindingSource.DataSource = Me.MLLDataSet
        '
        'cmbSGPrimer
        '
        Me.cmbSGPrimer.DataSource = Me.ViewPrimerListBindingSource
        Me.cmbSGPrimer.DisplayMember = "PrimerName"
        Me.cmbSGPrimer.FormattingEnabled = True
        Me.cmbSGPrimer.Location = New System.Drawing.Point(99, 69)
        Me.cmbSGPrimer.Name = "cmbSGPrimer"
        Me.cmbSGPrimer.Size = New System.Drawing.Size(235, 21)
        Me.cmbSGPrimer.TabIndex = 34
        Me.cmbSGPrimer.ValueMember = "ID"
        '
        'ViewPrimerListBindingSource
        '
        Me.ViewPrimerListBindingSource.DataMember = "viewPrimerList"
        Me.ViewPrimerListBindingSource.DataSource = Me.MLLDataSet
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(7, 124)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(36, 13)
        Me.Label23.TabIndex = 33
        Me.Label23.Text = "WAD:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(7, 97)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(28, 13)
        Me.Label24.TabIndex = 32
        Me.Label24.Text = "Hull:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(7, 72)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(39, 13)
        Me.Label25.TabIndex = 31
        Me.Label25.Text = "Primer:"
        '
        'txtSourceSG
        '
        Me.txtSourceSG.Location = New System.Drawing.Point(99, 43)
        Me.txtSourceSG.Name = "txtSourceSG"
        Me.txtSourceSG.Size = New System.Drawing.Size(235, 20)
        Me.txtSourceSG.TabIndex = 30
        '
        'chkBookSG
        '
        Me.chkBookSG.AutoSize = True
        Me.chkBookSG.Location = New System.Drawing.Point(184, 19)
        Me.chkBookSG.Name = "chkBookSG"
        Me.chkBookSG.Size = New System.Drawing.Size(86, 17)
        Me.chkBookSG.TabIndex = 29
        Me.chkBookSG.Text = "From a Book"
        Me.chkBookSG.UseVisualStyleBackColor = True
        '
        'chkPersonalSG
        '
        Me.chkPersonalSG.AutoSize = True
        Me.chkPersonalSG.Location = New System.Drawing.Point(99, 19)
        Me.chkPersonalSG.Name = "chkPersonalSG"
        Me.chkPersonalSG.Size = New System.Drawing.Size(67, 17)
        Me.chkPersonalSG.TabIndex = 28
        Me.chkPersonalSG.Text = "Personal"
        Me.chkPersonalSG.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(7, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(61, 13)
        Me.Label21.TabIndex = 27
        Me.Label21.Text = "Load Type:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(7, 46)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(71, 13)
        Me.Label22.TabIndex = 26
        Me.Label22.Text = "Load Source:"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.btnSavePowder)
        Me.TabPage4.Controls.Add(Me.chkPrefPowder)
        Me.TabPage4.Controls.Add(Me.Label20)
        Me.TabPage4.Controls.Add(Me.txtCUPSMax)
        Me.TabPage4.Controls.Add(Me.txtCUPSMid)
        Me.TabPage4.Controls.Add(Me.txtCUPSMin)
        Me.TabPage4.Controls.Add(Me.txtMVMax)
        Me.TabPage4.Controls.Add(Me.txtMVMid)
        Me.TabPage4.Controls.Add(Me.txtMVMin)
        Me.TabPage4.Controls.Add(Me.txtLMax)
        Me.TabPage4.Controls.Add(Me.txtLMid)
        Me.TabPage4.Controls.Add(Me.txtLMin)
        Me.TabPage4.Controls.Add(Me.cmbPowder)
        Me.TabPage4.Controls.Add(Me.Label10)
        Me.TabPage4.Controls.Add(Me.Label11)
        Me.TabPage4.Controls.Add(Me.Label12)
        Me.TabPage4.Controls.Add(Me.Label13)
        Me.TabPage4.Controls.Add(Me.Label14)
        Me.TabPage4.Controls.Add(Me.Label15)
        Me.TabPage4.Controls.Add(Me.Label16)
        Me.TabPage4.Controls.Add(Me.Label17)
        Me.TabPage4.Controls.Add(Me.Label18)
        Me.TabPage4.Controls.Add(Me.Label19)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(350, 326)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Powder"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'btnSavePowder
        '
        Me.btnSavePowder.Location = New System.Drawing.Point(118, 300)
        Me.btnSavePowder.Name = "btnSavePowder"
        Me.btnSavePowder.Size = New System.Drawing.Size(94, 23)
        Me.btnSavePowder.TabIndex = 42
        Me.btnSavePowder.Text = "Save Powder"
        Me.btnSavePowder.UseVisualStyleBackColor = True
        '
        'chkPrefPowder
        '
        Me.chkPrefPowder.AutoSize = True
        Me.chkPrefPowder.Location = New System.Drawing.Point(159, 10)
        Me.chkPrefPowder.Name = "chkPrefPowder"
        Me.chkPrefPowder.Size = New System.Drawing.Size(44, 17)
        Me.chkPrefPowder.TabIndex = 41
        Me.chkPrefPowder.Text = "Yes"
        Me.chkPrefPowder.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(21, 10)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(131, 13)
        Me.Label20.TabIndex = 40
        Me.Label20.Text = "Preferred/Default Powder:"
        '
        'txtCUPSMax
        '
        Me.txtCUPSMax.Location = New System.Drawing.Point(143, 268)
        Me.txtCUPSMax.Name = "txtCUPSMax"
        Me.txtCUPSMax.Size = New System.Drawing.Size(166, 20)
        Me.txtCUPSMax.TabIndex = 39
        '
        'txtCUPSMid
        '
        Me.txtCUPSMid.Location = New System.Drawing.Point(143, 242)
        Me.txtCUPSMid.Name = "txtCUPSMid"
        Me.txtCUPSMid.Size = New System.Drawing.Size(166, 20)
        Me.txtCUPSMid.TabIndex = 38
        '
        'txtCUPSMin
        '
        Me.txtCUPSMin.Location = New System.Drawing.Point(143, 218)
        Me.txtCUPSMin.Name = "txtCUPSMin"
        Me.txtCUPSMin.Size = New System.Drawing.Size(166, 20)
        Me.txtCUPSMin.TabIndex = 37
        '
        'txtMVMax
        '
        Me.txtMVMax.Location = New System.Drawing.Point(143, 192)
        Me.txtMVMax.Name = "txtMVMax"
        Me.txtMVMax.Size = New System.Drawing.Size(166, 20)
        Me.txtMVMax.TabIndex = 36
        '
        'txtMVMid
        '
        Me.txtMVMid.Location = New System.Drawing.Point(143, 165)
        Me.txtMVMid.Name = "txtMVMid"
        Me.txtMVMid.Size = New System.Drawing.Size(166, 20)
        Me.txtMVMid.TabIndex = 35
        '
        'txtMVMin
        '
        Me.txtMVMin.Location = New System.Drawing.Point(143, 140)
        Me.txtMVMin.Name = "txtMVMin"
        Me.txtMVMin.Size = New System.Drawing.Size(166, 20)
        Me.txtMVMin.TabIndex = 34
        '
        'txtLMax
        '
        Me.txtLMax.Location = New System.Drawing.Point(143, 114)
        Me.txtLMax.Name = "txtLMax"
        Me.txtLMax.Size = New System.Drawing.Size(166, 20)
        Me.txtLMax.TabIndex = 33
        '
        'txtLMid
        '
        Me.txtLMid.Location = New System.Drawing.Point(143, 88)
        Me.txtLMid.Name = "txtLMid"
        Me.txtLMid.Size = New System.Drawing.Size(166, 20)
        Me.txtLMid.TabIndex = 32
        '
        'txtLMin
        '
        Me.txtLMin.Location = New System.Drawing.Point(143, 62)
        Me.txtLMin.Name = "txtLMin"
        Me.txtLMin.Size = New System.Drawing.Size(166, 20)
        Me.txtLMin.TabIndex = 31
        '
        'cmbPowder
        '
        Me.cmbPowder.DataSource = Me.ConfigListPowderDataNSGViewBindingSource
        Me.cmbPowder.DisplayMember = "Powder_Name"
        Me.cmbPowder.FormattingEnabled = True
        Me.cmbPowder.Location = New System.Drawing.Point(143, 34)
        Me.cmbPowder.Name = "cmbPowder"
        Me.cmbPowder.Size = New System.Drawing.Size(166, 21)
        Me.cmbPowder.TabIndex = 30
        Me.cmbPowder.ValueMember = "ID"
        '
        'ConfigListPowderDataNSGViewBindingSource
        '
        Me.ConfigListPowderDataNSGViewBindingSource.DataMember = "Config_List_Powder_Data_NSG_View"
        Me.ConfigListPowderDataNSGViewBindingSource.DataSource = Me.MLLDataSet
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(22, 271)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(115, 13)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "Max Pressure C.U.P.S:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(21, 245)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(112, 13)
        Me.Label11.TabIndex = 28
        Me.Label11.Text = "Mid Pressure C.U.P.S:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(21, 221)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(112, 13)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Min Pressure C.U.P.S:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(21, 195)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(106, 13)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "Max Muzzle Velocity:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(21, 168)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(103, 13)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "Mid Muzzle Velocity:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(21, 143)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(103, 13)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "Min Muzzle Velocity:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(21, 117)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(57, 13)
        Me.Label16.TabIndex = 23
        Me.Label16.Text = "Load Max:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(21, 91)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(102, 13)
        Me.Label17.TabIndex = 22
        Me.Label17.Text = "Load Mid/Preferred:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(21, 65)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(54, 13)
        Me.Label18.TabIndex = 21
        Me.Label18.Text = "Load Min:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(21, 37)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(46, 13)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "Powder:"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.btnSavePowderSG)
        Me.TabPage5.Controls.Add(Me.chkDefaultPowderSG)
        Me.TabPage5.Controls.Add(Me.Label34)
        Me.TabPage5.Controls.Add(Me.txtPSI)
        Me.TabPage5.Controls.Add(Me.txtFPS)
        Me.TabPage5.Controls.Add(Me.Label30)
        Me.TabPage5.Controls.Add(Me.Label31)
        Me.TabPage5.Controls.Add(Me.txtCharge)
        Me.TabPage5.Controls.Add(Me.Label32)
        Me.TabPage5.Controls.Add(Me.Label33)
        Me.TabPage5.Controls.Add(Me.cmbPowderSG)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(350, 326)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Powder"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'btnSavePowderSG
        '
        Me.btnSavePowderSG.Location = New System.Drawing.Point(114, 172)
        Me.btnSavePowderSG.Name = "btnSavePowderSG"
        Me.btnSavePowderSG.Size = New System.Drawing.Size(94, 23)
        Me.btnSavePowderSG.TabIndex = 44
        Me.btnSavePowderSG.Text = "Save Powder"
        Me.btnSavePowderSG.UseVisualStyleBackColor = True
        '
        'chkDefaultPowderSG
        '
        Me.chkDefaultPowderSG.AutoSize = True
        Me.chkDefaultPowderSG.Location = New System.Drawing.Point(145, 16)
        Me.chkDefaultPowderSG.Name = "chkDefaultPowderSG"
        Me.chkDefaultPowderSG.Size = New System.Drawing.Size(44, 17)
        Me.chkDefaultPowderSG.TabIndex = 43
        Me.chkDefaultPowderSG.Text = "Yes"
        Me.chkDefaultPowderSG.UseVisualStyleBackColor = True
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(7, 17)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(131, 13)
        Me.Label34.TabIndex = 42
        Me.Label34.Text = "Preferred/Default Powder:"
        '
        'txtPSI
        '
        Me.txtPSI.Location = New System.Drawing.Point(145, 115)
        Me.txtPSI.Name = "txtPSI"
        Me.txtPSI.Size = New System.Drawing.Size(187, 20)
        Me.txtPSI.TabIndex = 15
        '
        'txtFPS
        '
        Me.txtFPS.Location = New System.Drawing.Point(145, 89)
        Me.txtFPS.Name = "txtFPS"
        Me.txtFPS.Size = New System.Drawing.Size(187, 20)
        Me.txtFPS.TabIndex = 14
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(7, 118)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(103, 13)
        Me.Label30.TabIndex = 13
        Me.Label30.Text = "Pressure (PSI/LUP):"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(7, 92)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(70, 13)
        Me.Label31.TabIndex = 12
        Me.Label31.Text = "Velocity (fps):"
        '
        'txtCharge
        '
        Me.txtCharge.Location = New System.Drawing.Point(145, 63)
        Me.txtCharge.Name = "txtCharge"
        Me.txtCharge.Size = New System.Drawing.Size(187, 20)
        Me.txtCharge.TabIndex = 11
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(7, 66)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(71, 13)
        Me.Label32.TabIndex = 10
        Me.Label32.Text = "Load Charge:"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(7, 39)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(46, 13)
        Me.Label33.TabIndex = 9
        Me.Label33.Text = "Powder:"
        '
        'cmbPowderSG
        '
        Me.cmbPowderSG.DataSource = Me.ConfigListPowderDataSGViewBindingSource
        Me.cmbPowderSG.DisplayMember = "Powder_Name"
        Me.cmbPowderSG.FormattingEnabled = True
        Me.cmbPowderSG.Location = New System.Drawing.Point(145, 36)
        Me.cmbPowderSG.Name = "cmbPowderSG"
        Me.cmbPowderSG.Size = New System.Drawing.Size(187, 21)
        Me.cmbPowderSG.TabIndex = 8
        Me.cmbPowderSG.ValueMember = "ID"
        '
        'ConfigListPowderDataSGViewBindingSource
        '
        Me.ConfigListPowderDataSGViewBindingSource.DataMember = "Config_List_Powder_Data_SG_View"
        Me.ConfigListPowderDataSGViewBindingSource.DataSource = Me.MLLDataSet
        '
        'btnSaveAll
        '
        Me.btnSaveAll.Location = New System.Drawing.Point(53, 361)
        Me.btnSaveAll.Name = "btnSaveAll"
        Me.btnSaveAll.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveAll.TabIndex = 16
        Me.btnSaveAll.Text = "Save All"
        Me.btnSaveAll.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(211, 361)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 17
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'GeneralPrimerBindingSource1
        '
        Me.GeneralPrimerBindingSource1.DataMember = "General_Primer"
        Me.GeneralPrimerBindingSource1.DataSource = Me.MLLDataSet
        '
        'List_CalibersTableAdapter
        '
        Me.List_CalibersTableAdapter.ClearBeforeFill = True
        '
        'General_Ammunition_TypeTableAdapter
        '
        Me.General_Ammunition_TypeTableAdapter.ClearBeforeFill = True
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
        'Config_List_Powder_Data_NSG_ViewTableAdapter
        '
        Me.Config_List_Powder_Data_NSG_ViewTableAdapter.ClearBeforeFill = True
        '
        'List_SG_CaseTableAdapter
        '
        Me.List_SG_CaseTableAdapter.ClearBeforeFill = True
        '
        'List_SG_WADTableAdapter
        '
        Me.List_SG_WADTableAdapter.ClearBeforeFill = True
        '
        'List_SG_ShotCharge_LoadsTableAdapter
        '
        Me.List_SG_ShotCharge_LoadsTableAdapter.ClearBeforeFill = True
        '
        'Config_List_Powder_Data_SG_ViewTableAdapter
        '
        Me.Config_List_Powder_Data_SG_ViewTableAdapter.ClearBeforeFill = True
        '
        'List_SG_ShotType_DetailsTableAdapter
        '
        Me.List_SG_ShotType_DetailsTableAdapter.ClearBeforeFill = True
        '
        'ViewPrimerListTableAdapter
        '
        Me.ViewPrimerListTableAdapter.ClearBeforeFill = True
        '
        'btnSaveShotgunDetails
        '
        Me.btnSaveShotgunDetails.Location = New System.Drawing.Point(99, 277)
        Me.btnSaveShotgunDetails.Name = "btnSaveShotgunDetails"
        Me.btnSaveShotgunDetails.Size = New System.Drawing.Size(146, 23)
        Me.btnSaveShotgunDetails.TabIndex = 48
        Me.btnSaveShotgunDetails.Text = "Save Shotgun Details"
        Me.btnSaveShotgunDetails.UseVisualStyleBackColor = True
        '
        'frmEditConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 390)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSaveAll)
        Me.Controls.Add(Me.TabControl1)
        Me.HelpProvider1.SetHelpKeyword(Me, "Edit Configuration - Rifle & Pistol")
        Me.HelpProvider1.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.KeywordIndex)
        Me.HelpProvider1.SetHelpString(Me, "Edit Configuration - Rifle & Pistol")
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditConfig"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.Text = "frmEditConfig"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.ListCalibersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.GeneralAmmunitionTypeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListCaseBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GeneralPrimerBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListBulletsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.ListSGShotTypeDetailsBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListSGShotTypeDetailsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListSGShotChargeLoadsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListSGCaseBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListSGWADBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewPrimerListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.ConfigListPowderDataNSGViewBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        CType(Me.ConfigListPowderDataSGViewBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GeneralPrimerBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents cmbCal As System.Windows.Forms.ComboBox
    Friend WithEvents chkShotgun As System.Windows.Forms.CheckBox
    Friend WithEvents chkRP As System.Windows.Forms.CheckBox
    Friend WithEvents txtConfigID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbAmmo As System.Windows.Forms.ComboBox
    Friend WithEvents txtLoad As System.Windows.Forms.TextBox
    Friend WithEvents chkBook As System.Windows.Forms.CheckBox
    Friend WithEvents chkPersonal As System.Windows.Forms.CheckBox
    Friend WithEvents cmbCase As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPrimer As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBullet As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents ListCalibersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_CalibersTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_CalibersTableAdapter
    Friend WithEvents GeneralAmmunitionTypeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents General_Ammunition_TypeTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.General_Ammunition_TypeTableAdapter
    Friend WithEvents ListBulletsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_BulletsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_BulletsTableAdapter
    Friend WithEvents GeneralPrimerBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents General_PrimerTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.General_PrimerTableAdapter
    Friend WithEvents ListCaseBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_CaseTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_CaseTableAdapter
    Friend WithEvents txtCUPSMax As System.Windows.Forms.TextBox
    Friend WithEvents txtCUPSMid As System.Windows.Forms.TextBox
    Friend WithEvents txtCUPSMin As System.Windows.Forms.TextBox
    Friend WithEvents txtMVMax As System.Windows.Forms.TextBox
    Friend WithEvents txtMVMid As System.Windows.Forms.TextBox
    Friend WithEvents txtMVMin As System.Windows.Forms.TextBox
    Friend WithEvents txtLMax As System.Windows.Forms.TextBox
    Friend WithEvents txtLMid As System.Windows.Forms.TextBox
    Friend WithEvents txtLMin As System.Windows.Forms.TextBox
    Friend WithEvents cmbPowder As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents ConfigListPowderDataNSGViewBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Config_List_Powder_Data_NSG_ViewTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.Config_List_Powder_Data_NSG_ViewTableAdapter
    Friend WithEvents chkPrefPowder As System.Windows.Forms.CheckBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnSaveAll As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSavePowder As System.Windows.Forms.Button
    Friend WithEvents btnSaveGD As System.Windows.Forms.Button
    Friend WithEvents btnSaveRPDetails As System.Windows.Forms.Button
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents txtSourceSG As System.Windows.Forms.TextBox
    Friend WithEvents chkBookSG As System.Windows.Forms.CheckBox
    Friend WithEvents chkPersonalSG As System.Windows.Forms.CheckBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmdHull As System.Windows.Forms.ComboBox
    Friend WithEvents cmdWAD As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSGPrimer As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cmbLoadType As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtShotCharge As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents cmdShotType As System.Windows.Forms.ComboBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents cmbSlug As System.Windows.Forms.ComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents txtPSI As System.Windows.Forms.TextBox
    Friend WithEvents txtFPS As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtCharge As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents cmbPowderSG As System.Windows.Forms.ComboBox
    Friend WithEvents chkDefaultPowderSG As System.Windows.Forms.CheckBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents GeneralPrimerBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents ListSGCaseBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_CaseTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_CaseTableAdapter
    Friend WithEvents ListSGWADBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_WADTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_WADTableAdapter
    Friend WithEvents ListSGShotChargeLoadsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_ShotCharge_LoadsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotCharge_LoadsTableAdapter
    Friend WithEvents ConfigListPowderDataSGViewBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Config_List_Powder_Data_SG_ViewTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.Config_List_Powder_Data_SG_ViewTableAdapter
    Friend WithEvents ListSGShotTypeDetailsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_ShotType_DetailsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotType_DetailsTableAdapter
    Friend WithEvents ViewPrimerListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ViewPrimerListTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.viewPrimerListTableAdapter
    Friend WithEvents ListSGShotTypeDetailsBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents btnSavePowderSG As System.Windows.Forms.Button
    Friend WithEvents btnSaveShotgunDetails As System.Windows.Forms.Button
End Class
