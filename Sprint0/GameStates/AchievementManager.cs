using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.GameStates
{
    public class AchievementManager
    {
        private List<Achievement> achievements;
        private List<Achievement> unlockedAchievements;
        private List<Achievement> alreadyPrintedAchievements;
        public int EnemyDefeatedCount { get; private set; }
        private float achievementVisibleTime = 10f; // achievements are visible for 10 seconds   
        private float achievementTimer = 0f;
        private Link _link;
        public Vector2 _scale;
        private Dictionary<Achievement, float> achievementTimers;

        public AchievementManager(Link link, Vector2 scale)
        {
            achievements = new List<Achievement>();
            unlockedAchievements = new List<Achievement>();
            alreadyPrintedAchievements = new List<Achievement>();
            _link = link;
            _scale = scale;
            achievementTimers = new Dictionary<Achievement, float>();
        }

        public void AddAchievement(Achievement achievement)
        {
            achievements.Add(achievement);
            Debug.WriteLine("number: " + achievements.Count);
        }

        public void Update(GameTime gameTime)
        {
            Debug.WriteLine($"Updating achievement: {achievements}");
            foreach (var achievement in achievements)
            {
                achievement.Update();
                if (achievement.IsUnlocked && !unlockedAchievements.Contains(achievement))
                {
                    unlockedAchievements.Add(achievement);
                    achievementTimers[achievement] = 0f;
                }
            }

            // Update timers for unlocked achievements
            var achievementsToRemove = new List<Achievement>();
            foreach (var achievement in unlockedAchievements)
            {
                achievementTimers[achievement] += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (achievementTimers[achievement] >= achievementVisibleTime)
                {
                    achievementsToRemove.Add(achievement); // Mark for removal
                }
            }

            // Remove achievements whose timers have expired
            foreach (var achievement in achievementsToRemove)
            {
                unlockedAchievements.Remove(achievement);
                achievementTimers.Remove(achievement); // Clean up timer
                alreadyPrintedAchievements.Add(achievement); // Prevent re-drawing
            }   

            //achievementTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if (achievementTimer >= achievementVisibleTime)
            //{
            //    unlockedAchievements.Clear();
            //    alreadyPrintedAchievements.Clear();
            //    achievementTimer = 0f;
            //}
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, GraphicsDevice graphicsDevice)
        {
            //spriteBatch.Begin();
            if (achievements.Count > 0)
            {
                Debug.WriteLine($"Link's position: {_link._position.X}, {_link._position.Y}");
                //Vector2 basePosition = new Vector2(graphicsDevice.Viewport.Width - 300, 50);
                Vector2 basePosition = new Vector2(623, 648);
                //Vector2 basePosition = new Vector2(_link._position.X * _scale.X, _link._position.Y * _scale.Y);
                int yOffset = 0;

                spriteBatch.Draw(new Texture2D(graphicsDevice, 1, 1), new Rectangle((int)basePosition.X, (int)basePosition.Y, 50, 50), Color.Red);

                foreach (var achievement in unlockedAchievements)
                {
                    Debug.WriteLine($"Drawing unlocked achievement: {achievement.Name}");
                    spriteBatch.DrawString(font,
                        $"Achievement: {achievement.Name}",
                        basePosition + new Vector2(2, yOffset + 2),
                        Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 0); // Shadow
                    spriteBatch.DrawString(font,
                        $"Achievement: {achievement.Name}",
                        basePosition + new Vector2(0, yOffset),
                        Color.Yellow, 0, Vector2.Zero, 2f, SpriteEffects.None, 0); // Text
                    yOffset += 30;
                }

                //foreach (var achievement in achievements)
                //{
                //    if (achievement.IsUnlocked && !alreadyPrintedAchievements.Contains(achievement))
                //    {
                //        Debug.WriteLine($"Achievement '{achievement.Name}' unlocked and being drawn.");
                //        spriteBatch.DrawString(font,
                //          $"Achievement: {achievement.Name}",
                //          basePosition + new Vector2(2, yOffset + 2),
                //          Color.Black, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);  // Shadow   
                //        spriteBatch.DrawString(font,
                //          $"Achievement: {achievement.Name}",
                //          basePosition + new Vector2(0, yOffset),
                //          Color.Yellow, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);  // Text   
                //        yOffset += 30;
                //        Debug.Write("Achievement: " + achievement.Name);
                //        alreadyPrintedAchievements.Add(achievement);
                //    }
                //}

                //spriteBatch.Draw(new Texture2D(graphicsDevice, 1, 1), new Rectangle((int)basePosition.X, (int)basePosition.Y, 100, 50), Color.Red);
                //spriteBatch.End();
            }
        }
    }
}