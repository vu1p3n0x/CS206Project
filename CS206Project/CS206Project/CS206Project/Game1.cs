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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        Stack<Screen> screens;
        Texture2D whatever;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screens = new Stack<Screen>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // screens.Push(MainScreen); 
            // screens.Peek().Initialize(this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            whatever = Content.Load<Texture2D>("card_images.png");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
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
                    screens.Push(screens.Peek().GetNextScreen());
                }
            }

            // exit the program if there are no screens
            if (screens.Count == 0)
                this.Exit();
            else
            {
                // exit the program if something wen wrong during the update
                if (screens.Peek().Update(this, gameTime))
                    this.Exit();
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            // exit the program if there are no screens
            if (screens.Count == 0)
                this.Exit();
            else
            {
                // exit the program if something wen wrong during the draw
                if (screens.Peek().Draw(this, gameTime))
                    this.Exit();
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
