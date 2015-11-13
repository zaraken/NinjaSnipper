namespace NinjaSnipper
{
    partial class SettingsForm
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
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.CopyToClipboardCheckBox = new System.Windows.Forms.CheckBox();
            this.SaveToFolderCheckBox = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.PrintScreenCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(221, 83);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(302, 83);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // CopyToClipboardCheckBox
            // 
            this.CopyToClipboardCheckBox.AutoSize = true;
            this.CopyToClipboardCheckBox.Checked = true;
            this.CopyToClipboardCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CopyToClipboardCheckBox.Location = new System.Drawing.Point(12, 12);
            this.CopyToClipboardCheckBox.Name = "CopyToClipboardCheckBox";
            this.CopyToClipboardCheckBox.Size = new System.Drawing.Size(109, 17);
            this.CopyToClipboardCheckBox.TabIndex = 3;
            this.CopyToClipboardCheckBox.Text = "Copy to Clipboard";
            this.CopyToClipboardCheckBox.UseVisualStyleBackColor = true;
            // 
            // SaveToFolderCheckBox
            // 
            this.SaveToFolderCheckBox.AutoSize = true;
            this.SaveToFolderCheckBox.Checked = true;
            this.SaveToFolderCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SaveToFolderCheckBox.Location = new System.Drawing.Point(12, 35);
            this.SaveToFolderCheckBox.Name = "SaveToFolderCheckBox";
            this.SaveToFolderCheckBox.Size = new System.Drawing.Size(104, 17);
            this.SaveToFolderCheckBox.TabIndex = 4;
            this.SaveToFolderCheckBox.Text = "Save to a Folder";
            this.SaveToFolderCheckBox.UseVisualStyleBackColor = true;
            this.SaveToFolderCheckBox.CheckedChanged += new System.EventHandler(this.SaveToFolderCheckBox_CheckedChanged);
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Location = new System.Drawing.Point(12, 58);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.Size = new System.Drawing.Size(284, 20);
            this.FilePathTextBox.TabIndex = 5;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(302, 55);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 6;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // PrintScreenCheckBox
            // 
            this.PrintScreenCheckBox.AutoSize = true;
            this.PrintScreenCheckBox.Checked = true;
            this.PrintScreenCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PrintScreenCheckBox.Location = new System.Drawing.Point(138, 12);
            this.PrintScreenCheckBox.Name = "PrintScreenCheckBox";
            this.PrintScreenCheckBox.Size = new System.Drawing.Size(158, 17);
            this.PrintScreenCheckBox.TabIndex = 7;
            this.PrintScreenCheckBox.Text = "Process PrintScreen Events";
            this.PrintScreenCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 111);
            this.ControlBox = false;
            this.Controls.Add(this.PrintScreenCheckBox);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.FilePathTextBox);
            this.Controls.Add(this.SaveToFolderCheckBox);
            this.Controls.Add(this.CopyToClipboardCheckBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "SettingsForm";
            this.Text = "NinjaSnipper Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.CheckBox CopyToClipboardCheckBox;
        private System.Windows.Forms.CheckBox SaveToFolderCheckBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.CheckBox PrintScreenCheckBox;
    }
}