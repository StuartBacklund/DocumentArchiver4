using System.IO;
using System.Text;
using DocumentArchiver.Services;

namespace DocumentArchiver.Pages
{
    public class CreateArticlePageDetails : BasePage
    {
        public void WriteHTML(string oName, string fullPath)
        {
            Encoding cp1252 = Encoding.GetEncoding(1252);
            StreamWriter streamWriter = new StreamWriter(fs, cp1252);
            string fileToRead = Path.Combine(DocumentSettings.InputFolder, oName);
            StreamReader sr = new StreamReader(fileToRead, cp1252);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                streamWriter.WriteLine(line);
            }
            sr.Close();
            streamWriter.Close();
        }
    }
}