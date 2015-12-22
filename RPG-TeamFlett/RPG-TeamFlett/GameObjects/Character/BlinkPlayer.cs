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
    class BlinkPlayer : Player
    {
        private const int DefaultBlinkCooldown = 300,
            DefaultBlinkRange = 150;
        private int blinkCooldown;

        public BlinkPlayer(Vector2 position) : base(position)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>(@"Resourses/Character/char2.png");
            this.blinkCooldown = DefaultBlinkCooldown /5;
        }

        private void Blink(Direction currentDirection)
        {
            if (currentDirection == Direction.Up)
            {
                var newPosition = this.Position + new Vector2(0, -DefaultBlinkRange);
                if (newPosition.Y < 0)
                {
                    this.Position += new Vector2(0, -this.Position.Y);
                }
                else
                {
                    this.Position += new Vector2(0, -DefaultBlinkRange);
                }
                this.blinkCooldown = DefaultBlinkCooldown;
            }
            else if (currentDirection == Direction.Left)
            {
                var newPosition = this.Position + new Vector2(-DefaultBlinkRange, 0);
                if (newPosition.X < 0)
                {
                    this.Position += new Vector2(-this.Position.X, 0);
                }
                else
                {
                    this.Position += new Vector2(-DefaultBlinkRange, 0);
                }
                this.blinkCooldown = DefaultBlinkCooldown;
            }
            else if (currentDirection == Direction.Down)
            {
                var newPosition = this.Position + new Vector2(0, DefaultBlinkRange);
                var screenHeight = ScreenManager.Instance.Dimentions.Y - this.BoundBox.Height;
                if (newPosition.Y > screenHeight)
                {
                    this.Position += new Vector2(0, screenHeight - this.Position.Y - 1);
                }
                else
                {
                    this.Position += new Vector2(0, DefaultBlinkRange);
                }
                this.blinkCooldown = DefaultBlinkCooldown;
            }
            else if (currentDirection == Direction.Right)
            {
                var newPosition = this.Position + new Vector2(DefaultBlinkRange, 0);
                var screenWidth = ScreenManager.Instance.Dimentions.X - this.BoundBox.Width;
                if (newPosition.X > screenWidth)
                {
                    this.Position += new Vector2(screenWidth - this.Position.X - 1, 0);
                }
                else
                {
                    this.Position += new Vector2(DefaultBlinkRange, 0);
                }
                this.blinkCooldown = DefaultBlinkCooldown;
            }
        }

        protected override void HandleInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Space) && blinkCooldown <= 0)
            {
                if (this.currentAnimation.Contains("Up"))
                {
                    this.PlayAnimation("AttackUp");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Up;
                    this.Blink(Direction.Up);
                }
                else if (this.currentAnimation.Contains("Left"))
                {
                    this.PlayAnimation("AttackLeft");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Left;
                    this.Blink(Direction.Left);
                }
                else if (this.currentAnimation.Contains("Down"))
                {
                    this.PlayAnimation("AttackDown");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Down;
                    this.Blink(Direction.Down);
                }
                else if (this.currentAnimation.Contains("Right"))
                {
                    this.PlayAnimation("AttackRight");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Right;
                    this.Blink(Direction.Right);
                }
            }
            base.HandleInput(keyState);
        }

        public override void Update(GameTime gameTime)
        {
            if (blinkCooldown > 0)
                blinkCooldown--;
            base.Update(gameTime);
        }
    }
}
