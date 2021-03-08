<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAddBullets
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddBullets))
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
        Me.txtManu = New System.Windows.Forms.TextBox
        Me.txtPartNo = New System.Windows.Forms.TextBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtDia = New System.Windows.Forms.TextBox
        Me.txtWei = New System.Windows.Forms.TextBox
        Me.txtSecDia = New System.Windows.Forms.TextBox
        Me.txtBC = New System.Windows.Forms.TextBox
        Me.cmbCalList = New System.Windows.Forms.ComboBox
        Me.ListCalibersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.txtPrice = New System.Windows.Forms.TextBox
        Me.List_CalibersTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_CalibersTableAdapter
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.nudQty = New System.Windows.Forms.NumericUpDown
        Me.cmbBT = New System.Windows.Forms.ComboBox
        Me.GeneralAmmunitionTypeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label11 = New System.Windows.Forms.Label
        Me.General_Ammunition_TypeTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.General_Ammunition_TypeTableAdapter
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        CType(Me.ListCalibersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GeneralAmmunitionTypeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Manufacturer:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Diameter:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Weight:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 139)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Sectional Density:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Part Number:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 165)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Ballistic Coefficient:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 190)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Caliber:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 248)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(26, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Qty:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 270)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Price:"
        '
        'txtManu
        '
        Me.txtManu.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtManu.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtManu.Location = New System.Drawing.Point(109, 6)
        Me.txtManu.Name = "txtManu"
        Me.txtManu.Size = New System.Drawing.Size(185, 20)
        Me.txtManu.TabIndex = 1
        '
        'txtPartNo
        '
        Me.txtPartNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtPartNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtPartNo.Location = New System.Drawing.Point(109, 32)
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.Size = New System.Drawing.Size(185, 20)
        Me.txtPartNo.TabIndex = 2
        '
        'txtName
        '
        Me.txtName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtName.Location = New System.Drawing.Point(109, 58)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(185, 20)
        Me.txtName.TabIndex = 3
        '
        'txtDia
        '
        Me.txtDia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtDia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtDia.Location = New System.Drawing.Point(109, 84)
        Me.txtDia.Name = "txtDia"
        Me.txtDia.Size = New System.Drawing.Size(185, 20)
        Me.txtDia.TabIndex = 4
        '
        'txtWei
        '
        Me.txtWei.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtWei.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtWei.Location = New System.Drawing.Point(109, 110)
        Me.txtWei.Name = "txtWei"
        Me.txtWei.Size = New System.Drawing.Size(185, 20)
        Me.txtWei.TabIndex = 5
        '
        'txtSecDia
        '
        Me.txtSecDia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtSecDia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtSecDia.Location = New System.Drawing.Point(109, 136)
        Me.txtSecDia.Name = "txtSecDia"
        Me.txtSecDia.Size = New System.Drawing.Size(185, 20)
        Me.txtSecDia.TabIndex = 6
        '
        'txtBC
        '
        Me.txtBC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtBC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtBC.Location = New System.Drawing.Point(109, 162)
        Me.txtBC.Name = "txtBC"
        Me.txtBC.Size = New System.Drawing.Size(185, 20)
        Me.txtBC.TabIndex = 7
        '
        'cmbCalList
        '
        Me.cmbCalList.DataSource = Me.ListCalibersBindingSource
        Me.cmbCalList.DisplayMember = "Cal"
        Me.cmbCalList.FormattingEnabled = True
        Me.cmbCalList.Location = New System.Drawing.Point(109, 187)
        Me.cmbCalList.Name = "cmbCalList"
        Me.cmbCalList.Size = New System.Drawing.Size(185, 21)
        Me.cmbCalList.TabIndex = 8
        Me.cmbCalList.ValueMember = "ID"
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
        'txtPrice
        '
        Me.txtPrice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtPrice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtPrice.Location = New System.Drawing.Point(109, 267)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(185, 20)
        Me.txtPrice.TabIndex = 11
        '
        'List_CalibersTableAdapter
        '
        Me.List_CalibersTableAdapter.ClearBeforeFill = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(31, 302)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 12
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(200, 302)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'nudQty
        '
        Me.nudQty.Location = New System.Drawing.Point(109, 241)
        Me.nudQty.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.nudQty.Name = "nudQty"
        Me.nudQty.Size = New System.Drawing.Size(185, 20)
        Me.nudQty.TabIndex = 10
        '
        'cmbBT
        '
        Me.cmbBT.DataSource = Me.GeneralAmmunitionTypeBindingSource
        Me.cmbBT.DisplayMember = "FType"
        Me.cmbBT.FormattingEnabled = True
        Me.cmbBT.Location = New System.Drawing.Point(109, 214)
        Me.cmbBT.Name = "cmbBT"
        Me.cmbBT.Size = New System.Drawing.Size(185, 21)
        Me.cmbBT.TabIndex = 9
        Me.cmbBT.ValueMember = "ID"
        '
        'GeneralAmmunitionTypeBindingSource
        '
        Me.GeneralAmmunitionTypeBindingSource.DataMember = "General_Ammunition_Type"
        Me.GeneralAmmunitionTypeBindingSource.DataSource = Me.MLLDataSetBindingSource
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(8, 217)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(91, 13)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "Ammunition Type:"
        '
        'General_Ammunition_TypeTableAdapter
        '
        Me.General_Ammunition_TypeTableAdapter.ClearBeforeFill = True
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'frmAddBullets
        '
        Me.AcceptButton = Me.btnAdd
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(317, 349)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmbBT)
        Me.Controls.Add(Me.nudQty)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtPrice)
        Me.Controls.Add(Me.cmbCalList)
        Me.Controls.Add(Me.txtBC)
        Me.Controls.Add(Me.txtSecDia)
        Me.Controls.Add(Me.txtWei)
        Me.Controls.Add(Me.txtDia)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtPartNo)
        Me.Controls.Add(Me.txtManu)
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
        Me.HelpProvider1.SetHelpKeyword(Me, "Add Bullets to Inventory")
        Me.HelpProvider1.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.KeywordIndex)
        Me.HelpProvider1.SetHelpString(Me, "Add Bullets to Inventory")
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddBullets"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.Text = "Add Bullets to Inventory"
        CType(Me.ListCalibersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GeneralAmmunitionTypeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtManu As System.Windows.Forms.TextBox
    Friend WithEvents txtPartNo As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtDia As System.Windows.Forms.TextBox
    Friend WithEvents txtWei As System.Windows.Forms.TextBox
    Friend WithEvents txtSecDia As System.Windows.Forms.TextBox
    Friend WithEvents txtBC As System.Windows.Forms.TextBox
    Friend WithEvents cmbCalList As System.Windows.Forms.ComboBox
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents MLLDataSetBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents ListCalibersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents List_CalibersTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_CalibersTableAdapter
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents nudQty As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbBT As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GeneralAmmunitionTypeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents General_Ammunition_TypeTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.General_Ammunition_TypeTableAdapter
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
End Class
