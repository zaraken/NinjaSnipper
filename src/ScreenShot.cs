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
 * 
 * Loosely based on http://www.codeproject.com/Articles/21913/TeboScreen-Basic-C-Screen-Capture-Application
 * *************************************************************************************/
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;

namespace NinjaSnipper
{
    class ScreenShot
    {
        public static bool saveToClipboard = false;
        private static Timer t = new Timer();

        public static void scheduleClipboardRead()
        {    
            t.Interval = 250;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        static void t_Tick(object sender, EventArgs e)
        {
            t.Stop();
            if (Properties.Settings.Default.SaveToFile){
                String SavePath = "";
                String ImageFormat = Properties.Settings.Default.ImageFormat; // ".jpg"
                if (Properties.Settings.Default.SavePath == "") SavePath = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop";
                SavePath += Properties.Settings.Default.SavePath + "\\scr-" + DateTime.Now.ToString("yyyy-MM-dd-ddd-HH-mm-ss") + ImageFormat;
                ScreenShot.CaptureImageFromClipboard(SavePath, ImageFormat);
            }
        }

        public static void CaptureImageFromClipboard(string FilePath, string extension)
        {
            Image img = Clipboard.GetImage();
            if (FilePath != "" && extension != ""){
                switch (extension)
                {
                    case ".bmp":
                        img.Save(FilePath, ImageFormat.Bmp); break;
                    case ".jpg":
                        img.Save(FilePath, ImageFormat.Jpeg); break;
                    case ".gif":
                        img.Save(FilePath, ImageFormat.Gif); break;
                    case ".tiff":
                        img.Save(FilePath, ImageFormat.Tiff); break;
                    case ".png":
                        img.Save(FilePath, ImageFormat.Png); break;
                    default:
                        img.Save(FilePath, ImageFormat.Jpeg);
                        MessageBox.Show("Image format " + extension + " is not supported." + "Jpeg was used instead.");
                        break;
                }
            }
        }
        public static void CaptureImage(bool showCursor, Size curSize, Point curPos, Point sourcePoint, Point destinationPoint, Rectangle selectionRectangle, string filePath, string extension)
        {
            Bitmap bitmap = new Bitmap(selectionRectangle.Width, selectionRectangle.Height);
            Graphics g = Graphics.FromImage(bitmap);

            g.CopyFromScreen(sourcePoint, destinationPoint, selectionRectangle.Size);

            if (Properties.Settings.Default.CopyToClipboard)
            {
                Image img = (Image)bitmap;
                Clipboard.SetImage(img);
            }
            if (filePath != "" && extension != "")
            {
                switch (extension)
                {
                    case ".bmp":
                        bitmap.Save(filePath, ImageFormat.Bmp); break;
                    case ".jpg":
                        bitmap.Save(filePath, ImageFormat.Jpeg); break;
                    case ".gif":
                        bitmap.Save(filePath, ImageFormat.Gif); break;
                    case ".tiff":
                        bitmap.Save(filePath, ImageFormat.Tiff); break;
                    case ".png":
                        bitmap.Save(filePath, ImageFormat.Png); break;
                    default:
                        bitmap.Save(filePath, ImageFormat.Jpeg);
                        MessageBox.Show("Image format " + extension + " is not supported." + "Jpeg was used instead.");
                        break;
                }
            }
        }
    }
}