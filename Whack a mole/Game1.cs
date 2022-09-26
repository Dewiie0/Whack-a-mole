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
        Texture2D moleKOTex;

        //Font
        SpriteFont spriteFont;

        //Classes
        Mole mole;

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

        //timer asset
        double timer = 0;
        double resetTimer = 2;

        //gamecount
        int score = 0;
        int lives = 5;


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
            moleKOTex = Content.Load<Texture2D>("mole_KO (1)");
            spriteFont = Content.Load<SpriteFont>("galleryFont");

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

            if (gameState == GameState.start)
            {
                startStateUpdate();
            }

            if (gameState == GameState.play)
            {
                playStateUpdate(gameTime);
            }

            if (gameState == GameState.gameOver)
            {
                gameoverStateUpdate();
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
            _spriteBatch.DrawString(spriteFont, "Score: "+ score.ToString(), new Vector2(0, 0), Color.White);

        }
        public void drawStartState()
        {

        }
        public void drawGameoverState()
        {

        }
        public void playStateUpdate(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= resetTimer)
            {
                Random rand = new Random();
                int x = rand.Next(0, 3);
                int y = rand.Next(0, 3);
                moleArray[y, x].moveUpAct(true);
                timer = 0;

            }


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    moleArray[i, j].Update(gameTime);

                    if (mState.LeftButton == ButtonState.Pressed && mRelease == true && moleArray[i, j].moleRect.Contains(mState.X, mState.Y) && moleArray[i, j].molePos.Y < moleArray[i,j].pos.Y-100)
                    {
                        mRelease = false;
                        score++;
                        moleArray[i, j].gotHit(true);
                    }

                    if (mState.LeftButton == ButtonState.Released)
                    {
                        mRelease = true;
                    }



                }
            }

        }
        public void startStateUpdate()
        {

        }
        public void gameoverStateUpdate()
        {

        }


    }
}