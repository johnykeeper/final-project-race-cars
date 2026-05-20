using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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

        bool startPressed = false;
        bool carsPressed = false;
        bool quitPressed = false;


        enum Screen {Menu, TrackSelect, Race }
        Screen screen;
        MouseState mouse;
        Rectangle window;

        float carsScale = 1.0f;
        float startScale = 1.0f;
        float quitScale = 1.0f;

        int startX = 40, startY = 206, startW = 300, startH = 68;
        int carsX = 40, carsY = 280, carsW = 300, carsH = 68;
        int quitX = 40, quitY = 354, quitW = 300, quitH = 68;

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

                if (mouseX >= startX && mouseX <= startX + startW && mouseY >= startY && mouseY <= startY + startH)
                {
                    if (mouse.LeftButton == ButtonState.Pressed && startPressed == false)
                    {
                        startPressed = true;
                        startScale = 0.9f;
                    }
                    if (mouse.LeftButton == ButtonState.Released && startPressed == true)
                    {
                        startPressed = false;
                        startScale = 1.0f;
                        //screen = Screen.TrackSelect;
                    }
                }
                else
                {
                    startScale = 1.0f;
                    startPressed = false;
                }

                if (mouseX >= carsX && mouseX <= carsX + carsW && mouseY >= carsY && mouseY <= carsY + carsH)
                {
                    if (mouse.LeftButton == ButtonState.Pressed && carsPressed == false)
                    {
                        carsPressed = true;
                        carsScale = 0.9f;
                    }
                    if (mouse.LeftButton == ButtonState.Released && carsPressed == true)
                    {
                        carsPressed = false;
                        carsScale = 1.0f;
                        //screen = Screen.Cars;
                    }
                }
                else
                {
                    carsScale = 1.0f;
                    carsPressed = false;
                }

                if (mouseX >= quitX && mouseX <= quitX + quitW && mouseY >= quitY && mouseY <= quitY + quitH)
                {
                    if (mouse.LeftButton == ButtonState.Pressed && quitPressed == false)
                    {
                        quitPressed = true;
                        quitScale = 0.9f;
                    }
                    if (mouse.LeftButton == ButtonState.Released && quitPressed == true)
                    {
                        quitPressed = false;
                        quitScale = 1.0f;
                        //Exit();
                    }
                }
                else
                {
                    quitScale = 1.0f;
                    quitPressed = false;
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
                if(screen == Screen.Menu)
{
                    _spriteBatch.Draw(menuBackground, window, Color.White);

                    // Draw buttons with scale
                    Vector2 startOrigin = new Vector2(0, 0);
                    _spriteBatch.Draw(startButton, new Rectangle(startX, startY, (int)(startW * startScale), (int)(startH * startScale)), Color.White);

                    _spriteBatch.Draw(carsButton, new Rectangle(carsX, carsY, (int)(carsW * carsScale), (int)(carsH * carsScale)), Color.White);

                    _spriteBatch.Draw(quitButton, new Rectangle(quitX, quitY, (int)(quitW * quitScale), (int)(quitH * quitScale)), Color.White);
                }
            }
            _spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
