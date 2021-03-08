<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOptions))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtLoadName = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtLic = New System.Windows.Forms.TextBox
        Me.txtPhone = New System.Windows.Forms.TextBox
        Me.txtZip = New System.Windows.Forms.TextBox
        Me.txtState = New System.Windows.Forms.TextBox
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkShotGun = New System.Windows.Forms.CheckBox
        Me.chkRiflePistol = New System.Windows.Forms.CheckBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtWord = New System.Windows.Forms.TextBox
        Me.txtPhrase = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtCPWD = New System.Windows.Forms.TextBox
        Me.txtPWD = New System.Windows.Forms.TextBox
        Me.txtUID = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkSec = New System.Windows.Forms.CheckBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.chkBackupOnExit = New System.Windows.Forms.CheckBox
        Me.lblLastSuc = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.nudDays = New System.Windows.Forms.NumericUpDown
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.chkBAKCleanup = New System.Windows.Forms.CheckBox
        Me.chkAOBU = New System.Windows.Forms.CheckBox
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.chkViewCUPS = New System.Windows.Forms.CheckBox
        Me.chkViewFPS = New System.Windows.Forms.CheckBox
        Me.chkIPer = New System.Windows.Forms.CheckBox
        Me.chkDoOriginalImage = New System.Windows.Forms.CheckBox
        Me.cmbDefaultList = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnApply = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.nudDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(322, 321)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.ImageKey = "_14_16x16.ico"
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(314, 294)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "User Details"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtLoadName)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.txtLic)
        Me.GroupBox2.Controls.Add(Me.txtPhone)
        Me.GroupBox2.Controls.Add(Me.txtZip)
        Me.GroupBox2.Controls.Add(Me.txtState)
        Me.GroupBox2.Controls.Add(Me.txtCity)
        Me.GroupBox2.Controls.Add(Me.txtAddress)
        Me.GroupBox2.Controls.Add(Me.txtName)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(299, 226)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "General Information"
        '
        'txtLoadName
        '
        Me.txtLoadName.Location = New System.Drawing.Point(84, 13)
        Me.txtLoadName.Name = "txtLoadName"
        Me.txtLoadName.Size = New System.Drawing.Size(210, 20)
        Me.txtLoadName.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(4, 16)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(65, 13)
        Me.Label18.TabIndex = 28
        Me.Label18.Text = "Load Name:"
        '
        'txtLic
        '
        Me.txtLic.Location = New System.Drawing.Point(84, 193)
        Me.txtLic.Name = "txtLic"
        Me.txtLic.Size = New System.Drawing.Size(210, 20)
        Me.txtLic.TabIndex = 8
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(84, 167)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(210, 20)
        Me.txtPhone.TabIndex = 7
        '
        'txtZip
        '
        Me.txtZip.Location = New System.Drawing.Point(84, 141)
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(210, 20)
        Me.txtZip.TabIndex = 6
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(84, 115)
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(210, 20)
        Me.txtState.TabIndex = 5
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(84, 89)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(210, 20)
        Me.txtCity.TabIndex = 4
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(84, 63)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(210, 20)
        Me.txtAddress.TabIndex = 3
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(84, 38)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(210, 20)
        Me.txtName.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(4, 196)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "License #:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(4, 170)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Phone Number:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(4, 144)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 13)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Zip Code:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(4, 118)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "State:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(4, 92)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(27, 13)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "City:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(4, 66)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(48, 13)
        Me.Label13.TabIndex = 15
        Me.Label13.Text = "Address:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(4, 41)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(38, 13)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "Name:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkShotGun)
        Me.GroupBox1.Controls.Add(Me.chkRiflePistol)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 235)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(299, 53)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "User Loader Type"
        '
        'chkShotGun
        '
        Me.chkShotGun.AutoSize = True
        Me.chkShotGun.Location = New System.Drawing.Point(132, 20)
        Me.chkShotGun.Name = "chkShotGun"
        Me.chkShotGun.Size = New System.Drawing.Size(68, 17)
        Me.chkShotGun.TabIndex = 10
        Me.chkShotGun.Text = "ShotGun"
        Me.chkShotGun.UseVisualStyleBackColor = True
        '
        'chkRiflePistol
        '
        Me.chkRiflePistol.AutoSize = True
        Me.chkRiflePistol.Location = New System.Drawing.Point(7, 20)
        Me.chkRiflePistol.Name = "chkRiflePistol"
        Me.chkRiflePistol.Size = New System.Drawing.Size(96, 17)
        Me.chkRiflePistol.TabIndex = 9
        Me.chkRiflePistol.Text = "Rifle and Pistol"
        Me.chkRiflePistol.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.chkSec)
        Me.TabPage2.ImageKey = "Computer_System_53_32x32.ico"
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(314, 294)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Security"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 235)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(296, 35)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "NOTE: The Current Username will be Admin if a Login Name is not assigned."
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtWord)
        Me.GroupBox3.Controls.Add(Me.txtPhrase)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.txtCPWD)
        Me.GroupBox3.Controls.Add(Me.txtPWD)
        Me.GroupBox3.Controls.Add(Me.txtUID)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(9, 40)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(296, 192)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Security Information"
        '
        'txtWord
        '
        Me.txtWord.Location = New System.Drawing.Point(10, 161)
        Me.txtWord.Name = "txtWord"
        Me.txtWord.Size = New System.Drawing.Size(256, 20)
        Me.txtWord.TabIndex = 6
        '
        'txtPhrase
        '
        Me.txtPhrase.Location = New System.Drawing.Point(10, 122)
        Me.txtPhrase.Name = "txtPhrase"
        Me.txtPhrase.Size = New System.Drawing.Size(256, 20)
        Me.txtPhrase.TabIndex = 5
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(7, 145)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(118, 13)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "Forgot Password Word:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(7, 106)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(122, 13)
        Me.Label17.TabIndex = 9
        Me.Label17.Text = "Forgot Password Phrase"
        '
        'txtCPWD
        '
        Me.txtCPWD.Location = New System.Drawing.Point(103, 73)
        Me.txtCPWD.Name = "txtCPWD"
        Me.txtCPWD.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtCPWD.Size = New System.Drawing.Size(187, 20)
        Me.txtCPWD.TabIndex = 4
        '
        'txtPWD
        '
        Me.txtPWD.Location = New System.Drawing.Point(103, 46)
        Me.txtPWD.Name = "txtPWD"
        Me.txtPWD.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPWD.Size = New System.Drawing.Size(187, 20)
        Me.txtPWD.TabIndex = 3
        '
        'txtUID
        '
        Me.txtUID.Location = New System.Drawing.Point(103, 20)
        Me.txtUID.Name = "txtUID"
        Me.txtUID.Size = New System.Drawing.Size(187, 20)
        Me.txtUID.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Confirm Password:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Password:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Login Name:"
        '
        'chkSec
        '
        Me.chkSec.AutoSize = True
        Me.chkSec.Location = New System.Drawing.Point(9, 16)
        Me.chkSec.Name = "chkSec"
        Me.chkSec.Size = New System.Drawing.Size(144, 17)
        Me.chkSec.TabIndex = 1
        Me.chkSec.Text = "Enable Security Features"
        Me.chkSec.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.chkBackupOnExit)
        Me.TabPage3.Controls.Add(Me.lblLastSuc)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.nudDays)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.Label15)
        Me.TabPage3.Controls.Add(Me.chkBAKCleanup)
        Me.TabPage3.Controls.Add(Me.chkAOBU)
        Me.TabPage3.ImageKey = "Winxpicons_Disk_2_32x32.ico"
        Me.TabPage3.Location = New System.Drawing.Point(4, 23)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(314, 294)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Backups"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'chkBackupOnExit
        '
        Me.chkBackupOnExit.AutoSize = True
        Me.chkBackupOnExit.Location = New System.Drawing.Point(8, 32)
        Me.chkBackupOnExit.Name = "chkBackupOnExit"
        Me.chkBackupOnExit.Size = New System.Drawing.Size(98, 17)
        Me.chkBackupOnExit.TabIndex = 15
        Me.chkBackupOnExit.Text = "Backup on Exit"
        Me.chkBackupOnExit.UseVisualStyleBackColor = True
        '
        'lblLastSuc
        '
        Me.lblLastSuc.AutoSize = True
        Me.lblLastSuc.Location = New System.Drawing.Point(162, 109)
        Me.lblLastSuc.Name = "lblLastSuc"
        Me.lblLastSuc.Size = New System.Drawing.Size(0, 13)
        Me.lblLastSuc.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 109)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(151, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Last Successful Backup Date:"
        '
        'nudDays
        '
        Me.nudDays.Location = New System.Drawing.Point(60, 78)
        Me.nudDays.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudDays.Name = "nudDays"
        Me.nudDays.Size = New System.Drawing.Size(48, 20)
        Me.nudDays.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(114, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Days."
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(5, 80)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(49, 13)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "Track by"
        '
        'chkBAKCleanup
        '
        Me.chkBAKCleanup.AutoSize = True
        Me.chkBAKCleanup.Location = New System.Drawing.Point(8, 55)
        Me.chkBAKCleanup.Name = "chkBAKCleanup"
        Me.chkBAKCleanup.Size = New System.Drawing.Size(186, 17)
        Me.chkBAKCleanup.TabIndex = 9
        Me.chkBAKCleanup.Text = "Automatically Delete Old Backups"
        Me.chkBAKCleanup.UseVisualStyleBackColor = True
        '
        'chkAOBU
        '
        Me.chkAOBU.AutoSize = True
        Me.chkAOBU.Location = New System.Drawing.Point(8, 13)
        Me.chkAOBU.Name = "chkAOBU"
        Me.chkAOBU.Size = New System.Drawing.Size(153, 17)
        Me.chkAOBU.TabIndex = 8
        Me.chkAOBU.Text = "Alert On Last Backup Time"
        Me.chkAOBU.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.chkViewCUPS)
        Me.TabPage4.Controls.Add(Me.chkViewFPS)
        Me.TabPage4.Controls.Add(Me.chkIPer)
        Me.TabPage4.Controls.Add(Me.chkDoOriginalImage)
        Me.TabPage4.Controls.Add(Me.cmbDefaultList)
        Me.TabPage4.Controls.Add(Me.Label5)
        Me.TabPage4.ImageKey = "book_16x16.ico"
        Me.TabPage4.Location = New System.Drawing.Point(4, 23)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(314, 294)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Misc."
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'chkViewCUPS
        '
        Me.chkViewCUPS.AutoSize = True
        Me.chkViewCUPS.Location = New System.Drawing.Point(13, 129)
        Me.chkViewCUPS.Name = "chkViewCUPS"
        Me.chkViewCUPS.Size = New System.Drawing.Size(183, 17)
        Me.chkViewCUPS.TabIndex = 6
        Me.chkViewCUPS.Text = "View CUPS in Configuration View"
        Me.chkViewCUPS.UseVisualStyleBackColor = True
        '
        'chkViewFPS
        '
        Me.chkViewFPS.AutoSize = True
        Me.chkViewFPS.Location = New System.Drawing.Point(13, 105)
        Me.chkViewFPS.Name = "chkViewFPS"
        Me.chkViewFPS.Size = New System.Drawing.Size(174, 17)
        Me.chkViewFPS.TabIndex = 5
        Me.chkViewFPS.Text = "View FPS in Configuration View"
        Me.chkViewFPS.UseVisualStyleBackColor = True
        '
        'chkIPer
        '
        Me.chkIPer.AutoSize = True
        Me.chkIPer.Location = New System.Drawing.Point(13, 81)
        Me.chkIPer.Name = "chkIPer"
        Me.chkIPer.Size = New System.Drawing.Size(175, 17)
        Me.chkIPer.TabIndex = 4
        Me.chkIPer.Text = "Use Personal Reports Markings"
        Me.chkIPer.UseVisualStyleBackColor = True
        '
        'chkDoOriginalImage
        '
        Me.chkDoOriginalImage.AutoSize = True
        Me.chkDoOriginalImage.Location = New System.Drawing.Point(13, 58)
        Me.chkDoOriginalImage.Name = "chkDoOriginalImage"
        Me.chkDoOriginalImage.Size = New System.Drawing.Size(253, 17)
        Me.chkDoOriginalImage.TabIndex = 3
        Me.chkDoOriginalImage.Text = "Use Picture Original Size or close to Original size"
        Me.chkDoOriginalImage.UseVisualStyleBackColor = True
        '
        'cmbDefaultList
        '
        Me.cmbDefaultList.FormattingEnabled = True
        Me.cmbDefaultList.Items.AddRange(New Object() {"Caliber List", "Configuration List"})
        Me.cmbDefaultList.Location = New System.Drawing.Point(126, 22)
        Me.cmbDefaultList.Name = "cmbDefaultList"
        Me.cmbDefaultList.Size = New System.Drawing.Size(130, 21)
        Me.cmbDefaultList.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Default Side List:"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "_14_16x16.ico")
        Me.ImageList1.Images.SetKeyName(1, "Computer_System_53_32x32.ico")
        Me.ImageList1.Images.SetKeyName(2, "Winxpicons_Disk_2_32x32.ico")
        Me.ImageList1.Images.SetKeyName(3, "book_16x16.ico")
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(10, 328)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(116, 328)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 12
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(228, 328)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 13
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "my_loaders_log_help.chm"
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(321, 361)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmOptions"
        Me.Text = "Options"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.nudDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtLic As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkShotGun As System.Windows.Forms.CheckBox
    Friend WithEvents chkRiflePistol As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents chkSec As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCPWD As System.Windows.Forms.TextBox
    Friend WithEvents txtPWD As System.Windows.Forms.TextBox
    Friend WithEvents txtUID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDefaultList As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkBackupOnExit As System.Windows.Forms.CheckBox
    Friend WithEvents lblLastSuc As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents nudDays As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents chkBAKCleanup As System.Windows.Forms.CheckBox
    Friend WithEvents chkAOBU As System.Windows.Forms.CheckBox
    Friend WithEvents chkIPer As System.Windows.Forms.CheckBox
    Friend WithEvents chkDoOriginalImage As System.Windows.Forms.CheckBox
    Friend WithEvents txtWord As System.Windows.Forms.TextBox
    Friend WithEvents txtPhrase As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtLoadName As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents chkViewCUPS As System.Windows.Forms.CheckBox
    Friend WithEvents chkViewFPS As System.Windows.Forms.CheckBox
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
End Class
