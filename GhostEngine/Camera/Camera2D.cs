using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GhostEngine.Images;
 
namespace GhostEngine.Camera
{
    public class Camera2D
    {
        private SpriteBatch CameraRenderer;
        private Vector2 CameraPosition; // top left corner of the camera
        public Vector2 CameraDefault;
        public Vector2 World;

        public Vector2 Position
        {
            get { return CameraPosition; }
            set { CameraPosition = value; }
        }

        public Camera2D(SpriteBatch renderer)
        {
            CameraRenderer = renderer;
            CameraPosition = new Vector2(0, 0);
        }

        public void DrawNode(TextureSprite node)
        {
            Vector2 DrawPosition = ApplyTransformations(node.Position);
            node.Draw(CameraRenderer, DrawPosition);
        }

        /*public void DrawNode(SpriteFont node,String text)
        {
            Vector2 DrawPosition = ApplyTransformations(node.Position);
            node.Draw(CameraRenderer, DrawPosition);
        }*/

        private Vector2 ApplyTransformations(Vector2 nodePosition)
        {
            Vector2 finalPosition = nodePosition - CameraPosition;
            // you can apply scaling and rotation here also

            //--------------------------------------------
            return finalPosition;
        }

        public void Translate(Vector2 moveVector, bool moveNode)
        {
            if (moveNode)
            {
                CameraPosition += moveVector;
            }
            else
            {
                CameraPosition = moveVector;
            }
        }

        public void CapPosition(GraphicsDeviceManager graphics)
        {
            if (CameraPosition.X > World.X - graphics.GraphicsDevice.Viewport.Width)
            {
                CameraPosition.X = World.X - graphics.GraphicsDevice.Viewport.Width;
            }
            if (CameraPosition.X < 0)
            {
                CameraPosition.X = 0;
            }
            if (CameraPosition.Y > World.Y - graphics.GraphicsDevice.Viewport.Height)
            {
                CameraPosition.Y = World.Y - graphics.GraphicsDevice.Viewport.Height;
            }
            if (CameraPosition.Y < 0)
            {
                CameraPosition.Y = 0;
            }
        }
    }
}
