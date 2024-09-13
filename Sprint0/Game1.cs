using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Classes;
using Sprint0.Interfaces;

namespace Sprint0
{
    //changeskkkkk
    public class Game1 : Game
    {
        private SpriteManager spriteManager;
        private IController keyboardController;
        private IController mouseController;
        private TextSprite creditsTextSprite;

        private SpriteFont font;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the font
            font = Content.Load<SpriteFont>("File");

            // Initialize the SpriteManager and load all textures
            spriteManager = new SpriteManager(GraphicsDevice, Content);

            // Initialize the KeyboardController and link it to the SpriteManager
            keyboardController = new KeyboardController(spriteManager);

            mouseController = new MouseController(spriteManager, GraphicsDevice);

            // Initialize the TextSprite for credits
            creditsTextSprite = new TextSprite(font, "Credits\nProgram Made By: Hengkai Zheng\nSprites from: https://www.spriters-resource.com/fullview/176365/",
                                               new Vector2(200, GraphicsDevice.Viewport.Height - 100), Color.Black);
        }


        protected override void Update(GameTime gameTime)
        {


            // Update the keyboard controller (handles switching between sprites)
            keyboardController.Update();

            // Update the mouse controller (handles switching between sprites using mouse)
            mouseController.Update();

            // Update the current active sprite
            spriteManager.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            _spriteBatch.Begin();

            spriteManager.Draw(_spriteBatch);

            creditsTextSprite.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


