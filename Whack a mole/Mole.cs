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
        Texture2D KO;

        int posX;
        int posY;
        int velocityY;
        public int moleType;
        public int moleHP;
        int resetMoleHP;
        bool moveUpActive=false;
        public bool molehit=false;
        public double timer = 0;
        double timerReset;
        public bool lostLife = false;

        public Vector2 pos;
        public Vector2 molePos;
        public Vector2 velocity;
        public Rectangle moleRect;

        Random random = new Random();

        Color color;

        public Mole(Texture2D moleTex, Texture2D holeTex, Texture2D foreTex,Texture2D KO,int posX,int posY,Color color,int moleHP)
        {
            this.moleTex = moleTex;
            this.holeTex = holeTex;
            this.foreTex = foreTex; 
            this.KO = KO;
            this.moleHP = moleHP;
            this.posX = posX;
            this.posY = posY;
            this.pos = new Vector2(posX, posY);
            this.molePos = new Vector2(posX, posY);
            this.color = color;

        }

        public void Update(GameTime gameTime)
        {
            moleRect = new Rectangle((int)molePos.X,(int)molePos.Y, moleTex.Width, moleTex.Height);
            molePos += velocity;
            timerReset = random.Next(2, 4);

            if (moveUpActive)
            {
                random = new Random();
                moleHP = resetMoleHP;
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

            if (molePos.Y >= posY&&velocity==new Vector2(0,3)&& molehit == false)
            {
                velocity = new Vector2(0, 0);
                lostLife = true;

            }

            if (molePos.Y >= posY && velocity == new Vector2(0, 3) && molehit == true)
            {
                velocity = new Vector2(0, 0);
                molehit = false;

            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(holeTex, pos, Color.White);          
            spriteBatch.Draw(moleTex,molePos,color);
            if (molehit)
            {
                spriteBatch.Draw(KO, molePos, color);
            }
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
        public void getMoleType(int getMoleType)
        {
            moleType = getMoleType;
        }
        public void resetHP(int hp)
        {
            resetMoleHP = hp;
        }
        public Color getColor()
        {
            return color;
        }

    }
}
