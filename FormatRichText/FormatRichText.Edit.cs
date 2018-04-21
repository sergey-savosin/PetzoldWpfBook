using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.FormatRichText
{
    partial class FormatRichText : Window
    {
        void AddEditToolBar(ToolBarTray tray, int band, int index)
        {
            ToolBar toolbar = new ToolBar()
            {
                Band = band,
                BandIndex = index
            };
            tray.ToolBars.Add(toolbar);

            RoutedUICommand[] comm =
            {
                ApplicationCommands.Cut, ApplicationCommands.Copy,
                ApplicationCommands.Paste, ApplicationCommands.Delete,
                ApplicationCommands.Undo, ApplicationCommands.Redo
            };

            string[] strImages =
            {
                "Cut_16x.png", "Copy_16x.png",
                "Paste_16x.png", "DeleteTag_16x.png",
                "Undo_16x.png", "Redo_16x.png"
            };

            for (int i=0; i<6; i++)
            {
                if (i == 4)
                    toolbar.Items.Add(new Separator());

                Image img = new Image()
                {
                    Source = new BitmapImage(
                        new Uri("pack://application:,,/Images/" + strImages[i])),
                    Stretch = Stretch.None
                };

                ToolTip tip = new ToolTip()
                {
                    Content = comm[i].Text
                };

                Button btn = new Button()
                {
                    Command = comm[i],
                    Content = img,
                    ToolTip = tip
                };

                toolbar.Items.Add(btn);
            }

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, OnDelete, CanDelete));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo));
        }

        private void CanDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !txtbox.Selection.IsEmpty;
        }

        private void OnDelete(object sender, ExecutedRoutedEventArgs e)
        {
            txtbox.Selection.Text = "";
        }
    }
}
