using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    public class Position
    {
        public int x,y;
        public Position() { x = 0; y = 0; }
        public Position(int x0, int y0)
        {
            x = x0;
            y = y0;
        }
        public double Norme1()
        {
            return x+y;
        }
        public double Norme2()
        {
            return Math.Sqrt(x * x + y * y);
        }
        public static Position operator +(Position p1, Position p2)
        {
            return new Position(p1.x + p2.x, p1.y + p2.y);
        }
        public static Position operator-(Position p1, Position p2)
        {
            return new Position(p1.x - p2.x, p1.y - p2.y);
        }
        public static Boolean operator==(Position p1, Position p2)
        {
            if ((Object)p1 == null || (Object)p2 == null)
                return false;
            return p1.x==p2.x && p1.y==p2.y;
        }
        public static Boolean operator !=(Position p1, Position p2)
        {
            return !(p1 == p2);
        }
        public override bool Equals(Object? obj)
        {
            if (obj is not Position pos)
                return false;
            return this == pos;
        }
        public override int GetHashCode()
        {
            int result = x;
            result = 31 * result + y;
            return result;
        }
    }
}
