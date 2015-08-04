using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "MP3 (*.mp3) | *.mp3",
            };
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                axWindowsMediaPlayer1.URL = ofd.FileName;
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

    }
}
