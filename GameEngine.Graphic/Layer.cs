using GameEngine.Data.Enums;
using GameEngine.Data.Model;
using GameEngine.Data.Models;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameEngine.Graphic
{
    public class Layer
    {
        private Graphic Graphic;

        public EnumLayer Id { get; private set; }

        private ConcurrentBag<Tuple<Sprite, RenderStates>> Sprites;
        private ConcurrentBag<Tuple<SpriteAnimated, RenderStates>> SpritesAnimated;
        private ConcurrentBag<Tuple<Text, RenderStates>> Texts;
        private ConcurrentBag<SpriteObject> SpriteObjects;
        private uint Height;
        private uint Width;

        public Layer(Graphic graphic, uint height, uint width, EnumLayer id)
        {
            Graphic = graphic;
            Height = height;
            Width = width;

            Sprites = new ConcurrentBag<Tuple<Sprite, RenderStates>>();
            SpritesAnimated = new ConcurrentBag<Tuple<SpriteAnimated, RenderStates>>();
            Texts = new ConcurrentBag<Tuple<Text, RenderStates>>();
            SpriteObjects = new ConcurrentBag<SpriteObject>();
        }

        public void AddSprite(Sprite sprite, RenderStates state)
        {
            Sprites.Add(new Tuple<Sprite, RenderStates>(sprite, state));
        }

        public void AddSpriteAnimated(SpriteAnimated drawable, RenderStates state)
        {
            SpritesAnimated.Add(new Tuple<SpriteAnimated, RenderStates>(drawable, state));
        }

        public void AddText(Text text, RenderStates state)
        {
            Texts.Add(new Tuple<Text, RenderStates>(text, state));
        }

        public void AddSpriteObject(SpriteObject sprite)
        {
            SpriteObjects.Add(sprite);
        }

        public void Clear()
        {
            var newBagSprite = new ConcurrentBag<Tuple<Sprite, RenderStates>>();
            Interlocked.Exchange(ref Sprites, newBagSprite);

            var newBagSpriteAnimated = new ConcurrentBag<Tuple<SpriteAnimated, RenderStates>>();
            Interlocked.Exchange(ref SpritesAnimated, newBagSpriteAnimated);

            var newBagText = new ConcurrentBag<Tuple<Text, RenderStates>>();
            Interlocked.Exchange(ref Texts, newBagText);
        }

        public List<SpriteObject> RayCast(int x, int y)
        {
            List<SpriteObject> result = new List<SpriteObject>();

            foreach (var item in SpriteObjects)
            {
                var trans = Transform.Identity;
                trans.Combine(item.State.Transform);
                trans.Translate(-item.Sprite.TextureRect.Left, -item.Sprite.TextureRect.Top);
                trans.Translate(-item.Sprite.Origin);
                var rect = trans.TransformRect((FloatRect)item.Sprite.TextureRect);

                if (rect.Left < x && x < rect.Left + rect.Width
                    && rect.Top < y && y < rect.Top + rect.Height)
                {
                    result.Add(item);
                    item.HoverStart();
                }
                else
                {
                    item.HoverEnd();
                }
            }

            return result;
        }

        public void Update(double elapsedTime = 0.0)
        {
            foreach (var item in SpritesAnimated)
            {
                item.Item1.Update(elapsedTime);
            }

            foreach (var item in SpriteObjects)
            {
                item.Update(elapsedTime);
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach(var item in Sprites)
            {
                item.Item1.Draw(window, item.Item2);
            }

            foreach (var item in SpritesAnimated)
            {
                item.Item1.Draw(window, item.Item2);
            }

            foreach (var item in Texts)
            {
                item.Item1.Draw(window, item.Item2);
            }

            foreach (var item in SpriteObjects)
            {
                item.Draw(window);
            }
        }
    }
}
