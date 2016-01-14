using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GhostEngine
{
    public class Menu
    {
        String Name;
        Boolean Selected = true;
        Texture2D Texture;
        Rectangle Size;
        Vector2 Position;

        public Menu(String name, Texture2D image, Vector2 position)
        {
            Name = name;
            Texture = image;
            Size = new Rectangle(0, 0, (image.Width / 2), image.Height);
            Position = position;
        }
        public Menu(String name, Texture2D image)
        {
            Name = name;
            Texture = image;
            Size = new Rectangle(0, 0, (image.Width / 2), image.Height);
            Position = Vector2.Zero;
        }
        public void Disabled() { Selected = false; }
        public void Undisabled() { Selected = true; }
    }
}
