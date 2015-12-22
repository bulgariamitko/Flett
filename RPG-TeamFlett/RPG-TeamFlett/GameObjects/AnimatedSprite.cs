using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG_TeamFlett.GameObjects
{
    public abstract class AnimatedSprite : GameObject
    {
        protected enum Direction { None, Up, Left, Down, Right };

        protected Direction CurrentDirection = Direction.None;

        protected int frameIndex = 0;
        private double timeElapsed = 0d;
        private double timeToUpdate = 0d;
        protected string currentAnimation = null;

        protected Vector2 sDirection;

        private Dictionary<string, Rectangle[]> sAnimations = new Dictionary<string, Rectangle[]>();

        protected AnimatedSprite(Vector2 position, Rectangle boundBox) : base(position, boundBox)
        {
            this.FramesPerSecound = 10;

            this.AddAnimation("Up", 9, 8, 0, 64, 64, new Vector2(0, 0));
            this.AddAnimation("IdleUp", 1, 8, 0, 64, 64, new Vector2(0, 0));

            this.AddAnimation("Left", 9, 9, 0, 64, 64, new Vector2(0, 0));
            this.AddAnimation("IdleLeft", 1, 9, 0, 64, 64, new Vector2(0, 0));

            this.AddAnimation("Down", 9, 10, 0, 64, 64, new Vector2(0, 0));
            this.AddAnimation("IdleDown", 1, 10, 0, 64, 64, new Vector2(0, 0));

            this.AddAnimation("Right", 9, 11, 0, 64, 64, new Vector2(0, 0));
            this.AddAnimation("IdleRight", 1, 11, 0, 64, 64, new Vector2(0, 0));

            this.AddAnimation("AttackUp", 8, 4, 0, 64, 64, new Vector2(0, 0));
            this.AddAnimation("AttackLeft", 8, 5, 0, 64, 64, new Vector2(0, 0));
            this.AddAnimation("AttackDown", 8, 6, 0, 64, 64, new Vector2(0, 0));
            this.AddAnimation("AttackRight", 8, 7, 0, 64, 64, new Vector2(0, 0));

            this.PlayAnimation("IdleDown");
        }

        public int FramesPerSecound
        {
            set
            {
                this.timeToUpdate = (1f / value);
            }
        }

        public void AddAnimation(string name, int frames, int yRow, int xStartFrame,
            int width, int height, Vector2 offset)
        {
            int yPos = 64 * yRow;

            Rectangle[] rectangle = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
            {
                rectangle[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }
            this.sAnimations.Add(name, rectangle);
        }

        protected virtual void AnimationDone(string currentAnimation)
        {
            
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timeElapsed > this.timeToUpdate)
            {
                this.timeElapsed -= this.timeToUpdate;

                if (this.frameIndex < this.sAnimations[this.currentAnimation].Length - 1)
                {
                    this.frameIndex++;
                }
                else
                {
                    AnimationDone(currentAnimation);
                    this.frameIndex = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.Texture,
                this.Position,
                this.sAnimations[this.currentAnimation][this.frameIndex],
                Color.White);
        }

        public virtual void PlayAnimation(string newAnimation)
        {                                                               
            if ((this.currentAnimation != newAnimation) &&
                (this.CurrentDirection == Direction.None))
            {
                this.currentAnimation = newAnimation;
                this.frameIndex = 0;
            }
        }
    }
}
