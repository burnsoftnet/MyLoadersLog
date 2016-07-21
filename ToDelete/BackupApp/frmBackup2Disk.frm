VERSION 5.00
Begin VB.Form rmBackup2Disk 
   Caption         =   "Backup to Disk"
   ClientHeight    =   540
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   Icon            =   "frmBackup2Disk.frx":0000
   LinkTopic       =   "Form2"
   MDIChild        =   -1  'True
   ScaleHeight     =   540
   ScaleWidth      =   4680
   Begin VB.CommandButton cmdBackup 
      Caption         =   "&Backup Now!"
      Height          =   375
      Left            =   3000
      TabIndex        =   2
      Top             =   120
      Width           =   1215
   End
   Begin VB.DriveListBox Drive1 
      Height          =   315
      Left            =   1320
      TabIndex        =   1
      Top             =   120
      Width           =   1215
   End
   Begin VB.Label Label1 
      Caption         =   "Select Target:"
      Height          =   255
      Left            =   0
      TabIndex        =   0
      Top             =   120
      Width           =   1095
   End
End
Attribute VB_Name = "rmBackup2Disk"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub cmdBackup_Click()
Call Copy
End Sub

Private Sub Form_Load()
frmBackup2Disk.Height = 945
frmBackup2Disk.Width = 4800
End Sub
