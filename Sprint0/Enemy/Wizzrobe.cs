﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;
using System;
using Sprint0.Player;
using Sprint2.UI;
using System.Collections.Generic;

namespace Sprint2.Enemy
{
    public class Wizzrobe : IEnemy
    {
        private Texture2D spriteSheet;
        private Rectangle[] sourceRectangles;
        private Vector2 position;
        private Vector2 initialPosition;
        private int currentFrame;
        private float timeElapsed;
        private Vector2 _scale;
        private Boolean alive;
        public ChatBox chatBox;
        private bool isPlayerNearby = false;
        private bool isActiveConversation = false;
        private const float INTERACTION_DISTANCE = 32f;  
        public Link _link;


        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; } = 16;
        public int Height { get; } = 16;



        private Dictionary<int, string[]> stageConversations = new Dictionary<int, string[]>
    {
        //convo for dungeon 0... cont
        { 0, new string[] {
            "Hello traveler (F to continue)",
            "Welcome to the Dungeon",
            "Pick up your weapon"
        }},
         { 1, new string[] {
            "This is the first dungeon...",
            "Be careful ahead",
            "Monsters are everywhere"
        }},
         { 2, new string[] {
            "yes",
            "yes",
            "yes"
        }}
        };
        private string defaultFinalMessage = "Go, traveler";

        private Dictionary<int, string> stageFinalMessages = new Dictionary<int, string>
    {
        { 0, "Go, traveler" },
        { 1, "Good luck in this dungeon" },
        { 2, "The path ahead is treacherous" }
    };

        private int _currentStage;


        private bool canInteract;
        private bool wasNearby;

        public Wizzrobe(Vector2 startPosition, Link link, int currentStage)
        {
            position = startPosition;
            initialPosition = startPosition;
            alive = true;
            _link = link;
            isPlayerNearby = false;
            _currentStage = currentStage;


        }

        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice, Vector2 scale)
        {
            spriteSheet = content.Load<Texture2D>(texturePath);
         
            sourceRectangles = SpriteSheetHelper.CreateWizzrobeFrames();
            _scale = scale;
            chatBox = new ChatBox(graphicsdevice, content, scale);
        }
        

        public void Update(GameTime gameTime)
        {
            if (alive && chatBox != null)
            {
               
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                  
                if (timeElapsed > 0.2f)
                {
                    currentFrame = (currentFrame + 1) % sourceRectangles.Length;
                    timeElapsed = 0f;
                }
                float distance = Vector2.Distance(_link._position, position);

                //ISSUE IS SOMEWHERE RIGHT HERE
                wasNearby = canInteract;

                canInteract = distance < 32 * _scale.X; // Interaction distance

                //SOMEWHERE HERE

                System.Diagnostics.Debug.WriteLine($"Player Position: {_link._position}, Wizzrobe Position: {Position}, Distance: {distance}");
                if (canInteract && !wasNearby)
                {
                    wasNearby = true;
                    StartConversation();
                }
                else if (!canInteract && wasNearby)
                {
                    isActiveConversation = false;
                    chatBox.Hide();
                    wasNearby = false;

                }

                chatBox?.Update();



            }
            }
        public void ResetConversationState()
        {
            wasNearby = false;
            canInteract = false;
            isActiveConversation = false;

            chatBox?.Hide();
        }
        public bool CanInteract => canInteract;

        public void StartConversation()
        {
            if (chatBox != null)   
            {
                string[] conversationLines = stageConversations.ContainsKey(_currentStage)
               ? stageConversations[_currentStage]
               : stageConversations[0];
                string finalMessage = stageFinalMessages.ContainsKey(_currentStage)
                ? stageFinalMessages[_currentStage]
                : defaultFinalMessage;

                isActiveConversation = true;
                System.Diagnostics.Debug.WriteLine($"Conversation Lines: {conversationLines.Length}");
                System.Diagnostics.Debug.WriteLine($"Final Message: {finalMessage}");
                chatBox.StartConversation(conversationLines, finalMessage, position);

            }
        }

        public void AdvanceConversation()
        {
            System.Diagnostics.Debug.WriteLine($"AdvanceConversation called. CanInteract: {CanInteract}");
            System.Diagnostics.Debug.WriteLine($"ChatBox is null: {chatBox == null}");

            if (chatBox != null && isActiveConversation)
            {
                bool conversationContinues = chatBox.AdvanceConversation(position);


                if (!conversationContinues)
                {
                    isActiveConversation = false;
                    chatBox.Hide();

                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
                chatBox?.Draw(spriteBatch);
            }
        }
        

        
        public void TakeDamage()
        {
            // NPCs don't take dmg
        }

        public Boolean GetState()
        {
            return alive;
        }

        public void Reset()
        {
            position = initialPosition;
            currentFrame = 0;
            timeElapsed = 0f;
            
        }
    }
}