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
    internal class EmulatorProfileChanger
    {
        private static readonly Regex cleaner = new Regex(@" *\([^)]*\) *", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static void chooseEmulatorProfile()
        {
            IItemCollection<Emulator> emulators = EmulationToolbox.playniteAPI.Database.Emulators;


            
        }

        public static void changeEmulatorProfile(Guid emulatorId, String profileId)
        {
            int processedCount = 0;
            int changedCount = 0;
            GlobalProgressResult progressResult = UI.UIService.showProgress("Changing emulator profiles from selection", false, true, (progressAction) =>
            {

                // Get selected games
                IEnumerable<Game> selectedGames = EmulationToolbox.playniteAPI.MainView.SelectedGames;

                // Change play action for the selected games
                foreach (Game game in selectedGames)
                {
                    if (game.Roms.Any())
                    {
                        changeGamePlayAction(game, emulatorId, profileId);
                        changedCount++;
                    }

                    processedCount++;
                };
            });

            // Show result message
            UI.UIService.showMessage(changedCount + " Games had their emulator profile changed");
        }

        public static void changeGamePlayAction(Game playniteGame, Guid emulatorId, String profileId)
        {
            // Create new GameAction
            GameAction playAction = new GameAction
            {
                IsPlayAction = true,
                Type = GameActionType.Emulator,
                Name = cleaner.Replace(playniteGame.Roms.FirstOrDefault().Name, ""),
                EmulatorId = emulatorId,
                EmulatorProfileId = profileId
            };

            // Clear old actions and add the new one to the game
            playniteGame.GameActions.Clear();
            playniteGame.GameActions.Add(playAction);
        }
    }
}
