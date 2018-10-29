using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ArisoTool;
using System.Xml.Serialization;
using System.IO;

namespace RemoteAccess
{
    class Program
    {

        static
            AES aes = new AES();

        static void Main(string[] args)
        {


            CreateRDSKeyFile();


            //https://gitee.com/huaant/pv01/blob/master/P01.txt

            //WebClient webclientForIP = new WebClient();
            //string IPdefine = webclientForIP.DownloadString("https://gitee.com/huaant/pv01/blob/master/P01.txt");
 
            //string decoded = aes.DecryptString(IPdefine);



            MSTSCConfig cfg = new MSTSCConfig();
            cfg.ServerIP = "23.100.96.142";
            cfg.PortNum = string.Empty;
            cfg.usrname = ".\\superqiu";
            cfg.password = "111111AAAAAA@";

            string text = aes.SerializeToString(cfg);

            string encrpyt = aes.EncryptString(text);
            string decript = aes.DecryptString(encrpyt);

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


            cfg.PortNum = DefaultSetup(cfg.PortNum,"3389");
            cfg.ServerIP = DefaultSetup(cfg.ServerIP, "23.100.96.142");
            cfg.usrname = DefaultSetup(cfg.usrname, ".\\superqiu");
            cfg.password = DefaultSetup(cfg.password, "111111AAAAAA@");


            string text = aes.SerializeToString(cfg);

            Console.WriteLine(text);

            Console.WriteLine();

            Console.WriteLine();
            string encrypt = aes.EncryptString(text);
            Console.WriteLine(encrypt);
            File.WriteAllText("P01.txt", encrypt);

            Console.WriteLine("p01 text created");

        }

        static string DefaultSetup(string input, string define)
        {
            if (string.IsNullOrWhiteSpace(input))
                return define;

            string rtn = input.Replace("\n","").Trim();
            if (string.IsNullOrWhiteSpace(rtn))
                rtn = define;

            return rtn;
        }


        // 雷洛他

    }

}
