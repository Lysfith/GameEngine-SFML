using GameEngine.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Data
{
    public class System : ISystem
    {
        protected System Parent { get; private set; }
        public IGraphic Graphic { get; protected set; }
        public IInput Input { get; protected set; }
        public IAudio Audio { get; protected set; }
        public IGame Game { get; protected set; }
        public IDebug Debug { get; protected set; }

        public bool IsRunning { get; protected set; }

        public System(System parent = null)
        {
            Parent = parent;
            IsRunning = false;
        }

        public virtual void Start()
        {

        }

        public virtual void Update(double elapsedTime = 0.0)
        {

        }

        public virtual void Stop()
        {

        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
