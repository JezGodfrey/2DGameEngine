using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine1.TheEngine
{
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2()
        {
            X = Zero().X;
            Y = Zero().Y;
        }
        public Vector2(float x, float y)
        {
            // this.X = X;
            // this.Y = Y; -- where the arguments are set to X and Y
            X = x;
            Y = y;
        }

        // When class is first initialized, if no values specified, X and Y set to 0. Made static as we'll only retrieve values here, not set them.
        public static Vector2 Zero()
        {
            return new Vector2(0,0);
        }

    }
}
