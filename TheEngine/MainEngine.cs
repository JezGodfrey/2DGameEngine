using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GameEngine1.TheEngine
{
    
    // Canvas class that inherits from Windows Forms
    class Canvas : Form
    {
       
        // If you force refresh a windows form you'll get a lot of flickering, the below solves this issue
        public Canvas()
        {
            DoubleBuffered = true;
        }
    }
    
    public abstract class MainEngine
    {
        // Set to private so they can't be edited outside of game starting up (initialisation)
        private Vector2 ScreenSize = new Vector2(1280, 720);
        private string Title = "New Game";
        private Canvas Window = null;
        public Thread RefreshThread = null;

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        private static List<Sprite2D> AllSprites = new List<Sprite2D>();

        // defaults, overridden by what's defined in TestGame
        public Color Background = Color.Beige;

        // Camera
        public Vector2 CameraPosition = new Vector2(-200, 0);

        // initialising MainEngine and creating a window
        public MainEngine(Vector2 screenSize, string title)
        {
            Log.Info("Game is starting...");
            ScreenSize = screenSize;
            Title = title;

            // Only one window displaying game instance, continously call to redraw entire thing every frame if possible using Canvas
            Window = new Canvas();
            
            // Setting the size of the Window using our X and Y variables from our passed in Vector2 class, casted to integers instead of floats
            Window.Size = new Size((int)screenSize.X, (int)screenSize.Y);
            // Stops window being re-sized
            Window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Window.Text = title;

            Window.Paint += Renderer;

            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;
            Window.MouseClick += Window_Click;

            // This calls the Refresh function as a different thread
            RefreshThread = new Thread(Refresher);
            RefreshThread.Start();

            // Running the custom doublebuffered window
            Application.Run(Window);
            Window.Close();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        private void Window_Click(object sender, MouseEventArgs e)
        {
            GetClick(e);
        }


        // Register new created shapes to the game engine, to AllShapes list
        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }

        // Remove shapes
        public static void UnRegisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }

        public static void RegisterSprite(Sprite2D sprite)
        {
            AllSprites.Add(sprite);
        }

        
        public static void UnRegisterSprite(Sprite2D sprite)
        {
            AllSprites.Remove(sprite);
        }
        

        // Refresher to constantly be updating the game instance
        void Refresher()
        {

            OnLoad(); // Load all assets, sprites and such, before the loop starts

            // While the thread is running it will continue to loop - make sure to abort the thread or else it will keep running when the game is closed and cause memory leaks
            while (RefreshThread.IsAlive)
            {
                // try catch because this loop runs before the window has even started, so while waiting for the application to run the window, just say game loading
                // This is a bad solution, look into how to improve this
                try
                {
                    // Telling Windows to constantly refresh something it doesn't want to refresh. Don't care what thread it's in, call this function regardless of what Windows is doing.
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate(); // Anything regarding movement or physics happens here
                    // Giving Windows some time between refreshes, else it might refresh on top of a refresh and freeze
                    Thread.Sleep(16); // 57 FPS
                }
                catch
                {
                    Log.Error("Game not found.");
                }
                
            }
        }

        public void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // The color used to clear the screen every frame every time it is refreshed
            g.Clear(Background);

            // Sets to camera - done before all other objects are drawn
            g.TranslateTransform(CameraPosition.X, CameraPosition.Y);

            try
            {
                foreach (Sprite2D sprite in AllSprites)
                {
                    g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
                }
            }
            catch
            {
                Log.Info("Loading Sprites");
            }

            foreach (Shape2D shape in AllShapes)
            {
                
                if (shape.Angle != 0)
                {
                    
                    g.RotateTransform(shape.Angle);
                    
                }

                // Draws a shape using the Position and Scale values from Shape2D, X and Y values of each coming from Vector2, then filled in with a solid colour.
                g.FillRectangle(new SolidBrush(shape.ShapeColor), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);

                if (shape.Angle != 0)
                {
                    g.ResetTransform();
                }

            }


        }

        // Creating sprites/game objects, tell the game where they are etc., loading them in before the renderer starts
        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
        public abstract void GetClick(MouseEventArgs e);
    }
}
