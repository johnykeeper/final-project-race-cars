using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace final_project__race_cars
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D menuBackground;
        Texture2D startButton;
        Texture2D quitButton;
        Texture2D carsButton;
        Texture2D trackSelectBackground;

        bool startPressed = false;
        bool carsPressed = false;
        bool quitPressed = false;
        bool backPressed = false;


        enum Screen {Menu, TrackSelect, Race, Cars}
        Screen screen;
        MouseState mouse;
        Rectangle window;

        float carsScale = 1.0f;
        float startScale = 1.0f;
        float quitScale = 1.0f;

        int startOrigX = 40, startOrigY = 206, startW = 300, startH = 68;
        int carsOrigX = 40, carsOrigY = 280, carsW = 300, carsH = 68;
        int quitOrigX = 40, quitOrigY = 358, quitW = 300, quitH = 68;

        int startX = 40, startY = 206;
        int carsX = 40, carsY = 280;
        int quitX = 40, quitY = 354;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 480);
            
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            screen = Screen.Menu;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            menuBackground = Content.Load<Texture2D>("menu-screen");
            startButton = Content.Load<Texture2D>("start-button");
            quitButton = Content.Load<Texture2D>("quit-button");
            carsButton = Content.Load<Texture2D>("cars-button");
            trackSelectBackground = Content.Load<Texture2D>("track-select");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouse = Mouse.GetState();
            if(screen == Screen.Menu)
            {
                int mouseX = mouse.X;
                int mouseY = mouse.Y;

                // START button
                if (mouseX >= startOrigX && mouseX <= startOrigX + startW && mouseY >= startOrigY && mouseY <= startOrigY + startH)
                {
                    if (mouse.LeftButton == ButtonState.Pressed && startPressed == false)
                    {
                        startPressed = true;
                        startScale = 0.9f;
                        startX = startOrigX + (int)(startW * (1 - startScale) / 2);
                        startY = startOrigY + (int)(startH * (1 - startScale) / 2);
                    }
                    if (mouse.LeftButton == ButtonState.Released && startPressed == true)
                    {
                        startPressed = false;
                        startScale = 1.0f;
                        startX = startOrigX;
                        startY = startOrigY;
                        screen = Screen.TrackSelect;
                    }
                }
                else
                {
                    startScale = 1.0f;
                    startPressed = false;
                    startX = startOrigX;
                    startY = startOrigY;
                }




                // CARS button
                if (mouseX >= carsOrigX && mouseX <= carsOrigX + carsW && mouseY >= carsOrigY && mouseY <= carsOrigY + carsH)
                {
                    if (mouse.LeftButton == ButtonState.Pressed && carsPressed == false)
                    {
                        carsPressed = true;
                        carsScale = 0.9f;
                        carsX = carsOrigX + (int)(carsW * (1 - carsScale) / 2);
                        carsY = carsOrigY + (int)(carsH * (1 - carsScale) / 2);
                    }
                    if (mouse.LeftButton == ButtonState.Released && carsPressed == true)
                    {
                        carsPressed = false;
                        carsScale = 1.0f;
                        carsX = carsOrigX;
                        carsY = carsOrigY;
                        //screen = Screen.Cars;
                    }
                }
                else
                {
                    carsScale = 1.0f;
                    carsPressed = false;
                    carsX = carsOrigX;
                    carsY = carsOrigY;
                }



                // QUIT button
                if (mouseX >= quitOrigX && mouseX <= quitOrigX + quitW && mouseY >= quitOrigY && mouseY <= quitOrigY + quitH)
                {
                    if (mouse.LeftButton == ButtonState.Pressed && quitPressed == false)
                    {
                        quitPressed = true;
                        quitScale = 0.9f;
                        quitX = quitOrigX + (int)(quitW * (1 - quitScale) / 2);
                        quitY = quitOrigY + (int)(quitH * (1 - quitScale) / 2);
                    }
                    if (mouse.LeftButton == ButtonState.Released && quitPressed == true)
                    {
                        quitPressed = false;
                        quitScale = 1.0f;
                        quitX = quitOrigX;
                        quitY = quitOrigY;
                        Exit();
                    }
                }
                else
                {
                    quitScale = 1.0f;
                    quitPressed = false;
                    quitX = quitOrigX;
                    quitY = quitOrigY;
                }




            }


            else if (screen == Screen.TrackSelect)
            {
                // Back button hitbox - adjust coordinates as needed
                if (mouse.X >= 20 && mouse.X <= 120 && mouse.Y >= 420 && mouse.Y <= 460)
                {
                    if (mouse.LeftButton == ButtonState.Pressed && backPressed == false)
                    {
                        backPressed = true;
                    }
                    if (mouse.LeftButton == ButtonState.Released && backPressed == true)
                    {
                        backPressed = false;
                        screen = Screen.Menu;
                    }
                }
                else
                {
                    backPressed = false;
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();


                if(screen == Screen.Menu)
{
                    _spriteBatch.Draw(menuBackground, window, Color.White);

                    // Draw buttons with scale
                    Vector2 startOrigin = new Vector2(0, 0);
                    _spriteBatch.Draw(startButton, new Rectangle(startX, startY, (int)(startW * startScale), (int)(startH * startScale)), Color.White);

                    _spriteBatch.Draw(carsButton, new Rectangle(carsX, carsY, (int)(carsW * carsScale), (int)(carsH * carsScale)), Color.White);

                    _spriteBatch.Draw(quitButton, new Rectangle(quitX, quitY, (int)(quitW * quitScale), (int)(quitH * quitScale)), Color.White);
                }
                else if(screen == Screen.TrackSelect)
                {
                    _spriteBatch.Draw(trackSelectBackground, window, Color.White);
                }

            _spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
