using GameEngine.Data;
using GameEngine.Data.Enums;
using GameEngine.Data.Factorys;
using GameEngine.Data.Interfaces;
using GameEngine.Data.Model;
using GameEngine.Data.Models;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameEngine.Graphic
{
    public class Graphic : GameEngine.Data.System, IGraphic
    {
        private RenderWindow Window;
        private Thread Thread;
        private double Fps;
        private double FpsMs;
        private string Title;
        private Styles Style;

        private uint Width;
        private uint Height;

        private Dictionary<EnumLayer, Layer> Layers;

        public Graphic(GameEngine.Data.System system, uint width, uint height, string title, double fps, Styles style = Styles.Default)
            : base(system)
        {
            Fps = fps;
            FpsMs = 1000.0 / Fps;
            Width = width;
            Height = height;
            Title = title;
            Style = style;

            Layers = new Dictionary<EnumLayer, Layer>();
        }

        public override void Start()
        {
            IsRunning = true;

            Layers.Add(EnumLayer.Background, new Layer(this, Width, Height, EnumLayer.Background));
            Layers.Add(EnumLayer.Principal, new Layer(this, Width, Height, EnumLayer.Principal));
            Layers.Add(EnumLayer.Foreground, new Layer(this, Width, Height, EnumLayer.Foreground));
            Layers.Add(EnumLayer.Ui, new Layer(this, Width, Height, EnumLayer.Ui));

            Thread = new Thread(Run);
            Thread.Start();
        }

        public override void Update(double elapsedTime = 0.0)
        {
            foreach(var layer in Layers)
            {
                layer.Value.Update(elapsedTime);
            }
        }

        public override void Stop()
        {
            IsRunning = false;
        }

        #region Graphic
        //public void AddSprite(EnumLayer layer, Sprite sprite, RenderStates state)
        //{
        //    Layers[layer].AddSprite(sprite, state);
        //}

        //public RectangleShape AddRectangle(EnumLayer layer, int x, int y, int width, int height, Color color)
        //{
        //    var r = new Sprite(new Vector2f(width, height));
        //    r.Color = color;

        //    RenderStates state = new RenderStates()
        //    {
        //        BlendMode = BlendMode.Alpha,
        //        Transform = Transform.Identity
        //    };

        //    state.Transform.Translate(x, y);

        //    Layers[layer].AddDrawable(r, state);

        //    return r;
        //}

        public Sprite AddSprite(EnumLayer layer, int x, int y, Color color, string textureName, int width = 0, int height = 0, bool repeated = false)
        {
            var r = SpriteFactory.Instance.GetSprite(textureName, width, height, repeated);
            r.Color = color;

            RenderStates state = new RenderStates()
            {
                BlendMode = new BlendMode(BlendMode.Factor.SrcAlpha, BlendMode.Factor.DstAlpha, BlendMode.Equation.Add),
                Transform = Transform.Identity
            };

            state.Transform.Translate(x, y);

            Layers[layer].AddSprite(r, state);

            return r;
        }

        public SpriteAnimated AddSpriteAnimated(EnumLayer layer, string textureName, int frameWidth, int frameHeight, int framesPerSecond, int x, int y, Color color, 
            int firstFrame = 0, int lastFrame = 0, bool isAnimated = false, bool isLooped = true, int width = 0, int height = 0)
        {
            var r = SpriteFactory.Instance.GetSpriteAnimated(textureName, frameWidth, frameHeight, framesPerSecond, firstFrame, lastFrame, isAnimated, isLooped, width, height);
            r.Color = color;

            RenderStates state = new RenderStates()
            {
                BlendMode = BlendMode.Alpha,
                Transform = Transform.Identity
            };

            state.Transform.Translate(x, y);

            Layers[layer].AddSpriteAnimated(r, state);

            return r;
        }

        public Text AddText(EnumLayer layer, int x, int y, uint fontSize, Color color)
        {
            var t = new Text("", FontFactory.Instance.DefaultFont());
            t.Color = color;
            t.CharacterSize = fontSize;

            RenderStates state = new RenderStates()
            {
                BlendMode = BlendMode.Alpha,
                Transform = Transform.Identity
            };

            state.Transform.Translate(x, y);

            Layers[layer].AddText(t, state);

            return t;
        }

        public SpriteObject AddSpriteObject(EnumLayer layer, SpriteObject sprite)
        {
            Layers[layer].AddSpriteObject(sprite);
            return sprite;
        }

        public void ClearLayer(EnumLayer layer)
        {
            Layers[layer].Clear();
        }

        public List<SpriteObject> RayCast(EnumLayer layer, int x, int y)
        {
            return Layers[layer].RayCast(x, y);
        }
        #endregion

        #region Window
        private void CreateWindow()
        {
            // Request a 32-bits depth buffer when creating the window
            ContextSettings contextSettings = new ContextSettings();
            contextSettings.DepthBits = 32;

            // Create the main window
            Window = new RenderWindow(new VideoMode(Width, Height), Title, Style, contextSettings);

            // Make it the active window for OpenGL calls
            Window.SetActive();

            // Setup event handlers
            Window.Closed += OnClosed;
            Window.Resized += OnResized;
        }

        private void Run()
        {
            CreateWindow();

            //Color clear = new Color(30, 140, 255);
            Color clear = new Color(0, 0, 0);

            Clock time = new Clock();
            Clock test = new Clock();

            int realElapsed = 0;

            // Start the game loop
            while (IsRunning)
            {
                test.Restart();

                Window.DispatchEvents();

                Window.Clear(clear);

                double elapsedTime = (double)time.ElapsedTime.AsMilliseconds();

                time.Restart();

                //========================
                Sprite sprite = new Sprite();
                sprite.TextureRect = new IntRect(0, 0, (int)Width, (int)Height);

                Layers[EnumLayer.Background].Draw(Window);
                Layers[EnumLayer.Principal].Draw(Window);
                Layers[EnumLayer.Foreground].Draw(Window);
                Layers[EnumLayer.Ui].Draw(Window);
                //============================

                Window.Display();

                realElapsed = test.ElapsedTime.AsMilliseconds();
                if (realElapsed >= 0 && realElapsed < FpsMs)
                {
                    Thread.Sleep((int)(FpsMs - (double)realElapsed));
                }
            }
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Parent.Stop();
        }

        private void OnResized(object sender, SizeEventArgs e)
        {

        }

        public RenderWindow GetWindow()
        {
            return Window;
        }

        public int GetWidth()
        {
            return (int)Width;
        }

        public int GetHeight()
        {
            return (int)Height;
        }
        #endregion


        public override void Dispose()
        {
            Window.Dispose();

            GC.SuppressFinalize(this);
        }

    }
}
