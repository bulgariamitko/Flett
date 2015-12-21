using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RPG_TeamFlett.GameObjects.Interfaces;

namespace RPG_TeamFlett.GameObjects
{
    public abstract class GameObject : IGameObject
    {
        private Rectangle boundBox;
        protected GameObject(Vector2 position, Rectangle boundBox)
        {
            this.BoundBox = boundBox;
            this.Position = position;
        }

        public virtual IGameObject Collides(IList<IGameObject> gameObjects)
        {
            return gameObjects.FirstOrDefault(gameObject => this.BoundBox.Intersects(gameObject.BoundBox));
        }

        public Texture2D Texture { get; protected set; }

        public Rectangle BoundBox
        {
            get { return this.boundBox; }
            private set { this.boundBox = value; }
        }

        public Vector2 Position { get; protected set; }

        public virtual void Update(GameTime gameTime)
        {
            this.boundBox.X = (int) this.Position.X + 10;
            this.boundBox.Y = (int) this.Position.Y + 10;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void LoadContent(ContentManager content)
        {
            
        }

        public abstract int GetID();
    }
}
