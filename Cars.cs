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
            else
            {
                spriteBatch.Draw(Texture, Position, Tint);
            }
        }














}
}
