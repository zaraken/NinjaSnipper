/************************************************************************
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
 * ***********************************************************************/
// Loosely based on http://www.codeproject.com/Articles/21913/TeboScreen-Basic-C-Screen-Capture-Application

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace NinjaSnipper
{
    public partial class SnipForm : Form
    {
        public bool mouseButtonDown = false;
        public bool rectangleDrawn = false;
        string savePath = "";
        string imageFormat = "";

        public Point clickPoint = new Point();
        public Point currentTopLeft = new Point();
        public Point currentBottomRight = new Point();
        public Point virtualScrOrigin = new Point();

        Graphics g;
        Pen selectionPen = new Pen(Color.Red, 3);
        SolidBrush transparentBrush = new SolidBrush(Color.White);
        Pen eraserPen = new Pen(Color.FromArgb(192, 192, 255), 3);
        SolidBrush eraserBrush = new SolidBrush(Color.FromArgb(192, 192, 255));

        private static bool running = false; // this flag is to prevent the form being launched multiple times

        public static void doSnip()
        {
            if (SnipForm.running) return;
            else { SnipForm.running = true; (new SnipForm()).Show(); }
        }

        public SnipForm()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(mouse_Down);
            this.MouseUp += new MouseEventHandler(mouse_Up);
            this.MouseMove += new MouseEventHandler(mouse_Move);
            this.KeyUp += new KeyEventHandler(key_press);
            this.MouseClick += new MouseEventHandler(SnipForm_MouseClick);
            this.Bounds = new Rectangle(SystemInformation.VirtualScreen.X, SystemInformation.VirtualScreen.Y, SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            virtualScrOrigin.X = SystemInformation.VirtualScreen.X;
            virtualScrOrigin.Y = SystemInformation.VirtualScreen.Y;
            g = this.CreateGraphics();
            
        }

        public void key_press(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape){
                SnipForm.running = false;
                this.Close();
            }
        }

        void SnipForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle){
                SnipForm.running = false;
                this.Close();
            }
        }

        private void mouse_Down(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) {
                mouseButtonDown = true;
                clickPoint = new Point(System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y);
                currentTopLeft.X = currentBottomRight.X = clickPoint.X;
                currentTopLeft.Y = currentBottomRight.Y = clickPoint.Y;
            }
        }

        private void mouse_Up(object sender, MouseEventArgs e)
        {
            mouseButtonDown = false;
            if (e.Button == MouseButtons.Left) SaveSelection(false, false, false);
            if (e.Button == MouseButtons.Right) SaveSelection(false, true, false);
        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (mouseButtonDown && !rectangleDrawn){
                DrawSelection();
            }
        }

        private void DrawSelection()
        {
            this.Cursor = Cursors.Arrow;

            //Erase the previous rectangle
            g.DrawRectangle(eraserPen, currentTopLeft.X - virtualScrOrigin.X, currentTopLeft.Y - virtualScrOrigin.Y, currentBottomRight.X - currentTopLeft.X, currentBottomRight.Y - currentTopLeft.Y);
            g.FillRectangle(eraserBrush, currentTopLeft.X - virtualScrOrigin.X, currentTopLeft.Y - virtualScrOrigin.Y, currentBottomRight.X - currentTopLeft.X, currentBottomRight.Y - currentTopLeft.Y);
            
            //Calculate X Coordinates
            currentTopLeft.X = ((Cursor.Position.X < clickPoint.X) ? Cursor.Position.X : clickPoint.X);
            currentBottomRight.X = ((Cursor.Position.X < clickPoint.X) ? clickPoint.X : Cursor.Position.X);
            //Calculate Y Coordinates
            currentTopLeft.Y = ((Cursor.Position.Y < clickPoint.Y) ? Cursor.Position.Y : clickPoint.Y);
            currentBottomRight.Y = ((Cursor.Position.Y < clickPoint.Y) ? clickPoint.Y : Cursor.Position.Y);

            //Draw a new rectangle
            g.DrawRectangle(selectionPen, currentTopLeft.X - virtualScrOrigin.X, currentTopLeft.Y - virtualScrOrigin.Y, currentBottomRight.X - currentTopLeft.X, currentBottomRight.Y - currentTopLeft.Y);
            g.FillRectangle(transparentBrush, currentTopLeft.X - virtualScrOrigin.X, currentTopLeft.Y - virtualScrOrigin.Y, currentBottomRight.X - currentTopLeft.X, currentBottomRight.Y - currentTopLeft.Y);
        }


        public void SaveSelection(bool showCursor, bool chooseLocation, bool fullscreen)
        {
            Point curPos = new Point(Cursor.Position.X - currentTopLeft.X, Cursor.Position.Y - currentTopLeft.Y);
            Size curSize = new Size();
            curSize.Height = Cursor.Current.Size.Height;
            curSize.Width = Cursor.Current.Size.Width;

            Point startPoint = new Point(0, 0);
            Rectangle bounds = new Rectangle(startPoint, new Size(1200, 700));
            if (!fullscreen){
                startPoint = new Point(currentTopLeft.X, currentTopLeft.Y);
                bounds = new Rectangle(currentTopLeft.X, currentTopLeft.Y, currentBottomRight.X - currentTopLeft.X, currentBottomRight.Y - currentTopLeft.Y);
            }
            if (bounds.Height == 0 || bounds.Width == 0) { SnipForm.running = false; this.Close(); return; }

            if (chooseLocation || !(Properties.Settings.Default.SaveToFile || Properties.Settings.Default.CopyToClipboard)){
                saveFileDialog1.DefaultExt = "jpg";
                saveFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|gif files (*.gif)|*.gif|tiff files (*.tiff)|*.tiff|png files (*.png)|*.png";
                saveFileDialog1.Title = "Save screenshot to...";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    savePath = saveFileDialog1.FileName;
                    imageFormat = new FileInfo(savePath).Extension;
                }
            }
            else{
                imageFormat = Properties.Settings.Default.ImageFormat; // ".jpg"
                if (Properties.Settings.Default.SavePath == "") savePath = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop";
                savePath += Properties.Settings.Default.SavePath + "\\scr-" + DateTime.Now.ToString("yyyy-MM-dd-ddd-HH-mm-ss") + imageFormat;
            }

            ScreenShot.CaptureImage(showCursor, curSize, curPos, startPoint, Point.Empty, bounds, savePath, imageFormat);
            SnipForm.running = false;
            this.Close();
        }

        private void SnipForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(SystemInformation.VirtualScreen.X,SystemInformation.VirtualScreen.Y);
            this.Size = new Size(SystemInformation.VirtualScreen.Width,SystemInformation.VirtualScreen.Height);
        }
    }
}