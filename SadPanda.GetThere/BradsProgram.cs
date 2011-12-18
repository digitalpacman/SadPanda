using System;

namespace SadPanda.GetThere
{
#if WINDOWS || XBOX
    static class BradsProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BradsGame game = new BradsGame())
            {
                game.Run();
            }
        }
    }
#endif
}

