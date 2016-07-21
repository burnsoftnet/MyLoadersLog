Imports BSMyLoadersLog.LoadersClass
Imports System.Data.Odbc
Imports System.IO
Imports System.Xml
Imports System.Data
Public Class frmImportConfiguration
    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.Filter = "XML File(*.xml)|*.xml"
        OpenFileDialog1.Title = "Import Config from XML"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim strFilePath As String = OpenFileDialog1.FileName
        Label2.Text = strFilePath
        If Len(strFilePath) > 0 Then btnImport.Enabled = True
    End Sub
    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Call ProcessXMLToDB(Label2.Text)
    End Sub
    Function GetXMLNode(ByVal instance As XmlNode) As String
        Dim sAns As String = ""
        On Error Resume Next
        sAns = instance.InnerText
        Return sAns
    End Function
    Sub ProcessXMLToDB(ByVal strPath As String)
        Dim ConfigName As String = ""
        Dim IsPersonal As Boolean = False
        Dim isShotgun As Boolean = False
        Dim iPersonal As Integer = 0
        Dim iShotgun As Integer = 0
        Dim Notes As String = ""
        Dim AmmoType As String = ""
        Dim Caliber As String = ""
        Dim CalID As Long = 0
        Dim GaugeName As String = ""
        Dim PrefShot_T As String = ""
        Dim PrefShot_D As Double = 0
        Dim ConfigID As Long = 0
        Dim BulletID As Long = 0
        Dim PrimerID As Long = 0
        Dim CaseID As Long = 0
        Dim WADID As Long = 0
        Dim AmmoTypeID As Long = 0
        Dim Refferance As String = ""
        Dim ObjGF As New GlobalFunctions
        Dim Obj As New BSDatabase
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 8
        Me.UseWaitCursor = True
        Dim I As Integer = 0
        lblProg.Text = "Getting Config Details"
        lblProg.Refresh()
        I += 1

        Call GetConfigDetails(strPath, "Details", ConfigName, IsPersonal, isShotgun, _
                Notes, AmmoType, Caliber, Refferance, PrefShot_T, PrefShot_D)
        If Not ObjGF.ObjectExistsinDB(ConfigName, "ConfigName", "Config_List_Name") Then
            If Not isShotgun Then
                ProgressBar1.Maximum = 8
                ProgressBar1.Value = I
                ProgressBar1.Refresh()
                lblProg.Text = "Getting Caliber ID"
                lblProg.Refresh()
                I += 1
                CalID = ObjGF.GetCaliberID(Caliber, True)
                ProgressBar1.Value = I
                ProgressBar1.Refresh()

                lblProg.Text = "Getting Bullet Information and ID"
                lblProg.Refresh()
                I += 1
                Call ProcessXMLToDB_Bullets(strPath, "List_Bullets", CalID, BulletID)
                ProgressBar1.Value = I
                ProgressBar1.Refresh()

                lblProg.Text = "Getting Case Information and ID"
                lblProg.Refresh()
                I += 1
                Call ProcessXMLToDB_Case(strPath, "List_Case", CalID, CaseID)
                ProgressBar1.Value = I
                ProgressBar1.Refresh()

                lblProg.Text = "Getting Primer Information and ID"
                lblProg.Refresh()
                I += 1
                Call ProcessXMLToDB_Primer(strPath, "General_Primer", PrimerID)
                If IsPersonal Then iPersonal = 1
                If isShotgun Then iShotgun = 1
                AmmoTypeID = ObjGF.GetID("Select * from General_Ammunition_Type where FType='" & AmmoType & "'")
                ProgressBar1.Value = I
                ProgressBar1.Refresh()

                lblProg.Text = "Inserting Config name"
                lblProg.Refresh()
                I += 1
                Dim SQL As String = "INSERT INTO Config_List_Name (ConfigName,ISPersonal,IsShotGun,Notes) VALUES('" & _
                                    ConfigName & "'," & iPersonal & "," & iShotgun & ",'" & Notes & "')"
                If Not ObjGF.ObjectExistsinDB(ConfigName, "ConfigName", "Config_list_Name") Then
                    Obj.ConnExec(SQL)
                End If
                ConfigID = ObjGF.GetID("Select * from Config_List_Name where Configname='" & ConfigName & "'")
                ProgressBar1.Value = I
                ProgressBar1.Refresh()
                lblProg.Text = "Inserting Configuration Data"
                lblProg.Refresh()
                I += 1
                SQL = "INSERT INTO Config_List_Data_NSG (CLNID,ATID,CALID,BID,PRID,CAID,Source) VALUES (" & _
                        ConfigID & "," & AmmoTypeID & "," & CalID & "," & BulletID & "," & _
                        PrimerID & "," & CalID & ",'" & Refferance & "')"
                Obj.ConnExec(SQL)
                ProgressBar1.Value = I
                ProgressBar1.Refresh()

                lblProg.Text = "Inserting Powder Data"
                lblProg.Refresh()
                I += 1
                Call ProcessXMLToDB_Powder(strPath, "General_Powder", ConfigID)
                ProgressBar1.Value = I
                ProgressBar1.Refresh()
                Me.UseWaitCursor = False
                MsgBox("Import of the " & ConfigName & " Configuration is complete!")
            Else
                ProgressBar1.Maximum = 9
                ProgressBar1.Value = I
                ProgressBar1.Refresh()
                lblProg.Text = "Getting Caliber ID"
                lblProg.Refresh()
                I += 1
                CalID = ObjGF.GetCaliberID(Caliber, True)
                ProgressBar1.Value = I
                ProgressBar1.Refresh()

                lblProg.Text = "Getting Shot/Slug Information and ID"
                lblProg.Refresh()
                I += 1
                Call ProcessXMLtoList_SG_ShotType_Details(strPath, "List_SG_ShotType_Details", CalID, BulletID)
                ProgressBar1.Value = I
                ProgressBar1.Refresh()

                lblProg.Text = "Getting Primer Information and ID"
                lblProg.Refresh()
                I += 1
                Call ProcessXMLToDB_Primer(strPath, "General_Primer", PrimerID)
                If IsPersonal Then iPersonal = 1
                If isShotgun Then iShotgun = 1
                'AmmoType is different from the Pistol Rifle Section, Ammotype ID might not apply.
                AmmoTypeID = ObjGF.GetID("Select * from General_Ammunition_Type where FType='" & AmmoType & "'")
                ProgressBar1.Value = I
                ProgressBar1.Refresh()

                lblProg.Text = "Getting Hull/Case Information and ID"
                lblProg.Refresh()
                I += 1
                Call ProcessXMLToDB_Hull(strPath, "List_SG_Case", CalID, CaseID, GaugeName)
                ProgressBar1.Value = I
                ProgressBar1.Refresh()

                lblProg.Text = "Getting WAD Information and ID"
                lblProg.Refresh()
                I += 1
                Call ProcessXMLToDB_WADS(strPath, "List_SG_WAD", GaugeName, WADID)
                ProgressBar1.Value = I
                ProgressBar1.Refresh()


                lblProg.Text = "Inserting Config name"
                lblProg.Refresh()
                I += 1
                Dim SQL As String = "INSERT INTO Config_List_Name (ConfigName,ISPersonal,IsShotGun,Notes) VALUES('" & _
                                    ConfigName & "'," & iPersonal & "," & iShotgun & ",'" & Notes & "')"
                If Not ObjGF.ObjectExistsinDB(ConfigName, "ConfigName", "Config_list_Name") Then
                    Obj.ConnExec(SQL)
                End If
                ConfigID = ObjGF.GetID("Select * from Config_List_Name where Configname='" & ConfigName & "'")
                ProgressBar1.Value = I
                ProgressBar1.Refresh()
                lblProg.Text = "Inserting Configuration Data"
                lblProg.Refresh()
                I += 1
                Dim GID As Long = ObjGF.GetGaugeID(GaugeName)
                Dim LTID As Long = ObjGF.GetAmmoTypeIDSG(AmmoType)
                SQL = "INSERT INTO Config_List_Data_SG (CLNID,ATID,CALID,PRID,CAID,WAD" & _
                    ",SCL,Source,GID,IsPersonal,LTID,SW,SW_t) VALUES(" & ConfigID & "," & CalID & "," & GID & "," & PrimerID & _
                    "," & CaseID & "," & WADID & "," & BulletID & ",'" & _
                    FluffContent(Refferance) & "'," & GID & "," & iPersonal & "," & LTID & _
                    "," & PrefShot_D & ",'" & PrefShot_T & "')"
                Obj.ConnExec(SQL)
                ProgressBar1.Value = I
                ProgressBar1.Refresh()

                lblProg.Text = "Inserting Powder Data"
                lblProg.Refresh()
                I += 1
                Call ProcessXMLToDB_PowderSG(strPath, "General_Powder", ConfigID)
                ProgressBar1.Value = I
                ProgressBar1.Refresh()
                Me.UseWaitCursor = False
                MsgBox("Import of the " & ConfigName & " Configuration is complete!")
            End If
            
            Call MDIParentMain.RefreshConfigData()
        Else
            Me.UseWaitCursor = False
            MsgBox(ConfigName & " already exists in Database!")
        End If
            Me.Close()
    End Sub
    Sub GetConfigDetails(ByVal strpath As String, ByVal strNodeName As String, ByRef ConfigName As String, _
                            ByRef IsPersonal As Boolean, ByRef IsShotGun As Boolean, ByRef Notes As String, _
                            ByRef AmmoType As String, ByRef Caliber As String, ByRef Refferance As String, _
                            Optional ByRef PrefShot_Text As String = "", Optional ByRef PrefShot_Doub As Double = 0)
        Dim doc As New XmlDocument
        Dim ObjGF As New GlobalFunctions
        Dim i As Integer = 0
        doc.Load(strpath)
        Dim elemlist As XmlNodeList = doc.GetElementsByTagName(strNodeName)
        For i = 0 To elemlist.Count - 1
            ConfigName = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("ConfigName")))
            IsPersonal = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("IsPersonal")))
            IsShotGun = CBool(ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("IsShotGun"))))
            Notes = FluffContent(ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("Notes"))))
            AmmoType = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("AmmoType")))
            Caliber = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("Caliber")))
            Refferance = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("Refferance")))
            If IsShotGun Then
                PrefShot_Text = ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("sw_t")))
                PrefShot_Doub = CDbl(ObjGF.FormatFromXML(GetXMLNode(elemlist(i).Item("sw"))))
            End If
        Next
        elemlist = Nothing
        doc = Nothing
    End Sub
    Sub ProcessXMLtoList_SG_ShotType_Details(ByVal strPath As String, ByVal strNodeName As String, ByVal CalID As Long, ByRef SDTID As Long)
        Dim doc As New XmlDocument
        Dim Obj As New BSDatabase
        Dim ObjGF As New GlobalFunctions
        Dim i As Integer = 0
        Dim Manu As String = ""
        Dim SQL As String = ""
        Dim Name As String = ""
        Dim Wei As String = ""
        Dim IsSlug As Integer = 0
        Dim Mat As String = ""
        Dim ShotNo As String = ""
        Dim CAL As String = ""
        doc.Load(strPath)
        Dim elemlist As XmlNodeList = doc.GetElementsByTagName(strNodeName)
        For i = 0 To elemlist.Count - 1
            Manu = GetXMLNode(elemlist(i).Item("Manufacturer"))
            Name = GetXMLNode(elemlist(i).Item("Name"))
            Wei = GetXMLNode(elemlist(i).Item("Weight"))
            IsSlug = GetXMLNode(elemlist(i).Item("IsSlug"))
            CAL = GetXMLNode(elemlist(i).Item("CAL"))
            If IsSlug = 0 Then
                Mat = GetXMLNode(elemlist(i).Item("Matt"))
                ShotNo = GetXMLNode(elemlist(i).Item("ShotNo"))
                SQL = "INSERT INTO List_SG_ShotType_Details(Manufacturer,NAME,IsSlug," & _
                        "mat,ShotNo,weight,CAL,Qty,EPPS,Price,ounces,grams) VALUE('" & _
                        Manu & "','" & Name & "'," & IsSlug & ",'" & Mat & _
                        "','" & ShotNo & "','" & Wei & "','" & CAL & _
                        "',0,0,0,0,0)"
            Else
                SQL = "INSERT INTO List_SG_ShotType_Details(Manufacturer,NAME,IsSlug," & _
                        "weight,CAL,Qty,EPPS,Price,ounces,grams) VALUE('" & _
                        Manu & "','" & Name & "'," & IsSlug & ",'" & Wei & _
                        "','" & CAL & "',0,0,0,0,0)"
            End If
            If Not ObjGF.ObjectExistsinDB(Manu, "Manufacturer", strNodeName) Or Not ObjGF.ObjectExistsinDB(Name, "Name", strNodeName) Then
                Obj.ConnExec(SQL)
            End If
            SDTID = ObjGF.GetID("Select * from " & strNodeName & " where Manufacturer='" & Manu & "' and Name='" & Name & "' and IsSlug=" & IsSlug)
        Next
        elemlist = Nothing
        doc = Nothing
    End Sub
    Sub ProcessXMLToDB_Bullets(ByVal strPath As String, ByVal strNodeName As String, ByVal CalID As Long, ByRef BulletID As Long)
        Dim doc As New XmlDocument
        Dim Obj As New BSDatabase
        Dim ObjGF As New GlobalFunctions
        Dim i As Integer = 0
        Dim Manu As String = ""
        Dim SQL As String = ""
        Dim Name As String = ""
        Dim Dia As String = ""
        Dim Wei As String = ""
        Dim SecDen As String = ""
        Dim PartNo As String = ""
        Dim BC As String = ""
        Dim BtName As String = ""
        Dim BTID As Long = 0
        doc.Load(strPath)
        Dim elemlist As XmlNodeList = doc.GetElementsByTagName(strNodeName)
        For i = 0 To elemlist.Count - 1
            Manu = GetXMLNode(elemlist(i).Item("Manufacturer"))
            Name = GetXMLNode(elemlist(i).Item("Name"))
            Dia = GetXMLNode(elemlist(i).Item("Diameter"))
            Wei = GetXMLNode(elemlist(i).Item("Weight"))
            SecDen = GetXMLNode(elemlist(i).Item("Sec_Den"))
            PartNo = FluffContent(GetXMLNode(elemlist(i).Item("Part_number")))
            BC = GetXMLNode(elemlist(i).Item("Ballistic_Coefficient"))
            BtName = GetXMLNode(elemlist(i).Item("Bullet_Type"))
            BTID = ObjGF.GetID("Select * from General_Ammunition_Type where FType='" & BtName & "'")
            SQL = "INSERT INTO " & strNodeName & " (Manufacturer,Name,Diameter,Weight,Sec_Den," & _
                    "Part_number,Ballistic_Coefficient,Bullet_Type,CID) VALUES('" & Manu & "','" & _
                    Name & "','" & Dia & "','" & Wei & "','" & SecDen & "','" & PartNo & "','" & _
                    BC & "'," & BTID & "," & CalID & ")"
            If Not ObjGF.ObjectExistsinDB(Manu, "Manufacturer", strNodeName) Or Not ObjGF.ObjectExistsinDB(Name, "Name", strNodeName) Then
                Obj.ConnExec(SQL)
            End If
            BulletID = ObjGF.GetID("Select * from " & strNodeName & " where Manufacturer='" & Manu & "' and Name='" & Name & "'")
        Next
        elemlist = Nothing
        doc = Nothing
    End Sub
    Sub ProcessXMLToDB_Case(ByVal strPath As String, ByVal strNodeName As String, ByVal CalID As Long, ByRef CaseID As Long)
        Dim doc As New XmlDocument
        Dim Obj As New BSDatabase
        Dim ObjGF As New GlobalFunctions
        Dim i As Integer = 0
        Dim Manu As String = ""
        Dim SQL As String = ""
        Dim Name As String = ""
        Dim ttl As String = ""
        Dim TimesUsed As String = ""
        Dim isNew As Long = 0
        doc.Load(strPath)
        Dim elemlist As XmlNodeList = doc.GetElementsByTagName(strNodeName)
        For i = 0 To elemlist.Count - 1
            Manu = GetXMLNode(elemlist(i).Item("Manufacturer"))
            Name = GetXMLNode(elemlist(i).Item("Name"))
            ttl = GetXMLNode(elemlist(i).Item("ttl"))
            TimesUsed = GetXMLNode(elemlist(i).Item("TimesUsed"))
            If CLng(TimesUsed) = 0 Then isNew = 1
            SQL = "INSERT INTO " & strNodeName & " (Manufacturer,Name,ttl,IsNew,TimesUsed,CID) VALUES('" & _
                    Manu & "','" & _
                    Name & "','" & ttl & "'," & isNew & "," & TimesUsed & "," & CalID & ")"
            If Not ObjGF.ObjectExistsinDB(Manu, "Manufacturer", strNodeName) Or Not ObjGF.ObjectExistsinDB(Name, "Name", strNodeName) Then
                Obj.ConnExec(SQL)
            End If
            CaseID = ObjGF.GetID("Select * from " & strNodeName & " where Manufacturer='" & Manu & "' and Name='" & Name & "'")
        Next
        elemlist = Nothing
        doc = Nothing
    End Sub
    Sub ProcessXMLToDB_Hull(ByVal strPath As String, ByVal strNodeName As String, ByVal CalID As Long, ByRef CaseID As Long, ByRef GaugeName As String)
        Dim doc As New XmlDocument
        Dim Obj As New BSDatabase
        Dim ObjGF As New GlobalFunctions
        Dim i As Integer = 0
        Dim Manu As String = ""
        Dim SQL As String = ""
        Dim Name As String = ""
        Dim Length As String = ""
        Dim TimesUsed As String = ""
        Dim Gauge As String = ""
        Dim DRAM As String = ""
        Dim GID As Long = 0
        Dim isNew As Long = 0
        doc.Load(strPath)
        Dim elemlist As XmlNodeList = doc.GetElementsByTagName(strNodeName)
        For i = 0 To elemlist.Count - 1
            Manu = GetXMLNode(elemlist(i).Item("Manufacturer"))
            Name = GetXMLNode(elemlist(i).Item("Name"))
            Length = GetXMLNode(elemlist(i).Item("Length"))
            Gauge = GetXMLNode(elemlist(i).Item("Gauge"))
            DRAM = GetXMLNode(elemlist(i).Item("DRAM"))
            GaugeName = Gauge
            GID = ObjGF.GetGaugeID(Gauge)
            SQL = "INSERT INTO " & strNodeName & " (Manufacturer,Name,Gauge,GID,Length,Qty,Price,epps,DRAM) VALUES('" & _
                    Manu & "','" & Name & "','" & Gauge & "'," & GID & ",'" & Length & "',0,0,0,'" & DRAM & "')"
            If Not ObjGF.ObjectExistsinDB(Manu, "Manufacturer", strNodeName) Or Not ObjGF.ObjectExistsinDB(Name, "Name", strNodeName) Then
                Obj.ConnExec(SQL)
            End If
            CaseID = ObjGF.GetID("Select * from " & strNodeName & " where Manufacturer='" & Manu & "' and Name='" & Name & "' and Length='" & Length & "' and GID=" & GID)
        Next
        elemlist = Nothing
        doc = Nothing
    End Sub
    Sub ProcessXMLToDB_WADS(ByVal strPath As String, ByVal strNodeName As String, ByVal Gauge As String, ByRef WADID As Long)
        Dim doc As New XmlDocument
        Dim Obj As New BSDatabase
        Dim ObjGF As New GlobalFunctions
        Dim i As Integer = 0
        Dim Manu As String = ""
        Dim SQL As String = ""
        Dim Name As String = ""
        Dim load_t As String = ""
        Dim GID As Long = ObjGF.GetGaugeID(Gauge)
        Dim load_d As Double = 0
        doc.Load(strPath)
        Dim elemlist As XmlNodeList = doc.GetElementsByTagName(strNodeName)
        For i = 0 To elemlist.Count - 1
            Manu = GetXMLNode(elemlist(i).Item("Manufacturer"))
            Name = GetXMLNode(elemlist(i).Item("Name"))
            load_t = GetXMLNode(elemlist(i).Item("load_t"))
            load_d = GetXMLNode(elemlist(i).Item("load_d"))
            GID = ObjGF.GetGaugeID(Gauge)
            SQL = "INSERT INTO " & strNodeName & " (Manufacturer,WAD,Gauge,GID,load_t,load_d,Qty,Price,eppw) VALUES('" & _
                    Manu & "','" & Name & "','" & Gauge & "'," & GID & ",'" & load_t & "'," & load_d & ",0,0,0)"
            If Not ObjGF.ObjectExistsinDB(Manu, "Manufacturer", strNodeName) Or Not ObjGF.ObjectExistsinDB(Name, "WAD", strNodeName) Then
                Obj.ConnExec(SQL)
            End If
            WADID = ObjGF.GetID("Select * from " & strNodeName & " where Manufacturer='" & Manu & "' and WAD='" & Name & "' and GID=" & GID)
        Next
        elemlist = Nothing
        doc = Nothing
    End Sub
    Sub ProcessXMLToDB_Primer(ByVal strPath As String, ByVal strNodeName As String, ByRef PrimerID As Long)
        Dim doc As New XmlDocument
        Dim Obj As New BSDatabase
        Dim ObjGF As New GlobalFunctions
        Dim i As Integer = 0
        Dim Manu As String = ""
        Dim SQL As String = ""
        Dim Name As String = ""
        Dim Primer_Type As String = ""
        Dim Primer_TypeID As Long = 0
        doc.Load(strPath)
        Dim elemlist As XmlNodeList = doc.GetElementsByTagName(strNodeName)
        For i = 0 To elemlist.Count - 1
            Manu = GetXMLNode(elemlist(i).Item("Manufacturer"))
            Name = GetXMLNode(elemlist(i).Item("Name"))
            Primer_Type = GetXMLNode(elemlist(i).Item("Primer_Type"))
            Primer_TypeID = ObjGF.GetID("SELECT * from General_Primer_Type where Name='" & Primer_Type & "'")
            SQL = "INSERT INTO " & strNodeName & " (Manufacturer,Name,Primer_Type) VALUES('" & _
                    Manu & "','" & Name & "'," & Primer_TypeID & ")"
            If Not ObjGF.ObjectExistsinDB(Manu, "Manufacturer", strNodeName) Or Not ObjGF.ObjectExistsinDB(Name, "Name", strNodeName) Then
                Obj.ConnExec(SQL)
            End If
            PrimerID = ObjGF.GetID("Select * from " & strNodeName & " where Manufacturer='" & Manu & "' and Name='" & Name & "'")
        Next
        elemlist = Nothing
        doc = Nothing
    End Sub
    Sub ProcessXMLToDB_Powder(ByVal strPath As String, ByVal strNodeName As String, ByVal ConfigID As Long)
        Dim doc As New XmlDocument
        Dim Obj As New BSDatabase
        Dim ObjGF As New GlobalFunctions
        Dim i As Integer = 0
        Dim Manu As String = ""
        Dim SQL As String = ""
        Dim Name As String = ""
        Dim PowderID As Long = 0
        Dim Load_Min As Double = 0
        Dim Load_Mid As Double = 0
        Dim Load_Max As Double = 0
        Dim FPS_Min As Double = 0
        Dim FPS_Mid As Double = 0
        Dim FPS_Max As Double = 0
        Dim CUPS_Min As Double = 0
        Dim CUPS_Mid As Double = 0
        Dim CUPS_Max As Double = 0
        Dim IsPref As Long = 0
        doc.Load(strPath)
        Dim elemlist As XmlNodeList = doc.GetElementsByTagName(strNodeName)
        For i = 0 To elemlist.Count - 1
            Manu = GetXMLNode(elemlist(i).Item("Manufacturer"))
            Name = GetXMLNode(elemlist(i).Item("Name"))
            Load_Min = GetXMLNode(elemlist(i).Item("Load_Min"))
            Load_Mid = GetXMLNode(elemlist(i).Item("Load_Mid"))
            Load_Max = GetXMLNode(elemlist(i).Item("Load_Max"))
            FPS_Min = GetXMLNode(elemlist(i).Item("FPS_Min"))
            FPS_Mid = GetXMLNode(elemlist(i).Item("FPS_MID"))
            FPS_Max = GetXMLNode(elemlist(i).Item("FPS_Max"))
            CUPS_Min = GetXMLNode(elemlist(i).Item("CUPS_Min"))
            CUPS_Mid = GetXMLNode(elemlist(i).Item("CUPS_Mid"))
            CUPS_Max = GetXMLNode(elemlist(i).Item("CUPS_Max"))
            IsPref = GetXMLNode(elemlist(i).Item("IsPref"))
            SQL = "INSERT INTO " & strNodeName & " (Manufacturer,Name,weightlbs,weightgn,Notes) VALUES('" & _
                                Manu & "','" & Name & "',0,0,'')"
            If Not ObjGF.ObjectExistsinDB(Manu, "Manufacturer", strNodeName) Or Not ObjGF.ObjectExistsinDB(Name, "Name", strNodeName) Then
                Obj.ConnExec(SQL)
            End If
            PowderID = ObjGF.GetID("SELECT * from " & strNodeName & " where Name='" & Name & "' and Manufacturer='" & Manu & "'")
            SQL = "INSERT INTO Config_List_Powder_Data_NSG (CLNID,PID,Load_Min,Load_Mid,Load_Max,FPS_Min," & _
                    "FPS_MID,FPS_Max,CUPS_Min,CUPS_Mid,CUPS_Max,IsPref) VALUES(" & ConfigID & "," & PowderID & _
                    "," & Load_Min & "," & Load_Mid & "," & Load_Max & "," & FPS_Min & "," & _
                    FPS_Mid & "," & FPS_Max & "," & CUPS_Min & "," & CUPS_Mid & "," & _
                    CUPS_Max & "," & IsPref & ")"
            Obj.ConnExec(SQL)
        Next
        elemlist = Nothing
        doc = Nothing
    End Sub
    Sub ProcessXMLToDB_PowderSG(ByVal strPath As String, ByVal strNodeName As String, ByVal ConfigID As Long)
        Dim doc As New XmlDocument
        Dim Obj As New BSDatabase
        Dim ObjGF As New GlobalFunctions
        Dim i As Integer = 0
        Dim Manu As String = ""
        Dim SQL As String = ""
        Dim Name As String = ""
        Dim PowderID As Long = 0
        Dim Load_Mid As Double = 0
        Dim FPS_Mid As Double = 0
        Dim PSI_Mid As Double = 0
        Dim IsPref As Long = 0
        doc.Load(strPath)
        Dim elemlist As XmlNodeList = doc.GetElementsByTagName(strNodeName)
        For i = 0 To elemlist.Count - 1
            Manu = GetXMLNode(elemlist(i).Item("Manufacturer"))
            Name = GetXMLNode(elemlist(i).Item("Name"))
            Load_Mid = GetXMLNode(elemlist(i).Item("Load_Mid"))
            FPS_Mid = GetXMLNode(elemlist(i).Item("FPS_MID"))
            PSI_Mid = GetXMLNode(elemlist(i).Item("PSI_Mid"))
            IsPref = GetXMLNode(elemlist(i).Item("IsPref"))
            SQL = "INSERT INTO " & strNodeName & " (Manufacturer,Name,weightlbs,weightgn,Notes) VALUES('" & _
                                Manu & "','" & Name & "',0,0,'')"
            If Not ObjGF.ObjectExistsinDB(Manu, "Manufacturer", strNodeName) Or Not ObjGF.ObjectExistsinDB(Name, "Name", strNodeName) Then
                Obj.ConnExec(SQL)
            End If
            PowderID = ObjGF.GetID("SELECT * from " & strNodeName & " where Name='" & Name & "' and Manufacturer='" & Manu & "'")
            SQL = "INSERT INTO Config_List_Powder_Data_SG (CLNID,PID,Load_Mid," & _
                    "FPS_MID,PSI_Mid,IsPref) VALUES(" & ConfigID & "," & PowderID & _
                    "," & Load_Mid & "," & FPS_Mid & "," & PSI_Mid & "," & IsPref & ")"
            Obj.ConnExec(SQL)
        Next
        elemlist = Nothing
        doc = Nothing
    End Sub
    Private Sub frmImportConfiguration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblProg.Text = ""
    End Sub
End Class