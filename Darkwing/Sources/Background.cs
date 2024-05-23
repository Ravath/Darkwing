using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    class Background
    {
        public readonly List<int> left;//left wall position
        public readonly List<int> right;//right wall position
        private readonly int width;
        public readonly int height;
        private readonly int noise;
        private readonly Random rand;
        public Background(int width, int height, int noise=0) {
            this.height = height;
            this.width = width;
            this.noise = noise;
            left = [];
            right = [];
            rand = new Random();
            left.Add(0);
            right.Add(width - 1);
            int _x = 0; // Wall shift
            for(int i=1; i< height; i++)
            {
                _x = rand.Next(-noise, noise+1);
                left.Add( Math.Max(0,left[i - 1] + _x) );
                _x = rand.Next(-noise, noise+1);
                right.Add( Math.Min(width-1, right[i - 1] + _x) );
            }
        }
        public void Display()
        {
            for(int i =0; i<height; i++)
            {
                Console.SetCursorPosition(left[i], i);
                Console.Write('H');
                Console.SetCursorPosition(right[i], i);
                Console.Write('H');
            }
           
        }
        /// <summary>
        /// Shift the background vertically by _y console characters.
        /// </summary>
        /// <param name="_y"> _y>0 : Upper shift. _y<0: Lower shift.</param>
        public void Scroll(int _y)
        {
            if (_y == 0)
                return;
            bool up = (_y > 0);
            int y = Math.Abs(_y);
            for(int i=0; i< y; i++)
            {
                RemoveLine(up);
                if (up)
                {
                    AddLineUp();
                }
                else
                {
                    AddLineDown();
                }
            }
        }
        /// <summary>
        /// Add a line to the top.
        /// </summary>
        private void AddLineUp()
        {
            int _x;
            _x = rand.Next(-noise, noise + 1);
            left.Insert(0, Math.Max( left[0] + _x , 0 ));
            _x = rand.Next(-noise, noise + 1);
            right.Insert(0, Math.Min( right[0] + _x , width - 1 ));
        }
        /// <summary>
        /// Add a line to the bottom.
        /// </summary>
        private void AddLineDown()
        {
            int _x;
            _x = rand.Next(-noise, noise + 1);
            left.Add(Math.Max(left[height-1] + _x, 0));
            _x = rand.Next(-noise, noise + 1);
            right.Add(Math.Min(right[height - 1] + _x, width - 1));
        }
        /// <summary>
        /// Removes a line, from the top or from the bottom.
        /// </summary>
        /// <param name="top"></param>
        private void RemoveLine(bool top = true)
        {
            if (top) // Remove top line
            {
                right.Remove(0);
                left.Remove(0);
                return;
            }
            else // Remove bottom line
            {
                right.Remove(height - 1);
                left.Remove(height - 1);
                return;
            }

        }
    }
}
