﻿using ImageCrvLib.Contract;
using ImageCrvLib.Viewers;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ImageCrvLib.Effects
{
    public class MoonLight : ISaver
    {
        public string Description => "Invierte el color de la Imagen";

        public List<string> Extension => new List<string>() { };

        string ImagePath = "";
        string DImagePath;
        private ProgressForm progress;

        public MoonLight(string Image_path, string DImagePath_param)
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
                    progress = Helper.ProgForm;
                    progress.ShowProgress(true);
                    image.Progress += Image_Progress;
                    progress.setText("Aplicando Efecto");
                    image.BlueShift();
                    progress.setText("Salvando Imagen");
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
