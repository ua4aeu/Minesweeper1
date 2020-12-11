using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    partial class Oberflaeche
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary><<<<<<<<<<
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeStartComponent()
        {
            // titeltext: Minesweeper
            FontFamily titel_font_family = new FontFamily("Courier New");
            Font titel_font = new Font(titel_font_family, 32, FontStyle.Bold);
            this.titel_label = new Label();
            this.titel_label.AutoSize = true;
            this.titel_label.Location = new Point(5, 5);
            this.titel_label.Name = "Titel";
            this.titel_label.Font = titel_font;
            this.titel_label.TabStop = false;
            this.titel_label.Text = "Игра Сапёр";
            this.Controls.Add(titel_label);
            // Info Text
            this.info_label = new Label();
            this.info_label.AutoSize = true;
            this.info_label.Location = new Point(95, 65);
            this.info_label.TextAlign = ContentAlignment.MiddleCenter;
            this.info_label.Name = "Информация";
            this.info_label.TabStop = false;
            this.info_label.Text = "Василенко И.К.";
            this.Controls.Add(info_label);
            // start button
            this.start_button = new Button();
            this.start_button.Location = new Point(140,110);
            this.start_button.AutoSize = true;
            this.start_button.Name = "start_button";
            this.start_button.Text = "Старт";
            this.start_button.Click += (sender,args) => { steuerung.make_auswahl_dialog(); };
            this.Controls.Add(start_button);
            // Oberflaeche
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Name = "Главный экран";
            this.Text = "Сапёр";
            this.Icon = new Icon("img\\flag.ico");
            this.MaximizeBox = false;
            this.ClientSize = new Size(380,150);
        }
        
        private void InitializeComponent(int x_size, int y_size, int button_size)
        {
            FontFamily label_font_family = new FontFamily("Courier New");
            Font label_font = new Font(label_font_family, 18, FontStyle.Bold);
            // Oberflaeche
            this.ClientSize = new Size(5 + x_size * button_size + 5, 5 + 32 + 5 + y_size * button_size + 5 + 30 + 5);
            // num_bombs_label
            this.num_bombs_label = new Label();
            this.num_bombs_label.AutoSize = true;
            this.num_bombs_label.Location = new Point(5, 5);
            this.num_bombs_label.Name = "Кол-во бомб";
            this.num_bombs_label.Font = label_font;
            this.num_bombs_label.TabStop = false;
            this.num_bombs_label.Text = "Бомбы: 000";
            this.Controls.Add(num_bombs_label);
            // num_clicks_label
            this.num_clicks_label = new Label();
            this.num_clicks_label.AutoSize = true;
            this.num_clicks_label.Name = "нажмите номер";
            this.num_clicks_label.Font = label_font;
            this.num_clicks_label.TabStop = false;
            this.num_clicks_label.Text = "Кликов: 000";
            this.num_clicks_label.Location = new Point(x_size * button_size - 205, 5);
            this.Controls.Add(num_clicks_label);
            // solve Bombs button
            this.slove_bombs_button = new Button();
            this.slove_bombs_button.Size = new Size(250, 30);
            this.slove_bombs_button.Location = new Point(5, y_size * button_size + 5 + 37 + 5);
            this.slove_bombs_button.AutoSize = false;
            this.slove_bombs_button.Name = "удалите закладку бомбы";
            this.slove_bombs_button.Text = "Все поля, которые являются бомбами";
            this.slove_bombs_button.Click += (sender, args) => { steuerung.hint_pressed(Show_hint.only_bombs); };
            this.Controls.Add(slove_bombs_button);
            // solve Freie Felder button
            this.solve_free_button = new Button();
            this.solve_free_button.AutoSize = false;
            this.solve_free_button.Size = new Size(250, 30);
            this.solve_free_button.Location = new Point(5 + (x_size * button_size) / 2 - 250 / 2, y_size * button_size + 5 + 37 + 5);
            this.solve_free_button.Name = "решите свободную кнопку";
            this.solve_free_button.Text = "Все поля, которые должны быть свободными";
            this.solve_free_button.Click += (sender, args) => { steuerung.hint_pressed(Show_hint.only_free); };
            this.Controls.Add(solve_free_button);
            // solve solvable Felder button
            this.solve_solvable_button = new Button();
            this.solve_solvable_button.AutoSize = false;
            this.solve_solvable_button.Size = new Size(250, 30);
            this.solve_solvable_button.Name = "решите разрешимую кнопку";
            this.solve_solvable_button.Text = "Все поля, которые разрешимы";
            this.solve_solvable_button.Location = new Point(5 + x_size * button_size - 250, y_size * button_size + 5 + 37 + 5);
            this.solve_solvable_button.Click += (sender, args) => { steuerung.hint_pressed(Show_hint.solvable); };
            this.Controls.Add(solve_solvable_button);
        }

        private void InitializeButtons(Minesweeper_Button[,] buttons, int x_size, int y_size, Action<Minesweeper_Button, MouseEventArgs> click_method)
        {
            // Erstellt die Windows.Forms.Button Objekte und zeigt diese an
            this.buttons = new Button[x_size, y_size];
            for (int x = 0; x < x_size; x++)
            {
                for (int y = 0; y < y_size; y++)
                {
                    Minesweeper_Button ms_button = buttons[x, y];
                    // Button erzeugen
                    Button btn = new Button();
                    // Einstellungen fuer Button
                    btn.Location = new Point(5 + x * button_size, 42 + y * button_size);
                    btn.Name = "кнопка_" + x + "_" + y;
                    btn.Size = new Size(button_size, button_size);
                    btn.Font = button_font;
                    btn.TabStop = false;
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.MouseUp += (sender, MouseEventArgs) => { click_method(ms_button, MouseEventArgs); };
                    this.buttons[x, y] = btn;
                    this.Controls.Add(btn);
                }
            }
        }

        private Label titel_label;
        private Label info_label;
        private Button start_button;
        private Label num_bombs_label;
        private Label num_clicks_label;
        private Button[,] buttons;
        private Button slove_bombs_button;
        private Button solve_free_button;
        private Button solve_solvable_button;
    }
}

