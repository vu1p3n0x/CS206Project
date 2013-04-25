﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CS206Project
{
    class Card
    {
        const int width = 933 / 13;//71.76 roughtly the width of each card
        const int height = 499 / 5;//100 roughly the height of each card
        int number;
        int suit; //Spade = 1; Club = 2; Heart = 3; Diamond = 4
        bool visible;

        public bool Initialize(Game1 game, int newNumber, int newSuit)
        {
            number = newNumber;
            suit = newSuit;
            visible = false;
            return true;
        }
        public void Draw(Game1 game, Rectangle dest, float rotation = 0.0f)
        {
            Rectangle src;
            if (visible)
                src = new Rectangle(game.settings.images.Width / 13 * (number - 1), game.settings.images.Height / 5 * (suit - 1), game.settings.images.Width / 13, game.settings.images.Height / 5);
            else if (number == 0 || suit == 0)
                src = new Rectangle(game.settings.images.Width / 13 * (1), game.settings.images.Height / 5 * (4), game.settings.images.Width / 13, game.settings.images.Height / 5);
            else
                src = new Rectangle(game.settings.images.Width / 13 * (5), game.settings.images.Height / 5 * (4), game.settings.images.Width / 13, game.settings.images.Height / 5);

            game.spriteBatch.Draw(game.settings.images, dest, src, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 1.0f);
        }

        public Card(int newNumber, int newSuit)
        {
            number = newNumber;
            suit = newSuit;
            visible = false;//defaults to false
        }

        public int getNumber()
        {
            return number;
        }
        public int getSuit()
        {
            return suit;
        }

        public bool isVisible()
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
