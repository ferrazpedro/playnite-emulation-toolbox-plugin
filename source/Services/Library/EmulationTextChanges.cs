using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
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
        private static readonly Regex theCleaner = new Regex(@", the", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex sortingTheCleaner = new Regex(@"^the ", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static void cleanupRomNames(GameMenuItemActionArgs args)
        {
            int processedCount = 0;
            int renamedCount = 0;
            GlobalProgressResult progressResult = UI.UIService.showProgress("Cleaning up ROMs names from selection", false, true, (progressAction) =>
            {

                IEnumerable<Game> selectedGames = args.Games;

                foreach (Game game in selectedGames)
                {
                    if (game.Roms.Any())
                    {
                        foreach (GameRom rom in game.Roms)
                        {
                            rom.Name = nameCleaner.Replace(rom.Path, "");
                            rom.Name = extensionCleaner.Replace(rom.Name, "");
                        }

                        EmulationToolbox.playniteAPI.Database.Games.Update(game);

                        renamedCount++;
                    }

                    processedCount++;
                };
            });

            UI.UIService.showMessage(renamedCount + " Games ROMs names cleaned up");
        }

        public static void changeGamePlayActionName(GameMenuItemActionArgs args)
        {
            int processedCount = 0;
            int changedCount = 0;
            GlobalProgressResult progressResult = UI.UIService.showProgress("Changing Play Action name from selection", false, true, (progressAction) =>
            {
                IEnumerable<Game> selectedGames = args.Games;

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

        public static void changeHyphensToColon(GameMenuItemActionArgs args)
        {
            int processedCount = 0;
            int renamedCount = 0;
            GlobalProgressResult progressResult = UI.UIService.showProgress("Changing all hyphens in Games names to colons", false, true, (progressAction) =>
            {

                IEnumerable<Game> selectedGames = args.Games;

                foreach (Game game in selectedGames)
                {
                    if (game.Roms.Any() && hyphensCleaner.IsMatch(game.Name))
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

        public static void moveTheToTheBeginning(GameMenuItemActionArgs args)
        {
            int processedCount = 0;
            int renamedCount = 0;
            GlobalProgressResult progressResult = UI.UIService.showProgress("Moving (The) to the beginning of the Games names", false, true, (progressAction) =>
            {

                IEnumerable<Game> selectedGames = args.Games;

                foreach (Game game in selectedGames)
                {
                    if (game.Roms.Any() && theCleaner.IsMatch(game.Name))
                    {
                        game.Name = "The " + theCleaner.Replace(game.Name, "");

                        EmulationToolbox.playniteAPI.Database.Games.Update(game);

                        renamedCount++;
                    }

                    processedCount++;
                };
            });

            UI.UIService.showMessage(renamedCount + " Games names changed");
        }

        public static void removeTheFromSortingName(GameMenuItemActionArgs args)
        {
            int processedCount = 0;
            int renamedCount = 0;
            GlobalProgressResult progressResult = UI.UIService.showProgress("Removing (The) from the Games sorting names", false, true, (progressAction) =>
            {

                IEnumerable<Game> selectedGames = args.Games;

                foreach (Game game in selectedGames)
                {
                    if (game.Roms.Any() && sortingTheCleaner.IsMatch(game.Name))
                    {
                        game.SortingName = sortingTheCleaner.Replace(game.Name, "");

                        EmulationToolbox.playniteAPI.Database.Games.Update(game);

                        renamedCount++;
                    }

                    processedCount++;
                };
            });

            UI.UIService.showMessage(renamedCount + " Games sorting names changed");
        }
    }
}
