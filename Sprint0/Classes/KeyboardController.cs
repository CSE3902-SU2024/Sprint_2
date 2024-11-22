using Microsoft.Xna.Framework.Input;
using Sprint0.Player;


namespace Sprint0.Classes
{
    public class KeyboardController
    {
        KeyboardState previousState;
        public Link _link;
      //  private StageManager _StageManager;
   

       public KeyboardController(Link link) //,StageManager stageManager )
        {
            _link = link;
         //   _StageManager = stageManager;
        }
        public int Update(int GameStateIndex)
        {
            KeyboardState state = Keyboard.GetState();

            switch (GameStateIndex)
            {
                // Start Menu
                case 0:
                    if (Keyboard.GetState().GetPressedKeys().Length > 0)
                    {
                        // Start => Game
                        return 1;
                    }
                    break;
                // Game state
                case 1:
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
                    else if (state.IsKeyDown(Keys.Space))
                    {
                        // Game => Pause
                        return 5;
                    }
                    else if (state.IsKeyDown(Keys.I) && !previousState.IsKeyDown(Keys.I))
                    {
                        // Game => Inventory  
                        _link.inventory.ToggleInventory();
                        return 2;
                    }
                    break;
                // Inventory state
                case 2:
                    if (state.IsKeyDown(Keys.Escape))
                    {
                        // Inventory => Game  
                        _link.inventory.ToggleInventory();
                        return 1;
                    }
                    break;
                // Pause menu
                case 5:
                    if (state.IsKeyDown(Keys.Escape))
                    {
                        // Pause => Game
                        return 1;
                    }
                    break;
                default:
                    break;
            }

            previousState = state;
            return GameStateIndex; // Return current state if no change
        }
    }
}