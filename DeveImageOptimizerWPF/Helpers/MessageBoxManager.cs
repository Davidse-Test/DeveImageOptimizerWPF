using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace DeveImageOptimizerWPF.Helpers
{
    /// <summary>
    /// Provides methods for showing message boxes in Avalonia.
    /// </summary>
    public static class MessageBoxManager
    {
        /// <summary>
        /// Gets a standard message box with the specified title, message, and icon.
        /// </summary>
        /// <param name="title">The title of the message box.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="icon">The icon to display.</param>
        /// <returns>An Avalonia message box.</returns>
        public static MsBox.Avalonia.BaseWindows.IMsBox<MsBox.Avalonia.BaseWindows.IMsBoxWindow<MsBox.Avalonia.Dto.ButtonResult>>
            GetMessageBoxStandard(string title, string message, Icon icon = Icon.Info)
        {
            // Commented out as requested - MessageBox functionality removed temporarily
            /*
            // Using MsBox.Avalonia directly to avoid recursive calls
            return MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(title, message, icon: icon);
            */
            
            // Return null to avoid compilation errors - this method is not being used as it's commented out in MainViewModel
            return null!;
        }
    }
}