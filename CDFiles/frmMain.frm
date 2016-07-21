VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Burnsoft - My My Loaders Log v1.x"
   ClientHeight    =   5955
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   7560
   Icon            =   "frmMain.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5955
   ScaleWidth      =   7560
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame Frame1 
      Caption         =   "Extras"
      Height          =   1335
      Left            =   2280
      TabIndex        =   3
      Top             =   4560
      Width           =   5175
      Begin VB.CommandButton Command8 
         Caption         =   "My Gun Collection Presentation"
         Height          =   495
         Left            =   2880
         TabIndex        =   8
         Top             =   600
         Width           =   2175
      End
      Begin VB.CommandButton Command4 
         Caption         =   "Try My Gun Collection"
         Height          =   255
         Left            =   2880
         TabIndex        =   7
         Top             =   240
         Width           =   2175
      End
      Begin VB.CommandButton Command7 
         Caption         =   "MDAC 2.7"
         Height          =   255
         Left            =   240
         TabIndex        =   6
         Top             =   960
         Width           =   2535
      End
      Begin VB.CommandButton Command6 
         Caption         =   "Jet Database Engine 4.3"
         Height          =   255
         Left            =   240
         TabIndex        =   5
         Top             =   600
         Width           =   2535
      End
      Begin VB.CommandButton Command5 
         Caption         =   "Microsoft .Net Framwework 2.0"
         Height          =   255
         Left            =   240
         TabIndex        =   4
         Top             =   240
         Width           =   2535
      End
   End
   Begin VB.CommandButton Command3 
      Caption         =   "&Presentation"
      Height          =   255
      Left            =   120
      TabIndex        =   2
      Top             =   5040
      Width           =   1815
   End
   Begin VB.CommandButton Command2 
      Caption         =   "E&xit"
      Height          =   255
      Left            =   120
      TabIndex        =   1
      Top             =   5400
      Width           =   1815
   End
   Begin VB.CommandButton Command1 
      Caption         =   "&Install"
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   4680
      Width           =   1815
   End
   Begin VB.Image Image1 
      Height          =   4515
      Left            =   0
      Picture         =   "frmMain.frx":0CCA
      Stretch         =   -1  'True
      Top             =   0
      Width           =   7545
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Const DOUPGRADE = False
Const DOPRES = True
Const INSTALLFILE = "setup.exe"
Const UPGRADEFILE = ""
Const PRESENTATIONFILE = "MyLoadersLog.exe"
Public sPath As String
Private Sub Command1_Click()
Dim Install As String
Install = Shell(INSTALLFILE, 1)
End Sub
Private Sub Command2_Click()
End
End Sub
Private Sub Command3_Click()
Dim Prez As String
Prez = Shell(PRESENTATIONFILE, 1)
End Sub
Private Sub Command4_Click()
Dim strPath As String
Dim iExtra As Integer
strPath = sPath & "Extras\MGC\setup.exe"
iExtra = Shell(strPath, 1)
End Sub

Private Sub Command5_Click()
Dim strPath As String
Dim iExtra As Integer
strPath = sPath & "Extras\DotNetFramework20\dotnetfx.exe"
iExtra = Shell(strPath, 1)
End Sub

Private Sub Command6_Click()
Dim strPath As String
Dim iExtra As Integer
strPath = sPath & "Extras\JetDatabaseEngine\jet40sp3_comp.exe"
iExtra = Shell(strPath, 1)
End Sub

Private Sub Command7_Click()
Dim strPath As String
Dim iExtra As Integer
strPath = sPath & "Extras\MDAC\mdac_typ.exe"
iExtra = Shell(strPath, 1)
End Sub

Private Sub Command8_Click()
Dim strPath As String
Dim iExtra As Integer
strPath = sPath & "Extras\MGC\MyGunCollection.exe"
iExtra = Shell(strPath, 1)
End Sub

Private Sub Form_Load()
Command3.Enabled = DOPRES
sPath = App.Path
End Sub
