namespace Minesweeper
{
    public enum Button_states
    {
        not_marked,   // markierrt
        marked,       // nicht markierrt
        maybe_marked, // vielleicht markiert
        opened        // aufgedeckt
    };
    enum Solved_states
    {
        not_solved,   // Feld nicht vom Computer geloest
        solved,       // Feld wurde vom Computer geloest
    };
    public struct Pos
    {
        public int x;
        public int y;
    }

    public class Minesweeper_Button
    {
        // In Objekten dieser Klasse sind alle fuer das Spiel relevanten Daten eines Spielfeldteils (Button) gespeichert
        // Dabei kann ein Button mehrere Zustaende annehmen (markiert,vielleicht markiert,nicht markiert und aufgedeckt)
        // Zusaetzlich kann ein Button zu einem Bombenfeld gemacht werden 

        // Speicherwerte
        private Pos pos = new Pos();                            // Position im Spielfeld
        private bool is_bomb = false;                           // ist Knopf Bombe oder nicht
        private Button_states state = Button_states.not_marked; // Knopfzustand
        private Solved_states solve_state = Solved_states.not_solved; // Computerloesungszustand


        public Minesweeper_Button(int x, int y)
        {
            // an mitgegebene Position setzten
            pos.x = x;
            pos.y = y;
        }

        // pos get
        public Pos get_pos()
        {
            // Position zurueckgeben
            return pos;
        }

        // bomb get und set
        public bool get_is_bomb()
        {
            // Zurueckgeben ob Feld eine Bombe ist oder nicht
            return is_bomb;
        }

        public void make_to_bomb()
        {
            // Feld zu einer Bombe machen
            is_bomb = true;
        }

        // Zustand get
        public bool get_is_opened()
        {
            // Zurueckgeben ob Feld schon geoeffnet wurde
            return state == Button_states.opened;
        }

        public bool get_is_marked()
        {
            // Zurueckgeben ob markiert oder nicht
            return (state == Button_states.marked || state == Button_states.maybe_marked);
        }

        public Button_states get_state()
        {
            // Zurueckgeben ob markiert oder nicht
            return state;
        }

        // Zustanduebergangsmethoden
        public void mark()
        {
            switch (state)
            {
                case Button_states.opened:
                    break;
                case Button_states.not_marked:
                    state = Button_states.marked;
                    break;
                case Button_states.marked:
                    state = Button_states.maybe_marked;
                    break;
                case Button_states.maybe_marked:
                    state = Button_states.not_marked;
                    break;
                default:
                    // sollte nicht erreicht werden
                    break;
            }
        }

        public void open()
        {
            if (state == Button_states.not_marked)
            {
                state = Button_states.opened;
            }
        }

        // Computerhinweis get und set
        public bool get_is_computer_solved()
        {
            // Zurueckgeben ob Feld vom Computer geloest wurde
            return (solve_state != Solved_states.not_solved);
        }

        public void set_computer_solved()
        {
            // Feld als vom Computer geloest setzten
            solve_state = Solved_states.solved;
        }
    }
}
