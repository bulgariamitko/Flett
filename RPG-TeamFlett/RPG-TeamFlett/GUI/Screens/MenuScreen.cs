using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG_TeamFlett.GUI.Core;

namespace RPG_TeamFlett.GUI.Screens
{
    class MenuScreen : GameScreen
    {
        private enum BState
        {
            HOVER,
            UP,
            JUST_RELEASED,
            DOWN
        }
        private const int NumberOfButtons = 3,
            firstClassButtonIndex = 0,
            secondClassButtonIndex = 1,
            thirdClassButtonIndex = 2,
            ButtonHeight = 40,
            buttonWidth = 151;

        private Texture2D background;
        private Color backgroundColor;
        private Color[] buttonColor = new Color[NumberOfButtons];
        private Rectangle[] buttonRectangle = new Rectangle[NumberOfButtons];
        private BState[] buttonState = new BState[NumberOfButtons];
        private Texture2D[] buttonTexture = new Texture2D[NumberOfButtons];
        private double[] buttonTimer = new double[NumberOfButtons];
        //mouse pressed and mouse just pressed
        private bool mPressed, prevmPressed = false;
        //mouse location in window
        private int mx, my;
        private double frameTime;

        public MenuScreen()
        {
            int x = (int)ScreenManager.Instance.Dimentions.X / 2 - buttonWidth / 2;
            int y = (int)ScreenManager.Instance.Dimentions.Y / 2 -
                NumberOfButtons / 2 * ButtonHeight -
                (NumberOfButtons % 2) * ButtonHeight / 2;
            for (int i = 0; i < NumberOfButtons; i++)
            {
                buttonState[i] = BState.UP;
                buttonColor[i] = Color.White;
                buttonTimer[i] = 0.0;
                buttonRectangle[i] = new Rectangle(x, y, buttonWidth, ButtonHeight);
                y += ButtonHeight + 5;
            }
            backgroundColor = Color.Black;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = Content.Load<Texture2D>(@"Resourses/Screens/menuBackground.png");
            buttonTexture[firstClassButtonIndex] =
                Content.Load<Texture2D>(@"Resourses/Buttons/button1.png");
            buttonTexture[secondClassButtonIndex] =
                Content.Load<Texture2D>(@"Resourses/Buttons/button2.png");
            buttonTexture[thirdClassButtonIndex] =
                Content.Load<Texture2D>(@"Resourses/Buttons/button3.png");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {


            // get elapsed frame time in seconds
            frameTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            // update mouse variables
            MouseState mouseState = Mouse.GetState();
            mx = mouseState.X;
            my = mouseState.Y;
            prevmPressed = mPressed;
            mPressed = mouseState.LeftButton == ButtonState.Pressed;

            UpdateButtons();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background,Vector2.Zero, Color.White);

            for (int i = 0; i < NumberOfButtons; i++)
                spriteBatch.Draw(buttonTexture[i], buttonRectangle[i], buttonColor[i]);
        }

        // wrapper for HitImageAlpha taking Rectangle and Texture
        bool HitImageAlpha(Rectangle rect, Texture2D tex, int x, int y)
        {
            return HitImageAlpha(0, 0, tex, tex.Width * (x - rect.X) /
                rect.Width, tex.Height * (y - rect.Y) / rect.Height);
        }

        // wraps HitImage then determines if hit a transparent part of image 
        bool HitImageAlpha(float tx, float ty, Texture2D tex, int x, int y)
        {
            if (HitImage(tx, ty, tex, x, y))
            {
                uint[] data = new uint[tex.Width * tex.Height];
                tex.GetData<uint>(data);
                if ((x - (int)tx) + (y - (int)ty) *
                    tex.Width < tex.Width * tex.Height)
                {
                    return ((data[
                        (x - (int)tx) + (y - (int)ty) * tex.Width
                        ] &
                                0xFF000000) >> 24) > 20;
                }
            }
            return false;
        }

        // determine if x,y is within rectangle formed by texture located at tx,ty
        bool HitImage(float tx, float ty, Texture2D tex, int x, int y)
        {
            return (x >= tx &&
                x <= tx + tex.Width &&
                y >= ty &&
                y <= ty + tex.Height);
        }

        // Logic for each button click goes here
        void TakeActionOnButton(int i)
        {
            //take action corresponding to which button was clicked
            switch (i)
            {
                case firstClassButtonIndex:
                    ScreenManager.Instance.CurrentScreen = new LevelScreen(1,1);
                    ScreenManager.Instance.CurrentScreen.LoadContent();
                    break;
                case secondClassButtonIndex:
                    ScreenManager.Instance.CurrentScreen = new LevelScreen(1,2);
                    ScreenManager.Instance.CurrentScreen.LoadContent();
                    break;
                case thirdClassButtonIndex:
                    ScreenManager.Instance.CurrentScreen = new LevelScreen(1,3);
                    ScreenManager.Instance.CurrentScreen.LoadContent();
                    break;
            }
        }

        void UpdateButtons()
        {
            for (int i = 0; i < NumberOfButtons; i++)
            {

                if (HitImageAlpha(
                    buttonRectangle[i], buttonTexture[i], mx, my))
                {
                    buttonTimer[i] = 0.0;
                    if (mPressed)
                    {
                        // mouse is currently down
                        buttonState[i] = BState.DOWN;
                        buttonColor[i] = Color.Blue;
                    }
                    else if (!mPressed && prevmPressed)
                    {
                        // mouse was just released
                        if (buttonState[i] == BState.DOWN)
                        {
                            // button i was just down
                            buttonState[i] = BState.JUST_RELEASED;
                        }
                    }
                    else
                    {
                        buttonState[i] = BState.HOVER;
                        buttonColor[i] = Color.LightBlue;
                    }
                }
                else
                {
                    buttonState[i] = BState.UP;
                    if (buttonTimer[i] > 0)
                    {
                        buttonTimer[i] = buttonTimer[i] - frameTime;
                    }
                    else
                    {
                        buttonColor[i] = Color.White;
                    }
                }

                if (buttonState[i] == BState.JUST_RELEASED)
                {
                    TakeActionOnButton(i);
                }
            }
        }
    }
}
