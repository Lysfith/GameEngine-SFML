using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Data.Interfaces
{
    public interface ISystem : IDisposable
    {
        void Start();
        void Stop();
        void Update(double elapsedTime = 0.0);
    }
}
