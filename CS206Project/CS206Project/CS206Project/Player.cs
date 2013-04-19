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
        private string name;                            // name of the player
        private int maxCards;                           // number of cards face-up needed to win this round
        private List<Card> field;                       // vector to hold all the cards the player has on the table
        private bool validPlays;                        // true if the player can play the card in their hand, false if they must discard or bury
        private Card hand;                              // card the player is currently holding during their turn
        private bool hasDrawn;

        //default constructor

        public Player() { }

        public void setName(string newName) { name = newName; }

        public void setMaxCards(int numCards) { maxCards = numCards; }

        public void addCard(Card theCard) { field.Add(theCard); }           // adds a card to the field, for use in deal function

        public void turn(GameScreen gamescreen)
        {
            validPlays = true;
            if (!hasDrawn)
            {
                drawCard(Mouse.GetState(), gamescreen);
            }
            else if (playCheck())
            {
                playCard(Mouse.GetState());
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

        public void playCard(MouseState clickLocation)
    {
	  for (int i = 1; i <= maxCards; i++)
	  {
		if (clickLocation == FIELD[i])
		{
		  if ((hand.getNumber() == i) || (hand.getNumber() == Game1.JACK))
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

	  return;
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
                    for (int i = 1; i <= maxCards; i++)
                    {
                        if (clickLocation == FIELD[i])
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
            name = game.settings.getPlayerName();
            maxCards = game.settings.getNumCards();
            field.Clear();                              // makes sure there is nothing in field
            field.Add(Card.Blank);                      // adds a blank card to the 0th index so we can start indexing at 1
            for (int i = 1; i <= maxCards; i++)
              field.Add(gamescreen.deck_pop());
            validPlays = true;
            hand = Card.Blank;                          //set hand to blank card
            hasDrawn = false;
            return true;
        }

        public override bool LoadContent(Game1 game)
        {

            return true;
        }

        public override bool Update(Game1 game, GameTime time, GameScreen gamescreen)
        {
            if(gamescreen.currentPlayer == 0)
                turn(gamescreen);
            return true;
        }

        public override bool Draw(Game1 game, GameTime time)
        {
            
            return true;
        }
    }

}
