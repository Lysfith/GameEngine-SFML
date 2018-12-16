using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Game.GameStates
{
    public class State : GameEngine.Data.System, IGameState
    {
        public State(GameEngine.Data.System system)
            : base(system)
        {

        }

        public virtual void Init()
        {
            throw new NotImplementedException();
        }

        public virtual void Pause()
        {
            throw new NotImplementedException();
        }

        public virtual void Render()
        {
            throw new NotImplementedException();
        }

        public virtual void Resume()
        {
            throw new NotImplementedException();
        }

        public virtual void Key()
        {
            throw new NotImplementedException();
        }
    }
}
