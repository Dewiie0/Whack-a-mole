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
    internal class Foreground
    {
        Texture2D tex;
        int x;
        int y;
        Vector2 pos;

        public Foreground(Texture2D tex, int x, int y)
        {
            this.tex = tex;
            this.pos = new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.LimeGreen);
        }
    }
}