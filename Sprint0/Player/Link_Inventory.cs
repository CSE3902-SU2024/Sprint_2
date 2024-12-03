using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Sprint2.Classes;
using static Sprint2.Classes.Iitem;
using Microsoft.Xna.Framework.Content;
using System.Linq;

namespace Sprint0.Player
{
    public class Link_Inventory
    {
        private Link _link;
        private List<Iitem> bagItems;
        private Iitem selectedItem;
        private bool isInventoryOpen;
        private int selectedIndex;
        private Vector2 _scale;
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;

        //  inventory layout
        private const int ITEM_SPACING = 105;
        private ItemType currentItemType;  

        //  access current item type
        public ItemType CurrentItemType => currentItemType;
        private int BombCount;
        private int GemCount;
        private int KeyCount;
        public bool IsInventoryOpen => isInventoryOpen;
        public Iitem SelectedItem => selectedItem;

        public Link_Inventory(Link link, SpriteBatch spriteBatch, Vector2 scale, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _link = link;
            _spriteBatch = spriteBatch;
            _scale = scale;
            _graphicsDevice = graphicsDevice;
            bagItems = new List<Iitem>();
            isInventoryOpen = false;
            selectedIndex = -1;
            currentItemType = ItemType.boom;
            BombCount = 3;
            GemCount = 0;
            KeyCount = 0;

        }

        public void InitializeStartingItems()
        {
            if (BombCount > 0)
            {
                var bombItem = bagItems.FirstOrDefault(item => item.CurrentItemType == ItemType.boom);
                if (bombItem != null)
                {
                    selectedItem = bombItem;
                    selectedIndex = bagItems.IndexOf(bombItem);
                    currentItemType = ItemType.boom;
                }
            }
        }
        public void AddItem(Iitem item)
        {
            bool itemExists = bagItems.Any(existingItem => existingItem.CurrentItemType == item.CurrentItemType);

            if (!itemExists)
            {
                bagItems.Add(item);
                
                if (selectedIndex == -1 || (item.CurrentItemType == ItemType.boom && _link.BombCount > 0))
                {
                    selectedIndex = bagItems.Count - 1;
                    selectedItem = item;
                    currentItemType = item.CurrentItemType;
                }
            }
        }

        public bool ShouldDisplayItem(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.key:
                    return KeyCount > 0;
                case ItemType.boom:
                    return BombCount > 0;
                case ItemType.diamond:
                    return GemCount > 0;    
                default:
                    return true;
            }
        }
        public void UpdateInventoryItems()
        {
         
            var previousType = selectedItem?.CurrentItemType;

            // Update inventory based on counts
            var updatedItems = bagItems.Where(item =>
                (item.CurrentItemType != ItemType.key || KeyCount > 0) &&
                (item.CurrentItemType != ItemType.boom || BombCount > 0) && 
                (item.CurrentItemType != ItemType.diamond || GemCount > 0)).ToList();
                

             
            if (updatedItems.Count != bagItems.Count)
            {
                bagItems = updatedItems;

                // If selected item was removed, select next available item
                if (selectedItem != null && !bagItems.Contains(selectedItem))
                {
                    if (bagItems.Count > 0)
                    {
                        selectedIndex = 0;
                        selectedItem = bagItems[0];
                        currentItemType = selectedItem.CurrentItemType;
                    }
                    else
                    {
                        selectedIndex = -1;
                        selectedItem = null;
                        currentItemType = ItemType.bow;  
                    }
                }
            }
        }


       

        public void ToggleInventory()
        {
            isInventoryOpen = !isInventoryOpen;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 transitionPosition)
        {
            UpdateInventoryItems();  

            if (!isInventoryOpen) return;

            Vector2 startPos = new Vector2(550, 230);
            const int ITEMS_PER_ROW = 4;
            const int ROW_SPACING = 95;

            // Draw  available items
            var availableItems = bagItems.Where(item => ShouldDisplayItem(item.CurrentItemType)).ToList();

            for (int i = 0; i < availableItems.Count; i++)
            {
                int row = i / ITEMS_PER_ROW;
                int col = i % ITEMS_PER_ROW;

                Vector2 itemPosition = new Vector2(
                    transitionPosition.X + startPos.X + (col * ITEM_SPACING),
                    transitionPosition.Y + startPos.Y + (row * ROW_SPACING)
                );

                spriteBatch.Draw(
                    availableItems[i].Sprite,
                    itemPosition,
                    availableItems[i].SourceRectangles[0],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    _scale * 1.2f,
                    SpriteEffects.None,
                    0f
                );
            }
        }

        public void CycleSelectedItem()
        {
            UpdateInventoryItems();
            var availableItems = bagItems.Where(item => ShouldDisplayItem(item.CurrentItemType)).ToList();

            if (availableItems.Count > 0)
            {
                // Find  index in available items
                int currentIndex = availableItems.IndexOf(selectedItem);
                // Cycle to next item
                int nextIndex = (currentIndex + 1) % availableItems.Count;
                selectedItem = availableItems[nextIndex];
                selectedIndex = bagItems.IndexOf(selectedItem);
                currentItemType = selectedItem.CurrentItemType;
            }
        }
        public Iitem GetCurrentItem()
        {
            UpdateInventoryItems();
            return selectedItem;
        }

        public List<Iitem> GetItems()
        {
            UpdateInventoryItems();
            return bagItems.Where(item => ShouldDisplayItem(item.CurrentItemType)).ToList();
        }

        public int GetBombCount()
        {
            return BombCount;
        }

        public void DecrementBombCount()
        {
            BombCount--;
        }

        public void IncrementBombCount()
        {
            BombCount++;
        }

        public int GetGemCount()
        {
            return GemCount;
        }
        public void DecrementGemCount(int deficit)
        {
            GemCount -= deficit;
        }
        public void IncrementGemCount()
        {
            GemCount++;
        }

        public int GetKeyCount()
        {
            return KeyCount;
        }
        public void DecrementKeyCount()
        {
            KeyCount--;
        }
        public void IncrementKeyCount()
        {
            KeyCount++;
        }        
    }
}