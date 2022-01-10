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
				    MenuSection = "@" + EmulationToolbox.PluginName,
				    Description = "Change the Emulator Profile for the Selected Games",
				    Action = (args) => EmulatorProfileChanger.chooseEmulatorProfile()
			    },
				new MainMenuItem
				{
					MenuSection = "@" + EmulationToolbox.PluginName,
					Description = "Cleanup the name of the ROMs associated to the Selected Games",
					Action = (args) => RomsNameCleanup.cleanupRomNames()
				},
			};
        }
    }
}
