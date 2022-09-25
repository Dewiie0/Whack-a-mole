using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Whack_a_mole
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Textures
        Texture2D holeTex;
        Texture2D foreTex;
        Texture2D backGround;
        Texture2D moleTex;

        //Classes
        Mole mole;
        Hole hole;

        //Positions

        int posX;
        int posY;

        //Arrays
        Mole[,] moleArray;
        Hole[,] holeArray;
        Foreground[,] foregroundArray;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.PreferredBackBufferWidth = 800;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            moleArray = new Mole[3,3];
            holeArray = new Hole[3, 3];
            foregroundArray = new Foreground[3, 3];


            moleTex = Content.Load<Texture2D>("mole");
            holeTex = Content.Load<Texture2D>("hole (1)");
            foreTex = Content.Load<Texture2D>("hole_foreground");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    posX = j * 200+105;
                    posY = i * 240+125;

                    moleArray[i,j] = new Mole(moleTex,posX,posY);
                }
            }

            for (int l = 0; l < 3; l++)
            {
                for (int o = 0; o < 3; o++)
                {
                    posX = o * 200 + 105;
                    posY = l * 240+150;

                    holeArray[l, o] = new Hole(holeTex, posX, posY);
                }
            }

            for (int f = 0; f < 3; f++)
            {
                for (int g = 0; g < 3; g++)
                {
                    posX = g * 200 + 105;
                    posY = f * 240 + 150;

                    foregroundArray[f,g]=new Foreground(foreTex,posX, posY);
                }
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LimeGreen);
            _spriteBatch.Begin();
            for (int l = 0; l < holeArray.GetLength(0); l++)
            {
                for (int o = 0; o < holeArray.GetLength(1); o++)
                {
                    holeArray[l, o].Draw(_spriteBatch);
                }
            }
            for (int i = 0; i < moleArray.GetLength(0); i++)
            {
                for (int j = 0; j < moleArray.GetLength(1); j++)
                {
                    moleArray[i, j].Draw(_spriteBatch);
                }
            }
            for (int f = 0; f < foregroundArray.GetLength(0); f++)
            {
                for (int g = 0; g < foregroundArray.GetLength(1); g++)
                {
                    foregroundArray[f, g].Draw(_spriteBatch);
                }
            }


            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}