using System;
using System.Windows.Forms;
using System.Drawing;

namespace Minesweeper
{
    public partial class Oberflaeche : Form
    {
        // Assoziation
        Steuerung steuerung;

        // Buttonsize (um zu pruefen ob Maus nach Klick noch auf dem Knopf ist)
        int button_size;

        // Design (fuer alle Instanzen gleich, daher static)
        private static String[] text_colors = { "silver", "blue", "green", "red", "navy", "maroon", "teal", "black", "grey" };
        private static String solved_as_bomb_color = "lightcoral";
        private static String solved_as_free_color = "lightgreen";
        private static String solved_as_solvable_color = "lightblue";
        private static String button_color = "lightgrey";
        private static Image flag_img = Image.FromFile("img\\flag.png");
        private static Image flag_img2 = Image.FromFile("img\\flag2.png");
        private static Image bomb_img = Image.FromFile("img\\bomb.png");
        private static Font button_font = new Font(SystemFonts.DefaultFont.Name, 11, FontStyle.Bold);
       

        public Oberflaeche(Steuerung steuerung)
        {
            // Assoziation zu Steuerung herstellen
            this.steuerung = steuerung;

            // Startbildschirm anzeigen
            InitializeStartComponent();
        }

        // initialisiren und zeichnen
        public void initialize_form(Minesweeper_Button[,] buttons, int x_size, int y_size, int button_size) 
        {
            // Knopfe und Fenster anzeigen
            this.button_size = button_size;
            this.Controls.Clear();
            InitializeButtons(buttons, x_size, y_size, button_pressed); 
            InitializeComponent(x_size,y_size,button_size); 
        }

        // Knopfdruck Methode
        private void button_pressed(Minesweeper_Button button, MouseEventArgs e)
        {
            // Wenn Maus noch auf dem Knopf ist, Links- oder Rechtsklick an Steuerung weitergeben
            if (e.X >= 0 && e.X < button_size && e.Y >= 0 && e.Y < button_size)
            switch (e.Button)
            {
                case MouseButtons.Left:
                    steuerung.button_pressed(button);
                    break;
                case MouseButtons.Right:
                    steuerung.button_right_pressed(button);
                    break;
                default:
                    break;
            }
        }

        // Aussehen einzelner Knoepfe aendern (Flagge, aufdecken oder Hinweis)
        public void show_computer_solve(int x, int y, Show_hint mode)
        {
            // Hinweis in passender Farbe je nach mode anzeigen
            switch (mode)
            {
                case Show_hint.only_bombs:
                    this.buttons[x,y].BackColor = Color.FromName(solved_as_bomb_color);
                    break;
                case Show_hint.only_free:
                    this.buttons[x, y].BackColor = Color.FromName(solved_as_free_color);
                    break;
                case Show_hint.solvable:
                    this.buttons[x, y].BackColor = Color.FromName(solved_as_solvable_color);
                    break;
                default:
                    //sollte nicht erreicht werden
                    break;
            }
        }

        public void show_flag(int x, int y, Button_states state)
        {
            // Flagge auf Feld anzeigen
            switch (state)
            {
                case Button_states.opened:
                    // keine Flagge, da schon geoeffnet
                    break;
                case Button_states.not_marked:
                    // keine Flagge
                    buttons[x, y].Text = "";
                    buttons[x, y].Image = null;
                    break;
                case Button_states.marked:
                    // rote Flagge
                    buttons[x, y].Text = "";
                    buttons[x, y].Image = flag_img;
                    break;
                case Button_states.maybe_marked:
                    // blaue Flagge
                    buttons[x, y].Text = "";
                    buttons[x, y].Image = flag_img2;
                    break;
                default:
                    // sollte nicht erreicht werden
                    break;
            }
        }

        public void aufdecken(int x, int y, bool is_bomb, int number)
        {
            // Feld als aufgedeckt anzeigen
            if (is_bomb)
            {
                // Bei Bombe kein Text aber Bild
                buttons[x,y].BackColor = Color.FromName(button_color);
                buttons[x, y].Text = "";
                buttons[x, y].Image = bomb_img;
            }
            else
            {
                // ansonsten Zahl in passender Farbe auf dunklerem Hintergrund und kein Bild
                buttons[x,y].Text = number.ToString();
                buttons[x,y].ForeColor = Color.FromName(text_colors[number]);
                buttons[x,y].BackColor = Color.FromName(text_colors[0]);
                buttons[x, y].Image = null;
            }
        }

        // Counter aktualisieren
        public void set_bomb_counter(int number)
        {
            // Bombenzaehler aktualisieren
            this.num_bombs_label.Text = String.Format("Бомб: {0:000}", number);
        }

        public void set_click_counter(int number)
        {
            // Klickzaehler aktualisieren
            this.num_clicks_label.Text = String.Format("Кликов: {0:000}", number);
        }

        // Message Box nach Gewinnen oder Verlieren
        public void show_message_box(string text, string title)
        {
            MessageBox.Show(this, text, title);
        }
    }
}
