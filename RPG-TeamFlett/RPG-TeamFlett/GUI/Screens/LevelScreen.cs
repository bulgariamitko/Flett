using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RPG_TeamFlett.GameObjects.Character;
using RPG_TeamFlett.GameObjects.Interfaces;
using RPG_TeamFlett.GUI.Core;

namespace RPG_TeamFlett.GUI.Screens
{
    class LevelScreen : GameScreen
    {
        private int levelNumber,
            characterClassNumber;
        public Texture2D background;
        public string path;
        public IList<IGameObject> GameObjects { get; set; }
        private Player player;

        public LevelScreen(int characterClassNumber)
        {
            this.levelNumber = 1;
            this.characterClassNumber = characterClassNumber;
            this.GameObjects = new List<IGameObject>();
            if (this.characterClassNumber == 1)
                player = new PlayerWithSpear(Vector2.Zero);
            if (this.characterClassNumber == 2)
                player = new BlinkPlayer(Vector2.Zero);
            if (this.characterClassNumber == 3)
                player = new FasterPlayer(Vector2.Zero);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            this.path = @"Resourses/Screens/Black_background.jpg";
            background = Content.Load<Texture2D>(path);
            player.LoadContent(Content);
            foreach (var gameObject in GameObjects)
            {
                gameObject.LoadContent(Content);
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
            player.Update(gameTime);
            foreach (var gameObject in GameObjects)
            {
                gameObject.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            player.Draw(spriteBatch);
            foreach (var gameObject in GameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
        }
    }
}
