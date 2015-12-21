using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace RPG_TeamFlett.GUI.Core
{
    public abstract class GameScreen
    {
        protected ContentManager Content;

        public virtual void LoadContent()
        {
            Content = ScreenManager.Instance.Content;
        }

        public virtual void UnloadContent()
        {
            Content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spirteBatch)
        {

        }
    }
}
