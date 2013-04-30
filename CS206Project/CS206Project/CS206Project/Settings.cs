using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CS206Project
{
    public class Settings
    {
        // variables
        int numCards;
        int maxPlayers;
        string playerName;

        public Texture2D images;
        public Texture2D table;
        public Texture2D pixel;

        public SpriteFont font;

        // constructor and destructor
        public Settings()
        {
            // set default values
            numCards = 8;
            maxPlayers = 4;
            playerName = "Ladeda";
        }
        ~Settings()
        {

        }

        // basic functions
        public bool Initialize()
        {
            // set default values
            numCards = 8;
            maxPlayers = 4;
            playerName = "N/A";

            return true;
        }
        public bool LoadContent(Game1 game)
        {
            // load images
            images = game.Content.Load<Texture2D>("card_images");
            table = game.Content.Load<Texture2D>("Table_top copy");
            pixel = game.Content.Load<Texture2D>("pixel");

            // load font
            font = game.Content.Load<SpriteFont>("mainfont");

            return true;
        }

        // accessor functions
        public int getNumCards()
        {
            return numCards;
        }
        public int getMaxPlayers()
        {
            return maxPlayers;
        }
        public string getPlayerName()
        {
            return playerName;
        }

        // mutator functions
        public void setNumCards(int n)
        {
            numCards = n;
        }
        public void setMaxPlayers(int n)
        {
            maxPlayers = n;
        }
        public void setPlayerName(string name)
        {
            playerName = name;
        }
    }
}
