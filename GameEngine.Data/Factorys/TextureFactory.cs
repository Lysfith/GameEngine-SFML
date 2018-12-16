using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Data.Factorys
{
    public class TextureFactory : IDisposable
    {

        private static TextureFactory _instance;

        public static TextureFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TextureFactory();
                }

                return _instance;
            }
        }

        private Dictionary<string, Texture> _textures;

        public TextureFactory()
        {
            _textures = new Dictionary<string, Texture>();
        }

        public Texture GetTexture(string textureName)
        {
            if (!_textures.ContainsKey(textureName))
            {
                var texture = new Texture(@"Resources/Textures/" + textureName);
                texture.Smooth = true;
                _textures.Add(textureName, texture);
            }

            return _textures[textureName];
        }

        public Texture GetBlankTexture()
        {
            if (!_textures.ContainsKey("blank"))
            {
                var texture = new Texture(128, 128);
                texture.Smooth = true;
                _textures.Add("blank", texture);
            }

            return _textures["blank"];
        }

        public void Dispose()
        {
            foreach(var texture in _textures)
            {
                texture.Value.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
