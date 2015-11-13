/*************************************************************************** 
 * Copyright 2012 Nikolay Manolov
 * 
 * This file is part of NinjaSnipper.

    NinjaSnipper is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    NinjaSnipper is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with NinjaSnipper.  If not, see <http://www.gnu.org/licenses/>.
****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NinjaSnipper
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            FilePathTextBox.Text = folderBrowserDialog1.SelectedPath;
        }

        private void SaveToFolderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveToFolderCheckBox.Checked){
                BrowseButton.Enabled = true;
                FilePathTextBox.Enabled = true;
            }else{
                BrowseButton.Enabled = false;
                FilePathTextBox.Enabled = false;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CatchPrintScreen = PrintScreenCheckBox.Checked;
            Properties.Settings.Default.CopyToClipboard = CopyToClipboardCheckBox.Checked;
            Properties.Settings.Default.SaveToFile = SaveToFolderCheckBox.Checked;
            if (SaveToFolderCheckBox.Checked) Properties.Settings.Default.SavePath = FilePathTextBox.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            PrintScreenCheckBox.Checked = Properties.Settings.Default.CatchPrintScreen;
            CopyToClipboardCheckBox.Checked = Properties.Settings.Default.CopyToClipboard;
            SaveToFolderCheckBox.Checked = Properties.Settings.Default.SaveToFile;
            FilePathTextBox.Text = Properties.Settings.Default.SavePath;
        }
    }
}
