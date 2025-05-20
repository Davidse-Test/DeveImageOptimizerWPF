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
        public static MessageBoxWindow GetMessageBoxStandardWindow(string title, string message, Icon icon = Icon.Info)
        {
            var msBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
            {
                ButtonDefinitions = ButtonEnum.Ok,
                ContentTitle = title,
                ContentMessage = message,
                Icon = icon,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            });

            return msBoxStandardWindow;
        }

        public static MessageBoxWindow GetMessageBoxStandardWindow(MessageBoxStandardParams @params)
        {
            return MessageBoxManager.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(@params);
        }
    }
}