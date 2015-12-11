using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPG_TeamFlett.GUI
{
    public class FadeEffect : ImageEffect
    {
        public FadeEffect()
        {
            this.FadeSpeed = 1f;
            this.Incrase = false;
        }

        public float FadeSpeed { get; set; }
        public bool Incrase { get; set; }

        public override void LoadContent(ref Image Image)
        {
            base.LoadContent(ref Image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (base.image.IsActive == true)
            {
                if (this.Incrase == false)
                {
                    base.image.Alpha -= this.FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    base.image.Alpha += this.FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (base.image.Alpha < 0.0f)
                {
                    this.Incrase = true;
                    base.image.Alpha = 0.0f;
                }
                else if (this.image.Alpha > 1.0f)
                {
                    this.Incrase = false;
                    this.image.Alpha = 1.0f;
                }
            }
            else
            {
                this.image.Alpha = 1.0f;
            }
        }
    }
}
