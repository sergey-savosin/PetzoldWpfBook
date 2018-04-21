using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.FormatRichText
{
    partial class FormatRichText : Window
    {
        ToggleButton[] btnAlignment = new ToggleButton[4];

        void AddParaToolBar(ToolBarTray tray, int band, int index)
        {
            ToolBar toolbar = new ToolBar()
            {
                Band = band,
                BandIndex = index
            };
            tray.ToolBars.Add(toolbar);

            // create elements
            toolbar.Items.Add(btnAlignment[0] =
                CreateButton(TextAlignment.Left, "Align Left", 0, 4));
            toolbar.Items.Add(btnAlignment[1] =
                CreateButton(TextAlignment.Center, "Align Center", 2, 2));
            toolbar.Items.Add(btnAlignment[2] =
                CreateButton(TextAlignment.Right, "Align Right", 4, 0));
            toolbar.Items.Add(btnAlignment[3] =
                CreateButton(TextAlignment.Justify, "Justify", 0, 0));

            txtbox.SelectionChanged += TxtboxOnSelectionChanged2;
        }

        ToggleButton CreateButton(TextAlignment align, string strToolTip, int offsetLeft, int offsetRight)
        {
            // create panel
            Canvas canv = new Canvas()
            {
                Width = 16,
                Height = 16
            };

            ToolTip tip = new ToolTip()
            {
                Content = strToolTip
            };

            // create button
            ToggleButton btn = new ToggleButton()
            {
                Tag = align,
                Content = canv,
                ToolTip = tip
            };
            btn.Click += BtnOnClick;

            // draw lines
            for (int i = 0; i<5; i++)
            {
                Polyline poly = new Polyline()
                {
                    Stroke = SystemColors.WindowTextBrush,
                    StrokeThickness = 1
                };
                if ((i & 1) == 0)
                    poly.Points = new PointCollection(
                        new Point[]
                        {
                            new Point(2, 2+3*i), new Point(14, 2+3*i)
                        });
                else
                    poly.Points = new PointCollection(
                        new Point[]
                        {
                            new Point(2 + offsetLeft, 2+3*i),
                            new Point(14 - offsetRight, 2+3*i)
                        });
                canv.Children.Add(poly);
            }

            return btn;
        }

        private void TxtboxOnSelectionChanged2(object sender, RoutedEventArgs e)
        {
            // get current TextAlignment
            object obj = txtbox.Selection.GetPropertyValue(
                Paragraph.TextAlignmentProperty);

            if (obj!=null && obj is TextAlignment)
            {
                TextAlignment align = (TextAlignment)obj;
                foreach (ToggleButton btn in btnAlignment)
                    btn.IsChecked = (align == (TextAlignment)btn.Tag);
            }
            else
            {
                foreach (ToggleButton btn in btnAlignment)
                    btn.IsChecked = false;
            }
        }

        private void BtnOnClick(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = e.Source as ToggleButton;
            foreach (ToggleButton btnAlign in btnAlignment)
                btnAlign.IsChecked = (btn == btnAlign);

            // set new value to text
            TextAlignment align = (TextAlignment)btn.Tag;
            txtbox.Selection.ApplyPropertyValue(
                Paragraph.TextAlignmentProperty, align);
        }
    }
}
