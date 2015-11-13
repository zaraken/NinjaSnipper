/***************************************************************************************
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
 * *************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NinjaSnipper
{
    public class SystemTrayForm : Form
    {
        private NotifyIcon  trayIcon;
        private ContextMenu trayMenu;
        public SystemTrayForm()
        {
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Snipp", DoSnipp);
            trayMenu.MenuItems.Add("Settings", ShowSettings);
            trayMenu.MenuItems.Add("Help", ShowHelp);
            trayMenu.MenuItems.Add("About", ShowAbout);
            trayMenu.MenuItems.Add("Exit", OnExit);

            trayIcon      = new NotifyIcon();
            trayIcon.Text = "NinjaSnipper";
            trayIcon.Icon = Properties.Resources.NinjaSnipper;
 
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible     = true;
            trayIcon.MouseClick += new MouseEventHandler(trayIcon_MouseClick);
        }

        void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                DoSnipp(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible       = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
            base.OnLoad(e);
        }
        private void ShowHelp(object sender, EventArgs e)
        {
            (new HelpForm()).Show();
        }
        private void ShowAbout(object sender, EventArgs e)
        {
            (new AboutForm()).Show();
        }
        private void ShowSettings(object sender, EventArgs e)
        {
            (new SettingsForm()).Show();
        }
        private void DoSnipp(object sender, EventArgs e)
        {
            SnipForm.doSnip();
        }
        private void OnExit(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing){
                trayIcon.Dispose();
            }
            base.Dispose(isDisposing);
        }
    }
}
