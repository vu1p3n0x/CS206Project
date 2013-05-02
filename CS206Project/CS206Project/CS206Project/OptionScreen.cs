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
                else
                {
                    // check for card background clicks
                    for (int i = 0; i < 4; i++)
                        if (new Rectangle(85 * i + 45, 195, 82, 110).Contains(state.X, state.Y))
                            game.settings.currentBack = i;
                }
            }

            prevState = state;

            return true;
        }
        public override bool Draw(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            // draw background
            game.spriteBatch.Draw(game.settings.table, game.settings.background, Color.White);

            // draw back button
            game.spriteBatch.Draw(game.settings.pixel, backButton, Color.White);
            game.spriteBatch.DrawString(game.settings.font, "BACK", new Vector2(backButton.X + 10.0f, backButton.Y + 10.0f), Color.Black);

            Card temp;
            for (int i = 0; i < 4; i++)
            {
                temp = new Card(4+i, 5);
                temp.show();
                if (i == game.settings.currentBack)
                    game.spriteBatch.Draw(game.settings.pixel, new Rectangle(85 * i + 45, 195, 82, 110), Color.Green);
                temp.Draw(game, new Rectangle(85 * i + 50, 200, 72, 100));
            }

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
