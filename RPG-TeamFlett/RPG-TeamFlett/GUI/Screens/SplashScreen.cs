﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG_TeamFlett.GUI.Core;

namespace RPG_TeamFlett.Core
{
    class SplashScreen : GameScreen
    {
        public Texture2D image;
        public string path;

        public override void LoadContent()
        {
            base.LoadContent();
            this.path = @"Resourses/Screens/Black_background.jpg";
            image = content.Load<Texture2D>(path);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spirteBatch)
        {
            spirteBatch.Draw(image, Vector2.Zero, Color.White);
        }
    }
}
