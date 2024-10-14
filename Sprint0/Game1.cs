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
        private Vector2 _scale = new Vector2(4.0f, 4.0f);
        



        private List<IEnemy> enemies;
        private int currentEnemyIndex = 0;

        KeyboardState previousKeyboardState;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
           
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            enemies = new List<IEnemy>();

            //DEBUG FOR ENEMY HITBOXES
            DebugDraw.Initialize(GraphicsDevice);



            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bossSpriteSheet = Content.Load<Texture2D>("Bosses1");


            // Create and load the Dragon (using the new Dragon class)
            Dragon dragon = new Dragon(new Vector2(400, 200));
            dragon.LoadContent(Content, "Bosses1");
            enemies.Add(dragon);

            // Create and load the Goriya (using the new Goriya class)
            Goriya goriya = new Goriya(new Vector2(400, 200));
            goriya.LoadContent(Content, "Dungeon1");
            enemies.Add(goriya);

            // Create and load the Stalfos (using the new Stalfos class)
            Stalfos stalfos = new Stalfos(new Vector2(400, 200));
            stalfos.LoadContent(Content, "Dungeon1");
            enemies.Add(stalfos);

            // Create and load the Keese (using the new Keese class)
            Keese keese = new Keese(new Vector2(400, 200));
            keese.LoadContent(Content, "Dungeon1");
            enemies.Add(keese);

            // Create and load the Gel (using the new Gel class)
            Gel gel = new Gel(new Vector2(400, 200));
            gel.LoadContent(Content, "Dungeon1");
            enemies.Add(gel);


            enemy = enemies[currentEnemyIndex];



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
            _StageManager = new StageManager(dungeonTiles, dungeonTexture, _spriteBatch, GraphicsDevice, _link);
            _keyboardController = new KeyboardController(_link, _StageManager);

            //Item texure
            Item = new Item(new Vector2(200, 200), 50f);
            Item.LoadContent(Content, "NES - The Legend of Zelda - Items & Weapons", Item.ItemType.unattackable);

            Item2 = new Item(new Vector2(600, 100), 0f);
            Item2.LoadContent(Content, "zeldaLink", Item.ItemType.attackable);


            

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

            //Item texure
            Item = new Item(new Vector2(200, 200), 50f);
            Item.LoadContent(Content, "NES - The Legend of Zelda - Items & Weapons", Item.ItemType.unattackable);

            Item2 = new Item(new Vector2(600, 100), 0f);
            Item2.LoadContent(Content, "zeldaLink", Item.ItemType.attackable);

            // Reset enemy positions
            enemy = enemies[0];
            currentEnemyIndex = 0;
            foreach (var enemy in enemies)
            {
                enemy.Reset();
            }
            
        }


        protected override void Update(GameTime gameTime)
        {
           

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
                Reset();

           

            _keyboardController.Update();
         //   _link.Update(gameTime, _keyboardController);

           //  animatedBlock.Update(gameTime, _keyboardController);

            if (_keyboardController.PreviousEnemy)
            {
                currentEnemyIndex = (currentEnemyIndex - 1 + enemies.Count) % enemies.Count;
                enemy = enemies[currentEnemyIndex];
            }
            else if (_keyboardController.NextEnemy)
            {
                currentEnemyIndex = (currentEnemyIndex + 1) % enemies.Count;
                enemy = enemies[currentEnemyIndex];
            }

            Item.Update(gameTime, _keyboardController);
            Item2.Update(gameTime, _keyboardController);
            _link.Update();
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
            //    LinkEnemyCollision.HandleCollisions(_link, enemies, _link._scale);
            }
            HandlePlayerWallCollision playerTopWallCollision = new HandlePlayerWallCollision(_link._position, Vector2.Zero, 16, 16, 112, 32);
            playerTopWallCollision.PlayerWallCollision(ref _link._position, _link._previousPosition, _StageManager._scale);
            HandlePlayerWallCollision playerTopWallCollision2 = new HandlePlayerWallCollision(_link._position, new Vector2(144 * _StageManager._scale.X,  0), 16, 16, 112, 32);
            playerTopWallCollision2.PlayerWallCollision(ref _link._position, _link._previousPosition, _StageManager._scale);

            HandlePlayerWallCollision playerBottomWallCollision = new HandlePlayerWallCollision(_link._position, new Vector2(0, 144 * _StageManager._scale.Y), 16, 16, 112, 32);
            playerBottomWallCollision.PlayerWallCollision(ref _link._position, _link._previousPosition, _StageManager._scale);
            HandlePlayerWallCollision playerBottomWallCollision2 = new HandlePlayerWallCollision(_link._position, new Vector2(144 * _StageManager._scale.X, 144 * _StageManager._scale.Y), 16, 16, 112, 32);
            playerBottomWallCollision2.PlayerWallCollision(ref _link._position, _link._previousPosition, _StageManager._scale);

            //HandlePlayerWallCollision playerLeftWallCollision = new HandlePlayerWallCollision(_link._position, new Vector2(0, 32 * _StageManager._scale.Y), 16, 16, 32, 40);
            //playerLeftWallCollision.PlayerWallCollision(ref _link._position, _link._previousPosition, _StageManager._scale);
            //HandlePlayerWallCollision playerLeftWallCollision2 = new HandlePlayerWallCollision(_link._position, new Vector2(0, 104 * _StageManager._scale.Y), 16, 16, 32, 40);
            //playerLeftWallCollision2.PlayerWallCollision(ref _link._position, _link._previousPosition, _StageManager._scale);

            HandlePlayerWallCollision playerRightWallCollision = new HandlePlayerWallCollision(_link._position, new Vector2(224 * _StageManager._scale.X, 32 * _StageManager._scale.Y), 16, 16, 32, 40);
            playerRightWallCollision.PlayerWallCollision(ref _link._position, _link._previousPosition, _StageManager._scale);
            HandlePlayerWallCollision playerRightWallCollision2 = new HandlePlayerWallCollision(_link._position, new Vector2(224 * _StageManager._scale.X, 104 * _StageManager._scale.Y), 16, 16, 32, 40);
            playerRightWallCollision2.PlayerWallCollision(ref _link._position, _link._previousPosition, _StageManager._scale);


            HandlePlayerBlockCollision playerTopBlockCollision = new HandlePlayerBlockCollision(_link._position, new Vector2(49 * _StageManager._scale.X, 49 * _StageManager._scale.Y), 16, 16, 16, 16);
            playerTopBlockCollision.PlayerBlockCollision(ref _link._position, _link._previousPosition, _StageManager._scale);

            //HandlePlayerBlockCollision playerBottomBlockCollision = new HandlePlayerBlockCollision(_link._position, 16, 16);
            //playerBottomBlockCollision.PlayerBlockCollision(ref _link._position, _link._previousPosition, _StageManager._scale);

            //HandlePlayerBlockCollision playerLeftBlockCollision = new HandlePlayerBlockCollision(_link._position, 16, 16);
            //playerLeftBlockCollision.PlayerBlockCollision(ref _link._position, _link._previousPosition, _StageManager._scale);

            //HandlePlayerBlockCollision playerRightBlockCollision = new HandlePlayerBlockCollision(_link._position, 16, 16);
            //playerRightBlockCollision.PlayerBlockCollision(ref _link._position, _link._previousPosition, _StageManager._scale);


            //Rectangle playerBoundingBox = new Rectangle((int)(_link._position.X), (int)(_link._position.Y), 16, 16);
            //Rectangle blockBoundingBox = new Rectangle(100, 100, 15, 15);

            //if (playerBoundingBox.Intersects(blockBoundingBox))
            //{
            //HandleCollisionB(playerBoundingBox, blockBoundingBox);
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            //  _link.Draw(_spriteBatch);
            _StageManager.Draw();
            enemy.Draw(_spriteBatch);
            Item.Draw(_spriteBatch);
            Item2.Draw(_spriteBatch);
            _link.Draw(_spriteBatch);
            DebugDraw.DrawHitboxes(_spriteBatch, _link, enemies, _scale);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
         
    }
}


