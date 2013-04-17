using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace CS206Project
{
    class GameScreen : Screen
    {
        public override bool Initialize(Game1 game)
        {
            throw new NotImplementedException();
        }
        public override bool LoadContent(Game1 game)
        {
            return true;
        }
        public override bool Update(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            throw new NotImplementedException();
        }
        public override bool Draw(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            throw new NotImplementedException();
        }

        public override bool HasNextScreen()
        {
            throw new NotImplementedException();
        }
        public override Screen GetNextScreen()
        {
            throw new NotImplementedException();
        }
        private List<Card> shuffle(List<Card> deck)
        {
            var rand = new Random();
            List<Card> tempDeck = new List<Card>();
            Card tempCard = Card.Blank;
            int deckSize = deck.Count;
            int i = 0;//counter
            bool locationUsed = false;
            while (i < deckSize)
            {
                tempCard = deck[rand.Next(1, deckSize)];
                for (int j = 0; j < tempDeck.Count; j++)
                {
                    if (tempDeck[j] == tempCard)
                    {
                        locationUsed = true;
                    }
                }
                if (!locationUsed)
                {
                    tempDeck.Add(tempCard);
                    i++;
                }
                locationUsed = false;
            }
            return tempDeck;
        }
    }
}
