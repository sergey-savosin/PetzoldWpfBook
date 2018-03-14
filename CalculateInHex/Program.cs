using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Petzold.CalculateInHex
{
    public class CalculateInHex : Window
    {
        RoundedButton btnDisplay;
        ulong numDisplay;
        ulong numFirst;
        bool bNewNumber = true;
        char chOperation = '=';

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CalculateInHex());
        }

        public CalculateInHex()
        {
            Title = "Calculate In Hex";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;

            // Create grid
            Grid grid = new Grid()
            {
                Margin = new Thickness(4)
            };
            Content = grid;

            // 5 cols
            for (int i = 0; i < 5; i++)
            {
                ColumnDefinition col = new ColumnDefinition()
                {
                    Width = GridLength.Auto
                };
                grid.ColumnDefinitions.Add(col);
            }

            // 7 rows
            for (int i = 0; i < 7; i++)
            {
                RowDefinition row = new RowDefinition()
                {
                    Height = GridLength.Auto
                };
                grid.RowDefinitions.Add(row);
            }

            // buttons' text
            string[] strButtons = { "0",
                "D", "E", "F", "+", "&",
                "A", "B", "C", "-", "|",
                "7", "8", "9", "*", "^",
                "4", "5", "6", "/", "<<",
                "1", "2", "3", "%", ">>",
                "0", "Back", "Equals" };
            int iRow = 0, iCol = 0;

            // create buttons
            foreach (string str in strButtons)
            {
                // create RoundedButton
                RoundedButton btn = new RoundedButton()
                {
                    Focusable = false,
                    Height = 32,
                    Margin = new Thickness(4)
                };
                btn.Click += ButtonOnClick;

                // create TextBlock
                TextBlock txt = new TextBlock()
                {
                    Text = str
                };
                btn.Child = txt;

                // link to grid
                grid.Children.Add(btn);
                Grid.SetRow(btn, iRow);
                Grid.SetColumn(btn, iCol);

                // special buttons:
                // 1. Display
                if (iRow == 0 && iCol == 0)
                {
                    btnDisplay = btn;
                    btn.Margin = new Thickness(4, 4, 4, 6);
                    Grid.SetColumnSpan(btn, 5);
                    iRow = 1;
                }
                // 2. Back and Enter
                else if (iRow == 6 && iCol >= 0)
                {
                    Grid.SetColumnSpan(btn, 2);
                    iCol += 2;
                }
                // 3. All other buttons
                else
                {
                    btn.Width = 32;
                    if (0 == (iCol = (iCol + 1) % 5))
                    {
                        iRow++;
                    }
                }
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            RoundedButton btn = e.Source as RoundedButton;
            if (btn == null)
                return;

            // get text
            string strButton = (btn.Child as TextBlock).Text;
            char chButton = strButton[0];

            // Special cases
            if (strButton == "Equals")
                chButton = '=';

            if (btn == btnDisplay)
                numDisplay = 0;
            else if (strButton == "Back")
                numDisplay /= 16;
            // hex digits
            else if (Char.IsLetterOrDigit(chButton))
            {
                if (bNewNumber)
                {
                    numFirst = numDisplay;
                    numDisplay = 0;
                    bNewNumber = false;
                }
                if (numDisplay <= ulong.MaxValue >> 4)
                {
                    numDisplay = 16 * numDisplay + (ulong)(chButton -
                        (Char.IsDigit(chButton) ? '0' : 'A' - 10));
                }
            }
            // Work case
            else
            {
                if (!bNewNumber)
                {
                    switch(chOperation)
                    {
                        case '=': break;
                        case '+': numDisplay = numFirst + numDisplay; break;
                        case '-': numDisplay = numFirst - numDisplay; break;
                        case '*': numDisplay = numFirst * numDisplay; break;
                        case '&': numDisplay = numFirst & numDisplay; break;
                        case '|': numDisplay = numFirst | numDisplay; break;
                        case '^': numDisplay = numFirst ^ numDisplay; break;
                        case '<': numDisplay = numFirst << (int)numDisplay; break;
                        case '>': numDisplay = numFirst >> (int)numDisplay; break;
                        case '/':
                            numDisplay =
                                numDisplay != 0 ? numFirst / numDisplay : ulong.MaxValue;
                            break;
                        case '%':
                            numDisplay =
                                numDisplay != 0 ? numFirst % numDisplay : ulong.MaxValue;
                            break;
                        default: numDisplay = 0; break;
                    }
                }
                bNewNumber = true;
                chOperation = chButton;
            }

            // formatting output
            TextBlock text = new TextBlock()
            {
                Text = String.Format("{0:X}", numDisplay)
            };
            btnDisplay.Child = text;

        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            if (e.Text.Length == 0)
                return;

            // get pressed key
            char chKey = Char.ToUpper(e.Text[0]);

            // iterate buttons
            foreach (UIElement child in (Content as Grid).Children)
            {
                RoundedButton btn = child as RoundedButton;
                string strButton = (btn.Child as TextBlock).Text;

                // check matching button
                if ((chKey == strButton[0] && btn != btnDisplay &&
                    strButton != "Equals" &&
                    strButton != "Back") ||
                    (chKey == '=' && strButton == "Equals") ||
                    (chKey == '\r' && strButton == "Equals") ||
                    (chKey == '\b' && strButton == "Back") ||
                    (chKey == '\x1B' && btn == btnDisplay))
                {
                    // make Click event
                    RoutedEventArgs argsClick = new RoutedEventArgs(RoundedButton.ClickEvent, btn);
                    btn.RaiseEvent(argsClick);

                    // make Click button
                    btn.IsPressed = true;

                    // start timer
                    DispatcherTimer tmr = new DispatcherTimer()
                    {
                        Interval = TimeSpan.FromMilliseconds(100),
                        Tag = btn
                    };
                    tmr.Tick += TimerOnTick;
                    tmr.Start();
                    e.Handled = true;
                }
            }
        }

        void TimerOnTick(object sender, EventArgs e)
        {
            // cancel button click
            DispatcherTimer tmr = sender as DispatcherTimer;
            RoundedButton btn = tmr.Tag as RoundedButton;
            btn.IsPressed = false;

            // stop timer
            tmr.Stop();
            tmr.Tick -= TimerOnTick;
        }
    }
}
