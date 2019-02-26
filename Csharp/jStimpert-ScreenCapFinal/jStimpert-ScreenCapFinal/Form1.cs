using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;



namespace jStimpert_ScreenCapFinal
{
    public partial class Form1 : Form
    {
        Rectangle selectionRect = new Rectangle();
        Color penColor = Color.Red;
        bool openFileOnSave = true;


        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //Method to capture screen image based on provided location and size. If no arguments are provided method will take screenshot of entire monitor space.
        public void CaptureScreen(int xValue = 0, int yValue = 0, int height = 0, int width = 0) {
            Rectangle captureArea = new Rectangle();
            //Force garbage collection each capture to avoid loading memory with garbage.
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            //if sent a rectangle, use those values. Otherwise, set capture rectangle to entire screenspace
            if (height != 0 && width != 0)
            {
                captureArea = new Rectangle(xValue, yValue, height, width);
            }
            else
            {
                captureArea = Screen.AllScreens[0].Bounds;
            }
            //Create bitmap of same size as rectangle
            Bitmap screenshot = new Bitmap(captureArea.Width, captureArea.Height, PixelFormat.Format32bppArgb);
            //Create Graphics object and use data from bitmap
            Graphics screenshotGrabber = Graphics.FromImage(screenshot);
            //Copy image from screen to graphics object
            screenshotGrabber.CopyFromScreen(captureArea.Location.X, captureArea.Location.Y, 0, 0, captureArea.Size);
            //Show image in picture box
            pictureBox1.Image = screenshot;
            screenshotGrabber.Dispose();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Cross;
            CaptureScreen();
        }


        //Clear image in picturebox
        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }


        private void button1_Click(object sender, EventArgs e)
        {
        }


        private void button4_Click(object sender, EventArgs e)
        {
            //If there is a picture to be saved, open file dialog and get location
            if (pictureBox1.Image != null)
            {
                SaveFileDialog saveImageDialog = new SaveFileDialog();
                saveImageDialog.Filter = "Jpeg Image|*.jpg";
                saveImageDialog.Title = "Save a Screenshot";
                saveImageDialog.ShowDialog();
                //Once a valid file name is recieved, open IO stream to the file, send image data in jpeg format, and close filestream.
                if (saveImageDialog.FileName != "")
                {
                    System.IO.FileStream fs = (System.IO.FileStream)saveImageDialog.OpenFile();
                    pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                    fs.Close();
                    if (openFileOnSave == true)
                        System.Diagnostics.Process.Start(saveImageDialog.FileName.ToString());
                    else
                        MessageBox.Show("Save Successful", "File Saved");
                }
            }
            //Show error if no screenshot has been taken yet.
            else
                MessageBox.Show("Must first capture a screenshot to save.","Error: No Screenshot");
        }


        private void mouse_Move(object sender, MouseEventArgs e)
        {
            //If the left mouse button is being held while moving over the pictureBox, keep track of location and compare against location from initial click -- then build rectangle based on information
            if (e.Button == MouseButtons.Left)
            {
                int x = Math.Min(selectionRect.Location.X, e.X);
                int y = Math.Min(selectionRect.Location.Y, e.Y);
                int width = Math.Max(selectionRect.Location.X, e.X) - Math.Min(selectionRect.Location.X, e.X);
                int height = Math.Max(selectionRect.Location.Y, e.Y) - Math.Min(selectionRect.Location.Y, e.Y);
                selectionRect = new Rectangle(x, y, width, height);
                Refresh();
            }
        
        }


        //Paint method to paint selection box into pictureBox
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(penColor, 1))
            {
                e.Graphics.DrawRectangle(pen, selectionRect);
            }
        }


        //set initial click location to build rectangle with.
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e){
            pictureBox1.Cursor = Cursors.Cross;
            selectionRect.Location = e.Location;
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) { }


        //Once mouse is released, take screenshot of cropped area and reprint. Since the rectangle can only be over the screenshot, there is no reason to use the old image. 
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //Set pen color to trasparent and clear old image data to avoid it showing up in screenshot
            penColor = Color.Transparent;
            pictureBox1.Image = null;
            Refresh();
            //Capture area inside rectangle specified by mouse move event
            CaptureScreen(selectionRect.X, selectionRect.Y, selectionRect.Width, selectionRect.Height);
            //Reset pen color, delete rectangle, and redraw image.
            pictureBox1.Cursor = Cursors.Default;
            penColor = Color.Red;
            selectionRect = new Rectangle();
            Refresh();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                openFileOnSave = true;
            else
                openFileOnSave = false;
        }
    }
}
