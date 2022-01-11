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
    internal class EmulationTextChanges
    {
        private static readonly Regex nameCleaner = new Regex(@"^[^\(]+", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex extensionCleaner = new Regex(@"\.[^.]*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex hyphensCleaner = new Regex(@" \- ", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static void cleanupRomNames()
        {
            int processedCount = 0;
            int renamedCount = 0;
            GlobalProgressResult progressResult = UI.UIService.showProgress("Cleaning up ROMs names from selection", false, true, (progressAction) =>
            {

                IEnumerable<Game> selectedGames = EmulationToolbox.playniteAPI.MainView.SelectedGames;

                foreach (Game game in selectedGames)
                {
                    if (game.Roms.Any())
                    {
                        renameRoms(game);

                        EmulationToolbox.playniteAPI.Database.Games.Update(game);

                        renamedCount++;
                    }

                    processedCount++;
                };
            });

            UI.UIService.showMessage(renamedCount + " Games ROMs names cleaned up");
        }

        public static void renameRoms(Game playniteGame)
        {
            foreach (GameRom rom in playniteGame.Roms)
            {
                rom.Name = nameCleaner.Replace(rom.Path, "");
                rom.Name = extensionCleaner.Replace(rom.Name, "");
            }
        }

        public static void changeGamePlayActionName()
        {
            int processedCount = 0;
            int changedCount = 0;
            GlobalProgressResult progressResult = UI.UIService.showProgress("Changing Play Action name from selection", false, true, (progressAction) =>
            {
                IEnumerable<Game> selectedGames = EmulationToolbox.playniteAPI.MainView.SelectedGames;

                foreach (Game game in selectedGames)
                {
                    if (game.Roms.Any())
                    {
                        game.GameActions.FirstOrDefault().Name = game.Name;

                        EmulationToolbox.playniteAPI.Database.Games.Update(game);

                        changedCount++;
                    }

                    processedCount++;
                };
            });

            UI.UIService.showMessage(changedCount + " Games had their Play Action name changed");
        }

        public static void changeHyphensToColon()
        {
            int processedCount = 0;
            int renamedCount = 0;
            GlobalProgressResult progressResult = UI.UIService.showProgress("Changing all hyphens in Games names to colons", false, true, (progressAction) =>
            {

                IEnumerable<Game> selectedGames = EmulationToolbox.playniteAPI.MainView.SelectedGames;

                foreach (Game game in selectedGames)
                {
                    if (game.Roms.Any())
                    {
                        game.Name = hyphensCleaner.Replace(game.Name, ": ");

                        EmulationToolbox.playniteAPI.Database.Games.Update(game);

                        renamedCount++;
                    }

                    processedCount++;
                };
            });

            UI.UIService.showMessage(renamedCount + " Games names changed");
        }
    }
}
