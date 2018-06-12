namespace Omanirial
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ImageListBox = new System.Windows.Forms.ListBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.Balloon = new System.Windows.Forms.ToolTip(this.components);
            this.BaseDirTextBox = new System.Windows.Forms.TextBox();
            this.ScanButton = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.LayoutListBox = new System.Windows.Forms.ToolStripComboBox();
            this.AddLayoutButton = new System.Windows.Forms.ToolStripButton();
            this.EditLayoutButton = new System.Windows.Forms.ToolStripButton();
            this.BasePictureBox = new Omanirial.behavior.CustomPictureBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BasePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageListBox
            // 
            this.ImageListBox.AllowDrop = true;
            this.ImageListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ImageListBox.FormattingEnabled = true;
            this.ImageListBox.ItemHeight = 16;
            this.ImageListBox.Location = new System.Drawing.Point(12, 32);
            this.ImageListBox.Name = "ImageListBox";
            this.ImageListBox.Size = new System.Drawing.Size(160, 628);
            this.ImageListBox.TabIndex = 4;
            this.ImageListBox.SelectedIndexChanged += new System.EventHandler(this.ImageListBox_SelectedIndexChanged);
            this.ImageListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImageListBox_DragDrop);
            this.ImageListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImageListBox_DragEnter);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.BackColor = System.Drawing.Color.MistyRose;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Location = new System.Drawing.Point(916, 674);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(80, 30);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "保存";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 707);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(1008, 22);
            this.StatusBar.TabIndex = 4;
            this.StatusBar.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 681);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "入出力：";
            // 
            // Balloon
            // 
            this.Balloon.IsBalloon = true;
            this.Balloon.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Balloon.ToolTipTitle = "確認";
            // 
            // BaseDirTextBox
            // 
            this.BaseDirTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BaseDirTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Omanirial.Properties.Settings.Default, "BaseDir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BaseDirTextBox.Location = new System.Drawing.Point(233, 678);
            this.BaseDirTextBox.Name = "BaseDirTextBox";
            this.BaseDirTextBox.Size = new System.Drawing.Size(677, 23);
            this.BaseDirTextBox.TabIndex = 0;
            this.BaseDirTextBox.TabStop = false;
            this.BaseDirTextBox.Text = global::Omanirial.Properties.Settings.Default.BaseDir;
            this.BaseDirTextBox.DoubleClick += new System.EventHandler(this.BaseDirTextBox_DoubleClick);
            // 
            // ScanButton
            // 
            this.ScanButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ScanButton.BackColor = System.Drawing.Color.Azure;
            this.ScanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ScanButton.Location = new System.Drawing.Point(13, 674);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(75, 30);
            this.ScanButton.TabIndex = 6;
            this.ScanButton.Text = "Scan";
            this.ScanButton.UseVisualStyleBackColor = false;
            this.ScanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LayoutListBox,
            this.AddLayoutButton,
            this.EditLayoutButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 28);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // LayoutListBox
            // 
            this.LayoutListBox.AutoSize = false;
            this.LayoutListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayoutListBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.LayoutListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LayoutListBox.Name = "LayoutListBox";
            this.LayoutListBox.Size = new System.Drawing.Size(160, 28);
            // 
            // AddLayoutButton
            // 
            this.AddLayoutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddLayoutButton.Image = global::Omanirial.Properties.Resources.AddLayoutItem_16x;
            this.AddLayoutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddLayoutButton.Name = "AddLayoutButton";
            this.AddLayoutButton.Size = new System.Drawing.Size(23, 25);
            this.AddLayoutButton.Text = "toolStripButton1";
            this.AddLayoutButton.ToolTipText = "新規レイアウト";
            this.AddLayoutButton.Click += new System.EventHandler(this.AddLayoutButton_Click);
            // 
            // EditLayoutButton
            // 
            this.EditLayoutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditLayoutButton.Image = global::Omanirial.Properties.Resources.EditLayoutTable_16x;
            this.EditLayoutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditLayoutButton.Name = "EditLayoutButton";
            this.EditLayoutButton.Size = new System.Drawing.Size(23, 25);
            this.EditLayoutButton.Text = "toolStripButton2";
            this.EditLayoutButton.ToolTipText = "レイアウト編集";
            this.EditLayoutButton.Click += new System.EventHandler(this.EditLayoutButton_Click);
            // 
            // BasePictureBox
            // 
            this.BasePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BasePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.BasePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BasePictureBox.LastMark = null;
            this.BasePictureBox.Location = new System.Drawing.Point(179, 32);
            this.BasePictureBox.MousePt = new System.Drawing.Point(0, 0);
            this.BasePictureBox.Name = "BasePictureBox";
            this.BasePictureBox.Page = null;
            this.BasePictureBox.ShowGrid = false;
            this.BasePictureBox.ShowMarks = true;
            this.BasePictureBox.Size = new System.Drawing.Size(817, 636);
            this.BasePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BasePictureBox.TabIndex = 0;
            this.BasePictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ScanButton);
            this.Controls.Add(this.BaseDirTextBox);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ImageListBox);
            this.Controls.Add(this.BasePictureBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BasePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private behavior.CustomPictureBox BasePictureBox;
        private System.Windows.Forms.ListBox ImageListBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BaseDirTextBox;
        private System.Windows.Forms.ToolTip Balloon;
        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox LayoutListBox;
        private System.Windows.Forms.ToolStripButton AddLayoutButton;
        private System.Windows.Forms.ToolStripButton EditLayoutButton;
    }
}

