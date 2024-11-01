using System;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Classes;
using Sprint0;
using Sprint0.Player;
using Sprint2.Enemy;
using Sprint2.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;


namespace Sprint2.GameStates
{
    public class LevelOne : IGameState
    {
        private GraphicsDeviceManager _graphics;
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        public Link _link;
        public StageManager _StageManager;
        private LinkSpriteFactory _linkSpriteFactory;
        private DungeonBlockSpriteFactory _dungeonBlockSpriteFactory;
       // private AnimatedBlock animatedBlock;
        private IEnemy enemy;
        private Texture2D bossSpriteSheet;
        private Texture2D dungeonSpriteSheet;
        private DungeonMap _map;
        public Vector2 _scale;
        private Enemy_Item_Map enemyItemMap;
        private KeyboardController _keyboardController;
        private int currentRoomNumber;
        private GameHUD _gameHUD;

        public LevelOne(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 scale, GraphicsDevice graphicsDevice)
        {
            _graphics = graphics;
            _spriteBatch = spriteBatch;
            _scale = scale;
            _graphicsDevice = graphicsDevice;
        }

        public void LoadContent(ContentManager Content)
        {
            

            bossSpriteSheet = Content.Load<Texture2D>("Bosses1");
 

            //initalize spritefactory
            _linkSpriteFactory = new LinkSpriteFactory(_graphicsDevice, Content, "LinkSpriteSheet2");
            _dungeonBlockSpriteFactory = new DungeonBlockSpriteFactory(_graphicsDevice, Content, "DungeonSheet");

            Rectangle[] linkFrames = _linkSpriteFactory.CreateFrames();
            Rectangle[] dungeonTiles = _dungeonBlockSpriteFactory.CreateFrames();

            SoundEffect swordAttackSound = Content.Load<SoundEffect>("LTTP_Sword1");
            SoundEffect bowAttackSound = Content.Load<SoundEffect>("OOT_Arrow_Shoot");
            SoundEffect bombExplosion = Content.Load<SoundEffect>("LTTP_Bomb_Blow");
            SoundEffect boomerangSound = Content.Load<SoundEffect>("OOT_Boomerang_Throw");


            //link texture
            Texture2D linkTexture = Content.Load<Texture2D>("LinkSpriteSheet2");
            Texture2D dungeonTexture = Content.Load<Texture2D>("DungeonSheet");

            //link instance
            //_link = new Link(linkFrames, linkTexture, _graphicsDevice, _scale);
            _link = new Link(linkFrames, linkTexture, _graphicsDevice, _scale, swordAttackSound, bowAttackSound, bombExplosion, boomerangSound);
            _StageManager = new StageManager(dungeonTiles, dungeonTexture, _spriteBatch, _graphicsDevice, _link, Content, _scale);
            _gameHUD = new GameHUD(_spriteBatch, _graphicsDevice, Content, _link, _scale);
            _keyboardController = new KeyboardController(_link, _StageManager);
        }


        public void Draw()
        {

            _spriteBatch.Begin();
            _link.Draw(_spriteBatch);
            _StageManager.Draw();

            _link.Draw(_spriteBatch);

            _spriteBatch.End();

            _gameHUD.Draw();
        }


        public void Update(GameTime gameTime)
        {
            _StageManager.Update(gameTime);
            _link.Update();
            _keyboardController.Update();

        }

       
    }
}
