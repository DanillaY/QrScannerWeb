using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using System.Net;

namespace copyqr.Models.Entities
{
    public static class IpConfiguration
    {
        public static string Ip { get; private set; } = String.Empty;
        public static string  Configurate()
        {
            foreach (IPAddress ip in Dns.GetHostByName(Dns.GetHostName()).AddressList)
                if (ip.ToString().Contains("192.168") && ip.ToString() != "")
                    Ip = ip.ToString();

            if(Ip == String.Empty || Ip == "")
                Ip = "YOUR IP HERE";
            return Ip;
        }
    }
}
