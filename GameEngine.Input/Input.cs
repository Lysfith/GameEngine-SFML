using GameEngine.Data;
using GameEngine.Data.Interfaces;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Input
{
    public class Input : GameEngine.Data.System, IInput
    {
        private bool InputLoaded;
        private RenderWindow Window;

        public Input(GameEngine.Data.System system)
            : base(system)
        {
            InputLoaded = false;
        }

        public override void Start()
        {
            IsRunning = true;
        }

        public override void Update(double elapsedTime = 0.0)
        {
            if(!InputLoaded)
            {
                Window = Parent.Graphic.GetWindow();

                if (Window != null)
                {
#if DEBUG
                    Console.WriteLine("Add events to window...");
#endif
                    Window.MouseButtonPressed += MouseClick;
                    Window.MouseMoved += MouseMove;
                    Window.MouseWheelMoved += MouseWheel;
                    Window.KeyPressed += KeyboardKeyDown;
#if DEBUG
                    Console.WriteLine("Events added !");
#endif

                    InputLoaded = true;
                }
            }
        }

        public override void Stop()
        {
            IsRunning = false;
        }

        #region Input
        public void MouseClick(object sender, MouseButtonEventArgs e)
        {
#if DEBUG
            //Console.WriteLine("MouseClick");
#endif
            var items = Parent.Graphic.RayCast(Data.Enums.EnumLayer.Ui, e.X, e.Y);

            if (items != null && items.Any())
            {
                foreach (var item in items)
                {
                    item.Click();
                }
            }
            else
            {
                items = Parent.Graphic.RayCast(Data.Enums.EnumLayer.Foreground, e.X, e.Y);

                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        item.Click();
                    }
                }
                else
                {
                    items = Parent.Graphic.RayCast(Data.Enums.EnumLayer.Principal, e.X, e.Y);

                    if (items != null && items.Any())
                    {
                        foreach (var item in items)
                        {
                            item.Click();
                        }
                    }
                    else
                    {
                        items = Parent.Graphic.RayCast(Data.Enums.EnumLayer.Background, e.X, e.Y);

                        if (items != null && items.Any())
                        {
                            foreach (var item in items)
                            {
                                item.Click();
                            }
                        }
                    }
                }
            }

           
        }

        public void MouseMove(object sender, MouseMoveEventArgs e)
        {
#if DEBUG
            //Console.WriteLine("MouseMove");
#endif
            var items = Parent.Graphic.RayCast(Data.Enums.EnumLayer.Ui, e.X, e.Y);

            if (items == null || !items.Any())
            {
                items = Parent.Graphic.RayCast(Data.Enums.EnumLayer.Foreground, e.X, e.Y);

                if (items == null || !items.Any())
                {
                    items = Parent.Graphic.RayCast(Data.Enums.EnumLayer.Principal, e.X, e.Y);

                    if (items == null || !items.Any())
                    {
                        items = Parent.Graphic.RayCast(Data.Enums.EnumLayer.Background, e.X, e.Y);
                    }
                }
            }
        }

        public void MouseWheel(object sender, MouseWheelEventArgs e)
        {
#if DEBUG
            //Console.WriteLine("MouseWheel");
#endif

        }

        public void KeyboardKeyDown(object sender, KeyEventArgs e)
        {
#if DEBUG
            //Console.WriteLine("KeyboardKeyDown");
#endif

        }
        #endregion

        public override void Dispose()
        {
            if (InputLoaded)
            {
                Window.MouseButtonPressed -= MouseClick;
                Window.MouseMoved -= MouseMove;
                Window.MouseWheelMoved -= MouseWheel;
                Window.KeyPressed -= KeyboardKeyDown;
            }

            GC.SuppressFinalize(this);
        }
    }
}
