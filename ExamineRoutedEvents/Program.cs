using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.ExamineRoutedEvents
{
    public class ExamineRoutedEvents : Application
    {
        static readonly FontFamily fontfam = new FontFamily("Lucida Console");
        const string strFormat = "{0, -30}, {1, -15}, {2, -15}, {3, -15}";
        StackPanel stackOutput;
        DateTime dtLast;

        [STAThread]
        static void Main()
        {
            ExamineRoutedEvents app = new ExamineRoutedEvents();
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs args)
        {
            base.OnStartup(args);

            // Create window
            Window win = new Window()
            {
                Title = "Examine Routed Events"
            };

            // Create grid
            Grid grid = new Grid();
            win.Content = grid;

            // rows
            RowDefinition rowdef = new RowDefinition()
            {
                Height = GridLength.Auto
            };
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition()
            {
                Height = GridLength.Auto
            };
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition()
            {
                Height = new GridLength(100, GridUnitType.Star)
            };
            grid.RowDefinitions.Add(rowdef);

            // create button
            Button btn = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(24),
                Padding = new Thickness(24)
            };
            grid.Children.Add(btn);

            // create TextBlock
            TextBlock text = new TextBlock
            {
                FontSize = 24,
                Text = win.Title
            };
            btn.Content = text;

            // text for a header
            TextBlock textHeadings = new TextBlock
            {
                FontFamily = fontfam
            };
            textHeadings.Inlines.Add(
                new Underline(
                    new Run(
                        String.Format(
                            strFormat,
                            "Routed Event",
                            "sender",
                            "Source",
                            "OriginalSource"))));
            grid.Children.Add(textHeadings);
            Grid.SetRow(textHeadings, 1);

            // create ScrollViewer
            ScrollViewer scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 2);

            // create StackPanel for messages
            stackOutput = new StackPanel();
            scroll.Content = stackOutput;

            // add event handlers
            UIElement[] els = { win, grid, scroll, btn, text };
            foreach(UIElement el in els)
            {
                // keyboard
                el.PreviewKeyDown += AllPurposeEventHandler;
                el.PreviewKeyUp += AllPurposeEventHandler;
                el.PreviewTextInput += AllPurposeEventHandler;
                el.KeyDown += AllPurposeEventHandler;
                el.KeyUp += AllPurposeEventHandler;
                el.TextInput += AllPurposeEventHandler;

                // mouse
                el.MouseDown += AllPurposeEventHandler;
                el.MouseUp += AllPurposeEventHandler;
                el.PreviewMouseDown += AllPurposeEventHandler;
                el.PreviewMouseUp += AllPurposeEventHandler;

                // stilus
                el.StylusDown += AllPurposeEventHandler;
                el.StylusUp += AllPurposeEventHandler;
                el.PreviewStylusUp += AllPurposeEventHandler;
                el.PreviewStylusDown += AllPurposeEventHandler;

                // click
                el.AddHandler(Button.ClickEvent, new RoutedEventHandler(AllPurposeEventHandler));
            }

            // Show window
            win.Show();
        }

        private void AllPurposeEventHandler(object sender, RoutedEventArgs args)
        {
            // show empty line
            DateTime dtNow = DateTime.Now;
            if (dtNow - dtLast > TimeSpan.FromMilliseconds(100))
                stackOutput.Children.Add(new TextBlock(new Run(" ")));
            dtLast = dtNow;

            // show event info
            TextBlock text = new TextBlock
            {
                FontFamily = fontfam,
                Text = String.Format(
                    strFormat,
                    args.RoutedEvent.Name,
                    TypeWithoutNamespace(sender),
                    TypeWithoutNamespace(args.Source),
                    TypeWithoutNamespace(args.OriginalSource))
            };
            stackOutput.Children.Add(text);

            (stackOutput.Parent as ScrollViewer).ScrollToBottom();
        }

        private object TypeWithoutNamespace(object obj)
        {
            string[] astr = obj.GetType().ToString().Split('.');
            return astr[astr.Length - 1];
        }
    }
}
