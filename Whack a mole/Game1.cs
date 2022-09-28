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
        Texture2D spriteSheet;

        //rect
        Rectangle sheetRect;

        //Font
        SpriteFont spriteFont;

        //Classes
        Mole mole;

        //Positions
        int posX;
        int posY;
        Vector2 stonePos;
        Vector2 velocity;

        //Arrays
        Mole[,] moleArray;

        //Random
        Random random = new Random();

        //Input
        MouseState mState;
        KeyboardState keyboardState;

        //bools
        bool mRelease = true;
        bool playText = false;

        //timer asset
        double timer = 0;
        double resetTimer = 1;
        int frame;
        double frameTimer = 100;
        double frameInterval = 100;
        double gameTimer = 120;
        

        //gamecount
        int score = 0;
        int lives = 5;
        int round = 0;


        GameState gameState=GameState.start;
        LevelState levelState=LevelState.level1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.PreferredBackBufferWidth = 650;
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
            backGround = Content.Load<Texture2D>("background (1)");
            spriteSheet = Content.Load<Texture2D>("spritesheet_stone");

            stonePos = new Vector2(125, -60);

            velocity = new Vector2(0, 2);


            sheetRect = new Rectangle(0,0, 60, 60);
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    posX = j * 165+65;
                    posY = i * 165+250;

                    moleArray[i,j] = new Mole(moleTex,holeTex,foreTex,moleKOTex,posX,posY);
                }
            }


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            if (gameState == GameState.start)
            {
                startStateUpdate(gameTime);
            }

            if (gameState == GameState.play)
            {
                playStateUpdate(gameTime);

                if (levelState==LevelState.level1)
                {
                    resetTimer = 1;

                }
                if (levelState == LevelState.level2)
                {
                    resetTimer = 0.75;
 
                }
                if (levelState == LevelState.level3)
                {
                    resetTimer = 0.5;
     
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(111,209,72));
            _spriteBatch.Begin();

            if (gameState == GameState.start)
            {
                drawStartState();
            }
            if (gameState == GameState.play)
            {
                drawPlayState();

            }
            if (gameState == GameState.gameOver)
            {
                drawGameoverState();
            }
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        //enum

        enum LevelState
        {
            level1,
            level2,
            level3,
        }
        enum GameState
        {
            start,
            play,
            gameOver,
        }
        public void drawPlayState()
        {
            _spriteBatch.Draw(backGround, new Vector2(0, 0), Color.White);
            for (int i = 0; i < moleArray.GetLength(0); i++)
            {
                for (int j = 0; j < moleArray.GetLength(1); j++)
                {
                    moleArray[i, j].Draw(_spriteBatch);
                }
            }
            _spriteBatch.DrawString(spriteFont, "Score: "+ score.ToString(), new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(spriteFont, "Lives: " + lives.ToString(), new Vector2(540, 0), Color.White);
            _spriteBatch.DrawString(spriteFont, ((int)gameTimer).ToString(), new Vector2(315, 0), Color.White);
        }
        public void drawStartState()
        {
            _spriteBatch.Draw(holeTex, new Vector2(60, 600), Color.White);
            _spriteBatch.Draw(spriteSheet,stonePos,sheetRect, Color.White);
            _spriteBatch.Draw(foreTex, new Vector2(60, 600), Color.White);

            if (playText == true)
            {
                _spriteBatch.DrawString(spriteFont, "Press Enter to play Whack a mole", new Vector2(90, 400), Color.White);
            }

        }
        public void drawGameoverState()
        {
            _spriteBatch.Draw(backGround, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(spriteFont, "Game Over !", new Vector2(240, 400), Color.White);
            _spriteBatch.DrawString(spriteFont, "Yours score was: "+score.ToString(), new Vector2(180, 450), Color.White);
        }
        public void playStateUpdate(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            gameTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= resetTimer)
            {
                timer = 0;
                Random rand = new Random();
                int x = rand.Next(0, 3);
                int y = rand.Next(0, 3);
                moleArray[y, x].moveUpAct(true);

            }

            if (gameTimer <= 0 || lives == 0)
            {
                gameState = GameState.gameOver;

            }


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    moleArray[i, j].Update(gameTime);

                    if (mState.LeftButton == ButtonState.Pressed && mRelease == true && moleArray[i, j].moleRect.Contains(mState.X, mState.Y) && moleArray[i, j].molePos.Y < moleArray[i, j].pos.Y - 50)
                    {
                        mRelease = false;
                        score+=10;
                        moleArray[i, j].gotHit(true);
                    }

                    if (mState.LeftButton == ButtonState.Released)
                    {
                        mRelease = true;
                    }

                    if (moleArray[i,j].lostLife==true)
                    {
                        lives--;
                        moleArray[i,j].lostLife=false;
                    }

                    if (score>=100&&score<200)
                    {
                        levelState = LevelState.level2;
                    }

                    if (score >=200)
                    {
                        levelState=LevelState.level3;
                    }
                }
            }

        }
        public void startStateUpdate(GameTime gameTime)
        {
            stonePos += velocity;
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if(frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                if (frame >= 4)
                {
                    sheetRect.Y =round * 65;
                    round++;
                    frame = 0;
                }

                if (round >= 4)
                {
                    round = 0;
                }

                sheetRect.X = frame * 65;
                
            }
            if(stonePos.Y >= 700)
            {
                velocity = new Vector2(0);
                playText = true;
            }
            if (keyboardState.IsKeyDown(Keys.Enter)&&playText==true)
            {
                gameState = GameState.play;
                playText = false;
            }
        }


    }
}