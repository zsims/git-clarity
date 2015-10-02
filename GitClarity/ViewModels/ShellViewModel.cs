using Caliburn.Micro;
using GitClarity.Properties;

namespace GitClarity.ViewModels
{
    /// <summary>
    /// View model for the main window
    /// </summary>
    interface IShellViewModel
    {
    }

    sealed class ShellViewModel : Screen, IShellViewModel
    {
        public ShellViewModel()
        {
            DisplayName = Resources.ShellViewModel_DisplayName;
        }
    }
}
