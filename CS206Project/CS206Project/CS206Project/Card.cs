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
        int number;
        int suit;//Spade = 1; Club = 2; Heart = 3; Diamond = 4
        bool visible;

        public Card(int newNumber, int newSuit)
        {
            number = newNumber;
            suit = newSuit;
            visible = false;//defaults to false
        }

        private int getNumber()
        {
            return number;
        }
        private int getSuit()
        {
            return suit;
        }
        private bool getVisible()
        {
            return visible;
        }

        public static bool operator== (Card lhs, Card rhs)
        {
            if (rhs.number == lhs.number && rhs.suit == lhs.suit)
                return true;
            else
                return false;
        }

        public static Card Blank()
        {
            return new Card(0, 0);
        }
    }
}
