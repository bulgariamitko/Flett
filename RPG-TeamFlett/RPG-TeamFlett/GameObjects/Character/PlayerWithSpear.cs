using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_TeamFlett.GameObjects.Interfaces;
using RPG_TeamFlett.GUI;

namespace RPG_TeamFlett.GameObjects.Character
{
    public class PlayerWithSpear : Player
    {
        private const int DefaulCooldown = 300;
        private int attackCooldown;
        private IList<IGameObject> enemies; 

        public PlayerWithSpear(Vector2 position, IList<IGameObject> enemies)
            : base(position)
        {
            this.enemies = enemies;
            this.attackCooldown = 0;
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>(@"Resourses/Character/char1.png");
        }

        protected override void HandleInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Space) && this.attackCooldown <= 0)
            {
                if (this.currentAnimation.Contains("Up"))
                {
                    this.PlayAnimation("AttackUp");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Up;
                    HitEnemy(this.Position + new Vector2(25, -25), this.enemies);
                }
                if (this.currentAnimation.Contains("Left"))
                {
                    this.PlayAnimation("AttackLeft");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Left;
                    HitEnemy(this.Position + new Vector2(-25, 25), this.enemies);
                }
                if (this.currentAnimation.Contains("Down"))
                {
                    this.PlayAnimation("AttackDown");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Down;
                    HitEnemy(this.Position + new Vector2(25, 75), this.enemies);
                }
                if (this.currentAnimation.Contains("Right"))
                {
                    this.PlayAnimation("AttackRight");
                    this.attacking = true;
                    this.CurrentDirection = Direction.Right;
                    HitEnemy(this.Position + new Vector2(75, 25), this.enemies);
                }
            }
            base.HandleInput(keyState);
        }

        private void HitEnemy(Vector2 position, IList<IGameObject> enemies)
        {
            
            for (int i = 0; i < enemies.Count; i++)
            {
                var distance = Math.Sqrt(Math.Pow(position.X - enemies[i].Position.X + 25, 2)
                                  + Math.Pow(position.Y - enemies[i].Position.Y + 25, 2));
                if (distance < 50)
                {
                    enemies.RemoveAt(i);
                    break;
                }
            }
            this.attackCooldown = DefaulCooldown;
        }

        public override void Update(GameTime gameTime)
        {
            if (attackCooldown > 0)
                attackCooldown--;
            base.Update(gameTime);
        }
    }
}

//private const float MySpeed = 100f;

//        private bool attacking = false;

//        public PlayerWithSpear(Vector2 position)
//            : base(position, new Rectangle((int)position.X,(int) position.Y, 64, 64))
//        {
//            this.FramesPerSecound = 10;

//            this.AddAnimation("Up", 9, 8, 0, 64, 64, new Vector2(0, 0));
//            this.AddAnimation("IdleUp", 1, 8, 0, 64, 64, new Vector2(0, 0));

//            this.AddAnimation("Left", 9, 9, 0, 64, 64, new Vector2(0, 0));
//            this.AddAnimation("IdleLeft", 1, 9, 0, 64, 64, new Vector2(0, 0));

//            this.AddAnimation("Down", 9, 10, 0, 64, 64, new Vector2(0, 0));
//            this.AddAnimation("IdleDown", 1, 10, 0, 64, 64, new Vector2(0, 0));

//            this.AddAnimation("Right", 9, 11, 0, 64, 64, new Vector2(0, 0));
//            this.AddAnimation("IdleRight", 1, 11, 0, 64, 64, new Vector2(0, 0));

//            this.AddAnimation("AttackUp", 8, 4, 0, 64, 64, new Vector2(0, 0));
//            this.AddAnimation("AttackLeft", 8, 5, 0, 64, 64, new Vector2(0, 0));
//            this.AddAnimation("AttackDown", 8, 6, 0, 64, 64, new Vector2(0, 0));
//            this.AddAnimation("AttackRight", 8, 7, 0, 64, 64, new Vector2(0, 0));

//            this.PlayAnimation("IdleDown");

//        }


//        public override void LoadContent(ContentManager content)
//        {
//            this.Texture = content.Load<Texture2D>(@"Resourses/Character/char1.png");
//        }

//        private void HandleInput(KeyboardState keyState)
//        {
//            if (this.attacking == false)
//            {
//                if (keyState.IsKeyDown(Keys.Up))
//                {
//                    //Moving Up
//                    this.sDirection += new Vector2(0, -1);
//                    this.PlayAnimation("Up");
//                    this.CurrentDirection = Direction.Up;
//                }

//                else if (keyState.IsKeyDown(Keys.Left))
//                {
//                    //Moving Left
//                    this.sDirection += new Vector2(-1, 0);
//                    this.PlayAnimation("Left");
//                    this.CurrentDirection = Direction.Left;

//                }

//                else if (keyState.IsKeyDown(Keys.Down))
//                {
//                    //Moving Down
//                    this.sDirection += new Vector2(0, +1);
//                    this.PlayAnimation("Down");
//                    this.CurrentDirection = Direction.Down;

//                }

//                else if (keyState.IsKeyDown(Keys.Right))
//                {
//                    //Moving Right
//                    this.sDirection += new Vector2(1, 0);
//                    this.PlayAnimation("Right");
//                    this.CurrentDirection = Direction.Right;

//                }
//            }
//            if (keyState.IsKeyDown(Keys.Space))
//            {
//                if (this.currentAnimation.Contains("Up"))
//                {
//                    this.PlayAnimation("AttackUp");
//                    this.attacking = true;
//                    this.CurrentDirection = Direction.Up;
//                }
//                if (this.currentAnimation.Contains("Left"))
//                {
//                    this.PlayAnimation("AttackLeft");
//                    this.attacking = true;
//                    this.CurrentDirection = Direction.Left;
//                }
//                if (this.currentAnimation.Contains("Down"))
//                {
//                    this.PlayAnimation("AttackDown");
//                    this.attacking = true;
//                    this.CurrentDirection = Direction.Down;
//                }
//                if (this.currentAnimation.Contains("Right"))
//                {
//                    this.PlayAnimation("AttackRight");
//                    this.attacking = true;
//                    this.CurrentDirection = Direction.Right;
//                }

//            }
//            else if (this.attacking == false)
//            {
//                if (this.currentAnimation.Contains("Up"))
//                {
//                    this.PlayAnimation("IdleUp");
//                }
//                if (this.currentAnimation.Contains("Left"))
//                {
//                    this.PlayAnimation("IdleLeft");
//                }
//                if (this.currentAnimation.Contains("Down"))
//                {
//                    this.PlayAnimation("IdleDown");
//                }
//                if (this.currentAnimation.Contains("Right"))
//                {
//                    this.PlayAnimation("IdleRight");
//                }
//            }
//            this.CurrentDirection = Direction.None;
//         }

//        public override void Update(GameTime gameTime)
//        {
//            this.sDirection = Vector2.Zero;
//            this.HandleInput(Keyboard.GetState());

//            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

//            this.sDirection *= MySpeed;

//            this.Position += (deltaTime * this.sDirection);

//            base.Update(gameTime);
//        }


//        protected override void AnimationDone(string currentAnimation)
//        {
//            if(currentAnimation != "Attack")
//            {
//                this.attacking = false;
//            }
//        }
