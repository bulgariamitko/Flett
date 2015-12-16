using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RPG_TeamFlett.ENUM;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG_TeamFlett.GUI
{
    public abstract class AnimatedSprite
    {
        protected enum Direction { None, Up, Left, Down, Right };

        protected Direction CurrentDirection = Direction.None;

        private readonly Vector2 DefaultConst = new Vector2(0, 0);

        protected Texture2D sTexture = null;
        private int frameIndex = 0;
        private double timeElapsed = 0d;
        private double timeToUpdate = 0d;
        protected string currentAnimation = null;
        protected Vector2 sPosition = Vector2.Zero;
        protected Vector2 sDirection = Vector2.Zero;
        private Dictionary<string, Rectangle[]> sAnimations = new Dictionary<string, Rectangle[]>();

        protected AnimatedSprite(Vector2 postion)
        {
            this.sPosition = postion;
            // this.sAnimations = new Dictionary<string, Rectangle[]>();
        }

        /// <summary>
        /// Calculates the frames per secound.
        /// </summary>
        public int FramesPerSecound
        {
            set
            {
                this.timeToUpdate = (1f / value);
            }
        }

        /// <summary>
        /// Adds the current animation to the array of rectangles
        /// </summary>
        /// <param name="frames"></param>
        public void AddAnimation(
            string name, 
            int frames, 
            int yRow,
            int xStartFrame,
            int width, 
            int height,
            Vector2 offset)
        {
            int yPos = 64 * yRow;

            Rectangle[] rectangle = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
            {
                rectangle[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }
            this.sAnimations.Add(name, rectangle);
        }

        /// <summary>
        /// Time it takes to update a frame
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
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

        protected abstract void AnimationDone(string currentAnimation);


        /// <summary>
        /// Draws the current animation on the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.sTexture,
                this.sPosition,
                this.sAnimations[this.currentAnimation][this.frameIndex],
                Color.White);
        }

        public void PlayAnimation(string newAnimation)
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
