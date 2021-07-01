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
using System.Data.Common;

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
        /// x: Width of a cell in px
        /// y: Scale of value/valueceiling at x (0-1)
        /// </summary>
        private Vector2 RenderResolutionPx;

        public TimeSpan DrawSpan = TimeSpan.FromSeconds(10);
        public int TotalCells = 0;
        public int CellsToDraw = 0;
        public Vector2 ValueNormal = Vector2.One;

        /// <summary>
        /// Rendered line's vertex to start drawing at for a given frame
        /// </summary>
        public int DrawCursor = 0;
        public void SetDrawCursor(TimeSpan elapsed)
        {
            DrawCursor = (int)(elapsed.TotalMilliseconds / RenderResolutionTs.TotalMilliseconds);
        }
        /// <summary>
        /// //todo: update
        /// This vector array describes a series of points that are derived from
        /// the data provided by WaveSegment. It exploits the WaveSegment.GetValueAt()
        /// to generate data between points in the WaveSegment.
        /// 
        /// [0].X: should only have a value of 0 if the lowest key in the WaveSegment is 0
        /// [Z].X: is not normalized
        /// [last].X: should have the last key of WaveSegment
        /// [Z].Y: should be a value between 0-1;
        /// X: Convert: WaveSegment.X's (timespan) to offset from ...ex: when/on first: x*renderresolutionpx.x*(0||-N1)*
        ///                     ter having been adjusted to 0-N-End where length of N-N 
        ///                    is limited to RenderResolutionTs and then map that to the screenspace  Width/(zoom*RenderResolutionPx.X)
        /// Y: Height - (Value*RenderResolutionPx.Y*Height)
        /// </summary>
        public Vector2[] RenderSequence;
        //todo: expand to [list] for multi-segment playlist feature
        public WaveSegment WaveSegment;
        public float Zoom = 1.0f;

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
            if(Width % 2 == 1)
            {
                Width--;
            }
            WaveSegment.ValueCeiling = WaveSegment.Sequence.Max(s => s.Value);
            //todo: catch events with zoom function and control resizing events
            //refactor: overload TimeSpan./ operator
            TotalCells = (int)Math.Ceiling(WaveSegment.Sequence.Keys.Last() / RenderResolutionTs.TotalMilliseconds);
            CellsToDraw = (int)Math.Ceiling(DrawSpan.TotalMilliseconds / RenderResolutionTs.TotalMilliseconds);
            ValueNormal = Vector2.Normalize(new Vector2(WaveSegment.Sequence.Keys.Last(), WaveSegment.ValueCeiling));
            RenderResolutionPx.X = Width / TotalCells;//next: is this right?
            RenderResolutionPx.Y = 1 / WaveSegment.ValueCeiling;
            
            RenderSequence = new Vector2[TotalCells];
            try
            {
                Vector2 slope = Vector2.Zero;
                for (int x = 0; x < TotalCells; x++)
                {
                    //todo: check for ntree connection in following
                    slope = WaveSegment.GetSlopeAt(x * (int)RenderResolutionTs.TotalMilliseconds);
                    RenderSequence[x] = slope;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("todo:Add proper error messaging.", ex);
            }
        }

        private Matrix4x4 TransformaationMatrix(Vector2 a, Vector2 b)
        {

            return Matrix4x4.Identity;
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

                int cursorhead = 0;
                if(RenderSequence.Length > 0 && RenderSequence[0].X <= 0)
                {
                    cursorhead++;
                }

                for (int x = 0 + cursorhead; x < length-1; x++)
                {
                    startpoint.Y += (int)Math.Round(Height - RenderSequence[x].Y / RenderResolutionPx.Y);
                    endpoint.X += startpoint.X + (int)RenderResolutionPx.X;
                    endpoint.Y += (int)Math.Round(Height - RenderSequence[x + 1].Y / RenderResolutionPx.Y);

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
