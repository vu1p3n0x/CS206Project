using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CS206Project
{
    class Settings
    {
        int numCards;
        int maxPlayers;
        string playerName;
        Texture2D images;
        Rectangle[,] cardBacks = new Rectangle[5,13];
        int currentCardBack;
        int currentBack;

        public override bool LoadContent(Game1 game)
        {
            images = game.Content.Load<Texture2D>("card_images");
            return true;
        }

        public Settings(){
            numCards = 8;
            maxPlayers = 4;
            playerName = "Bob";
            loadCardBacks();
            loadTableBacks();
            currentCardBack = 1;
            currentBack = 1;
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

        public int getCurrentCardBack()
        {
            return currentCardBack;
        }

        public int getCurrentBack()
        {
            return currentBack;
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

        public void setCurrentCardBack(int n)
        {
            currentCardBack = n;
        }

        public void setCurrentBack(int n)
        {
            currentBack = n;
        }

        private void loadCardBacks()
        {
            Rectangle temp;
            int width = images.Width / 13;
            int height = images.Height / 5;
            int currentFrame = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                }
            }
        }
    }
}
