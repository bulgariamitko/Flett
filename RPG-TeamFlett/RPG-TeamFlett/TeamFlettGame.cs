using System;

namespace RPG_TeamFlett
{
#if WINDOWS || LINUX
    public static class TeamFlettGame
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
