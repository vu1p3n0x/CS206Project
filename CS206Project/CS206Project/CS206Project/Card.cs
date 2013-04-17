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
        int suit; //Spade = 1; Club = 2; Heart = 3; Diamond = 4
        bool visible;
        Texture2D card_images;
        Texture2D front;
        Texture2D back;
        

        public Card(int newNumber, int newSuit)
        {
            number = newNumber;
            suit = newSuit;
            visible = false;//defaults to false
            front = new Texture2D(card_images.GraphicsDevice, card_images.Width / 13, card_images.Height / 5);
        }

        private int getNumber()
        {
            return number;
        }
        private int getSuit()
        {
            return suit;
        }

        private bool isVisible()
        {
            return visible;
        }
        public void show()
        {
            visible = true;
        }

        public static bool operator== (Card lhs, Card rhs)
        {
            if (rhs.number == lhs.number && rhs.suit == lhs.suit)
                return true;
            else
                return false;
        }
        public static bool operator!= (Card lhs, Card rhs)
        {
            return !(lhs == rhs);
        }

        public static readonly Card Blank = new Card(0, 0);
    }
}
