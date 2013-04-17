using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS206Project
{
    class MainScreen : Screen
    {
        Texture2D pixel;
        SpriteFont font;

        Rectangle optionsButton;
        Rectangle gameButton;

        bool options_pressed;
        bool game_pressed;

        public override bool Initialize(Game1 game)
        {
            // set button sizes
            optionsButton = new Rectangle(5, 5, 100, 40);
            gameButton = new Rectangle(110, 5, 100, 40);

            // initialize boolean variables
            options_pressed = false;
            game_pressed = false;

            return true;
        }
        public override bool LoadContent(Game1 game)
        {
            // load in graphics content
            pixel = game.Content.Load<Texture2D>("pixel");
            font = game.Content.Load<SpriteFont>("mainfont");

            return true;
        }
        public override bool Update(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            // get mouse input
            MouseState mouseState = Mouse.GetState();

            // check if pressed on button
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (optionsButton.Contains(mouseState.X, mouseState.Y))
                    options_pressed = true;
                else if (gameButton.Contains(mouseState.X, mouseState.Y))
                    game_pressed = true;
            }

            return true;
        }
        public override bool Draw(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            // draw options button
            game.spriteBatch.Draw(pixel, optionsButton, Color.White);
            game.spriteBatch.DrawString(font, "OPTIONS", new Vector2(optionsButton.X+5.0f, optionsButton.Y+10.0f), Color.Black);

            // draw game button
            game.spriteBatch.Draw(pixel, gameButton, Color.White);
            game.spriteBatch.DrawString(font, "OPTIONS", new Vector2(gameButton.X + 5.0f, gameButton.Y + 10.0f), Color.Black);

            return true;
        }

        public override bool HasNextScreen()
        {
            return options_pressed || game_pressed;
        }
        public override Screen GetNextScreen()
        {
            if (options_pressed)
                // change to OptionsScreen when created
                return new GameScreen();
            else if (game_pressed)
                return new GameScreen();
            else
                return new ScreenEmpty();
        }
    }
}
