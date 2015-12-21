using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RPG_TeamFlett.Core;

namespace RPG_TeamFlett.GUI.Core
{
    public class ScreenManager
    {
        private static ScreenManager instance;
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        public Vector2 Dimentions { private set; get; }
        public ContentManager Content { private set; get; }

        public GameScreen CurrentScreen { get; set; }

        private ScreenManager()
        {
            Dimentions = new Vector2(640, 480);
            CurrentScreen = new SplashScreen();
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = Content;
            CurrentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            CurrentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
        }
    }
}
