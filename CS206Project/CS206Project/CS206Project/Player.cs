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
        private bool drewOrPlayed;

        //default constructor

        public Player(Game1 game, GameScreen gamescreen, string newName)
        {
            name = string.Copy(newName);
            maxCards = game.settings.getNumCards();
            hand = Card.Blank;
            field = new List<Card>(maxCards);
        }
          // adds a card to the field, for use in deal function

        public void turn(Game1 game, GameScreen gamescreen)
        {
            validPlays = true;
            drewOrPlayed = false;

            if (!hasDrawn)
            {
                drawCard(Mouse.GetState(), gamescreen);
            }
            else if (playCheck())
            {
                playCard(Mouse.GetState(), gamescreen);
            }

            if (!drewOrPlayed)
            {
                if (discardCard(Mouse.GetState(), gamescreen))
                {
                    hasDrawn = false;
                    hasWon = true;
                    for (int i = 0; i < maxCards; i++)
                    {
                        if (!field[i].isVisible())
                            hasWon = false;
                    }
                    if (game.settings.getMaxPlayers() > 1)
                        gamescreen.currentPlayer = 1;
                }
            }
        }

        public void drawCard(MouseState clickLocation, GameScreen gamescreen)
        {
            if ((clickLocation.LeftButton == ButtonState.Pressed) && (previousState.LeftButton != ButtonState.Pressed))
            {
                if (gamescreen.deck_location.Contains(clickLocation.X, clickLocation.Y))
                {
                    hand = gamescreen.deck_pop();
                    hand.show();
                    hasDrawn = true;
                    drewOrPlayed = true;

                }
                else if (gamescreen.discard_location.Contains(clickLocation.X, clickLocation.Y))
                {
                    hand = gamescreen.discardPile_pop();
                    hasDrawn = true;
                    drewOrPlayed = true;
                }
            }
            previousState = clickLocation;
            return;
        }

        public void playCard(MouseState clickLocation, GameScreen gamescreen)
    {
        if ((clickLocation.LeftButton == ButtonState.Pressed) && (previousState.LeftButton != ButtonState.Pressed))
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
                            hand.show();
                            i = maxCards + 1;
                            drewOrPlayed = true;
                        }
                        else if (field[i].isVisible() && (field[i].getNumber() == Game1.JACK))
                        {
                            Card temp = field[i];
                            field[i] = hand;
                            field[i].show();
                            hand = temp;
                            hand.show();
                            i = maxCards + 1;
                            drewOrPlayed = true;
                        }
                        else if (field[i].isVisible() && hand.getNumber() == Game1.JACK)
                        {
                            Card temp = field[i];
                            field[i] = hand;
                            field[i].show();
                            hand = temp;
                            hand.show();
                            i = maxCards + 1;
                            drewOrPlayed = true;
                        }

                    }
                }
            }
        }
        previousState = clickLocation;
	  return;
    }

        public bool discardCard(MouseState clickLocation, GameScreen gamescreen)
        {
            bool hasDiscarded = false;

            if ((clickLocation.LeftButton == ButtonState.Pressed) && (previousState.LeftButton != ButtonState.Pressed))
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
            previousState = clickLocation;
            return hasDiscarded;
        }
        public bool buryCard(int i, GameScreen gamescreen)
        {
            if(!field[i].isVisible())
            {
                Card temp = field[i];
                field[i] = hand;
                field[i].hide();
                gamescreen.discardPile_push(temp);
                hand = Card.Blank;
                return true;
            }
            else
                return false;
        }

        public bool playCheck()
        {
            bool allShowing = true;
            for (int i = 0; i < maxCards; i++)
            {
                if (!field[i].isVisible())
                    allShowing = false;
            }

            if (hand.getNumber() > maxCards && hand.getNumber() != Game1.JACK)
                validPlays = false;
            else
            {
                if (hand.getNumber() == Game1.JACK)
                {
                    for (int i = 0; i < maxCards; i++)
                        if (!field[i].isVisible())
                        {
                            validPlays = true;
                            return validPlays;
                        }
                    validPlays = false;
                }
                else
                {
                    if (!field[hand.getNumber() - 1].isVisible())
                        validPlays = true;
                    else if (field[hand.getNumber() - 1].isVisible() && (field[hand.getNumber() - 1].getNumber() == Game1.JACK) && !allShowing)
                        validPlays = true;
                    else
                        validPlays = false;
                }
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
                turn(game, gamescreen);
            return true;
        }

        public override bool Draw(Game1 game, GameTime time)
        {
            MouseState state = Mouse.GetState();
            if (hand != Card.Blank)
                hand.Draw(game, new Rectangle(state.X - 36, state.Y - 50, 72, 100));
            return true;
        }
    }

}
