using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.GameStates
{
    public class PauseMenu : IGameState
    {
        public PauseMenu()
        {

        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void Update(GameTime gameTime)
        {
        }

        public void UpdatePauseMenu(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
        }

        public void Draw()
        {
        }

        public void DrawPauseMenu()
        {

        }

        public int GetLinkHealth()
        {
            return 1;
        }
    }
}
