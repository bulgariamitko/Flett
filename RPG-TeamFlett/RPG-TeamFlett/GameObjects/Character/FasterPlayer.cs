using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG_TeamFlett.GameObjects.Character
{
    class FasterPlayer : Player
    {
        private const float DefaultSpeed = 150f;

        public FasterPlayer(Vector2 position)
            : base(position, DefaultSpeed)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>(@"Resourses/Character/char3.png");
        }
    }
}
