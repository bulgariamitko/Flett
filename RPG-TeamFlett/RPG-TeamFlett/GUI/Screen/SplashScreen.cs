using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Input;

namespace RPG_TeamFlett.GUI.Screen
{
    public class SplashScreen : GameScreen
    {
        [XmlElement("Image")]
        public Image Image { get; set; }

        // [XmlElement("Path")]
        //public string path = null;
        //public string Path { get; set; }

        public override void LoadContent()
        {
            base.LoadContent();
            //Don't need we get it from XML file
            // this.path = @"Resourses/SplashScreen/Test";
            this.Image.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            this.Image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Image.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && ScreenManager.Instance.IsTransitioning == false)
            {
                ScreenManager.Instance.ChangeScreen("SplashScreen");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Image.Draw(spriteBatch);
        }
    }
}
