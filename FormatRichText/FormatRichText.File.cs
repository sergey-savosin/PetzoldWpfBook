using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.FormatRichText
{
    partial class FormatRichText : Window
    {
        string[] formats =
        {
            DataFormats.Xaml, DataFormats.XamlPackage, DataFormats.Rtf,
			DataFormats.Text, DataFormats.Text
        };

        string strFilter =
            "XAML Documets files (*.xaml)|*.xaml|" +
            "XAML Package Files (*.zip)|*.zip|" +
            "Rich Text Format Files (*.rtf)|*.rtf|" +
            "Text files (*.txt)|*.txt|" +
            "All files (*.*)|*.*";

		void AddFileToolBar(ToolBarTray tray, int band, int index)
        {
            ToolBar toolbar = new ToolBar()
            {
                Band = band,
                BandIndex = index
            };
            tray.ToolBars.Add(toolbar);

            RoutedUICommand[] comm =
            {
                ApplicationCommands.New, ApplicationCommands.Open, ApplicationCommands.Save
            };

            string[] strImages =
            {
                "Document_16x.png", "OpenFile_16x.png", "Save_16x.png"
            };

			for (int i = 0; i<3; i++)
            {
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

            // bindings
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, OnNew));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnOpen));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, OnSave));
        }

        private void OnSave(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog()
            {
                Filter = strFilter
            };

            if ((bool)dlg.ShowDialog(this))
            {
                FlowDocument flow = txtbox.Document;
                TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
                FileStream strm = null;
                try
                {
                    strm = new FileStream(dlg.FileName, FileMode.Create);
                    range.Save(strm, formats[dlg.FilterIndex - 1]);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, Title);
                }
                finally
                {
                    if (strm != null)
                        strm.Close();
                }
            }
        }

        private void OnOpen(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                CheckFileExists = true,
                Filter = strFilter
            };
            if ((bool)dlg.ShowDialog(this))
            {
                FlowDocument flow = txtbox.Document;
                TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
                FileStream strm = null;
                try
                {
                    strm = new FileStream(dlg.FileName, FileMode.Open);
                    range.Load(strm, formats[dlg.FilterIndex - 1]);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, Title);
                }
                finally
                {
                    if (strm != null)
                        strm.Close();
                }
            }
        }

        private void OnNew(object sender, ExecutedRoutedEventArgs e)
        {
            FlowDocument flow = txtbox.Document;
            TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
            range.Text = "";
        }
    }
}
