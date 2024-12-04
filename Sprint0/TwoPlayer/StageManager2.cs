﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sprint0.Collisions;
using Sprint0.Player;
using System;
using System.Diagnostics;
using Sprint2.Classes;
using System.Reflection.Metadata.Ecma335;
using Sprint2.Map;


namespace Sprint2.TwoPlayer
{


    public class StageManager2
    {
        public GameStage currentGameStage; // To track the current game stage
        public int StageIndex;
        public DrawDungeon2 _DrawDungeon2;
        public Texture2D _texture;
        public SpriteBatch _spriteBatch;
        public Vector2 _scale;
        static GraphicsDevice _graphicsDevice;
        private DoorDecoder _doorDecoder;
        public NextStageDecider2 _nextStageDecider2;
        DungeonMap _DungeonMap;
        DoorMap _DoorMap;
        Enemy_Item_Map _EnemyItem;
        ItemMap _ItemMap;
        bool StageAnimating;
        int AnimatingCount;
        private Link _link;
        private Link _link2;
        private StageAnimator2 _StageAnimator2;

        public static StageManager2 Instance { get; private set; }
        public Boolean drawHitboxes;

        // Start Menu
        Song backgroundMusic;
        public MovableBlock movableBlock14;
        public MovableBlock movableBlock8;

        public StageManager2(Rectangle[] sourceRectangles, Texture2D texture, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Link link, Link link2, ContentManager content, Vector2 scale)
        {
            Debug.WriteLine("load 2");
            currentGameStage = GameStage.StartMenu;
            StageAnimating = false;
            AnimatingCount = 0;
            StageIndex = 0;
            _texture = texture;
            _spriteBatch = spriteBatch;
            _link = link;
            _link2 = link2;
            _scale = scale;
            Instance = this;
            drawHitboxes = false;
            _graphicsDevice = graphicsDevice;
            _DungeonMap = new DungeonMap("../../../Map/DungeonMap2.csv");
            _DoorMap = new DoorMap("../../../Map/Dungeon_Doors.csv");
            _EnemyItem = new Enemy_Item_Map("../../../Map/EnemyItem_Map.csv", _scale, graphicsDevice, content, _link);
            _ItemMap = new ItemMap("../../../Map/ItemMap.csv", _scale, graphicsDevice, content, _link, _link2);

            _nextStageDecider2 = new NextStageDecider2(_link, _link2, _scale, _DoorMap, this);
            _DrawDungeon2 = new DrawDungeon2(sourceRectangles, texture, spriteBatch, _scale, _link, _link2, _DungeonMap, _DoorMap, _EnemyItem, _ItemMap);
            GameHUD2 gameHUD = new GameHUD2(spriteBatch, graphicsDevice, content, link,link2, scale, this);

            _DrawDungeon2.SetHUDResources(gameHUD);

            Texture2D itemTexture = content.Load<Texture2D>("NES - The Legend of Zelda - Items & Weapons");
            _DrawDungeon2.SetItemTexture(itemTexture);


            //Music

            backgroundMusic = content.Load<Song>("DungeonTheme");
           
            _StageAnimator2 = new StageAnimator2(_DungeonMap, _DoorMap, _scale, sourceRectangles, _texture, spriteBatch, _DrawDungeon2);

            //MovableBlock movableblock14 = new MovableBlock(new Vector2(100, 100));
            Vector2 EasierAccessTilePosition14 = new Vector2(100, 100) + new Vector2(3, 3);
            movableBlock14 = new MovableBlock(_link._position, EasierAccessTilePosition14, 16, 16, 13, 13);
            movableBlock14.LoadContent(content, "DungeonSheet", new Rectangle(212, 323, 16, 16));
            if (texture == null)
            {
                Debug.WriteLine("Texture not loaded");
            }
            else
            {
                //  Debug.WriteLine("Texture loaded: " + texture.Width + "x" + texture.Height);
            }


            //MovableBlock movableblock8 = new MovableBlock(new Vector2(100, 100));
            //Vector2 EasierAccessTilePosition8 = new Vector2(100, 100) + new Vector2(3, 3);
            //movableBlock8 = new MovableBlock(_link._position, EasierAccessTilePosition8, 16, 16, 13, 13);
            //movableBlock8.LoadContent(content, "DungeonSheet", new Rectangle(212, 323, 16, 16));


        }

        public void switchHitbox()
        {
            drawHitboxes = !drawHitboxes;
            Debug.WriteLine($"drawHitboxes: {drawHitboxes}");
        }


        public void Update(GameTime gameTime)
        {
            if (StageAnimating)
            {
                AnimatingCount -= 2;
                _StageAnimator2.Update();
            }
            if (AnimatingCount <= 0)
            {
                StageAnimating = false;
                _DoorMap.SpecialDoorLogic(StageIndex);
            }

            if (!StageAnimating)
            {
                _nextStageDecider2.Update(StageIndex);
                _DrawDungeon2.Update(StageIndex);
                _EnemyItem.Update(StageIndex, gameTime);
                _ItemMap.Update(StageIndex, gameTime);
                LinkEnemyCollision.HandleCollisions(_link, _EnemyItem, StageIndex, _link._scale, _link.BulletManager);
                LinkEnemyCollision.HandleCollisions(_link2, _EnemyItem, StageIndex, _link2._scale, _link2.BulletManager);
            }
            if (StageIndex == 0)
            {
                Boolean enemiesPresent = _EnemyItem.AreThereEnemies(StageIndex);
                _DoorMap.AllEnemiesDead(StageIndex, enemiesPresent);
            }
            if (StageIndex == 3)
            {
                if (_EnemyItem.AreThereEnemies(StageIndex))
                {

                    _ItemMap.SpawnKey(StageIndex);
                }
            }

            if (StageIndex == 5)
            {
                Vector2 BoomCoords = _link.GetBoomCoords();
                if (BoomCoords.X > 115 * _scale.X && BoomCoords.X < 150 * _scale.X)
                {
                    if (BoomCoords.Y < 125 * _scale.Y && BoomCoords.Y > 75 * _scale.Y)
                    {
                        _DoorMap.BoomLogic(5);
                    }

                }
            }
            if (StageIndex == 6)
            {
                Vector2 BoomCoords = _link.GetBoomCoords();
                if (BoomCoords.X > 115 * _scale.X && BoomCoords.X < 150 * _scale.X)
                {
                    if (BoomCoords.Y < 125 * _scale.Y && BoomCoords.Y > 75 * _scale.Y)
                    {
                        _DoorMap.BoomLogic(6);
                    }

                }
            }
            // stage 14 and 8 have the movable block
            if (StageIndex == 14)
            {
                if (movableBlock14 != null)
                {
                    Console.WriteLine("Updating movable block...");
                    Console.WriteLine($"Position before update: {movableBlock14.blockPosition}");
                    movableBlock14.Update(ref _link._position, _scale);
                    Console.WriteLine($"Position after update: {movableBlock14.blockPosition}");
                }
                //Console.WriteLine("Updating movable block...");
                //Console.WriteLine($"Position before update: {movableBlock14.blockPosition}");
                //movableBlock14.Update(ref _link._position, _scale);
                //Console.WriteLine($"Position after update: {movableBlock14.blockPosition}");

            }

            if (StageIndex == 8)
            {
                if (movableBlock8 != null)
                {
                    Console.WriteLine("Updating movable block...");
                    Console.WriteLine($"Position before update: {movableBlock8.blockPosition}");
                    movableBlock8.Update(ref _link._position, _scale); // error right now
                    Console.WriteLine($"Position after update: {movableBlock14.blockPosition}");
                }
                //Console.WriteLine("Updating movable block...");
                //Console.WriteLine($"Position before update: {movableBlock8.blockPosition}");
                //movableBlock8.Update(ref _link._position, _scale); // error right now
                //Console.WriteLine($"Position after update: {movableBlock14.blockPosition}");
            }

          

            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true; // loop the music
            }

            if (Keyboard.GetState().IsKeyDown(Keys.G) || _link.win)
            {
                currentGameStage = GameStage.End;
            }

            _link.SetExplosionCoords(Vector2.Zero);
        }

        public void NextStage()
        {

            StageIndex = _nextStageDecider2.DecideStage();
        }
        public void Draw()
        {
            if (!StageAnimating)
            {
                _DrawDungeon2.Draw(Vector2.Zero, false, StageIndex);
                if (drawHitboxes)
                {
                    DebugDraw.DrawHitboxes(_spriteBatch, _link, _EnemyItem, StageIndex, _scale, _link.BulletManager);
                    DebugDraw.DrawHitboxes(_spriteBatch, _link2, _EnemyItem, StageIndex, _scale, _link2.BulletManager);
                }

                if (StageIndex == 14)
                {
                    if (movableBlock14 != null)
                    {
                        movableBlock14.Draw(_spriteBatch, movableBlock14.blockPosition);
                        Debug.WriteLine("Drawing movable block 14");
                    }

                }

                if (StageIndex == 8)
                {
                    if (movableBlock8 != null)
                    {
                        Console.WriteLine("Block position: " + movableBlock8.blockPosition);
                        movableBlock8.Draw(_spriteBatch, movableBlock8.blockPosition);
                        Debug.WriteLine("Drawing movable block 8");
                        _spriteBatch.Draw(new Texture2D(_graphicsDevice, 1, 1), new Rectangle((int)movableBlock8.blockPosition.X, (int)movableBlock8.blockPosition.Y, 50, 50), Color.Red);
                    }
                }
            }
            else
            {
                _StageAnimator2.Draw();
            }
        }


        public void Animate(int currentStage, int nextStage, int direction)
        {
            StageAnimating = true;
            if (direction <= 2)
            {
                AnimatingCount = 255;
            }
            else
            {
                AnimatingCount = 176;
            }

            _StageAnimator2.Animate(currentStage, nextStage, direction);
        }

        public bool GetAnimationState()
        {
            return StageAnimating;
        }

        public int GetCurrentStage()
        {
            return StageIndex;
        }
    }
}