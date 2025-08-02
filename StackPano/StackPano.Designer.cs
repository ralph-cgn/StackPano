namespace StackPano
{
    partial class StackPano
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
            this.rootLabel = new System.Windows.Forms.Label();
            this.rootTextBox = new System.Windows.Forms.TextBox();
            this.rootButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.methodLabel = new System.Windows.Forms.Label();
            this.methodComboBox = new System.Windows.Forms.ComboBox();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.radiusTextBox = new System.Windows.Forms.TextBox();
            this.smoothingLabel = new System.Windows.Forms.Label();
            this.smoothingTextBox = new System.Windows.Forms.TextBox();
            this.progressLabel = new System.Windows.Forms.Label();
            this.compNrCheckBox = new System.Windows.Forms.CheckBox();
            this.createSubdirsCheckBox = new System.Windows.Forms.CheckBox();
            this.stackDepthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.stackDepthLabel = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.rawCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.stackDepthNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // rootLabel
            // 
            this.rootLabel.AutoSize = true;
            this.rootLabel.Location = new System.Drawing.Point(22, 31);
            this.rootLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.rootLabel.Name = "rootLabel";
            this.rootLabel.Size = new System.Drawing.Size(52, 25);
            this.rootLabel.TabIndex = 0;
            this.rootLabel.Text = "Root";
            // 
            // rootTextBox
            // 
            this.rootTextBox.Location = new System.Drawing.Point(116, 29);
            this.rootTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.rootTextBox.Name = "rootTextBox";
            this.rootTextBox.Size = new System.Drawing.Size(552, 29);
            this.rootTextBox.TabIndex = 0;
            this.rootTextBox.Text = "C:\\Users\\rsche\\Bilder\\Camera Roll\\Technik\\Elektronik\\IC";
            this.rootTextBox.TextChanged += new System.EventHandler(this.rootTextBox_TextChanged);
            // 
            // rootButton
            // 
            this.rootButton.Location = new System.Drawing.Point(715, 26);
            this.rootButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.rootButton.Name = "rootButton";
            this.rootButton.Size = new System.Drawing.Size(110, 42);
            this.rootButton.TabIndex = 8;
            this.rootButton.Text = "Select";
            this.rootButton.UseVisualStyleBackColor = true;
            this.rootButton.Click += new System.EventHandler(this.rootButton_Click);
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(715, 92);
            this.startButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(110, 42);
            this.startButton.TabIndex = 7;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // methodLabel
            // 
            this.methodLabel.AutoSize = true;
            this.methodLabel.Location = new System.Drawing.Point(22, 97);
            this.methodLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.methodLabel.Name = "methodLabel";
            this.methodLabel.Size = new System.Drawing.Size(78, 25);
            this.methodLabel.TabIndex = 5;
            this.methodLabel.Text = "Method";
            // 
            // methodComboBox
            // 
            this.methodComboBox.FormattingEnabled = true;
            this.methodComboBox.Items.AddRange(new object[] {
            "A (weighted average)",
            "B (depth image - sorted)",
            "C (pyramid)"});
            this.methodComboBox.Location = new System.Drawing.Point(115, 93);
            this.methodComboBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.methodComboBox.Name = "methodComboBox";
            this.methodComboBox.Size = new System.Drawing.Size(234, 32);
            this.methodComboBox.TabIndex = 1;
            this.methodComboBox.Text = "A (weighted average)";
            this.methodComboBox.SelectedIndexChanged += new System.EventHandler(this.methodComboBox_SelectedIndexChanged);
            // 
            // radiusLabel
            // 
            this.radiusLabel.AutoSize = true;
            this.radiusLabel.Location = new System.Drawing.Point(359, 97);
            this.radiusLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.radiusLabel.Name = "radiusLabel";
            this.radiusLabel.Size = new System.Drawing.Size(72, 25);
            this.radiusLabel.TabIndex = 6;
            this.radiusLabel.Text = "Radius";
            // 
            // radiusTextBox
            // 
            this.radiusTextBox.Location = new System.Drawing.Point(442, 95);
            this.radiusTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.radiusTextBox.Name = "radiusTextBox";
            this.radiusTextBox.Size = new System.Drawing.Size(48, 29);
            this.radiusTextBox.TabIndex = 2;
            this.radiusTextBox.Text = "1";
            this.radiusTextBox.TextChanged += new System.EventHandler(this.radiusTextBox_TextChanged);
            // 
            // smoothingLabel
            // 
            this.smoothingLabel.AutoSize = true;
            this.smoothingLabel.Location = new System.Drawing.Point(504, 97);
            this.smoothingLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.smoothingLabel.Name = "smoothingLabel";
            this.smoothingLabel.Size = new System.Drawing.Size(106, 25);
            this.smoothingLabel.TabIndex = 8;
            this.smoothingLabel.Text = "Smoothing";
            // 
            // smoothingTextBox
            // 
            this.smoothingTextBox.Location = new System.Drawing.Point(620, 95);
            this.smoothingTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.smoothingTextBox.Name = "smoothingTextBox";
            this.smoothingTextBox.Size = new System.Drawing.Size(48, 29);
            this.smoothingTextBox.TabIndex = 3;
            this.smoothingTextBox.Text = "1";
            this.smoothingTextBox.TextChanged += new System.EventHandler(this.smoothingTextBox_TextChanged);
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(22, 213);
            this.progressLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(69, 25);
            this.progressLabel.TabIndex = 9;
            this.progressLabel.Text = "bla bla";
            // 
            // compNrCheckBox
            // 
            this.compNrCheckBox.AutoSize = true;
            this.compNrCheckBox.Location = new System.Drawing.Point(22, 157);
            this.compNrCheckBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.compNrCheckBox.Name = "compNrCheckBox";
            this.compNrCheckBox.Size = new System.Drawing.Size(172, 29);
            this.compNrCheckBox.TabIndex = 4;
            this.compNrCheckBox.Text = "Comp. Nr. Files";
            this.compNrCheckBox.UseVisualStyleBackColor = true;
            this.compNrCheckBox.CheckedChanged += new System.EventHandler(this.compNrCheckBox_CheckedChanged);
            // 
            // createSubdirsCheckBox
            // 
            this.createSubdirsCheckBox.AutoSize = true;
            this.createSubdirsCheckBox.Checked = true;
            this.createSubdirsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createSubdirsCheckBox.Location = new System.Drawing.Point(208, 157);
            this.createSubdirsCheckBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.createSubdirsCheckBox.Name = "createSubdirsCheckBox";
            this.createSubdirsCheckBox.Size = new System.Drawing.Size(169, 29);
            this.createSubdirsCheckBox.TabIndex = 5;
            this.createSubdirsCheckBox.Text = "Create Subdirs";
            this.createSubdirsCheckBox.UseVisualStyleBackColor = true;
            this.createSubdirsCheckBox.CheckedChanged += new System.EventHandler(this.createSubdirsCheckBox_CheckedChanged);
            // 
            // stackDepthNumericUpDown
            // 
            this.stackDepthNumericUpDown.Location = new System.Drawing.Point(585, 157);
            this.stackDepthNumericUpDown.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.stackDepthNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.stackDepthNumericUpDown.Name = "stackDepthNumericUpDown";
            this.stackDepthNumericUpDown.Size = new System.Drawing.Size(86, 29);
            this.stackDepthNumericUpDown.TabIndex = 6;
            this.stackDepthNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // stackDepthLabel
            // 
            this.stackDepthLabel.AutoSize = true;
            this.stackDepthLabel.Location = new System.Drawing.Point(491, 158);
            this.stackDepthLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.stackDepthLabel.Name = "stackDepthLabel";
            this.stackDepthLabel.Size = new System.Drawing.Size(88, 25);
            this.stackDepthLabel.TabIndex = 13;
            this.stackDepthLabel.Text = "S. Depth";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(715, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 42);
            this.button1.TabIndex = 14;
            this.button1.Text = "Merge";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rawCheckBox
            // 
            this.rawCheckBox.AutoSize = true;
            this.rawCheckBox.Location = new System.Drawing.Point(386, 157);
            this.rawCheckBox.Name = "rawCheckBox";
            this.rawCheckBox.Size = new System.Drawing.Size(85, 29);
            this.rawCheckBox.TabIndex = 15;
            this.rawCheckBox.Text = "RAW";
            this.rawCheckBox.UseVisualStyleBackColor = true;
            // 
            // StackPano
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 251);
            this.Controls.Add(this.rawCheckBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.stackDepthLabel);
            this.Controls.Add(this.stackDepthNumericUpDown);
            this.Controls.Add(this.createSubdirsCheckBox);
            this.Controls.Add(this.compNrCheckBox);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.smoothingTextBox);
            this.Controls.Add(this.smoothingLabel);
            this.Controls.Add(this.radiusTextBox);
            this.Controls.Add(this.radiusLabel);
            this.Controls.Add(this.methodComboBox);
            this.Controls.Add(this.methodLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.rootButton);
            this.Controls.Add(this.rootTextBox);
            this.Controls.Add(this.rootLabel);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(885, 315);
            this.MinimumSize = new System.Drawing.Size(885, 315);
            this.Name = "StackPano";
            this.Text = "StackPano";
            ((System.ComponentModel.ISupportInitialize)(this.stackDepthNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label rootLabel;
        private System.Windows.Forms.TextBox rootTextBox;
        private System.Windows.Forms.Button rootButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label methodLabel;
        private System.Windows.Forms.ComboBox methodComboBox;
        private System.Windows.Forms.Label radiusLabel;
        private System.Windows.Forms.TextBox radiusTextBox;
        private System.Windows.Forms.Label smoothingLabel;
        private System.Windows.Forms.TextBox smoothingTextBox;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.CheckBox compNrCheckBox;
        private System.Windows.Forms.CheckBox createSubdirsCheckBox;
        private System.Windows.Forms.NumericUpDown stackDepthNumericUpDown;
        private System.Windows.Forms.Label stackDepthLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox rawCheckBox;
    }
}

