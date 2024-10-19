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





        KeyboardState previousKeyboardState;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
           
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
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


            //string enemyMapPath = Path.Combine(Content.RootDirectory, "EnemyLayout.txt");
            //enemyItemMap = new Enemy_Item_Map(
            //    enemyMapPath,
            //    _scale,
            //    GraphicsDevice,
            //    Content
            //);
            // Create and load the Dragon (using the new Dragon class)
            //Dragon dragon = new Dragon(new Vector2(250, 200));
            //dragon.LoadContent(Content, "Bosses1", GraphicsDevice);
            //enemies.Add(dragon);


            //Goriya goriya = new Goriya(new Vector2(400, 200));
            //goriya.LoadContent(Content, "Dungeon1", GraphicsDevice);
            //enemies.Add(goriya);


            //Stalfos stalfos = new Stalfos(new Vector2(400, 200));
            //stalfos.LoadContent(Content, "Dungeon1", GraphicsDevice);
            //enemies.Add(stalfos);


            ////Keese keese = new Keese(new Vector2(400, 200));
            //keese.LoadContent(Content, "Dungeon1", GraphicsDevice);
            //enemies.Add(keese);


            //Gel gel = new Gel(new Vector2(400, 200));
            //gel.LoadContent(Content, "Dungeon1", GraphicsDevice);
            //enemies.Add(gel);


            //enemy = enemies[currentEnemyIndex];



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

            //Item texure
            //Item = new Item(new Vector2(200, 200), 50f);
            //Item.LoadContent(Content, "NES - The Legend of Zelda - Items & Weapons", Item.ItemType.unattackable);

            //Item2 = new Item(new Vector2(600, 100), 0f);
            //Item2.LoadContent(Content, "zeldaLink", Item.ItemType.attackable);


            

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

            //// Reset enemy positions
            //enemy = enemies[0];
            //currentEnemyIndex = 0;
            //foreach (var enemy in enemies)
            //{
            //    enemy.Reset();
            //}
            //needs to be refactored
            
        }


        protected override void Update(GameTime gameTime)
        {
           

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
                Reset();


            _StageManager.Update(gameTime);
            _keyboardController.Update();
         //   _link.Update(gameTime, _keyboardController);
         //bruh 
           //  animatedBlock.Update(gameTime, _keyboardController);

            //if (_keyboardController.PreviousEnemy)
            //{
            //    currentEnemyIndex = (currentEnemyIndex - 1 + enemies.Count) % enemies.Count;
            //    enemy = enemies[currentEnemyIndex];
            //}
            //else if (_keyboardController.NextEnemy)
            //{
            //    currentEnemyIndex = (currentEnemyIndex + 1) % enemies.Count;
            //    enemy = enemies[currentEnemyIndex];
            //}
            //needs to be refactored

            //Item.Update(gameTime, _keyboardController);
            //Item2.Update(gameTime, _keyboardController);
            _link.Update();
            
            //enemy.Update(gameTime);
        
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _link.Draw(_spriteBatch);
            _StageManager.Draw();
            //enemy.Draw(_spriteBatch);
            //Item.Draw(_spriteBatch);
            //Item2.Draw(_spriteBatch);
            _link.Draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
         
    }
}


