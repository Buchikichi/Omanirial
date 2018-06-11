namespace Omanirial
{
    partial class EditingForm
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
            this.components = new System.ComponentModel.Container();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.springLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.Balloon = new System.Windows.Forms.ToolTip(this.components);
            this.PageListView = new System.Windows.Forms.TreeView();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.ColumnsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RowsUpDown = new System.Windows.Forms.NumericUpDown();
            this.ThresholdBar = new System.Windows.Forms.TrackBar();
            this.ThresholdLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CursorButton = new Omanirial.behavior.ToolStripRadioButton();
            this.AddItemButton = new Omanirial.behavior.ToolStripRadioButton();
            this.AddItem2Button = new Omanirial.behavior.ToolStripRadioButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowGridButton = new System.Windows.Forms.ToolStripButton();
            this.BasePictureBox = new Omanirial.behavior.CustomPictureBox();
            this.StatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RowsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdBar)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BasePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Location = new System.Drawing.Point(892, 674);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(80, 30);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "キャンセル";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.BackColor = System.Drawing.Color.PaleGreen;
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SaveButton.Location = new System.Drawing.Point(806, 674);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(80, 30);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "OK";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.springLabel,
            this.ProgressBar});
            this.StatusBar.Location = new System.Drawing.Point(0, 707);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(1008, 22);
            this.StatusBar.TabIndex = 2;
            this.StatusBar.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // springLabel
            // 
            this.springLabel.Name = "springLabel";
            this.springLabel.Size = new System.Drawing.Size(673, 17);
            this.springLabel.Spring = true;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // Balloon
            // 
            this.Balloon.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Balloon.ToolTipTitle = "確認";
            // 
            // PageListView
            // 
            this.PageListView.AllowDrop = true;
            this.PageListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PageListView.Location = new System.Drawing.Point(12, 41);
            this.PageListView.Name = "PageListView";
            this.PageListView.ShowRootLines = false;
            this.PageListView.Size = new System.Drawing.Size(165, 626);
            this.PageListView.TabIndex = 3;
            this.PageListView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.PageListView_AfterSelect);
            this.PageListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.PageListView_DragDrop);
            this.PageListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.PageListView_DragEnter);
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleTextBox.Location = new System.Drawing.Point(12, 12);
            this.TitleTextBox.MaxLength = 200;
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(960, 23);
            this.TitleTextBox.TabIndex = 4;
            // 
            // ColumnsTextBox
            // 
            this.ColumnsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ColumnsTextBox.Enabled = false;
            this.ColumnsTextBox.Location = new System.Drawing.Point(184, 678);
            this.ColumnsTextBox.Name = "ColumnsTextBox";
            this.ColumnsTextBox.Size = new System.Drawing.Size(34, 23);
            this.ColumnsTextBox.TabIndex = 5;
            this.ColumnsTextBox.Text = "99";
            this.ColumnsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 681);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "x";
            // 
            // RowsUpDown
            // 
            this.RowsUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RowsUpDown.Location = new System.Drawing.Point(245, 679);
            this.RowsUpDown.Name = "RowsUpDown";
            this.RowsUpDown.Size = new System.Drawing.Size(42, 23);
            this.RowsUpDown.TabIndex = 7;
            this.RowsUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.RowsUpDown.Value = new decimal(new int[] {
            99,
            0,
            0,
            0});
            // 
            // ThresholdBar
            // 
            this.ThresholdBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ThresholdBar.AutoSize = false;
            this.ThresholdBar.Location = new System.Drawing.Point(319, 682);
            this.ThresholdBar.Maximum = 255;
            this.ThresholdBar.Name = "ThresholdBar";
            this.ThresholdBar.Size = new System.Drawing.Size(300, 20);
            this.ThresholdBar.TabIndex = 8;
            this.ThresholdBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ThresholdBar.Scroll += new System.EventHandler(this.ThresholdBar_Scroll);
            // 
            // ThresholdLabel
            // 
            this.ThresholdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ThresholdLabel.Location = new System.Drawing.Point(293, 679);
            this.ThresholdLabel.Name = "ThresholdLabel";
            this.ThresholdLabel.Size = new System.Drawing.Size(33, 20);
            this.ThresholdLabel.TabIndex = 9;
            this.ThresholdLabel.Text = "999";
            this.ThresholdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CursorButton,
            this.AddItemButton,
            this.AddItem2Button,
            this.toolStripSeparator1,
            this.ShowGridButton});
            this.toolStrip1.Location = new System.Drawing.Point(976, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(32, 707);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CursorButton
            // 
            this.CursorButton.AutoSize = false;
            this.CursorButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CursorButton.BackgroundImage = global::Omanirial.Properties.Resources.Cursor_16x;
            this.CursorButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CursorButton.Checked = true;
            this.CursorButton.Name = "CursorButton";
            this.CursorButton.Size = new System.Drawing.Size(20, 20);
            // 
            // AddItemButton
            // 
            this.AddItemButton.AutoSize = false;
            this.AddItemButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.AddItemButton.BackgroundImage = global::Omanirial.Properties.Resources.AddControl;
            this.AddItemButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AddItemButton.Checked = false;
            this.AddItemButton.Name = "AddItemButton";
            this.AddItemButton.Size = new System.Drawing.Size(20, 20);
            // 
            // AddItem2Button
            // 
            this.AddItem2Button.AutoSize = false;
            this.AddItem2Button.BackgroundImage = global::Omanirial.Properties.Resources.AddControl_2;
            this.AddItem2Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AddItem2Button.Checked = false;
            this.AddItem2Button.Name = "AddItem2Button";
            this.AddItem2Button.Size = new System.Drawing.Size(20, 20);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(29, 6);
            // 
            // ShowGridButton
            // 
            this.ShowGridButton.AutoSize = false;
            this.ShowGridButton.CheckOnClick = true;
            this.ShowGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ShowGridButton.Image = global::Omanirial.Properties.Resources.AppearanceTabGrid_16x;
            this.ShowGridButton.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.ShowGridButton.Name = "ShowGridButton";
            this.ShowGridButton.Size = new System.Drawing.Size(20, 20);
            this.ShowGridButton.Text = "ShowGrid";
            this.ShowGridButton.CheckedChanged += new System.EventHandler(this.ShowGridButton_CheckedChanged);
            // 
            // BasePictureBox
            // 
            this.BasePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BasePictureBox.LastMark = null;
            this.BasePictureBox.Location = new System.Drawing.Point(184, 42);
            this.BasePictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.BasePictureBox.MousePt = null;
            this.BasePictureBox.Name = "BasePictureBox";
            this.BasePictureBox.Page = null;
            this.BasePictureBox.ShowGrid = false;
            this.BasePictureBox.ShowMarks = false;
            this.BasePictureBox.Size = new System.Drawing.Size(787, 625);
            this.BasePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BasePictureBox.TabIndex = 0;
            this.BasePictureBox.TabStop = false;
            // 
            // EditingForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.RowsUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ColumnsTextBox);
            this.Controls.Add(this.TitleTextBox);
            this.Controls.Add(this.PageListView);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.BasePictureBox);
            this.Controls.Add(this.ThresholdLabel);
            this.Controls.Add(this.ThresholdBar);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "EditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "編集";
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RowsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdBar)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BasePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private behavior.CustomPictureBox BasePictureBox;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolTip Balloon;
        private System.Windows.Forms.TreeView PageListView;
        private System.Windows.Forms.TextBox TitleTextBox;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel springLabel;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.TextBox ColumnsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown RowsUpDown;
        private System.Windows.Forms.TrackBar ThresholdBar;
        private System.Windows.Forms.Label ThresholdLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ShowGridButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private behavior.ToolStripRadioButton CursorButton;
        private behavior.ToolStripRadioButton AddItemButton;
        private behavior.ToolStripRadioButton AddItem2Button;
    }
}