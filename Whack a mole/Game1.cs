using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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

        // Random
        Random random = new Random();

        //Timer
        MouseState mState;

        //bools
        bool mRelease = true;
        public bool moveTheMole = false;

        //timer asset
        double timer = 0;
        double resetTimer = 1;

        GameState gameState=GameState.play;

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



            moleTex = Content.Load<Texture2D>("mole");
            holeTex = Content.Load<Texture2D>("hole (1)");
            foreTex = Content.Load<Texture2D>("hole_foreground");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    posX = j * 200+110;
                    posY = i * 220+250;

                    moleArray[i,j] = new Mole(moleTex,holeTex,foreTex,posX,posY);
                }
            }


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mState = Mouse.GetState();
            timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (gameState == GameState.play)
            {
                playStateUpdate(gameTime);
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LimeGreen);
            _spriteBatch.Begin();
            if (gameState == GameState.play)
            {
                drawPlayState();

            }
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        //enum
        enum GameState
        {
            start,
            play,
            gameOver,
        }
        public void drawPlayState()
        {

            for (int i = 0; i < moleArray.GetLength(0); i++)
            {
                for (int j = 0; j < moleArray.GetLength(1); j++)
                {
                    moleArray[i, j].Draw(_spriteBatch);
                }
            }
            

        }
        public void playStateUpdate(GameTime gameTime)
        {
            if (timer >= resetTimer)
            {
                timer = 0;
                mole.moveUpAct(true);
                
            }


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    moleArray[i, j].Update();

                    if (mState.LeftButton == ButtonState.Pressed && mRelease == true && moleArray[i, j].moleRect.Contains(mState.X, mState.Y))
                    {
                        mRelease = false;
                    }

                    if (mState.LeftButton == ButtonState.Released)
                    {
                        mRelease = true;
                    }

                }
            }

        }


    }
}