using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace CS206Project
{
    class Player : PlayerBase
    {
                                    // name of the player
        // moved to playerBase      // number of cards face-up needed to win this round
                                    // vector to hold all the cards the player has on the table
        private bool validPlays;                        // true if the player can play the card in their hand, false if they must discard or bury
        private Card hand;                              // card the player is currently holding during their turn
        private bool hasDrawn;

        //default constructor

        public Player(Game1 game, GameScreen gamescreen, string newName)
        {
            name = string.Copy(newName);
            maxCards = game.settings.getNumCards();
            hand = Card.Blank;
            field = new List<Card>();
        }
          // adds a card to the field, for use in deal function

        public void computerTurn(GameScreen gamescreen)
        {
            validPlays = true;
            if (!hasDrawn)
            {
                drawCard(gamescreen);
            }
            else if (playCheck())
            {
                playCard(gamescreen);
            }
            else if (!playCheck())
            {
                if (discardCard(gamescreen))
                {
                    hasDrawn = false;
                    gamescreen.currentPlayer++;
                    if (gamescreen.currentPlayer == 4)
                        gamescreen.currentPlayer = 0;
                }
            }
        }
        public void turn(GameScreen gamescreen)
        {
            validPlays = true;
            if (!hasDrawn)
            {
                drawCard(Mouse.GetState(), gamescreen);
            }
            else if (playCheck())
            {
                playCard(Mouse.GetState(), gamescreen);
            }
            else if (!playCheck())
            {
                if (discardCard(Mouse.GetState(), gamescreen))
                {
                    hasDrawn = false;
                    gamescreen.currentPlayer = 1;
                }
            }
        }

        public void drawCard(GameScreen gamescreen)
        {
            Card temp;
            temp = gamescreen.discardPile[gamescreen.discardPile.Count - 1];
            if ((temp.getNumber() <= maxCards) || (temp.getNumber() == Game1.JACK))
            {
                if (temp.getNumber() == Game1.JACK)
                {
                    hand = gamescreen.discardPile_pop();
                    hasDrawn = true;
                }
                else
                {
                    if (!field[temp.getNumber() - 1].isVisible())
                    {
                        hand = gamescreen.discardPile_pop();
                        hasDrawn = true;
                    }
                }
            }
            else
            {
                hand = gamescreen.deck_pop();
                hasDrawn = true;
            }
            return;
        }
        public void drawCard(MouseState clickLocation, GameScreen gamescreen)
        {
            if (clickLocation.LeftButton == ButtonState.Pressed)
            {
                if (gamescreen.deck_location.Contains(clickLocation.X, clickLocation.Y))
                {
                    hand = gamescreen.deck_pop();
                    hasDrawn = true;
                }
                else if (gamescreen.discard_location.Contains(clickLocation.X, clickLocation.Y))
                {
                    hand = gamescreen.discardPile_pop();
                    hasDrawn = true;
                }
            }
            return;
        }
        public void playCard(GameScreen gamescreen)
        {
            if (hand.getNumber() == Game1.JACK)
            {
                for (int i = 0; i < maxCards; i++)
                {
                    if (!field[i].isVisible())
                    {
                        Card temp = field[i];
                        field[i] = hand;
                        field[i].show();
                        hand = temp;
                        i = maxCards + 1;
                    }
                }
            }
            else
            {
                Card temp = field[hand.getNumber() - 1];
                field[hand.getNumber() - 1] = hand;
                field[hand.getNumber() - 1].show();
                hand = temp;
            }
        }
        public void playCard(MouseState clickLocation, GameScreen gamescreen)
    {
        if (clickLocation.LeftButton == ButtonState.Pressed)
        {
            for (int i = 0; i < maxCards; i++)
            {
                if (gamescreen.fields[0,i].Contains(clickLocation.X, clickLocation.Y))
                {
                    if ((hand.getNumber() == (i+1)) || (hand.getNumber() == Game1.JACK))
                    {
                        if (!field[i].isVisible())
                        {
                            Card temp = field[i];
                            field[i] = hand;
                            field[i].show();
                            hand = temp;
                            i = maxCards + 1;
                        }
                        else if (field[i].isVisible() && (field[i].getNumber() == Game1.JACK))
                        {
                            Card temp = field[i];
                            field[i] = hand;
                            field[i].show();
                            hand = temp;
                            i = maxCards + 1;
                        }
                    }
                }
            }
        }

	  return;
    }
        public bool discardCard(GameScreen gamescreen)
        {
            int nextPlayer;
            bool hasDiscarded = false;
            if (gamescreen.currentPlayer == 3)
                nextPlayer = 0;
            else
                nextPlayer = gamescreen.currentPlayer + 1;
            if (hand.getNumber() > gamescreen.players[nextPlayer].maxCards)
            {
                gamescreen.discardPile_push(hand);
                hand = Card.Blank;
                hasDiscarded = true;
            }
            else
            {
                if (!gamescreen.players[nextPlayer].field[hand.getNumber()-1].isVisible())
                {
                    for (int i = 0; i < maxCards; i++)
                    {
                        if (!field[i].isVisible())
                        {
                            hasDiscarded = buryCard(i, gamescreen);
                            i = maxCards + 1;
                        }
                    }
                }
                
            }

            return hasDiscarded;
        }
        public bool discardCard(MouseState clickLocation, GameScreen gamescreen)
        {
            bool hasDiscarded = false;

            if (clickLocation.LeftButton == ButtonState.Pressed)
            {

                if (gamescreen.discard_location.Contains(clickLocation.X, clickLocation.Y))
                {
                    gamescreen.discardPile_push(hand);
                    hand = Card.Blank;
                    hasDiscarded = true;
                }
                else
                {
                    for (int i = 0; i < maxCards; i++)
                    {
                        if (gamescreen.fields[0, i].Contains(clickLocation.X, clickLocation.Y))
                        {
                            hasDiscarded = buryCard(i, gamescreen);
                            i = maxCards + 1;
                        }
                    }
                }
            }
            return hasDiscarded;
        }
        public bool buryCard(int i, GameScreen gamescreen)
        {
            if(!field[i].isVisible())
            {
                Card temp = field[i];
                field[i] = hand;
                gamescreen.discardPile_push(temp);
                hand = Card.Blank;
                return true;
            }
            else
                return false;
        }

        public bool playCheck()
        {
            if ((hand.getNumber() > maxCards) && (hand.getNumber() != Game1.JACK))
                validPlays = false;
            else if (hand.getNumber() != Game1.JACK)
            {
                if (field[hand.getNumber()].isVisible() && (field[hand.getNumber()].getNumber() != Game1.JACK))
                    validPlays = false;
            }
            return validPlays;
        }

        public override bool Initialize(Game1 game, GameScreen gamescreen)
        {

            return true;
        }

        public override bool LoadContent(Game1 game)
        {

            return true;
        }

        public override bool Update(Game1 game, GameTime time, GameScreen gamescreen)
        {
            if (gamescreen.currentPlayer == 0)
                turn(gamescreen);
            else
                computerTurn(gamescreen);

            return true;
        }

        public override bool Draw(Game1 game, GameTime time)
        {
            
            return true;
        }
    }

}
