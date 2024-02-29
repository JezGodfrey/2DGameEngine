using System;
using System.Collections.Generic;
using GameEngine1.TheEngine;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GameEngine1
{
    
    // Demo of the game that inherits from the MainEngine class, including all abstract functions
    class TestGame : MainEngine
    {

        // Animation functions - need to improve how animations are handled generally in its own class
        public void AnimatePlayer(int start, int end)
        {

            if (frame == 1 || frame == 11)
            {
                animationSpeed = 160;
            }
            else
            {
                animationSpeed = 8;
            }


            playerSprites[frame].Scale.X = 0;
            playerSprites[frame].Scale.Y = 0;

            animationFrame++;

            if (animationFrame >= animationSpeed)
            {
                frame++;
                animationFrame = 0;
            }

            if (frame > end || frame < start)
            {
                frame = start;
            }

            playerSprites[frame].Scale.X = 80;
            playerSprites[frame].Scale.Y = 80;

        }

        public void AnimateCoin(int start, int end)
        {

            coinSprites[fc].Scale.X = 0;
            coinSprites[fc].Scale.Y = 0;

            aFc++;

            if (aFc >= aSc)
            {
                fc++;
                aFc = 0;
            }

            if (fc > end || fc < start)
            {
                fc = start;
            }

            coinSprites[fc].Scale.X = 80;
            coinSprites[fc].Scale.Y = 80;

        }

        // Declaring sprite and shape variables
        Sprite2D player;
        Sprite2D playerIdleL1;
        Sprite2D playerIdleL2;
        Sprite2D playerIdleR1;
        Sprite2D playerIdleR2;
        Sprite2D playerIdleR3;
        Sprite2D playerJump1;
        Sprite2D playerJump2;
        Sprite2D playerJump3;
        Sprite2D playerJump4;
        Sprite2D playerJumpR1;
        Sprite2D playerJumpR2;
        Sprite2D playerJumpR3;
        Sprite2D playerJumpR4;
        List<Sprite2D> playerSprites;

        Sprite2D coin;
        Sprite2D coin2;
        Sprite2D coin3;
        Sprite2D coin4;
        List<Sprite2D> coinSprites;

        Sprite2D bg;
        Sprite2D fg;

        // Walls for collision
        Shape2D hcolbox;
        Shape2D hcolbox2;
        Shape2D hcolbox3;
        Shape2D hcolbox4;
        Shape2D hcolbox5;
        Shape2D hcolbox6;
        Shape2D hcolbox7;
        Shape2D hcolbox8;
        Shape2D hcolbox9;
        Shape2D hcolbox10;
        Shape2D hcolbox11;
        Shape2D hcolbox12;
        Shape2D hcolbox13;
        Shape2D hcolbox14;
        Shape2D hcolbox15;
        Shape2D hcolbox16;
        Shape2D hcolbox17;
        Shape2D hcolbox18;
        Shape2D hcolbox19;
        Shape2D hcolbox20;
        Shape2D hcolbox21;
        Shape2D hcolbox22;

        Shape2D vcolbox;

        Shape2D playerbox;
        Shape2D coinbox;

        // Storing last vector positions of player position and camera position for use with wall collision
        Vector2 lastPos = Vector2.Zero();
        Vector2 lastCam = Vector2.Zero();

        // Shape colors
        Color Transparent = Color.Transparent;
        Color Red = Color.Red;
        Color Blue = Color.Blue;
        Color Green = Color.Green;
        Color Yellow = Color.Yellow;

        // player/camera speed when moving
        float move = 2f;

        // Variables for animation - frame counter and speed to move to next frame
        int frame = 0;
        int animationFrame = 0;
        int animationSpeed = 8;

        // Coin animation - find better way to handle animation
        int fc = 0;
        int aFc = 0;
        int aSc = 8;

        // Checks for key presses
        bool left;
        bool right;
        bool up;
        bool down;
        bool rtrn;

        // Check whether player is facing left or right
        bool positiveX;

        // Debug code
        bool bKey;
        bool uKey;
        bool gKey;

        // TestGame instantiates the base of the engine, setting parameters and then runs the Application
        public TestGame() : base(new Vector2(1280,720), "Engine Demo")
        {
            
        }

        public override void OnLoad()
        {
            Background = Color.Aqua;
            

            // Loading in sprites - Whatever is drawn first will be on the bottom layer
            bg = new Sprite2D(new Vector2(200, -300), new Vector2(2000, 1062), "YIbg", "testbg");
            
            player = new Sprite2D(new Vector2(300, 500), new Vector2(80, 80),"Frog/tile008","player");
            playerIdleL1 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/Idle/tile001", "playeridleL1");
            playerIdleL2 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/Idle/tile002", "playeridleL2");
            playerIdleR1 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/Idle/tile003", "playeridleR1");
            playerIdleR2 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/Idle/tile004", "playeridleR2");
            playerIdleR3 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/Idle/tile005", "playeridleR3");
            playerJump1 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/tile003", "playerjump1");
            playerJump2 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/tile004", "playerjump2");
            playerJump3 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/tile006", "playerjump3");
            playerJump4 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/tile005", "playerjump4");
            playerJumpR1 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/tile010", "playerjump1");
            playerJumpR2 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/tile011", "playerjump2");
            playerJumpR3 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/tile013", "playerjump3");
            playerJumpR4 = new Sprite2D(player.Position, new Vector2(0, 0), "Frog/tile012", "playerjump4");

            coin = new Sprite2D(new Vector2(300, 200), new Vector2(40, 40), "Coin01", "coin1");
            coin2 = new Sprite2D(coin.Position, new Vector2(0, 0), "Coin02", "coin2");
            coin3 = new Sprite2D(coin.Position, new Vector2(0, 0), "Coin03", "coin3");
            coin4 = new Sprite2D(coin.Position, new Vector2(0, 0), "Coin04", "coin4");

            fg = new Sprite2D(new Vector2(200, -300), new Vector2(2000, 1062), "YIfg", "testfg");

            // Loading shapes - used for hitboxes/collision
            playerbox = new Shape2D(new Vector2(300, 500), new Vector2(30, 20), Transparent, "playerbox");
            coinbox = new Shape2D(new Vector2 (coin.Position.X + 15, coin.Position.Y + 15), new Vector2(50, 50), Transparent, "cbox");
            
            // Adding walls
            hcolbox = new Shape2D(new Vector2(200, 596), new Vector2(530, 5), Transparent, "hbox");
            hcolbox2 = new Shape2D(new Vector2(483, 567), new Vector2(40, 5), Transparent, "hbox");
            hcolbox3 = new Shape2D(new Vector2(713, 552), new Vector2(82, 5), Transparent, "hbox");
            hcolbox4 = new Shape2D(new Vector2(790, 572), new Vector2(200, 5), Transparent, "hbox");
            hcolbox5 = new Shape2D(new Vector2(826, 493), new Vector2(40, 5), Transparent, "hbox");
            hcolbox6 = new Shape2D(new Vector2(888, 493), new Vector2(40, 5), Transparent, "hbox");
            hcolbox7 = new Shape2D(new Vector2(973, 490), new Vector2(100, 5), Transparent, "hbox");
            hcolbox8 = new Shape2D(new Vector2(1140, 410), new Vector2(149, 5), Transparent, "hbox");
            hcolbox9 = new Shape2D(new Vector2(1074, 524), new Vector2(111, 5), Transparent, "hbox");
            hcolbox10 = new Shape2D(new Vector2(1180, 411), new Vector2(82, 5), Transparent, "hbox");
            hcolbox11 = new Shape2D(new Vector2(1140, 450), new Vector2(147, 5), Transparent, "hbox");
            hcolbox12 = new Shape2D(new Vector2(1474, 348), new Vector2(277, 5), Transparent, "hbox");
            hcolbox13 = new Shape2D(new Vector2(1326, 512), new Vector2(40, 5), Transparent, "hbox");
            hcolbox14 = new Shape2D(new Vector2(1388, 512), new Vector2(40, 5), Transparent, "hbox");
            hcolbox15 = new Shape2D(new Vector2(1260, 590), new Vector2(256, 5), Transparent, "hbox");
            hcolbox16 = new Shape2D(new Vector2(1680, 200), new Vector2(239, 5), Transparent, "hbox");
            hcolbox17 = new Shape2D(new Vector2(1868, -170), new Vector2(330, 5), Transparent, "hbox");
            hcolbox18 = new Shape2D(new Vector2(1474, 328), new Vector2(42, 5), Transparent, "hbox");
            hcolbox19 = new Shape2D(new Vector2(1474, 328), new Vector2(130, 5), Transparent, "hbox");
            hcolbox20 = new Shape2D(new Vector2(1680, 247), new Vector2(64, 5), Transparent, "hbox");
            hcolbox21 = new Shape2D(new Vector2(1868, -134), new Vector2(46, 5), Transparent, "hbox");
            hcolbox22 = new Shape2D(new Vector2(200, -1000), new Vector2(2020, 20), Transparent, "hbox");

            vcolbox = new Shape2D(new Vector2(1516, 330), new Vector2(5, 260), Transparent, "vbox");
            hcolbox2.addWalls(29, 29);
            hcolbox3.addWalls(43, 23);
            hcolbox7.addWalls(84, 37);
            hcolbox8.addWalls(40, 40);
            hcolbox10.addWalls(120, 190);
            hcolbox16.addWalls(47, 0);
            hcolbox17.addWalls(36, 0);
            hcolbox19.addWalls(20, 20);
            hcolbox20.addWalls(0, 100);
            hcolbox21.addWalls(0, 330);
            hcolbox22.addWalls(1600, 1600);

            // List of loaded sprites for animation
            playerSprites = new List<Sprite2D> { player, player, playerIdleL1, playerIdleL2, playerIdleL1, playerIdleL2, playerIdleL1, playerJump1, playerJump2, playerJump3, playerJump4,
            playerIdleR1, playerIdleR2, playerIdleR3, playerIdleR2, playerIdleR3, playerIdleR2, playerJumpR1, playerJumpR2, playerJumpR3, playerJumpR4 };

            coinSprites = new List<Sprite2D> { coin, coin2, coin3, coin4 };

        }
        
        public override void OnUpdate()
        {

            
            // Checking for debug code - if successful, shows all shapes
            if (bKey && uKey && gKey)
            {
                foreach (Shape2D shp in AllShapes)
                {
                    if (shp.ShapeColor != Transparent)
                    {
                        shp.ShapeColor = Transparent;
                    } else
                    {

                        switch (shp.Tag)
                        {
                            case "hbox":
                                shp.ShapeColor = Red;
                                break;
                            case "vbox":
                                shp.ShapeColor = Blue;
                                break;
                            case "playerbox":
                                shp.ShapeColor = Green;
                                break;
                            case "cbox":
                                shp.ShapeColor = Yellow;
                                break;

                        }
                        
                    }
                }
                // Give player time to let go of the keys
                Thread.Sleep(300);
            }

            // Console.WriteLine(player.Position.X);
            // Console.WriteLine(player.Position.Y);

            // Tying the the player's hitbox to the player's position
            playerbox.Position.X = player.Position.X + 24;
            playerbox.Position.Y = player.Position.Y + 58;

            AnimateCoin(0, 3);


            if (playerbox.IsColliding("cbox") != null)
            {
                coin.DestroySprite();
                coin2.DestroySprite();
                coin3.DestroySprite();
                coin4.DestroySprite();
            }

            // Very basic physics for colliding with walls - restricting X-axis/Y-axis movement accordingly
            if (playerbox.IsColliding("hbox") != null)
            {
                player.Position.Y = lastPos.Y;
                CameraPosition.Y = lastCam.Y;
            } else
            {
                lastPos.Y = player.Position.Y;
                lastCam.Y = CameraPosition.Y;
            }

            if (playerbox.IsColliding("vbox") != null)
            {
                player.Position.X = lastPos.X;
                CameraPosition.X = lastCam.X;
            } else
            {
                lastPos.X = player.Position.X;
                lastCam.X = CameraPosition.X;
            }

            
            // If no directions are being pressed, the player has an idle animation
            if (!left && !right && !up && !down)
            {
                playerSprites[frame].Scale.X = 0;
                playerSprites[frame].Scale.Y = 0;
                if (positiveX)
                {
                    AnimatePlayer(11, 16);
                } else
                {
                    AnimatePlayer(1, 6);
                }                
                
            } else
            {
                player.Scale.X = 80;
                player.Scale.Y = 80;
                playerSprites[1].Scale.X = 0;
                playerSprites[1].Scale.Y = 0;
                playerSprites[2].Scale.X = 0;
                playerSprites[2].Scale.Y = 0;
                playerSprites[3].Scale.X = 0;
                playerSprites[3].Scale.Y = 0;
            }

            if (left)
            {

                positiveX = false;
                player.Position.X -= move;
                if (player.Position.X >= 780 && player.Position.X <= 1514)
                {
                    CameraPosition.X += move;
                }
                AnimatePlayer(7, 10);

            }

            if (right)
            {


                positiveX = true;
                player.Position.X += move;
                if (player.Position.X >= 780 && player.Position.X <= 1514)
                {
                    CameraPosition.X -= move;
                }
                AnimatePlayer(17, 20);
            }

            if (up)
            {
                player.Position.Y -= move;
                if (player.Position.Y >= -500 && player.Position.Y <= 300)
                {
                    CameraPosition.Y += move;
                }
                AnimatePlayer(1, 6);
            }

            if (down)
            {
                player.Position.Y += move;
                if (player.Position.Y >= -500 && player.Position.Y <= 300)
                {
                    CameraPosition.Y -= move;
                }
                AnimatePlayer(1, 6);
            }

            if (rtrn)
            {
                // Abort refresh thread and close the application
                Application.Exit();
                RefreshThread.Abort();
                Environment.Exit(0);
            }



        }

        // If a key is pressed then the according bool is true. When the key is released, bool is false.
        public override void GetKeyDown(KeyEventArgs e)
        {
            
            // Shortened to one line just for tidy's sake
            
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { left = true; }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { right = true; }
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { up = true; }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { down = true; }

            if (e.KeyCode == Keys.B) { bKey = true; }
            if (e.KeyCode == Keys.U) { uKey = true; }
            if (e.KeyCode == Keys.G) { gKey = true; }

            if (e.KeyCode == Keys.Enter) { rtrn = true; }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { right = false; }
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { up = false; }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { down = false; }

            if (e.KeyCode == Keys.B) { bKey = false; }
            if (e.KeyCode == Keys.U) { uKey = false; }
            if (e.KeyCode == Keys.G) { gKey = false; }

            if (e.KeyCode == Keys.Enter) { rtrn = false; }
        }

        public override void GetClick(MouseEventArgs e)
        {
            Console.WriteLine(e.Location);

            
        }
    }
}
