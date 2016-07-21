<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig_Add_Wizard_SG_3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig_Add_Wizard_SG_3))
        Me.chkPersonal = New System.Windows.Forms.CheckBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtSource = New System.Windows.Forms.TextBox
        Me.btnNext = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmbLoadType = New System.Windows.Forms.ComboBox
        Me.ListSGShotChargeLoadsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmdHull = New System.Windows.Forms.ComboBox
        Me.ListSGCaseBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cmdWAD = New System.Windows.Forms.ComboBox
        Me.ListSGWADBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cmbPrimer = New System.Windows.Forms.ComboBox
        Me.ViewPrimerListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbSlug = New System.Windows.Forms.ComboBox
        Me.ListSGShotTypeDetailsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ViewPrimerListTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.viewPrimerListTableAdapter
        Me.List_SG_CaseTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_CaseTableAdapter
        Me.List_SG_WADTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_WADTableAdapter
        Me.List_SG_ShotCharge_LoadsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotCharge_LoadsTableAdapter
        Me.List_SG_ShotType_DetailsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotType_DetailsTableAdapter
        CType(Me.ListSGShotChargeLoadsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListSGCaseBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListSGWADBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewPrimerListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListSGShotTypeDetailsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkPersonal
        '
        Me.chkPersonal.AutoSize = True
        Me.chkPersonal.Checked = True
        Me.chkPersonal.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPersonal.Location = New System.Drawing.Point(109, 200)
        Me.chkPersonal.Name = "chkPersonal"
        Me.chkPersonal.Size = New System.Drawing.Size(44, 17)
        Me.chkPersonal.TabIndex = 35
        Me.chkPersonal.Text = "Yes"
        Me.chkPersonal.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 201)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 34
        Me.Label9.Text = "Personal Load?"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 225)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 13)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "Source"
        '
        'txtSource
        '
        Me.txtSource.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtSource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtSource.Location = New System.Drawing.Point(109, 222)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.ReadOnly = True
        Me.txtSource.Size = New System.Drawing.Size(235, 20)
        Me.txtSource.TabIndex = 32
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(269, 248)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 31
        Me.btnNext.Text = "&Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(12, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(332, 44)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Select the Components below that will go into make this kind of shotgun load.  Af" & _
            "ter you have finished, click on the Next button to add the Powder Information."
        '
        'cmbLoadType
        '
        Me.cmbLoadType.DataSource = Me.ListSGShotChargeLoadsBindingSource
        Me.cmbLoadType.DisplayMember = "Name"
        Me.cmbLoadType.FormattingEnabled = True
        Me.cmbLoadType.Location = New System.Drawing.Point(109, 173)
        Me.cmbLoadType.Name = "cmbLoadType"
        Me.cmbLoadType.Size = New System.Drawing.Size(235, 21)
        Me.cmbLoadType.TabIndex = 29
        Me.cmbLoadType.ValueMember = "ID"
        '
        'ListSGShotChargeLoadsBindingSource
        '
        Me.ListSGShotChargeLoadsBindingSource.DataMember = "List_SG_ShotCharge_Loads"
        Me.ListSGShotChargeLoadsBindingSource.DataSource = Me.MLLDataSet
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 176)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Load Type:"
        '
        'cmdHull
        '
        Me.cmdHull.DataSource = Me.ListSGCaseBindingSource
        Me.cmdHull.DisplayMember = "Name"
        Me.cmdHull.FormattingEnabled = True
        Me.cmdHull.Location = New System.Drawing.Point(109, 91)
        Me.cmdHull.Name = "cmdHull"
        Me.cmdHull.Size = New System.Drawing.Size(235, 21)
        Me.cmdHull.TabIndex = 24
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
        Me.cmdWAD.Location = New System.Drawing.Point(109, 118)
        Me.cmdWAD.Name = "cmdWAD"
        Me.cmdWAD.Size = New System.Drawing.Size(235, 21)
        Me.cmdWAD.TabIndex = 23
        Me.cmdWAD.ValueMember = "ID"
        '
        'ListSGWADBindingSource
        '
        Me.ListSGWADBindingSource.DataMember = "List_SG_WAD"
        Me.ListSGWADBindingSource.DataSource = Me.MLLDataSet
        '
        'cmbPrimer
        '
        Me.cmbPrimer.DataSource = Me.ViewPrimerListBindingSource
        Me.cmbPrimer.DisplayMember = "PrimerName"
        Me.cmbPrimer.FormattingEnabled = True
        Me.cmbPrimer.Location = New System.Drawing.Point(109, 66)
        Me.cmbPrimer.Name = "cmbPrimer"
        Me.cmbPrimer.Size = New System.Drawing.Size(235, 21)
        Me.cmbPrimer.TabIndex = 22
        Me.cmbPrimer.ValueMember = "ID"
        '
        'ViewPrimerListBindingSource
        '
        Me.ViewPrimerListBindingSource.DataMember = "viewPrimerList"
        Me.ViewPrimerListBindingSource.DataSource = Me.MLLDataSet
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "WAD:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Hull:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Primer:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 149)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "Slug Type:"
        '
        'cmbSlug
        '
        Me.cmbSlug.DataSource = Me.ListSGShotTypeDetailsBindingSource
        Me.cmbSlug.DisplayMember = "Name"
        Me.cmbSlug.FormattingEnabled = True
        Me.cmbSlug.Location = New System.Drawing.Point(109, 145)
        Me.cmbSlug.Name = "cmbSlug"
        Me.cmbSlug.Size = New System.Drawing.Size(235, 21)
        Me.cmbSlug.TabIndex = 37
        Me.cmbSlug.ValueMember = "ID"
        '
        'ListSGShotTypeDetailsBindingSource
        '
        Me.ListSGShotTypeDetailsBindingSource.DataMember = "List_SG_ShotType_Details"
        Me.ListSGShotTypeDetailsBindingSource.DataSource = Me.MLLDataSet
        '
        'ViewPrimerListTableAdapter
        '
        Me.ViewPrimerListTableAdapter.ClearBeforeFill = True
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
        'List_SG_ShotType_DetailsTableAdapter
        '
        Me.List_SG_ShotType_DetailsTableAdapter.ClearBeforeFill = True
        '
        'frmConfig_Add_Wizard_SG_3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(366, 290)
        Me.Controls.Add(Me.cmbSlug)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.chkPersonal)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtSource)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbLoadType)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmdHull)
        Me.Controls.Add(Me.cmdWAD)
        Me.Controls.Add(Me.cmbPrimer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfig_Add_Wizard_SG_3"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Slug Configuration"
        CType(Me.ListSGShotChargeLoadsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListSGCaseBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListSGWADBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewPrimerListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListSGShotTypeDetailsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkPersonal As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbLoadType As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdHull As System.Windows.Forms.ComboBox
    Friend WithEvents cmdWAD As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPrimer As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents ViewPrimerListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ViewPrimerListTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.viewPrimerListTableAdapter
    Friend WithEvents ListSGCaseBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_CaseTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_CaseTableAdapter
    Friend WithEvents ListSGWADBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_WADTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_WADTableAdapter
    Friend WithEvents ListSGShotChargeLoadsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_ShotCharge_LoadsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotCharge_LoadsTableAdapter
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbSlug As System.Windows.Forms.ComboBox
    Friend WithEvents ListSGShotTypeDetailsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_ShotType_DetailsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotType_DetailsTableAdapter
End Class
