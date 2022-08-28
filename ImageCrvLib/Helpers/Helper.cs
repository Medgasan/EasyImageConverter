using ImageCrvLib.Viewers;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageCrvLib
{
    // Format can save

    //--System.Drawing.Imaging.ImageFormat.Bmp
    //--System.Drawing.Imaging.ImageFormat.Icon
    //--System.Drawing.Imaging.ImageFormat.Jpeg
    //--System.Drawing.Imaging.ImageFormat.Png
    //System.Drawing.Imaging.ImageFormat.Emf
    //System.Drawing.Imaging.ImageFormat.Gif
    //System.Drawing.Imaging.ImageFormat.Tiff
    //System.Drawing.Imaging.ImageFormat.Wmf

    public class Helper
    {

        public static ProgressForm ProgForm = new ProgressForm();

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static Color GetDominantColor(Bitmap bmp)
        {
            //Used for tally
            int r = 0;
            int g = 0;
            int b = 0;
            int total = 0;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color clr = bmp.GetPixel(x, y);
                    r += clr.R;
                    g += clr.G;
                    b += clr.B;
                    total++;
                }
            }
            //Calculate average
            r /= total;
            g /= total;
            b /= total;
            return Color.FromArgb(r, g, b);
        }

        public static Bitmap RemoveTansparency(Image image)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Color color = GetDominantColor(bmp);
                Color colorBack = Color.FromArgb((int)(1f - color.GetBrightness()));
                g.Clear(Color.White);
                g.DrawImage(image,
                    new Rectangle(new Point(), image.Size),
                    new Rectangle(new Point(), image.Size),
                    GraphicsUnit.Pixel);
            }
            return bmp;
        }

    }
}
