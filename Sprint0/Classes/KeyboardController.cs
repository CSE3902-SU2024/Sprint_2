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
        public bool previousItem { get; private set; }
        public bool nextItem { get; private set; }
        public bool previousBlock { get; private set; }
        public bool nextBlock { get; private set; }
        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            

            IsMovingLeft = state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A);
            IsMovingRight = state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D);
            IsMovingUp = state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W);
            IsMovingDown = state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S);
            previousItem = state.IsKeyDown(Keys.U) && previousState.IsKeyUp(Keys.U);
            nextItem = state.IsKeyDown(Keys.I) && previousState.IsKeyUp(Keys.I);
            previousBlock = state.IsKeyDown(Keys.T) && previousState.IsKeyUp(Keys.T);
            nextBlock = state.IsKeyDown(Keys.Y) && previousState.IsKeyUp(Keys.Y);

            previousState = state;
        }
    }
}