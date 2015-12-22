using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG_TeamFlett.GUI.Core;

namespace RPG_TeamFlett.GUI.Screens
{
    class GameOverScreen : GameScreen
    {
        private Texture2D background;
        public override void LoadContent()
        {
            base.LoadContent();
            background = Content.Load<Texture2D>(@"Resourses/Screens/gameOverScreen.png");
        }

        public override void Draw(SpriteBatch spirteBatch)
        {
            spirteBatch.Draw(background, Vector2.Zero, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                ScreenManager.Instance.CurrentScreen = new MenuScreen();
                ScreenManager.Instance.CurrentScreen.LoadContent();
                return;
            }
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Space))
            {
                ScreenManager.Instance.CurrentScreen = new MenuScreen();
                ScreenManager.Instance.CurrentScreen.LoadContent();
            }
        }
    }
}
