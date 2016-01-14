using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace GhostEngine.Images
{
    public class TextureSprite
    {
        private Texture2D Texture;
        private Vector2 WorldInPosition;
        private Rectangle TextureReSize;
        private Color TextureColor;
        private int TextureType;
        public Vector2 Position
        {
            get { return WorldInPosition; }
            set { WorldInPosition = value; }
        }

        public Rectangle TextureSize
        {
            get { return TextureReSize; }
            set { TextureReSize = value; }
        }

        public TextureSprite(Texture2D texture2d, Vector2 position)
        {
            TextureType = 1;
            Texture = texture2d;
            WorldInPosition = position;
            TextureReSize = new Rectangle(0, 0, texture2d.Width, texture2d.Height);
            TextureColor = Color.White;
        }

        public TextureSprite(Texture2D texture2d, Rectangle size, Color color)
        {
            TextureType = 2;
            Texture = texture2d;
            WorldInPosition = Vector2.Zero;
            TextureReSize = size;
            TextureColor = color;
        }

        public TextureSprite(Texture2D texture2d, Vector2 position, Rectangle size, Color color)
        {
            TextureType = 3;
            Texture = texture2d;
            WorldInPosition = position;
            TextureReSize = size;
            TextureColor = color;
        }


        public void Draw(SpriteBatch renderer, Vector2 drawPosition)
        {
            if (TextureType == 1)
            {
                renderer.Draw(Texture, drawPosition, TextureColor);
            }
            else if (TextureType == 2)
            {
                renderer.Draw(Texture, TextureReSize, TextureColor);
            }
            else if (TextureType == 3)
            {
                renderer.Draw(Texture, drawPosition, TextureReSize, TextureColor);
            }            
        }

    }
}
