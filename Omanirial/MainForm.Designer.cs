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
            this.BasePictureBox = new Omanirial.behavior.CustomPictureBox();
            this.LayoutListBox = new System.Windows.Forms.ComboBox();
            this.ImageListBox = new System.Windows.Forms.ListBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.Balloon = new System.Windows.Forms.ToolTip(this.components);
            this.BaseDirTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.BasePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BasePictureBox
            // 
            this.BasePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BasePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.BasePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BasePictureBox.Location = new System.Drawing.Point(179, 12);
            this.BasePictureBox.MousePt = new System.Drawing.Point(0, 0);
            this.BasePictureBox.Name = "BasePictureBox";
            this.BasePictureBox.Page = null;
            this.BasePictureBox.Size = new System.Drawing.Size(817, 656);
            this.BasePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BasePictureBox.TabIndex = 0;
            this.BasePictureBox.TabStop = false;
            // 
            // LayoutListBox
            // 
            this.LayoutListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayoutListBox.FormattingEnabled = true;
            this.LayoutListBox.Items.AddRange(new object[] {
            "Q677819スペースワン",
            "編集",
            "削除"});
            this.LayoutListBox.Location = new System.Drawing.Point(13, 13);
            this.LayoutListBox.Name = "LayoutListBox";
            this.LayoutListBox.Size = new System.Drawing.Size(160, 24);
            this.LayoutListBox.TabIndex = 1;
            // 
            // ImageListBox
            // 
            this.ImageListBox.AllowDrop = true;
            this.ImageListBox.FormattingEnabled = true;
            this.ImageListBox.ItemHeight = 16;
            this.ImageListBox.Location = new System.Drawing.Point(13, 79);
            this.ImageListBox.Name = "ImageListBox";
            this.ImageListBox.Size = new System.Drawing.Size(160, 580);
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
            // CreateButton
            // 
            this.CreateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateButton.Location = new System.Drawing.Point(13, 43);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(70, 30);
            this.CreateButton.TabIndex = 2;
            this.CreateButton.Text = "新規";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditButton.Location = new System.Drawing.Point(103, 43);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(70, 30);
            this.EditButton.TabIndex = 3;
            this.EditButton.Text = "編集";
            this.EditButton.UseVisualStyleBackColor = true;
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
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.BaseDirTextBox);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ImageListBox);
            this.Controls.Add(this.LayoutListBox);
            this.Controls.Add(this.BasePictureBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.BasePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private behavior.CustomPictureBox BasePictureBox;
        private System.Windows.Forms.ComboBox LayoutListBox;
        private System.Windows.Forms.ListBox ImageListBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BaseDirTextBox;
        private System.Windows.Forms.ToolTip Balloon;
    }
}

