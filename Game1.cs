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
        Cars firstplace;
        Cars secondplace;

        SpriteFont font;

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
        Rectangle finishLinestart = new Rectangle(390, 300, 6, 77);
        
        Rectangle finishLineend = new Rectangle(402, 297, 7, 79);

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
            new Rectangle(152, 59, 130, 4),
            new Rectangle(272, 61, 21, 9),
            new Rectangle(281, 70, 20, 7),
            new Rectangle(290, 73, 217, 5),
            new Rectangle(506, 57, 19, 20),
            new Rectangle(525, 58, 107, 4),
            new Rectangle(630, 63, 19, 6),
            new Rectangle(648, 69, 14, 12),
            new Rectangle(660, 79, 15, 18),
            new Rectangle(672, 94, 10, 20),
            new Rectangle(679, 110, 8, 219),
            new Rectangle(676, 321, 6, 17),
            new Rectangle(671, 335, 11, 21),
            new Rectangle(667, 346, 13, 20),
            new Rectangle(657, 354, 16, 18),
            new Rectangle(650, 366, 14, 17),
            new Rectangle(638, 368, 16, 16),
            new Rectangle(621, 374, 21, 13),
            new Rectangle(174, 377, 446, 15),
            new Rectangle(134, 373, 40, 11),
            new Rectangle(102, 362, 60, 9),
            new Rectangle(102, 353, 48, 12),
            new Rectangle(103, 339, 34, 15),
            new Rectangle(99, 321, 29, 16),
            new Rectangle(112, 107, 9, 213),
            new Rectangle(119, 85, 11, 27),
            new Rectangle(130, 69, 13, 27),
            new Rectangle(142, 58, 23, 24),
            new Rectangle(163, 58, 18, 8),
            new Rectangle(204, 144, 35, 13),
            new Rectangle(235, 147, 12, 22),
            new Rectangle(248, 154, 11, 25),
            new Rectangle(256, 159, 286, 16),
            new Rectangle(539, 154, 15, 12),
            new Rectangle(550, 149, 13, 8),
            new Rectangle(557, 145, 40, 15),
            new Rectangle(586, 160, 10, 136),
            new Rectangle(201, 286, 387, 9),
            new Rectangle(202, 151, 6, 135)
             };
            slowRects = new List<Rectangle>
            {
                new Rectangle(124, 76, 16, 245),
                new Rectangle(164, 64, 110, 16),
                new Rectangle(145, 73, 15, 9),
                new Rectangle(139, 80, 7, 15),
                new Rectangle(150, 80, 11, 9),
                new Rectangle(275, 73, 11, 14),
                new Rectangle(283, 78, 231, 15),
                new Rectangle(511, 76, 11, 14),
                new Rectangle(521, 69, 9, 13),
                new Rectangle(530, 63, 100, 14),
                new Rectangle(630, 70, 14, 15),
                new Rectangle(643, 78, 13, 17),
                new Rectangle(656, 95, 13, 19),
                new Rectangle(663, 117, 13, 211),
                new Rectangle(659, 329, 8, 11),
                new Rectangle(652, 337, 11, 14),
                new Rectangle(645, 351, 7, 7),
                new Rectangle(633, 357, 11, 7),
                new Rectangle(618, 360, 16, 8),
                new Rectangle(189, 361, 427, 14),
                new Rectangle(164, 363, 24, 13),
                new Rectangle(144, 348, 19, 16),
                new Rectangle(128, 319, 11, 25),
                new Rectangle(206, 293, 389, 18),
                new Rectangle(595, 287, 12, 26),
                new Rectangle(600, 138, 8, 148),
                new Rectangle(557, 129, 42, 12),
                new Rectangle(545, 129, 12, 19),
                new Rectangle(532, 144, 10, 9),
                new Rectangle(265, 148, 265, 15),
                new Rectangle(252, 140, 13, 23),
                new Rectangle(240, 132, 12, 11),
                new Rectangle(195, 129, 44, 15),
                new Rectangle(190, 141, 10, 160),
                new Rectangle(195, 300, 14, 7)

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
            font = Content.Load<SpriteFont>("font");




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
                Rectangle p1bounds = new Rectangle((int)player1Car.pos.X - 5, (int)player1Car.pos.Y - 7, 10, 14);
                Rectangle p2bounds = new Rectangle((int)player2Car.pos.X - 5, (int)player2Car.pos.Y - 7, 10, 14);



                if (kb.IsKeyDown(Keys.A))
                    player1Car.rotationAngle -= 0.05f;
                if (kb.IsKeyDown(Keys.D))
                    player1Car.rotationAngle += 0.05f;
                if (kb.IsKeyDown(Keys.W))
                {

                    float moveX = (float)Math.Cos(player1Car.rotationAngle - MathHelper.PiOver2) * player1Car.speed;
                    float moveY = (float)Math.Sin(player1Car.rotationAngle - MathHelper.PiOver2) * player1Car.speed;

                    player1Car.pos.X += moveX;

                    p1bounds = new Rectangle((int)player1Car.pos.X - 5, (int)player1Car.pos.Y - 7, 10, 14);

                    foreach (Rectangle wall in wallRects)
                    {
                        if(p1bounds.Intersects(wall))
                        {
                            player1Car.pos.X -= moveX;
                            break;
                        }

                    }
                    player1Car.pos.Y += moveY;

                    p1bounds = new Rectangle((int)player1Car.pos.X - 5, (int)player1Car.pos.Y - 7, 10, 14);

                    foreach (Rectangle wall in wallRects)
                    {
                        if (p1bounds.Intersects(wall))
                        {
                            player1Car.pos.Y -= moveY;
                            break;
                        }

                    }

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
                    float moveX = (float)Math.Cos(player2Car.rotationAngle - MathHelper.PiOver2) * player2Car.speed;
                    float moveY = (float)Math.Sin(player2Car.rotationAngle - MathHelper.PiOver2) * player2Car.speed;

                    player2Car.pos.X += moveX;

                    p2bounds = new Rectangle((int)player2Car.pos.X - 5, (int)player2Car.pos.Y - 7, 10, 14);

                    foreach (Rectangle wall in wallRects)
                    {
                        if(p2bounds.Intersects(wall))
                        {
                            player2Car.pos.X -= moveX;
                            break;
                        }

                    } 
                    player2Car.pos.Y += moveY;

                    p2bounds = new Rectangle((int)player2Car.pos.X - 5, (int)player2Car.pos.Y - 7, 10, 14);

                    foreach (Rectangle wall in wallRects)
                    {
                        if (p2bounds.Intersects(wall))
                        {
                            player2Car.pos.Y -= moveY;
                            break;
                        }

                    }

                }
                else
                {
                    player2Car.currentSpeed = 0f;
                }

                p1bounds = new Rectangle((int)player1Car.pos.X - 5, (int)player1Car.pos.Y - 7, 10, 14);
                p2bounds = new Rectangle((int)player2Car.pos.X - 5, (int)player2Car.pos.Y - 7, 10, 14);



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


                player1Car.speed = 3f;
                player2Car.speed = 3f;

                foreach (Rectangle slow in slowRects)
                {
                    if (p1bounds.Intersects(slow))
                    {
                        player1Car.speed = 1.5f;

                    }
                    if (p2bounds.Intersects(slow))
                    {
                        player2Car.speed = 1.5f;
                    }

                }
                player1Car.currentLaptime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                player2Car.currentLaptime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if(p1bounds.Intersects(finishLineend))
                {
                    if(!player1Car.touchingEnd)
                    {
                        player1Car.touchingEnd = true;
                        player1Car.crossedEnd = true;
                    }
                    else { player1Car.touchingEnd = false;}
                    
                }
                if(p1bounds.Intersects(finishLinestart))
                {
                    if (!player1Car.touchingEnd)
                    {
                        player1Car.touchingStart = true;
                        if (player1Car.crossedEnd)
                        {
                            player1Car.lapCount++;
                            player1Car.totalLaptime += player1Car.totalLaptime / player1Car.lapCount;
                            player1Car.currentLaptime = 0f;
                            player1Car.crossedEnd = false;
                        }
                    }
                    else
                    {
                        player1Car.touchingStart = false;
                    }
                }
                if (p2bounds.Intersects(finishLineend))
                {
                    if (!player2Car.touchingEnd)
                    {
                        player2Car.touchingEnd = true;
                        player2Car.crossedEnd = true;
                    }
                    else { player2Car.touchingEnd = false; }
                }
                if (p2bounds.Intersects(finishLinestart))
                {
                    if (!player2Car.touchingEnd)
                    {
                        player2Car.touchingStart = true;
                        if (player2Car.crossedEnd)
                        {
                            player2Car.lapCount++;
                            player2Car.totalLaptime += player2Car.totalLaptime / player2Car.lapCount;
                            player2Car.currentLaptime = 0f;
                            player2Car.crossedEnd = false;
                        }
                    }
                    else
                    {
                        player2Car.touchingStart = false;
                    }
                }

                

                if(player1Car.lapCount > player2Car.lapCount)
                {
                    firstplace = player1Car;
                    secondplace = player2Car;
                }
                else if (player2Car.lapCount > player1Car.lapCount)
                {
                    firstplace = player2Car;
                    secondplace = player1Car;
                }
                else
                {
                    if(player1Car.currentLaptime < player2Car.currentLaptime)
                    {
                        firstplace = player1Car;
                        secondplace = player2Car;
                    }

                    else
                    {
                        firstplace = player2Car;
                        secondplace = player1Car;
                    }
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

                _spriteBatch.DrawString(font, $"1st: {firstplace.colorName}", new Vector2(600, 20), Color.White);
                _spriteBatch.DrawString(font, $"2nd: {secondplace.colorName}", new Vector2(600, 50), Color.White);

            }



                _spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
