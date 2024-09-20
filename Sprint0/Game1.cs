using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Classes;
using Sprint0.Interfaces;

namespace Sprint0
{
    public class Game1 : Game
    {
        
        private IController keyboardController;
        //private IController mouseController;
        
        
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



            // Initialize the TextSprite for credits
        }
         


        protected override void Update(GameTime gameTime)
        {


            // Update the keyboard controller (handles switching between sprites)
            //keyboardController.Update();

            //Link.Update(gameTime);

            // Update the mouse controller (handles switching between sprites using mouse)
            //mouseController.Update();

            // Update the current active sprite
            //spriteManager.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            _spriteBatch.Begin();

            //spriteManager.Draw(_spriteBatch);


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


