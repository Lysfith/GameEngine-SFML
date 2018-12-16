using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Data.Factorys
{
    public class FontFactory
    {

        private static FontFactory _instance;

        public static FontFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FontFactory();
                }

                return _instance;
            }
        }

        private Dictionary<string, Font> _fonts;

        public FontFactory()
        {
            _fonts = new Dictionary<string, Font>();
        }

        public Font DefaultFont()
        {
            if (!_fonts.ContainsKey("default"))
            {
                var font = new Font(@"Resources/Fonts/homespun.ttf");
                _fonts.Add("default", font);
            }

            return _fonts["default"];
        }

        public void Dispose()
        {
            foreach (var font in _fonts)
            {
                font.Value.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
