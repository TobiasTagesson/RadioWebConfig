﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RadioWebConfig
{
    public partial class _Default : Page
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        //public static string path { get; } = @"C:\Users\daniel.rydqvist\Desktop\ConfigMapp\";
        // public static string path { get; } = @"C:\Users\Marcus.Lundgren\Desktop\ConfigCreator";
        public static string path { get; } = @"C:\Users\tobia\Desktop\ConfigCreator";

        public static string fileName = "Config.txt";
        public static string fullPath = "";
        public static string fullPathWithFileName = "";
        public static string stationCode { get; set; } = "";

        public static List<ConfigValues> lists = new List<ConfigValues>();
        public static ConfigValues listObject = new ConfigValues();

        public static List<string> filteredLinesInDoc = new List<string>();
        public static List<string> unFilteredLinesInDoc = new List<string>();
        public static List<string> linesInDoc = new List<string>();

        public static int listDivider;
     
        
        protected string GetStationCode()
        {
            try
            {
                HttpCookie myCookie = Request.Cookies["myCookie"];
                if (myCookie == null)
                {
                    
                }

                //ok - cookie is found.
                //Gracefully check if the cookie has the key-value as expected.
                if (!string.IsNullOrEmpty(myCookie.Values["userid"]))
                {
                    stationCode = myCookie.Values["userid"].ToString();
                    //Yes userId is found. Mission accomplished.
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.ToString());
                return stationCode;
                
            }

            return stationCode;
        }


        protected void Page_Load(object sender, EventArgs e)
        {  
            if(Session["myUser"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (Request.Cookies["myCookie"] == null || Session["myUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            var user = (LoggedInUser)Session["myUser"];

            // -- Skapa en meny som bara syns för admin -- //

            //if (user.Role != 1) 
            //{
            //    MenuItemCollection menuItems = mTopMenu.Items;
            //    MenuItem adminItem = new MenuItem();

            //    foreach (MenuItem menuItem in menuItems)
            //    {
            //        if (menuItem.Text == "AdminMenu")
            //            adminItem = menuItem;
            //    }
            //    menuItems.Remove(adminItem);
            //}
            
            AdminHidden.Value = user.Role.ToString();

            if (user.Role == 1)
            {
                RenderAdminTextBoxes(1);
            }

            RenderSomeTextBoxes(30);
            RenderTgTextBoxes(60);
            RenderRestOfTextBoxes(10);
            GetStationCode();
            GetTruckList();
            
        }

        [WebMethod]
        public static void ClearSession()
        {
            System.Web.HttpContext.Current.Session.Remove("myUser");
            return;
        }


        protected void RenderSomeTextBoxes(int count)
        {

            int[] c = new int[count];

            statusDataList.DataSource = c;
            statusDataList.DataBind();

            portDataList.DataSource = c;
            portDataList.DataBind();

            shortNrDataList.DataSource = c;
            shortNrDataList.DataBind();
        }
        protected void RenderTgTextBoxes(int count)
        {
            int[] c = new int[count];

            tgDataList.DataSource = c;
            tgDataList.DataBind();
        }
        protected void RenderRestOfTextBoxes(int count)
        {
            int[] c = new int[count];

            urlDataList.DataSource = c;
            urlDataList.DataBind();

            quickButtonDataList.DataSource = c;
            quickButtonDataList.DataBind();
            
        }

        // Skapa textboxar för admin
        protected void RenderAdminTextBoxes(int count)
        {
            int[] c = new int[count];
            adminDataList.DataSource = c;
            adminDataList.DataBind();
           // addCustomerDataList.DataSource = c;
           // addCustomerDataList.DataBind();

        }


        [WebMethod]
        [ScriptMethod]
        public static List<string> GetTruckList()
        {
            try
            {

                fullPath = path + "\\" + stationCode;

                List<string> truckList = new List<string>();

                foreach (var file in
                Directory.EnumerateFiles(fullPath, "*.txt"))
                {
                    truckList.Add(Path.GetFileName(file));
                }

                return truckList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return null;

            }

        }

        public static int CheckQuickButtonIndex(int index, string[] lines)
        {
            try
            {
                string ld = lines[index].Substring(6, 2);
                string ld2 = lines[index].Substring(6, 1);

                bool successfullyParsed = int.TryParse(ld, out listDivider);

                if (!successfullyParsed)
                {
                    int.TryParse(ld2, out listDivider);
                }
                return listDivider;
            }
            catch (ArgumentOutOfRangeException aex)
            {
                _logger.Error(aex.ToString());
                return listDivider;
            }
            catch (IndexOutOfRangeException iex)
            {
                _logger.Error(iex.ToString());
                return listDivider;
            }
        }
       
        public static int CheckButtonIndex(int index, string[] lines)
        {
            try
            {
                string ld = lines[index].Substring(3, 2);
                string ld2 = lines[index].Substring(3, 1);

                bool successfullyParsed = int.TryParse(ld, out listDivider);

                if (!successfullyParsed)
                {
                    int.TryParse(ld2, out listDivider);
                }
                return listDivider;
            }
            catch (ArgumentOutOfRangeException aex)
            {
                _logger.Error(aex.ToString());
                return listDivider;
            }
            catch (IndexOutOfRangeException iex)
            {
                _logger.Error(iex.ToString());
                return listDivider;
            }
        }

        public static void ExtractQuickButtonInfo()
        {
            try
            {
                int j = 1;



                var lines = linesInDoc.Where(x => x.StartsWith("Status")).ToArray();

                QuickButtonInfo qi = new QuickButtonInfo();

                for (int x = 0; x < lines.Count(); x++)
                {

                    CheckQuickButtonIndex(x, lines);

                    if (listDivider == j)
                    {

                        if (lines[x].Contains("Text,"))
                        {
                            qi = new QuickButtonInfo();

                            qi.qbName = ReadValue(lines[x]);
                        }
                        else if (lines[x].Contains(string.Format("Status" + listDivider + ",")))
                        {
                            qi.qbStatus = ReadValue(lines[x]);

                        }
                        else if (lines[x].Contains("Dest1,"))
                        {
                            qi.qbDest1 = ReadValue(lines[x]);

                        }
                        else if (lines[x].Contains("Dest2,"))
                        {
                            qi.qbDest2 = ReadValue(lines[x]);

                            listObject.quickList.Add(qi);
                            
                            j++;
                        }
                    }
                }
                lists.Add(listObject);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            
        }

        public static void ExtractPortInfo()
        {
            try
            {
                int j = 1;

                var lines = linesInDoc.Where(x => x.Contains("Port") && x.StartsWith("Btn")).ToArray();

                PortInfo pi = new PortInfo();

                for (int x = 0; x < lines.Count(); x++)
                {
                    CheckButtonIndex(x, lines);

                    if (listDivider == j)
                    {

                        if (lines[x].Contains("PortNamn,"))
                        {
                            pi = new PortInfo();

                            pi.portName = ReadValue(lines[x]);
                        }
                        else if (lines[x].Contains("PortStatus,"))
                        {
                            pi.portStatus = ReadValue(lines[x]);
                        }
                        else if (lines[x].Contains("PortDest,"))
                        {
                            pi.portDest = ReadValue(lines[x]);

                            listObject.portList.Add(pi);
                            j++;
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException aex)
            {
                _logger.Error(aex.ToString());
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.Error(ex.ToString());
            }
            
        }

        public static void ExtractStatusInfo()
        {
            try
            {
                int j = 1;

                var lines = linesInDoc.Where(x => x.Contains("Status") && x.StartsWith("Btn")).ToArray();

                StatusInfo si = new StatusInfo();
                for (int x = 0; x < lines.Count(); x++)
                {

                    CheckButtonIndex(x, lines);

                    if (listDivider == j)
                    {

                        if (lines[x].Contains("StatusText,"))
                        {
                            si = new StatusInfo();

                            si.stName = ReadValue(lines[x]);
                        }
                        else if (lines[x].Contains("Status,"))
                        {
                            si.stStatus = ReadValue(lines[x]);
                        }
                        else if (lines[x].Contains("StatusDest1,"))
                        {
                            si.stDest1 = ReadValue(lines[x]);
                        }
                        else if (lines[x].Contains("StatusDest2,"))
                        {
                            si.stDest2 = ReadValue(lines[x]);

                            listObject.statusList.Add(si);

                            j++;
                        }
                    }
                    
                }
            }
            catch (ArgumentOutOfRangeException aex)
            {
                _logger.Error(aex.ToString());
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.Error(ex.ToString());
            }
            
        }

    
        public static void ExtractKortNRInfo()
        {
            try
            {
                int j = 1;

                var lines = linesInDoc.Where(x => x.Contains("Kort") && x.StartsWith("Btn")).ToArray();

                ShortNrInfo sni = new ShortNrInfo();

                for (int x = 0; x < lines.Count(); x++)
                {
                    CheckButtonIndex(x, lines);

                    if (listDivider == j)
                    {

                        if (lines[x].Contains("KortNamn,"))
                        {
                            sni = new ShortNrInfo();

                            sni.shortName = ReadValue(lines[x]);
                        }
                        else if (lines[x].Contains("KortNr,"))
                        {
                            sni.shortNr = ReadValue(lines[x]);

                            listObject.shortList.Add(sni);
                            j++;

                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException aex)
            {
                _logger.Error(aex.ToString());
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        public static void ExtractUrlInfo()
        {
            try
            {
                int j = 1;

               

                var lines = linesInDoc.Where(x => x.Contains("Url") && x.StartsWith("Btn")).ToArray();

                LinkInfo li = new LinkInfo();

                for (int x = 0; x < lines.Count(); x++)
                {
                    CheckButtonIndex(x, lines);

                    if (listDivider == j)
                    {

                        if (lines[x].Contains("UrlNamn,"))
                        {
                            li = new LinkInfo();

                            li.linkName = ReadValue(lines[x]);
                        }
                        else if (lines[x].Contains("UrlData,"))
                        {
                            li.linkUrl = ReadValue(lines[x]);

                            listObject.linkList.Add(li);
                            j++;

                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException aex)
            {
                _logger.Error(aex.ToString());
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.Error(ex.ToString());
            }
            
        }

        public static void ExtractTgInfo()
        {
            try
            {
                int j = 1;

                var lines = linesInDoc.Where(x => x.Contains("Tg") && x.StartsWith("Btn")).ToArray();

                TgInfo ti = new TgInfo();

                for (int x = 0; x < lines.Count(); x++)
                {
                    CheckButtonIndex(x, lines);

                    if (listDivider == j)
                    {

                        if (lines[x].Contains("TgNamn,"))
                        {
                            ti = new TgInfo();

                            ti.tgName = ReadValue(lines[x]);
                            
                        }
                        else if (lines[x].Contains("TgGissi,"))
                        {
                            ti.tgGissi = ReadValue(lines[x]);

                            listObject.tgList.Add(ti);
                            j++;

                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException aex)
            {
                _logger.Error(aex.ToString());
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.Error(ex.ToString());
            }
            
        }

        public static void ExtractName()
        {
            try
            {
                var name = unFilteredLinesInDoc.FirstOrDefault(x => x.StartsWith("Namn"));
                var licenseNr = unFilteredLinesInDoc.FirstOrDefault(x => x.StartsWith("LicensNumber"));
                var orgNr = unFilteredLinesInDoc.FirstOrDefault(x => x.StartsWith("ORGNR"));
                var issi = unFilteredLinesInDoc.FirstOrDefault(x => x.StartsWith("ISSI"));

                AdminInfo ai = new AdminInfo();

                if (name != null)
                {
                    //listObject.Namn = ReadValue(name);
                    ai.adNamn = ReadValue(name);
                }
                if(licenseNr != null)
                {
                    //listObject.LicenseNumber = ReadValue(licenseNr);
                    ai.adLicenseNumber = ReadValue(licenseNr);
                }
                if(orgNr != null)
                {
                    //listObject.OrgNr = ReadValue(orgNr);
                    ai.adOrgNr = ReadValue(orgNr);
                }
                if(issi != null)
                {
                   // listObject.Issi = ReadValue(issi);
                    ai.adIssi = ReadValue(issi);
                   // listObject.adminList.Add(ai);
                    listObject.adminInfo = ai;
                }
                


            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }


        }

        
        //Method that gets the data from the list
        private static string ReadValue(string lineWithData)
        {
            int fromChar = lineWithData.IndexOf(",") + ",".Length;
            int toChar = lineWithData.LastIndexOf("|");
            return lineWithData.Substring(fromChar, toChar - fromChar);
        }


        [WebMethod]
        [ScriptMethod]
        public static List<ConfigValues> OpenFile_Click(string fileToOpen)
        {
            try
            {

                linesInDoc.Clear();
                unFilteredLinesInDoc.Clear();
                fileName = fileToOpen;

                using (StreamReader reader = new StreamReader(fullPath + "\\" + fileToOpen))
                {


                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line) && (!line.StartsWith("BtnRadio") && (line.StartsWith("Btn") || line.StartsWith("Status"))))
                        {

                            linesInDoc.Add(line);

                        }
                        else if (line.Contains("//"))
                        {

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(line))
                            {
                                unFilteredLinesInDoc.Add(line);

                            }
                        }
                    }
                }

                listObject.statusList.Clear();
                listObject.tgList.Clear();
                listObject.portList.Clear();
                listObject.shortList.Clear();
                listObject.linkList.Clear();
                listObject.quickList.Clear();
                lists.Clear();

                ExtractStatusInfo();
                ExtractTgInfo();
                ExtractPortInfo();
                ExtractKortNRInfo();
                ExtractUrlInfo();
                ExtractName();
                ExtractQuickButtonInfo();

                return lists;
            }
            catch(Exception ex)
            {
                _logger.Error(ex.ToString());
                return lists;
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static void DownloadFile_Click(string statusArr, string tgArr, string portArr, string shortArr, string linkArr, string quickArr, string adminArr)
        {
            try
            {
                
                GetTruckList();
                
                fullPathWithFileName = fullPath + "\\" + fileName;
                
                JavaScriptSerializer json = new JavaScriptSerializer();
                List<StatusInfo> statusList = json.Deserialize<List<StatusInfo>>(statusArr);
                List<TgInfo> tgList = json.Deserialize<List<TgInfo>>(tgArr);
                List<PortInfo> portList = json.Deserialize<List<PortInfo>>(portArr);
                List<ShortNrInfo> shortList = json.Deserialize<List<ShortNrInfo>>(shortArr);
                List<LinkInfo> linkList = json.Deserialize<List<LinkInfo>>(linkArr);
                List<QuickButtonInfo> quickList = json.Deserialize<List<QuickButtonInfo>>(quickArr);
                List<AdminInfo> adminInfo = json.Deserialize<List<AdminInfo>>(adminArr);

                using (StreamWriter sw = new StreamWriter(fullPathWithFileName))
                {
                    int j = 0;

                    for (int i = 0; i < unFilteredLinesInDoc.Count; i++)
                    {
                        if (unFilteredLinesInDoc[i].StartsWith("Namn"))
                        {
                            // sw.WriteLine(string.Format("Namn," + adminInfo[0].adNamn + "|"));
                            unFilteredLinesInDoc[i] = "Namn," + adminInfo[0].adNamn + "|";
                        }
                        else if (unFilteredLinesInDoc[i].StartsWith("LicensNumber"))
                        {
                           // sw.WriteLine(string.Format("LicensNumber," + adminInfo[0].adLicenseNumber + "|"));
                            unFilteredLinesInDoc[i] = "LicensNumber," + adminInfo[0].adLicenseNumber + "|";
                        }
                        else if (unFilteredLinesInDoc[i].StartsWith("ORGNR"))
                        {
                           // sw.WriteLine(string.Format("ORGNR," + adminInfo[0].adOrgNr + "|"));
                            unFilteredLinesInDoc[i] = "ORGNR," + adminInfo[0].adOrgNr + "|";
                        }
                        else if (unFilteredLinesInDoc[i].StartsWith("ISSI"))
                        {
                           // sw.WriteLine(string.Format("ISSI," + adminInfo[0].adIssi + "|"));
                            unFilteredLinesInDoc[i] = "ISSI," + adminInfo[0].adIssi + "|";
                        }

                    }

                    //  if (unFilteredLinesInDoc.Count != 0)
                    // {

                    for (j = 0; j < unFilteredLinesInDoc.Count; j++)
                        {
                            sw.WriteLine(unFilteredLinesInDoc[j]);
                            if (unFilteredLinesInDoc[j].Contains("TabletIMEI,") || unFilteredLinesInDoc[j].Contains("MSISDN,") ||
                                unFilteredLinesInDoc[j].Contains("BtnRadio1Mac,") || unFilteredLinesInDoc[j].Contains("BtnRadio2Mac,"))
                            {
                                sw.WriteLine("");
                            }

                            else if (unFilteredLinesInDoc[j].Contains("BtnRadio3Mac,"))
                            {
                                sw.WriteLine("");
                                j++;
                                break;
                            }
                        }

                    


                        for (int i = 0; i < tgList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sw.WriteLine("//Talgrupper");
                            }
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "TgNamn," + tgList[i].tgName + "|"));
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "TgGissi," + tgList[i].tgGissi + "|"));
                            sw.WriteLine("");
                        }
                        for (int i = 0; i < quickList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sw.WriteLine("//Status");
                            }
                            sw.WriteLine(string.Format("Status" + (i + 1) + "Text," + quickList[i].qbName + "|"));
                            sw.WriteLine(string.Format("Status" + (i + 1) + "," + quickList[i].qbStatus + "|"));
                            sw.WriteLine(string.Format("Status" + (i + 1) + "Dest1," + quickList[i].qbDest1 + "|"));
                            sw.WriteLine(string.Format("Status" + (i + 1) + "Dest2," + quickList[i].qbDest2 + "|"));
                            sw.WriteLine("");
                        }
                        for (int i = 0; i < statusList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sw.WriteLine("//Status");
                            }
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "StatusText," + statusList[i].stName + "|"));
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "Status," + statusList[i].stStatus + "|"));
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "StatusDest1," + statusList[i].stDest1 + "|"));
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "StatusDest2," + statusList[i].stDest2 + "|"));
                            if (i == statusList.Count - 1)
                            {
                                for (int k = 0; k < unFilteredLinesInDoc.Count; k++)
                                {
                                    if (unFilteredLinesInDoc[k].Contains("---"))
                                    {
                                        sw.WriteLine(unFilteredLinesInDoc[k]);
                                        break;
                                    }
                                }
                            }
                            sw.WriteLine("");

                        }
                        for (int i = 0; i < portList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sw.WriteLine("//Portar");

                                for (int k = 0; k < unFilteredLinesInDoc.Count; k++)
                                {
                                    if (unFilteredLinesInDoc[k].StartsWith("Portar,"))
                                    {
                                        sw.WriteLine(unFilteredLinesInDoc[k]);
                                        break;
                                    }
                                }
                            }
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "PortNamn," + portList[i].portName + "|"));
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "PortStatus," + portList[i].portStatus + "|"));
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "PortDest," + portList[i].portDest + "|"));
                            sw.WriteLine("");
                        }
                        for (int i = 0; i < shortList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sw.WriteLine("//KortNummer");
                            }
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "KortNamn," + shortList[i].shortName + "|"));
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "KortNr," + shortList[i].shortNr + "|"));
                            sw.WriteLine("");
                        }
                        for (int i = 0; i < linkList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sw.WriteLine("//Länkar");
                            }
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "UrlNamn," + linkList[i].linkName + "|"));
                            sw.WriteLine(string.Format("Btn" + (i + 1) + "UrlData," + linkList[i].linkUrl + "|"));
                            sw.WriteLine("");
                        }

                        for (int i = j; i < unFilteredLinesInDoc.Count; i++)
                        {
                            if (!unFilteredLinesInDoc[i].StartsWith("Portar,") && !unFilteredLinesInDoc[i].Contains("---"))
                                sw.WriteLine(unFilteredLinesInDoc[i]);

                            if (unFilteredLinesInDoc[i].Contains("ExtAlarmText,") || unFilteredLinesInDoc[i].Contains("ExtRadio4Mode,") || unFilteredLinesInDoc[i].Contains("MyRole,")
                               || unFilteredLinesInDoc[i].Contains("TgInsats1,") || unFilteredLinesInDoc[i].Contains("GateWayPort,") || unFilteredLinesInDoc[i].Contains("Slav2Port,")
                               || unFilteredLinesInDoc[i].Contains("IpModePath,") || unFilteredLinesInDoc[i].Contains("Button6Text,") || unFilteredLinesInDoc[i].Contains("Button21Text,"))
                            {
                                sw.WriteLine("");
                            }
                        }
                    }
               // }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        protected void LinkBtnClick(object sender, EventArgs e)
        {
            //Response.Write("window.open('www.google.se','_blank');");
        }
    }
}