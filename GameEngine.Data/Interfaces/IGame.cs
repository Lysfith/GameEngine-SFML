using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Data.Interfaces
{
    public interface IGame : ISystem
    {
        void MouseClick(object sender, MouseButtonEventArgs e);
        void MouseMove(object sender, MouseMoveEventArgs e);
        void MouseWheel(object sender, MouseWheelEventArgs e);
        void KeyboardKeyDown(object sender, KeyEventArgs e);
    }
}
