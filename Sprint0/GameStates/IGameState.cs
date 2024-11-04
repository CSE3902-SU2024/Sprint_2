using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.GameStates
{
    enum State
    {
        StartMenu,
        InGame,
        PauseMenu,
        GameOver
    }
    public interface IGameState
    {
        public void Update(GameTime gameTime);
        public void Draw();
        public void LoadContent(ContentManager Content);
        public int GetLinkHealth();
    }
    


}
