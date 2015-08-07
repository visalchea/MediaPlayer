using System;
using System.Data;
using System.Linq;
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

        void Play()
        {
            if (player.URL.Length == 0)
            {
                string path = lstPlaylist.SelectedItem.ToString();
                player.URL = path;
            }
            Text = Path.GetFileName(player.URL) + " - Media Player";
            if (lstPlaylist.Items.Count == 0) return;
            player.Ctlcontrols.play();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string supportedExtensions = "*.mp3,*.wav,*.mp4,*.mpg,*.mpeg,*.wma,*.avi";
                foreach (string file in Directory.GetFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower())))
                    lstPlaylist.Items.Add(file);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void btnFastForward_Click(object sender, EventArgs e)
        {
            // To get all of the available functionality of the player controls, cast the
            // value returned by player.Ctlcontrols to a WMPLib.IWMPControls3 interface. 
            WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)player.Ctlcontrols;

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
            WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)player.Ctlcontrols;

            // Check first to be sure the operation is valid.
            if (controls.get_isAvailable("fastReverse"))
            {
                controls.fastReverse();
            }
        }

        private void lstPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            //player.URL = lstPlaylist.SelectedItem.ToString();

        }

        private void lstPlaylist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstPlaylist_DoubleClick(null, null);
                e.SuppressKeyPress = true;
            }
        }

        private void lstPlaylist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // use DoubleClick instead, MouseDoubleClick is only with Mouse, not general
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //player.uiMode = "none";

            // Load play list from saved file
            var path = Path.Combine(Application.StartupPath, "playlist.m3u");
            if (!File.Exists(path)) return;
            var sr = new StreamReader(path);
            while (sr.Peek() >= 0)
                lstPlaylist.Items.Add(sr.ReadLine());
            sr.Close();
        }

        private void lstPlaylist_DoubleClick(object sender, EventArgs e)
        {
            player.URL = lstPlaylist.SelectedItem.ToString();
            Play();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Save current play list to file            
            if (lstPlaylist.Items.Count == 0) return;
            Hide();
            var path = Path.Combine(Application.StartupPath, "playlist.m3u");
            var sw = new StreamWriter(path);
            foreach (var f in lstPlaylist.Items)
            {
                sw.WriteLine(f);
            }
            sw.Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // TODO: open files (multiple selection is good), play then add to play list as well
        }

        //    private void player_EndOfStream(object sender, AxWMPLib._WMPOCXEvents_EndOfStreamEvent e)
        //    {
        //        var ind = lstPlaylist.SelectedIndex + 1;
        //        if (ind == lstPlaylist.Items.Count) ind = 0;
        //        lstPlaylist.SelectedIndex = ind;
        //    }
    }
}
