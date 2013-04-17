using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace CS206Project
{
    class Player
    {
        private string name;                            // name of the player
        private int maxCards;                           // number of cards face-up needed to win this round
        private List<Card> field;                       // vector to hold all the cards the player has on the table
        private bool validPlays;                        // true if the player can play the card in their hand, false if they must discard or bury
        private Card hand;                              // card the player is currently holding during their turn
        
        //default constructor
        public Player()
        {
            name = Settings.getPlayerName();
            maxCards = Settings.getNumCards();
            field.Clear();                              // makes sure there is nothing in field
            field.Add(Card.Blank);                      // adds a blank card to the 0th index so we can start indexing at 1
            //for (int i = 1; i <= maxCards; i++)
              //  field.Add(deck.pop());
            validPlays = true;
            hand = Card.Blank;                          //set hand to blank card
        }

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
                hand = deck.pop();
                hasDrawn = true;
            }
            else if (clickLocation == DISCARD_PILE)
            {
                hand = discardPile.pop();
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
		  if ((hand.getNumber() == i) || (hand.number == JACK))
		  {
		    if (!field[i].isVisible())
		    {
			  card temp = field[i];
			  field[i] = hand;
			  field[i].show();
			  hand = temp;
			  hasPlayed = true;
			  i = maxCards + 1;
		    }
		    else if (field[i].isVisible() && (field[i].number == JACK))
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
                    discardPile.push(hand);
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
            while (field[j].visible)
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
            discardPile.push(temp);
            hand = Card.Blank;
            return true;
        }
        public void playCheck()
        {
            if ((hand.number > maxCards) && (hand.number != JACK))
                validPlays = false;
            else if (hand.number != JACK)
            {
                if (field[hand.number].visible && (field[hand.number].number != JACK))
                    validPlays = false;
            }
            return;
        }
    }

}
