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
        Texture2D mallet;
        Texture2D heartTex;

        //Rect
        Rectangle sheetRect;

        //Font
        SpriteFont spriteFont;

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
        bool isRotate = false;
        bool isRotateBack;

        //timer asset
        double timer = 0;
        double resetTimer = 1;
        int frame;
        double frameTimer = 100;
        double frameInterval = 100;
        double gameTimer = 120;

        //rotation count
        double rotationAngle;
        float circle;
        Vector2 origin;

        //gamecount
        int score = 0;
        int lives = 5;
        int round = 0;
        int streaks = 0;
        int moleHP;
        int moleType;


        GameState gameState=GameState.play;
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
            mallet = Content.Load<Texture2D>("mallet");
            heartTex = Content.Load<Texture2D>("Heart");

            stonePos = new Vector2(125, -60);
            velocity = new Vector2(0, 2);

            sheetRect = new Rectangle(0,0, 60, 60);

            origin.X = mallet.Width / 2 + 40;
            origin.Y = mallet.Height / 2;
            rotationAngle = 0;
            circle = MathHelper.Pi * 2;

            randomMole();

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
                    resetTimer = 2;

                }
                if (levelState == LevelState.level2)
                {
                    resetTimer = 1;
 
                }
                if (levelState == LevelState.level3)
                {
                    resetTimer = 0.5;
     
                }
            }
            if (gameState == GameState.gameOver)
            {
                IsMouseVisible = true;
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

        //Draw
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
            _spriteBatch.DrawString(spriteFont, "Score: "+ score.ToString(), new Vector2(0, 0), Color.Yellow);
            _spriteBatch.DrawString(spriteFont, ((int)gameTimer).ToString(), new Vector2(315, 0), Color.Yellow);
            _spriteBatch.DrawString(spriteFont,streaks.ToString()+" /10",new Vector2(580,0), Color.Yellow);
            _spriteBatch.Draw(mallet,new Vector2(mState.X+130,mState.Y),null, Color.White,(float)rotationAngle,origin,1.0f,SpriteEffects.None,0f);
            drawHeart(lives);

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

        //Update
        public void playStateUpdate(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            gameTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            rotationAngle = rotationAngle % circle;
            IsMouseVisible = true;


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

                    if (mState.LeftButton == ButtonState.Pressed && mRelease == true && moleArray[i, j].moleRect.Contains(mState.X, mState.Y) && moleArray[i, j].molePos.Y < moleArray[i, j].pos.Y - 60 && moleArray[i,j].molehit==false)
                    {
                        mRelease = false;
                        moleArray[i,j].moleHP--;

                        if (moleArray[i,j].moleHP == 0)
                        {
                            moleArray[i, j].gotHit(true);
                            streaks++;
                            score += 10;
                        }

                    }

                    if (mState.LeftButton == ButtonState.Released)
                    {
                        mRelease = true;
                    }

                    if (moleArray[i,j].lostLife==true)
                    {
                        lives--;
                        streaks=0;  
                        moleArray[i,j].lostLife=false;
                    }

                    if (streaks >= 10)
                    {
                        if (lives < 5)
                        {
                            lives++;
                        }
                        streaks = 0;
                    }

                    if (score>=100&&score<200)
                    {
                        levelState = LevelState.level2;
                    }

                    if (score >=200)
                    {
                        levelState=LevelState.level3;
                    }


                    malletRotate(gameTime);
                    resetHP(moleArray);

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
        public void malletRotate(GameTime gameTime)
        {

            if (mState.LeftButton == ButtonState.Pressed)
            {
                isRotate = true;
            }


            if (isRotate == true)
            {
                rotationAngle += gameTime.ElapsedGameTime.TotalSeconds;
                if (rotationAngle >= 1)
                {
                    isRotateBack = true;
                }

            }

            if (isRotateBack == true)
            {
                rotationAngle -= gameTime.ElapsedGameTime.TotalSeconds;
                isRotate = false;

                if (rotationAngle <= 0)
                {
                    isRotateBack = false;
                }
            }
        }
        public void drawHeart(int lives)
        {
            if (lives == 5)
            {
                _spriteBatch.Draw(heartTex, new Vector2(0, 60), Color.White);
                _spriteBatch.Draw(heartTex, new Vector2(50, 60), Color.White);
                _spriteBatch.Draw(heartTex, new Vector2(100, 60), Color.White);
                _spriteBatch.Draw(heartTex, new Vector2(150, 60), Color.White);
                _spriteBatch.Draw(heartTex, new Vector2(200, 60), Color.White);
            }

            if (lives == 4)
            {
                _spriteBatch.Draw(heartTex, new Vector2(0, 60), Color.White);
                _spriteBatch.Draw(heartTex, new Vector2(50, 60), Color.White);
                _spriteBatch.Draw(heartTex, new Vector2(100, 60), Color.White);
                _spriteBatch.Draw(heartTex, new Vector2(150, 60), Color.White);
            }

            if (lives == 3)
            {
                _spriteBatch.Draw(heartTex, new Vector2(0, 60), Color.White);
                _spriteBatch.Draw(heartTex, new Vector2(50, 60), Color.White);
                _spriteBatch.Draw(heartTex, new Vector2(100, 60), Color.White);
            }

            if (lives == 2)
            {
                _spriteBatch.Draw(heartTex, new Vector2(0, 60), Color.White);
                _spriteBatch.Draw(heartTex, new Vector2(50, 60), Color.White);
            }

            if (lives == 1)
            {
                _spriteBatch.Draw(heartTex, new Vector2(0, 60), Color.White);
            }
            if (lives == 0)
            {

            }
        }
        public void randomMole()
        {
            for (int i = 0; i < moleArray.GetLength(0); i++)
            {
                for (int j = 0; j < moleArray.GetLength(1); j++)
                {
                    posX = j * 165 + 65;
                    posY = i * 165 + 250;
                    moleType = random.Next(0, 10);

                    if (moleType >= 0&&moleType<=4)
                    {
                        moleHP = 1;
                        moleArray[i, j] = new Mole(moleTex, holeTex, foreTex, moleKOTex, posX, posY,Color.White,moleHP);
                    }

                    if (moleType >4&&moleType<=6)
                    {
                        moleHP = 2;
                        moleArray[i, j] = new Mole(moleTex, holeTex, foreTex, moleKOTex, posX, posY, Color.Red,moleHP);
                    }

                    if (moleType >6&&moleType<=8)
                    {
                        moleHP = 3;
                        moleArray[i, j] = new Mole(moleTex, holeTex, foreTex, moleKOTex, posX, posY, Color.Coral,moleHP);
                    }

                    if (moleType >8&&moleType<=10)
                    {
                        moleHP = 4;
                        moleArray[i, j] = new Mole(moleTex, holeTex, foreTex, moleKOTex, posX, posY, Color.HotPink,moleHP);
                    }

                }
            }
        }
        internal void resetHP(Mole[,] moles)
        {
            for (int i = 0; i < moleArray.GetLength(0); i++)
            {
                for (int j = 0; j < moleArray.GetLength(1); j++)
                {

                    if (moleArray[i, j].getColor()==Color.White)
                    {
                        moleArray[i, j].resetHP(1);
                    }

                    if (moleArray[i, j].getColor() == Color.Red)
                    {
                        moleArray[i, j].resetHP(2);
                    }

                    if (moleArray[i, j].getColor() == Color.Coral)
                    {
                        moleArray[i, j].resetHP(3);
                    }

                    if (moleArray[i, j].getColor() == Color.HotPink)
                    {
                        moleArray[i, j].resetHP(4);
                    }
                    
                }
            }
        }
        

    }
}