﻿using System;
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
        

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //var ofd = new OpenFileDialog
            //{
            //    Filter = "MP3 (*.mp3) | *.mp3|Wav File (*.wav) | *.wav| All Files (*.*)|*.*",
            //};
            //if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    axWindowsMediaPlayer1.URL = ofd.FileName;
                
            //}


            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string[] files = Directory.GetFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories);
                
                foreach(string file in files)
                {
                    if (Path.GetExtension(file).ToLower() == ".mp3" || Path.GetExtension(file).ToLower() == ".wav" || Path.GetExtension(file).ToLower() == ".mp4")
                  {
                        lstPlaylist.Items.Add(file);
                  } 
                }
                
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
           // axWindowsMediaPlayer1.URL = lstPlaylist.SelectedItem.ToString();
            axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(axWindowsMediaPlayer1_PlayStateChange);
      
        }

        private void lstPlaylist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                lstPlaylist.SelectedIndex++;
            }
            else if (e.KeyCode == Keys.Down)
            {
                lstPlaylist.SelectedIndex--;
            }
           
        }

        private void lstPlaylist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2 )
            {
                axWindowsMediaPlayer1.URL = lstPlaylist.SelectedItem.ToString();
            }
        }


        private void lstPlaylistEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                axWindowsMediaPlayer1.URL = lstPlaylist.SelectedItem.ToString();
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }


        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if  (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                lblTest.Text = "stop";
                lstPlaylist.SelectedIndex++;
            }
            else
            {
                lblTest.Text = "play";
            }
        }

    }
}
