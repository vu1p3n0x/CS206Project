using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace CS206Project
{
    class Settings
    {int numCards;
        int maxPlayers;
        string playerName;
        int currentCardBack;
        int currentBack;

        public Settings(){
            numCards = 8;
            maxPlayers = 4;
            playerName = "Bob";
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
    }
}
