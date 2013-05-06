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

                    // check for number of card clicks
                    for (int i = 0; i < 8; i++)
                        if (new Rectangle(45 + 40 * i, 130, 30, 30).Contains(state.X, state.Y))
                        {
                            game.settings.setNumCards(i + 1);
                        }

                    for (int i = 0; i < 4; i++)
                        if (new Rectangle(45 + 40 * i, 340, 30, 30).Contains(state.X, state.Y))
                        {
                            game.settings.setMaxPlayers(i + 1);
                        }
                }
            }

            prevState = state;
            return true;
        }
        public override bool Draw(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            Card temp;
            // draw background
            game.spriteBatch.Draw(game.settings.options_table, game.settings.background, new Color(255, 255, 255, 255));

            // draw back button
            game.spriteBatch.Draw(game.settings.pixel, backButton, Color.White);
            game.spriteBatch.DrawString(game.settings.font, "BACK", new Vector2(backButton.X + 10.0f, backButton.Y + 10.0f), Color.Black);

            // draw max card options
            game.spriteBatch.DrawString(game.settings.font, "Choose the number of cards", new Vector2(50, 100), Color.Black);
            for (int i = 0; i < 8; i++)
            {
                game.spriteBatch.Draw(game.settings.pixel, new Rectangle(45 + 40 * i, 130, 30, 30), Color.Black);
                if (game.settings.getNumCards() == i + 1)
                    game.spriteBatch.Draw(game.settings.pixel, new Rectangle(48 + 40 * i, 133, 24, 24), Color.Green);
                else
                    game.spriteBatch.Draw(game.settings.pixel, new Rectangle(48 + 40 * i, 133, 24, 24), Color.White);
                game.spriteBatch.DrawString(game.settings.font, (i + 1).ToString(), new Vector2(54+40*i, 132), Color.Black);
            }

            // draw card background options
            game.spriteBatch.DrawString(game.settings.font, "Choose a card background", new Vector2(50, 165), Color.Black);
            for (int i = 0; i < 4; i++)
            {
                temp = new Card(4+i, 5);
                temp.show();
                if (i == game.settings.currentBack)
                {
                    game.spriteBatch.Draw(game.settings.pixel, new Rectangle(85 * i + 45, 195, 82, 110), Color.Black);
                    game.spriteBatch.Draw(game.settings.pixel, new Rectangle(85 * i + 48, 198, 76, 104), Color.Green);
                }
                temp.Draw(game, new Rectangle(85 * i + 50, 200, 72, 100));
            }

            // draw max player options
            game.spriteBatch.DrawString(game.settings.font, "Choose the number of players", new Vector2(50, 310), Color.Black);
            for (int i = 0; i < 4; i++)
            {
                game.spriteBatch.Draw(game.settings.pixel, new Rectangle(45 + 40 * i, 340, 30, 30), Color.Black);
                if (game.settings.getMaxPlayers() == i + 1)
                    game.spriteBatch.Draw(game.settings.pixel, new Rectangle(48 + 40 * i, 343, 24, 24), Color.Green);
                else
                    game.spriteBatch.Draw(game.settings.pixel, new Rectangle(48 + 40 * i, 343, 24, 24), Color.White);
                game.spriteBatch.DrawString(game.settings.font, (i + 1).ToString(), new Vector2(54 + 40 * i, 342), Color.Black);
            }
            //printing rules
            game.spriteBatch.DrawString(game.settings.font, "RULES \n1.Obtain Ace through 8 face up\n  on the field.\n2.Jacks are wild.\n3.Draw from discard pile or deck\n  to start.\n4.If the card is a number you need,\n  swap with facedown card\n  in that position,\n  Ace is top left, 8 is bottom right.\n5.Your turn ends when you can't play\n  and must discard a card.\n6.If the player on your left\n  needs the card you have,\n  you can bury it in a\n  facedown location.\n7.When a round ends,\n  each player who won needs 1 less\n  card and the next round begins.\n8.Game ends when a player is\n  at zero cards needed to win. ", new Vector2(380, 40), Color.Black);
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
