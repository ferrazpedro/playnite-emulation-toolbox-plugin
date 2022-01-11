using EmulationToolbox.Services.Library;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationToolbox.Configuration
{
    internal class MainMenu
    {
        public static List<MainMenuItem> getPluginMenuItems()
        {
            return new List<MainMenuItem>
            {
				new MainMenuItem
			    {
				    MenuSection = "@" + EmulationToolbox.PluginName + "|Text",
				    Description = "Change the Play Action name for the Selected Games",
				    Action = (args) => EmulationTextChanges.changeGamePlayActionName()
			    },
				new MainMenuItem
				{
					MenuSection = "@" + EmulationToolbox.PluginName + "|Text",
					Description = "Cleanup the name of the ROMs entries associated to the Selected Games",
					Action = (args) => EmulationTextChanges.cleanupRomNames()
				},
				new MainMenuItem
				{
					MenuSection = "@" + EmulationToolbox.PluginName + "|Text",
					Description = "Changes all hyphen (-) to colons (:) in the Selected Games names",
					Action = (args) => EmulationTextChanges.changeHyphensToColon()
				},
				new MainMenuItem
				{
					MenuSection = "@" + EmulationToolbox.PluginName + "|Games",
					Description = "Merge the associated ROMs of the Selected Games names",
					Action = (args) => EmulationGameChanges.mergeEmulatedGames()
				},
			};
        }
    }
}
