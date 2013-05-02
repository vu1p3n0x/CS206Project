using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CS206Project
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Settings settings;
        public static int JACK = 11;
        public static int QUEEN = 12;
        public static int KING = 13;

        Stack<Screen> screens;

        public Game1()
        {
            // program-wide settings
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            // set graphics
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 780;

            // set content directory
            Content.RootDirectory = "Content";

            // create screen stack
            screens = new Stack<Screen>();
        }

        protected override void Initialize()
        {
            // create settings object
            settings = new Settings();
            settings.Initialize();

            // set main screen on screen stack
            screens.Push(new MainScreen()); 
            screens.Peek().Initialize(this);

            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // have settings and main screen load content
            settings.LoadContent(this);
            screens.Peek().LoadContent(this);

            // TODO: use this.Content to load your game content here
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // if screen requests to be removed, then remove it
            if (!screens.Peek().IsActive())
            {
                // screen wants to be replaced with new screen
                if (screens.Peek().HasNextScreen())
                {
                    Screen newscreen = screens.Peek().GetNextScreen();
                    screens.Pop();
                    screens.Push(newscreen);
                    screens.Peek().Initialize(this);
                    screens.Peek().LoadContent(this);
                }
                // screen simply wants to be removed
                else
                {
                    screens.Pop();
                }
            }
            else
            {
                // screen wishes to add a screen on top of it
                if (screens.Peek().HasNextScreen())
                {
                    Screen newscreen = screens.Peek().GetNextScreen();
                    screens.Push(newscreen);
                    screens.Peek().Initialize(this);
                    screens.Peek().LoadContent(this);
                }
            }

            // exit the program if there are no screens
            if (screens.Count == 0)
                this.Exit();
            else
            {
                // exit the program if something wen wrong during the update
                if (!screens.Peek().Update(this, gameTime))
                    this.Exit();
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            // initialize drawing
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            // exit the program if there are no screens
            if (screens.Count == 0)
                this.Exit();
            else
            {
                // exit the program if something wen wrong during the draw
                if (!screens.Peek().Draw(this, gameTime))
                    this.Exit();
            }

            // stop drawing
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

