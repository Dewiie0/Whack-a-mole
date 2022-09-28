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
        int velocityY;
        bool moveUpActive=false;
        bool molehit=false;
        public double timer = 0;
        double timerReset;

        public Vector2 pos;
        public Vector2 molePos;
        public Vector2 velocity;
        public Rectangle moleRect;

        Random random = new Random();

        public Mole(Texture2D moleTex, Texture2D holeTex, Texture2D foreTex,int posX,int posY)
        {
            this.moleTex = moleTex;
            this.holeTex = holeTex;
            this.foreTex = foreTex;
            this.posX = posX;
            this.posY = posY;
            this.pos = new Vector2(posX, posY);
            this.molePos = new Vector2(posX, posY);

        }

        public void Update(GameTime gameTime)
        {
            moleRect = new Rectangle((int)molePos.X,(int)molePos.Y, moleTex.Width, moleTex.Height);
            molePos += velocity;
            timerReset = random.Next(2, 4);
            
            if (moveUpActive)
            {
                random = new Random();

                velocityY = random.Next(-10, -4);
                velocity = new Vector2(0,velocityY);
            }

            if (molePos.Y <= posY-160)
            {
                velocity = new Vector2(0, 0);
                moveUpActive = false;

                timer += gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= timerReset)
                {
                    velocity = new Vector2(0, 3);
                    timer= 0;
                }
            }

            if (molehit)
            {
                velocity = new Vector2(0, 3);
            }

            if (molePos.Y >= posY&&velocity==new Vector2(0,3))
            {
                velocity = new Vector2(0, 0);
                molehit = false;
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
            moveUpActive = moveTheMole;
        }
        public void gotHit(bool hit)
        {
            molehit = hit;
        }
    }
}
