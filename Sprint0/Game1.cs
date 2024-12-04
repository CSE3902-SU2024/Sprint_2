using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Classes;
using Sprint0.Collisions;
using Sprint0.Player;
using Sprint2.GameStates;
using Sprint2.Map;

namespace Sprint0
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;    
        private IGameState CurrentGameState;
        public Vector2 _scale;
        //KeyboardState previousKeyboardState;
        //public SoundEffect swordAttackSound;
        //public SoundEffect bowAttackSound;
        //public SoundEffect bombExplosion;
        //public SoundEffect boomerangSound;
        //public SoundEffect linkDeath;

        private GameStateManager _GameStateManager;
        public int enemyDefeatedCount = 0;
        public int itemCollectedCount = 0;
        public bool isDungeonComplete = false;
        private StageManager _stageManager;


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
           // _spriteBatch.Begin();
            Content.RootDirectory = "Content";
            _scale.X = (float)GraphicsDevice.Viewport.Width / 256.0f;
            _scale.Y = (float)GraphicsDevice.Viewport.Height / 230.0f;
            CurrentGameState = new StartMenu(GraphicsDevice,_spriteBatch, Content, _scale);
            CurrentGameState.LoadContent(Content);
            //swordAttackSound = Content.Load<SoundEffect>("LTTP_Sword1");
            //bowAttackSound = Content.Load<SoundEffect>("OOT_Arrow_Shoot");
            //bombExplosion = Content.Load<SoundEffect>("LTTP_Bomb_Blow");
            //boomerangSound = Content.Load<SoundEffect>("OOT_Boomerang_Throw");
            //linkDeath = Content.Load<SoundEffect>("LinkDeath");

            _GameStateManager = new GameStateManager(_graphics, GraphicsDevice, _spriteBatch, _scale);
            _GameStateManager.LoadContent(Content);
            //_stageManager.InitializeAchievements();
        }


        public void Reset()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content";
            _scale.X = (float)GraphicsDevice.Viewport.Width / 256.0f;
            _scale.Y = (float)GraphicsDevice.Viewport.Height / 230.0f;
            //   CurrentGameState = new LevelOne(_graphics, _spriteBatch, _scale, GraphicsDevice, _link);
            //  CurrentGameState.LoadContent(Content);
            //swordAttackSound = Content.Load<SoundEffect>("LTTP_Sword1");
            //bowAttackSound = Content.Load<SoundEffect>("OOT_Arrow_Shoot");
            //bombExplosion = Content.Load<SoundEffect>("LTTP_Bomb_Blow");
            //boomerangSound = Content.Load<SoundEffect>("OOT_Boomerang_Throw");
            //linkDeath = Content.Load<SoundEffect>("LinkDeath");
            _GameStateManager = new GameStateManager(_graphics, GraphicsDevice, _spriteBatch, _scale);
            _GameStateManager.LoadContent(Content);
        }


        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Reset();
            }
            else if (_GameStateManager.GetLinkHealth() <= 0)
            {
              //  linkDeath.Play();
                Reset();
            }
            _GameStateManager.Update(gameTime);
           // CurrentGameState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Black);
            _GameStateManager.Draw();
           // CurrentGameState.Draw();
           _spriteBatch.End();
            base.Draw(gameTime);
        }      
    }
}


