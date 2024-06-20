using IDEProject.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace IDE_CASHCOUNT.Common.Security
{
    public class LicenseProvider
    {
        string licenseKey = EncryptDecrypt.Encrypt(HttpContext.Current.Request.Url.Host + ":" + DeviceProparties.GetMacAddress(), "ide2018Soft");
        //string url = "http://localhost:3012//WebService.asmx/AuthLicenseControl" + "?mac=" + DeviceProparties.GetMacAddress() + "&license=" + licenseKey.Replace("+", "%2B");
        public string LicenseControl() {
           // string url = "https://localhost:44328/WebService.asmx/AuthLicenseControl" + "?mac=" + DeviceProparties.GetMacAddress() + "&license=" + licenseKey.Replace("+", "%2B");
            string url = "http://81.176.228.197:1818/WebService.asmx/AuthLicenseControl" + "?mac=" + DeviceProparties.GetMacAddress() + "&license=" + licenseKey.Replace("+", "%2B");
            var client = new WebClient();
            string content = "";
            try
            {
                content = client.DownloadString(url).ToString();
                return content;
            }
            catch (Exception)
            {
                return "Xəta baş verdi.Lisenziya oxuna bilmedi!";
            }

        }
    }
}