using Microsoft.Xna.Framework.Input;
using Sprint0.Player;
using Sprint2.Enemy;
using Sprint2.Map;
using System;
using System.Collections.Generic;


namespace Sprint0.Classes
{
    public class KeyboardController
    {
        KeyboardState previousState;
        public Link _link;
        private Wizzrobe _wizzrobe;
        //  private StageManager _StageManager;


        public KeyboardController(Link link)
        {
            _link = link;
             
        }
        public void SetWizzrobe(Wizzrobe wizzrobe)
        {
            _wizzrobe = wizzrobe;
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
                    if (state.IsKeyDown(Keys.F) && !previousState.IsKeyDown(Keys.F))
                    {

                        System.Diagnostics.Debug.WriteLine($"F Key Pressed");
                        System.Diagnostics.Debug.WriteLine($"Wizzrobe Null: {_wizzrobe == null}");
                        //check if wizzrobe is in the room
                        if (_wizzrobe != null)
                        {
                            System.Diagnostics.Debug.WriteLine($"CanInteract: {_wizzrobe.CanInteract}");
                            System.Diagnostics.Debug.WriteLine($"ChatBox Null: {_wizzrobe.chatBox == null}");

                            if (_wizzrobe.chatBox != null)
                            {
                                System.Diagnostics.Debug.WriteLine($"ChatBox Visible: {_wizzrobe.chatBox.IsVisible}");
                                System.Diagnostics.Debug.WriteLine($"ChatBox Conversation Complete: {_wizzrobe.chatBox.IsConversationComplete}");
                            }

                            // If chatbox is visible, explicitly call AdvanceConversation
                            if (_wizzrobe.chatBox != null && _wizzrobe.chatBox.IsVisible)
                            {
                                _wizzrobe.AdvanceConversation();
                            }
                            else
                            {
                                _wizzrobe.StartConversation();
                            }
                        }
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
                    if (state.IsKeyDown(Keys.P) && !previousState.IsKeyDown(Keys.P))
                    {
                        _link.inventory.CycleSelectedItem();
                    }
                    else if (state.IsKeyDown(Keys.I) && !previousState.IsKeyDown(Keys.I))
                    {
                        _link.inventory.ToggleInventory();
                        return 2;
                    }
                    else if (state.IsKeyDown(Keys.K) && !previousState.IsKeyDown(Keys.K))
                    {
                        _link.IncrementKey();
              
                    }

                    break;
                // Inventory state
                case 2:
                    if (state.IsKeyDown(Keys.P) && !previousState.IsKeyDown(Keys.P))
                    {
                        _link.inventory.CycleSelectedItem();
                    }
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