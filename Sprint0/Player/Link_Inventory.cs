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
        private const int ITEM_SPACING = 110;
        

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

            // If the item doesn't exist in the inventory, add it (may be can be increment the count of the item later I think)
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

        public void Draw(SpriteBatch spriteBatch)
        {
            // Debug output to check if method is being called
            

            if (!isInventoryOpen || bagItems.Count == 0) return;

            //  fixed positions  for  testing
            Vector2 startPos = new Vector2(550, 230); // Test purpose position (change later)
            const int ITEMS_PER_ROW = 4;
            const int ROW_SPACING = 95;

            // Draw each item
            for (int i = 0; i < bagItems.Count; i++)
            {
                int row = i / ITEMS_PER_ROW;
                int col = i % ITEMS_PER_ROW;

                Vector2 itemPosition = new Vector2(
                    startPos.X + (col * ITEM_SPACING),
                    startPos.Y + (row * ROW_SPACING)
                );

                

                // item drawing
                spriteBatch.Draw(
                    bagItems[i].Sprite,
                    itemPosition,
                    bagItems[i].SourceRectangles[0],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    _scale * 1.2f, // change scale accordingly
                    SpriteEffects.None,
                    0f
                );
            }
        }

        public void CycleSelectedItem()
        {
            if (bagItems.Count > 0)
            {
                selectedIndex = (selectedIndex + 1) % bagItems.Count;
                selectedItem = bagItems[selectedIndex];
            }
        }

        public List<Iitem> GetItems()
        {
            return bagItems;
        }
    }
}