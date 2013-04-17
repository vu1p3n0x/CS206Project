using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CS206Project
{
    abstract class PlayerBase
    {
        // constructors and destructors
        public PlayerBase()
        {

        }
        ~PlayerBase()
        {

        }

        // basic override functions
        public abstract bool Initialize(Game1 game);
        public abstract bool LoadContent(Game1 game);
        public abstract bool Update(Game1 game, GameTime time);
        public abstract bool Draw(Game1 game, GameTime time);
    }
}
