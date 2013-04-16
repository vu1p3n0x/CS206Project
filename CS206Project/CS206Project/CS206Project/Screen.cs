using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;

namespace CS206Project
{
    abstract class Screen
    {
        // private members
        private bool m_remove;

        // constructors and destructors
        public Screen()
        {
            m_remove = false;
        }
        ~Screen()
        {

        }

        // basic override functions
        public abstract bool Initialize(Game1 game);
        public abstract bool LoadContent(Game1 game);
        public abstract bool Update(Game1 game, GameTime time);
        public abstract bool Draw(Game1 game, GameTime time);

        // set functions
        protected void Remove()
        {
            m_remove = true;
        }

        // get functions
        public bool IsActive()
        {
            return !m_remove;
        }
        
        private Card[] shuffle(Card[] deck)
        {
            var rand = new Random();
            Card[] tempDeck = new Card[deck.Length];
            Card tempCard = Card.Blank;
            int deckSize = deck.Length;
            bool locationUsed = false;
            for (int i = 0; i < deckSize; i++)
            {
                tempCard = deck[rand.Next(1, deckSize)];
                for (int j = 0; j < tempDeck.Length; j++)
                {
                    if (tempDeck[j] == tempCard)
                    {
                        locationUsed = true;
                    }
                }
                if (!locationUsed)
                    tempDeck[i] = tempCard;
                locationUsed = false;
            }
            return tempDeck;
        }

        public abstract bool HasNextScreen();
        public abstract Screen GetNextScreen();
    }

    class ScreenEmpty : Screen
    {
        public override bool Initialize(Game1 game)
        {
            return true;
        }
        public override bool LoadContent(Game1 game)
        {
            return true;
        }
        public override bool Update(Game1 game, GameTime time)
        {
            this.Remove();
            return true;
        }
        public override bool Draw(Game1 game, GameTime time)
        {
            return true;
        }

        public override bool HasNextScreen()
        {
            return false;
        }
        public override Screen GetNextScreen()
        {
            return this;
        }
    }
}
