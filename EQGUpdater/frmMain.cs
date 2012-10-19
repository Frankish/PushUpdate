using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PushUpdate;

namespace PushUpdate
{

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        // System folder and variable shortcuts
        string windowsMedia = Environment.GetEnvironmentVariable("SystemRoot") + @"\Media\"; //Windows system sounds folder
        string VBNewLine = System.Environment.NewLine; //Shortened version of a newline in VB format

        // Set the minimum Windows version requirements
        int OSVersionMjr = 5; //OS Major version
        int OSVersionMnr = 1; //OS Minor version

        // Choose whether to enforce the minimum OS Req.
        Boolean enforceVersion = false;
        
        //  OS Version Table
        //  5.0 - Windows 2000
        //  5.1 - Windows XP (32bit)
        //  5.2 - Windows XP (64bit), Windows Server 2003
        //  6.0 - Windows Vista, Windows Server 2008
        //  6.1 - Windows Server 2008 R2, Windows 7
        //  6.2 - Windows 8
        
        
        //Interface buttons - Run or Cancel the update
        private void button1_Click(object sender, EventArgs e)      //Cancel the update
        {
            systemSound("windows exclamation.wav");

            string msg = "This is not a recommended option." + VBNewLine  + "Are you sure you do not wish to update now?";
            DialogResult dialogResult = MessageBox.Show(msg, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                //Exit application
                this.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)      //Run the install package
        {
            frmUpdate frmU = new frmUpdate();
            DialogResult result = frmU.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                //Dialog was cancelled
                MessageBox.Show("Operation Cancelled..");
            }
            else if (result == System.Windows.Forms.DialogResult.OK)
            {
                //Dialog was sucessful
                trayNotifier("Operation Completed", "This is body text to show the operation completed.");
            }
        }
        private void Form1_Load(object sender, EventArgs e)         //Used to run pre-start subs
        {
            //Cancel checking for crossthreadcalls for easier backgroundworker use
            CheckForIllegalCrossThreadCalls = false;
            
            //Run OS Check to meet minimum spec requirements
            CheckOSVersion(System.Environment.OSVersion.Version.Major, System.Environment.OSVersion.Version.Minor);

            //All other subs to be run onload
            systemSound("chimes.wav");
        }
        
        //Functions
        private void CheckOSVersion(int major, int minor)           //Runs on load.
        {
            
            if (major >= OSVersionMjr && minor >= OSVersionMnr)
            {
                //OS version meets minimum spec.
            }
            else
            {
                String msg = "";
                String caption = "";
                Boolean appClose = false;
                
                if (enforceVersion == true) //Check to see if the OS Version must be enforced.
                {
                    //OS Version req. is enforced and the application must exit.
                    caption = "PushUpdate Error";
                    msg = "Your version of Windows is not supported and this application cannot run.";
                    appClose = true;
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //Warn user about minimum requirements
                    caption = "PushUpdate Warning";
                    msg = "Your version of windows is not supported." + VBNewLine + "The application may behave in unpredictable ways, do you want to continue using it?";
                    appClose = false;
                    DialogResult result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == System.Windows.Forms.DialogResult.No)
                    {
                        Environment.Exit(2);
                    }

                }
                
                if (appClose == true)
                {
                    Environment.Exit(2);
                }
            }
        }
        private void systemSound(string filename)                   //Play Sound
        {
            SoundPlayer systemSound = new SoundPlayer(windowsMedia + filename);
            systemSound.Play();
        }
        private void trayNotifier(string title, string body)        //Show NotifyIcon Balloon
        {
            if (title != "")
            {
                if (body != "")
                {
                    notifyIcon1.BalloonTipTitle = title;
                    notifyIcon1.BalloonTipText = body;
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;

                    notifyIcon1.ShowBalloonTip(5000);
                    systemSound("chimes.wav");
                }
                else
                {
                    //Error no body
                }
            }
            else
            {
                //Error no title
            }
        }
    }
}
