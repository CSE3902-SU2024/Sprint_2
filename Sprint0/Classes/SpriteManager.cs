using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Classes;
using Sprint0.Interfaces;


namespace Sprint0.Classes
{
    public enum SpriteType
    {
        Static,
        Moving,
        Animated,
        MovingAnimated
    }

    public class SpriteManager
    {
        private ISprite currentSprite;
        private readonly GraphicsDevice graphicsDevice;
        private readonly ContentManager content;

       // private Texture2D staticSpriteTexture;
        private Texture2D[] animatedSpriteFrames;
        private Texture2D movingSpriteTexture;
        private Texture2D[] movingAnimatedSpriteFrames;
        private Rectangle[] sourceRectangles;
        private Vector2 scale;

        //Link spritesheet[sprint2]:
        Texture2D LinkSheet;

        private Direction facingDirection;
        private Vector2 position;
        private bool isAttacking;


        public SpriteManager(GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.graphicsDevice = graphicsDevice;
            this.content = content;

        
            LoadTextures();
            SetSprite(SpriteType.Static); // Default to Static Sprite
        }

        private void LoadTextures()
        {
            //Link sheet [sprint2]
            LinkSheet = content.Load<Texture2D>("zeldaLink");
            sourceRectangles = new Microsoft.Xna.Framework.Rectangle[8]
            {
                //https://pixspy.com/
               new Microsoft.Xna.Framework.Rectangle(1, 11, 16, 16), //animation frame 1 {down 1}
               new Microsoft.Xna.Framework.Rectangle(18, 11, 16, 16), //animation frame 2 {down 2}
               new Microsoft.Xna.Framework.Rectangle(35, 11, 16, 16), //animation frame 2 {right 1}
               new Microsoft.Xna.Framework.Rectangle(52, 11, 16, 16), //animation frame 2 {right 2}
               new Microsoft.Xna.Framework.Rectangle(69, 11, 16, 16), //animation frame 2 {up 1}
               new Microsoft.Xna.Framework.Rectangle(86, 11, 16, 16), //animation frame 2 {up 2}
               new Microsoft.Xna.Framework.Rectangle(35, 11, 16, 16), //animation frame 2 {left 1}  //gotta flip these horizontally
               new Microsoft.Xna.Framework.Rectangle(52, 11, 16, 16), //animation frame 2 {left 2}
            };

            //add a scale for sprites: 
            scale = new Vector2(2.0f, 2.0f); //2x scale


            // Load static sprite texture

            //staticSpriteTexture = content.Load<Texture2D>("mario_standing");

            // Load moving sprite texture
            movingSpriteTexture = content.Load<Texture2D>("mario_standing"); // Reuse for moving sprite

            // Load animated sprite frames (non-moving, animated)
            animatedSpriteFrames = new Texture2D[3];
            animatedSpriteFrames[0] = content.Load<Texture2D>("mario_run1");
            animatedSpriteFrames[1] = content.Load<Texture2D>("mario_run2");
            animatedSpriteFrames[2] = content.Load<Texture2D>("mario_run3");

            // Load moving animated sprite frames (moving, animated)
            movingAnimatedSpriteFrames = new Texture2D[3];
            movingAnimatedSpriteFrames[0] = content.Load<Texture2D>("mario_run1");
            movingAnimatedSpriteFrames[1] = content.Load<Texture2D>("mario_run2");
            movingAnimatedSpriteFrames[2] = content.Load<Texture2D>("mario_run3");
        }

        public void SetSprite(SpriteType type)
        {
            switch (type)
            {
                case SpriteType.Static:
                    currentSprite = new StaticSprite(LinkSheet, sourceRectangles, scale);
                    break;
                case SpriteType.Moving:
                    currentSprite = new MovingSprite(movingSpriteTexture, graphicsDevice.Viewport.Height);
                    break;
                case SpriteType.Animated:
                    currentSprite = new AnimatedSprite(animatedSpriteFrames);
                    break;
                case SpriteType.MovingAnimated:
                    currentSprite = new MovingAnimatedSprite(movingAnimatedSpriteFrames, graphicsDevice.Viewport.Width);
                   break;
            }
         }

        public void Update(GameTime gameTime)
        {
            currentSprite?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentSprite?.Draw(spriteBatch);
        }
    }
}
