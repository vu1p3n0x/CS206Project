using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace CS206Project
{
    abstract class PlayerBase
    {
        public string name;
        public int maxCards;
        public List<Card> field;
        public bool hasWon = false;
        public bool ULTIMATE_VICTOR = false;
        public MouseState previousState;

        // constructors and destructors
        public PlayerBase()
        {

        }
        ~PlayerBase()
        {

        }

        // basic override functions
        public abstract bool Initialize(Game1 game, GameScreen gameScreen);
        public abstract bool LoadContent(Game1 game);
        public abstract bool Update(Game1 game, GameTime time, GameScreen gamescreen);
        public abstract bool Draw(Game1 game, GameTime time);

        // player functions
        public void setName(string newName)
        { 
            name = newName; 
        }
        public void setMaxCards(int numCards) 
        { 
            maxCards = numCards; 
        }
        public void addCard(Card theCard) 
        { 
            field.Add(theCard); 
        }
    }
}
