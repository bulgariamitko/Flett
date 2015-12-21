using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG_TeamFlett.GUI.Core;
using RPG_TeamFlett.GUI.Screens;

namespace RPG_TeamFlett.Core
{
    class SplashScreen : GameScreen
    {
        public Texture2D image;
        public string path;
        private long splashScreenTimer = 0;

        public override void LoadContent()
        {
            base.LoadContent();
            this.path = @"Resourses/Screens/SplashScreen.jpg";
            image = Content.Load<Texture2D>(path);
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Space))
            {
                ScreenManager.Instance.CurrentScreen = new MenuScreen();
                ScreenManager.Instance.CurrentScreen.LoadContent();
            }
        }

        public override void Draw(SpriteBatch spirteBatch)
        {
            spirteBatch.Draw(image, Vector2.Zero, Color.White);
        }
    }
}
