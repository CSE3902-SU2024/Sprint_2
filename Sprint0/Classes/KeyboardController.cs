using Microsoft.Xna.Framework.Input;

namespace Sprint0.Classes
{
    public class KeyboardController
    {
        KeyboardState previousState;
        public bool IsMovingLeft { get; private set; }
        public bool IsMovingRight { get; private set; }
        public bool IsMovingUp { get; private set; }
        public bool IsMovingDown { get; private set; }
        public bool SwordAttack { get; private set; }
        public bool TakeDamage { get; private set; }
        public bool IsFirePressed { get; private set; }

        public bool previousItem { get; private set; }
        public bool nextItem { get; private set; }
        public bool previousBlock { get; private set; }
        public bool nextBlock { get; private set; }

        public bool PreviousEnemy { get; private set; }
        public bool NextEnemy { get; private set; }
        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            
            // BASIC MOVEMENT
            IsMovingLeft = state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A);
            IsMovingRight = state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D);
            IsMovingUp = state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W);
            IsMovingDown = state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S);

            // ATTACK
            SwordAttack = state.IsKeyDown(Keys.Z);
            TakeDamage = state.IsKeyDown(Keys.E);
            IsFirePressed = state.IsKeyDown(Keys.D1); // Use Space to fire arrows


            // ITEMS AND BLOCKS
            previousItem = state.IsKeyDown(Keys.U) && previousState.IsKeyUp(Keys.U);
            nextItem = state.IsKeyDown(Keys.I) && previousState.IsKeyUp(Keys.I);
            previousBlock = state.IsKeyDown(Keys.T) && previousState.IsKeyUp(Keys.T);
            nextBlock = state.IsKeyDown(Keys.Y) && previousState.IsKeyUp(Keys.Y);

            // ENEMY SWITCHING
            PreviousEnemy = state.IsKeyDown(Keys.O) && previousState.IsKeyUp(Keys.O);  
            NextEnemy = state.IsKeyDown(Keys.P) && previousState.IsKeyUp(Keys.P);    

            previousState = state;
        }
    }
}