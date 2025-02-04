/// <summary>
/// Counter Class
/// used in other classes as a counter
/// for example the score or number of lines
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProgramTetris
{
    public class Counter
    {
        private int _count;

        public Counter()
        {
            _count = 0;
        }

        public void Increment(int num) // increase the counter by the given number
        {
            _count += num;
        }

        public void Reset() // resets the counter
        {
            _count = 0;
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }
    }
}
