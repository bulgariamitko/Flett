using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG_TeamFlett.GameObjects.Character
{
    class BlinkPlayer : Player
    {
        public BlinkPlayer(Vector2 position) : base(position)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>(@"Resourses/Character/char2.png");
        }
    }
}
