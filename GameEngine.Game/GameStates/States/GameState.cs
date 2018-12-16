using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Game.GameStates.States
{
    public class GameState : State
    {
        public GameState(GameEngine.Data.System system)
            : base(system)
        {

        }

        public void Init()
        {
#if DEBUG
            Console.WriteLine("GameState init!");
#endif
        }


        public void Pause()
        {
            
        }

        public void Resume()
        {
            
        }


        public void Input()
        {

        }

        public void Update(double elapsedTime)
        {

        }

        public void Render()
        {

        }

        public void Dispose()
        { 

        }
    }
}
