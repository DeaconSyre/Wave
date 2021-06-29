using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            for(int x = 0; x <= 10; x++)
            {
                seg.Sequence.Add(x*1000, x);
            }

            cont.WaveSegment = seg;
            cont.InitConstants();
        }
    }
}
