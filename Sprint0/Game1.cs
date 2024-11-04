using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Classes;
using Sprint0.Collisions;
using Sprint0.Player;
using Sprint2.GameStates;

namespace Sprint0
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;    
        private IGameState CurrentGameState;
        public Vector2 _scale;
        //KeyboardState previousKeyboardState;
        public SoundEffect swordAttackSound;
        public SoundEffect bowAttackSound;
        public SoundEffect bombExplosion;
        public SoundEffect boomerangSound;
        public SoundEffect linkDeath;
        


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
            swordAttackSound = Content.Load<SoundEffect>("LTTP_Sword1");
            bowAttackSound = Content.Load<SoundEffect>("OOT_Arrow_Shoot");
            bombExplosion = Content.Load<SoundEffect>("LTTP_Bomb_Blow");
            boomerangSound = Content.Load<SoundEffect>("OOT_Boomerang_Throw");
            linkDeath = Content.Load<SoundEffect>("LinkDeath");


        }


        public void Reset()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content";
            _scale.X = (float)GraphicsDevice.Viewport.Width / 256.0f;
            _scale.Y = (float)GraphicsDevice.Viewport.Height / 230.0f;
            CurrentGameState = new LevelOne(_graphics, _spriteBatch, _scale, GraphicsDevice);
            CurrentGameState.LoadContent(Content);
            swordAttackSound = Content.Load<SoundEffect>("LTTP_Sword1");
            bowAttackSound = Content.Load<SoundEffect>("OOT_Arrow_Shoot");
            bombExplosion = Content.Load<SoundEffect>("LTTP_Bomb_Blow");
            boomerangSound = Content.Load<SoundEffect>("OOT_Boomerang_Throw");
            linkDeath = Content.Load<SoundEffect>("LinkDeath");

        }


        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Reset();
            }
            else if (CurrentGameState.GetLinkHealth() == 0)
            {
                linkDeath.Play();
                Reset();
            }
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


