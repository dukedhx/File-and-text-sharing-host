using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.Win32;

namespace ConsoleApplication1
{
    static public class IntlHelper
    {
       
        //static public String getIP()
        //{

        //    String localIP = "";

        //    ShowNetworkInterfaceMessage();

        //    return localIP;
        //}
        static public String[] ShowNetworkInterfaceMessage()
        {
            LinkedList<String> ips = new LinkedList<String>();
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            

                //    string fCardType = "unknow net card";




                //  string fRegistryKey ="SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\"+ adapter.Id + "\\Connection";


                //  RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                //  if (rk != null)
                //  {
                //      string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();


                //      string fDefaultNameResourceId = rk.GetValue("DefaultNameResourceId", "").ToString();

                //      int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));

                //      if (fPnpInstanceID.Length > 3 &&

                //          fPnpInstanceID.Substring(0, 3) == "PCI")
                //      {


                //          if (fDefaultNameResourceId == "1803")

                //              fCardType = "local net card";

                //          else

                //              fCardType = "wireless net card";

                //      }

                //      else if (fMediaSubType == 2)

                //          fCardType = "wireless net card";

                ////      Console.WriteLine(fMediaSubType);


                if (!String.IsNullOrEmpty(adapter.GetPhysicalAddress().ToString()))

                foreach (UnicastIPAddressInformation UnicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)

                    if (UnicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                       String addr= UnicastIPAddressInformation.Address.ToString();
                       if (!(addr.StartsWith("0") || addr.StartsWith("169")))
                           ips.AddLast(addr);
                    }
                //    DisplayDescription(fCardType, adapter);

            return ips.ToArray();
            
        }
       // private static void DisplayDescription(string fCardType, NetworkInterface adapter)
       // {

       //     Console.WriteLine("------------------------------------");

       //     Console.WriteLine("-- " + fCardType);

       //     Console.WriteLine("------------------------------------");

       //     Console.WriteLine("Id .................. : {0}", adapter.Id);

       //     Console.WriteLine("Name ................ : {0}", adapter.Name);

       //     Console.WriteLine("Description ......... : {0}", adapter.Description);

            
       //     Console.WriteLine("Interface type ...... : {0}", adapter.NetworkInterfaceType);


       //     Console.WriteLine("Is receive only...... : {0}", adapter.IsReceiveOnly);
           
       //     Console.WriteLine("Multicast............ : {0}", adapter.SupportsMulticast);
          

       //     Console.WriteLine("Speed ............... : {0}", adapter.Speed);
       
       //     Console.WriteLine("Physical Address .... : {0}", adapter.GetPhysicalAddress().ToString());
            
       //     IPInterfaceProperties fIPInterfaceProperties = adapter.GetIPProperties();

       //     UnicastIPAddressInformationCollection UnicastIPAddressInformationCollection = fIPInterfaceProperties.UnicastAddresses;

       //     foreach (UnicastIPAddressInformation UnicastIPAddressInformation in UnicastIPAddressInformationCollection)
       //     {
       //         if (UnicastIPAddressInformation.Address.AddressFamily== AddressFamily.InterNetwork)

       //             Console.WriteLine("Ip Address .......... : {0}",UnicastIPAddressInformation.Address); 
       //     }

       //     Console.WriteLine();
       //}       
    }
}
