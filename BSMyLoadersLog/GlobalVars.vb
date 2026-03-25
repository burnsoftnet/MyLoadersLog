Imports BurnSoft.Applications.MLL.Global

''' <summary>
''' Class GlobalVars holds Global variables and Global Logging Function
''' </summary>
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
    ''' <summary>
    ''' The owner load name
    ''' </summary>
    Public OwnerLoadName As String
    ''' <summary>
    ''' The use my password
    ''' </summary>
    Public UseMyPwd As String
    ''' <summary>
    ''' The use my uid
    ''' </summary>
    Public UseMyUid As String
    ''' <summary>
    ''' The use my forgot word
    ''' </summary>
    Public UseMyForgotWord As String
    ''' <summary>
    ''' The use my forgot phrase
    ''' </summary>
    Public UseMyForgotPhrase As String
    ''' <summary>
    ''' The is logged in
    ''' </summary>
    Public IsLoggedIn As Boolean
    ''' <summary>
    ''' My log file
    ''' </summary>
    Public MyLogFile As String
    ''' <summary>
    ''' The do automatic backup
    ''' </summary>
    Public DoAutoBackup As Boolean
    ''' <summary>
    ''' The do original image
    ''' </summary>
    Public DoOriginalImage As Boolean
    ''' <summary>
    ''' The use individual reports
    ''' </summary>
    Public UseIndividualReports As Boolean
    ''' <summary>
    ''' The loadertype shotgun
    ''' </summary>
    Public LoaderTypeShotGun As Boolean
    ''' <summary>
    ''' The loader type metallic reloading
    ''' </summary>
    Public LoaderTypeMetalic As Boolean
    ''' <summary>
    ''' The defaultlist
    ''' </summary>
    Public Defaultlist As String
    ''' <summary>
    ''' The MGC path
    ''' </summary>
    Public MgcPath As String
    ''' <summary>
    ''' The last suc backup
    ''' </summary>
    Public LastSucBackup As String
    ''' <summary>
    ''' The alert on back up
    ''' </summary>
    Public AlertOnBackUp As Boolean
    ''' <summary>
    ''' The track history days
    ''' </summary>
    Public TrackHistoryDays As Integer
    ''' <summary>
    ''' The track history
    ''' </summary>
    Public TrackHistory As Boolean
    ''' <summary>
    ''' The view cups
    ''' </summary>
    Public ViewCups As Boolean
    ''' <summary>
    ''' The view FPS
    ''' </summary>
    Public ViewFps As Boolean
    ''' <summary>
    ''' The lastconfigedviewed
    ''' </summary>
    Public Lastconfigedviewed As Long
    ''' <summary>
    ''' The application path
    ''' </summary>
    Public ApplicationPath As String
    ''' <summary>
    ''' The application path data
    ''' </summary>
    Public ApplicationPathData As String
    '''' <summary>
    '''' Help File path
    '''' </summary>
    'Public Const MyHelpFile = GeneralSettings.MY_HELP_FILE
    ''' <summary>
    ''' Hotfixe file path
    ''' </summary>
    Public Const MyHotfixFile = GeneralSettings.MY_HOTFIX_FILE
    '''' <summary>
    '''' Backup application
    '''' </summary>
    'Public Const MyBackup = GeneralSettings.MY_BACKUP
    '''' <summary>
    '''' Restore application
    '''' </summary>
    'Public Const MyRestore = GeneralSettings.MY_RESTORE
    '''' <summary>
    '''' Wiki Link
    '''' </summary>
    'Public Const MenuWiki = GeneralSettings.MENU_WIKI
    '''' <summary>
    '''' Shop Menu, might not be used anymore
    '''' </summary>
    'Public Const MenuShop = GeneralSettings.MENU_SHOP
    '''' <summary>
    '''' Bug Report link
    '''' </summary>
    'Public Const MenuBug = GeneralSettings.MENU_BUG
    '''' <summary>
    '''' Support Link
    '''' </summary>
    'Public Const MenuSupport = GeneralSettings.MENU_SUPPORT
    '''' <summary>
    '''' Search Site, might not be used anymore
    '''' </summary>
    'Public Const MenuSitesearch = GeneralSettings.MENU_SITESEARCH
    '''' <summary>
    '''' Menu Links Might not Be used anymore
    '''' </summary>
    'Public Const MenuLinks = GeneralSettings.MENU_LINKS
    ''' <summary>
    ''' Weight Grains 1 lbs
    ''' </summary>
    Public Const WeightGrains1Lbs = WeightValues.WEIGHT_GRAINS_1LBS
    ''' <summary>
    ''' Weight in Grains 1 gm
    ''' </summary>
    Public Const WeightGrains1Gm = WeightValues.WEIGHT_GRAINS_1GM
    ''' <summary>
    ''' Weight in Grams for 1 lbs
    ''' </summary>
    Public Const WeightGrams1Lbs = WeightValues.WEIGHT_GRAMS_1LBS
    ''' <summary>
    ''' Weight in oz for 1 lbs
    ''' </summary>
    Public Const WeightOz1Lbs = WeightValues.WEIGHT_OZ_1LBS
    ''' <summary>
    ''' Weight in Grams from Ounce
    ''' </summary>
    Public Const WeightGramsOz = WeightValues.WEIGHT_GRAMS_OZ
    ''' <summary>
    ''' The Database Name
    ''' </summary>
    Public Const DatabaseName = "MLL.mdb"
    ''' <summary>
    ''' Use shotgun loading
    ''' </summary>
    Public Const UseShotgun As Boolean = True
    ''' <summary>
    ''' Logs the error.
    ''' </summary>
    ''' <param name="sForm">The s form.</param>
    ''' <param name="sProcedure">The s procedure.</param>
    ''' <param name="iErrNo">The i error no.</param>
    ''' <param name="sErrorDesc">The s error desc.</param>
    Public Sub LogError(ByVal sForm As String, ByVal sProcedure As String, ByVal iErrNo As Long, ByVal sErrorDesc As String)
        Dim objFs As New BurnSoft.Universal.FileIO
        Dim sMessage As String = sForm & "." & sProcedure & "::" & iErrNo & "::" & sErrorDesc
        objFs.LogFile(MyLogFile, sMessage)
    End Sub

End Module
