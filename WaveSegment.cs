using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Numerics;

namespace Wave
{
    public class WaveSegment
    {
        //refactor: no performance gain or user experience net gain for SortedList. Use SortedSet.getregion()?
        /// <summary>
        /// SortedList of Milliseconds elapsed to Height
        /// </summary>
        public SortedList<int, float> Sequence = new SortedList<int, float>();
        public float ValueCeiling = 1.0f;

        public WaveSegment() { }
        public WaveSegment(SortedList<int, float> _segment, float _ValueCeiling)
        {
            Sequence = new SortedList<int, float>(_segment);
            ValueCeiling = _ValueCeiling;
        }

        public int IndexLeftOf(int time)
        {
            if(Sequence.Count == 0)
            {
                return -1;
            }

            if (time > Sequence.Keys.Last()) {
                return Sequence.Count - 1;
            }

            return Sequence.IndexOfKey(Sequence.Last(v => v.Key <= time).Key);
        }
        public int IndexRightOf(int time)
        {
            if (Sequence.Count == 0 || time < 0)
            {
                return -1;
            }
            else if(time == 0)
            {
                return 0;
            }

            return Sequence.IndexOfKey(Sequence.First(v => v.Key >= time).Key);
        }

        public float GetValueAt(int time)
        {
            if(time < 0.0f || time > Sequence.Last().Key) {
                return -1;
            }
            else if (Sequence.ContainsKey(time))
            {
                return Sequence[time];
            }

            var lowerboundkey = Sequence.Keys[IndexLeftOf(time)];
            var upperboundkey = Sequence.Keys[IndexRightOf(time)];
            var upperbound = Sequence[upperboundkey];
            var lowerbound = Sequence[lowerboundkey];

            var stepline = (upperbound - lowerbound) * ((upperboundkey - lowerboundkey) / (time - lowerboundkey));
            return lowerbound + stepline;
        }
    }
}
