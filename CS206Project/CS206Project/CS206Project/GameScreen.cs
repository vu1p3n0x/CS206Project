using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CS206Project
{
    class GameScreen : Screen
    {
        public List<Card> deck = new List<Card>();
        public List<Card> discardPile = new List<Card>();

        public Rectangle[,] fields;
        public Rectangle deck_location;
        public Rectangle discard_location;
        Rectangle ULTIMATE_VICTOR_LOCATION;
        MouseState mousestate;

        public List<PlayerBase> players = new List<PlayerBase>();
        public int currentPlayer;
        bool ULTIMATE_VICTOR_DETERMINED = false;

        public override bool Initialize(Game1 game)
        {
            fields = new Rectangle[4,8];//current allowed max players
            fields[0, 0] = new Rectangle(250, 375, 70, 100);
            fields[0, 1] = new Rectangle(325, 375, 70, 100);
            fields[0, 2] = new Rectangle(400, 375, 70, 100);
            fields[0, 3] = new Rectangle(475, 375, 70, 100);
            fields[0, 4] = new Rectangle(250, 480, 70, 100);
            fields[0, 5] = new Rectangle(325, 480, 70, 100);
            fields[0, 6] = new Rectangle(400, 480, 70, 100);
            fields[0, 7] = new Rectangle(475, 480, 70, 100);

            fields[1, 0] = new Rectangle(225, 150, 70, 100);
            fields[1, 1] = new Rectangle(225, 225, 70, 100);
            fields[1, 2] = new Rectangle(225, 300, 70, 100);
            fields[1, 3] = new Rectangle(225, 375, 70, 100);
            fields[1, 4] = new Rectangle(120, 150, 70, 100);
            fields[1, 5] = new Rectangle(120, 225, 70, 100);
            fields[1, 6] = new Rectangle(120, 300, 70, 100);
            fields[1, 7] = new Rectangle(120, 375, 70, 100);

            fields[2, 0] = new Rectangle(250, 20, 70, 100);
            fields[2, 1] = new Rectangle(325, 20, 70, 100);
            fields[2, 2] = new Rectangle(400, 20, 70, 100);
            fields[2, 3] = new Rectangle(475, 20, 70, 100);
            fields[2, 4] = new Rectangle(250, 125, 70, 100);
            fields[2, 5] = new Rectangle(325, 125, 70, 100);
            fields[2, 6] = new Rectangle(400, 125, 70, 100);
            fields[2, 7] = new Rectangle(475, 125, 70, 100);

            fields[3, 0] = new Rectangle(655, 150, 70, 100);
            fields[3, 1] = new Rectangle(655, 225, 70, 100);
            fields[3, 2] = new Rectangle(655, 300, 70, 100);
            fields[3, 3] = new Rectangle(655, 375, 70, 100);
            fields[3, 4] = new Rectangle(760, 150, 70, 100);
            fields[3, 5] = new Rectangle(760, 225, 70, 100);
            fields[3, 6] = new Rectangle(760, 300, 70, 100);
            fields[3, 7] = new Rectangle(760, 375, 70, 100);
            ULTIMATE_VICTOR_LOCATION = new Rectangle(120, 100, 540, 400);


            deck_location = new Rectangle(325, 250, 70, 100);
            discard_location = new Rectangle(400, 250, 70, 100);

            players.Add(new Player(game, this, game.settings.getPlayerName()));
            players.Add(new PlayerAI(game, this, "Bob"));
            players.Add(new PlayerAI(game, this, "Tom"));
            players.Add(new PlayerAI(game, this, "Jerry"));

            InitializeGame(game);
            return true;
        }
        public override bool LoadContent(Game1 game)
        {
            return true;
        }
        public override bool Update(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            if (ULTIMATE_VICTOR_DETERMINED == false)
            {
                if (players[currentPlayer].hasWon)
                {
                    if (players[currentPlayer].maxCards == 1)
                    {
                        players[currentPlayer].ULTIMATE_VICTOR = true;
                        ULTIMATE_VICTOR_DETERMINED = true;
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        if (players[k].hasWon)
                            players[k].maxCards--;
                        players[k].hasWon = false;
                        if (players[k].maxCards == 0 && !ULTIMATE_VICTOR_DETERMINED)
                        {
                            players[k].ULTIMATE_VICTOR = true;
                            ULTIMATE_VICTOR_DETERMINED = true;
                        }
                    }
                    InitializeGame(game);
                }
                else
                    players[currentPlayer].Update(game, time, this);
            }
            else
            {
                mousestate = Mouse.GetState();
                if (mousestate.LeftButton == ButtonState.Pressed)
                    this.Remove();
            }
            return true;
        }
        public override bool Draw(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            //Draws background
            game.spriteBatch.Draw(game.settings.table, game.settings.background, Color.White);

            for (int j = 0; j < game.settings.getMaxPlayers(); j++)
                for (int i = 0; i < players[j].maxCards; i++)
                    if (j == 1 || j == 3)
                        players[j].field[i].Draw(game, fields[j, i], 1.57f);
                    else
                        players[j].field[i].Draw(game, fields[j, i]);

            // draw deck pile
            deck[0].Draw(game, deck_location);

            // draw discard pile
            if (discardPile.Count > 0)
                discardPile[discardPile.Count-1].Draw(game, discard_location);
            else
                Card.Blank.Draw(game, discard_location);

            players[currentPlayer].Draw(game, time);

            for(int k = 0; k < 4; k++)
                if(players[k].ULTIMATE_VICTOR)
                {
                    ULTIMATE_VICTOR_DETERMINED = true;
                    game.spriteBatch.DrawString(game.settings.font, players[k].name + "\nWINS", new Vector2(ULTIMATE_VICTOR_LOCATION.X, ULTIMATE_VICTOR_LOCATION.Y), Color.Black, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
                    k = 4;
                }

            return true;
        }

        public override bool HasNextScreen()
        {
            return false;
        }
        public override Screen GetNextScreen()
        {
            return new ScreenEmpty();
        }
        public void deal(Game1 game)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < game.settings.getMaxPlayers(); j++)
                {
                    if (i < players[j].maxCards)
                        players[j].field.Add(deck_pop());
                }
            }

            discardPile_push(deck_pop());
        }
        private List<Card> shuffle(List<Card> deck)
        {
            var rand = new Random();
            List<Card> tempDeck = new List<Card>();
            Card tempCard = Card.Blank;
            int deckSize = deck.Count;
            int i = 1;//counter
            bool locationUsed = false;
            tempCard = deck[rand.Next(0, deckSize)];
            tempDeck.Add(tempCard);
            while (i < deckSize)
            {
                tempCard = deck[rand.Next(0, deckSize)];
                for (int j = 0; j < tempDeck.Count; j++)
                {
                    if (tempDeck[j] == tempCard)
                    {
                        locationUsed = true;
                    }
                }
                if (!locationUsed)
                {
                    tempCard.hide();
                    tempDeck.Add(tempCard);
                    i++;
                }
                locationUsed = false;
            }
            return tempDeck;
        }
        public void deck_push(Card theCard)
        {
            deck.Add(theCard);
            return;
        }
        public void discardPile_push(Card theCard)
        {
            theCard.show();
            discardPile.Add(theCard);
            return;
        }
        public Card deck_pop()
        {
            Card temp = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);

            if (deck.Count == 0)
            {
                for (int i = 0; i < discardPile.Count; i++)
                {
                    discardPile[i].hide();
                }
                deck = shuffle(discardPile);
                discardPile.Clear();
            }
            return temp;
        }
        public Card discardPile_pop()
        {
            Card temp = discardPile[discardPile.Count - 1];
            discardPile.RemoveAt(discardPile.Count - 1);
            return temp;
        }

        void InitializeGame(Game1 game)
        {
            currentPlayer = 0;
            for (int k = 0; k < 4; k++)
                players[k].field.Clear();
            deck.Clear();
            discardPile.Clear();
            for (int i = 1; i <= 4; i++)
                for (int j = 1; j <= Game1.KING; j++)
                    deck.Add(new Card(j, i));
            deck = shuffle(deck);
            deal(game);
            return;
        }
    }
}
