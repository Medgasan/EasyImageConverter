using ImageCrvLib.Contract;
using ImageCrvLib.Viewers;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ImageCrvLib.Conversors
{
    public class ToPNGSaver : ISaver
    {
        string ImagePath = "";
        string DImagePath;
        private ProgressForm progress;

        public ToPNGSaver(string Image_path, string DImagePath_param)
        {
            ImagePath = Image_path;
            DImagePath = DImagePath_param + Extension[0];
            progress = Helper.ProgForm;
        }


        public string Description => "Salva una imagen en formato PNG";

        public List<string> Extension => new List<string>()
        {
            ".png"
        };

        public void Convert()
        {
            using (var image = new MagickImage(ImagePath))
            {
                try
                {
                    image.Progress += Image_Progress;
                    progress.ShowProgress(true);
                    progress.setText("Convirtiendo y salvando Imagen");
                    image.Write(DImagePath);
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
