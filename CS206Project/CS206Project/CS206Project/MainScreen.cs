using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace CS206Project
{
    class MainScreen : Screen
    {
        Texture2D pixel;

        public override bool Initialize(Game1 game)
        {
            return true;
        }
        public override bool LoadContent(Game1 game)
        {
            pixel = game.Content.Load<Texture2D>("pixel");

            return true;
        }
        public override bool Update(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            throw new NotImplementedException();
        }
        public override bool Draw(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            throw new NotImplementedException();
        }

        public override bool HasNextScreen()
        {
            throw new NotImplementedException();
        }
        public override Screen GetNextScreen()
        {
            throw new NotImplementedException();
        }
    }
}
