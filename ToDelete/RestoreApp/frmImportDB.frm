VERSION 5.00
Begin VB.Form frmImportDB 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "BurnSoft DB Restore"
   ClientHeight    =   3915
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   5910
   Icon            =   "frmImportDB.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3915
   ScaleWidth      =   5910
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CheckBox chkRunApp 
      Height          =   255
      Left            =   2640
      TabIndex        =   9
      Top             =   960
      Width           =   3015
   End
   Begin VB.CheckBox Check1 
      Caption         =   "Cancel"
      Height          =   255
      Left            =   2280
      TabIndex        =   8
      Top             =   3360
      Visible         =   0   'False
      Width           =   1095
   End
   Begin VB.PictureBox Picture1 
      Height          =   255
      Left            =   240
      ScaleHeight     =   195
      ScaleWidth      =   5355
      TabIndex        =   7
      Top             =   2880
      Width           =   5415
   End
   Begin VB.FileListBox File1 
      Height          =   1455
      Left            =   240
      Pattern         =   "*.bak"
      TabIndex        =   5
      Top             =   1320
      Width           =   5415
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Select Path"
      Height          =   375
      Left            =   4440
      TabIndex        =   4
      Top             =   480
      Width           =   1215
   End
   Begin VB.CommandButton cmdCancel 
      Caption         =   "&Exit"
      Height          =   375
      Left            =   3840
      TabIndex        =   1
      Top             =   3360
      Width           =   1215
   End
   Begin VB.CommandButton cmdImport 
      Caption         =   "&Import"
      Height          =   375
      Left            =   480
      TabIndex        =   0
      Top             =   3360
      Width           =   1215
   End
   Begin VB.Label Label2 
      Caption         =   "Select one of the following Files:"
      Height          =   255
      Left            =   240
      TabIndex        =   6
      Top             =   960
      Width           =   2295
   End
   Begin VB.Label lblPath 
      BorderStyle     =   1  'Fixed Single
      Height          =   255
      Left            =   240
      TabIndex        =   3
      Top             =   600
      Width           =   3975
   End
   Begin VB.Label Label1 
      Alignment       =   2  'Center
      Caption         =   "Select the Drive/Directory were the Backup file(s) is at."
      Height          =   255
      Left            =   120
      TabIndex        =   2
      Top             =   120
      Width           =   4575
   End
End
Attribute VB_Name = "frmImportDB"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Public BSReg As Registry
Const RegRoot = "HKLM"
Const RegKey = "Software\BurnSoft\BSMLL\"
Const MainApp = "BSMyLoadersLog.exe"
Const MainAppName = "My Loaders Log"
Private Sub cmdCancel_Click()
Unload Me
End Sub
Private Sub cmdImport_Click()
'Call Import
lblPath.Enabled = False
Command1.Enabled = False
File1.Enabled = False
Check1.Visible = True
cmdImport.Enabled = False
Dim strFrom As String
Dim strTo As String
For X = 0 To File1.ListCount - 1
    If File1.Selected(X) = True Then
        strFrom = lblPath & File1.List(X)
        Exit For
    End If
Next X
strTo = App.Path & "\MLL.mdb"
If HardCopy(strFrom, strTo) Then
    MsgBox "Restore was Successful!", vbInformation, Me.Caption
    If chkRunApp.Value = 1 Then i = Shell(App.Path & "\" & MainApp, vbMaximizedFocus)
    End
Else
    If Check1.Value = 0 Then
        MsgBox "Restore Canceled!", vbCritical, Me.Caption
    Else
        MsgBox "Restore Failed!", vbCritical, Me.Caption
    End If
    Kill (strTo)
    End
End If
cmdImport.Enabled = True
Check1.Visible = False
lblPath.Enabled = True
Command1.Enabled = True
File1.Enabled = True
End Sub

Private Sub Command1_Click()
Dim WallPath2 As String

WallPath2 = BrowseForFolder(hWnd, "Choose folder.")
If WallPath2 = "C:\" Or WallPath2 = "D:\" Or WallPath2 = "E:\" Or WallPath2 = "F:\" Or WallPath2 = "G:\" Or WallPath2 = "H:\" Or WallPath2 = "I:\" Or WallPath2 = "J:\" Or WallPath2 = "K:\" Or WallPath2 = "L:\" Or WallPath2 = "M:\" Or WallPath2 = "N:\" Or WallPath2 = "O:\" Or WallPath2 = "P:\" Or WallPath2 = "Q:\" Or WallPath2 = "R:\" Or WallPath2 = "S:\" Or WallPath2 = "T:\" Or WallPath2 = "U:\" Or WallPath2 = "V:\" Or WallPath2 = "W:\" Or WallPath2 = "X:\" Or WallPath2 = "Y:\" Or WallPath2 = "Z:\" Then
    selectedpath = WallPath2
Else
    selectedpath = WallPath2 & "\"
End If
lblPath.Caption = selectedpath
File1.Path = selectedpath
End Sub

Private Sub Form_Load()
On Error GoTo ErrHandler
frmImportDB.Height = 4395
frmImportDB.Width = 6000
Set BSReg = New Registry
chkRunApp.Caption = "Run " & MainAppName & " after restore."
chkRunApp.Value = 1
lblPath.Caption = BSReg.GetString(RegRoot, RegKey & "Settings", "LastPath")
If Len(lblPath.Caption) > 0 Then
    File1.Path = lblPath.Caption
End If
Exit Sub
ErrHandler:
    lblPath.Caption = ""
End Sub

Sub SetStatus(Progressbar As Object, Percent As Integer, Optional Style As Integer, Optional Style2 As Integer)
    Progressbar.AutoRedraw = True
    Progressbar.Cls
    Progressbar.FontTransparent = True
    Progressbar.Tag = Percent
    Progressbar.ScaleWidth = 100
    Progressbar.ScaleHeight = 10
    Progressbar.DrawStyle = Style2
    Progressbar.DrawMode = 13
    Progressbar.FillStyle = Style
    Progressbar.Line (0, 0)-(Percent, Progressbar.ScaleHeight - 1), , BF
    Progressbar.Line (0, 0)-(Percent, Progressbar.ScaleHeight - 1), , B
    Progressbar.FontTransparent = False
    Progressbar.CurrentX = 50 - Progressbar.TextWidth(Percent & "%")
    Progressbar.CurrentY = (Progressbar.ScaleHeight / 2) - (Progressbar.TextHeight(Percent & "%") / 2)
    Progressbar.FontBold = True
    Progressbar.FontName = "small fonts"
    Progressbar.FontSize = 6
    Progressbar.Print " " & Percent & "% "
End Sub


Function HardCopy(Source As String, Target As String) As Boolean
    Dim A As Byte
    Dim File1 As Long
    Dim File2 As Long
    File1 = FreeFile
    Open Source For Random As #File1 Len = 1
    File2 = FreeFile
    Open Target For Binary As #File2 Len = 1

    Do While Not LOF(File1) < Seek(File1) And Not Check1.Value = 1
        Get #File1, Loc(File1) + 1, A
        Put #File2, , A
        If Loc(File1) / 128 = CLng(Loc(File1) / 128) Then
            SetStatus Me.Picture1, (100 / LOF(File1)) * Loc(File1), 0, 0
            DoEvents
            End If
        Loop
        If Not LOF(1) < Seek(1) Then
            HardCopy = False
            SetStatus Me.Picture1, 0, 3, 0
            If Check1.Value = 1 Then
                Caption = "USER CANCELED FILETRANSFER"
                Check1.Value = 0
            End If
        Else
            SetStatus Me.Picture1, 100, 3, 0
            HardCopy = True
        End If
        Close #2
        Close #1
    End Function
Private Sub Check1_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
    If Check1.Value = 0 Then
        Check1.Value = 1
    Else
        Check1.Value = 0
    End If
End Sub
