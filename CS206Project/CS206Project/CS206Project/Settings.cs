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
        int numCards;
        int maxPlayers;
        string playerName;
        Texture2D images;
        public Rectangle[,] cardBacks = new Rectangle[5,13];
        Texture2D table;
        public bool LoadContent(Game1 game)
        {
            images = game.Content.Load<Texture2D>("card_images");
            table = game.Content.Load<Texture2D>("Table_top copy");
            return true;
        }

        public Settings()
        {
            numCards = 8;
            maxPlayers = 4;
            playerName = "Bob";
            loadCardBacks();
        }

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

        private void loadCardBacks()
        {
            Rectangle temp;
            const int NUM_COLUMNS = 13;
            const int NUM_ROWS = 5;
            int width = images.Width / NUM_COLUMNS;
            int height = images.Height / NUM_ROWS;
            int currentFrame = 0;
            int row;
            int column;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    row = (int)((float)currentFrame / (float) NUM_COLUMNS);
                    column = currentFrame % NUM_COLUMNS;
                    temp = new Rectangle(width * column, height * row, width, height);
                    cardBacks[i, j] = temp;
                    currentFrame++;
                }
            }
        }
    }
}
