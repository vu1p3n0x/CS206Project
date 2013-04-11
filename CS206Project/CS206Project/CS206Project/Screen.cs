using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;

namespace CS206Project
{
    abstract class Screen
    {
        // private members
        private bool m_remove;

        // constructors and destructors
        public Screen()
        {
            m_remove = false;
        }
        ~Screen()
        {

        }

        // basic override functions
        public abstract bool Initialize(Game1 game);
        public abstract bool Update(Game1 game, GameTime time);
        public abstract bool Draw(Game1 game, GameTime time);

        // set functions
        protected void Remove()
        {
            m_remove = true;
        }

        // get functions
        public bool IsActive()
        {
            return !m_remove;
        }
        public abstract bool HasNextScreen();
        public abstract Screen GetNextScreen();
    }
}
