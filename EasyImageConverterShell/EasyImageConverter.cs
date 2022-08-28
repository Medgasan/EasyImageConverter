using ImageCrvLib.Contract;
using ImageCrvLib.Conversors;
using ImageCrvLib.Effects;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EasyImageConverterShell
{
    [ComVisible(true)]
    [Guid("6A6640E1-E52A-4DA2-AA3D-BF2E1711A192")]
    [COMServerAssociation(AssociationType.AllFiles)]
    public class EasyImageConverter : SharpContextMenu
    {
        private List<string> sipath;
        private readonly List<string> ext_can_read =
            new List<string> { ".bmp", ".gif", ".jpg", ".jpeg", ".png", ".tif", ".tiff", ".heic", ".svg", ".eps",".webp" };
        private readonly List<string> ext_can_write =
            new List<string> { ".bmp", ".gif", ".jpg", ".jpeg", ".png", ".tif", ".tiff", ".svg", ".eps" };
        private string ImageName;
        private string ImageFile;
        private string ImagePath;
        private string ImageDestinyPath;
        private string ImageDestinyPathEffect;
        private string ImageType;
        private string ImageFullPath;

        protected override bool CanShowMenu()
        {
            sipath = (List<string>)SelectedItemPaths;
            ImageFullPath = sipath[0].ToLower();
            ImageFile = Path.GetFileName(ImageFullPath);
            ImageType = Path.GetExtension(ImageFile);
            ImageName = Path.GetFileName(ImageFile).Replace(ImageType, "");
            ImagePath = Path.GetDirectoryName(ImageFullPath);
            ImageDestinyPath = ImagePath + "\\" + ImageName + "_conv";
            ImageDestinyPathEffect = ImagePath + "\\" + ImageName + "_f";
            return (sipath.Count == 1 && ext_can_read.Contains(ImageType));
        }

        protected override ContextMenuStrip CreateMenu()
        {
            //  Create the menu strip
            var menu = new ContextMenuStrip();

            //  Create a items

            ToolStripMenuItem itemMain = GetSubmenu("EasyImage ", Resource1.icon);

            ToolStripMenuItem itemConvertTo = GetSubmenu("Convert Image To ", Resource1.icon);
            ToolStripMenuItem itemFXTo = GetSubmenu("Apply Effect ", Resource1.icon);

            // Create subItems

            // Converters
            ToolStripMenuItem subItemToBMP = GetSubmenu(ImageName + "_c.bmp", Resource1.bmp);
            subItemToBMP.Click += (sender, args) => ExecuteCmd(new ToBMPSaver(ImageFullPath, ImageDestinyPath));

            ToolStripMenuItem subItemToICO = GetSubmenu(ImageName + "_c.ico", Resource1.ico);
            subItemToICO.Click += (sender, args) => ExecuteCmd(new ToICOSaver(ImageFullPath, ImageDestinyPath));

            ToolStripMenuItem subItemToJPG = GetSubmenu(ImageName + "_c.jpg", Resource1.jpg);
            subItemToJPG.Click += (sender, args) => ExecuteCmd(new ToJPGSaver(ImageFullPath, ImageDestinyPath, 75));

            ToolStripMenuItem subItemToPNG = GetSubmenu(ImageName + "_c.png", Resource1.png);
            subItemToPNG.Click += (sender, args) => ExecuteCmd(new ToPNGSaver(ImageFullPath, ImageDestinyPath));

            // no soportado ----
            //ToolStripMenuItem subItemToHEIC = GetSubmenu(ImageName + "_c.heic", Resource1.png);
            //subItemToHEIC.Click += (sender, args) => ExecuteCmd(new ToHEICSaver(ImageFullPath, ImageDestinyPath));

            // Effects
            //TODO: No se puede guardar los efectos en el formato original. Ofrecer otro formato o indicar que hay que convertir antes la imagen
            ToolStripMenuItem subItemInvert = GetSubmenu("Invert Color: " + ImageName + "_f.png", Resource1.icon);
            subItemInvert.Click += (sender, args) => ExecuteCmd(new InvertColor(ImageFullPath, ImageDestinyPathEffect));

            ToolStripMenuItem subItemRNDInvert = GetSubmenu("Rand Invert Color: " + ImageName + "_f.png", Resource1.icon);
            subItemRNDInvert.Click += (sender, args) => ExecuteCmd(new RNDInver(ImageFullPath, ImageDestinyPathEffect));

            ToolStripMenuItem subItemMoon = GetSubmenu("Moonlight : " + ImageName + "_f.png", Resource1.icon);
            subItemMoon.Click += (sender, args) => ExecuteCmd(new MoonLight(ImageFullPath, ImageDestinyPathEffect));

            ToolStripMenuItem subAutoLevels = GetSubmenu("AutoCorrection : " + ImageName + "_f.png", Resource1.icon);
            subAutoLevels.Click += (sender, args) => ExecuteCmd(new AutoLevels(ImageFullPath, ImageDestinyPathEffect));


            // Add subItems to principal Item
            itemConvertTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                subItemToBMP,
                subItemToICO,
                subItemToJPG,
                subItemToPNG,
                //subItemToHEIC
            });

            if (ext_can_write.Contains(ImageType))
            {
                itemFXTo.DropDownItems.AddRange(new ToolStripItem[] {
                    subItemInvert,
                    subItemRNDInvert,
                    subItemMoon,
                    subAutoLevels,
                });
            }

            itemMain.DropDownItems.AddRange(new ToolStripItem[]
            {
                itemConvertTo,
                itemFXTo,
            });


            // Add Item to Menu
            menu.Items.Add(itemMain);

            return menu;
        }

        private void ExecuteCmd(ISaver Command)
        {
            Command.Convert();
        }

        private ToolStripMenuItem GetSubmenu(string text, Image image)
        {

            ToolStripMenuItem subItem = new ToolStripMenuItem
            {
                Text = text,
                Image = image
            };

            return subItem;

        }



    }
}
