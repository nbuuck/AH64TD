using System;
using System.IO;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AH64TD
{

    class TextureManager<T>
    {

        private Game runningGame;
        private T[] textures;

        public TextureManager(Game thisGame)
        {
            runningGame = thisGame;
        }

        public bool AddTexture(string strTexturePath)
        {

            if (strTexturePath.Length == 0)
            {
                throw new Exception("TextureManager: Texture path is an empty string.");
            }

            if (!File.Exists(strTexturePath))
            {
                throw new Exception("TextureManager: The specified texture doesn't exist.");
            }

            T txCurrent = runningGame.Content.Load<T>(strTexturePath);


            return true;
        }

    }

}