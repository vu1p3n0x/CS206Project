using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CS206Project
{
    class Card
    {
        public int number;
        enum suits { Spades, Hearts, Diamonds, Clubs };
        public suits cardSuit;
        public bool visible;

        public Card(int newNumber, suits newSuit)
        {
            number = newNumber;
            cardSuit = newSuit;
        }

    }
}
