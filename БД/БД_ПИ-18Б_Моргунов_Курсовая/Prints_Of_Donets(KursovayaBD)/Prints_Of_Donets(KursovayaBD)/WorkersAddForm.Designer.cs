namespace Prints_Of_Donets_KursovayaBD_
{
    partial class WorkersAddForm
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
            this.panel = new System.Windows.Forms.Panel();
            this.second_nameTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.third_nameTextBox = new System.Windows.Forms.TextBox();
            this.printComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.first_nameTextBox = new System.Windows.Forms.TextBox();
            this.backButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.second_nameTextBox);
            this.panel.Controls.Add(this.label7);
            this.panel.Controls.Add(this.third_nameTextBox);
            this.panel.Controls.Add(this.printComboBox);
            this.panel.Controls.Add(this.label8);
            this.panel.Controls.Add(this.label9);
            this.panel.Controls.Add(this.label10);
            this.panel.Controls.Add(this.first_nameTextBox);
            this.panel.Controls.Add(this.backButton);
            this.panel.Controls.Add(this.addButton);
            this.panel.Location = new System.Drawing.Point(12, 12);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(367, 426);
            this.panel.TabIndex = 1;
            // 
            // second_nameTextBox
            // 
            this.second_nameTextBox.Location = new System.Drawing.Point(172, 108);
            this.second_nameTextBox.Name = "second_nameTextBox";
            this.second_nameTextBox.Size = new System.Drawing.Size(134, 20);
            this.second_nameTextBox.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Количество()";
            // 
            // third_nameTextBox
            // 
            this.third_nameTextBox.Location = new System.Drawing.Point(172, 160);
            this.third_nameTextBox.Name = "third_nameTextBox";
            this.third_nameTextBox.Size = new System.Drawing.Size(134, 20);
            this.third_nameTextBox.TabIndex = 24;
            // 
            // printComboBox
            // 
            this.printComboBox.FormattingEnabled = true;
            this.printComboBox.Location = new System.Drawing.Point(172, 222);
            this.printComboBox.Name = "printComboBox";
            this.printComboBox.Size = new System.Drawing.Size(134, 21);
            this.printComboBox.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 225);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Тпография";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Фамилия";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Имя";
            // 
            // first_nameTextBox
            // 
            this.first_nameTextBox.Location = new System.Drawing.Point(172, 61);
            this.first_nameTextBox.Name = "first_nameTextBox";
            this.first_nameTextBox.Size = new System.Drawing.Size(134, 20);
            this.first_nameTextBox.TabIndex = 19;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(211, 275);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(104, 51);
            this.backButton.TabIndex = 13;
            this.backButton.Text = "Назад";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(47, 275);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(97, 51);
            this.addButton.TabIndex = 12;
            this.addButton.Text = "Добавить";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Отчество";
            // 
            // WorkersAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 370);
            this.Controls.Add(this.panel);
            this.Name = "WorkersAddForm";
            this.Text = "Добавление элемента в таблицу \"Работники\"";
            this.Load += new System.EventHandler(this.AddForm_Load);
            this.Click += new System.EventHandler(this.addButton_Click);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox second_nameTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox third_nameTextBox;
        private System.Windows.Forms.ComboBox printComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox first_nameTextBox;
        private System.Windows.Forms.Label label1;
    }
}