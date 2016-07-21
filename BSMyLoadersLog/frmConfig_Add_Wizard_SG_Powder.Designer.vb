<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig_Add_Wizard_SG_Powder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig_Add_Wizard_SG_Powder))
        Me.cmbPowder = New System.Windows.Forms.ComboBox
        Me.GeneralPowderBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCharge = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtFPS = New System.Windows.Forms.TextBox
        Me.txtPSI = New System.Windows.Forms.TextBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.General_PowderTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.General_PowderTableAdapter
        Me.Label5 = New System.Windows.Forms.Label
        CType(Me.GeneralPowderBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbPowder
        '
        Me.cmbPowder.DataSource = Me.GeneralPowderBindingSource
        Me.cmbPowder.DisplayMember = "Name"
        Me.cmbPowder.FormattingEnabled = True
        Me.cmbPowder.Location = New System.Drawing.Point(121, 55)
        Me.cmbPowder.Name = "cmbPowder"
        Me.cmbPowder.Size = New System.Drawing.Size(187, 21)
        Me.cmbPowder.TabIndex = 0
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Powder:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Load Charge:"
        '
        'txtCharge
        '
        Me.txtCharge.Location = New System.Drawing.Point(121, 82)
        Me.txtCharge.Name = "txtCharge"
        Me.txtCharge.Size = New System.Drawing.Size(187, 20)
        Me.txtCharge.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 111)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Velocity (fps):"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 137)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Pressure (PSI/LUP):"
        '
        'txtFPS
        '
        Me.txtFPS.Location = New System.Drawing.Point(121, 108)
        Me.txtFPS.Name = "txtFPS"
        Me.txtFPS.Size = New System.Drawing.Size(187, 20)
        Me.txtFPS.TabIndex = 6
        '
        'txtPSI
        '
        Me.txtPSI.Location = New System.Drawing.Point(121, 134)
        Me.txtPSI.Name = "txtPSI"
        Me.txtPSI.Size = New System.Drawing.Size(187, 20)
        Me.txtPSI.TabIndex = 7
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(204, 166)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(104, 23)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "&Save && Finish"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'General_PowderTableAdapter
        '
        Me.General_PowderTableAdapter.ClearBeforeFill = True
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(13, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(296, 49)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Select your preferred Powder for this configuration.  You can add more powder aft" & _
            "er you have entered in your preferred powder configuration."
        '
        'frmConfig_Add_Wizard_SG_Powder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(323, 197)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtPSI)
        Me.Controls.Add(Me.txtFPS)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCharge)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbPowder)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfig_Add_Wizard_SG_Powder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Powder Information"
        CType(Me.GeneralPowderBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbPowder As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCharge As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFPS As System.Windows.Forms.TextBox
    Friend WithEvents txtPSI As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents GeneralPowderBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents General_PowderTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.General_PowderTableAdapter
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
