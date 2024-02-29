using System;

namespace GameEngine1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            /*
            This program is a showcase of what I have learnt so far in C#. It is a basic 2D game engine, with a demo built in (TestGame).

            TODO        :   Add physics;
                            add multiple collectibles; refactor and tidy code

            TO IMPROVE  :   Improve animations as well as how animations are handled;
                            improve collision detection to better handle corners;
                            A lot of variables declared in TestGame looks messy, move to another file

            */


            // *1 Main calls a new TestGame 'game'.
            TestGame game = new TestGame();
        }
    }
}
