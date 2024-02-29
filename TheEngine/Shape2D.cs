using System;
using System.Drawing;

namespace GameEngine1.TheEngine
{
    public class Shape2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public Color ShapeColor;
        public string Tag = "";
        public float Angle;

        public Shape2D(Vector2 position, Vector2 scale, Color shapecolor, string tag, float angle = 0)
        {
            Position = position;
            Scale = scale;
            ShapeColor = shapecolor;
            Tag = tag;
            Angle = angle;

            Log.Info($"[SHAPE2D]({Tag}) has been created.");

            // Whenever a new shape is created, register it in the game engine
            MainEngine.RegisterShape(this);

        }

        // Collision detection - initially done between 2 specific objects, now done by tag to apply to multiple objects more cleanly
        public Shape2D IsColliding(string tag)
        {

            foreach (Shape2D b in MainEngine.AllShapes)
            {
                if (b.Tag == tag)
                {
                    if (Position.X < b.Position.X + b.Scale.X && Position.X + Scale.X > b.Position.X && Position.Y < b.Position.Y + b.Scale.Y && Position.Y + Scale.Y > b.Position.Y)
                    {
                        return b;
                    }
                }
            }

            return null;
        }

        public void addWalls(int wallheightL, int wallheightR)
        {
            if (wallheightL > 0)
            {
                Shape2D wallL = new Shape2D(new Vector2(this.Position.X - 2, this.Position.Y + 2), new Vector2(5, wallheightL), Color.Transparent, "vbox");
            }
            if (wallheightR > 0)
            {
                Shape2D wallR = new Shape2D(new Vector2(this.Position.X + this.Scale.X - 3, this.Position.Y + 2), new Vector2(5, wallheightR), Color.Transparent, "vbox");
            }
        }

        // Remove shape from game engine, need to find a way to destroy it completely else it still exists in the code.
        public void DestroyShape()
        {
            Log.Info($"[SHAPE2D]({Tag}) has been destroyed.");
            MainEngine.UnRegisterShape(this);
        }
    }
}
