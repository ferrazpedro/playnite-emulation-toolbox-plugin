using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmulationToolbox.Services.UI
{
    internal class UIService
    {
        public static GlobalProgressResult showProgress(string text, bool cancelable, bool indeterminate, Action<GlobalProgressActionArgs> f)
        {
            GlobalProgressOptions options = new GlobalProgressOptions(text, cancelable);
            options.IsIndeterminate = indeterminate;

            return EmulationToolbox.playniteAPI.Dialogs.ActivateGlobalProgress(f, options);
        }

        public static void showMessage(string text)
        {
            EmulationToolbox.playniteAPI.Dialogs.ShowMessage(text);
        }

        public static void showError(string title, string text)
        {
            EmulationToolbox.playniteAPI.Dialogs.ShowErrorMessage(text, title);
        }

        public static DialogResult openAskDialog(string title, string text)
        {
            return MessageBox.Show(text, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        public static string openFileDialogChooser(string extensionFiltersType)
        {
            return EmulationToolbox.playniteAPI.Dialogs.SelectFile(extensionFiltersType);
        }

        public static string openDirectoryDialogChooser()
        {
            return EmulationToolbox.playniteAPI.Dialogs.SelectFolder();
        }
    }
}
