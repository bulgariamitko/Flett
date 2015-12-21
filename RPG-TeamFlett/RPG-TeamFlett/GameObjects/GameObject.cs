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
        protected GameObject(Vector2 position, Rectangle boundingBox)
        {
            this.BoundingBox = boundingBox;
            this.Position = position;
        }

        public virtual IGameObject Collides(List<IGameObject> gameObjects)
        {
            return gameObjects.FirstOrDefault(gameObject => this.BoundingBox.Intersects(gameObject.BoundingBox));
        }

        public Texture2D Texture { get; protected set; }

        public Rectangle BoundingBox { get; private set; }

        public Vector2 Position { get; protected set; }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void LoadContent(ContentManager content)
        {
            
        }
    }
}
