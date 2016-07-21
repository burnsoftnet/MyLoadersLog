<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig_Add_Wizard_SG_2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig_Add_Wizard_SG_2))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbPrimer = New System.Windows.Forms.ComboBox
        Me.ViewPrimerListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.cmdWAD = New System.Windows.Forms.ComboBox
        Me.ListSGWADBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cmdHull = New System.Windows.Forms.ComboBox
        Me.ListSGCaseBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cmdShotType = New System.Windows.Forms.ComboBox
        Me.ListSGShotTypeDetailsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ViewPrimerListTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.viewPrimerListTableAdapter
        Me.List_SG_WADTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_WADTableAdapter
        Me.List_SG_CaseTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_CaseTableAdapter
        Me.List_SG_ShotType_DetailsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotType_DetailsTableAdapter
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtShotCharge = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmbLoadType = New System.Windows.Forms.ComboBox
        Me.ListSGShotChargeLoadsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.List_SG_ShotCharge_LoadsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotCharge_LoadsTableAdapter
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnNext = New System.Windows.Forms.Button
        Me.txtSource = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.chkPersonal = New System.Windows.Forms.CheckBox
        CType(Me.ViewPrimerListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListSGWADBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListSGCaseBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListSGShotTypeDetailsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListSGShotChargeLoadsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Primer:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Hull:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 125)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "WAD:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Shot Type:"
        '
        'cmbPrimer
        '
        Me.cmbPrimer.DataSource = Me.ViewPrimerListBindingSource
        Me.cmbPrimer.DisplayMember = "PrimerName"
        Me.cmbPrimer.FormattingEnabled = True
        Me.cmbPrimer.Location = New System.Drawing.Point(110, 70)
        Me.cmbPrimer.Name = "cmbPrimer"
        Me.cmbPrimer.Size = New System.Drawing.Size(235, 21)
        Me.cmbPrimer.TabIndex = 4
        Me.cmbPrimer.ValueMember = "ID"
        '
        'ViewPrimerListBindingSource
        '
        Me.ViewPrimerListBindingSource.DataMember = "viewPrimerList"
        Me.ViewPrimerListBindingSource.DataSource = Me.MLLDataSet
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cmdWAD
        '
        Me.cmdWAD.DataSource = Me.ListSGWADBindingSource
        Me.cmdWAD.DisplayMember = "WAD"
        Me.cmdWAD.FormattingEnabled = True
        Me.cmdWAD.Location = New System.Drawing.Point(110, 122)
        Me.cmdWAD.Name = "cmdWAD"
        Me.cmdWAD.Size = New System.Drawing.Size(235, 21)
        Me.cmdWAD.TabIndex = 5
        Me.cmdWAD.ValueMember = "ID"
        '
        'ListSGWADBindingSource
        '
        Me.ListSGWADBindingSource.DataMember = "List_SG_WAD"
        Me.ListSGWADBindingSource.DataSource = Me.MLLDataSet
        '
        'cmdHull
        '
        Me.cmdHull.DataSource = Me.ListSGCaseBindingSource
        Me.cmdHull.DisplayMember = "Name"
        Me.cmdHull.FormattingEnabled = True
        Me.cmdHull.Location = New System.Drawing.Point(110, 95)
        Me.cmdHull.Name = "cmdHull"
        Me.cmdHull.Size = New System.Drawing.Size(235, 21)
        Me.cmdHull.TabIndex = 6
        Me.cmdHull.ValueMember = "ID"
        '
        'ListSGCaseBindingSource
        '
        Me.ListSGCaseBindingSource.DataMember = "List_SG_Case"
        Me.ListSGCaseBindingSource.DataSource = Me.MLLDataSet
        '
        'cmdShotType
        '
        Me.cmdShotType.DataSource = Me.ListSGShotTypeDetailsBindingSource
        Me.cmdShotType.DisplayMember = "Name"
        Me.cmdShotType.FormattingEnabled = True
        Me.cmdShotType.Location = New System.Drawing.Point(110, 149)
        Me.cmdShotType.Name = "cmdShotType"
        Me.cmdShotType.Size = New System.Drawing.Size(235, 21)
        Me.cmdShotType.TabIndex = 7
        Me.cmdShotType.ValueMember = "ID"
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
        'List_SG_WADTableAdapter
        '
        Me.List_SG_WADTableAdapter.ClearBeforeFill = True
        '
        'List_SG_CaseTableAdapter
        '
        Me.List_SG_CaseTableAdapter.ClearBeforeFill = True
        '
        'List_SG_ShotType_DetailsTableAdapter
        '
        Me.List_SG_ShotType_DetailsTableAdapter.ClearBeforeFill = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Shot Charge (oz):"
        '
        'txtShotCharge
        '
        Me.txtShotCharge.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtShotCharge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtShotCharge.Location = New System.Drawing.Point(110, 176)
        Me.txtShotCharge.Name = "txtShotCharge"
        Me.txtShotCharge.Size = New System.Drawing.Size(235, 20)
        Me.txtShotCharge.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 205)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Load Type:"
        '
        'cmbLoadType
        '
        Me.cmbLoadType.DataSource = Me.ListSGShotChargeLoadsBindingSource
        Me.cmbLoadType.DisplayMember = "Name"
        Me.cmbLoadType.FormattingEnabled = True
        Me.cmbLoadType.Location = New System.Drawing.Point(110, 202)
        Me.cmbLoadType.Name = "cmbLoadType"
        Me.cmbLoadType.Size = New System.Drawing.Size(235, 21)
        Me.cmbLoadType.TabIndex = 11
        Me.cmbLoadType.ValueMember = "ID"
        '
        'ListSGShotChargeLoadsBindingSource
        '
        Me.ListSGShotChargeLoadsBindingSource.DataMember = "List_SG_ShotCharge_Loads"
        Me.ListSGShotChargeLoadsBindingSource.DataSource = Me.MLLDataSet
        '
        'List_SG_ShotCharge_LoadsTableAdapter
        '
        Me.List_SG_ShotCharge_LoadsTableAdapter.ClearBeforeFill = True
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(13, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(332, 44)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Select the Components below that will go into make this kind of shotgun load.  Af" & _
            "ter you have finished, click on the Next button to add the Powder Information."
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(270, 277)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 13
        Me.btnNext.Text = "&Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'txtSource
        '
        Me.txtSource.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtSource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtSource.Location = New System.Drawing.Point(110, 251)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.ReadOnly = True
        Me.txtSource.Size = New System.Drawing.Size(235, 20)
        Me.txtSource.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 254)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Source"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 230)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Personal Load?"
        '
        'chkPersonal
        '
        Me.chkPersonal.AutoSize = True
        Me.chkPersonal.Checked = True
        Me.chkPersonal.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPersonal.Location = New System.Drawing.Point(110, 229)
        Me.chkPersonal.Name = "chkPersonal"
        Me.chkPersonal.Size = New System.Drawing.Size(44, 17)
        Me.chkPersonal.TabIndex = 17
        Me.chkPersonal.Text = "Yes"
        Me.chkPersonal.UseVisualStyleBackColor = True
        '
        'frmConfig_Add_Wizard_SG_2
        '
        Me.AcceptButton = Me.btnNext
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(364, 313)
        Me.Controls.Add(Me.chkPersonal)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtSource)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbLoadType)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtShotCharge)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmdShotType)
        Me.Controls.Add(Me.cmdHull)
        Me.Controls.Add(Me.cmdWAD)
        Me.Controls.Add(Me.cmbPrimer)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfig_Add_Wizard_SG_2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Shot Configuration"
        CType(Me.ViewPrimerListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListSGWADBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListSGCaseBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListSGShotTypeDetailsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListSGShotChargeLoadsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPrimer As System.Windows.Forms.ComboBox
    Friend WithEvents cmdWAD As System.Windows.Forms.ComboBox
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents ViewPrimerListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ViewPrimerListTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.viewPrimerListTableAdapter
    Friend WithEvents ListSGWADBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_WADTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_WADTableAdapter
    Friend WithEvents cmdHull As System.Windows.Forms.ComboBox
    Friend WithEvents ListSGCaseBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_CaseTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_CaseTableAdapter
    Friend WithEvents cmdShotType As System.Windows.Forms.ComboBox
    Friend WithEvents ListSGShotTypeDetailsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_ShotType_DetailsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotType_DetailsTableAdapter
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtShotCharge As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbLoadType As System.Windows.Forms.ComboBox
    Friend WithEvents ListSGShotChargeLoadsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_SG_ShotCharge_LoadsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_SG_ShotCharge_LoadsTableAdapter
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkPersonal As System.Windows.Forms.CheckBox
End Class
