using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Game.GameStates
{
    public interface IGameState : IDisposable
    {
        void Init();

        void Pause();
        void Resume();

        void Key();
        void Update(double elapsedTime);
        void Render();
    }
}
