using ImageCrvLib.Contract;
using ImageCrvLib.Viewers;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ImageCrvLib.Conversors
{
    public class ToJPGSaver : ISaver
    {
        string Image;
        string DImagePath;
        int Image_cuality;
        private ProgressForm progress;

        public ToJPGSaver(string Image_param, string DImagePath_param, int Image_cuality_param)
        {
            Image = Image_param;
            DImagePath = DImagePath_param + Extension[0];
            Image_cuality = Image_cuality_param;
            progress = Helper.ProgForm;
        }

        public string Description => "Salva una imagen en formato JPG";

        public List<string> Extension => new List<string>()
        {
            ".jpg",
            ".jpeg"
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

                    if (image.HasAlpha)
                    {
                        var img2 = image.Clone();
                        img2.Resize(1, 1);
                        img2.Negate();
                        image.BackgroundColor = img2.GetPixels().GetPixel(0, 0).ToColor();
                    }
                    image.Alpha(AlphaOption.Remove);
                    image.Format = MagickFormat.Jpg;
                    image.Quality = Image_cuality;
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
