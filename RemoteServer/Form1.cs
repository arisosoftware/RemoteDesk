using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MSTSCLib;
using System.Net;
using RemoteAccess;
using System.IO;
using ArisoTool;

namespace RemoteServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            WebClient webclientForIP = new WebClient();


            string IPdefine = File.ReadAllText("..\\..\\P01.txt");

            AES aes = new AES();

            
            //   string IPdefine = webclientForIP.DownloadString("https://raw.githubusercontent.com/arisosoftware/RemoteDesk/master/RemoteAccess/P01.txt");
            string decoded = aes.DecryptString(IPdefine);
            Console.WriteLine(decoded);


            MSTSCConfig cfg = RemoteAccess.Program.SerializeFromString(decoded) as MSTSCConfig;



            this.axMsRdpClient71.Server = cfg.ServerIP;

            this.axMsRdpClient71.UserName = cfg.usrname;


            IMsTscNonScriptable secured = (IMsTscNonScriptable)this.axMsRdpClient71.GetOcx();


            this.axMsRdpClient71.OnConnected += new EventHandler(axMsRdpClient71_OnConnected);
            this.axMsRdpClient71.OnFatalError += new AxMSTSCLib.IMsTscAxEvents_OnFatalErrorEventHandler(axMsRdpClient71_OnFatalError);

            secured.ClearTextPassword = cfg.password;
            this.axMsRdpClient71.Connect();
            
        }

        void axMsRdpClient71_OnFatalError(object sender, AxMSTSCLib.IMsTscAxEvents_OnFatalErrorEvent e)
        {
            throw new NotImplementedException();
        }

        void axMsRdpClient71_OnConnected(object sender, EventArgs e)
        {
            this.axMsRdpClient71.Dock = DockStyle.Fill;
        }



    }
}
