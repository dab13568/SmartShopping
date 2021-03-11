using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartShopping.EditProductWindow
{
    class ImagePickerCMD : ICommand
    {
        private EditProductVM VM;

        public ImagePickerCMD(EditProductVM VM)
        {
            this.VM = VM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void Execute(object parameter)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "";

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;

            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                dlg.Filter = String.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }

            dlg.Filter = String.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, "All Files", "*.*");

            dlg.DefaultExt = ".png"; // Default file extension 
            VM.loadImgPicker(dlg);
        }
    }
}
