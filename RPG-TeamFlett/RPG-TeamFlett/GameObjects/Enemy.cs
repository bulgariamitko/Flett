using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG_TeamFlett.GUI.Core;

namespace RPG_TeamFlett.GameObjects
{
    public class Enemy : AnimatedSprite
    {
        private const float DefaultEnemySpeed = 100f;
        private bool isMovingUpDown;
        public Enemy(Vector2 position, bool isMovingUpDown, bool isMovingPositive, float speed = DefaultEnemySpeed)
            : base(position, new Rectangle((int)position.X + 10, (int)position.Y + 10, 50, 50))
        {
            this.Speed = speed;
            if (isMovingUpDown)
            {
                if (isMovingPositive)
                {
                    this.CurrentDirection = Direction.Down;
                    this.PlayAnimation("Down");
                }
                else
                {
                    this.CurrentDirection = Direction.Up;
                    this.PlayAnimation("Up");
                }
            }
            else
            {
                if (isMovingPositive)
                {
                    this.CurrentDirection = Direction.Right;
                    this.PlayAnimation("Right");
                }
                else
                {
                    this.CurrentDirection = Direction.Left;
                    this.PlayAnimation("Left");
                }
            }
            this.isMovingUpDown = isMovingUpDown;
        }

        public float Speed { get; private set; }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>(@"Resourses/Character/guard.png");
        }

        public override int GetID()
        {
            return 2;
        }

        public override void Update(GameTime gameTime)
        {
            this.sDirection = Vector2.Zero;
            this.Movement();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.sDirection *= Speed;

            this.Position += (deltaTime * this.sDirection);

            base.Update(gameTime);
        }

        protected virtual void Movement()
        {
            if (isMovingUpDown)
            {
                if (this.Position.Y < 5)
                    this.CurrentDirection = Direction.Down;
                if (this.Position.Y > ScreenManager.Instance.Dimentions.Y - 50)
                    this.CurrentDirection = Direction.Up;

                if (this.CurrentDirection == Direction.Up)
                {
                    //Moving Up
                    this.sDirection += new Vector2(0, -1);
                    this.PlayAnimation("Up");
                    this.CurrentDirection = Direction.Up;
                }

                else if (this.CurrentDirection == Direction.Down)
                {
                    //Moving Down
                    this.sDirection += new Vector2(0, +1);
                    this.PlayAnimation("Down");
                    this.CurrentDirection = Direction.Down;
                }
            }
            else
            {
                if (this.Position.X < 5)
                    this.CurrentDirection = Direction.Right;
                if (this.Position.X > ScreenManager.Instance.Dimentions.X - 50)
                    this.CurrentDirection = Direction.Left;

                if (this.CurrentDirection == Direction.Left)
                {
                    //Moving Left
                    this.sDirection += new Vector2(-1, 0);
                    this.PlayAnimation("Left");
                    this.CurrentDirection = Direction.Left;

                }
                else if (this.CurrentDirection == Direction.Right)
                {
                    //Moving Right
                    this.sDirection += new Vector2(1, 0);
                    this.PlayAnimation("Right");
                    this.CurrentDirection = Direction.Right;
                }
            }
         }

        public override void PlayAnimation(string newAnimation)
        {
            if ((this.currentAnimation != newAnimation))
            {
                this.currentAnimation = newAnimation;
                this.frameIndex = 0;
            }
        }
    }
}
