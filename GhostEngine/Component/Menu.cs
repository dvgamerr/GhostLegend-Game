using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GhostEngine.Component
{
    public class Menu
    {
        public String NameStage;
        public Boolean HoverMenu;
        public Boolean DisableMenu;
        public Texture2D NameTexture;
        public Rectangle SizeTexture;
        public Vector2 PositionTexture;

        public Menu(String name, Texture2D texture,Vector2 position, Boolean disable)
        {
            NameStage = name;           
            NameTexture = texture;
            PositionTexture = position;
            this.DisableMenu = disable;
            this.UnSelected();

            // รูปภาพสัดส่วนไม่เท่ากัน
            if ((texture.Width % 2) != 0)
                Console.WriteLine(name + ": Texture sizes are not equal.");            
        }

        public void Selected()
        {
            SizeTexture = new Rectangle(((int)NameTexture.Width / 2), 0, ((int)NameTexture.Width / 2), (int)NameTexture.Height);
            this.HoverMenu = true;
        }

        public void UnSelected()
        {            
            SizeTexture = new Rectangle(0, 0, ((int)NameTexture.Width / 2), (int)NameTexture.Height);
            this.HoverMenu = false;
        }

    }
}
