<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig_Add_Wizard_Powder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig_Add_Wizard_Powder))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.cmbPowder = New System.Windows.Forms.ComboBox
        Me.GeneralPowderBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.General_PowderTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.General_PowderTableAdapter
        Me.txtLMin = New System.Windows.Forms.TextBox
        Me.txtLMid = New System.Windows.Forms.TextBox
        Me.txtLMax = New System.Windows.Forms.TextBox
        Me.txtMVMin = New System.Windows.Forms.TextBox
        Me.txtMVMid = New System.Windows.Forms.TextBox
        Me.txtMVMax = New System.Windows.Forms.TextBox
        Me.txtCUPSMin = New System.Windows.Forms.TextBox
        Me.txtCUPSMid = New System.Windows.Forms.TextBox
        Me.txtCUPSMax = New System.Windows.Forms.TextBox
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        CType(Me.GeneralPowderBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Powder:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Load Min:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Load Mid/Preferred:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Load Max:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 127)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Min Muzzle Velocity:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 152)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Mid Muzzle Velocity:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 179)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Max Muzzle Velocity:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 205)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(112, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Min Pressure C.U.P.S:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 229)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(112, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Mid Pressure C.U.P.S:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 255)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(115, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Max Pressure C.U.P.S:"
        '
        'cmbPowder
        '
        Me.cmbPowder.DataSource = Me.GeneralPowderBindingSource
        Me.cmbPowder.DisplayMember = "Name"
        Me.cmbPowder.FormattingEnabled = True
        Me.cmbPowder.Location = New System.Drawing.Point(134, 18)
        Me.cmbPowder.Name = "cmbPowder"
        Me.cmbPowder.Size = New System.Drawing.Size(166, 21)
        Me.cmbPowder.TabIndex = 10
        Me.cmbPowder.ValueMember = "ID"
        '
        'GeneralPowderBindingSource
        '
        Me.GeneralPowderBindingSource.DataMember = "General_Powder"
        Me.GeneralPowderBindingSource.DataSource = Me.MLLDataSet
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'General_PowderTableAdapter
        '
        Me.General_PowderTableAdapter.ClearBeforeFill = True
        '
        'txtLMin
        '
        Me.txtLMin.Location = New System.Drawing.Point(134, 46)
        Me.txtLMin.Name = "txtLMin"
        Me.txtLMin.Size = New System.Drawing.Size(166, 20)
        Me.txtLMin.TabIndex = 11
        '
        'txtLMid
        '
        Me.txtLMid.Location = New System.Drawing.Point(134, 72)
        Me.txtLMid.Name = "txtLMid"
        Me.txtLMid.Size = New System.Drawing.Size(166, 20)
        Me.txtLMid.TabIndex = 12
        '
        'txtLMax
        '
        Me.txtLMax.Location = New System.Drawing.Point(134, 98)
        Me.txtLMax.Name = "txtLMax"
        Me.txtLMax.Size = New System.Drawing.Size(166, 20)
        Me.txtLMax.TabIndex = 13
        '
        'txtMVMin
        '
        Me.txtMVMin.Location = New System.Drawing.Point(134, 124)
        Me.txtMVMin.Name = "txtMVMin"
        Me.txtMVMin.Size = New System.Drawing.Size(166, 20)
        Me.txtMVMin.TabIndex = 14
        '
        'txtMVMid
        '
        Me.txtMVMid.Location = New System.Drawing.Point(134, 149)
        Me.txtMVMid.Name = "txtMVMid"
        Me.txtMVMid.Size = New System.Drawing.Size(166, 20)
        Me.txtMVMid.TabIndex = 15
        '
        'txtMVMax
        '
        Me.txtMVMax.Location = New System.Drawing.Point(134, 176)
        Me.txtMVMax.Name = "txtMVMax"
        Me.txtMVMax.Size = New System.Drawing.Size(166, 20)
        Me.txtMVMax.TabIndex = 16
        '
        'txtCUPSMin
        '
        Me.txtCUPSMin.Location = New System.Drawing.Point(134, 202)
        Me.txtCUPSMin.Name = "txtCUPSMin"
        Me.txtCUPSMin.Size = New System.Drawing.Size(166, 20)
        Me.txtCUPSMin.TabIndex = 17
        '
        'txtCUPSMid
        '
        Me.txtCUPSMid.Location = New System.Drawing.Point(134, 226)
        Me.txtCUPSMid.Name = "txtCUPSMid"
        Me.txtCUPSMid.Size = New System.Drawing.Size(166, 20)
        Me.txtCUPSMid.TabIndex = 18
        '
        'txtCUPSMax
        '
        Me.txtCUPSMax.Location = New System.Drawing.Point(134, 252)
        Me.txtCUPSMax.Name = "txtCUPSMax"
        Me.txtCUPSMax.Size = New System.Drawing.Size(166, 20)
        Me.txtCUPSMax.TabIndex = 19
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(31, 286)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(83, 23)
        Me.btnAdd.TabIndex = 20
        Me.btnAdd.Text = "Add && Finish"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(178, 286)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 21
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'frmConfig_Add_Wizard_Powder
        '
        Me.AcceptButton = Me.btnAdd
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(318, 321)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtCUPSMax)
        Me.Controls.Add(Me.txtCUPSMid)
        Me.Controls.Add(Me.txtCUPSMin)
        Me.Controls.Add(Me.txtMVMax)
        Me.Controls.Add(Me.txtMVMid)
        Me.Controls.Add(Me.txtMVMin)
        Me.Controls.Add(Me.txtLMax)
        Me.Controls.Add(Me.txtLMid)
        Me.Controls.Add(Me.txtLMin)
        Me.Controls.Add(Me.cmbPowder)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
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
        Me.Name = "frmConfig_Add_Wizard_Powder"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Configuration - Powder Details"
        CType(Me.GeneralPowderBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbPowder As System.Windows.Forms.ComboBox
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents GeneralPowderBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents General_PowderTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.General_PowderTableAdapter
    Friend WithEvents txtLMin As System.Windows.Forms.TextBox
    Friend WithEvents txtLMid As System.Windows.Forms.TextBox
    Friend WithEvents txtLMax As System.Windows.Forms.TextBox
    Friend WithEvents txtMVMin As System.Windows.Forms.TextBox
    Friend WithEvents txtMVMid As System.Windows.Forms.TextBox
    Friend WithEvents txtMVMax As System.Windows.Forms.TextBox
    Friend WithEvents txtCUPSMin As System.Windows.Forms.TextBox
    Friend WithEvents txtCUPSMid As System.Windows.Forms.TextBox
    Friend WithEvents txtCUPSMax As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
End Class
