using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CS206Project
{
    class GameScreen : Screen
    {
        public List<Card> deck;
        public List<Card> discardPile;

        Texture2D table;
        Rectangle background;
        Rectangle[] fields;
        Rectangle deck_location;
        Rectangle discard_location;
        Texture2D pixel;
        public List<PlayerBase> players;

        int currentPlayer;
        public void deck_push(Card theCard)
        {
            deck.Add(theCard);
            return;
        }

        public void discardPile_push(Card theCard)
        {
            discardPile.Add(theCard);
            return;
        }

        public Card deck_pop()
        {
            return deck[deck.Count - 1];
        }

        public Card discardPile_pop()
        {
            return discardPile[discardPile.Count - 1];
        }

        public override bool Initialize(Game1 game)
        {
            fields = new Rectangle[4];//current allowed max players
            fields[0] = new Rectangle(150, 350, 200, 100);
            fields[1] = new Rectangle(25, 150, 100, 200);
            fields[2] = new Rectangle(150, 50, 200, 100);
            fields[3] = new Rectangle(375, 150, 100, 200);
            deck_location = new Rectangle(170, 200, 70, 100);
            discard_location = new Rectangle(260, 200, 70, 100);
            background = new Rectangle(0, 0, 500, 500);
            currentPlayer = 0;
            //players.Add(new Player());

            for (int i = 1; i <= 4; i++)
                for (int j = 1; j <= Game1.KING; j++)
                    //deck.Add(new Card(j, i));
                    //deck = shuffle(deck);
                    ; ;
            return true;
        }

        public override bool LoadContent(Game1 game)
        {
            table = game.Content.Load<Texture2D>("Table_top copy");
            pixel = game.Content.Load<Texture2D>("pixel");
            return true;
        }

        public override bool Update(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            //players[currentPlayer].Update(game, time, this);
            return true;
        }

        public override bool Draw(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            game.spriteBatch.Draw(table,background,Color.White);
            game.spriteBatch.Draw(pixel, fields[0], Color.White);
            game.spriteBatch.Draw(pixel, fields[1], Color.White);
            game.spriteBatch.Draw(pixel, fields[2], Color.White);
            game.spriteBatch.Draw(pixel, fields[3], Color.White);
            game.spriteBatch.Draw(pixel, deck_location, Color.White);
            game.spriteBatch.Draw(pixel, discard_location, Color.White);
            return true;
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
