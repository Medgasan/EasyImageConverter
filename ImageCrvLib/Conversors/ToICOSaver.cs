using ImageCrvLib.Contract;
using ImageCrvLib.Viewers;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ImageCrvLib.Conversors
{
    public class ToICOSaver : ISaver
    {
        string Image;
        string DImagePath;
        private ProgressForm progress;

        public ToICOSaver(string Image_param, string DImagePath_param)
        {
            Image = Image_param;
            DImagePath = DImagePath_param + Extension[0];
            progress = Helper.ProgForm;
        }

        public string Description => "Salva una imagen en formato ICO";

        public List<string> Extension => new List<string>()
        {
            ".ico"
        };

        public void Convert()
        {

            using (var image = new MagickImage(Image))
            {
                try
                {
                    image.Progress += Image_Progress;
                    progress.ShowProgress(true);
                    progress.setText("Convirtiendo y salvando Imagen");
                    image.Resize(128, 128);
                    image.Write(DImagePath);
                    image.Dispose();
                    progress.ShowProgress(false);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    progress.ShowProgress(false);
                }
            }

        }

        private void Image_Progress(object sender, ProgressEventArgs e)
        {
            progress.SetProgress(e.Progress.ToInt32());
        }
    }
}
