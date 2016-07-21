<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchConfig_RiflePistol
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearchConfig_RiflePistol))
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtString = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbCol = New System.Windows.Forms.ComboBox
        Me.SearchFieldsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.Search_FieldsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.Search_FieldsTableAdapter
        Me.btnAdd = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lstQueue = New System.Windows.Forms.ListBox
        Me.cmbAddOpt = New System.Windows.Forms.ComboBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnSQL = New System.Windows.Forms.Button
        Me.txtSQL = New System.Windows.Forms.TextBox
        Me.btnResults = New System.Windows.Forms.Button
        Me.btnShowSQL = New System.Windows.Forms.Button
        Me.btnHideSQL = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        CType(Me.SearchFieldsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckedListBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(245, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(221, 161)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Columns to Display"
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.CheckOnClick = True
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(6, 19)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(209, 139)
        Me.CheckedListBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Look For:"
        '
        'txtString
        '
        Me.txtString.Location = New System.Drawing.Point(68, 18)
        Me.txtString.Name = "txtString"
        Me.txtString.Size = New System.Drawing.Size(115, 20)
        Me.txtString.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(189, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "in"
        '
        'cmbCol
        '
        Me.cmbCol.DataSource = Me.SearchFieldsBindingSource
        Me.cmbCol.DisplayMember = "Dis"
        Me.cmbCol.FormattingEnabled = True
        Me.cmbCol.Location = New System.Drawing.Point(210, 18)
        Me.cmbCol.Name = "cmbCol"
        Me.cmbCol.Size = New System.Drawing.Size(114, 21)
        Me.cmbCol.TabIndex = 5
        Me.cmbCol.ValueMember = "ID"
        '
        'SearchFieldsBindingSource
        '
        Me.SearchFieldsBindingSource.DataMember = "Search_Fields"
        Me.SearchFieldsBindingSource.DataSource = Me.MLLDataSet
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Search_FieldsTableAdapter
        '
        Me.Search_FieldsTableAdapter.ClearBeforeFill = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(391, 16)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lstQueue)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(227, 161)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Search Queue"
        '
        'lstQueue
        '
        Me.lstQueue.FormattingEnabled = True
        Me.lstQueue.Location = New System.Drawing.Point(6, 19)
        Me.lstQueue.Name = "lstQueue"
        Me.lstQueue.Size = New System.Drawing.Size(215, 134)
        Me.lstQueue.TabIndex = 8
        '
        'cmbAddOpt
        '
        Me.cmbAddOpt.FormattingEnabled = True
        Me.cmbAddOpt.Items.AddRange(New Object() {"AND", "OR"})
        Me.cmbAddOpt.Location = New System.Drawing.Point(338, 18)
        Me.cmbAddOpt.Name = "cmbAddOpt"
        Me.cmbAddOpt.Size = New System.Drawing.Size(47, 21)
        Me.cmbAddOpt.TabIndex = 9
        Me.cmbAddOpt.Text = "AND"
        Me.cmbAddOpt.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnSQL)
        Me.GroupBox3.Controls.Add(Me.txtSQL)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 274)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(452, 124)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "SQL Statment"
        '
        'btnSQL
        '
        Me.btnSQL.Location = New System.Drawing.Point(131, 92)
        Me.btnSQL.Name = "btnSQL"
        Me.btnSQL.Size = New System.Drawing.Size(180, 22)
        Me.btnSQL.TabIndex = 1
        Me.btnSQL.Text = "Generate SQL Statement"
        Me.btnSQL.UseVisualStyleBackColor = True
        '
        'txtSQL
        '
        Me.txtSQL.Location = New System.Drawing.Point(7, 20)
        Me.txtSQL.Multiline = True
        Me.txtSQL.Name = "txtSQL"
        Me.txtSQL.Size = New System.Drawing.Size(439, 63)
        Me.txtSQL.TabIndex = 0
        '
        'btnResults
        '
        Me.btnResults.Location = New System.Drawing.Point(383, 225)
        Me.btnResults.Name = "btnResults"
        Me.btnResults.Size = New System.Drawing.Size(75, 23)
        Me.btnResults.TabIndex = 11
        Me.btnResults.Text = "Get Results"
        Me.btnResults.UseVisualStyleBackColor = True
        '
        'btnShowSQL
        '
        Me.btnShowSQL.Location = New System.Drawing.Point(292, 225)
        Me.btnShowSQL.Name = "btnShowSQL"
        Me.btnShowSQL.Size = New System.Drawing.Size(75, 23)
        Me.btnShowSQL.TabIndex = 12
        Me.btnShowSQL.Text = "Show SQL"
        Me.btnShowSQL.UseVisualStyleBackColor = True
        '
        'btnHideSQL
        '
        Me.btnHideSQL.Location = New System.Drawing.Point(292, 224)
        Me.btnHideSQL.Name = "btnHideSQL"
        Me.btnHideSQL.Size = New System.Drawing.Size(75, 23)
        Me.btnHideSQL.TabIndex = 13
        Me.btnHideSQL.Text = "Hide SQL"
        Me.btnHideSQL.UseVisualStyleBackColor = True
        Me.btnHideSQL.Visible = False
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(13, 225)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(126, 23)
        Me.btnClear.TabIndex = 14
        Me.btnClear.Text = "Clear Search Queue"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmSearchConfig_RiflePistol
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 393)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnHideSQL)
        Me.Controls.Add(Me.btnShowSQL)
        Me.Controls.Add(Me.btnResults)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.cmbAddOpt)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.cmbCol)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtString)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.HelpProvider1.SetHelpKeyword(Me, "Search - Rifle & Pistol Configurations")
        Me.HelpProvider1.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.KeywordIndex)
        Me.HelpProvider1.SetHelpString(Me, "Search - Rifle & Pistol Configurations")
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSearchConfig_RiflePistol"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.Text = "Search Rifle and Pistol Configurations"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.SearchFieldsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtString As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbCol As System.Windows.Forms.ComboBox
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents SearchFieldsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Search_FieldsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.Search_FieldsTableAdapter
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstQueue As System.Windows.Forms.ListBox
    Friend WithEvents cmbAddOpt As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSQL As System.Windows.Forms.Button
    Friend WithEvents txtSQL As System.Windows.Forms.TextBox
    Friend WithEvents btnResults As System.Windows.Forms.Button
    Friend WithEvents btnShowSQL As System.Windows.Forms.Button
    Friend WithEvents btnHideSQL As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
End Class
