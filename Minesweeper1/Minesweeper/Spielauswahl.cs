using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Spielauswahl : Form
    {
        // Klasse erstellt Spielauswahl Dialog
        // In diesem kann Spielfeldgroesse und Bombenanzahl gesetzt werden

        // gibt an ob Dialog durch Knopf oder Fenster schließen beendet wurde
        private bool start_button_clicked = false;
        
        public Spielauswahl(Steuerung steuerung,int x_size,int y_size,int num_bombs)
        {
            // Dialog erstellen und Startwerte setzen
            InitializeComponent();
            spielfeldX.Value = x_size;
            spielfeldY.Value = y_size;
            anzBomben.Value = num_bombs;
            FormClosed += (sender, args) => { steuerung.handle_close_auswahl_dialog(sender); };
        }

        public int get_x_size()
        {
            // Spielfeldbreite zurueckgeben
            return (int)spielfeldX.Value;
        }
        public int get_y_size()
        {
            // Spielfeldbreite zurueckgeben
            return (int)spielfeldY.Value;
        }
        public int get_anz_bombs()
        {
            // Bombenzahl zurueckgeben
            return (int)anzBomben.Value;
        }

        public bool get_start_button_clicked()
        {
            // Gibt zurueck ob Dialog per Start button (true) oder durch Fenster schließen (false) geschlossen wurde
            return start_button_clicked;
        }

        private void start_pressed(object sender, EventArgs e)
        {
            // Wenn Spiel Starten Knopf gedrueckt Dialog schließen
            start_button_clicked = true;
            this.Close();
        }
    }
}
