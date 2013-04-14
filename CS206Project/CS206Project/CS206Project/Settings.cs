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

        private int getNumCards()
        {
            return numCards;
        }
        
        private int getMaxPlayers()
        {
            return maxPlayers;
        }

        private string getPlayerName()
        {
            return playerName;
        }

        private int getCurrentCardBack()
        {
            return currentCardBack;
        }

        private int getCurrentBack()
        {
            return currentBack;
        }

        private void setNumCards(int n)
        {
            numCards = n;
        }

        private void setMaxPlayers(int n)
        {
            maxPlayers = n;
        }

        private void setPlayerName(string name)
        {
            playerName = name;
        }

        private void setCurrentCardBack(int n)
        {
            currentCardBack = n;
        }

        private void setCurrentBack(int n)
        {
            currentBack = n;
        }
    }
}
