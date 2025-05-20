using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Models;

namespace DeveImageOptimizerWPF
{
    /// <summary>
    /// Simple helper class to create MessageBox.Avalonia windows
    /// </summary>
    public static class MessageBoxManager
    {
        public static MessageBox.Avalonia.MessageBoxWindow GetMessageBoxStandardWindow(string title, string message, Icon icon = Icon.Info)
        {
            // Comment out this code for now as requested, but keep it in the codebase
            /*
            var msBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
            {
                ButtonDefinitions = ButtonEnum.Ok,
                ContentTitle = title,
                ContentMessage = message,
                Icon = icon,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            });

            return msBoxStandardWindow;
            */
            
            // Use MessageBox.Avalonia directly without recursion
            return MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
            {
                ButtonDefinitions = ButtonEnum.Ok,
                ContentTitle = title,
                ContentMessage = message,
                Icon = icon,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            });
        }

        public static MessageBox.Avalonia.MessageBoxWindow GetMessageBoxStandardWindow(MessageBoxStandardParams @params)
        {
            /*
            // Comment out this code for now as requested, but keep it in the codebase
            return MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(@params);
            */
            
            // Temporary empty implementation
            return MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(@params);
        }
    }
}
}