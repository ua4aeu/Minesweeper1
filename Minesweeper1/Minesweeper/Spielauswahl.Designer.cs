namespace Minesweeper
{
    partial class Spielauswahl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.spielfeldX = new System.Windows.Forms.NumericUpDown();
            this.spielfeldY = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.anzBomben = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.spielfeldX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spielfeldY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.anzBomben)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(296, 90);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(116, 22);
            this.startButton.TabIndex = 10;
            this.startButton.Text = "Начать игру";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.start_pressed);
            // 
            // spielfeldX
            // 
            this.spielfeldX.Location = new System.Drawing.Point(162, 20);
            this.spielfeldX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spielfeldX.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.spielfeldX.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.spielfeldX.Name = "spielfeldX";
            this.spielfeldX.Size = new System.Drawing.Size(66, 23);
            this.spielfeldX.TabIndex = 0;
            this.spielfeldX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spielfeldX.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // spielfeldY
            // 
            this.spielfeldY.Location = new System.Drawing.Point(268, 20);
            this.spielfeldY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spielfeldY.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.spielfeldY.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spielfeldY.Name = "spielfeldY";
            this.spielfeldY.Size = new System.Drawing.Size(66, 23);
            this.spielfeldY.TabIndex = 1;
            this.spielfeldY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spielfeldY.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Размер игрового поля:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "на";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Количество бомб:";
            // 
            // anzBomben
            // 
            this.anzBomben.Location = new System.Drawing.Point(162, 58);
            this.anzBomben.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.anzBomben.Maximum = new decimal(new int[] {
            125,
            0,
            0,
            0});
            this.anzBomben.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.anzBomben.Name = "anzBomben";
            this.anzBomben.Size = new System.Drawing.Size(66, 23);
            this.anzBomben.TabIndex = 9;
            this.anzBomben.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.anzBomben.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // Spielauswahl
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 122);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.anzBomben);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.spielfeldY);
            this.Controls.Add(this.spielfeldX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Spielauswahl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Сапёр";
            ((System.ComponentModel.ISupportInitialize)(this.spielfeldX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spielfeldY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.anzBomben)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown spielfeldX;
        private System.Windows.Forms.NumericUpDown spielfeldY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown anzBomben;
        private System.Windows.Forms.Button startButton;
    }
}