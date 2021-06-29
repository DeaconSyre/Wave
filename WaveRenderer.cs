using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Numerics;

namespace Wave
{
    public partial class WaveRenderer : UserControl
    {
        /// <summary>
        /// The polling interval the WavePlayer reads from its WaveSegment
        /// expressed in milliseconds
        /// </summary>
        public TimeSpan RenderResolutionTs = TimeSpan.FromMilliseconds(256);
        /// <summary>
        /// Width of a cell in px
        /// </summary>
        private Vector2 RenderResolutionPx;

        public TimeSpan DrawSpan = TimeSpan.FromSeconds(10);
        public int TotalCells = 0;
        public int CellsToDraw = 0;
        public float ValueNormal = 1.0f;

        /// <summary>
        /// Rendered line's vertex to start drawing at for a given frame
        /// </summary>
        public int DrawCursor = 0;
        public void SetDrawCursor(TimeSpan elapsed)
        {
            DrawCursor = (int)(elapsed.TotalMilliseconds / RenderResolutionTs.TotalMilliseconds);
        }
        /// <summary>
        /// X: Intervals of RenderResolution
        /// Y: Normalized value of WavePoint Value
        /// </summary>
        public Vector2[] RenderSequence;
        //todo: expand to [list] for multi-segment playlist feature
        public WaveSegment WaveSegment;

        //todo: customizable pen
        public Pen LinePen = new Pen(Color.Black);

        private bool SegmentRendered = false;

        public WaveRenderer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// After you assign a Wavesegment call this.
        /// </summary>
        public void InitConstants()
        {
            SegmentRendered = true;
            if(Height % 2 == 1)
            {
                Height--;
            }
            WaveSegment.ValueCeiling = WaveSegment.Sequence.Max(s => s.Value);
            //todo: catch events with zoom function and control resizing events
            //refactor: overload TimeSpan./ operator
            TotalCells = (int)Math.Ceiling(WaveSegment.Sequence.Keys.Last() / RenderResolutionTs.TotalMilliseconds);
            CellsToDraw = (int)Math.Ceiling(DrawSpan.TotalMilliseconds / RenderResolutionTs.TotalMilliseconds);
            ValueNormal = Vector2.Normalize(new Vector2(WaveSegment.Sequence.Keys.Last(), WaveSegment.ValueCeiling)).Y;
            RenderResolutionPx.X = Width / TotalCells;
            RenderResolutionPx.Y = Height / TotalCells;
            //next: transform from native to canvas coordinate system is wrong. something about ambiguous rectangles and a square treeing trying to mix
            RenderSequence = new Vector2[TotalCells];
            try
            {
                for (int x = 0; x < TotalCells; x++)
                {
                    int currentTime = x * (int)RenderResolutionTs.TotalMilliseconds;
                    int currentLevel = (int)Math.Ceiling(WaveSegment.GetValueAt(currentTime) * ValueNormal);
                    RenderSequence[x] = new Vector2(x, currentLevel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("todo:Add proper error messaging.", ex);
            }
        }

        private void WaveRender_Paint(object sender, PaintEventArgs e)
        {
            if (!SegmentRendered)
                return;

            try
            {
                var canvas = e.Graphics;

                Point startpoint = Point.Empty;
                Point endpoint = Point.Empty;

                var length = CellsToDraw - DrawCursor;

                for (int x = 0; x < length-1; x++)
                {
                    startpoint.Y = Height - (int)RenderSequence[x].Y;
                    endpoint.X += (int)RenderResolutionPx.X;
                    endpoint.Y = Height - (int)RenderSequence[x + 1].Y;

                    canvas.DrawLine(LinePen, startpoint, endpoint);

                    startpoint.X += (int)RenderResolutionPx.X;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("todo: please add a proper error handler.", ex);
            }
        }
    }
}
