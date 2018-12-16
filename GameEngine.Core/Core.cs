using GameEngine.Data;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameEngine.Core
{
    public class Core : GameEngine.Data.System
    {
        private Thread Thread;

        private double Fps;
        private double FpsMs;

        /// <summary>
        /// Point d'entrée.
        /// </summary>
        public Core(int fps = 60)
        {
            Fps = fps;
            FpsMs = 1000.0 / Fps;

            Load();

            Start();
        }

        /// <summary>
        /// Chargement des différents systèmes.
        /// </summary>
        private void Load()
        {
            

#if DEBUG
            Console.WriteLine("Loadind systems...");
#endif
            Graphic = new Graphic.Graphic(this, 1024, 768, "Game", 60, Styles.Default);
            Input = new Input.Input(this);
            Audio = new Audio.Audio(this);
            Game = new Game.Game(this);
#if DEBUG
            Debug = new Debug.Debug(this);
            Console.WriteLine("Systems loaded!");
#endif
        }

        /// <summary>
        /// Lance la boucle.
        /// </summary>
        public override void Start()
        {
#if DEBUG
            Console.WriteLine("Systems starting...");
#endif
            Input.Start();
            Graphic.Start();
            Audio.Start();
            Game.Start();
#if DEBUG
            Debug.Start();
            Console.WriteLine("Systems started!");
#endif

            IsRunning = true;

            //Création thread pour la boucle
            Thread = new Thread(Run);
            Thread.Start();
        }

        /// <summary>
        /// Boucle.
        /// </summary>
        public void Run()
        {
            Update();
        }

        public override void Update(double elapsedTime = 0.0)
        {
            Clock time = new Clock();
            Clock test = new Clock();

            int realElapsed = 0;

            while (IsRunning)
            {
                elapsedTime = time.ElapsedTime.AsMicroseconds() / 1000.0;

                Input.Update(elapsedTime);
                Graphic.Update(elapsedTime);
                Audio.Update(elapsedTime);
                Game.Update(elapsedTime);

#if DEBUG
                Debug.Update(elapsedTime);
#endif

                time.Restart();

                realElapsed = time.ElapsedTime.AsMilliseconds();
                if (realElapsed >= 0 && realElapsed < FpsMs)
                {
                    Thread.Sleep((int)(FpsMs - (double)realElapsed));
                }
            }

            Dispose();
        }

        public override void Stop()
        {
#if DEBUG
            Console.WriteLine("Systems stopping...");
#endif
            Input.Stop();
            Graphic.Stop();
            Audio.Stop();
            Game.Stop();
#if DEBUG
            Debug.Stop();
            Console.WriteLine("Systems stopped!");
#endif

            IsRunning = false;
        }

        /// <summary>
        /// Sortie.
        /// </summary>
        public override void Dispose()
        {
#if DEBUG
            Console.WriteLine("Systems disposing...");
#endif
            Input.Dispose();
            Graphic.Dispose();
            Audio.Dispose();
            Game.Dispose();
#if DEBUG
            Debug.Dispose();
            Console.WriteLine("Systems disposed!");
#endif

            GC.SuppressFinalize(this);
        }
    }
}
