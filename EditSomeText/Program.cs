using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Petzold.EditSomeText
{
    class EditSomeText : Window
    {
        static string strFileName = @"C:\git\PetzoldWpfBook\EditSomeText\EditSomeText.txt";
        TextBox txtbox;

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new EditSomeText());
        }

        public EditSomeText()
        {
            Title = "Edit some text";

            // Create text field
            txtbox = new TextBox();
            txtbox.AcceptsReturn = true;
            txtbox.TextWrapping = TextWrapping.Wrap;
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtbox.KeyDown += TextBoxOnKeyDown;
            Content = txtbox;

            // load file
            try
            {
                txtbox.Text = File.ReadAllText(strFileName);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, Title);
            }

            txtbox.CaretIndex = txtbox.Text.Length;
            txtbox.Focus();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //base.OnClosing(e);
            try
            {
                Directory.CreateDirectory(
                    Path.GetDirectoryName(strFileName));
                File.WriteAllText(strFileName, txtbox.Text);
            }
            catch (Exception exc)
            {
                MessageBoxResult result =
                    MessageBox.Show("File could not be saved: "
                    + exc.Message
                    + "\nClose anyway?",
                    Title,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Exclamation);
                e.Cancel = (result == MessageBoxResult.No);
            }
        }

        private void TextBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                txtbox.SelectedText = DateTime.Now.ToString();
                txtbox.CaretIndex = txtbox.SelectionStart + txtbox.SelectionLength;
            }
        }
    }
}