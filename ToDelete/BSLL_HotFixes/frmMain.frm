VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   0  'None
   Caption         =   "My Gun Collection Database Updater"
   ClientHeight    =   2145
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   4650
   Icon            =   "frmMain.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2145
   ScaleWidth      =   4650
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.Timer Timer1 
      Interval        =   1000
      Left            =   4080
      Top             =   480
   End
   Begin VB.PictureBox Picture1 
      AutoRedraw      =   -1  'True
      AutoSize        =   -1  'True
      Height          =   420
      Left            =   120
      Picture         =   "frmMain.frx":0CCA
      ScaleHeight     =   360
      ScaleWidth      =   360
      TabIndex        =   3
      Top             =   120
      Width           =   420
   End
   Begin VB.ListBox List1 
      Height          =   1035
      ItemData        =   "frmMain.frx":10EF
      Left            =   120
      List            =   "frmMain.frx":10F1
      TabIndex        =   1
      Top             =   960
      Width           =   4335
   End
   Begin VB.Label Label2 
      AutoSize        =   -1  'True
      Caption         =   "My Loaders Log Hotfix Application"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   240
      Left            =   720
      TabIndex        =   2
      Top             =   240
      Width           =   3555
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "Applying Updates Please Wait."
      Height          =   195
      Left            =   720
      TabIndex        =   0
      Top             =   600
      Width           =   2190
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim IsRunning As Boolean
Dim ReDo As Boolean
Sub RunApp()
    Dim sAns As String
    Dim UpdateMsg As String
    UpdateMsg = ""
    sAns = vbYes
    IsRunning = True
    strDBPath = GetDatabasePath
    MainApp = MainApplication
    CurPath = CurrentAppPath
'    Call RemovePassword
    Call AddPassword
    Call RegHotfixExists
    Call DoUpdates
    Call DeleteUpdateFile
    If HotFixApplied Then
        UpdateMsg = "Updates were applied!"
        sAns = vbYes
    Else
        sAns = MsgBox("No updates where applied.  You are currently up-to-date!" & Chr(13) & "Do you wish to start the My Loaders Log Application Now?", vbYesNo, "BSMLL Hotfix")
    End If
    If sAns = vbYes Then
        Dim i As Long
        i = Shell(MainApp, vbMaximizedFocus)
    End If
    End
End Sub

Private Sub Form_Initialize()
    ReDo = False
    Dim sCmd As String
    sCmd = Command()
    If sCmd = "/redo" Then ReDo = True
End Sub

Private Sub Form_Load()
    If ReDo Then Call RedoAll
    IsRunning = False
    HotFixApplied = False
End Sub

Private Sub Timer1_Timer()
    If Not IsRunning Then
        Call RunApp
        Timer1.Enabled = False
    End If
End Sub
