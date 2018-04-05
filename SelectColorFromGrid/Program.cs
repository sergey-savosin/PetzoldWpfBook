using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Petzold.SelectColorFromGrid
{
    class SelectColorFromGrid : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromGrid());
        }

        public SelectColorFromGrid()
        {
            Title = "Select color from grid";
        }
    }
}
