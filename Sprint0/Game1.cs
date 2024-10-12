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
        private Enemy enemy;
        private Texture2D bossSpriteSheet;
        private Texture2D dungeonSpriteSheet;
        private DungeonMap _map;
        private Vector2 _scale = new Vector2(4.0f, 4.0f);
        



        private List<Enemy> enemies;
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
            enemies = new List<Enemy>();

            //DEBUG FOR ENEMY HITBOXES
            DebugDraw.Initialize(GraphicsDevice);



            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bossSpriteSheet = Content.Load<Texture2D>("Bosses1");
          

            // Create and load the Dragon (from Bosses sheet)
            Enemy dragon = new Enemy(new Vector2(400, 200))
            {
                currentEnemyType = Enemy.EnemyType.Dragon
            };
            dragon.LoadContent(Content, "Bosses1"); // Load Dragon content (using "Bosses" sheet)
            enemies.Add(dragon); // Add Dragon to enemies list

            // Create and load the Goriya (from Dungeon sheet)
            Enemy goriya = new Enemy(new Vector2(400, 200))
            {
                currentEnemyType = Enemy.EnemyType.Goriya
            };
            goriya.LoadContent(Content, "Dungeon1"); // Load Goriya content (using "Dungeon" sheet)
            enemies.Add(goriya); // Add Goriya to enemies list

            Enemy staflos = new Enemy(new Vector2(400, 200))
            {
                currentEnemyType = Enemy.EnemyType.Stalfos
            };
            staflos.LoadContent(Content, "Dungeon1");
            enemies.Add(staflos);

            Enemy Keese = new Enemy(new Vector2(400, 200))
            {
                currentEnemyType = Enemy.EnemyType.Keese
            };
            Keese.LoadContent(Content, "Dungeon1");
            enemies.Add(Keese);

            Enemy Gel = new Enemy(new Vector2(400, 200))
            {
                currentEnemyType = Enemy.EnemyType.Gel
            };
            Gel.LoadContent(Content, "Dungeon1");
            enemies.Add(Gel);

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
           // playerTopWallCollision.PlayerWallCollision(ref _link._position, _link.speed, _StageManager._scale);
            HandlePlayerWallCollision playerTopWallCollision2 = new HandlePlayerWallCollision(_link._position, new Vector2(144 * _StageManager._scale.X,  0), 16, 16, 112, 32);
          //  playerTopWallCollision2.PlayerWallCollision(ref _link._position, _link.speed, _StageManager._scale);


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
            GraphicsDevice.Clear(Color.Gray);

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


