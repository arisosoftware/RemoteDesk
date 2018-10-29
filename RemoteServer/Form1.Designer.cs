namespace RemoteServer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axMsRdpClient71 = new AxMSTSCLib.AxMsRdpClient7();
            this.axMsRdpClient61 = new AxMSTSCLib.AxMsRdpClient6();
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient71)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient61)).BeginInit();
            this.SuspendLayout();
            // 
            // axMsRdpClient71
            // 
            this.axMsRdpClient71.Enabled = true;
            this.axMsRdpClient71.Location = new System.Drawing.Point(30, 25);
            this.axMsRdpClient71.Name = "axMsRdpClient71";
            this.axMsRdpClient71.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMsRdpClient71.OcxState")));
            this.axMsRdpClient71.Size = new System.Drawing.Size(192, 192);
            this.axMsRdpClient71.TabIndex = 0;
            // 
            // axMsRdpClient61
            // 
            this.axMsRdpClient61.Enabled = true;
            this.axMsRdpClient61.Location = new System.Drawing.Point(0, 0);
            this.axMsRdpClient61.Name = "axMsRdpClient61";
            this.axMsRdpClient61.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMsRdpClient61.OcxState")));
            this.axMsRdpClient61.Size = new System.Drawing.Size(192, 192);
            this.axMsRdpClient61.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 708);
            this.Controls.Add(this.axMsRdpClient61);
            this.Controls.Add(this.axMsRdpClient71);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient71)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient61)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMSTSCLib.AxMsRdpClient7 axMsRdpClient71;
        private AxMSTSCLib.AxMsRdpClient6 axMsRdpClient61;
    }
}

