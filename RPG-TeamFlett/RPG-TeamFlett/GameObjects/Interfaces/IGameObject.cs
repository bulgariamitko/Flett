using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG_TeamFlett.GameObjects.Interfaces
{
    public interface IGameObject
    {
        IGameObject Collides(IList<IGameObject> gameObjects);

        Texture2D Texture { get; }

        Rectangle BoundBox { get; }

        Vector2 Position { get; }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

        void LoadContent(ContentManager content);

        int GetID();
    }
}
