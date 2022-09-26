using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Whack_a_mole
{
    internal class Mole
    {
        Texture2D moleTex;
        Texture2D holeTex;
        Texture2D foreTex;

        int posX;
        int posY;
        bool moveActive;

        Vector2 pos;
        public Vector2 molePos;
        public Rectangle moleRect;

        public Mole(Texture2D moleTex, Texture2D holeTex, Texture2D foreTex,int posX,int posY)
        {
            this.moleTex = moleTex;
            this.holeTex = holeTex;
            this.foreTex = foreTex;
            this.posX = posX;
            this.posY = posY;
            this.pos = new Vector2(posX, posY);
            this.molePos = new Vector2(posX, posY);
            moleRect = new Rectangle(posX,posY, moleTex.Width, moleTex.Height);

        }

        public void Update()
        {
            moleRect.X = posX;
            moleRect.Y = posY;

            if (moveActive && molePos.Y < pos.Y - 50)
            {
                moleRect.Y -= 4;
            }
            
            
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(holeTex, pos, Color.White);
            spriteBatch.Draw(moleTex, molePos, Color.White);
            spriteBatch.Draw(foreTex, pos, Color.White);
        }

        public void moveUpAct(bool moveTheMole)
        {
            moveActive = moveTheMole;
        }

    }
}
