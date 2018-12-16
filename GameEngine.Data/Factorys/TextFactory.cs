using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Data.Factorys
{
    public class TextFactory : IDisposable
    {
        private static TextFactory _instance;

        public static TextFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TextFactory();
                }

                return _instance;
            }
        }

        private Dictionary<string, Text> _texts;

        public TextFactory()
        {
            _texts = new Dictionary<string, Text>();
        }

        public Text GetText(string id, string text)
        {
            if (!_texts.ContainsKey(id))
            {
                var t = new Text();
                t.DisplayedString = text;
                t.Font = FontFactory.Instance.DefaultFont();
                _texts.Add(id, t);
            }

            return _texts[id];
        }

        public void Dispose()
        {
            foreach (var text in _texts)
            {
                text.Value.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
