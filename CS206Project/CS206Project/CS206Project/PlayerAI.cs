using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS206Project
{
    class PlayerAI : PlayerBase
    {
        private bool validPlays;
        private bool hasDrawn;
        private Card hand;

        // constructor and destructor
        public PlayerAI(Game1 game, GameScreen gameScreen, string Name)
        {
            this.name = Name;
            maxCards = game.settings.getNumCards();
            hand = Card.Blank;
            field = new List<Card>(maxCards);
        }
        ~PlayerAI()
        {

        }

        // basic override functions
        public override bool Initialize(Game1 game, GameScreen gameScreen)
        {
            return true;
        }
        public override bool LoadContent(Game1 game)
        {
            return true;
        }
        public override bool Update(Game1 game, Microsoft.Xna.Framework.GameTime time, GameScreen gamescreen)
        {
            
            
            // Draw a card
            if (!hasDrawn)
            {
                drawCard(gamescreen);
                hasDrawn = true;
            }
            else if (canPlay())
            {
                // continue to play if one is available
                playCard(gamescreen);
            }
            else
            {
                // Discard when there are no more plays
                discardCard(gamescreen);
                hasDrawn = false;
                hasWon = true;

                for (int i = 0; i < maxCards; i++)
                {
                    if (!field[i].isVisible())
                        hasWon = false;
                }
                gamescreen.currentPlayer++;
                if (gamescreen.currentPlayer == 4)
                    gamescreen.currentPlayer = 0;
            }
            System.Threading.Thread.Sleep(250);
            // move to next player

            
            return true;
        }
        public override bool Draw(Game1 game, Microsoft.Xna.Framework.GameTime time)
        {
            return true;
        }

        public void drawCard(GameScreen gamescreen)
        {
            Card temp = gamescreen.discardPile[gamescreen.discardPile.Count - 1];
            if (temp.getNumber() == Game1.JACK || (temp.getNumber() <= maxCards && (!field[temp.getNumber()-1].isVisible() || (field[temp.getNumber()-1].isVisible() && field[temp.getNumber()-1].getNumber() == Game1.JACK))))
            {
                hand = gamescreen.discardPile_pop();
                hasDrawn = true;
            }
            else
            {
                hand = gamescreen.deck_pop();
                hasDrawn = true;
            }

            hand.show();
        }
        public bool canPlay()
        {
            if (hand.getNumber() > maxCards && hand.getNumber() != Game1.JACK)
                return false;
            else
            {
                if (hand.getNumber() == Game1.JACK)
                {
                    for (int i = 0; i < maxCards; i++)
                        if (!field[i].isVisible())
                            return true;
                    return false;
                }
                else
                {
                    if (!field[hand.getNumber() - 1].isVisible())
                        return true;
                    else if (field[hand.getNumber() - 1].isVisible() && (field[hand.getNumber() - 1].getNumber() == Game1.JACK))
                        return true;
                    else

                        return false;
                }
            }
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
                if (!gamescreen.players[nextPlayer].field[hand.getNumber() - 1].isVisible())
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

            if (!hasDiscarded)
            {
                gamescreen.discardPile_push(hand);
                hand = Card.Blank;
            }

            return hasDiscarded;
        }
        public void playCard(GameScreen gamescreen)
        {
            if (hand.getNumber() == Game1.JACK)
            {
                for (int i = 0; i < maxCards; i++)
                    if (!field[i].isVisible())
                    {
                        Card temp = field[i];
                        field[i] = hand;
                        hand = temp;
                        hand.show();
                        return;
                    }
            }
            else
            {
                Card temp = field[hand.getNumber()-1];
                field[hand.getNumber() - 1] = hand;
                hand = temp;
                hand.show();
            }
        }
        public bool buryCard(int i, GameScreen gamescreen)
        {
            if (!field[i].isVisible())
            {
                Card temp = field[i];
                hand.hide();
                field[i] = hand;
                field[i].hide();
                gamescreen.discardPile_push(temp);
                hand = Card.Blank;
                return true;
            }
            else
                return false;
        }
    }
}
