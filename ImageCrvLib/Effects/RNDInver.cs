using ImageCrvLib.Contract;
using ImageCrvLib.Viewers;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ImageCrvLib.Effects
{
    public class RNDInver : ISaver
    {
        public string Description => "Invierte el color de la Imagen";

        public List<string> Extension => new List<string>() { };

        string ImagePath = "";
        string DImagePath;
        private ProgressForm progress;

        public RNDInver(string Image_path, string DImagePath_param)
        {
            ImagePath = Image_path;
            DImagePath = DImagePath_param + Path.GetExtension(ImagePath);
        }



        public void Convert()
        {
            using (var image = new MagickImage(ImagePath))
            {
                try
                {
                    image.Progress += Image_Progress;

                    progress = Helper.ProgForm;
                    progress.ShowProgress(true);

                    progress.setText("Aplicando efecto en canal r");

                    string formula = "";
                    Random random = new Random();

                    formula = String.Format("abs({0}-u.r)", random.NextDouble()).Replace(",", ".");
                    image.Fx(formula, Channels.Red);

                    progress.setText("Aplicando efecto en canal g");
                    formula = String.Format("abs({0}-u.g)", random.NextDouble()).Replace(",", ".");
                    image.Fx(formula, Channels.Green);

                    progress.setText("Aplicando efecto en canal b");
                    formula = String.Format("abs({0}-u.b)", random.NextDouble()).Replace(",", ".");
                    image.Fx(formula, Channels.Blue);

                    progress.setText("Guardando Imagen");

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
