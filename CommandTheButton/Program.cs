using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Petzold.CommandTheButton
{
    class CommandTheButton : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new CommandTheButton());
        }

        public CommandTheButton()
        {
            Title = "Command the button";
            // Create button
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Command = ApplicationCommands.Paste;
            btn.Content = ApplicationCommands.Paste.Text;
            Content = btn;

            // Binding
            CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Paste, PasteOnExecute, PasteCanExecute));
        }

        private void PasteCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsText();
        }

        private void PasteOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Title = Clipboard.GetText();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Title = "Command the button";
        }
    }
}