using System;
using System.Drawing;

namespace GameEngine1.TheEngine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";
        public string Tag = "";
        public Bitmap Sprite = null;

        public Sprite2D(Vector2 position, Vector2 scale, string directory, string tag)
        {
            Position = position;
            Scale = scale;
            Directory = directory;
            Tag = tag;
            Sprite = new Bitmap(Image.FromFile($"Assets/Sprites/{Directory}.png"));

            Log.Info($"[SPRITE2D]({Tag}) has been created from \"{Directory}\"");

            // Whenever a new shape is created, register it in the game engine
            MainEngine.RegisterSprite(this);

        }


        public void DestroySprite()
        {
            Log.Info($"[SPRITE2D]({Tag}) has been destroyed.");
            MainEngine.UnRegisterSprite(this);
        }
    }
}
