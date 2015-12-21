﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG_TeamFlett.GameObjects.Character
{
    public abstract class Player : AnimatedSprite
    {
        private bool attacking = false;

        protected Player(Vector2 position, float speed = 100f)
            : base(position, new Rectangle((int)position.X,(int) position.Y, 64, 64))
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
            this.MySpeed = speed;
        }

        public float MySpeed { get; private set; }

        public abstract void LoadContent(ContentManager content);

        protected virtual void HandleInput(KeyboardState keyState)
        {
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
            if (keyState.IsKeyDown(Keys.Space))
            {
                if (this.currentAnimation.Contains("Up"))
                {
                    this.PlayAnimation("AttackUp");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Up;
                }
                if (this.currentAnimation.Contains("Left"))
                {
                    this.PlayAnimation("AttackLeft");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Left;
                }
                if (this.currentAnimation.Contains("Down"))
                {
                    this.PlayAnimation("AttackDown");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Down;
                }
                if (this.currentAnimation.Contains("Right"))
                {
                    this.PlayAnimation("AttackRight");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Right;
                }

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
            this.CurrentDirection = Direction.None;
         }

        public override void Update(GameTime gameTime)
        {
            this.sDirection = Vector2.Zero;
            this.HandleInput(Keyboard.GetState());

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.sDirection *= MySpeed;

            this.Position += (deltaTime * this.sDirection);

            base.Update(gameTime);
        }

        protected override void AnimationDone(string currentAnimation)
        {
            if(currentAnimation != "Attack")
            {
                this.attacking = false;
            }
        }
    }
}