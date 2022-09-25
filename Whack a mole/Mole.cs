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
        public Texture2D tex;
        int posX;
        int posY;
        public Vector2 pos;
        public Rectangle moleRect;
        public Vector2 velocity;

        public Mole(Texture2D tex, int posX,int posY)
        {
            this.tex = tex;
            this.pos = new Vector2(posX, posY);
        }

        public void Update()
        {
            moleRect=new Rectangle((int)posX,(int)posY, tex.Width, tex.Height);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
