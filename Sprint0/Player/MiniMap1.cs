﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint2.Map;
using Sprint0.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace Sprint2.Player
{
    public class MiniMap1
    {
        Texture2D _MiniMap;
        int currentStage;
        Vector2 _scale;
        Vector2 Base;
        Vector2 currentCorner;
        Vector2 _linkPosition;
        Vector2 IconPosition;

        Vector2 TriForcePosition;
        Rectangle TriForce;
        Rectangle map;
        Rectangle linkIcon;
        StageManager stageManager;
        List<Vector2> StagePosition;
        Link _link;
        Boolean Flicker;
        int FlickerCount;
        public MiniMap1(Vector2 scale, StageManager StageManager, Link link)
        {
            Base = new Vector2(10 * scale.X, 10 * scale.Y);
            TriForcePosition = new Vector2(67 *scale.X, 21 *scale.Y);
            _scale = scale;
            stageManager = StageManager;
            _link = link;
            _linkPosition = link.GetLocation();
            Flicker = false;
            FlickerCount = 10;
            // Corner = (0,1) 64x40
            map = new Rectangle(1,0,63,40);
            linkIcon = new Rectangle(69, 4, 1, 1);
            TriForce = new Rectangle(69, 7, 1, 1);

            StagePosition = new List<Vector2>();
            StagePosition.Add(new Vector2(24, 33)); // 1
            StagePosition.Add(new Vector2(14, 33)); // 2
            StagePosition.Add(new Vector2(34, 33)); // 3
            StagePosition.Add(new Vector2(24, 27));   //4
            StagePosition.Add(new Vector2(24, 21)); //5
            StagePosition.Add(new Vector2(14, 21)); //6
            StagePosition.Add(new Vector2(34, 21)); //7
            StagePosition.Add(new Vector2(14, 15)); //8
            StagePosition.Add(new Vector2(4, 15)); //9
            StagePosition.Add(new Vector2(24, 15)); //10
            StagePosition.Add(new Vector2(34, 15)); // 11
            StagePosition.Add(new Vector2(24, 9)); //12
            StagePosition.Add(new Vector2(24, 3)); //13
            StagePosition.Add(new Vector2(14, 3)); //14
            StagePosition.Add(new Vector2(44, 15)); // 15
            StagePosition.Add(new Vector2(44, 9)); //16
            StagePosition.Add(new Vector2(54, 9)); //17 
            //StagePosition.Add(new Vector2(54, 9)); //18
         
        }

        public void LoadMap(ContentManager content)
        {
            
            _MiniMap = content.Load<Texture2D>("MiniMap_Level1");

        }

        public void Update()
        {
            currentStage = stageManager.GetCurrentStage();
            if(currentStage > 0)
            {
                currentCorner = StagePosition.ElementAt(currentStage -1);
                currentCorner.X = (currentCorner.X - 1) * _scale.X;
                currentCorner.Y = currentCorner.Y * _scale.Y;
            }
            
            _linkPosition = _link.GetLocation();
            //IconPosition = DecodePosition(_linkPosition);
            IconPosition = Base + currentCorner + DecodePosition(_linkPosition);

            FlickerCount -= 1;
            if (FlickerCount < 0)
            {
                FlickerCount = 10;
                Flicker = !Flicker;
            }
            
        }

        public Vector2 DecodePosition(Vector2 _linkPosition)
        {
            Vector2 result = new Vector2();
            
            if (_linkPosition.Y < 109 * _scale.Y)
            {
                result.Y = 0;
            }
            else if (_linkPosition.Y >= 109 * _scale.Y && _linkPosition.Y <= 133 * _scale.Y)
            {
                result.Y = 1 * _scale.Y;
            }
            else if (_linkPosition.Y >= 133 * _scale.Y && _linkPosition.Y <= 155 * _scale.Y)
            {
                result.Y = 2 * _scale.Y;
            }
            else if (_linkPosition.Y >= 155 * _scale.Y && _linkPosition.Y <= 177 * _scale.Y)
            {
                result.Y = 3 * _scale.Y;
            }
            else if (_linkPosition.Y >= 177 * _scale.Y)
            {
                result.Y = 4* _scale.Y;
            }

            if (_linkPosition.X< 53 * _scale.X)
            {
                result.X = 0;
            }
            else if (_linkPosition.X >= 53 * _scale.X && _linkPosition.X <= 74 * _scale.X)
            {
                result.X = 1 * _scale.X;
            }
            else if (_linkPosition.X >= 74 * _scale.X && _linkPosition.X <= 95 * _scale.X)
            {
                result.X = 2 * _scale.X;
            }
            else if (_linkPosition.X >= 95 * _scale.X && _linkPosition.X <= 116 * _scale.X)
            {
                result.X = 3 * _scale.X;
            }
            else if (_linkPosition.X >= 116 * _scale.X && _linkPosition.X <= 137 * _scale.X)
            {
                result.X = 4 * _scale.X;
            }
            else if (_linkPosition.X >= 137 * _scale.X && _linkPosition.X <= 158 * _scale.X)
            {
                result.X = 5 * _scale.X;
            }
            else if (_linkPosition.X >=158 * _scale.X && _linkPosition.X <= 179 * _scale.X)
            {
                result.X = 6 * _scale.X;
            }
            else if (_linkPosition.X >= 179 * _scale.X && _linkPosition.X <= 200 * _scale.X)
            {
                result.X = 7 * _scale.X;
            }
            else if (_linkPosition.X >= 200 * _scale.X)
            {
                result.X = 8 * _scale.X;
            }

            return result;
        }   
        
        public void Draw(SpriteBatch _spriteBatch)
        {
            if(currentStage > 0 && _link.hasMap)
            {
                _spriteBatch.Draw(_MiniMap, Base, map, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
                if (Flicker)
                {
                    _spriteBatch.Draw(_MiniMap, IconPosition, linkIcon, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
                    if (_link.hasCompass)
                    {
                        _spriteBatch.Draw(_MiniMap, TriForcePosition, TriForce, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
                    }
                }
            }
            
        }

    
    }
}





//        // Calculate the range size for each segment
//        int rangeSize = (224 - 32 + 1) / 5; // This will be 39

//// Determine the output value based on the segment
//return (input - 32) / rangeSize;