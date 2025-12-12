namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Text = "Drone Command Lexical Analyzer";

            // Title label
            var lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = "Drone Command Analyzer";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(12, 10);
            this.Controls.Add(lblTitle);

            // Command input label
            var lblCommand = new System.Windows.Forms.Label();
            lblCommand.Text = "Command Input:";
            lblCommand.Location = new System.Drawing.Point(12, 50);
            lblCommand.AutoSize = true;
            this.Controls.Add(lblCommand);

            // Command input textbox (multiline)
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.txtCommand.Multiline = true;
            this.txtCommand.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCommand.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtCommand.Location = new System.Drawing.Point(12, 75);
            this.txtCommand.Size = new System.Drawing.Size(480, 200);
            this.txtCommand.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.Controls.Add(this.txtCommand);

            // Task type label
            var lblTask = new System.Windows.Forms.Label();
            lblTask.Text = "Task Type:";
            lblTask.Location = new System.Drawing.Point(12, 290);
            lblTask.AutoSize = true;
            this.Controls.Add(lblTask);

            // Task type combobox
            this.cmbTaskType = new System.Windows.Forms.ComboBox();
            this.cmbTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaskType.Location = new System.Drawing.Point(12, 315);
            this.cmbTaskType.Size = new System.Drawing.Size(200, 23);
            this.cmbTaskType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.Controls.Add(this.cmbTaskType);

            // Priority label and trackbar
            this.lblPriorityValue = new System.Windows.Forms.Label();
            this.lblPriorityValue.Text = "Priority:3";
            this.lblPriorityValue.Location = new System.Drawing.Point(230, 290);
            this.lblPriorityValue.AutoSize = true;
            this.Controls.Add(this.lblPriorityValue);

            this.trackPriority = new System.Windows.Forms.TrackBar();
            this.trackPriority.Minimum = 1;
            this.trackPriority.Maximum = 5;
            this.trackPriority.Value = 3;
            this.trackPriority.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackPriority.SmallChange = 1;
            this.trackPriority.LargeChange = 1;
            this.trackPriority.Location = new System.Drawing.Point(230, 310);
            this.trackPriority.Size = new System.Drawing.Size(260, 45);
            this.trackPriority.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.trackPriority.ValueChanged += new System.EventHandler(this.trackPriority_ValueChanged);
            this.Controls.Add(this.trackPriority);

            // Operational zone label and textbox
            var lblZone = new System.Windows.Forms.Label();
            lblZone.Text = "Operational Zone:";
            lblZone.Location = new System.Drawing.Point(12, 355);
            lblZone.AutoSize = true;
            this.Controls.Add(lblZone);

            this.txtZone = new System.Windows.Forms.TextBox();
            this.txtZone.Location = new System.Drawing.Point(12, 380);
            this.txtZone.Size = new System.Drawing.Size(300, 23);
            this.txtZone.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.Controls.Add(this.txtZone);

            // Analyze button
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.Location = new System.Drawing.Point(330, 375);
            this.btnAnalyze.Size = new System.Drawing.Size(100, 30);
            this.btnAnalyze.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            this.Controls.Add(this.btnAnalyze);

            // Tokens ListView
            var lblTokens = new System.Windows.Forms.Label();
            lblTokens.Text = "Tokenized Instructions:";
            lblTokens.Location = new System.Drawing.Point(510, 50);
            lblTokens.AutoSize = true;
            this.Controls.Add(lblTokens);

            this.lvTokens = new System.Windows.Forms.ListView();
            this.lvTokens.Location = new System.Drawing.Point(510, 75);
            this.lvTokens.Size = new System.Drawing.Size(470, 260);
            this.lvTokens.View = System.Windows.Forms.View.Details;
            this.lvTokens.FullRowSelect = true;
            this.lvTokens.GridLines = true;
            this.lvTokens.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left;
            this.lvTokens.Columns.Add("Token", 220);
            this.lvTokens.Columns.Add("Type", 220);
            this.Controls.Add(this.lvTokens);

            // Classification report label and textbox
            var lblReport = new System.Windows.Forms.Label();
            lblReport.Text = "Classification Report:";
            lblReport.Location = new System.Drawing.Point(12, 420);
            lblReport.AutoSize = true;
            this.Controls.Add(lblReport);

            this.txtReport = new System.Windows.Forms.TextBox();
            this.txtReport.Multiline = true;
            this.txtReport.ReadOnly = true;
            this.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReport.Location = new System.Drawing.Point(12, 445);
            this.txtReport.Size = new System.Drawing.Size(480, 160);
            this.txtReport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.Controls.Add(this.txtReport);

            // Error flags label and listbox
            var lblErrors = new System.Windows.Forms.Label();
            lblErrors.Text = "Error Flags:";
            lblErrors.Location = new System.Drawing.Point(510, 350);
            lblErrors.AutoSize = true;
            this.Controls.Add(lblErrors);

            this.lstErrors = new System.Windows.Forms.ListBox();
            this.lstErrors.Location = new System.Drawing.Point(510, 375);
            this.lstErrors.Size = new System.Drawing.Size(470, 230);
            this.lstErrors.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.Controls.Add(this.lstErrors);

            // Finalize
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.ComboBox cmbTaskType;
        private System.Windows.Forms.TrackBar trackPriority;
        private System.Windows.Forms.Label lblPriorityValue;
        private System.Windows.Forms.TextBox txtZone;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.ListView lvTokens;
        private System.Windows.Forms.TextBox txtReport;
        private System.Windows.Forms.ListBox lstErrors;
    }
}
