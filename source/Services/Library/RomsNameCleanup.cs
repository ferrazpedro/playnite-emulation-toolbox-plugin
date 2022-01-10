using Playnite.SDK;
using Playnite.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmulationToolbox.Services.Library
{
    internal class RomsNameCleanup
    {
        private static readonly Regex nameCleaner = new Regex(@"^[^\(]*\(", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex extensionCleaner = new Regex(@"\.[^.]*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static void cleanupRomNames()
        {
            int processedCount = 0;
            int renamedCount = 0;
            GlobalProgressResult progressResult = UI.UIService.showProgress("Changing emulator profiles from selection", false, true, (progressAction) =>
            {

                // Get selected games
                IEnumerable<Game> selectedGames = EmulationToolbox.playniteAPI.MainView.SelectedGames;

                // Change play action for the selected games
                foreach (Game game in selectedGames)
                {
                    if (game.Roms.Any())
                    {
                        renameRoms(game);
                        renamedCount++;
                    }

                    processedCount++;
                };
            });

            // Show result message
            UI.UIService.showMessage(renamedCount + " Games had their emulator profile changed");
        }

        public static void renameRoms(Game playniteGame)
        {
            foreach (GameRom rom in playniteGame.Roms)
            {
                rom.Name = nameCleaner.Replace(rom.Path, "");
                rom.Name = extensionCleaner.Replace(rom.Name, "");
            }
        }
    }
}
