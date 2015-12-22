using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RPG_TeamFlett.GameObjects;
using RPG_TeamFlett.GameObjects.Character;
using RPG_TeamFlett.GameObjects.Interfaces;
using RPG_TeamFlett.GUI.Core;

namespace RPG_TeamFlett.GUI.Screens
{
    class LevelScreen : GameScreen
    {
        private int levelNumber, characterNumber;
        public Texture2D background;
        public string path;
        public IList<IGameObject> GameObjects { get; set; }
        private Player player;

        public LevelScreen(int levelNumber,int characterClassNumber)
        {
            this.levelNumber = levelNumber;
            this.characterNumber = characterClassNumber;
            this.GameObjects = new List<IGameObject>();
            this.GenerateEnemy(levelNumber);
            if (characterClassNumber == 1)
                player = new PlayerWithSpear(Vector2.Zero, this.GameObjects);
            else if (characterClassNumber == 2)
                player = new BlinkPlayer(Vector2.Zero);
            else if (characterClassNumber == 3)
                player = new FasterPlayer(Vector2.Zero);

            float screenWidth = ScreenManager.Instance.Dimentions.X,
                screenHeight = ScreenManager.Instance.Dimentions.Y;
            this.GameObjects.Add(
                new StaticSprite(
                    new Vector2(screenWidth - 100,screenHeight - 100),
                    100,100,
                    @"Resourses/Door.png"));
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

        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
            player.Update(gameTime);
            foreach (var gameObject in GameObjects)
            {
                gameObject.Update(gameTime);
            }


            var gameObj = player.Collides(this.GameObjects);
            if (gameObj != null)
            {
                if (gameObj.GetID() == 2)
                {
                    ScreenManager.Instance.CurrentScreen = new GameOverScreen();
                    ScreenManager.Instance.CurrentScreen.LoadContent();
                }
                else if (gameObj.GetID() == 3)
                {
                    ScreenManager.Instance.CurrentScreen = 
                        new LevelScreen(this.levelNumber + 1, this.characterNumber);
                    ScreenManager.Instance.CurrentScreen.LoadContent();
                }
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
        private void GenerateEnemy(int levelNumber)
        {
            Vector2[] spawsPositions = new Vector2[10];
            int screenWidth = (int)ScreenManager.Instance.Dimentions.X;
            int screenHeight = (int) ScreenManager.Instance.Dimentions.Y;
            int firstSpawnX = 100, firstSpawnY = 100;
            int xInterval = (screenWidth - firstSpawnX)/10, 
                yInterval = (screenHeight - firstSpawnY)/10;
            for (int i = 0; i < 10; i++)
            {
                spawsPositions[i] = new Vector2(firstSpawnX, firstSpawnY);
                firstSpawnX += xInterval;
                firstSpawnY += yInterval;
            }


            bool movingUp = true, movingPositive = false;
            Random rand = new Random();
            for (int i = 0; i < levelNumber + 5 && i < 10; i++)
            {
                this.GameObjects.Add(
                    new Enemy(spawsPositions[i],movingUp, movingPositive, 100 + (levelNumber - 1) * 20));

                int tmp1 = rand.Next(0, 10),
                    tmp2 = rand.Next(0, 10);
                movingUp = tmp1 < 5;
                movingPositive = tmp2 >= 5;
            }
        }
    }
}
