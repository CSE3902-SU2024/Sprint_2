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
using static System.Formats.Asn1.AsnWriter;


namespace Sprint2.GameStates
{
    public class GameStateManager
    {
        int GameStateIndex;

        private GraphicsDeviceManager _graphics;
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private ContentManager content;
        Vector2 _scale;


        private SoundEffect swordAttackSound;
        private SoundEffect bowAttackSound;
        private SoundEffect bombExplosion;
        private SoundEffect boomerangSound;
        private SoundEffect linkDeath;
        public Link _link;
        public StageManager _StageManager;
        private LinkSpriteFactory _linkSpriteFactory;

        private KeyboardController _keyboardController;
        int keyBoardVal;

        IGameState CurrentGameState;
        IGameState CurrentLevel;
        IGameState PauseMenu;
       
        private GameHUD _gameHUD;
        private InventoryMenu _inventoryMenu;


        public GameStateManager(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice,SpriteBatch spriteBatch, Vector2 scale) 
        { 
            _graphics = graphics;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;
            _scale = scale;
            GameStateIndex = 0;
            keyBoardVal = 100;
            
            

        }

        public int GetLinkHealth()
        {
            return _link.Health;
        }

        public void LoadContent(ContentManager Content)
        {

            _linkSpriteFactory = new LinkSpriteFactory(_graphicsDevice, Content, "LinkSpriteSheet2");
            Rectangle[] linkFrames = _linkSpriteFactory.CreateFrames();
            Texture2D linkTexture = Content.Load<Texture2D>("LinkSpriteSheet2");
            swordAttackSound = Content.Load<SoundEffect>("LTTP_Sword1");
            bowAttackSound = Content.Load<SoundEffect>("OOT_Arrow_Shoot");
            bombExplosion = Content.Load<SoundEffect>("LTTP_Bomb_Blow");
            boomerangSound = Content.Load<SoundEffect>("OOT_Boomerang_Throw");
            linkDeath = Content.Load<SoundEffect>("LinkDeath");
            _link = new Link(linkFrames, linkTexture, _graphicsDevice, _scale, swordAttackSound, bowAttackSound, bombExplosion, boomerangSound);
            _gameHUD = new GameHUD(_spriteBatch, _graphicsDevice, Content, _link, _scale);
            _keyboardController = new KeyboardController(_link);
            CurrentGameState = new StartMenu(_graphicsDevice,_spriteBatch, Content, _scale);
            CurrentLevel = new LevelOne(_graphics,_spriteBatch, _scale,_graphicsDevice, _link);
            CurrentLevel.LoadContent(Content);
            PauseMenu = new PauseMenu(linkTexture, _spriteBatch, _graphicsDevice, Content);
            _inventoryMenu = new InventoryMenu(_spriteBatch, _graphicsDevice, Content, _gameHUD);
            content = Content;
        }
        public void Update(GameTime gameTime)
        {
            keyBoardVal = _keyboardController.Update(GameStateIndex);
            if (keyBoardVal == 1)
            {
                GameStateIndex = 1;
                CurrentGameState = CurrentLevel;
            }
            else if (keyBoardVal == 2)
            {
                GameStateIndex = 2;
                CurrentGameState = _inventoryMenu;
            }
            else if (keyBoardVal == 5)
            {
                GameStateIndex = 5;
                CurrentGameState = PauseMenu;
            }
            CurrentGameState.Update(gameTime);
        }
        public void Draw()
        {
            CurrentGameState.Draw();
        }

    }
}
