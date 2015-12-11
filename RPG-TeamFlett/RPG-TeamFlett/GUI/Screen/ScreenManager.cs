using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;

namespace RPG_TeamFlett.GUI.Screen
{
    public class ScreenManager
    {
        private static ScreenManager instance = null;

        private GameScreen currentScreen = null;
        private GameScreen newGameScreen = null;
        private XmlManager<GameScreen> xmlGameScreenManager = null;

        private ScreenManager()
        {
            this.Demensions = new Vector2(800, 480);
            this.currentScreen = new SplashScreen();
            this.xmlGameScreenManager = new XmlManager<GameScreen>();
            this.xmlGameScreenManager.Type = currentScreen.Type;
            this.currentScreen = this.xmlGameScreenManager.Load("Content/XmlLoad/SplashScreen.xml");
        }

        [XmlIgnore]
        public static ScreenManager Instance
        {
            get
            {
                if (ScreenManager.instance == null)
                {
                    XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                    ScreenManager.instance = xml.Load("Content/XmlLoad/ScreenManager.xml");
                    ScreenManager.instance = new ScreenManager();
                }
                return ScreenManager.instance;
            }
        }

        public Image Image { get; set; }

        [XmlIgnore]
        public GraphicsDevice GraphicDevice { get; set; }

        [XmlIgnore]
        public SpriteBatch SpriteBatch { get; set; }

        [XmlIgnore]
        public Vector2 Demensions { get; private set; }

        [XmlIgnore]
        public ContentManager Content { get; private set; }

        [XmlIgnore]
        public bool IsTransitioning { get; private set; }

        public void ChangeScreen(string screenName)
        {
            this.newGameScreen = (GameScreen)Activator.CreateInstance(
                Type.GetType("RPG_TeamFlett.GUI.Screen." + screenName));
            this.Image.IsActive = true;
            this.Image.FadeEffect.Incrase = true;
            this.Image.Alpha = 0.0f;
            this.IsTransitioning = true;
        }

        public void Transition(GameTime gameTime)
        {
            if (this.IsTransitioning == true)
            {
                this.Image.Update(gameTime);
                if (this.Image.Alpha == 1.0f)
                {
                    this.currentScreen.UnloadContent();
                    this.currentScreen = this.newGameScreen;
                    this.xmlGameScreenManager.Type = this.currentScreen.Type;
                    if (File.Exists(this.currentScreen.XmlPath))
                    {
                        this.currentScreen = this.xmlGameScreenManager.Load(this.currentScreen.XmlPath);
                    }
                    this.currentScreen.LoadContent();
                }
                else if (this.Image.Alpha == 0.0f)
                {
                    this.Image.IsActive = false;
                    this.IsTransitioning = false;
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content");
            this.currentScreen.LoadContent();
            this.Image.LoadContent();
        }

        public void UnloadContent()
        {
            this.currentScreen.UnloadContent();
            this.Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            this.currentScreen.Update(gameTime);
            this.Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.currentScreen.Draw(spriteBatch);
            if (this.IsTransitioning == true)
            {
                this.Image.Draw(spriteBatch);
            }
        }

    }
}
