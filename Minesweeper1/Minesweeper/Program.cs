using System;
using System.Windows.Forms;

namespace Minesweeper
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Objekt der Steuerung erstellen
            Steuerung game = new Steuerung();
            // Frame, den die Steuerung erstellt hat, laufen lassen
            Application.Run(game.get_gui());
        }
    }
}

  













