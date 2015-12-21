using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        protected override void HandleInput(KeyboardState keyState)
        {
            base.HandleInput(keyState);
            if (keyState.IsKeyDown(Keys.Space))
            {
                if (this.currentAnimation.Contains("Up"))
                {
                    this.attacking = true;
                    this.sDirection += new Vector2(0,-50);
                    this.CurrentDirection = Direction.Up;
                }
                if (this.currentAnimation.Contains("Left"))
                {
                    this.PlayAnimation("AttackLeft");
                    this.attacking = true;
                    this.sDirection += new Vector2(-50, 0);
                    this.CurrentDirection = Direction.Left;
                }
                if (this.currentAnimation.Contains("Down"))
                {
                    this.PlayAnimation("AttackDown");
                    this.attacking = true;
                    this.sDirection += new Vector2(0, 50);
                    this.CurrentDirection = Direction.Down;
                }
                if (this.currentAnimation.Contains("Right"))
                {
                    this.PlayAnimation("AttackRight");
                    this.attacking = true;
                    this.sDirection += new Vector2(50, 0);
                    this.CurrentDirection = Direction.Right;
                }
                this.attacking = false;
            }
            else if (this.attacking == false)
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
        }
    }
}
