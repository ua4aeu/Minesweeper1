using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Minesweeper
{
    public enum Show_hint
    {
        only_bombs,   // Zum Anzeigen aller bekannten Bomben
        only_free,    // Zum Anzeigen aller freien Felder
        solvable,     // Zum Anzeigen der Felder, die loesbar sind
    };
    public class Steuerung
    {
        // Die Steuerung ist mit der Oberflaeche und den einzelnen Knoepfen assoziiert
        // Sie steuert das Spiel und regelt den zeitlichen Ablauf

        // Spielwerte
        private int x_size = 25;
        private int y_size = 15;
        private int button_size = 50;
        private int num_bombs = 45;
        private int num_clicks = 0;
        private int num_hints = 0;
        
        // Assoziationen
        private Oberflaeche gui;
        private Minesweeper_Button[,] buttons;
        
        // Hilfsobjekte
        private Stopwatch sw = new Stopwatch();
        private Random rand = new Random();

        public Steuerung()
        {
            // Oberflaechen Objekt erstellen und mit Steuerung assoziieren
            gui = new Oberflaeche(this);
        }

        public Oberflaeche get_gui() { return gui; }

        // Knopfdruck Methoden
        public void hint_pressed(Show_hint mode)
        {
            // Je nach mode alle sicheren Bomben, freien Felder oder loesbaren Felder farbig markieren
            bool filter_aus = mode == Show_hint.solvable;
            bool filter = mode == Show_hint.only_bombs;
            num_hints++;
            foreach (Minesweeper_Button btn in buttons)
            {
                if (btn.get_is_opened() == false && btn.get_is_computer_solved() && (filter_aus || btn.get_is_bomb() == filter))
                {
                    gui.show_computer_solve(btn.get_pos().x, btn.get_pos().y, mode);
                }
            }
        }

        public void button_right_pressed(Minesweeper_Button button)
        {
            // Bei rechts Klick Knopf markieren und Bombencounter aktualisieren
            // Knopfzustand wechseln, falls noch nicht geoeffnet: markieren, nicht markieren oder vielleicht markieren
            // Text und Bild werden passend gesetzt
            button.mark();
            gui.show_flag(button.get_pos().x, button.get_pos().y, button.get_state());
            gui.set_bomb_counter(num_bombs - count_marked());
        }

        public void button_pressed(Minesweeper_Button button)
        {
            if (button.get_is_marked() == false)
            {
                // Klick hochzaehlen und Anzeige aktualisieren
                num_clicks++;
                gui.set_click_counter(num_clicks);
                // wenn Bombe aufgedeckt Spiel zuende
                if (button.get_is_bomb())
                {
                    end_game(false);
                }
                else
                {
                    // ansonsten aufdecken und Nachbarn oeffnen
                    aufdecken_und_nachbarn_oeffnen(button);
                    // Die Computerloesung mit eventuell neu gefundenen Informationen aktualisieren
                    do_computer_solve();
                    if (check_alles_aufgedeckt())
                    {
                        // Spiel gewonnen, wenn alles aufgedeckt
                        end_game(true);
                    }
                }
            }
        }

        // Rekursives aufdecken
        private void aufdecken_und_nachbarn_oeffnen(Minesweeper_Button button)
        {
            // Feld aufdecken und eventuell rekursiv Nachbarn auch noch aufdecken
            if (button.get_is_bomb() == false && button.get_is_opened() == false)
            {
                // Wenn Feld keine Bombe ist, Anzahl der Bomben im Umkreis auf Feld schreiben ...
                Pos pos = button.get_pos();
                int num_bombs_around = get_bombs_around_button(button);
                button.open();
                gui.aufdecken(pos.x,pos.y,button.get_is_bomb(),num_bombs_around);
                if (num_bombs_around == 0)
                {
                    // rekursiv die direkten und diagonalen Nachbarn aufdecken
                    foreach (Minesweeper_Button btn in get_buttons_around_button(button))
                    {
                        aufdecken_und_nachbarn_oeffnen(btn);
                    }
                }
            }
        }

        private int get_bombs_around_button(Minesweeper_Button button)
        {
            // Anzahl der Bomben in den Nachbarfeldern zurueckgeben
            int anzahl = 0;
            foreach (Minesweeper_Button btn in get_buttons_around_button(button))
            {
                if (btn.get_is_bomb())
                {
                    anzahl += 1;
                }     
            }
            return anzahl;
        }

        // Computer Hinweise
        private void do_computer_solve()
        {
            // Computerloesung erstellen
            for (int i = 0; i < 10; i++)
            {
                foreach (Minesweeper_Button btn in buttons)
                {
                    // Falls 1: Um ein Feld herum sind bereits alle Bomben gesetzt
                    if (btn.get_is_opened() && get_computer_solved_bomb_fields_around_button(btn) == get_bombs_around_button(btn))
                    {
                        set_computer_solved_around_button(btn);
                    }
                    // Fall 2: Um ein Feld herum sind nurnoch so viel freie Felder wie die Zahl auf dem Feld
                    if (btn.get_is_opened() && get_buttons_around_button(btn).Count - get_computer_solved_free_fields_around_button(btn) == get_bombs_around_button(btn))
                    {
                        set_computer_solved_around_button(btn);
                    }
                }
            }
        }

        private int get_computer_solved_free_fields_around_button(Minesweeper_Button button)
        {
            // Anzahl der vom Computer als freie Felder geloesten Felder zurueckgeben
            int anzahl = 0;
            foreach (Minesweeper_Button btn in get_buttons_around_button(button))
            {
                if ((btn.get_is_opened() || btn.get_is_computer_solved()) && btn.get_is_bomb() == false)
                {
                    anzahl += 1;
                }
            }
            return anzahl;
        }

        private int get_computer_solved_bomb_fields_around_button(Minesweeper_Button button)
        {
            // Anzahl der vom Computer als Bombe geloesten Felder zurueckgeben
            int anzahl = 0;
            foreach (Minesweeper_Button btn in get_buttons_around_button(button))
            {
                if ((btn.get_is_opened() || btn.get_is_computer_solved()) && btn.get_is_bomb() == true)
                {
                    anzahl += 1;
                }
            }
            return anzahl;
        }

        private void set_computer_solved_around_button(Minesweeper_Button button)
        {
            // Alle Knoepfe um den Knopf herum als fuer den Computer geloest setzten
            foreach (Minesweeper_Button btn in get_buttons_around_button(button))
            {
                btn.set_computer_solved();
            }
        }

        // Spielfeldwerte oder Objekte ermitteln
        private int count_marked()
        {
            // Zaehle markierte Bomben (um Anzeige mit Anzahl der Bomben zu berechnen)
            int counter = 0;
            foreach (Minesweeper_Button btn in buttons)
            {
                if (btn.get_is_marked())
                {
                    counter++;
                }
            }
            return counter;
        }

        private List<Minesweeper_Button> get_buttons_around_button(Minesweeper_Button button)
        {
            // Gibt eine Liste mit den um einen Knopf herumliegenden Knoepfe zurueck
            List<Minesweeper_Button> surrounding_buttons = new List<Minesweeper_Button>();
            Pos pos = button.get_pos();
            for (int x = pos.x - 1; x <= pos.x + 1; x++)
            {
                if (0 <= x && x < x_size)
                {
                    for (int y = pos.y - 1; y <= pos.y + 1; y++)
                    {
                        if (0 <= y && y < y_size && (x != pos.x || y != pos.y))
                        {
                            surrounding_buttons.Add(buttons[x, y]);
                        }
                    }
                }
            }
            return surrounding_buttons;
        }

        // Spiel Ende
        private bool check_alles_aufgedeckt()
        {
            // Alle Knoepfe durchgehen und pruefen ob alle nicht Bomben aufgedeckt sind
            bool alles_aufgedeckt = true;
            foreach (Minesweeper_Button btn in buttons)
            {
                if (btn.get_is_opened() == false && btn.get_is_bomb() == false)
                {
                    alles_aufgedeckt = false;
                }
            }
            return alles_aufgedeckt;
        }

        private void end_game(bool won)
        {
            // Felder aufdecken, Gewonnen oder verloren MessageBox zeigen und Spielauswahl Dialog wieder zeigen
            // Zeit stoppen
            sw.Stop();
            // Alles aufdecken
            foreach (Minesweeper_Button btn in buttons)
            {
                gui.aufdecken(btn.get_pos().x, btn.get_pos().y, btn.get_is_bomb(), get_bombs_around_button(btn));
            }
            string time_string = String.Format("{0:00}:{1:00}мин ", sw.Elapsed.Minutes, sw.Elapsed.Seconds);
            if (won) {
                // Gewonnen MessageBox
                if (num_hints == 0)
                {
                    gui.show_message_box("Молодец! Ты сделал это без подсказок! Время: " + time_string + "\n ", "Spiel gewonnen");
                }
                else
                {
                    gui.show_message_box("Молодец! У вас есть это с " + num_hints+ " удалось! Время: " + time_string + "\n ", "Spiel gewonnen");
                }
            }
            else
            {
                // Verloren MessageBox
                gui.show_message_box("Этого, наверное, ничего не было! Время: " + time_string, "Игра проиграна");
            }
            // Spiel Auswahl Dialog oeffnen
            make_auswahl_dialog();
        }

        // Spielauswahl
        public void make_auswahl_dialog()
        {
            // Spiel Auswahl Dialog erstellen und modal anzeigen
            Spielauswahl auswahl_dialog = new Spielauswahl(this,x_size,y_size,num_bombs);
            auswahl_dialog.ShowDialog();
        }

        // Spieltart
        public void handle_close_auswahl_dialog(object sender)
        {
            // Bei ok druecken Werte aus dem Spielauswahl Dialog holen und Spiel starten
            Spielauswahl auswahl_dialog = sender as Spielauswahl;
            if (auswahl_dialog.get_start_button_clicked())
            {
                x_size = auswahl_dialog.get_x_size();
                y_size = auswahl_dialog.get_y_size();
                num_bombs = auswahl_dialog.get_anz_bombs();

                new_game();
            }
            // Bei schliessen des Fensters Anwendung schliessen
            else
            {
                gui.Close();
            }
        }

        private void set_bombs(int anz)
        {
            // Setzt anz viele Bomben auf zufaellige Knoepfe
            int placed_bombs = 0;
            while (placed_bombs < anz)
            {
                int rand_x_pos = rand.Next(0, x_size);
                int rand_y_pos = rand.Next(0, y_size);
                if (buttons[rand_x_pos, rand_y_pos].get_is_bomb() == false)
                {
                    buttons[rand_x_pos, rand_y_pos].make_to_bomb();
                    placed_bombs++;
                }
            }
        }

        private void new_game()
        {
            // Knoepfe erstellen
            buttons = new Minesweeper_Button[x_size, y_size];
            for (int x = 0; x < x_size; x++)
            {
                for (int y = 0; y < y_size; y++)
                {
                    buttons[x, y] = new Minesweeper_Button(x, y);
                }
            }

            // Initalize Funktion der Oberflaeche aufrufen, damit Knoepfe auf Oberflaeche angezeigt werden koennen
            gui.initialize_form(buttons, x_size, y_size, button_size);

            // Klicks zuruecksetzen
            num_clicks = 0;
            gui.set_click_counter(num_clicks);

            // Bomben setzen
            set_bombs(num_bombs);
            gui.set_bomb_counter(num_bombs - count_marked());

            // Tipps Zaehler zuruecksetzten
            num_hints = 0;

            // Zeit neu starten
            sw.Restart();
        }
    }
}
