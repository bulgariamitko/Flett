using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RPG_TeamFlett.GUI.Screen
{
    public class GameScreen
    {
        protected ContentManager content = null;

        public GameScreen()
        {
            this.Type = this.GetType();
            this.XmlPath = "Content/XmlLoad/" + 
                this.Type.ToString().Replace("RPG_TeamFlett.GUI.Screen.", string.Empty) + ".xml";
        }

        [XmlIgnore]
        public Type Type { get; set; }

        public string XmlPath { get; set; }

        public virtual void LoadContent()
        {
            this.content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            this.content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
