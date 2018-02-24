using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.TuneTheRadio
{
    class TuneTheRadio : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new TuneTheRadio());
        }

        public TuneTheRadio()
        {
            Title = "Tune the radio";
            SizeToContent = SizeToContent.WidthAndHeight;

            GroupBox group = new GroupBox();
            group.Header = "Window Style";
            group.Margin = new Thickness(96);
            group.Padding = new Thickness(5);
            Content = group;

            StackPanel stack = new StackPanel();
            group.Content = stack;

            stack.Children.Add(
                CreateRadioButton("No border or caption", WindowStyle.None));
            stack.Children.Add(
                CreateRadioButton("Single-border window", WindowStyle.SingleBorderWindow));
            stack.Children.Add(
                CreateRadioButton("3D-border window", WindowStyle.ThreeDBorderWindow));
            stack.Children.Add(
                CreateRadioButton("Tool window", WindowStyle.ToolWindow));
            AddHandler(RadioButton.CheckedEvent, new RoutedEventHandler(RadioOnChecked));
        }

        private void RadioOnChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = e.Source as RadioButton;
            this.WindowStyle = (WindowStyle)radio.Tag;
        }

        private RadioButton CreateRadioButton(string strText, WindowStyle winstyle)
        {
            RadioButton radio = new RadioButton();
            radio.Content = strText;
            radio.Tag = winstyle;
            radio.Margin = new Thickness(5);
            radio.IsChecked = (winstyle == this.WindowStyle);

            return radio;
        }
    }
}
