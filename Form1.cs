using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Wave
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WaveRenderer cont = new WaveRenderer();
            DrawArea.Controls.Add(cont);
            cont.Size = cont.Parent.Size;

            WaveSegment seg = new WaveSegment();
            seg.Sequence.Add(0, 0);
            seg.Sequence.Add(10000, 10);

            cont.WaveSegment = seg;
            cont.InitConstants();
            for(int x =0; x < cont.RenderSequence.Length;x++)
            {
                DebugTxtBx.Text += cont.RenderSequence[x].ToString() + " ";
            }
        }
    }
}
