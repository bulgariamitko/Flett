using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG_TeamFlett.GameObjects.Character;
using RPG_TeamFlett.GUI.Core;

namespace RPG_TeamFlett
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D enemy;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = (int) ScreenManager.Instance.Dimentions.X;
            graphics.PreferredBackBufferHeight = (int) ScreenManager.Instance.Dimentions.Y;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Instance.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            ScreenManager.Instance.UnloadContent();
        }

      
        protected override void Update(GameTime gameTime)
        {
            ScreenManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin();
            ScreenManager.Instance.Draw(spriteBatch);

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
