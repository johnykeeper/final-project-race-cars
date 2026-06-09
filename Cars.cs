using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace final_project__race_cars
{
    public class Cars
{

        public Texture2D Texture;
        public Rectangle Position;
        public Color Tint;
        public float scale;
        public bool IsSelected;
        public Vector2 velocity;
        public Vector2 pos;
        public float speed = 3f;
        public float rotationAngle = 0f;
        public bool OnTrack = false;
        public float currentSpeed = 0f;

        public int lapCount = 0;

        public bool crossedStart = false;
        public bool crossedEnd = false;
        public bool touchingStart = false;
        public bool touchingEnd = false;

        public float currentLaptime = 0f;
        public float totalLaptime = 0f;
        public float averageLaptime = 0f;

        public string colorName = "";

        Cars firstplace;
        Cars secondplace;

        public Cars(Texture2D texture, Rectangle position)
        {
            Texture = texture;
            Position = position;
            Tint = Color.White;
            scale = 1.0f;
            IsSelected = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsSelected)
            {
                int newWidth = (int)(Position.Width * 1.2f);
                int newHeight = (int)(Position.Height * 1.2f);
                int newX = Position.X - (newWidth - Position.Width) / 2;
                int newY = Position.Y - (newHeight - Position.Height) / 2;
                spriteBatch.Draw(Texture, new Rectangle(newX, newY, newWidth, newHeight), Tint);
            }
            else if(OnTrack)
            {
                Vector2 origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
                Vector2 drawpos = pos;
                spriteBatch.Draw(Texture, drawpos, null, Tint, rotationAngle, origin, 0.02f, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(Texture, Position, Tint);
            }
        }














}
}
