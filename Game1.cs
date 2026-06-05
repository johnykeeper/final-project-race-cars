using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


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
        List<Rectangle> wallRects = new List<Rectangle>();
        List<Rectangle> slowRects = new List<Rectangle>();

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
            wallRects = new List<Rectangle>
            {
             new Rectangle(189, 297, 418, 13),
             new Rectangle(191, 135, 9, 163),
             new Rectangle(197, 130, 45, 10),
             new Rectangle(239, 136, 26, 21),
             new Rectangle(265, 148, 272, 14),
             new Rectangle(538, 134, 20, 21),
             new Rectangle(558, 125, 49, 18),
             new Rectangle(597, 137, 14, 167),
             new Rectangle(169, 362, 470, 14),
             new Rectangle(134, 331, 16, 25),
             new Rectangle(124, 100, 13, 217),
             new Rectangle(158, 69, 118, 10),
             new Rectangle(136, 80, 23, 16),
             new Rectangle(274, 72, 249, 22),
             new Rectangle(521, 62, 117, 16),
             new Rectangle(632, 73, 29, 16),
             new Rectangle(656, 108, 22, 239),
             new Rectangle(638, 345, 20, 22)
             };
            slowRects = new List<Rectangle>
            {
                new Rectangle(81, 375, 650, 18),
                new Rectangle(681, 52, 37, 325),
                new Rectangle(667, 333, 8, 38),
                new Rectangle(658, 351, 6, 17),
                new Rectangle(635, 364, 20, 7),
                new Rectangle(669, 58, 10, 43),
                new Rectangle(652, 53, 11, 35),
                new Rectangle(632, 44, 14, 30),
                new Rectangle(509, 46, 122, 20),
                new Rectangle(498, 56, 34, 18),
                new Rectangle(280, 59, 230, 19),
                new Rectangle(267, 47, 12, 22),
                new Rectangle(141, 49, 123, 14),
                new Rectangle(140, 55, 26, 15),
                new Rectangle(131, 62, 26, 16),
                new Rectangle(123, 69, 19, 17),
                new Rectangle(116, 82, 16, 24),
                new Rectangle(102, 107, 24, 223),
                new Rectangle(118, 329, 17, 27),
                new Rectangle(135, 341, 9, 29),
                new Rectangle(143, 355, 15, 27),
                new Rectangle(157, 368, 25, 18),
                new Rectangle(203, 280, 395, 19),
                new Rectangle(573, 143, 23, 140),
                new Rectangle(556, 141, 17, 19),
                new Rectangle(550, 145, 6, 27),
                new Rectangle(538, 156, 9, 17),
                new Rectangle(258, 160, 276, 14),
                new Rectangle(239, 146, 8, 35),
                new Rectangle(247, 152, 8, 32),
                new Rectangle(206, 139, 27, 15),
                new Rectangle(202, 145, 10, 139)

            };
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
                    player1Car.pos = new Vector2(300, 350);
                    player2Car.pos = new Vector2(400, 350);
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
                    player1Car.currentSpeed = player1Car.speed;
                    player1Car.pos.X += (float)Math.Cos(player1Car.rotationAngle - MathHelper.PiOver2) * player1Car.speed;
                    player1Car.pos.Y += (float)Math.Sin(player1Car.rotationAngle - MathHelper.PiOver2) * player1Car.speed;
                }
                else
                {
                    player1Car.currentSpeed = 0f;
                }
                    
                    



                //player2

                if (kb.IsKeyDown(Keys.Left))
                    player2Car.rotationAngle -= 0.05f;
                if (kb.IsKeyDown(Keys.Right))
                    player2Car.rotationAngle += 0.05f;
                if (kb.IsKeyDown(Keys.Up))
                {
                    player2Car.currentSpeed = player2Car.speed;
                    player2Car.pos.X += (float)Math.Cos(player2Car.rotationAngle - MathHelper.PiOver2) * player2Car.speed;
                    player2Car.pos.Y += (float)Math.Sin(player2Car.rotationAngle - MathHelper.PiOver2) * player2Car.speed;

                }
                else
                {
                    player2Car.currentSpeed = 0f;
                }

                    Rectangle p1bounds = new Rectangle((int)player1Car.pos.X - 5, (int)player1Car.pos.Y - 7, 10, 14);
                Rectangle p2bounds = new Rectangle((int)player2Car.pos.X - 5, (int)player2Car.pos.Y - 7, 10, 14);



                if(p1bounds.Intersects(p2bounds))
                {
                    Vector2 diff = player1Car.pos - player2Car.pos;
                    diff.Normalize();
                    while(p1bounds.Intersects(p2bounds))
                    {
                        p1bounds = new Rectangle((int)player1Car.pos.X - 5, (int)player1Car.pos.Y - 7, 10, 14);
                        p2bounds = new Rectangle((int)player2Car.pos.X - 5, (int)player2Car.pos.Y - 7, 10, 14);

                        player1Car.pos += diff * 2f;
                        player2Car.pos -= diff * 2f;
                    }
                    player1Car.pos += diff * (player2Car.currentSpeed * 0.5f);
                    player2Car.pos += diff * (player1Car.currentSpeed * 0.5f);

                }


                foreach(Rectangle wall in wallRects)
                {
                    if(p1bounds.Intersects(wall))
                    {
                        Vector2 diff = player1Car.pos - new Vector2(wall.Center.X, wall.Center.Y);
                        diff.Normalize();
                        while (p1bounds.Intersects(wall))
                        {
                            player1Car.pos += diff * 1f;
                            p1bounds = new Rectangle((int)player1Car.pos.X - 5, (int)player1Car.pos.Y - 7, 10, 14);
                        }
                    }

                    if (p2bounds.Intersects(wall))
                    {
                        Vector2 diff = player2Car.pos - new Vector2(wall.Center.X, wall.Center.Y);
                        diff.Normalize();
                        while (p2bounds.Intersects(wall))
                        {
                            player2Car.pos += diff * 1f;
                            p2bounds = new Rectangle((int)player2Car.pos.X - 5, (int)player2Car.pos.Y - 7, 10, 14);
                        }
                    }

                }

                //foreach (Rectangle slow in slowRects)
                //{
                //    if (p1bounds.Intersects(slow))
                //    {
                //        player1Car.speed = 1f;
                        
                //    }
                //    if(p2bounds.Intersects(slow))
                //    {
                //        player2Car.speed = 1f;
                //    }
                    
                //}

                
                

               


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
