<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport_Configuration_Sheet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReport_Configuration_Sheet))
        Me.Config_List_Powder_Data_NSG_ViewBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MLLDataSet = New BSMyLoadersLog.MLLDataSet
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.Config_List_Powder_Data_NSG_ViewTableAdapter = New BSMyLoadersLog.MLLDataSetTableAdapters.Config_List_Powder_Data_NSG_ViewTableAdapter
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        CType(Me.Config_List_Powder_Data_NSG_ViewBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Config_List_Powder_Data_NSG_ViewBindingSource
        '
        Me.Config_List_Powder_Data_NSG_ViewBindingSource.DataMember = "Config_List_Powder_Data_NSG_View"
        Me.Config_List_Powder_Data_NSG_ViewBindingSource.DataSource = Me.MLLDataSet
        '
        'MLLDataSet
        '
        Me.MLLDataSet.DataSetName = "MLLDataSet"
        Me.MLLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "MLLDataSet_Config_List_Powder_Data_NSG_View"
        ReportDataSource1.Value = Me.Config_List_Powder_Data_NSG_ViewBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "BSMyLoadersLog.Report_Config_RiflePistol.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(608, 430)
        Me.ReportViewer1.TabIndex = 1
        '
        'Config_List_Powder_Data_NSG_ViewTableAdapter
        '
        Me.Config_List_Powder_Data_NSG_ViewTableAdapter.ClearBeforeFill = True
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'frmReport_Configuration_Sheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 430)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmReport_Configuration_Sheet"
        Me.Text = "Configuration Sheet Report"
        CType(Me.Config_List_Powder_Data_NSG_ViewBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MLLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Config_List_Powder_Data_NSG_ViewBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MLLDataSet As BSMyLoadersLog.MLLDataSet
    Friend WithEvents Config_List_Powder_Data_NSG_ViewTableAdapter As BSMyLoadersLog.MLLDataSetTableAdapters.Config_List_Powder_Data_NSG_ViewTableAdapter
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
End Class
