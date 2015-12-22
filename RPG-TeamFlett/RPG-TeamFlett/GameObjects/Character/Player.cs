using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG_TeamFlett.GUI.Core;

namespace RPG_TeamFlett.GameObjects.Character
{
    public abstract class Player : AnimatedSprite
    {
        private const float DefaultNormalPlayerSpeed = 100f;
        protected bool attacking = false;

        protected Player(Vector2 position, float speed = DefaultNormalPlayerSpeed)
            : base(position, new Rectangle((int)position.X + 10,(int) position.Y + 10, 50, 50))
        {
            this.MySpeed = speed;
        }

        public float MySpeed { get; private set; }

        public abstract void LoadContent(ContentManager content);

        protected virtual void HandleInput(KeyboardState keyState)
        {
             //Every player can implement other keys
             //This is basic movement
            if (this.attacking == false)
            {
                if (keyState.IsKeyDown(Keys.Up))
                {
                    //Moving Up
                    this.sDirection += new Vector2(0, -1);
                    this.PlayAnimation("Up");
                    this.CurrentDirection = Direction.Up;
                }

                else if (keyState.IsKeyDown(Keys.Left))
                {
                    //Moving Left
                    this.sDirection += new Vector2(-1, 0);
                    this.PlayAnimation("Left");
                    this.CurrentDirection = Direction.Left;

                }

                else if (keyState.IsKeyDown(Keys.Down))
                {
                    //Moving Down
                    this.sDirection += new Vector2(0, +1);
                    this.PlayAnimation("Down");
                    this.CurrentDirection = Direction.Down;

                }

                else if (keyState.IsKeyDown(Keys.Right))
                {
                    //Moving Right
                    this.sDirection += new Vector2(1, 0);
                    this.PlayAnimation("Right");
                    this.CurrentDirection = Direction.Right;

                }
            }
            
            if (this.attacking == false)
            {
                if (this.currentAnimation.Contains("Up"))
                {
                    this.PlayAnimation("IdleUp");
                }
                if (this.currentAnimation.Contains("Left"))
                {
                    this.PlayAnimation("IdleLeft");
                }
                if (this.currentAnimation.Contains("Down"))
                {
                    this.PlayAnimation("IdleDown");
                }
                if (this.currentAnimation.Contains("Right"))
                {
                    this.PlayAnimation("IdleRight");
                }
            }
            this.CurrentDirection = Direction.None;
         }

        public override void Update(GameTime gameTime)
        {
            this.sDirection = Vector2.Zero;
            this.HandleInput(Keyboard.GetState());

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.sDirection *= MySpeed;

            var newPos = this.Position + (deltaTime * this.sDirection);
            if (newPos.X >= 0 && newPos.X < ScreenManager.Instance.Dimentions.X - this.BoundBox.Width &&
                newPos.Y >= 0 && newPos.Y < ScreenManager.Instance.Dimentions.Y - this.BoundBox.Height)
                this.Position = newPos;

            base.Update(gameTime);
        }

        protected override void AnimationDone(string currentAnimation)
        {
            if(currentAnimation != "Attack")
            {
                this.attacking = false;
            }
        }

        public override int GetID()
        {
            return 1;
        }
    }
}
