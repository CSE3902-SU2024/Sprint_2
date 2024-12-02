using Microsoft.Xna.Framework.Input;
using Sprint0.Player;
using Sprint2.Enemy;
using Sprint2.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Sprint0.Classes
{
    public class KeyboardController
    {
        KeyboardState previousState;
        public Link _link;
        public Link _link2;
        private List<Wizzrobe> _wizzrobes;
        int previousIdx;


        public KeyboardController(Link link, Link link2)
        {
            _link = link;
            _link2 = link2;
            _wizzrobes = new List<Wizzrobe>();
            previousIdx = 0;

        }
        public void SetWizzrobe(Wizzrobe wizzrobe)
        {
            if (!_wizzrobes.Contains(wizzrobe))
            {
                _wizzrobes.Add(wizzrobe);
            }
        }
        
        public int Update(int GameStateIndex)
        {
            KeyboardState state = Keyboard.GetState();

            switch (GameStateIndex)
            {
                // Start Menu
                case 0:
                    if (state.IsKeyDown(Keys.D1))
                    {
                        // Start => Game
                        previousIdx = 1;
                        return 1;
                    }
                    else if (state.IsKeyDown(Keys.D2))
                    {
                        previousIdx = 2;
                        return 2;
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

                        
                            foreach (var wizzrobe in _wizzrobes)
                            {
                                if (wizzrobe.CanInteract)
                                {
                                    if (wizzrobe.chatBox != null && wizzrobe.chatBox.IsVisible)
                                    {
                                        wizzrobe.AdvanceConversation();
                                    }
                                    else
                                    {
                                        wizzrobe.StartConversation();
                                    }
                                    break; // Only interact with one Wizzrobe at a time
                                }
                            }
                     }
                        if (state.IsKeyDown(Keys.Z))
                    {
                        _link.SwordAttack();
                    }
                    else if (state.IsKeyDown(Keys.D1))
                    {
                        if (_link.inventory?.SelectedItem?.CurrentItemType == ItemType.bow)
                        {
                            _link.ArrowAttack();
                        }
                    }
                    else if (state.IsKeyDown(Keys.D2))
                    {
                        _link.UseBoomerang();
                    }
                    else if (state.IsKeyDown(Keys.D3))
                    {
                        if (_link.inventory?.SelectedItem?.CurrentItemType == ItemType.boom)
                        {
                            _link.UseBomb();
                        }
                    }
                    else if (state.IsKeyDown(Keys.Space))
                    {
                        // Game => Pause
                        previousIdx = 1;
                        return 5;
                    }
                    if (state.IsKeyDown(Keys.P) && !previousState.IsKeyDown(Keys.P))
                    {
                        _link.inventory.CycleSelectedItem();
                    }
                    else if (state.IsKeyDown(Keys.I) && !previousState.IsKeyDown(Keys.I))
                    {
                        _link.inventory.ToggleInventory();
                        previousIdx = 1;
                        return 3;
                    }
                    else if (state.IsKeyDown(Keys.K) && !previousState.IsKeyDown(Keys.K))
                    {
                        _link.IncrementKey();
              
                    }
                    break;

                case 2:
                    if (state.IsKeyDown(Keys.B))
                    {

                        return 0;
                    }
                    if (state.IsKeyDown(Keys.D1))
                    {
                        previousIdx = 4;
                        Debug.WriteLine("To two Player");
                        return 11;
                    }
                    

                    break;
                // Inventory state
                case 3:
                    if (state.IsKeyDown(Keys.P) && !previousState.IsKeyDown(Keys.P))
                    {
                        _link.inventory.CycleSelectedItem();
                    }
                    if (state.IsKeyDown(Keys.Escape))
                    {
                        // Inventory => Game  
                        _link.inventory.ToggleInventory();
                        if(previousIdx == 1)
                        {
                            return 1;
                        } else
                        {
                            return 4;
                        }
                       
                    }
                    break;


                case 4: // Two PlayerMode

                    //Player2 Controls
                     if (state.IsKeyDown(Keys.Down))
                    {
                        _link2.MoveDown();
                    }
                    else if (state.IsKeyDown(Keys.Up))
                    {
                        _link2.MoveUp();
                    }
                    else if (state.IsKeyDown(Keys.Left))
                    {
                        _link2.MoveLeft();
                    }
                    else if (state.IsKeyDown(Keys.Right))
                    {
                        _link2.MoveRight();
                    }

                    if (state.IsKeyDown(Keys.S))
                    {
                        _link.MoveDown();
                    }
                    else if (state.IsKeyDown(Keys.W))
                    {
                        _link.MoveUp();
                    }
                    else if (state.IsKeyDown(Keys.A))
                    {
                        _link.MoveLeft();
                    }
                    else if (state.IsKeyDown(Keys.D))
                    {
                        _link.MoveRight();
                    }
                    if (state.IsKeyDown(Keys.F) && !previousState.IsKeyDown(Keys.F))
                    {

                            foreach (var wizzrobe in _wizzrobes)
                            {
                                if (wizzrobe.CanInteract)
                                {
                                    if (wizzrobe.chatBox != null && wizzrobe.chatBox.IsVisible)
                                    {
                                        wizzrobe.AdvanceConversation();
                                    }
                                    else
                                    {
                                        wizzrobe.StartConversation();
                                    }
                                    break; // Only interact with one Wizzrobe at a time
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
                        previousIdx = 4;
                        return 5;
                    }
                    if (state.IsKeyDown(Keys.P) && !previousState.IsKeyDown(Keys.P))
                    {
                        _link.inventory.CycleSelectedItem();
                    }
                    else if (state.IsKeyDown(Keys.I) && !previousState.IsKeyDown(Keys.I))
                    {
                        _link.inventory.ToggleInventory();
                        previousIdx = 4;
                        return 3;
                    }
                    else if (state.IsKeyDown(Keys.K) && !previousState.IsKeyDown(Keys.K))
                    {
                        _link.IncrementKey();
              
                    }
                    break;
                // Pause menu
                case 5:
                    if (state.IsKeyDown(Keys.Escape))
                    {
                        // Pause => Game
                        if(previousIdx == 1)
                        {
                            return 1;
                        } else
                        {
                            return 4;
                        }
                        
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