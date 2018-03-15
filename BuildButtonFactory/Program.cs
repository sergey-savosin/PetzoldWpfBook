using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.BuildButtonFactory
{
    class BuildButtonFactory : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new BuildButtonFactory());
        }

        public BuildButtonFactory()
        {
            Title = "Build button factory";

            // create ControlTemplate for Button
            ControlTemplate template = new ControlTemplate(typeof(Button));

            // create FrameworkElementFactory for Border
            FrameworkElementFactory factoryBorder = new FrameworkElementFactory(typeof(Border))
            {
                Name = "border"
            };
            factoryBorder.SetValue(Border.BorderBrushProperty, Brushes.Red);
            factoryBorder.SetValue(Border.BorderThicknessProperty, new Thickness(3));
            factoryBorder.SetValue(Border.BackgroundProperty, SystemColors.ControlLightBrush);

            // create FrameworkElementFactory for ContentPresenter
            FrameworkElementFactory factoryContent = new FrameworkElementFactory(typeof(ContentPresenter))
            {
                Name = "content"
            };
            factoryContent.SetValue(
                ContentPresenter.ContentProperty
                , new TemplateBindingExtension(Button.ContentProperty));

            // Padding = button's Margin
            factoryContent.SetValue(ContentPresenter.MarginProperty,
                new TemplateBindingExtension(Button.PaddingProperty));

            // contentPresenter = child of Border
            factoryBorder.AppendChild(factoryContent);

            // border = root
            template.VisualTree = factoryBorder;

            // trigger for mouseOver
            Trigger trig = new Trigger()
            {
                Property = UIElement.IsMouseOverProperty,
                Value = true
            };

            // link Setter with trigger
            Setter set = new Setter()
            {
                Property = Border.CornerRadiusProperty,
                Value = new CornerRadius(24),
                TargetName = "border"
            };

            trig.Setters.Add(set);

            // setter for FontStyle
            set = new Setter()
            {
                Property = Control.FontStyleProperty,
                Value = FontStyles.Italic
            };
            trig.Setters.Add(set);

            // add trig to template
            template.Triggers.Add(trig);

            // define trigger for IsPressed
            trig = new Trigger()
            {
                Property = Button.IsPressedProperty,
                Value = true
            };
            set = new Setter()
            {
                Property = Border.BackgroundProperty,
                Value = SystemColors.ControlDarkBrush,
                TargetName = "border"
            };
            trig.Setters.Add(set);
            template.Triggers.Add(trig);

            // create Button
            Button btn = new Button()
            {
                Template = template,
                Content = "Button with custom template",
                Padding = new Thickness(20),
                FontSize = 48,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            btn.Click += ButtonOnClick;
            Content = btn;

        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked the button", Title);
        }
    }
}
