using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using GameEngine.Data.Model;

namespace GameEngine.Data.Factorys
{
    public class SpriteFactory
    {

        private static SpriteFactory _instance;

        public static SpriteFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpriteFactory();
                }

                return _instance;
            }
        }

        private ConcurrentDictionary<string, Sprite> _sprites;
        private ConcurrentDictionary<string, SpriteAnimated> _spritesAnimated;

        public SpriteFactory()
        {
            _sprites = new ConcurrentDictionary<string, Sprite>();
            _spritesAnimated = new ConcurrentDictionary<string, SpriteAnimated>();
        }

        public Sprite GetSprite(string textureName, int width = 0, int height = 0, bool repeated = false, byte red = 255, byte green = 255, byte blue = 255, byte alpha = 255)
        {
            if (!_sprites.ContainsKey(textureName) || _sprites[textureName].TextureRect.Width != width || _sprites[textureName].TextureRect.Height != height)
            {
                var texture = TextureFactory.Instance.GetTexture(textureName);
                texture.Repeated = repeated;
                var sprite = new Sprite(texture);
                if (!repeated)
                {
                    sprite.TextureRect = new IntRect(0, 0, (int)texture.Size.X, (int)texture.Size.Y);
                }
                else
                {
                    sprite.TextureRect = new IntRect(0, 0, width, height);
                }

                sprite.Color = new Color(red, green, blue, alpha);

                if (width != 0 && height != 0)
                {
                    if (!repeated)
                    {
                        sprite.Scale = new Vector2f(1f * width / texture.Size.X, 1f * height / texture.Size.Y);
                    }
                    sprite.Origin = new Vector2f((int)((float)width / 2f), (int)((float)height / 2f));
                }
                else if (width != 0)
                {
                    if (!repeated)
                    {
                        sprite.Scale = new Vector2f(1f * width / texture.Size.X, 1f * width / texture.Size.X);
                    }
                    sprite.Origin = new Vector2f((int)((float)width / 2f), (int)((float)width / 2f));
                }
                else if (height != 0)
                {
                    if (!repeated)
                    {
                        sprite.Scale = new Vector2f(1f * height / texture.Size.Y, 1f * height / texture.Size.Y);
                    }
                    sprite.Origin = new Vector2f((int)((float)height / 2f), (int)((float)height / 2f));
                }
                else
                {
                    sprite.Origin = new Vector2f((int)((float)texture.Size.X / 2f), (int)((float)texture.Size.Y / 2f));
                }

                //sprite.Origin = new Vector2f((int)texture.Size.X/2, (int)texture.Size.Y/2);

                _sprites.AddOrUpdate(textureName, sprite, (id, spriteTemp) => spriteTemp);
            }

            return _sprites[textureName];
        }

        public Transform GetResizedSprite(Sprite sprite, float width = 0, float height = 0)
        {
            return GetResizedSprite(sprite.TextureRect.Width, sprite.TextureRect.Height, width, height);
        }

        public Transform GetResizedSprite(float width = 0, float height = 0, float newWidth = 0, float newHeight = 0)
        {
            var t = Transform.Identity;
            if (newWidth != 0 && newHeight != 0)
            {
                t.Scale(1f * newWidth / width, 1f * newHeight / height);
            }
            else if (width != 0)
            {
                t.Scale(1f * newWidth / width, 1f * newWidth / width);
            }
            else if (newHeight != 0)
            {
                t.Scale(1f * newHeight / height, 1f * newHeight / height);
            }

            return t;
        }

        public SpriteAnimated GetSpriteAnimated(string textureName, int frameWidth, int frameHeight, int framesPerSecond, int firstFrame = 0, int lastFrame = 0, bool isAnimated = false, bool isLooped = true, int width = 0, int height = 0)
        {
            //if (!_spritesAnimated.ContainsKey(textureName) || _spritesAnimated[textureName].TextureRect.Width != frameWidth || _spritesAnimated[textureName].TextureRect.Height != frameHeight)
            //{
                var texture = TextureFactory.Instance.GetTexture(textureName);
                var spriteAnimated = new SpriteAnimated(texture, frameWidth, frameHeight, framesPerSecond, firstFrame, lastFrame, isAnimated, isLooped);

                if (width != 0 && height != 0)
                {
                    spriteAnimated.Scale = new Vector2f(1f * width / frameWidth, 1f * height / frameHeight);
                    spriteAnimated.Origin = new Vector2f((int)((float)width / 2f), (int)((float)height / 2f));
                }
                else if (width != 0)
                {
                    spriteAnimated.Scale = new Vector2f(1f * width / frameWidth, 1f * width / frameWidth);
                    spriteAnimated.Origin = new Vector2f((int)((float)width / 2f), (int)((float)width / 2f));
                }
                else if (height != 0)
                {
                    spriteAnimated.Scale = new Vector2f(1f * height / frameHeight, 1f * height / frameHeight);
                    spriteAnimated.Origin = new Vector2f((int)((float)height / 2f), (int)((float)height / 2f));
                }
                else
                {
                    spriteAnimated.Origin = new Vector2f((int)((float)texture.Size.X / 2f), (int)((float)texture.Size.Y / 2f));
                }

            //    _spritesAnimated.AddOrUpdate(textureName, spriteAnimated, (id, spriteTemp) => spriteTemp);
            ////}

            //return _spritesAnimated[textureName];

            return spriteAnimated;
        }

        public void Update(double elapsedTime)
        {
            foreach (var sprite in _spritesAnimated)
            {
                sprite.Value.Update(elapsedTime);
            }
        }


        public int SpriteCount()
        {
            return _sprites.Count;
        }

        public int SpriteAnimatedCount()
        {
            return _spritesAnimated.Count;
        }

        public void Dispose()
        {
            foreach (var sprite in _sprites)
            {
                sprite.Value.Dispose();
            }

            foreach (var spriteAnimated in _spritesAnimated)
            {
                spriteAnimated.Value.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
