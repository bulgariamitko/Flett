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
        private Color[] button_color = new Color[NumberOfButtons];
        private Rectangle[] button_rectangle = new Rectangle[NumberOfButtons];
        private BState[] button_state = new BState[NumberOfButtons];
        private Texture2D[] button_texture = new Texture2D[NumberOfButtons];
        private double[] button_timer = new double[NumberOfButtons];
        //mouse pressed and mouse just pressed
        private bool mpressed, prev_mpressed = false;
        //mouse location in window
        private int mx, my;
        private double frame_time;

        public MenuScreen()
        {
            int x = (int)ScreenManager.Instance.Dimentions.X / 2 - buttonWidth / 2;
            int y = (int)ScreenManager.Instance.Dimentions.Y / 2 -
                NumberOfButtons / 2 * ButtonHeight -
                (NumberOfButtons % 2) * ButtonHeight / 2;
            for (int i = 0; i < NumberOfButtons; i++)
            {
                button_state[i] = BState.UP;
                button_color[i] = Color.White;
                button_timer[i] = 0.0;
                button_rectangle[i] = new Rectangle(x, y, buttonWidth, ButtonHeight);
                y += ButtonHeight + 5;
            }
            backgroundColor = Color.Black;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = Content.Load<Texture2D>(@"Resourses/Screens/Black_background.jpg");
            button_texture[firstClassButtonIndex] =
                Content.Load<Texture2D>(@"Resourses/Buttons/button1.png");
            button_texture[secondClassButtonIndex] =
                Content.Load<Texture2D>(@"Resourses/Buttons/button2.png");
            button_texture[thirdClassButtonIndex] =
                Content.Load<Texture2D>(@"Resourses/Buttons/button3.png");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {


            // get elapsed frame time in seconds
            frame_time = gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            // update mouse variables
            MouseState mouse_state = Mouse.GetState();
            mx = mouse_state.X;
            my = mouse_state.Y;
            prev_mpressed = mpressed;
            mpressed = mouse_state.LeftButton == ButtonState.Pressed;

            UpdateButtons();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(background);
            for (int i = 0; i < NumberOfButtons; i++)
                spriteBatch.Draw(button_texture[i], button_rectangle[i], button_color[i]);
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
                    ScreenManager.Instance.CurrentScreen = new LevelScreen(1);
                    ScreenManager.Instance.CurrentScreen.LoadContent();
                    break;
                case secondClassButtonIndex:
                    ScreenManager.Instance.CurrentScreen = new LevelScreen(2);
                    ScreenManager.Instance.CurrentScreen.LoadContent();
                    break;
                case thirdClassButtonIndex:
                    ScreenManager.Instance.CurrentScreen = new LevelScreen(3);
                    ScreenManager.Instance.CurrentScreen.LoadContent();
                    break;
            }
        }

        void UpdateButtons()
        {
            for (int i = 0; i < NumberOfButtons; i++)
            {

                if (HitImageAlpha(
                    button_rectangle[i], button_texture[i], mx, my))
                {
                    button_timer[i] = 0.0;
                    if (mpressed)
                    {
                        // mouse is currently down
                        button_state[i] = BState.DOWN;
                        button_color[i] = Color.Blue;
                    }
                    else if (!mpressed && prev_mpressed)
                    {
                        // mouse was just released
                        if (button_state[i] == BState.DOWN)
                        {
                            // button i was just down
                            button_state[i] = BState.JUST_RELEASED;
                        }
                    }
                    else
                    {
                        button_state[i] = BState.HOVER;
                        button_color[i] = Color.LightBlue;
                    }
                }
                else
                {
                    button_state[i] = BState.UP;
                    if (button_timer[i] > 0)
                    {
                        button_timer[i] = button_timer[i] - frame_time;
                    }
                    else
                    {
                        button_color[i] = Color.White;
                    }
                }

                if (button_state[i] == BState.JUST_RELEASED)
                {
                    TakeActionOnButton(i);
                }
            }
        }
    }
}
