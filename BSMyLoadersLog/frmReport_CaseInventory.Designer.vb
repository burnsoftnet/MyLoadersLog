<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport_CaseInventory
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
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReport_CaseInventory))
        Me.List_Case_DetailsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.List_Case_DetailsTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.List_Case_DetailsTableAdapter
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        CType(Me.List_Case_DetailsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'List_Case_DetailsBindingSource
        '
        Me.List_Case_DetailsBindingSource.DataMember = "List_Case_Details"
        Me.List_Case_DetailsBindingSource.DataSource = Me.MLLDataSet
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ToolStripComboBox1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(684, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(46, 22)
        Me.ToolStripLabel1.Text = "Sort By:"
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.Items.AddRange(New Object() {"All Manufacturer", "All Caliber", "All Trim to Length", "All Times Used", "All Qty", "All Price", "Instock Manufacturer", "Instock Caliber", "Instock Trim to Length", "Instock Times Used", "Instock Qty", "Instock Price", "Out-Of-Stock Manufacturer", "Out-Of-Stock Caliber", "Out-Of-Stock Trim to Length", "Out-Of-Stock Times Used", "Out-Of-Stock Qty", "Out-Of-Stock Price"})
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(121, 25)
        Me.ToolStripComboBox1.Text = "Default"
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "MLLDataSet_List_Case_Details"
        ReportDataSource1.Value = Me.List_Case_DetailsBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "BSMyLoadersLog.Report_CaseInventory.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 25)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(684, 337)
        Me.ReportViewer1.TabIndex = 1
        '
        'List_Case_DetailsTableAdapter
        '
        Me.List_Case_DetailsTableAdapter.ClearBeforeFill = True
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'frmReport_CaseInventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 362)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.HelpProvider1.SetHelpKeyword(Me, "Case Inventory")
        Me.HelpProvider1.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.KeywordIndex)
        Me.HelpProvider1.SetHelpString(Me, "Case Inventory")
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmReport_CaseInventory"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.Text = "Case/Brass Inventory Report"
        CType(Me.List_Case_DetailsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripComboBox1 As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents List_Case_DetailsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents List_Case_DetailsTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.List_Case_DetailsTableAdapter
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
End Class
