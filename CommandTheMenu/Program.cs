using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Petzold.CommandTheMenu
{
    class CommandTheMenu : Window
    {
        TextBlock text;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new CommandTheMenu());
        }

        public CommandTheMenu()
        {
            Title = "Command the menu";

            DockPanel dock = new DockPanel();
            Content = dock;

            // menu
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // textblock
            text = new TextBlock()
            {
                Text = "Sample clipboard text",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 32,
                TextWrapping = TextWrapping.Wrap
            };
            dock.Children.Add(text);

            // menu Edit
            MenuItem itemEdit = new MenuItem()
            {
                Header = "_Edit"
            };
            menu.Items.Add(itemEdit);

            // Edit commands
            MenuItem itemCut = new MenuItem()
            {
                Header = "C_ut",
                Command = ApplicationCommands.Cut
            };
            itemEdit.Items.Add(itemCut);

            MenuItem itemCopy = new MenuItem()
            {
                Header = "_Copy",
                Command = ApplicationCommands.Copy
            };
            itemEdit.Items.Add(itemCopy);

            MenuItem itemPaste = new MenuItem()
            {
                Header = "_Paste",
                Command = ApplicationCommands.Paste
            };
            itemEdit.Items.Add(itemPaste);

            MenuItem itemDelete = new MenuItem()
            {
                Header = "_Delete",
                Command = ApplicationCommands.Delete
            };
            itemEdit.Items.Add(itemDelete);

            // Restore - custom command
            InputGestureCollection collGestures = new InputGestureCollection();
            collGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));

            RoutedUICommand commRestore = new RoutedUICommand("_Restore", "Restore", GetType(), collGestures);

            MenuItem itemRestore = new MenuItem()
            {
                Header = "_Restore",
                Command = commRestore
            };
            itemEdit.Items.Add(itemRestore);

            // Bindings
            CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Cut, CutOnExecute, CutCanExecute));
            CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Copy, CopyOnExecute, CutCanExecute));
            CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Paste, PasteOnExecute, PasteCanExecute));
            CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Delete, DeleteOnExecute, CutCanExecute));
            CommandBindings.Add(
                new CommandBinding(commRestore, RestoreOnExecuted));
        }

        private void RestoreOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            text.Text = "Sampled clipboard text: " + DateTime.Now;
        }

        private void DeleteOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            text.Text = null;
        }

        private void PasteCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsText();
        }

        private void PasteOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                text.Text = Clipboard.GetText();
            }
            catch (Exception ex)
            {
                text.Text = "at Clipboard.GetText() => " + ex.Message;
            }
        }

        private void CopyOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText(text.Text);
            }
            catch (Exception ex)
            {
                text.Text = "at Clipboard.SetText() => " + ex.Message;
            }
        }

        private void CutCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = text.Text != null && text.Text.Length > 0;
        }

        private void CutOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            ApplicationCommands.Copy.Execute(null, this);
            ApplicationCommands.Delete.Execute(null, this);
        }
    }
}
