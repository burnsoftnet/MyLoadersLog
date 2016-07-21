VERSION 5.00
Begin VB.Form frmBackup1Disk 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "BurnSoft DB Backup"
   ClientHeight    =   1275
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   5820
   Icon            =   "frmBackup1Disk.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1275
   ScaleWidth      =   5820
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.Timer tmrAUtoBackup 
      Enabled         =   0   'False
      Interval        =   1000
      Left            =   4680
      Top             =   840
   End
   Begin VB.CheckBox Check1 
      Caption         =   "Cancel"
      Height          =   255
      Left            =   1080
      TabIndex        =   6
      Top             =   840
      Visible         =   0   'False
      Width           =   975
   End
   Begin VB.PictureBox Picture1 
      Height          =   255
      Left            =   240
      ScaleHeight     =   195
      ScaleWidth      =   4035
      TabIndex        =   5
      Top             =   480
      Visible         =   0   'False
      Width           =   4095
   End
   Begin VB.Timer Timer1 
      Left            =   5400
      Top             =   960
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Select Path"
      Height          =   375
      Left            =   4560
      TabIndex        =   4
      Top             =   360
      Width           =   1215
   End
   Begin VB.CommandButton cmdCancel 
      Caption         =   "&Exit"
      Height          =   255
      Left            =   3000
      TabIndex        =   1
      Top             =   840
      Width           =   1215
   End
   Begin VB.CommandButton cmdBackup 
      Caption         =   "&Backup"
      Height          =   255
      Left            =   960
      TabIndex        =   0
      Top             =   840
      Width           =   1215
   End
   Begin VB.Label lblPath 
      BorderStyle     =   1  'Fixed Single
      Height          =   255
      Left            =   240
      TabIndex        =   3
      Top             =   480
      Width           =   4095
   End
   Begin VB.Label Label1 
      Alignment       =   2  'Center
      Caption         =   "Select the Drive that you wish to Backup the DataBase."
      Height          =   255
      Left            =   360
      TabIndex        =   2
      Top             =   120
      Width           =   3975
   End
End
Attribute VB_Name = "frmBackup1Disk"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim SelectedPath As String
Dim i As Integer
Public BSReg As Registry
Public DoOver As Boolean
Public DoAutoBackup As Boolean
Const RegRoot = "HKLM"
Const RegKey = "Software\BurnSoft\BSMLL\"
Sub DoBackup()
    On Error GoTo ErrHandler
    cmdBackup.Enabled = False
    cmdCancel.Visible = False
    lblPath.Visible = False
    Label1.Caption = "Backing up database, Please Wait!"
    Picture1.Visible = True
    Command1.Visible = False
    Check1.Visible = True
    cmdBackup.Visible = False
    Dim strBackupFile As String
    Dim strFrom As String
    Dim strTo As String
    Set BSReg = New Registry
    strFrom = App.Path & "\MLL.mdb"
    strBackupFile = "MLL_" & Format(Now(), "YYYYMMDD") & "_" & Format(Now(), "HHNNSS") & ".bak"
    strTo = lblPath.Caption & strBackupFile
    Call BSReg.SaveString(RegRoot, RegKey & "Settings", "LastPath", lblPath.Caption)
    Call BSReg.SaveString(RegRoot, RegKey & "Settings", "LastFile", strBackupFile)
    If HardCopy(strFrom, strTo) Then
        MsgBox "Backup was Successful!", vbInformation, Me.Caption
        Call BSReg.SaveString(RegRoot, RegKey & "Settings", "Successful", Now)
        End
    Else
        If Check1.Value = 0 Then
            MsgBox "Backup Canceled!", vbCritical, Me.Caption
            Call BSReg.SaveString(RegRoot, RegKey & "Settings", "Canceled", Now)
        Else
            MsgBox "Backup Failed!", vbCritical, Me.Caption
            Call BSReg.SaveString(RegRoot, RegKey & "Settings", "Failed", Now)
        End If
        Kill (strTo)
        If Not DoOver Then End
    End If
    Label1.Caption = "Select the Drive that you wish to Backup the DataBase."
    cmdBackup.Enabled = True
    cmdBackup.Visible = True
    Check1.Visible = False
    Picture1.Visible = False
    lblPath.Visible = True
    If DoOver Then
        cmdBackup.Enabled = True
        cmdCancel.Visible = True
        lblPath.Visible = True
        Picture1.Visible = False
        Command1.Visible = True
        Check1.Visible = False
        cmdBackup.Visible = True
    End If
    Exit Sub
ErrHandler:
        Dim sMsg As String
        sMsg = Err.Number & "::" & Err.Description
        Call UpdateLog(sMsg, "frmBackup1Disk", "DoBackup")
        DoOver = True
        Resume Next
End Sub
Private Sub cmdBackup_Click()
    Call DoBackup
End Sub

Private Sub cmdCancel_Click()
    Check1.Value = 1
    End
End Sub

Private Sub Command1_Click()
    Dim WallPath2 As String
    On Error GoTo ErrHandler
    WallPath2 = BrowseForFolder(hWnd, "Choose folder.")
    If WallPath2 = "C:\" Or WallPath2 = "D:\" Or WallPath2 = "E:\" Or WallPath2 = "F:\" Or WallPath2 = "G:\" Or WallPath2 = "H:\" Or WallPath2 = "I:\" Or WallPath2 = "J:\" Or WallPath2 = "K:\" Or WallPath2 = "L:\" Or WallPath2 = "M:\" Or WallPath2 = "N:\" Or WallPath2 = "O:\" Or WallPath2 = "P:\" Or WallPath2 = "Q:\" Or WallPath2 = "R:\" Or WallPath2 = "S:\" Or WallPath2 = "T:\" Or WallPath2 = "U:\" Or WallPath2 = "V:\" Or WallPath2 = "W:\" Or WallPath2 = "X:\" Or WallPath2 = "Y:\" Or WallPath2 = "Z:\" Then
        SelectedPath = WallPath2
    Else
        SelectedPath = WallPath2 & "\"
    End If
    lblPath.Caption = SelectedPath
    Exit Sub
ErrHandler:
        Call UpdateLog(Err.Number & "::" & Err.Description, "frmBackup1Disk", "Command1_Click")
        Resume Next
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
    On Error GoTo ErrHandler
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
        Exit Function
ErrHandler:
    Dim sMsg As String
    sMsg = Err.Number & "::" & Err.Description & "||" & "Source=" & Source & ",Target=" & Target
    Dim intErr As Integer
    intErr = Err.Number
    Call UpdateLog(sMsg, "frmBackup1Disk", "HardCopy")
    Select Case intErr
        Case 76
            MsgBox ("Unable to Find Target Path, Please try again!.")
            DoOver = True
            Exit Function
        Case Else
            MsgBox ("Unknown error has occured. Please check your path to make sure it is vaild!")
            DoOver = True
            Exit Function
    End Select
    End Function
Private Sub Check1_MouseDown(Button As Integer, Shift As Integer, x As Single, Y As Single)
    If Check1.Value = 0 Then
        Check1.Value = 1
    Else
        Check1.Value = 0
    End If
End Sub

Private Sub Form_Initialize()
    If Len(Command()) > 0 Then
        Dim sCommand As String
        sCommand = Command()
        Select Case UCase(sCommand)
            Case "/AUTO"
                DoAutoBackup = True
        End Select
    Else
        DoAutoBackup = False
    End If
End Sub

Private Sub Form_Load()
    DoOver = False
    Dim AutoBackup As Boolean
    On Error GoTo ErrHandler
    lblPath.Caption = "Select A Path"
    Label1.Caption = "Select the Drive that you wish to Backup the DataBase."
    Set BSReg = New Registry
    lblPath.Caption = BSReg.GetString(RegRoot, RegKey & "Settings", "LastPath")
    Call DoDelete
    If DoAutoBackup Then tmrAUtoBackup.Enabled = True
    Exit Sub
ErrHandler:
            Call UpdateLog(Err.Number & "::" & Err.Description, "frmBackup1Disk", "Command1_Click")
            Resume Next
End Sub

Private Sub tmrAUtoBackup_Timer()
    If Not DoOver Then Call DoBackup
    tmrAUtoBackup.Enabled = False
End Sub
