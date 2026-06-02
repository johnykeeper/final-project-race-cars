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
        Texture2D trackSelectBackground;
        Texture2D carsSelectBackground;
        Texture2D Track1Background;
        Texture2D Track1Cover;
        Cars player1Car;
        Cars player2Car;

        bool startPressed = false;
        bool carsPressed = false;
        bool quitPressed = false;
        bool backPressed = false;
        bool Track1Pressed = false;
        bool carsBackPressed = false;
        bool raceStarted = false;

        Rectangle player1Rect = new Rectangle(50, 130, 240, 160);
        Rectangle player2Rect = new Rectangle(500, 130, 240, 160);

        Rectangle[] colorRects = new Rectangle[8];
        Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Purple, Color.Orange, Color.Pink, Color.White };

        int selectedCar = 1;


        enum Screen {Menu, TrackSelect, Race, carsColour, Track1}
        Screen screen;
        MouseState mouse;
        MouseState oldMouse;
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
            carsSelectBackground = Content.Load<Texture2D>("cars-colours");
            player1Car = new Cars(Content.Load<Texture2D>("player1car"), player1Rect);
            player2Car = new Cars(Content.Load<Texture2D>("player2car"), player2Rect);
            Track1Background = Content.Load<Texture2D>("track1");
            Track1Cover = Content.Load<Texture2D>("track1-Cover");




            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouse = Mouse.GetState();
            Window.Title = $"Mouse position : X={mouse.X}, Y={mouse.Y}";
            if (screen == Screen.Menu)
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
                        screen = Screen.carsColour;
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
                else if (mouse.X >= 35 && mouse.X <= 184 && mouse.Y >= 107 && mouse.Y <= 251)
                {
                    if (mouse.LeftButton == ButtonState.Pressed && Track1Pressed == false)
                    {
                        Track1Pressed = true;
                    }
                    if (mouse.LeftButton == ButtonState.Released && Track1Pressed == true)
                    {
                        Track1Pressed = false;
                        raceStarted = false;
                        screen = Screen.Track1;
                    }

                }
                else
                {
                    backPressed = false;
                    Track1Pressed = false;
                }
            }

            else if(screen == Screen.Track1)
            {
                KeyboardState kb = Keyboard.GetState();
                if(!raceStarted)
                {
                    player1Car.Position = new Rectangle(300, 350, 40, 60);
                    player2Car.Position = new Rectangle(400, 350, 40, 60);
                    player1Car.rotationAngle = MathHelper.PiOver2;
                    player2Car.rotationAngle = MathHelper.PiOver2;
                    player1Car.IsSelected = false;
                    player2Car.IsSelected = false;
                    player1Car.OnTrack = true;
                    player2Car.OnTrack = true;
                    raceStarted = true;
                }

                if (mouse.X >= 20 && mouse.X <= 160 && mouse.Y >= 410 && mouse.Y <= 460)
                {
                    if (mouse.LeftButton == ButtonState.Pressed && backPressed == false)
                    {
                        backPressed = true;
                    }
                    if (mouse.LeftButton == ButtonState.Released && backPressed == true)
                    {
                        backPressed = false;
                        player1Car.OnTrack = false;
                        player2Car.OnTrack = false;
                        raceStarted = false;
                        screen = Screen.TrackSelect;
                    }
                }

                else
                {
                    backPressed = false;
                }

                //player1


                if (kb.IsKeyDown(Keys.A))
                    player1Car.rotationAngle -= 0.05f;
                if (kb.IsKeyDown(Keys.D))
                    player1Car.rotationAngle += 0.05f;
                if (kb.IsKeyDown(Keys.W))
                {
                    player1Car.Position = new Rectangle(player1Car.Position.X + (int)(Math.Cos(player1Car.rotationAngle - MathHelper.PiOver2) * player1Car.speed),
                    player1Car.Position.Y + (int)(Math.Sin(player1Car.rotationAngle - MathHelper.PiOver2) * player1Car.speed),
                    player1Car.Position.Width, player1Car.Position.Height);
                }



                //player2
                
                if (kb.IsKeyDown(Keys.Left))
                    player2Car.rotationAngle -= 0.05f;
                if (kb.IsKeyDown(Keys.Right))
                    player2Car.rotationAngle += 0.05f;
                if (kb.IsKeyDown(Keys.Up))
                {
                    player2Car.Position = new Rectangle(player2Car.Position.X + (int)(Math.Cos(player2Car.rotationAngle - MathHelper.PiOver2) * player2Car.speed),
                   player2Car.Position.Y + (int)(Math.Sin(player2Car.rotationAngle - MathHelper.PiOver2) * player2Car.speed),
                   player2Car.Position.Width, player2Car.Position.Height);
                }



                



               
               

                

            }

            //cars
            else if (screen == Screen.carsColour)
            {
                // Check for mouse click

                player1Car.Position = new Rectangle(50, 130, 240, 160);
                player2Car.Position = new Rectangle(500, 130, 240, 160);
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                {

                    if (mouse.X >= player1Rect.X && mouse.X <= player1Rect.X + 240 && mouse.Y >= player1Rect.Y && mouse.Y <= player1Rect.Y + 160)
                    {
                        selectedCar = 1;
                        player1Car.IsSelected = true;
                        player2Car.IsSelected = false;
                    }

                    if (mouse.X >= player2Rect.X && mouse.X <= player2Rect.X + 240 && mouse.Y >= player2Rect.Y && mouse.Y <= player2Rect.Y + 160)
                    {
                        selectedCar = 2;
                        player1Car.IsSelected = false;
                        player2Car.IsSelected = true;
                    }

                    if (mouse.Y >= 350 && mouse.Y <= 392)
                    {
                        if (mouse.X >= 75 && mouse.X <= 112)      // Colour 1
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.Red;
                            else player2Car.Tint = Color.Red;
                        }
                        else if (mouse.X >= 130 && mouse.X <= 167) // Colour 2
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.Orange;
                            else player2Car.Tint = Color.Orange;
                        }
                        else if (mouse.X >= 185 && mouse.X <= 222) // Colour 3
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.Yellow;
                            else player2Car.Tint = Color.Yellow;
                        }
                        else if (mouse.X >= 240 && mouse.X <= 277) // Colour 4
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.LightGreen;
                            else player2Car.Tint = Color.LightGreen;
                        }
                        else if (mouse.X >= 195 && mouse.X <= 332) // Colour 5
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.Green;
                            else player2Car.Tint = Color.Green;
                        }
                        else if (mouse.X >= 350 && mouse.X <= 387) // Colour 6
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.LightBlue;
                            else player2Car.Tint = Color.LightBlue;
                        }
                        else if (mouse.X >= 405 && mouse.X <= 442) // Colour 7
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.Blue;
                            else player2Car.Tint = Color.Blue;
                        }
                        else if (mouse.X >= 460 && mouse.X <= 497) // Colour 8
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.Purple;
                            else player2Car.Tint = Color.Purple;
                        }
                        else if (mouse.X >= 515 && mouse.X <= 552) // Colour 9
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.Pink;
                            else player2Car.Tint = Color.Pink;
                        }
                        else if (mouse.X >= 570 && mouse.X <= 607) // Colour 9
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.White;
                            else player2Car.Tint = Color.White;
                        }
                        else if (mouse.X >= 625 && mouse.X <= 662) // Colour 9
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.Gray;
                            else player2Car.Tint = Color.Gray;
                        }
                        else if (mouse.X >= 680 && mouse.X <= 717) // Colour 9
                        {
                            if (selectedCar == 1) player1Car.Tint = Color.Black;
                            else player2Car.Tint = Color.Black;
                        }
                    }


                    if (mouse.X >= 20 && mouse.X <= 100 && mouse.Y >= 430 && mouse.Y <= 470)
                    {
                        player1Car.OnTrack = false;
                        player2Car.OnTrack = false;
                        raceStarted = false;
                        screen = Screen.Menu;
                    }
                }
            }








            oldMouse = mouse;

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

                _spriteBatch.Draw(Track1Cover, new Rectangle(43, 116, 137, 110), Color.White);
                }
            else if (screen == Screen.carsColour)
            {
                _spriteBatch.Draw(carsSelectBackground, window, Color.White);

                player1Car.Draw(_spriteBatch);
                player2Car.Draw(_spriteBatch);
            }
            else if (screen == Screen.Track1)
            { 
                
                _spriteBatch.Draw(Track1Background, window, Color.White);
                player1Car.Draw(_spriteBatch);
                player2Car.Draw(_spriteBatch);

            }



                _spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
