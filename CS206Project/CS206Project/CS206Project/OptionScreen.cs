using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS206Project
{
    class OptionScreen : Screen
    {
        Texture2D pixel;
        bool IsDone;

        // constructors and destructors
        public OptionScreen()
        {
            IsDone = false;
        }
        ~OptionScreen()
        {

        }

        // base override functions
        public override bool Initialize(Game1 game)
        {
            // throw new NotImplementedException();

            return true;
        }
        public override bool LoadContent(Game1 game)
        {
            pixel = game.Content.Load<Texture2D>("pixel");

            return true;
        }
        public override bool Update(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            // check for button press 

            return true;
        }
        public override bool Draw(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            // draw back button

            return true;
        }

        public override bool HasNextScreen()
        {
            return IsDone;
        }
        public override Screen GetNextScreen()
        {
            return new ScreenEmpty();
        }
    }
}
