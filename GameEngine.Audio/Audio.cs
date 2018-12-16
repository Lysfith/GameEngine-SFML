using GameEngine.Data;
using GameEngine.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Audio
{
    public class Audio : GameEngine.Data.System, IAudio
    {
        public Audio(GameEngine.Data.System system)
            : base(system)
        {

        }

        public override void Start()
        {
            IsRunning = true;
        }

        public override void Update(double elapsedTime = 0.0)
        {

        }

        public override void Stop()
        {
            IsRunning = false;
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
