
using iTextSharp.text;
using iTextSharp.text.pdf;


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace SmartShopping.RecommendedShoppingUC
{
    class loadPDFrecommendedCMD : ICommand
    {
        RecommendedShoppingUserControlVM VM;

        public loadPDFrecommendedCMD(RecommendedShoppingUserControlVM vm)
        {
            VM = vm;
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
            Document Doc = new Document(PageSize.LETTER);

            //Create our file stream
            using (FileStream fs = new FileStream(@"C:\courses\SmartShopping\SmartShopping\SmartShopping\PdfFiles"+ ".pdf", FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                //Bind PDF writer to document and stream
                PdfWriter writer = PdfWriter.GetInstance(Doc, fs);

                //Open document for writing
                Doc.Open();

                //Add a page
                Doc.NewPage();

                //Full path to the Arial file
                string ARIALUNI_TFF = Path.Combine(@"C:\courses\SmartShopping\SmartShopping\SmartShopping\Fonts\Heebo.ttf");

                //Create a base font object making sure to specify IDENTITY-H
                BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                //Create a specific font object
                iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 12);

                //Use a table so that we can set the text direction
                PdfPTable T = new PdfPTable(1);
                //Hide the table border
                T.DefaultCell.BorderWidth = 0;
                //Set RTL mode
                T.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                //Add our text
                T.AddCell(new Phrase("רשימת הקניות המומלצת עבור היום שביקשת הוא כדלהלן:", f));
                T.AddCell(new Phrase("חביתה", f));
                T.AddCell(new Phrase("פיצה", f));
                T.AddCell(new Phrase("", f));
                T.AddCell(new Phrase("", f));
                T.AddCell(new Phrase("קנייה מהנה :-)", f));

                 //Add a page  
           //PdfPageBase page = doc.Pages.Add();  
  
           ////Create a pdf grid  
           //PdfGrid grid = new PdfGrid();
                //Add table to document
                Doc.Add(T);

                //Close the PDF
                Doc.Close();


                //PdfDocument pdf = new PdfDocument();
                //pdf.Info.Title = "My First PDF";
                //PdfPage pdfPage = pdf.AddPage();
                //XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                //XFont fontBold = new XFont("Verdana", 20, XFontStyle.Bold);
                //XFont font = new XFont("Hebbo", 20, XFontStyle.Regular);

                //if (VM.SelectedDay!="")
                //    graph.DrawString("רשימת קניות עבור יום " + VM.SelectedDay, fontBold, XBrushes.Black, new XRect(0, 50, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);
                //else graph.DrawString("רשימת קניות ", fontBold, XBrushes.Black, new XRect(0, 50, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);

                //if(VM.SourceList.Count!=0)
                //{
                //    int cnt = 1;
                //    int y = 70;
                //    foreach (var item in VM.SourceList)
                //    {
                //        graph.DrawString(item.name + "  ." +cnt.ToString(), font, XBrushes.Black, new XRect(10, y, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);
                //        y += 22;
                //        cnt++;
                //    }
                //}
                //string pdfFilename = "רשימת_קניות.pdf";
                //pdf.Save(pdfFilename);
                //Process.Start(pdfFilename);
                ////var client=Trans
                ///
            }
        }
    }
}
