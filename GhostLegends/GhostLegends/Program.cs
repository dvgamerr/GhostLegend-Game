using System;

namespace GhostLegends
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (EngineGame game = new EngineGame())
            {
                game.Run();
            }
        }
    }
#endif
}

