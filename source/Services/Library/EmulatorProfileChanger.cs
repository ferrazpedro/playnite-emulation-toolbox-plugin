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


    }
}
