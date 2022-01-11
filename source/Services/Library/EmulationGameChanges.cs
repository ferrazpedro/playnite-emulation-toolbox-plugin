using Playnite.SDK;
using Playnite.SDK.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationToolbox.Services.Library
{
    internal class EmulationGameChanges
    {
        public static void mergeEmulatedGames()
        {
            int processedCount = 0;
            int renamedCount = 0;
            GlobalProgressResult progressResult = UI.UIService.showProgress("Merging the associated ROMs of the emulated Games", false, true, (progressAction) =>
            {

                List<Game> selectedGames = EmulationToolbox.playniteAPI.MainView.SelectedGames.ToList();

                Game mergedGame = selectedGames.FirstOrDefault();

                if (mergedGame.Roms.Any())
                {
                    selectedGames.Remove(mergedGame);

                    foreach (Game game in selectedGames)
                    {
                        if (game.Roms.Any())
                        {
                            mergedGame.Roms.Concat(game.Roms);
                            renamedCount++;
                        }

                        processedCount++;
                    };
                }
            });

            UI.UIService.showMessage(renamedCount + " Games names changed");
        }
    }
}
