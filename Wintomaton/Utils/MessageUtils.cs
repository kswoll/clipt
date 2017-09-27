using System.Threading;
using System.Windows;
using Wintomaton.Windows;

namespace Wintomaton.Utils
{
    public class MessageUtils
    {
        private MessageBoxResult ShowCore(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            var waiter = new ManualResetEvent(false);
            var result = MessageBoxResult.None;
            MainWindow.Instance.Dispatcher.Invoke(() =>
            {
                if (owner != null)
                {
                    result = MessageBox.Show(owner, messageBoxText, caption, button, icon, defaultResult, options);
                }
                else
                {
                    result = MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);
                }
                waiter.Set();
            });
            return result;
        }

        /// <summary>
        /// Displays a message box that has a message, title bar caption, button, and icon; and that accepts a default
        /// message box result, complies with the specified options, and returns a result.
        /// </summary>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        /// <param name="button">
        /// A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to
        /// display.
        /// </param>
        /// <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        /// <param name="defaultResult">
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result
        /// of the message box.
        /// </param>
        /// <param name="options">A <see cref="T:System.Windows.MessageBoxOptions" /> value object that specifies the options.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            return ShowCore(null, messageBoxText, caption, button, icon, defaultResult, options);
        }

        /// <summary>
        /// Displays a message box that has a message, title bar caption, button, and icon; and that accepts a default
        /// message box result and returns a result.
        /// </summary>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        /// <param name="button">
        /// A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to
        /// display.
        /// </param>
        /// <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        /// <param name="defaultResult">
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result
        /// of the message box.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return ShowCore(null, messageBoxText, caption, button, icon, defaultResult, MessageBoxOptions.None);
        }

        /// <summary>Displays a message box that has a message, title bar caption, button, and icon; and that returns a result.</summary>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        /// <param name="button">
        /// A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to
        /// display.
        /// </param>
        /// <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return ShowCore(null, messageBoxText, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>Displays a message box that has a message, title bar caption, and button; and that returns a result.</summary>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        /// <param name="button">
        /// A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to
        /// display.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return ShowCore(null, messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>Displays a message box that has a message and title bar caption; and that returns a result.</summary>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(string messageBoxText, string caption)
        {
            return ShowCore(null, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>Displays a message box that has a message and that returns a result.</summary>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(string messageBoxText)
        {
            return ShowCore(null, messageBoxText, string.Empty, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays a message, title bar
        /// caption, button, and icon; and accepts a default message box result, complies with the specified options, and returns a
        /// result.
        /// </summary>
        /// <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        /// <param name="button">
        /// A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to
        /// display.
        /// </param>
        /// <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        /// <param name="defaultResult">
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result
        /// of the message box.
        /// </param>
        /// <param name="options">A <see cref="T:System.Windows.MessageBoxOptions" /> value object that specifies the options.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            return ShowCore(owner, messageBoxText, caption, button, icon, defaultResult, options);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays a message, title bar
        /// caption, button, and icon; and accepts a default message box result and returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        /// <param name="button">
        /// A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to
        /// display.
        /// </param>
        /// <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        /// <param name="defaultResult">
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result
        /// of the message box.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return ShowCore(owner, messageBoxText, caption, button, icon, defaultResult, MessageBoxOptions.None);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays a message, title bar
        /// caption, button, and icon; and it also returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        /// <param name="button">
        /// A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to
        /// display.
        /// </param>
        /// <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return ShowCore(owner, messageBoxText, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays a message, title bar
        /// caption, and button; and it also returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        /// <param name="button">
        /// A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to
        /// display.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            return ShowCore(owner, messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays a message and title bar
        /// caption; and it returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(Window owner, string messageBoxText, string caption)
        {
            return ShowCore(owner, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays a message and returns a
        /// result.
        /// </summary>
        /// <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        /// <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by
        /// the user.
        /// </returns>
        public MessageBoxResult Show(Window owner, string messageBoxText)
        {
            return ShowCore(owner, messageBoxText, string.Empty, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }
    }
}