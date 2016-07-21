<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig_Add_Wizard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig_Add_Wizard))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtConfigID = New System.Windows.Forms.TextBox
        Me.chkRP = New System.Windows.Forms.CheckBox
        Me.chkShotgun = New System.Windows.Forms.CheckBox
        Me.cmbCal = New System.Windows.Forms.ComboBox
        Me.ListCalibersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.List_CalibersTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_CalibersTableAdapter
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnNext = New System.Windows.Forms.Button
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        CType(Me.ListCalibersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Configuration ID:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Type:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Caliber:"
        '
        'txtConfigID
        '
        Me.txtConfigID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtConfigID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtConfigID.Location = New System.Drawing.Point(99, 49)
        Me.txtConfigID.Name = "txtConfigID"
        Me.txtConfigID.Size = New System.Drawing.Size(195, 20)
        Me.txtConfigID.TabIndex = 4
        '
        'chkRP
        '
        Me.chkRP.AutoSize = True
        Me.chkRP.Location = New System.Drawing.Point(99, 76)
        Me.chkRP.Name = "chkRP"
        Me.chkRP.Size = New System.Drawing.Size(77, 17)
        Me.chkRP.TabIndex = 5
        Me.chkRP.Text = "Rifle/Pistol"
        Me.chkRP.UseVisualStyleBackColor = True
        '
        'chkShotgun
        '
        Me.chkShotgun.AutoSize = True
        Me.chkShotgun.Location = New System.Drawing.Point(195, 76)
        Me.chkShotgun.Name = "chkShotgun"
        Me.chkShotgun.Size = New System.Drawing.Size(66, 17)
        Me.chkShotgun.TabIndex = 6
        Me.chkShotgun.Text = "Shotgun"
        Me.chkShotgun.UseVisualStyleBackColor = True
        '
        'cmbCal
        '
        Me.cmbCal.DataSource = Me.ListCalibersBindingSource
        Me.cmbCal.DisplayMember = "Cal"
        Me.cmbCal.FormattingEnabled = True
        Me.cmbCal.Location = New System.Drawing.Point(99, 99)
        Me.cmbCal.Name = "cmbCal"
        Me.cmbCal.Size = New System.Drawing.Size(195, 21)
        Me.cmbCal.TabIndex = 7
        Me.cmbCal.ValueMember = "ID"
        '
        'ListCalibersBindingSource
        '
        Me.ListCalibersBindingSource.DataMember = "List_Calibers"
        Me.ListCalibersBindingSource.DataSource = Me.MLLDataSetBindingSource
        '
        'MLLDataSetBindingSource
        '
        Me.MLLDataSetBindingSource.DataSource = Me.MLLDataSet
        Me.MLLDataSetBindingSource.Position = 0
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'List_CalibersTableAdapter
        '
        Me.List_CalibersTableAdapter.ClearBeforeFill = True
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(7, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(320, 38)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "The Add Configuration Sheet Wizard will walk you through adding a new load to the" & _
            " database."
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(219, 126)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 9
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'frmConfig_Add_Wizard
        '
        Me.AcceptButton = Me.btnNext
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(331, 159)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbCal)
        Me.Controls.Add(Me.chkShotgun)
        Me.Controls.Add(Me.chkRP)
        Me.Controls.Add(Me.txtConfigID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.HelpProvider1.SetHelpKeyword(Me, "Adding a New Configuration")
        Me.HelpProvider1.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.KeywordIndex)
        Me.HelpProvider1.SetHelpString(Me, "Adding a New Configuration")
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfig_Add_Wizard"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Configuration Sheet Wizard"
        CType(Me.ListCalibersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtConfigID As System.Windows.Forms.TextBox
    Friend WithEvents chkRP As System.Windows.Forms.CheckBox
    Friend WithEvents chkShotgun As System.Windows.Forms.CheckBox
    Friend WithEvents cmbCal As System.Windows.Forms.ComboBox
    Friend WithEvents MLLDataSetBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents ListCalibersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_CalibersTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_CalibersTableAdapter
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
End Class
