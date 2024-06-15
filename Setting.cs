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
        public Form1 form1;

        public Setting()
        {
            InitializeComponent();

            this.MinimumSize = new Size(600, 600);
            this.MaximumSize = new Size(600, 600);

            form1 = new Form1();

            //Win32.SetSoundVolume(5);  초기 볼륨 설정 (0~10)

            trackBar1.Value = Volume.GetSoundVolume();
            trackBar2.Value = 5;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int volume;
            volume = trackBar1.Value;

            Volume.SetSoundVolume(volume);
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
            Form1 form2 = new Form1();
            form2.setenemySpeed(trackBar2.Value);
            form2.setSkillCount(trackBar3.Value);
            form2.setPlayerSpeed(trackBar4.Value);
            form2.Show();
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
    }
}
