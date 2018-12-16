using GameEngine.Data.Enums;
using GameEngine.Data.Model;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameEngine.Data.Models
{
    public class SpriteObject
    {
        public SpriteAnimated Sprite { get; private set; }

        public RenderStates State { get; private set; }

        public bool Visible { get; set; }
        public bool IsHovered { get; set; }
        public bool IsClicked { get; private set; }

        private Callback CallbackOnHoverStart;
        private Callback CallbackOnHoverEnd;
        private Callback CallbackOnClick;

        public delegate void Callback(SpriteObject obj);

        public SpriteObject(SpriteAnimated sprite, RenderStates state, 
            Callback callbackOnHoverStart = null, Callback callbackOnHoverEnd = null, Callback callbackOnClick = null)
        {
            Visible = true;
            IsHovered = false;
            IsClicked = false;

            Sprite = sprite;
            State = state;

            CallbackOnHoverStart = callbackOnHoverStart;
            CallbackOnHoverEnd = callbackOnHoverEnd;
            CallbackOnClick = callbackOnClick;
        }

        public void Click()
        {
            
            if (!IsClicked && CallbackOnClick != null)
            {
                IsClicked = true;
                CallbackOnClick(this);
                IsClicked = false;
            }
            
        }

        public void HoverStart()
        {
            if (!IsHovered && !IsClicked && CallbackOnHoverStart != null)
            {
                CallbackOnHoverStart(this);
            }
            IsHovered = true;
        }

        public void HoverEnd()
        {
            if (IsHovered && CallbackOnHoverEnd != null)
            {
                CallbackOnHoverEnd(this);
            }
            IsHovered = false;
        }

        public void Update(double elapsedTime)
        {
            Sprite.Update(elapsedTime);
        }

        public void Draw(RenderWindow window)
        {
            Sprite.Draw(window, State);
        }
    }
}
