using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.GameStates
{
    public class AchievementManager
    {
        private List<Achievement> achievements;
        public int EnemyDefeatedCount { get; private set; }
        private float achievementVisibleTime = 5f; // achievements are visible for 5 seconds  
        private float achievementTimer = 0f;


        public AchievementManager()
        {
            achievements = new List<Achievement>();
        }

        public void AddAchievement(Achievement achievement)
        {
            achievements.Add(achievement);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var achievement in achievements)
            {
                achievement.Update();
            }
            achievementTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (achievementTimer >= achievementVisibleTime)
            {
                achievements.Clear();
                achievementTimer = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, GraphicsDevice graphicsDevice)
        {
            // Optionally render unlocked achievements on the screen
            //int yOffset = 10;
            //foreach (var achievement in achievements)
            //{
            //    string status = achievement.IsUnlocked ? "Unlocked" : "Locked";
            //    spriteBatch.DrawString(font, $"{achievement.Name}: {status}", new Vector2(10, yOffset), Color.White);
            //    yOffset += 20;
            //}

            int yOffset = graphicsDevice.Viewport.Height - 20;
            foreach (var achievement in achievements)
            {
                string status = achievement.IsUnlocked ? "Unlocked" : "Locked";
                spriteBatch.DrawString(font, $"{achievement.Name}: {status}", new Vector2(10, yOffset), Color.White);
                yOffset -= 20;
            }
        }
    }
}