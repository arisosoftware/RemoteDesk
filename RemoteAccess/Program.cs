using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ArisoTool;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace RemoteAccess
{
    public class Program
    {

        static
            AES aes = new AES();



        public static void RemoteConnect(MSTSCConfig cfg)
        {


            AxMSTSCLib.AxMsRdpClient4 rdpc = new AxMSTSCLib.AxMsRdpClient4();

            rdpc.UserName = cfg.usrname;
            rdpc.AdvancedSettings2.ClearTextPassword = cfg.password;
            rdpc.Server = cfg.ServerIP;
            rdpc.AdvancedSettings2.RDPPort = int.Parse(cfg.PortNum);
            rdpc.Domain = ".";
            rdpc.Connect();

            //////AxMSTSCLib.AxMsTscAxNotSafeForScripting remoteserver = new AxMSTSCLib.AxMsTscAxNotSafeForScripting();

            //////// 
            //////// RDP
            //////// 
            //////remoteserver.UserName = cfg.usrname;

            //////remoteserver.Server = cfg.ServerIP;

            //////remoteserver.SecuredSettings.FullScreen = 1;
            //////remoteserver.AdvancedSettings.Compress = 1;

            //////MSTSCLib.IMsTscNonScriptable secured = (MSTSCLib.IMsTscNonScriptable)remoteserver.SecuredSettings;
            //////secured.ClearTextPassword = cfg.password;
            //////remoteserver.Connect();


        }

        public static void ReadAndRemoteLogin()
        {


            WebClient webclientForIP = new WebClient();


            string IPdefine = File.ReadAllText("..\\..\\P01.txt");


            //   string IPdefine = webclientForIP.DownloadString("https://raw.githubusercontent.com/arisosoftware/RemoteDesk/master/RemoteAccess/P01.txt");
            string decoded = aes.DecryptString(IPdefine);
            Console.WriteLine(decoded);


            MSTSCConfig cfg = SerializeFromString(decoded) as MSTSCConfig;

            string file = File.ReadAllText("remoteserver.RDP");

           file =  file.Replace("ServerIP", cfg.ServerIP);
            File.WriteAllText("server.RDP", file);

            Console.WriteLine("请 输入密码 111111AAAAAA@  或者 control -P 复制");


            Clipboard.SetDataObject("111111AAAAAA@");

            //// try again...
            //System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //proc.EnableRaisingEvents = false;
            //proc.StartInfo.FileName = "rundll32.exe";
            //proc.StartInfo.Arguments = "shell32,OpenAs_RunDLL " + "remoteserver.RDP";
            //proc.Start();

 
            System.Diagnostics.Process shellProcess = new System.Diagnostics.Process();
            shellProcess.StartInfo.FileName = "mstsc.exe";
            shellProcess.StartInfo.Arguments = "server.RDP";
            shellProcess.StartInfo.ErrorDialog = true;
            shellProcess.Start();

        }

        public static object SerializeFromString(string fromSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MSTSCConfig));

            using (StringReader textReader = new StringReader(fromSerialize))
            {
                object obj = xmlSerializer.Deserialize(textReader);
                return obj;
            }
        }

        [STAThread]
        static void Main(string[] args)
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            if (args.Length > 1)
            {

                CreateRDSKeyFile();

            }
            else
            {
                ReadAndRemoteLogin();
            }


            ////https://gitee.com/huaant/pv01/blob/master/P01.txt

            ////WebClient webclientForIP = new WebClient();
            ////string IPdefine = webclientForIP.DownloadString("https://gitee.com/huaant/pv01/blob/master/P01.txt");

            ////string decoded = aes.DecryptString(IPdefine);



            //MSTSCConfig cfg = new MSTSCConfig();
            //cfg.ServerIP = "23.100.96.142";
            //cfg.PortNum = string.Empty;
            //cfg.usrname = ".\\superqiu";
            //cfg.password = "111111AAAAAA@";

            //string text = aes.SerializeToString(cfg);

            //string encrpyt = aes.EncryptString(text);
            //string decript = aes.DecryptString(encrpyt);

        }


        static void CreateRDSKeyFile()
        {
            Console.WriteLine("Please enter server IP");
            MSTSCConfig cfg = new MSTSCConfig();
            cfg.ServerIP = Console.ReadLine();
            Console.WriteLine("Please enter Port number");
            cfg.PortNum = Console.ReadLine();
            Console.WriteLine("Please enter username");
            cfg.usrname = Console.ReadLine();
            Console.WriteLine("Please enter password");
            cfg.usrname = Console.ReadLine();


            cfg.PortNum = DefaultSetup(cfg.PortNum, "3389");
            cfg.ServerIP = DefaultSetup(cfg.ServerIP, "23.100.96.142");
            cfg.usrname = DefaultSetup(cfg.usrname, ".\\superqiu");
            cfg.password = DefaultSetup(cfg.password, "111111AAAAAA@");


            string text = aes.SerializeToString(cfg);

            Console.WriteLine(text);

            Console.WriteLine();

            Console.WriteLine();
            string encrypt = aes.EncryptString(text);
            Console.WriteLine(encrypt);
            File.WriteAllText("..\\..\\P01.txt", encrypt);

            Console.WriteLine("p01 text created");


        }

        static string DefaultSetup(string input, string define)
        {
            if (string.IsNullOrWhiteSpace(input))
                return define;

            string rtn = input.Replace("\n", "").Trim();
            if (string.IsNullOrWhiteSpace(rtn))
                rtn = define;

            return rtn;
        }


        // 雷洛他

    }

}


//RemoteConnect(cfg);


//MSTSCLib.IMsRdpClient7 client = n 


//var rdpGateway = "gw.domain.local";
//var rdpServer = "rdsh.domain.local";

//rdp.Server = rdpServer;
//rdp.AdvancedSettings7.EnableCredSspSupport = true;
//rdp.AdvancedSettings8.NegotiateSecurityLayer = true;
//rdp.AdvancedSettings7.AuthenticationLevel = 0;

//if (!string.IsNullOrWhiteSpace(rdpGateway))
//{
//    rdp.TransportSettings3.GatewayHostname = rdpGateway;
//    rdp.TransportSettings3.GatewayUsageMethod = 1;
//    rdp.TransportSettings3.GatewayCredsSource = 0;
//    rdp.TransportSettings3.GatewayProfileUsageMethod = 1;
//}

//rdp.Connect();