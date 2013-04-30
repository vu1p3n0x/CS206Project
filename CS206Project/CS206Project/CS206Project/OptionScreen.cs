using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS206Project
{
    class OptionScreen : Screen
    {
        Rectangle backButton;
        MouseState prevState;

        // constructors and destructors
        public OptionScreen()
        {

        }
        ~OptionScreen()
        {

        }

        // base override functions
        public override bool Initialize(Game1 game)
        {
            backButton = new Rectangle(5, 5, 100, 40);
            prevState = new MouseState(0, 0, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);

            return true;
        }
        public override bool LoadContent(Game1 game)
        {
            return true;
        }
        public override bool Update(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            MouseState state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)
            {
                if (backButton.Contains(state.X, state.Y))
                    Remove();
            }

            prevState = state;

            return true;
        }
        public override bool Draw(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            // draw back button
            game.spriteBatch.Draw(game.settings.pixel, backButton, Color.White);
            game.spriteBatch.DrawString(game.settings.font, "BACK", new Vector2(backButton.X + 10.0f, backButton.Y + 10.0f), Color.Black);

            return true;
        }

        public override bool HasNextScreen()
        {
            return false;
        }
        public override Screen GetNextScreen()
        {
            return new ScreenEmpty();
        }
    }
}
