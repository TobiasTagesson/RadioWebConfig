using System.Collections.Generic;

namespace RadioWebConfig
{
  
        public class ConfigValues 
        {
           public List<StatusInfo> statusList = new List<StatusInfo>();
           public List<TgInfo> tgList = new List<TgInfo>();
           public List<PortInfo> portList = new List<PortInfo>();
           public List<ShortNrInfo> shortList = new List<ShortNrInfo>();
           public List<LinkInfo> linkList = new List<LinkInfo>();
           public List<QuickButtonInfo> quickList = new List<QuickButtonInfo>();
        // public List<AdminInfo> adminList = new List<AdminInfo>();
          public AdminInfo adminInfo { get; set; }

    }
    public class StatusInfo
    {
        public string stName { get; set; }
        public string stStatus { get; set; }
        public string stDest1 { get; set; }
        public string stDest2 { get; set; }
    }
    public class TgInfo
    {
        public string tgName { get; set; }
        public string tgGissi { get; set; }
    }
    public class PortInfo
    {
        public string portName { get; set; }
        public string portStatus { get; set; }
        public string portDest { get; set; }
        public string portLat { get; set; }
        public string portLon { get; set; }

    }
    public class ShortNrInfo
    {
        public string shortName { get; set; }
        public string shortNr { get; set; }
    }
    public class LinkInfo
    {
        public string linkName { get; set; }
        public string linkUrl { get; set; }
    }
    public class QuickButtonInfo
    {
        public string qbName { get; set; }
        public string qbStatus { get; set; }
        public string qbDest1 { get; set; }
        public string qbDest2 { get; set; }
    }

    public class AdminInfo
    {
        public string adNamn { get; set; }
        public string adLicenseNumber { get; set; }
        public string adOrgNr { get; set; }
        public string adIssi { get; set; }
        public string adMsisdn { get; set; }
    }

}