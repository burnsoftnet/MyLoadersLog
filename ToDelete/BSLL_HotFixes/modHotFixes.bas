Attribute VB_Name = "modHotFixes"
Public HotFixApplied As Boolean
Public Sub DoUpdates()
    If Not HotFixExists("1") Then Call HotFix_1
    'If Not HotFixExists("2") Then Call HotFix_2
    'If Not HotFixExists("3") Then Call HotFix_3
    'If Not HotFixExists("4") Then Call HotFix_4
    'If Not HotFixExists("5") Then Call HotFix_5
    'If Not HotFixExists("6") Then Call HotFix_6
End Sub
Sub RedoAll()
    Call DelRegValue("HotFix", "1", "")
    'Call DelRegValue("HotFix", "2", "")
    'Call DelRegValue("HotFix", "3", "")
    'Call DelRegValue("HotFix", "4", "")
    'Call DelRegValue("HotFix", "5", "")
    'Call DelRegValue("HotFix", "6", "")
    Call DelRegValue("HotFix", "LastUpdate", "")
End Sub
Sub HotFix_1()
    Dim strUpdateName As String
    strUpdateName = "1"
    Dim SQL As String
'    Dim eMsg As String
'    eMsg = ""
    'INSERT UPDATES HERE!!
    Call DropTable("List_SG_ShotType_Details_Slugs")
    Call DropTable("List_SG_ShotType_Details_Shot")
    Call DropTable("List_SG_ShotType")
    Err.Clear
    SQL = "CREATE TABLE List_SG_ShotType_Details (ID AUTOINCREMENT PRIMARY KEY,Manufacturer Text(255),NAME Text(255),IsSlug INTEGER,mat Text(255),ShotNo Text(255),weight Text(255),CAL Text(255),Qty INTEGER,EPPS DOUBLE,Price DOUBLE,ounces DOUBLE, grams DOUBLE);"
    Call RunSQL(SQL)
'    If Len(eMsg) > 0 Then Call RunSQL("ALTER TABLE List_SG_ShotType_Details (ID AUTOINCREMENT PRIMARY KEY,Manufacturer Text(255),NAME Text(255),IsSlug INTEGER,mat Text(255),ShotNo Text(255),weight Text(255),CAL Text(255),Qty INTEGER,EPPS DOUBLE,Price DOUBLE,ounces DOUBLE, grams DOUBLE);")
'    Err.Clear
    Call AddColumn("load_t", "List_SG_WAD", "N/A", "Text(255)")
    Call AddColumn("load_d", "List_SG_WAD", "0", "Double")
    Call AddColumn("gauge", "List_SG_WAD", "N/A", "Text(255)")
    Call AddColumn("GID", "List_SG_WAD", "0", "Number")
    Call AddColumn("SW_t", "Config_List_Data_SG", "N/A", "Text(255)")
    Call AlterColumn("SW", "Config_List_Data_SG", "N/A", "Double")
    Call AddColumn("GID", "Config_List_Data_SG", "N/A", "Number")
    Call AddColumn("IsPersonal", "Config_List_Data_SG", "N/A", "Number")
    Call AddColumn("IsPref", "Config_List_Powder_Data_SG", "N/A", "Number")
    Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,mat,ShotNo,weight,price,ounces) VALUES('Lawrence','Magnum Lead Shot',0,'Lead','9','25',29.99,400)")
    Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,mat,ShotNo,weight,price,ounces) VALUES('Lawrence','Magnum Lead Shot',0,'Lead','4','25',39.99,400)")
    Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,mat,ShotNo,weight,price,ounces) VALUES('Winchester','Steel Shot',0,'Steel','4','0',0,0)")
    Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,mat,ShotNo,weight,price,ounces) VALUES('Winchester','Lead Shot',0,'Lead','7','0',25,0)")
    Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,weight,cal,qty,epps,price) VALUES('Gualandi','Hunting Shotshell Slug',1,'1-1/8 oz','12 Gauge',25,0.6916,17.29)")
    Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,weight,cal,qty,epps,price) VALUES('Gualandi','Hunting Shotshell Slug',1,'7/8 oz','20 Gauge',25,0.5596,13.99)")
    Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,weight,cal,qty,epps,price) VALUES('BPI-Hornady','Ballistic Sabot XTP Jacketed Flat Nose Magnum',1,'500 Grain','12 Gauge',20,1.0445,20.89)")
    Call RunSQL("INSERT INTO List_SG_ShotType_Details (Manufacturer,Name,IsSlug,weight,cal,qty,epps,price) VALUES('BPI AQ','Shotshell Slug',1,'1oz','12 Gauge',25,0.5996,14.99)")
    Call RunSQL("UPDATE List_SG_Gauge set ga='20 Gauge' where ID=1")
    Call RunSQL("UPDATE List_SG_Gauge set ga='12 Gauge' where ID=2")
    Call RunSQL("UPDATE List_SG_Gauge set ga='28 Gauge' where ID=3")
    Call RunSQL("UPDATE List_SG_Gauge set ga='10 Gauge' where ID=4")
    Call RunSQL("UPDATE List_SG_Gauge set ga='18 Gauge' where ID=5")
    Call RunSQL("UPDATE List_SG_Gauge set ga='410 Gauge' where ID=6")
    If Not ValueDoesExist("List_SG_Gauge", "ga", "16 Gauge") Then Call RunSQL("INSERT INTO List_SG_Gauge(ga) Values('16 Gauge')")
    'End Updates
    Call UpdateLastUpdate(strUpdateName)
    Call AppliedUpdates(strUpdateName)
    frmMain.List1.AddItem ("HotFix_" & strUpdateName & " was applied!")
    frmMain.List1.Refresh
    HotFixApplied = True
End Sub

Sub HotFix_2()
    Dim strUpdateName As String
    strUpdateName = "2"
    'INSERT UPDATES HERE!!
    'If Not ValueDoesExist("Gun_Cal", "Cal", "10 Gauge") Then RunSQL ("INSERT INTO Gun_Cal (Cal) VALUES('10 Gauge')")
    'If Not ValueDoesExist("Gun_Cal", "Cal", "12 Gauge") Then RunSQL ("INSERT INTO Gun_Cal (Cal) VALUES('12 Gauge')")
    'If Not ValueDoesExist("Gun_Cal", "Cal", "16 Gauge") Then RunSQL ("INSERT INTO Gun_Cal (Cal) VALUES('16 Gauge')")
    'If Not ValueDoesExist("Gun_Cal", "Cal", "20 Gauge") Then RunSQL ("INSERT INTO Gun_Cal (Cal) VALUES('20 Gauge')")
    'If Not ValueDoesExist("Gun_Cal", "Cal", "28 Gauge") Then RunSQL ("INSERT INTO Gun_Cal (Cal) VALUES('28 Gauge')")
    'If Not ValueDoesExist("Gun_Cal", "Cal", ".410 Gauge") Then RunSQL ("INSERT INTO Gun_Cal (Cal) VALUES('.410 Gauge')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Black Powder") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Black Powder')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Semi-Auto Pistol") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Semi-Auto Pistol')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Semi-Auto") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Semi-Auto')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Single Action") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Single Action')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Slide Action") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Slide Action')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Commemorative") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Commemorative')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Fixed Barrel") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Fixed Barrel')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Flintlock") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Flintlock')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Derringer") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Derringer')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Pistol: Bolt Action") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Pistol: Bolt Action')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Pistol: Semi-Auto - SA/DA") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Pistol: Semi-Auto - SA/DA')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Pistol: Semi-Auto - SA Only") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Pistol: Semi-Auto - SA Only')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Pistol: Semi-Auto - DAO") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Pistol: Semi-Auto - DAO')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Pistol: Single Shot") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Pistol: Single Shot')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Pistol: Misc") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Pistol: Misc')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Revolver: SA/DA") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Revolver: SA/DA')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Revolver: SA only") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Revolver: SA only')")
    'If Not ValueDoesExist("Gun_Type", "Type", "Revolver: Misc") Then RunSQL ("INSERT INTO Gun_Type (Type) VALUES('Revolver: Misc')")
    'Call SwapGunColPurchaseValues("Gun_Collection", "dt", "dtp")
    'End Updates
    Call UpdateLastUpdate(strUpdateName)
    Call AppliedUpdates(strUpdateName)
    frmMain.List1.AddItem ("HotFix_" & strUpdateName & " was applied!")
    frmMain.List1.Refresh
    HotFixApplied = True
End Sub
Sub HotFix_3()
    Dim strUpdateName As String
    strUpdateName = "3"
    'INSERT UPDATES HERE!!
    'Call AddColumn("ISMAIN", "Gun_Collection_Pictures", "0", "Number")
    'RunSQL ("UPDATE Gun_Collection_Pictures set ISMAIN=0 where ISMAIN <> 1")
    'Call SetMainPictures
    'End Updates
    Call UpdateLastUpdate(strUpdateName)
    Call AppliedUpdates(strUpdateName)
    frmMain.List1.AddItem ("HotFix_" & strUpdateName & " was applied!")
    frmMain.List1.Refresh
    HotFixApplied = True
End Sub
Sub HotFix_4()
    Dim strUpdateName As String
    strUpdateName = "4"
    'INSERT UPDATES HERE!!
    'Call SaveRegValue("Settings", "BackupOnExit", "False")
    'Call AddColumn("Importer", "Gun_Collection", "N/A", "Text(255)")
    'Call DropTable("Gun_Collection_Ammo_Details")
    'Call DropTable("Gun_Collection_Ammo_Details_Pictures")
    'Call DropTable("Gun_Shop_SalesEmp")
    'If Not ValueDoesExist("Gun_Cal", "Cal", ".223 Remington") Then RunSQL ("INSERT INTO Gun_Cal (Cal) VALUES('.223 Remington')")
    'If Not ValueDoesExist("Gun_Cal", "Cal", "5.56 x 45mm") Then RunSQL ("INSERT INTO Gun_Cal (Cal) VALUES('5.56 x 45mm')")
    'If Not ValueDoesExist("Gun_Cal", "Cal", ".30-06") Then RunSQL ("INSERT INTO Gun_Cal (Cal) VALUES('.30-06')")
    'Call SaveRegValue("Settings", "UseOrgImage", "False")
    'Call SaveRegValue("Settings", "ViewPetLoads", "True")
    'Call SaveRegValue("Settings", "IndvReports", "True")
    'Call AddColumn("UID", "Owner_Info", "N/A", "Memo")
    'Call AddColumn("forgot_word", "Owner_Info", "N/A", "Memo")
    'Call AddColumn("forgot_phrase", "Owner_Info", "N/A", "Memo")
    'Call AddColumn("dcal", "Gun_Collection_Ammo", "N/A", "number")
    'Call ConvertAmmoGrainsToNum
    'End Updates
    Call UpdateLastUpdate(strUpdateName)
    Call AppliedUpdates(strUpdateName)
    frmMain.List1.AddItem ("HotFix_" & strUpdateName & " was applied!")
    frmMain.List1.Refresh
    HotFixApplied = True
End Sub
Sub HotFix_5()
    Dim strUpdateName As String
    strUpdateName = "5"
    'INSERT UPDATES HERE!!
    'End Updates
    Call UpdateLastUpdate(strUpdateName)
    Call AppliedUpdates(strUpdateName)
    frmMain.List1.AddItem ("HotFix_" & strUpdateName & " was applied!")
    frmMain.List1.Refresh
    HotFixApplied = True
End Sub
Sub HotFix_6()
    Dim strUpdateName As String
    strUpdateName = "6"
    'INSERT UPDATES HERE!!
    'End Updates
    Call UpdateLastUpdate(strUpdateName)
    Call AppliedUpdates(strUpdateName)
    frmMain.List1.AddItem ("HotFix_" & strUpdateName & " was applied!")
    frmMain.List1.Refresh
    HotFixApplied = True
End Sub
