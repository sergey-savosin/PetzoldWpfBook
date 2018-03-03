using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Petzold.SetSpaceProperty
{
    public class SpaceWindow : Window
    {
        // DependencyProperty and property
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
        static SpaceWindow()
        {
            // define metadata
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata()
            {
                Inherits = true
            };
            SpaceProperty = SpaceButton.SpaceProperty.AddOwner(typeof(SpaceWindow));
            SpaceProperty.OverrideMetadata(typeof(SpaceWindow), metadata);
        }
    }
}
