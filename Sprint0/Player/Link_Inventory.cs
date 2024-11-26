using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Sprint2.Classes;
using static Sprint2.Classes.Iitem;
using Microsoft.Xna.Framework.Content;

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
            currentItemType = ItemType.bow;

        }

        public void AddItem(Iitem item)
        {
            // Check if the item is already in the inventory
            bool itemExists = false;
            foreach (var existingItem in bagItems)
            {
                if (existingItem.CurrentItemType == item.CurrentItemType)
                {
                    itemExists = true;
                    break;
                }
            }

            
            if (!itemExists)
            {
                bagItems.Add(item);

                // If this is the first item, select it
                if (selectedIndex == -1)
                {
                    selectedIndex = 0;
                    selectedItem = item;
                }
            }
        }

        public void ToggleInventory()
        {
            isInventoryOpen = !isInventoryOpen;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 transitionPosition)
        {
            if (!isInventoryOpen || bagItems.Count == 0) return;

            // Base positions 
            Vector2 startPos = new Vector2(550, 230);

            const int ITEMS_PER_ROW = 4;
            const int ROW_SPACING = 95;

            // Draw each item
            for (int i = 0; i < bagItems.Count; i++)
            {
                int row = i / ITEMS_PER_ROW;
                int col = i % ITEMS_PER_ROW;

                // transtion for item
                Vector2 itemPosition = new Vector2(
                    transitionPosition.X + startPos.X + (col * ITEM_SPACING),
                    transitionPosition.Y + startPos.Y + (row * ROW_SPACING)
                );

                spriteBatch.Draw(
                    bagItems[i].Sprite,
                    itemPosition,
                    bagItems[i].SourceRectangles[0],
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
            if (bagItems.Count > 0)
            {
                // Move to next item index
                selectedIndex = (selectedIndex + 1) % bagItems.Count;
                selectedItem = bagItems[selectedIndex];

                // Update the current item type
                if (selectedItem != null)
                {
                    currentItemType = selectedItem.CurrentItemType;

                    //  add logic in work
                    // playing some sound when switching item...
                }
            }
        }

        public Iitem GetCurrentItem()
        {
            if (bagItems.Count > 0 && selectedIndex >= 0 && selectedIndex < bagItems.Count)
            {
                return bagItems[selectedIndex];
            }
            return null;
        }
        public List<Iitem> GetItems()
        {
            return bagItems;
        }
    }
}