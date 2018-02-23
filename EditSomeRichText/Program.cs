using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace EditSomeRichText
{
    class EditSomeRichText : Window
    {
        RichTextBox txtbox;
        string strFilter = "Document files(*.xaml)|*.xaml|All files(*.*)|*.*";

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new EditSomeRichText());
        }

        public EditSomeRichText()
        {
            Title = "Edit some rich text";
            txtbox = new RichTextBox();
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Content = txtbox;

            txtbox.Focus();
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (e.ControlText.Length > 0 &&
                e.ControlText[0] == '\x0F')
            {
                OpenFileDialog dlg = new OpenFileDialog
                {
                    CheckFileExists = true,
                    Filter = strFilter
                };

                if ((bool)dlg.ShowDialog(this))
                {
                    FlowDocument flow = txtbox.Document;
                    TextRange range = new TextRange(flow.ContentStart,
                        flow.ContentEnd);

                    Stream strm = null;

                    try
                    {
                        strm = new FileStream(dlg.FileName, FileMode.Open);
                        range.Load(strm, DataFormats.Xaml);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, Title);
                    }
                    finally
                    {
                        if (strm != null)
                            strm.Close();
                    }

                    //try
                    //{
                    //    using (var strm2 = new FileStream(dlg.FileName, FileMode.Open))
                    //    {
                    //        range.Load(strm, DataFormats.Xaml);
                    //    }
                    //}
                    //catch (Exception exc)
                    //{
                    //    MessageBox.Show(exc.Message, Title);
                    //}
                }

                e.Handled = true;
            }

            if (e.ControlText.Length > 0 &&
                e.ControlText[0] == '\x13')
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    Filter = strFilter
                };

                if ((bool)dlg.ShowDialog(this))
                {
                    FlowDocument flow = txtbox.Document;
                    TextRange range = new TextRange(flow.ContentStart,
                        flow.ContentEnd);
                    Stream strm = null;

                    try
                    {
                        strm = new FileStream(dlg.FileName, FileMode.Create);
                        range.Save(strm, DataFormats.Xaml);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, Title);
                    }
                    finally
                    {
                        if (strm != null)
                            strm.Close();
                    }
                }

                e.Handled = true;
            }

            base.OnPreviewTextInput(e);

        }

    }
}
