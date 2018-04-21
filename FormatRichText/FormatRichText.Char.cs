using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.FormatRichText
{
    partial class FormatRichText : Window
    {
        ComboBox comboFamily, comboSize;
        ToggleButton btnBold, btnItalic;
        ColorGridBox clrboxBackground, clrboxForeground;

        void AddCharToolBar(ToolBarTray tray, int band, int index)
        {
            ToolBar toolbar = new ToolBar()
            {
                Band = band,
                BandIndex = index
            };
            tray.ToolBars.Add(toolbar);

            // font family box
            ToolTip tip = new ToolTip()
            {
                Content = "Font Family"
            };

            comboFamily = new ComboBox()
            {
                Width = 144,
                ItemsSource = Fonts.SystemFontFamilies,
                SelectedItem = txtbox.FontFamily,
                ToolTip = tip
            };
            comboFamily.SelectionChanged += ComboFamilyOnSelectionChanged;
            toolbar.Items.Add(comboFamily);

            // font size box
            tip = new ToolTip()
            {
                Content = "Font Size"
            };

            comboSize = new ComboBox()
            {
                Width = 48,
                IsEditable = true,
                Text = (0.75 * txtbox.FontSize).ToString(),
                ItemsSource = new double[]
                {
                    8, 9, 10, 11, 12, 14, 16, 18,
                    20, 22, 24, 26, 28, 36, 48, 72
                }
            };
            comboSize.SelectionChanged += ComboSizeOnSelectionChanged;
            comboSize.GotKeyboardFocus += ComboSizeOnGotKeyboardFocus;
            comboSize.LostKeyboardFocus += ComboSizeOnLostKeyboardFocus;
            comboSize.PreviewKeyDown += ComboSizeOnPreviewKeyDown;
            toolbar.Items.Add(comboSize);

            // Bold button
            Image img = new Image()
            {
                Source = new BitmapImage(
                    new Uri("pack://application:,,/Images/Bold_16x.png")),
                Stretch = Stretch.None
            };

            tip = new ToolTip()
            {
                Content = "Bold"
            };

            btnBold = new ToggleButton()
            {
                Content = img,
                ToolTip = tip
            };
            btnBold.Checked += BtnBoldOnChecked;
            btnBold.Unchecked += BtnBoldOnChecked;
            toolbar.Items.Add(btnBold);

            // Italic button
            img = new Image()
            {
                Source = new BitmapImage(
                    new Uri("pack://application:,,/Images/Italic_16x.png")),
                Stretch = Stretch.None
            };

            tip = new ToolTip()
            {
                Content = "Italic"
            };

            btnItalic = new ToggleButton()
            {
                Content = img,
                ToolTip = tip
            };
            btnItalic.Checked += BtnItalicOnChecked;
            btnItalic.Unchecked += BtnItalicOnChecked;
            toolbar.Items.Add(btnItalic);

            // color menu
            Menu menu = new Menu();
            toolbar.Items.Add(menu);

            // Background menu
            img = new Image()
            {
                Source = new BitmapImage(
                    new Uri("pack://application:,,/Images/ColorPalette_16x.png")),
                Stretch = Stretch.None
            };

            clrboxBackground = new ColorGridBox();
            clrboxBackground.SelectionChanged += ClrboxBackgroundOnSelectionChanged;

            tip = new ToolTip()
            {
                Content = "Background color"
            };

            MenuItem item = new MenuItem()
            {
                Header = img,
                ToolTip = tip
            };
            item.Items.Add(clrboxBackground);
            menu.Items.Add(item);

            // Foreground menu

            img = new Image()
            {
                Source = new BitmapImage(
                    new Uri("pack://application:,,/Images/FontColor_16x.png")),
                Stretch = Stretch.None
            };

            clrboxForeground = new ColorGridBox();
            clrboxForeground.SelectionChanged += ClrboxForegroundOnSelectionChanged;

            tip = new ToolTip()
            {
                Content = "Foreground color"
            };

            item = new MenuItem()
            {
                Header = img,
                ToolTip = tip
            };
            item.Items.Add(clrboxForeground);
            menu.Items.Add(item);

            txtbox.SelectionChanged += TxtboxOnSelectionChanged;
        }

        private void TxtboxOnSelectionChanged(object sender, RoutedEventArgs e)
        {
            // get font family
            object obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontFamilyProperty);

            // init combobox
            if (obj is FontFamily)
                comboFamily.SelectedItem = (FontFamily)obj;
            else
                comboFamily.SelectedIndex = -1;

            // get font size
            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontSizeProperty);

            // init size combobox
            if (obj is double)
                comboSize.Text = (0.75 * (double)obj).ToString();
            else
                comboSize.SelectedIndex = -1;

            // get bold property
            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontWeightProperty);

            // init bold toggle button
            if (obj is FontWeight)
                btnBold.IsChecked = (FontWeight)obj == FontWeights.Bold;

            // get FontStyle property
            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontStyleProperty);

            // init italic toggle button
            if (obj is FontStyle)
                btnItalic.IsChecked = (FontStyle)obj == FontStyles.Italic;

            // get colors
            obj = txtbox.Selection.GetPropertyValue(FlowDocument.BackgroundProperty);
            if (obj != null && obj is Brush)
                clrboxBackground.SelectedValue = (Brush)obj;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.ForegroundProperty);
            if (obj != null && obj is Brush)
                clrboxForeground.SelectedValue = (Brush)obj;
        }

        string strOriginal;

        private void ClrboxBackgroundOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorGridBox clrbox = e.Source as ColorGridBox;
            txtbox.Selection.ApplyPropertyValue(
                FlowDocument.BackgroundProperty,
                clrbox.SelectedValue);
        }

        private void ClrboxForegroundOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorGridBox clrbox = e.Source as ColorGridBox;
            txtbox.Selection.ApplyPropertyValue(
                FlowDocument.ForegroundProperty,
                clrbox.SelectedValue);
        }

        private void BtnItalicOnChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = e.Source as ToggleButton;
            txtbox.Selection.ApplyPropertyValue(
                FlowDocument.FontStyleProperty,
                (bool)btn.IsChecked ? FontStyles.Italic : FontStyles.Normal);
        }

        private void BtnBoldOnChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = e.Source as ToggleButton;
            txtbox.Selection.ApplyPropertyValue(
                FlowDocument.FontWeightProperty,
                (bool)btn.IsChecked ? FontWeights.Bold : FontWeights.Normal);
        }

        private void ComboSizeOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                (sender as ComboBox).Text = strOriginal;
                e.Handled = true;
                txtbox.Focus();
            }
            else if (e.Key == Key.Enter)
            {
                e.Handled = true;
                txtbox.Focus();
            }
        }

        private void ComboSizeOnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            double size;

            if (Double.TryParse((sender as ComboBox).Text, out size))
                txtbox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, size / 0.75);
            else
                (sender as ComboBox).Text = strOriginal;
        }

        private void ComboSizeOnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            strOriginal = (sender as ComboBox).Text;
        }

        private void ComboSizeOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = e.Source as ComboBox;
            if (combo.SelectedIndex != -1)
            {
                double size = (double)combo.SelectedValue;
                txtbox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, size / 0.75);
                txtbox.Focus();
            }
        }

        private void ComboFamilyOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = e.Source as ComboBox;
            FontFamily family = combo.SelectedItem as FontFamily;

            // apply seleted value to selection
            if (family != null)
                txtbox.Selection.ApplyPropertyValue(FlowDocument.FontFamilyProperty, family);

            txtbox.Focus();
        }
    }
}
