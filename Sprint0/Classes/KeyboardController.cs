using Microsoft.Xna.Framework.Input;
using Sprint0.Player;
using Sprint2.Map;


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
        public bool previousItem { get; private set; }
        public bool nextItem { get; private set; }
        public bool previousBlock { get; private set; }
        public bool nextBlock { get; private set; }

        public bool PreviousEnemy { get; private set; }
        public bool NextEnemy { get; private set; }

        public Link _link;
        private StageManager _StageManager;

       public KeyboardController(Link link, StageManager stageManager )
        {
            _link = link;
            _StageManager = stageManager;
        }
        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

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
            } else if (state.IsKeyDown(Keys.D3))
            {
                _link.UseBomb();
            }


            if (state.IsKeyDown(Keys.E))
            {
                _link.TakeDamage();
            }

            
            if (state.IsKeyDown(Keys.K))
            {
                _StageManager.nextStage();
            }



           


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