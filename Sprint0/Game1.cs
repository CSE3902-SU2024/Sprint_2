using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Classes;
using Sprint0.Player;
using Sprint0.Interfaces;
using System.Collections.Generic;
using Sprint2.Map;
using Sprint0.Collisions;
using System.IO;
using Sprint2.Collisions;
using Sprint2.Enemy;
using static System.Formats.Asn1.AsnWriter;
using static Sprint2.Classes.Iitem;
using Sprint2;




namespace Sprint0
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Link _link;
        public StageManager _StageManager;
        private KeyboardController _keyboardController;
        private LinkSpriteFactory _linkSpriteFactory;
        private DungeonBlockSpriteFactory _dungeonBlockSpriteFactory;
        private AnimatedBlock animatedBlock;
        private Item Item;
        private Item Item2;
        private IEnemy enemy;
        private Texture2D bossSpriteSheet;
        private Texture2D dungeonSpriteSheet;
        private DungeonMap _map;

        public Vector2 _scale;

        private Enemy_Item_Map enemyItemMap;
        private int currentRoomNumber;

        //private GameHUD _gameHUD;
        //dont forget to uncomment loadcontent and draw



        KeyboardState previousKeyboardState;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
           
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //_graphics.PreferredBackBufferWidth = 1920;
            //_graphics.PreferredBackBufferHeight = 1080;
            //_graphics.IsFullScreen = false;
            // need to rescale everything to start with hud

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

            bossSpriteSheet = Content.Load<Texture2D>("Bosses1");
            _scale.X = (float)GraphicsDevice.Viewport.Width / 256.0f;
            _scale.Y = (float)GraphicsDevice.Viewport.Height / 176.0f;


            //initalize spritefactory
            _linkSpriteFactory = new LinkSpriteFactory(GraphicsDevice, Content, "LinkSpriteSheet2");
            _dungeonBlockSpriteFactory = new DungeonBlockSpriteFactory(GraphicsDevice, Content, "DungeonSheet");

            Rectangle[] linkFrames = _linkSpriteFactory.CreateFrames();
            Rectangle[] dungeonTiles = _dungeonBlockSpriteFactory.CreateFrames();


            //link texture
            Texture2D linkTexture = Content.Load<Texture2D>("LinkSpriteSheet2");
            Texture2D dungeonTexture = Content.Load<Texture2D>("DungeonSheet");

            //link instance
            _link = new Link(linkFrames, linkTexture, GraphicsDevice);
            _StageManager = new StageManager(dungeonTiles, dungeonTexture, _spriteBatch, GraphicsDevice, _link, Content);
            _keyboardController = new KeyboardController(_link, _StageManager);

            //_gameHUD = new GameHUD(_spriteBatch, GraphicsDevice, Content, _link, _scale);


        }


        public void Reset()
        {
           

            Rectangle[] linkFrames = _linkSpriteFactory.CreateFrames();

            //link texture
            Texture2D linkTexture = Content.Load<Texture2D>("LinkSpriteSheet2");
          

            //link instance
            _link = new Link(linkFrames, linkTexture, GraphicsDevice);
            _keyboardController = new KeyboardController(_link, _StageManager);
            //Block texture
            animatedBlock = new AnimatedBlock(new Vector2(100, 100));
            animatedBlock.LoadContent(Content, "DungeonSheet");
            
        }


        protected override void Update(GameTime gameTime)
        {
           

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
                Reset();


            _StageManager.Update(gameTime);
            _keyboardController.Update();

            _link.Update();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _link.Draw(_spriteBatch);
            _StageManager.Draw();

            _link.Draw(_spriteBatch);

            _spriteBatch.End();

            //_gameHUD.Draw();

            base.Draw(gameTime);
        }
         
    }
}


