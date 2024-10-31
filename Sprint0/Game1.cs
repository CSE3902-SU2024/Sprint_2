using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Collisions;
using Sprint2.GameStates;





namespace Sprint0
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;    
        private IGameState CurrentGameState;
        public Vector2 _scale;
        KeyboardState previousKeyboardState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1020;
            _graphics.PreferredBackBufferHeight = 920;
          //  _graphics.IsFullScreen = true;
            _graphics.ApplyChanges(); 
        }

        protected override void Initialize()
        {

            //DEBUG FOR ENEMY HITBOXES
            DebugDraw.Initialize(GraphicsDevice);
            base.Initialize();

        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content";
            _scale.X = (float)GraphicsDevice.Viewport.Width / 256.0f;
            _scale.Y = (float)GraphicsDevice.Viewport.Height / 230.0f;
            CurrentGameState = new LevelOne(_graphics, _spriteBatch, _scale, GraphicsDevice);
            CurrentGameState.LoadContent(Content);
               
        }


        public void Reset()
        {


            //Rectangle[] linkFrames = _linkSpriteFactory.CreateFrames();

            ////link texture
            //Texture2D linkTexture = Content.Load<Texture2D>("LinkSpriteSheet2");


            ////link instance
            //_link = new Link(linkFrames, linkTexture, GraphicsDevice);
            //_keyboardController = new KeyboardController(_link, _StageManager);
            ////Block texture
            //animatedBlock = new AnimatedBlock(new Vector2(100, 100));
            //animatedBlock.LoadContent(Content, "DungeonSheet");

        }


        protected override void Update(GameTime gameTime)
        {
           

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
                Reset();
            CurrentGameState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            CurrentGameState.Draw();
            base.Draw(gameTime);
        }
         
    }
}


