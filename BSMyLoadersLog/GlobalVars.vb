Imports BurnSoft.Applications.MLL.Global

Module GlobalVars
    ''' <summary>
    ''' The database path
    ''' </summary>
    Public DatabasePath as String
    ''' <summary>
    ''' The debug mode
    ''' </summary>
    Public DebugMode As Boolean
    ''' <summary>
    ''' The owner identifier
    ''' </summary>
    Public OwnerId As String
    'Public OwnerName As String
    Public OwnerLoadName As String
    'Public UseLogin As Boolean
    Public UseMyPwd As String
    Public UseMyUid As String
    Public UseMyForgotWord As String
    Public UseMyForgotPhrase As String
    Public IsLoggedIn As Boolean
    Public MyLogFile As String
    Public DoAutoBackup As Boolean
    Public DoOriginalImage As Boolean
    Public UseIndividualReports As Boolean
    'Public PersonalMark As Boolean
    Public LoadertypeShotgun As Boolean
    Public LoadertypeNonshotgun As Boolean
    Public Defaultlist As String
    Public MgcPath As String
    Public LastSucBackup As String
    Public AlertOnBackUp As Boolean
    Public TrackHistoryDays As Integer
    Public TrackHistory As Boolean
    Public ViewCups As Boolean
    Public ViewFps As Boolean
    Public Lastconfigedviewed As Long
    Public ApplicationPath As String
    Public ApplicationPathData As String

    Public Const MyHelpFile = GeneralSettings.MY_HELP_FILE

    Public Const MyHotfixFile = GeneralSettings.MY_HOTFIX_FILE

    Public Const MyBackup = GeneralSettings.MY_BACKUP

    Public Const MyRestore = GeneralSettings.MY_RESTORE

    Public Const MenuWiki = GeneralSettings.MENU_WIKI

    Public Const MenuShop = GeneralSettings.MENU_SHOP
   
    Public Const MenuBug = GeneralSettings.MENU_BUG
    
    Public Const MenuSupport = GeneralSettings.MENU_SUPPORT
    
    Public Const MenuSitesearch = GeneralSettings.MENU_SITESEARCH
    
    Public Const MenuLinks = GeneralSettings.MENU_LINKS
    
    Public Const WeightGrains1Lbs = WeightValues.WEIGHT_GRAINS_1LBS
    
    Public Const WeightGrains1Gm = WeightValues.WEIGHT_GRAINS_1GM
    
    Public Const WeightGrams1Lbs = WeightValues.WEIGHT_GRAMS_1LBS
    
    Public Const WeightOz1Lbs = WeightValues.WEIGHT_OZ_1LBS
    
    Public Const WeightGramsOz = WeightValues.WEIGHT_GRAMS_OZ
    Public Const DatabaseName = "MLL.mdb"
    Public Const UseShotgun As Boolean = True
    
    Public Sub LogError(ByVal sForm As String, ByVal sProcedure As String, ByVal iErrNo As Long, ByVal sErrorDesc As String)
        Dim objFs As New BurnSoft.Universal.FileIO
        Dim sMessage As String = sForm & "." & sProcedure & "::" & iErrNo & "::" & sErrorDesc
        objFs.LogFile(MyLogFile, sMessage)
    End Sub

End Module
