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
        
        //default constructor

        public void setName(string newName) { name = newName; }

        public void setMaxCards(int numCards) { maxCards = numCards; }

        public void addCard(Card theCard) { field.Add(theCard); }           // adds a card to the field, for use in deal function

        public void turn()
        {
            validPlays = true;
            while (!drawCard(Mouse.GetState())) { }
            playCheck();
            while (validPlays)
            {
                while (!playCard(Mouse.GetState())) { }
                playCheck();
            }
            discardCard();
        }

        public bool drawCard(MouseState clickLocation)
        {
            bool hasDrawn = false;
            if (clickLocation == DECK)
            {
                hand = GameScreen.deck_pop();
                hasDrawn = true;
            }
            else if (clickLocation == DISCARD_PILE)
            {
                hand = GameScreen.discardPile_pop();
                hasDrawn = true;
            }
            return hasDrawn;
        }

        public bool playCard(MouseState clickLocation)
    {
	  bool hasPlayed = false;

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
			  hasPlayed = true;
			  i = maxCards + 1;
		    }
		    else if (field[i].isVisible() && (field[i].getNumber() == Game1.JACK))
		    {
			  Card temp = field[i];
			  field[i] = hand;
			  field[i].show();
			  hand = temp;
			  hasPlayed = true;
			  i = maxCards + 1;
		    }
		  }
		}
	  }

	  return hasPlayed;
    }

        public void discardCard()
        {
            MouseState clickLocation = new MouseState();
            bool hasDiscarded = false;

            while (!hasDiscarded)
            {
                clickLocation = Mouse.GetState();
                if (clickLocation == DISCARD_PILE)
                {
                    GameScreen.discardPile_push(hand);
                    hand = Card.Blank;
                    hasDiscarded = true;
                }
                else
                {
                    for (int i = 1; i <= maxCards; i++)
                    {
                        if (clickLocation == FIELD[i])
                        {
                            hasDiscarded = buryCard(i);
                            i = maxCards + 1;
                        }
                    }
                }
            }

            return;
        }

        public bool buryCard(int i)
        {
            int j = i;
            MouseState clickLocation;
            while (field[j].isVisible())
            {
                clickLocation = Mouse.GetState();
                for (int k = 1; k <= maxCards; k++)
                {
                    if (clickLocation == FIELD[k])
                    {
                        j = k;
                        k = maxCards + 1;
                    }
                }
            }
            Card temp = field[j];
            field[j] = hand;
            GameScreen.discardPile_push(temp);
            hand = Card.Blank;
            return true;
        }

        public void playCheck()
        {
            if ((hand.getNumber() > maxCards) && (hand.getNumber() != Game1.JACK))
                validPlays = false;
            else if (hand.getNumber() != Game1.JACK)
            {
                if (field[hand.getNumber()].isVisible() && (field[hand.getNumber()].getNumber() != Game1.JACK))
                    validPlays = false;
            }
            return;
        }

        public override bool Initialize(Game1 game)
        {
            name = game.settings.getPlayerName();
            maxCards = game.settings.getNumCards();
            field.Clear();                              // makes sure there is nothing in field
            field.Add(Card.Blank);                      // adds a blank card to the 0th index so we can start indexing at 1
            //for (int i = 1; i <= maxCards; i++)
            //  field.Add(deck.pop());
            validPlays = true;
            hand = Card.Blank;                          //set hand to blank card
            return true;
        }

        public override bool LoadContent(Game1 game)
        {

            return true;
        }

        public override bool Update(Game1 game, GameTime time)
        {
            
            return true;
        }

        public override bool Draw(Game1 game, GameTime time)
        {
            
            return true;
        }
    }

}
