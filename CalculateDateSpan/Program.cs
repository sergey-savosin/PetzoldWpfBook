using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.CalculateDateSpan
{
    class CalculateDateSpan : Window
    {
        TextBox txtboxBegin, txtboxEnd;
        Label lblLifeYears;

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new CalculateDateSpan());
        }

        public CalculateDateSpan()
        {
            Title = "Calculate date span";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;

            // Create grid
            Grid grid = new Grid();
            Content = grid;

            // rows and cols
            for (int i = 0; i<3; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            for (int i=0; i<2; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();
                coldef.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(coldef);
            }

            // first label
            Label lbl = new Label();
            lbl.Content = "Begin date: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);

            // first textbox
            txtboxBegin = new TextBox();
            txtboxBegin.Text = new DateTime(1980, 1, 1).ToShortDateString();
            txtboxBegin.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtboxBegin);
            Grid.SetRow(txtboxBegin, 0);
            Grid.SetColumn(txtboxBegin, 1);

            // Second label
            lbl = new Label();
            lbl.Content = "End date: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 1);
            Grid.SetColumn(lbl, 0);

            // Second textbox
            txtboxEnd = new TextBox();
            txtboxEnd.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtboxEnd);
            Grid.SetRow(txtboxEnd, 1);
            Grid.SetColumn(txtboxEnd, 1);

            // Third label
            lbl = new Label();
            lbl.Content = "Life time span: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 2);
            Grid.SetColumn(lbl, 0);

            // result label
            lblLifeYears = new Label();
            grid.Children.Add(lblLifeYears);
            Grid.SetRow(lblLifeYears, 2);
            Grid.SetColumn(lblLifeYears, 1);

            // margins
            Thickness thick = new Thickness(5); // 1/20 of inch
            grid.Margin = thick;
            foreach (Control ctrl in grid.Children)
            {
                ctrl.Margin = thick;
            }

            // set focus
            txtboxBegin.Focus();
            txtboxEnd.Text = DateTime.Now.ToShortDateString();
            
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime dtBeg, dtEnd;
            if (DateTime.TryParse(txtboxBegin.Text, out dtBeg) &&
                DateTime.TryParse(txtboxEnd.Text, out dtEnd))
            {
                var res = dtEnd - dtBeg;
                lblLifeYears.Content = res.ToString();
            }
            else
            {
                lblLifeYears.Content = "n/a";
            }
        }
    }
}
