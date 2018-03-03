using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.SetSpaceProperty
{
    public class SpaceButton : Button
    {
        string txt;

        public string Text
        {
            set
            {
                txt = value;
                Content = SpaceOutText(txt);
            }

            get
            {
                return txt;
            }
        }

        public static readonly DependencyProperty SpaceProperty;

        public int Space
        {
            set
            {
                SetValue(SpaceProperty, value);
            }
            get
            {
                return (int)GetValue(SpaceProperty);
            }
        }

        // static constructor
        static SpaceButton()
        {
            // metadata
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata()
            {
                DefaultValue = 1,
                AffectsMeasure = true,
                Inherits = true
            };
            metadata.PropertyChangedCallback += OnSpacePropertyChanged;

            // registering
            SpaceProperty = DependencyProperty.Register(
                "Space",
                typeof(int),
                typeof(SpaceButton),
                metadata,
                ValidateSpaceValue);
        }

        // callback for validate valus
        private static bool ValidateSpaceValue(object value)
        {
            int i = (int)value;
            return i >= 0;
        }

        // callback for notify about changes
        static void OnSpacePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SpaceButton btn = obj as SpaceButton;
            btn.Content = btn.SpaceOutText(btn.txt);
        }

        string SpaceOutText(string str)
        {
            if (str == null)
                return null;

            StringBuilder build = new StringBuilder();
            foreach (char ch in str)
            {
                build.Append(ch + new string(' ', Space));
            }

            return build.ToString();
        }
    }
}
