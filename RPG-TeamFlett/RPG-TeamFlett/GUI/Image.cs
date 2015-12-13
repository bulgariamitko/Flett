using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;
using RPG_TeamFlett.GUI.Screen;

namespace RPG_TeamFlett.GUI
{
    public class Image
    {
        private Vector2 origin = default(Vector2);
        private ContentManager content = null;
        private RenderTarget2D renderTarget = null;
        private Dictionary<string, ImageEffect> effectList = null;

        //private SpriteFont font = null;

        public Image()
        {
            this.Path = string.Empty;
            this.Text = string.Empty;
            //   this.FontName = "Fonts/Orbitron";
            this.Position = Vector2.Zero;
            this.Scale = Vector2.One;
            this.Alpha = 1.0f;
            this.SourceRect = Rectangle.Empty;
            this.effectList = new Dictionary<string, ImageEffect>();
        }

        public float Alpha { get; set; }

        public string Text { get; set; }
        //  public string FontName { get; set; }
        public string Path { get; set; }
        public string Effects { get; set; }

        public bool IsActive { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }

        [XmlIgnore]
        public Texture2D Texture { get; set; }

        public Rectangle SourceRect { get; set; }

        public FadeEffect FadeEffect;

        public void SetEffect<T>(ref T effect)
        {
            if (effect == null)
            {
                effect = (T)Activator.CreateInstance(typeof(T));
            }
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }
            this.effectList.Add(
                effect.GetType().ToString().Replace("RPG_TeamFlett.GUI.", string.Empty),
                effect as ImageEffect);
        }

        public void ActivateEffect(string effect)
        {
            if (this.effectList.ContainsKey(effect))
            {
                this.effectList[effect].IsActive = true;
                var obj = this;
                this.effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (this.effectList.ContainsKey(effect))
            {
                this.effectList[effect].IsActive = false;
                this.effectList[effect].UnloadContent();
            }
        }

        public void LoadContent()
        {
            this.content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (this.Path != string.Empty)
            {
                this.Texture = this.content.Load<Texture2D>(this.Path);
            }

            //   this.font = this.content.Load<SpriteFont>(this.FontName);

            Vector2 dimensions = Vector2.Zero;

            if (this.Texture != null)
            {
                dimensions.X += Texture.Width;
            }
            // dimensions.X += this.font.MeasureString(this.Text).X;

            if (this.Texture != null)
            {
                dimensions.Y = this.Texture.Height; //Math.Max(this.Texture.Height, font.MeasureString(this.Text).Y);
            }
            //else
            //{
            //    dimensions.Y = this.font.MeasureString(this.Text).Y;
            //}

            if (this.SourceRect == Rectangle.Empty)
            {
                this.SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
            }

            if (this.renderTarget == null)
            {
                return;
            }
            
            this.renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicDevice,
                (int)dimensions.X, (int)dimensions.Y);
            ScreenManager.Instance.GraphicDevice.SetRenderTarget(this.renderTarget);
            ScreenManager.Instance.GraphicDevice.Clear(Color.Transparent);

            ScreenManager.Instance.SpriteBatch.Begin();
            if (this.Texture != null)
            {
                ScreenManager.Instance.SpriteBatch.Draw(this.Texture, Vector2.Zero, Color.White);
            }
            //   ScreenManager.Instance.SpriteBatch.DrawString(this.font, this.Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.End();

            this.Texture = this.renderTarget;
            ScreenManager.Instance.GraphicDevice.SetRenderTarget(null);

            this.SetEffect<FadeEffect>(ref FadeEffect);
            if (this.Effects != string.Empty)
            {
                string[] split = this.Effects.Split(':');
                foreach (string item in split)
                {
                    this.ActivateEffect(item);
                }
            }
        }

        public void UnloadContent()
        {
            this.content.Unload();
            foreach (var effect in this.effectList)
            {
                this.DeactivateEffect(effect.Key);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in this.effectList)
            {
                if (effect.Value.IsActive == true)
                {
                    effect.Value.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.origin = new Vector2(this.SourceRect.X / 2,
                this.SourceRect.Y / 2);
            spriteBatch.Draw(this.Texture, this.Position + this.origin, this.SourceRect, Color.White * this.Alpha,
                0.0f, this.origin, this.Scale, SpriteEffects.None, 0.0f);
        }
    }
}
