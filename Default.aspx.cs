using System;
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

        public static int listDevider;

  

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
            if (Request.Cookies["myCookie"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            RenderSomeTextBoxes(30);
            RenderTgTextBoxes(60);
            RenderRestOfTextBoxes(10);

            GetStationCode();
            GetTruckList();
            
            
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

        public static int CheckQuickButtonIndex(int index)
        {
            try
            {
                string ld = filteredLinesInDoc[index].Substring(6, 2);
                string ld2 = filteredLinesInDoc[index].Substring(6, 1);

                bool successfullyParsed = int.TryParse(ld, out listDevider);

                if (!successfullyParsed)
                {
                    int.TryParse(ld2, out listDevider);
                }
                return listDevider;
            }
            catch (ArgumentOutOfRangeException aex)
            {
                _logger.Error(aex.ToString());
                return listDevider;
            }
            catch (IndexOutOfRangeException iex)
            {
                _logger.Error(iex.ToString());
                return listDevider;
            }
        }
        public static int CheckButtonIndex(int index)
        {
            try
            {
                string ld = filteredLinesInDoc[index].Substring(3, 2);
                string ld2 = filteredLinesInDoc[index].Substring(3, 1);

                bool successfullyParsed = int.TryParse(ld, out listDevider);

                if (!successfullyParsed)
                {
                    int.TryParse(ld2, out listDevider);
                }
                return listDevider;
            }
            catch (ArgumentOutOfRangeException aex)
            {
                _logger.Error(aex.ToString());
                return listDevider;
            }
            catch (IndexOutOfRangeException iex)
            {
                _logger.Error(iex.ToString());
                return listDevider;
            }
        }

        public static void ExtractQuickButtonInfo()
        {
            try
            {
                int j = 1;


                filteredLinesInDoc.Clear();


                foreach (string buttonLine in linesInDoc)
                {
                    if (buttonLine.StartsWith("Status"))
                    {
                        filteredLinesInDoc.Add(buttonLine);
                    }
                }

                /*listObject.quickList = new List<QuickButtonInfo>()*/;
                QuickButtonInfo qi = new QuickButtonInfo();

                for (int x = 0; x < filteredLinesInDoc.Count; x++)
                {

                    CheckQuickButtonIndex(x);

                    if (listDevider == j)
                    {

                        if (filteredLinesInDoc[x].Contains("Text,"))
                        {
                            qi = new QuickButtonInfo();
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            //ListofLists.statusList[0].
                            qi.qbName = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);
                        }
                        else if (filteredLinesInDoc[x].Contains(string.Format("Status" + listDevider + ",")))
                        {
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            qi.qbStatus = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);
                        }
                        else if (filteredLinesInDoc[x].Contains("Dest1,"))
                        {
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            qi.qbDest1 = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);
                        }
                        else if (filteredLinesInDoc[x].Contains("Dest2,"))
                        {
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            qi.qbDest2 = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);

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


                filteredLinesInDoc.Clear();


                foreach (string buttonLine in linesInDoc)
                {
                    if (buttonLine.Contains("Port"))
                    {
                        if (buttonLine.Contains("Btn"))
                        {
                            filteredLinesInDoc.Add(buttonLine);
                        }
                    }
                }

                //listObject.portList = new List<PortInfo>();
                PortInfo pi = new PortInfo();

                for (int x = 0; x < filteredLinesInDoc.Count; x++)
                {
                    CheckButtonIndex(x);

                    if (listDevider == j)
                    {

                        if (filteredLinesInDoc[x].Contains("PortNamn,"))
                        {
                            pi = new PortInfo();
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            pi.portName = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);
                        }
                        else if (filteredLinesInDoc[x].Contains("PortStatus,"))
                        {
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            pi.portStatus = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);
                        }
                        else if (filteredLinesInDoc[x].Contains("PortDest,"))
                        {
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            pi.portDest = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);

                            listObject.portList.Add(pi);
                            j++;
                        }
                    }
                }
                //lists.Add(listObject);
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


                filteredLinesInDoc.Clear();
                

                foreach (string buttonLine in linesInDoc)
                {
                    if (buttonLine.Contains("Status"))
                    {
                        if (buttonLine.Contains("Btn"))
                        {
                            filteredLinesInDoc.Add(buttonLine);
                        }
                    }
                }

                //listObject.statusList = new List<StatusInfo>();

                StatusInfo si = new StatusInfo();
                for (int x = 0; x < filteredLinesInDoc.Count; x++)
                {

                    CheckButtonIndex(x);

                    if (listDevider == j)
                    {

                        if (filteredLinesInDoc[x].Contains("StatusText,"))
                        {
                            si = new StatusInfo();
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            si.stName = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);
                        }
                        else if (filteredLinesInDoc[x].Contains("Status,"))
                        {
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            si.stStatus = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);
                        }
                        else if (filteredLinesInDoc[x].Contains("StatusDest1,"))
                        {
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            si.stDest1 = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);
                        }
                        else if (filteredLinesInDoc[x].Contains("StatusDest2,"))
                        {
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            si.stDest2 = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);

                            listObject.statusList.Add(si);

                            

                            j++;
                        }
                    }
                    
                }
                //lists.Add(listObject);
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

                filteredLinesInDoc.Clear();
                    


                foreach (string buttonLine in linesInDoc)
                {
                    if (buttonLine.Contains("Kort"))
                    {
                        if (buttonLine.Contains("Btn"))
                        {
                            filteredLinesInDoc.Add(buttonLine);
                        }
                    }
                }
                //listObject.shortList = new List<ShortNrInfo>();
                ShortNrInfo sni = new ShortNrInfo();

                for (int x = 0; x < filteredLinesInDoc.Count; x++)
                {
                    CheckButtonIndex(x);

                    if (listDevider == j)
                    {

                        if (filteredLinesInDoc[x].Contains("KortNamn,"))
                        {
                            sni = new ShortNrInfo();
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");

                            sni.shortName = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);
                        }
                        else if (filteredLinesInDoc[x].Contains("KortNr,"))
                        {
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            sni.shortNr = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);

                            listObject.shortList.Add(sni);
                            j++;

                        }
                    }
                }
                //lists.Add(listObject);
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

               
                filteredLinesInDoc.Clear();
                   


                foreach (string buttonLine in linesInDoc)
                {
                    if (buttonLine.Contains("Url"))
                    {
                        if (buttonLine.Contains("Btn"))
                        {
                            filteredLinesInDoc.Add(buttonLine);
                        }
                    }
                }

                //listObject.linkList = new List<LinkInfo>();
                LinkInfo li = new LinkInfo();

                for (int x = 0; x < filteredLinesInDoc.Count; x++)
                {
                    CheckButtonIndex(x);

                    if (listDevider == j)
                    {

                        if (filteredLinesInDoc[x].Contains("UrlNamn,"))
                        {

                            li = new LinkInfo();
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            li.linkName = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);
                        }
                        else if (filteredLinesInDoc[x].Contains("UrlData,"))
                        {
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            li.linkUrl = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);

                            listObject.linkList.Add(li);
                            j++;

                        }
                    }
                }
                //lists.Add(listObject);
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

                
                filteredLinesInDoc.Clear();
                    

                foreach (string buttonLine in linesInDoc)
                {
                    if (buttonLine.Contains("Tg"))
                    {
                        if (buttonLine.Contains("Btn"))
                        {
                            filteredLinesInDoc.Add(buttonLine);
                        }
                    }
                }

                //listObject.tgList = new List<TgInfo>();
                TgInfo ti = new TgInfo();

                for (int x = 0; x < filteredLinesInDoc.Count; x++)
                {
                    CheckButtonIndex(x);

                    if (listDevider == j)
                    {

                        if (filteredLinesInDoc[x].Contains("TgNamn,"))
                        {
                            ti = new TgInfo();
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");

                            ti.tgName = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);
                        }
                        else if (filteredLinesInDoc[x].Contains("TgGissi,"))
                        {
                            int fromChar = filteredLinesInDoc[x].IndexOf(",") + ",".Length;
                            int toChar = filteredLinesInDoc[x].LastIndexOf("|");
                            ti.tgGissi = filteredLinesInDoc[x].Substring(fromChar, toChar - fromChar);

                            listObject.tgList.Add(ti);
                            j++;

                        }
                    }
                }
                //lists.Add(listObject);
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
                            //unFilteredLinesInDoc.Add(line);
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

                //string docFilter = "Status";
                //ExtractStatusInfo(docFilter);
                //docFilter = "Tg";
                //ExtractTgInfo(docFilter);
                //docFilter = "Port";
                //ExtractPortInfo(docFilter);
                //docFilter = "Kort";
                //ExtractKortNRInfo(docFilter);
                //docFilter = "Url";
                //ExtractUrlInfo(docFilter);
                //docFilter = "Status";

                ExtractStatusInfo();
                ExtractTgInfo();
                ExtractPortInfo();
                ExtractKortNRInfo();
                ExtractUrlInfo();



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
        public static void DownloadFile_Click(string statusArr, string tgArr, string portArr, string shortArr, string linkArr, string quickArr)
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

                using (StreamWriter sw = new StreamWriter(fullPathWithFileName))
                {
                    int j = 0;

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
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }
    }
}