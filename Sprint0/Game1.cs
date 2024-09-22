using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Classes;
using Sprint0.Interfaces;
using System.Collections.Generic;

namespace Sprint0
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Link _link;
        private KeyboardController _keyboardController;
        private LinkSpriteFactory _linkSpriteFactory;
        private AnimatedBlock animatedBlock;
        private Item Item;
        int currentItemIndex;
        KeyboardState previousKeyboardState;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            _keyboardController = new KeyboardController();

            base.Initialize();
            
            currentItemIndex = 0;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //initalize spritefactory
            _linkSpriteFactory = new LinkSpriteFactory(GraphicsDevice, Content, "zeldaLink");

            Rectangle[] linkFrames = _linkSpriteFactory.CreateFrames();

            //link texture
            Texture2D linkTexture = Content.Load<Texture2D>("zeldaLink");

            //link instance
            _link = new Link(linkTexture, new Vector2(100, 100), linkFrames);

            //Ben Addition
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load individual block textures (block1, block2, block3)
            Texture2D block1 = Content.Load<Texture2D>("DungeonBlock1");
            Texture2D block2 = Content.Load<Texture2D>("DungeonBlock2");
            Texture2D block3 = Content.Load<Texture2D>("DungeonBlock3");

            // Put the textures in an array
            Texture2D[] blockTextures = new Texture2D[] { block1, block2, block3 };

            animatedBlock = new AnimatedBlock(blockTextures, new Vector2(100, 100));

            Texture2D item1 = Content.Load<Texture2D>("DungeonBlock3");
            Texture2D item2 = Content.Load<Texture2D>("DungeonBlock2");

            Texture2D[] itemList = new Texture2D[] { item1, item2 };
            Item = new Item(itemList, new Vector2(200, 200), 50f);
        }
         


        protected override void Update(GameTime gameTime)
        {


            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardController.Update();
            _link.Update(gameTime, _keyboardController);

            animatedBlock.Update(gameTime, _keyboardController);

            Item.Update(gameTime, _keyboardController);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _link.Draw(_spriteBatch);
            animatedBlock.Draw(_spriteBatch);
            Item.Draw(_spriteBatch);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}


