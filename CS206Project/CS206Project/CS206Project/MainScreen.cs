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
        Rectangle optionsButton;
        Rectangle gameButton;

        MouseState prevState;

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

            prevState = new MouseState();

            return true;
        }
        public override bool LoadContent(Game1 game)
        {
            return true;
        }
        public override bool Update(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            // get mouse input
            MouseState mouseState = Mouse.GetState();

            // check if pressed on button
            if (mouseState.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)
            {
                if (optionsButton.Contains(mouseState.X, mouseState.Y))
                    options_pressed = true;
                else if (gameButton.Contains(mouseState.X, mouseState.Y))
                    game_pressed = true;
            }

            // set next iteration previous value
            prevState = mouseState;

            return true;
        }
        public override bool Draw(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            // draw background
            game.spriteBatch.Draw(game.settings.table, game.settings.background, Color.White);

            // draw options button
            game.spriteBatch.Draw(game.settings.pixel, optionsButton, Color.White);
            game.spriteBatch.DrawString(game.settings.font, "OPTIONS", new Vector2(optionsButton.X + 10.0f, optionsButton.Y + 10.0f), Color.Black);

            // draw game button
            game.spriteBatch.Draw(game.settings.pixel, gameButton, Color.White);
            game.spriteBatch.DrawString(game.settings.font, "START", new Vector2(gameButton.X + 10.0f, gameButton.Y + 10.0f), Color.Black);

            return true;
        }

        public override bool HasNextScreen()
        {
            return options_pressed || game_pressed;
        }
        public override Screen GetNextScreen()
        {
            Screen screen;

            if (options_pressed)
                screen = new OptionScreen();
            else if (game_pressed)
                screen = new GameScreen();
            else
                screen = new ScreenEmpty();

            options_pressed = false;
            game_pressed = false;

            return screen;
        }
    }
}
