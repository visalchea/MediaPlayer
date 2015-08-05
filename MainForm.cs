using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MediaPlayer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        string[] files;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //var ofd = new OpenFileDialog
            //{
            //    Filter = "MP3 (*.mp3) | *.mp3|Wav File (*.wav) | *.wav",
            //};
            //if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    axWindowsMediaPlayer1.URL = ofd.FileName;
            //}

           
                var fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    files = Directory.GetFiles(fbd.SelectedPath);
                    foreach (string file in files)
                    {
                        if (Path.GetExtension(file).ToLower() == ".mp3" || Path.GetExtension(file) == ".wav")
                        {
                            //lstPlaylist.Items.Add(Path.GetFileName(file));
                            lstPlaylist.Items.Add(file);
                        }
                    }
                    lstPlaylist.SelectedIndex = 0;
                }

          
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();      
        }

        private void btnFastForward_Click(object sender, EventArgs e)
        {
            // To get all of the available functionality of the player controls, cast the
            // value returned by player.Ctlcontrols to a WMPLib.IWMPControls3 interface. 
            WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)axWindowsMediaPlayer1.Ctlcontrols;

            // Check first to be sure the operation is valid.
            if (controls.get_isAvailable("fastForward"))
            {
                controls.fastForward();
            }
        }

        private void btnReversePlayer_Click(object sender, EventArgs e)
        {
            // To get all of the available functionality of the player controls, cast the
            // value returned by player.Ctlcontrols to a WMPLib.IWMPControls3 interface. 
            WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)axWindowsMediaPlayer1.Ctlcontrols;

            // Check first to be sure the operation is valid.
            if (controls.get_isAvailable("fastReverse"))
            {
                controls.fastReverse();
            }
        }

        private void lstPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = lstPlaylist.SelectedItem.ToString();
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

    }
}
