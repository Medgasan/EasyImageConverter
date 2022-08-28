using System;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace EasyImageConverter
{
    public partial class Form1 : Form
    {
        private string ImageFullPath;
        private string ImageFile;
        private string ImageType;
        private string ImageName;
        private string ImagePath;

        public Form1()
        {
            InitializeComponent();

            // ------------ Save Formats --------------
            //System.Drawing.Imaging.ImageFormat.Bmp
            //System.Drawing.Imaging.ImageFormat.Emf
            //System.Drawing.Imaging.ImageFormat.Gif
            //System.Drawing.Imaging.ImageFormat.Icon
            //System.Drawing.Imaging.ImageFormat.Jpeg
            //System.Drawing.Imaging.ImageFormat.Png
            //System.Drawing.Imaging.ImageFormat.Tiff
            //System.Drawing.Imaging.ImageFormat.Wmf

            // ------------ Load Formats --------------
            //BMP
            //GIF
            //JPEG
            //PNG
            //TIFF
            try
            {
                FileInfo fi = new FileInfo("c:\\swapfile.sys");
                DateTime lastAccessTime = fi.LastAccessTime;
                _ = lastAccessTime.Ticks;
                //FileStream fs = fi.OpenRead();
                //FileSecurity fsec = fi.GetAccessControl().GetOwner(GetType(System.Security.Principal.NTAccount));
                //Console.WriteLine(fsec.Value);
            }
            catch (IOException e) { 
                MessageBox.Show(e.StackTrace); 
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (!File.Exists(textBox1.Text)) return;
            Image image = Image.FromFile(ImageFullPath);
            image.Save(ImagePath + "\\" + ImageName + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            image.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;

            ImageFullPath = openFileDialog1.FileName.ToLower();
            ImageFile = Path.GetFileName(ImageFullPath);
            ImageType = Path.GetExtension(ImageFile);
            ImageName = Path.GetFileName(ImageFile).Replace(ImageType, "");
            ImagePath = Path.GetDirectoryName(ImageFullPath);

        }
    }
}
