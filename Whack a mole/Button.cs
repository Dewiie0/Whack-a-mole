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
    internal class Button
    {
        Texture2D texture;
        Vector2 pos;
        Rectangle buttonRect;

        public Button(Texture2D texture, Vector2 pos, Rectangle buttonRect)
        {
            this.texture = texture;
            this.pos = pos;
        }

        public void Update()
        {
            buttonRect=new Rectangle((int)pos.X,(int)pos.Y,texture.Width,texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, Color.White);
        }
    }
}
