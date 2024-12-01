using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.GameStates
{
    public class Achievement
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsUnlocked { get; private set; }

        private Func<bool> UnlockCondition;

        public Achievement(string name, string description, Func<bool> unlockCondition)
        {
            Name = name;
            Description = description;
            IsUnlocked = false;
            UnlockCondition = unlockCondition;
        }

        public void Update()
        {
            if (!IsUnlocked && UnlockCondition())
            {
                IsUnlocked = true;
                OnUnlock();
            }
        }

        private void OnUnlock()
        {
            // Trigger UI notification or other behavior
            Console.WriteLine($"Achievement Unlocked: {Name}");
        }
    }
}
