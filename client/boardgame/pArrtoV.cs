using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace boardgame
{
    public static class pArrtoV
    {
        public static Vector2[] Vect(this Point[] point)
        {
            return new Vector2[] { new Vector2(point[0].X, point[0].Y), new Vector2(point[1].X, point[1].Y), new Vector2(point[2].X, point[2].Y), new Vector2(point[3].X, point[3].Y) };
        }
    }
}
