using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GhostEngine.Data;
using GhostEngine.Images;

namespace GhostEngine.Component
{
    public class MainMenu
    {
        private Vector2 PositionInWorld;
        private ContentMenuReader[] MenuReader;
        private ContentManager Content;
        public List<Menu> CurrentStage;
        public String CurrentMenu;
        public String NextMenu;
        private int hSpaceTexture;
        private String FolderContent;

        public void ContentIn(String folder)
        {
            FolderContent = folder;
        }

        public void SpaceTexture(int size)
        {
            hSpaceTexture = size;
        }

        public MainMenu(ContentMenuReader[] contentMenu, ContentManager contentLoad)
        {
            PositionInWorld = Vector2.Zero;
            CurrentMenu = "";
            hSpaceTexture = 38;
            Content = contentLoad;
            MenuReader = contentMenu;
        }

        public MainMenu(String[] list, ContentManager contentLoad, String name)
        {
            MenuReader = new ContentMenuReader[list.Length+1];
            ContentMenuReader tmpMenu = new ContentMenuReader();
            tmpMenu.Name = name;
            tmpMenu.Disable = false;
            tmpMenu.ListString = new List<String>();
            foreach (String tmp in list)
            {
                tmpMenu.ListString.Add(tmp);
            }
            MenuReader[0] = tmpMenu;
            
            for (int key = 0; key < list.Length; ++key)
            {
                tmpMenu = new ContentMenuReader();
                tmpMenu.Name = list[key];
                tmpMenu.Disable = false;
                tmpMenu.ListString = new List<String>();
                MenuReader[key+1] = tmpMenu;
            }            
            PositionInWorld = Vector2.Zero;
            CurrentMenu = "";
            hSpaceTexture = 38;
            Content = contentLoad;
        }

        public String ChangeStage()
        {
            String Status = null;
            List<String> MenuString = this.GetListStringMenu(NextMenu);
            CurrentMenu = NextMenu;

            if (MenuString.Count() != 1)
            {
                CurrentStage = new List<Menu>();
                int iMenu = 0;
                foreach (String Name in MenuString)
                {
                    Texture2D tmpTexture = Content.Load<Texture2D>(FolderContent + "\\" + Name);
                    Vector2 tmpPosition = new Vector2(PositionInWorld.X, PositionInWorld.Y + (hSpaceTexture * iMenu));
                    CurrentStage.Add(new Menu(Name, tmpTexture, tmpPosition, this.StatusMenu(Name)));
                    Status = null;
                    iMenu++;
                }                                   
            }
            else
            {
                List<String> chkString = this.GetListStringMenu(MenuString[0]);
                if (chkString.Count() != 1)
                {
                    this.NextMenu = MenuString[0];
                    Status = null;
                    this.ChangeStage();
                }
                else
                {
                    Status = MenuString[0];
                }                
            }
            return Status;
        }

        private List<String> GetListStringMenu(String nameFind)
        {
            List<String> MenuList = new List<String>();
            foreach (ContentMenuReader tmpString in MenuReader)
            {                
                if (tmpString.Name == nameFind)
                {
                    MenuList = tmpString.ListString;
                }
                
            }
            return MenuList;
        }
        private Boolean StatusMenu(String nameFind)
        {
            Boolean status = false;
            foreach (ContentMenuReader tmpString in MenuReader)
            {
                if (tmpString.Name == nameFind)
                {
                    status = tmpString.Disable;
                }                
            }
            return status;
        }

        public Vector2 WorldPosition
        {
            set { PositionInWorld = value; }
        }
    }
}
