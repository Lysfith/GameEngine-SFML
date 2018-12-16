using GameEngine.Data;
using GameEngine.Data.Interfaces;
using GameEngine.Game.GameStates;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Game
{
    public class Game : GameEngine.Data.System, IGame
    {
        private StateManager _stateManager;

        public Game(GameEngine.Data.System system)
            : base(system)
        {

        }

        public override void Start()
        {
            IsRunning = true;

            _stateManager = new StateManager(this.Parent);
        }


        public override void Update(double elapsedTime = 0.0)
        {
            _stateManager.Update(elapsedTime);
        }

        public override void Stop()
        {
            IsRunning = false;

        }

        #region Input
        public void MouseClick(object sender, MouseButtonEventArgs e)
        {
#if DEBUG
            Console.WriteLine("MouseClick");
#endif
        }

        public void MouseMove(object sender, MouseMoveEventArgs e)
        {
#if DEBUG
            Console.WriteLine("MouseMove");
#endif

        }

        public void MouseWheel(object sender, MouseWheelEventArgs e)
        {
#if DEBUG
            Console.WriteLine("MouseWheel");
#endif

        }

        public void KeyboardKeyDown(object sender, KeyEventArgs e)
        {
#if DEBUG
            Console.WriteLine("KeyboardKeyDown");
#endif

        }
        #endregion

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
