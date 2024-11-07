using Microsoft.Xna.Framework.Input;
using Sprint0.Player;
using Sprint2.GameStates;
using Sprint2.Map;
using System.Diagnostics;


namespace Sprint0.Classes
{
    public class KeyboardController
    {
        KeyboardState previousState;
        public Link _link;
        private StageManager _StageManager;
         

        public KeyboardController(Link link, StageManager stageManager)
        {
            _link = link;
            _StageManager = stageManager;
   
        }

      

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape) && previousState.IsKeyUp(Keys.Escape))
            {
                if (_StageManager.currentGameStage == GameStage.Dungeon)
                {
                    _StageManager.currentGameStage = GameStage.PauseMenu;
                }
                else if (_StageManager.currentGameStage == GameStage.PauseMenu)
                {
                    _StageManager.currentGameStage = GameStage.Dungeon;
                }
            }

            if (_StageManager.currentGameStage == GameStage.Dungeon)

            {
                    if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
                    {
                        _link.MoveDown();
                    }
                    else if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
                    {
                        _link.MoveUp();
                    }
                    else if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
                    {
                        _link.MoveLeft();
                    }
                    else if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
                    {
                        _link.MoveRight();
                    }

                    if (state.IsKeyDown(Keys.Z))
                    {
                        _link.SwordAttack();
                    }
                    else if (state.IsKeyDown(Keys.D1))
                    {
                        _link.ArrowAttack();
                    }
                    else if (state.IsKeyDown(Keys.D2))
                    {
                        _link.UseBoomerang();
                    }
                    else if (state.IsKeyDown(Keys.D3))
                    {
                        _link.UseBomb();
                    }


                    if (state.IsKeyDown(Keys.E))
                    {
                        _link.TakeDamage();
                    }
                }
                //if (state.IsKeyDown(Keys.K) && previousState.IsKeyUp(Keys.K))
                //{
                //    _StageManager.NextStage();
                //}

                //// ITEMS AND BLOCKS
                //previousItem = state.IsKeyDown(Keys.U) && previousState.IsKeyUp(Keys.U);
                //nextItem = state.IsKeyDown(Keys.I) && previousState.IsKeyUp(Keys.I);
                //previousBlock = state.IsKeyDown(Keys.T) && previousState.IsKeyUp(Keys.T);
                //nextBlock = state.IsKeyDown(Keys.Y) && previousState.IsKeyUp(Keys.Y);

                //// ENEMY SWITCHING
                //PreviousEnemy = state.IsKeyDown(Keys.O) && previousState.IsKeyUp(Keys.O);  
                //NextEnemy = state.IsKeyDown(Keys.P) && previousState.IsKeyUp(Keys.P);    

                previousState = state;
            }
        }
    }
