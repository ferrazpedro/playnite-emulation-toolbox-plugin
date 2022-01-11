using EmulationToolbox.Services.Library;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationToolbox.Configuration
{
    internal class GameMenu
    {
        public static List<GameMenuItem> getPluginMenuItems()
        {
            return new List<GameMenuItem>
            {
                new GameMenuItem
                {
                    MenuSection = EmulationToolbox.PluginName + "|Text",
                    Description = "Change the Play Action name for the Selected Games",
                    Action = (args) => EmulationTextChanges.changeGamePlayActionName(args)
                },
                new GameMenuItem
                {
                    MenuSection = EmulationToolbox.PluginName + "|Text",
                    Description = "Cleanup the name of the ROMs entries associated to the Selected Games",
                    Action = (args) => EmulationTextChanges.cleanupRomNames(args)
                },
                new GameMenuItem
                {
                    MenuSection = EmulationToolbox.PluginName + "|Text",
                    Description = "Changes all hyphen (-) to colons (:) in the Selected Games names",
                    Action = (args) => EmulationTextChanges.changeHyphensToColon(args)
                },
                new GameMenuItem
                {
                    MenuSection = EmulationToolbox.PluginName + "|Text",
                    Description = "Move (The) to the beginning of the Selected Games names",
                    Action = (args) => EmulationTextChanges.moveTheToTheBeginning(args)
                },
                new GameMenuItem
                {
                    MenuSection = EmulationToolbox.PluginName + "|Text",
                    Description = "Remove (The) from the Selected Games sorting names",
                    Action = (args) => EmulationTextChanges.removeTheFromSortingName(args)
                },
                new GameMenuItem
                {
                    MenuSection = EmulationToolbox.PluginName + "|Games",
                    Description = "Merge the associated ROMs of the Selected Games",
                    Action = (args) => EmulationGameChanges.mergeEmulatedGames(args)
                },
            };
        }
    }
}
