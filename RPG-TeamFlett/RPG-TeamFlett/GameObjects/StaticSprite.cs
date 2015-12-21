using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG_TeamFlett.GameObjects
{
    class StaticSprite : GameObject
    {
        private string pathToLoad;

        public StaticSprite(Vector2 position,int width, int height, string path)
            : base(position, new Rectangle((int)position.X, (int)position.Y,width,height))
        {
            this.pathToLoad = path;
        }

        public override int GetID()
        {
            return 3;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture,this.BoundBox,Color.White);
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>(this.pathToLoad);
        }
    }
}
