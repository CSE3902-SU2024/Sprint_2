using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Classes;
using Sprint0.Player;
using Sprint2.Map;
using System.Diagnostics;


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
        private SoundEffect ak47Sound;
        public Link _link;
        public StageManager _StageManager;
        private LinkSpriteFactory _linkSpriteFactory;

        public static KeyboardController _keyboardController;
        public static KeyboardController _currentKeyboardController;
        int keyBoardVal;

        IGameState CurrentGameState;
        IGameState _StartMenu;
        IGameState CurrentLevel;
        IGameState PauseMenu;
        IGameState SinglePlayer;
        IGameState TwoPlayer;
        IGameState TwoPlayerMenu;
       
        private GameHUD _gameHUD;
        private InventoryMenu _inventoryMenu;

        // 2 player stuff
        private LinkSpriteFactory _linkSpriteFactory2;
        private Link _link2;

        bool levelCreated;
        


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

            levelCreated = false;
            _linkSpriteFactory = new LinkSpriteFactory(_graphicsDevice, Content, "LinkSpriteSheet2");
            Rectangle[] linkFrames = _linkSpriteFactory.CreateFrames();
            Texture2D linkTexture = Content.Load<Texture2D>("LinkSpriteSheet2");
            swordAttackSound = Content.Load<SoundEffect>("LTTP_Sword1");
            bowAttackSound = Content.Load<SoundEffect>("OOT_Arrow_Shoot");
            bombExplosion = Content.Load<SoundEffect>("LTTP_Bomb_Blow");
            boomerangSound = Content.Load<SoundEffect>("OOT_Boomerang_Throw");
            linkDeath = Content.Load<SoundEffect>("LinkDeath");
            ak47Sound =  Content.Load<SoundEffect>("ak47SoundEffect");

            _link = new Link(linkFrames,linkTexture, _graphicsDevice, _spriteBatch,_scale,Content,swordAttackSound,bowAttackSound, bombExplosion,boomerangSound, null, ak47Sound);
            _keyboardController = new KeyboardController(_link, null);
            _StartMenu = new StartMenu(_graphicsDevice,_spriteBatch, Content, _scale);
            PauseMenu = new PauseMenu( _spriteBatch,Content, _graphicsDevice);
            _gameHUD = new GameHUD(_spriteBatch, _graphicsDevice, Content, _link, _scale, _StageManager);
          
            _inventoryMenu = new InventoryMenu(_spriteBatch, _graphicsDevice, Content, _gameHUD, _link);
            CurrentGameState = _StartMenu;
            TwoPlayerMenu = new TwoPlayerMenu(_graphicsDevice,_spriteBatch,Content, _scale);
            content = Content;

            _currentKeyboardController = _keyboardController;

            
        }
        public void Update(GameTime gameTime)
        {
            int newStateIndex = _currentKeyboardController.Update(GameStateIndex);

            if (_link.isPaused)
            {
                _link.pauseTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_link.pauseTimer <= 0f)
                {
                    _link.isPaused = false;
                }
                return;
            }
            //   state transitions
            if (newStateIndex != GameStateIndex)
            {
                switch (newStateIndex)
                {
                    case 0:
                        CurrentGameState = _StartMenu;
                        break;
                    case 1: //to game  
                        if (!levelCreated)
                        {
                            ActivateSinglePlayer();
                        }
                        if (GameStateIndex == 2)  //  from inventory
                        {
                            _inventoryMenu.Reset();
                        }
                        CurrentGameState = SinglePlayer;
                        break;
                    case 2: // to Two player
                        CurrentGameState = TwoPlayerMenu;
                        break;
                    case 3: // to inventory
                        CurrentGameState = _inventoryMenu;
                        _inventoryMenu.StartTransitionIn();
                        break;
                    case 4:
                        if (levelCreated)
                        {
                            CurrentGameState = TwoPlayer;
                        }
                        break;
                    case 5: // pause
                        CurrentGameState = PauseMenu;
                        break;
                    default: break;
                }
                if (newStateIndex > 10)
                {
                   
                    Activate2Player(newStateIndex);
                    newStateIndex = 4;
                    if (!levelCreated)
                    {
                        Activate2Player(newStateIndex);
                    }
                    if (GameStateIndex == 2)  //  from inventory
                    {
                        _inventoryMenu.Reset();
                    }
                        
                        CurrentGameState = TwoPlayer;
                        Debug.WriteLine("current game state is two player");
              

                }

                GameStateIndex = newStateIndex;
            }

            

            CurrentGameState.Update(gameTime);
        }
        public void Draw()
        {
            CurrentGameState.Draw();
        }

        public void Activate2Player(int colorIdx)
        {
            levelCreated = true;
            Rectangle[] linkFrames2;
            Texture2D texture2;
            switch (colorIdx)
            {
                case 11:
                    _linkSpriteFactory2 = new LinkSpriteFactory(_graphicsDevice, content, "LinkSpriteSheetPink");
                    texture2 = content.Load<Texture2D>("LinkSpriteSheetPink");
                    break;
                case 12:
                    _linkSpriteFactory2 = new LinkSpriteFactory(_graphicsDevice, content, "LinkSpriteSheetCyan");
                    texture2 = content.Load<Texture2D>("LinkSpriteSheetCyan");
                    break;
                case 13:
                    _linkSpriteFactory2 = new LinkSpriteFactory(_graphicsDevice, content, "LinkSpriteSheetBlack");
                    texture2 = content.Load<Texture2D>("LinkSpriteSheetBlack");
                    break;
                case 14:
                    _linkSpriteFactory2 = new LinkSpriteFactory(_graphicsDevice, content, "LinkSpriteSheetBlue");
                    texture2 = content.Load<Texture2D>("LinkSpriteSheetBlue");
                    break;
                default:
                    _linkSpriteFactory2 = new LinkSpriteFactory(_graphicsDevice, content, "LinkSpriteSheet2");
                    texture2 = content.Load<Texture2D>("LinkSpriteSheet2");
                    break;
            }
            linkFrames2 = _linkSpriteFactory2.CreateFrames();
            Link_Inventory inventory = _link.GetInventory();
            _link2 = new Link(linkFrames2, texture2, _graphicsDevice, _spriteBatch, _scale, content, swordAttackSound, bowAttackSound, bombExplosion, boomerangSound, inventory, ak47Sound);
            _currentKeyboardController = new KeyboardController(_link, _link2);

            TwoPlayer = new TwoPlayerMode(_graphics, _spriteBatch, _scale, _graphicsDevice, _link, _link2);
            TwoPlayer.LoadContent(content);
        }
        public void ActivateSinglePlayer()
        {
            levelCreated = true;
            SinglePlayer = new LevelOne(_graphics, _spriteBatch, _scale, _graphicsDevice, _link);
            SinglePlayer.LoadContent(content);
        }

    }
}
