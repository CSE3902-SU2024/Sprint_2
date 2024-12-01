using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Classes;
using System;
using System.Reflection.Metadata;
using static Sprint0.Player.ILinkState;
using static Sprint2.Classes.Iitem;


namespace Sprint0.Player
{
    public class Link
    {

        public ILinkState currentState;
        public Rectangle[] _sourceRectangles;
        public Vector2 _position;
        public Vector2 _previousPosition;
        public Texture2D _texture;
        public Vector2 _scale;
        public Color _color;
        public float speed;
        public float boomerangSpeed;
        public int framesPerStep;
        public int framesPerSword;
        public int framesPerDamage;
        public int framesPerBoomerang;
        public int RemainingDamagedFrames;
        public Boolean Damaged;
        private int immunityDuration;
        private int remainingImmunityFrames;
        private bool isImmune;
        public bool transitioning;
        public bool hasKey;
        public bool hasBow;
        public bool hasPotion;
        public bool win;
        public bool hasMap;
        public bool isPaused;
        public float pauseTimer = 0f;
        public float pauseDuration = 2f;
        public bool hasCompass;
        public Link_Inventory inventory;
        public Direction currentDirection;

        private SpriteEffects spriteEffects;

        //for hud:
        public int Health { get; set; } = 16; // each heart = 2 hp

        public int keyCount { get; set; } = 0; // start with 0 keys

        public int GemCount { get; set; } = 0; // start with 0 gems

        public int BombCount { get; set; } = 3; //start with 3 for now

        public SoundEffect SwordAttackSound { get; private set; }
        public SoundEffect bowAttackSound { get; private set; }
        public SoundEffect bombExplosion { get; private set; }
        public SoundEffect BoomerangSound { get; private set; }

        private Vector2 BoomCoords;


        public Link(Rectangle[] sourceRectangles, Texture2D texture, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Vector2 scale, ContentManager content, SoundEffect swordSound, SoundEffect bowSound, SoundEffect bombSound, SoundEffect boomerangSound)
        {
            currentState = new LinkDown(this);
            _sourceRectangles = sourceRectangles;
            _position = new Vector2(500.0f, 500.0f);
            _scale = scale;
            _texture = texture;
            speed = 5.0f;
            boomerangSpeed = 10.0f;
            spriteEffects = SpriteEffects.None;
            framesPerStep = 8;
            framesPerSword = 4;
            framesPerDamage = 50;
            framesPerBoomerang = 3;
            RemainingDamagedFrames = framesPerDamage;
            Damaged = false;
            _color = Color.White;
            _previousPosition = new Vector2(200.0f, 200.0f);
            immunityDuration = 50;
            remainingImmunityFrames = 0;
            transitioning = false;
            hasKey = false;
            hasBow = false;
            win = false;
            hasMap = false;
            isPaused = false;
            hasCompass = false;
            inventory = new Link_Inventory(this, spriteBatch, scale, graphicsDevice, content);
            currentDirection = Direction.down;
            BoomCoords = new Vector2(0,0);
            var startingBomb = new Boom(Vector2.Zero, this);
            startingBomb.LoadContent(content, "NES - The Legend of Zelda - Items & Weapons", graphicsDevice, ItemType.boom, scale);
            inventory.AddItem(startingBomb);

            
            inventory.InitializeStartingItems();


            SwordAttackSound = swordSound;
            bowAttackSound = bowSound;
            bombExplosion = bombSound;
            BoomerangSound = boomerangSound;
        }

        public void MoveDown()
        {
            currentDirection = Direction.down;
            if (!transitioning)
                currentState.MoveDown();
        }

        public void MoveUp()
        {
            currentDirection = Direction.up;
            if (!transitioning)
            currentState.MoveUp();
        }

        public void MoveLeft()
        {
            currentDirection = Direction.left;
            if (!transitioning)
                currentState.MoveLeft();
        }

        public void MoveRight()
        {
            currentDirection = Direction.right;
            if (!transitioning)
                currentState.MoveRight();
        }

        public void SwordAttack()
        {
            currentState.UseSword();
        }

        public void ArrowAttack()
        {
            if (hasBow)
            currentState.UseArrow();
        }
        public void UseBoomerang()
        {
            currentState.UseBoomerang();
        }
        public void UseBomb()
        {
            if (BombCount >0)
            currentState.UseBomb();
        }
        public void TakeDamage()
        {
            currentState.IsDamaged();

            if (!isImmune) 
            {
                Health = Math.Max(0, Health - 1);
                remainingImmunityFrames = immunityDuration;
                isImmune = true; 
            }

        }
        public void Update()
        {
            currentState.Update();

            if (isImmune)
            {
                remainingImmunityFrames--;

                if (remainingImmunityFrames <= 0)
                {
                    isImmune = false;
                }
            }
            if (keyCount ==0)
            {
                hasKey = false;
            } else { hasKey = true; }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            currentState.Draw(_spriteBatch);
        }

        public void DrawSprite(SpriteBatch _spriteBatch, int frame, Boolean flipped)
        {
            if (flipped)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }

            if (Damaged)
            {
                _color = Color.Red;
            }
            else
            {
                _color = Color.White;
            }
            _spriteBatch.Draw(_texture, _position, _sourceRectangles[frame], _color, 0f, Vector2.Zero, _scale, spriteEffects, 0f);
        }

        public void DrawWeapon(SpriteBatch _spriteBatch, int frame, Boolean flippedHorizontal, Boolean flippedVertical, Vector2 _weaponPosition)
        {
            if (flippedHorizontal)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else if (flippedVertical)
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }
            _spriteBatch.Draw(_texture, _weaponPosition, _sourceRectangles[frame], Color.White, 0f, Vector2.Zero, _scale, spriteEffects, 0f);
        }

        public Vector2 GetLocation()
        {
            return _position;
        }

        public int GetKeyCount()
        {
            return keyCount;
        }

        public bool HasKey()
        {
            return hasKey;
        }

        public void DecrementKey()
        {
            keyCount--;
        }

        public void SetExplosionCoords(Vector2 boomCoords)
        {
            BoomCoords = boomCoords;
        }

        public Vector2 GetBoomCoords()
        {
            return BoomCoords;
        }

        public void IncrementKey()
        {
            keyCount++;
        }
    }
}
