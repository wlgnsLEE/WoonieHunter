using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace WoonieHunter
{
    public partial class Setting : Form
    {
       
        int gamevolume;
        int chspeed;
        int enemyspeed;
        int skillnum;

        public event Action<int, int, int, int> ValuesUpdated;
        public Setting(int gv, int es, int cs, int sn)
        {
            InitializeComponent();

            this.MinimumSize = new Size(600, 600);
            this.MaximumSize = new Size(600, 600);

            //Win32.SetSoundVolume(5);  초기 볼륨 설정 (0~10)

            trackBar1.Value = Volume.GetSoundVolume();
            trackBar2.Value = es;
            trackBar3.Value = cs;
            trackBar4.Value = sn;

            gamevolume = trackBar1.Value;
            chspeed=trackBar3.Value;
            enemyspeed=trackBar2.Value;
            skillnum=trackBar4.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
           
            gamevolume = trackBar1.Value;

            Volume.SetSoundVolume(gamevolume);
        }
        public class Volume
        {
            #region
            [DllImport("winmm.dll")]
            public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

            [DllImport("winmm.dll")]
            public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

            public static void SetSoundVolume(int volume)
            {
                try
                {
                    int newVolume = ((ushort.MaxValue / 10) * volume);
                    uint newVolumeAllChannels = (((uint)newVolume & 0x0000ffff) | ((uint)newVolume << 16));
                    waveOutSetVolume(IntPtr.Zero, newVolumeAllChannels);
                }
                catch (Exception) { }
            }

            public static int GetSoundVolume()
            {
                int value = 0;
                try
                {
                    uint CurrVol = 0;
                    waveOutGetVolume(IntPtr.Zero, out CurrVol);
                    ushort CalcVol = (ushort)(CurrVol & 0x0000ffff);
                    value = CalcVol / (ushort.MaxValue / 10);
                }
                catch (Exception) { }
                return value;
            }
            #endregion
        }

       private void button1_Click(object sender, EventArgs e)
        {
            gamevolume = trackBar1.Value;
            enemyspeed = trackBar2.Value;
            chspeed = trackBar3.Value;
            skillnum = trackBar4.Value;

            ValuesUpdated.Invoke(gamevolume,enemyspeed,chspeed,skillnum);

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            
        }

        private void Setting_Load(object sender, EventArgs e)
        {

        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar4_Scroll_1(object sender, EventArgs e)
        {

        }
    }
}
