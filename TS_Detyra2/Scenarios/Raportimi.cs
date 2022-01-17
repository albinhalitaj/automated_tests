using System;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using NUnit.Framework;

namespace TS_Detyra2.Scenarios
{
    public class Raportimi
    {
        [Test]
        public void Raportimi_I_Klienteve()
        {
            var path = Environment.GetEnvironmentVariable("USERPROFILE");
            var content = readPdfContent($"{path}\\Downloads\\print.pdf");
            
            Assert.IsTrue(content.Contains("Total Klientë: 6"));
        }

        [Test]
        public void Raportimi_I_Huazimeve()
        {
            var path = Environment.GetEnvironmentVariable("USERPROFILE");
            var content = readPdfContent($"{path}\\Downloads\\huazimet.pdf");
            
            Assert.IsTrue(content.Contains("Total Huazime: 5"));
        }

        private static string readPdfContent(string url)
        {
            var sb = new StringBuilder();
            using (var pdfReader = new PdfReader(url))
            {
                for (var i = 1; i <= pdfReader.NumberOfPages; i++)
                {
                    var strategy = new SimpleTextExtractionStrategy();
                    var text = PdfTextExtractor.GetTextFromPage(pdfReader, i, strategy);
                    text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                    sb.Append(text);
                }
            }

            var content = sb.ToString();
            return content;
        }
    }
}