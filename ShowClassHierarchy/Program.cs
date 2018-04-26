using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Petzold.ShowClassHierarchy
{
    class ShowClassHierarchy : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ShowClassHierarchy());
        }

        public ShowClassHierarchy()
        {
            Title = "Show class hierarchy";

            ClassHierarchyTreeView treevue = new ClassHierarchyTreeView(
                typeof(System.Windows.Threading.DispatcherObject));
            Content = treevue;
        }
    }
}
