Module modHotFixes
    Public HotFixApplied As Boolean
    Public Sub DoUpdates()
        If Not HotFixExists("1") Then Call HotFix_1()
        If Not HotFixExists("2") Then Call HotFix_2()
        'If Not HotFixExists("3") Then Call HotFix_3()
        'If Not HotFixExists("4") Then Call HotFix_4()
        'If Not HotFixExists("5") Then Call HotFix_5()
        'If Not HotFixExists("6") Then Call HotFix_6()
        'If Not HotFixExists("7") Then Call HotFix_7
    End Sub
    Sub RedoAll()
        Call DelRegValue("HotFix", "1", "")
        Call DelRegValue("HotFix", "2", "")
        'Call DelRegValue("HotFix", "3", "")
        'Call DelRegValue("HotFix", "4", "")
        'Call DelRegValue("HotFix", "5", "")
        'Call DelRegValue("HotFix", "6", "")
        'Call DelRegValue("HotFix", "7", "")
        Call DelRegValue("HotFix", "LastUpdate", "")
    End Sub
    Sub HotFix_1()
        Dim strUpdateName As String
        strUpdateName = "1"
        Call DebugLog("HotFix_" & strUpdateName, "Starting HotFix " & strUpdateName)
        'INSERT UPDATES HERE!!
        Dim SQL As String = ""
        Call DropTable("List_SG_ShotType_Details_Slugs")
        Call DropTable("List_SG_ShotType_Details_Shot")
        Call DropTable("List_SG_ShotType")
        Err.Clear()
        SQL = "CREATE TABLE List_SG_ShotType_Details (ID AUTOINCREMENT PRIMARY KEY,Manufacturer Text(255),NAME Text(255),IsSlug INTEGER,mat Text(255),ShotNo Text(255),weight Text(255),CAL Text(255),Qty INTEGER,EPPS DOUBLE,Price DOUBLE,ounces DOUBLE, grams DOUBLE,sync_lastupdate DATETIME);"
        Call RunSQL(SQL)
        Call AddColumn("load_t", "List_SG_WAD", "N/A", "Text(255)")
        Call AddColumn("load_d", "List_SG_WAD", "0", "Double")
        Call AddColumn("gauge", "List_SG_WAD", "N/A", "Text(255)")
        Call AddColumn("GID", "List_SG_WAD", "0", "Number")
        Call AddColumn("SW_t", "Config_List_Data_SG", "N/A", "Text(255)")
        Call AddColumn("sCharge", "List_SG_Bushing", "N/A", "Text(255)")
        Call AddColumn("ForShot", "List_SG_Bushing", "N/A", "Number")
        Call AddColumn("ForPowder", "List_SG_Bushing", "N/A", "Number")
        Call AlterColumn("SW", "Config_List_Data_SG", "N/A", "Double")
        Call AddColumn("GID", "Config_List_Data_SG", "N/A", "Number")
        Call AddColumn("IsPersonal", "Config_List_Data_SG", "N/A", "Number")
        Call AddColumn("LTID", "Config_List_Data_SG", "N/A", "Number")
        Call AddColumn("BushingID", "Config_List_Data_SG", "N/A", "Number")
        Call AddColumn("ChargeBarID", "Config_List_Data_SG", "N/A", "Number")
        Call AddColumn("IsPref", "Config_List_Powder_Data_SG", "N/A", "Number")

        Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,mat,ShotNo,weight,price,ounces) VALUES('Lawrence','Magnum Lead Shot',0,'Lead','9','25',29.99,400)")
        Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,mat,ShotNo,weight,price,ounces) VALUES('Lawrence','Magnum Lead Shot',0,'Lead','4','25',39.99,400)")
        Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,mat,ShotNo,weight,price,ounces) VALUES('Winchester','Steel Shot',0,'Steel','4','0',0,0)")
        Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,mat,ShotNo,weight,price,ounces) VALUES('Winchester','Lead Shot',0,'Lead','7','0',25,0)")
        Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,weight,cal,qty,epps,price) VALUES('Gualandi','Hunting Shotshell Slug',1,'1-1/8 oz','12 Gauge',25,0.6916,17.29)")
        Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,weight,cal,qty,epps,price) VALUES('Gualandi','Hunting Shotshell Slug',1,'7/8 oz','20 Gauge',25,0.5596,13.99)")
        Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,weight,cal,qty,epps,price) VALUES('BPI-Hornady','Ballistic Sabot XTP Jacketed Flat Nose Magnum',1,'500 Grain','12 Gauge',20,1.0445,20.89)")
        Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,weight,cal,qty,epps,price) VALUES('BPI AQ','Shotshell Slug',1,'1oz','12 Gauge',25,0.5996,14.99)")
        If Not ValueDoesExist("List_SG_Gauge", "ga", "20 Gauge") Then Call RunSQL("UPDATE List_SG_Gauge set ga='20 Gauge' where ID=1")
        If Not ValueDoesExist("List_SG_Gauge", "ga", "12 Gauge") Then Call RunSQL("UPDATE List_SG_Gauge set ga='12 Gauge' where ID=2")
        If Not ValueDoesExist("List_SG_Gauge", "ga", "28 Gauge") Then Call RunSQL("UPDATE List_SG_Gauge set ga='28 Gauge' where ID=3")
        If Not ValueDoesExist("List_SG_Gauge", "ga", "10 Gauge") Then Call RunSQL("UPDATE List_SG_Gauge set ga='10 Gauge' where ID=4")
        If Not ValueDoesExist("List_SG_Gauge", "ga", "18 Gauge") Then Call RunSQL("UPDATE List_SG_Gauge set ga='18 Gauge' where ID=5")
        If Not ValueDoesExist("List_SG_Gauge", "ga", "410 Gauge") Then Call RunSQL("UPDATE List_SG_Gauge set ga='410 Gauge' where ID=6")
        If Not ValueDoesExist("List_SG_Gauge", "ga", "16 Gauge") Then Call RunSQL("INSERT INTO List_SG_Gauge(ga) Values('16 Gauge')")
        SQL = "SELECT [Config_List_Powder_Data_SG].[CLNID], [Config_List_Powder_Data_SG].[Load_Mid], [Config_List_Powder_Data_SG].[FPS_MID], [Config_List_Powder_Data_SG].[PSI_Mid], [Config_List_Powder_Data_SG].[IsPref], [List_SG_ShotCharge_Loads].[Name] AS ShotChargeName, [Config_List_Name].[ConfigName], [Config_List_Name].[Notes], [Config_List_Data_SG].[SW], [Config_List_Data_SG].[SW_t], [Config_List_Data_SG].[Source], [General_Powder].[Manufacturer] AS PowderManu, [General_Powder].[Name] AS PowderName, [General_Primer].[Manufacturer] AS PrimerManu, [General_Primer].[Name] AS PrimerName, [List_SG_WAD].[Manufacturer] AS WADManu, [List_SG_WAD].[WAD] AS WADName, [List_SG_ShotType_Details].[Manufacturer] AS ShotManu, [List_SG_ShotType_Details].[NAME] AS ShotName, [List_SG_ShotType_Details].[IsSlug], [List_SG_ShotType_Details].[mat], [List_SG_ShotType_Details].[ShotNo], [List_SG_ShotType_Details].[weight], [List_SG_ShotType_Details].[CAL], [List_SG_Case].[Manufacturer] AS CaseManu, [List_SG_Case].[Name] AS CaseName, [List_SG_Case].[Gauge], [List_SG_Case].[Length] " & _
                "FROM List_SG_Case INNER JOIN (List_SG_ShotType_Details INNER JOIN (List_SG_WAD INNER JOIN ((((Config_List_Name INNER JOIN Config_List_Powder_Data_SG ON [Config_List_Name].[ID]=[Config_List_Powder_Data_SG].[CLNID]) INNER JOIN General_Powder ON [Config_List_Powder_Data_SG].[PID]=[General_Powder].[ID]) INNER JOIN (Config_List_Data_SG INNER JOIN General_Primer ON [Config_List_Data_SG].[PRID]=[General_Primer].[ID]) ON [Config_List_Powder_Data_SG].[CLNID]=[Config_List_Data_SG].[CLNID]) INNER JOIN List_SG_ShotCharge_Loads ON [Config_List_Data_SG].[LTID]=[List_SG_ShotCharge_Loads].[ID]) ON [List_SG_WAD].[ID]=[Config_List_Data_SG].[WAD]) ON [List_SG_ShotType_Details].[ID]=[Config_List_Data_SG].[SCL]) ON [List_SG_Case].[ID]=[Config_List_Data_SG].[CALID];"
        Call CreateView("qry_CFG_SR_PowderList_SG", SQL)

        'End Updates
        Call UpdateLastUpdate(strUpdateName)
        Call AppliedUpdates(strUpdateName)
        Console.WriteLine("HotFix_" & strUpdateName & " was applied!")
        HotFixApplied = True
    End Sub
    Sub HotFix_2()
        Dim strUpdateName As String
        strUpdateName = "2"
        Call DebugLog("HotFix_" & strUpdateName, "Starting HotFix " & strUpdateName)
        'INSERT UPDATES HERE!!
        Dim SQL As String = ""
        Dim DBVersion As String = "2.1"
        SQL = "CREATE TABLE List_Bullets_SupportingCaliber (ID AUTOINCREMENT PRIMARY KEY,BID INTEGER,CID INTEGER,sync_lastupdate DATETIME);"
        Call RunSQL(SQL)
        Call AddColumn("DRAM", "List_SG_Case", "N/A", "Text(255)")
        Call AlterColumn("Load_Min", "Config_List_Powder_Data_SG", "N/A", "Double")
        Call AlterColumn("Load_Mid", "Config_List_Powder_Data_SG", "N/A", "Double")
        Call AlterColumn("Load_Max", "Config_List_Powder_Data_SG", "N/A", "Double")
        SQL = "CREATE TABLE sync_tables(ID AUTOINCREMENT PRIMARY KEY,tblname TEXT(255));"
        Call RunSQL(SQL)
        SQL = "CREATE TABLE List_SG_Bushing_Powder (ID AUTOINCREMENT PRIMARY KEY,Manufacturer Text(255),sNAME Text(255),sCharge Text(255),sType Text(255),PowderName Text(255),sync_lastupdate);"
        Call RunSQL(SQL)
        SQL = "CREATE TABLE List_SG_Bushing_Shot (ID AUTOINCREMENT PRIMARY KEY,Manufacturer Text(255),sNAME Text(255),sCharge Text(255),sType Text(255),sync_lastupdate);"
        Call RunSQL(SQL)
        SQL = "CREATE TABLE DB_Version(ID AUTOINCREMENT PRIMARY KEY,dbver TEXT(255),dta DATETIME);"
        Call RunSQL(SQL)
        If Not ValueDoesExist("DB_Version", "dbver", DBVersion) Then
            Call SaveDatabaseVersion(DBVersion)
            Console.WriteLine(vbTab & "You are now updated to Database Version " & DBVersion)
        End If
        'Loaders_Log_Ammunition
        Call AddColumn("Vel", "Loaders_Log_Ammunition", "N/A", "Number")
        'End Updates
        Call UpdateLastUpdate(strUpdateName)
        Call AppliedUpdates(strUpdateName)
        Console.WriteLine("HotFix_" & strUpdateName & " was applied!")
        HotFixApplied = True
    End Sub
    Sub HotFix_3()
        Dim strUpdateName As String
        strUpdateName = "3"
        Call DebugLog("HotFix_" & strUpdateName, "Starting HotFix " & strUpdateName)
        'INSERT UPDATES HERE!!
        
        'End Updates
        Call UpdateLastUpdate(strUpdateName)
        Call AppliedUpdates(strUpdateName)
        Console.WriteLine("HotFix_" & strUpdateName & " was applied!")
        HotFixApplied = True
    End Sub
    Sub HotFix_4()
        Dim strUpdateName As String
        strUpdateName = "4"
        Call DebugLog("HotFix_" & strUpdateName, "Starting HotFix " & strUpdateName)
        'INSERT UPDATES HERE!!

        'End Updates
        Call UpdateLastUpdate(strUpdateName)
        Call AppliedUpdates(strUpdateName)
       Console.WriteLine("HotFix_" & strUpdateName & " was applied!")
        HotFixApplied = True
    End Sub
    Sub HotFix_5()
        Dim strUpdateName As String
        strUpdateName = "5"
        Call DebugLog("HotFix_" & strUpdateName, "Starting HotFix " & strUpdateName)
        'INSERT UPDATES HERE!!
        
        'End Updates
        Call UpdateLastUpdate(strUpdateName)
        Call AppliedUpdates(strUpdateName)
        Console.WriteLine("HotFix_" & strUpdateName & " was applied!")
        HotFixApplied = True
    End Sub
    Sub HotFix_6()
        Dim strUpdateName As String
        strUpdateName = "6"
        Call DebugLog("HotFix_" & strUpdateName, "Starting HotFix " & strUpdateName)
        'INSERT UPDATES HERE!!
        
        'End Updates
        Call UpdateLastUpdate(strUpdateName)
        Call AppliedUpdates(strUpdateName)
        Console.WriteLine("HotFix_" & strUpdateName & " was applied!")
        HotFixApplied = True
    End Sub
    Sub HotFix_7()
        Dim strUpdateName As String
        strUpdateName = "7"
        Call DebugLog("HotFix_" & strUpdateName, "Starting HotFix " & strUpdateName)
        'INSERT UPDATES HERE!!
        'End Updates
        Call UpdateLastUpdate(strUpdateName)
        Call AppliedUpdates(strUpdateName)
        Console.WriteLine("HotFix_" & strUpdateName & " was applied!")
        HotFixApplied = True
    End Sub
End Module
