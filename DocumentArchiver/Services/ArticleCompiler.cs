using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using DocumentArchiver.Pages;

namespace DocumentArchiver.Services
{
    public class ArticleCompiler
    {
        private StreamWriter sw;
        private XmlTextWriter toc;
        private DocumentSettings documentSettings;
        private DirectoryInfo workingDirectory { get; set; }
        public string ReturnMessage { get; set; }

        private List<IGrouping<string, FileInfo>> filesGrouping;
        private List<FileInfo> files;

        private List<IEnumerable<char>> sectionList;

        public List<IEnumerable<char>> SectionList
        {
            get { return sectionList; }
            set { sectionList = value; }
        }

        public List<IGrouping<string, FileInfo>> FilesGrouping
        {
            get { return filesGrouping; }
            set { filesGrouping = value; }
        }

        public List<FileInfo> Files
        {
            get { return files; }
            set { files = value; }
        }

        private string DefaultTopic
        {
            get
            {
                return string.Concat(DocumentSettings.DocumentName, ".Default.htm").Replace(" ", "");
            }
        }

        public bool IncludeFavorites { get; set; }

        private string ProjectFilename()
        {
            return DocumentSettings.ProjectName + ".hhp";
        }

        private string ContentsFilename()
        {
            return DocumentSettings.ProjectName + ".hhc";
        }

        private string IndexFilename()
        {
            return DocumentSettings.ProjectName + ".hhk";
        }

        private string LogFilename()
        {
            return DocumentSettings.ProjectName + ".log";
        }

        private string CompiledHtmlFilename()
        {
            return DocumentSettings.ProjectName + ".chm";
        }

        private string PathToProjectFile()
        {
            return Path.Combine(DocumentSettings.InputFolder, ProjectFilename());
        }

        private string PathToContentsFile()
        {
            return Path.Combine(DocumentSettings.InputFolder, ContentsFilename());
        }

        private string PathToIndexFile()
        {
            return Path.Combine(DocumentSettings.InputFolder, IndexFilename());
        }

        private string PathToLogFile()
        {
            return Path.Combine(DocumentSettings.InputFolder, LogFilename());
        }

        /// <summary>Gets the path the the CHM file.</summary>
        /// <returns>The path to the CHM file.</returns>
        public string PathToCompiledHtmlFile()
        {
            return Path.Combine(DocumentSettings.InputFolder, CompiledHtmlFilename());
        }

        public string ProjectName
        {
            get
            {
                return DocumentSettings.DocumentName.Replace(" ", "");
            }
        }

        public string DirectoryName
        {
            get
            {
                return DocumentSettings.InputFolder;
            }
        }

        public string HtmlHelpCompiler
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Html Help Workshop\hhc.exe"; }
        }

        public DocumentSettings DocumentSettings
        {
            get
            {
                return documentSettings;
            }
            set
            {
                documentSettings = value;
            }
        }

        public ArticleCompiler(DocumentSettings documentSetting)
        {
            this.DocumentSettings = documentSetting;
            sw = new StreamWriter(File.Open(PathToProjectFile(), FileMode.Create));
            toc = new XmlTextWriter(PathToContentsFile(), null);
        }

        public void Setup()
        {
            var path = new DirectoryInfo(DocumentSettings.InputFolder);
            if (!path.Exists)
            {
                return;
            }
            this.FilesGrouping = path.GetFiles().Where(x => x.Extension == ".htm").OrderBy(n => n.Name).GroupBy(x => x.Name.Substring(0, 1)).ToList();
            this.SectionList = this.FilesGrouping.Select(x => x.Key.Distinct()).ToList();

            this.Files = new List<FileInfo>();
            this.OpenProjectFile();
            this.OpenContentsFile();
            this.OpenBookInContents();

            this.CreateHomePage();
            this.CreateIndexPage();
            this.CreateContentsPage();

            // Menu items
            foreach (var item in this.SectionList.AsEnumerable())
            {
                string prefix = item.FirstOrDefault().ToString();

                var filegroup = new List<FileInfo>();
                foreach (IGrouping<string, FileInfo> grouping in this.FilesGrouping.Where(x => x.Key == prefix))
                {
                    foreach (var filename in grouping)
                    {
                        filegroup.Add(filename);
                        Files.Add(filename);
                    }
                    CreateHTMPage(prefix, filegroup);
                    CreateHTMPageDetails(prefix, filegroup);
                }
            }

            this.CloseProjectFile();
            this.CloseBookInContents();
            this.CloseContentsFile();
            this.WriteEmptyIndexFile();
        }

        public bool ZipFiles()
        {
            bool success = true;
            try
            {
                var path = new DirectoryInfo(DocumentSettings.InputFolder);
                var htmOutputFiles = path.GetFiles().Where(x => x.Extension == ".htm" &&
                                     x.Name.ToUpper().Contains(DocumentSettings.DocumentName.Replace(" ", "").ToUpper()))
                                     .OrderBy(n => n.Name).ToList();

                var zipFile = $"{DocumentSettings.ProjectName}.zip";
                if (File.Exists(Path.Combine(DocumentSettings.InputFolder, zipFile)))
                {
                    File.Delete(Path.Combine(DocumentSettings.InputFolder, zipFile));
                }
                using (ZipArchive zip = ZipFile.Open($"{DocumentSettings.ProjectName}.zip", ZipArchiveMode.Create))
                {
                    foreach (var item in htmOutputFiles)
                    {
                        zip.CreateEntryFromFile($"{DocumentSettings.InputFolder}\\{item.Name}", item.Name);
                    }
                }

                foreach (FileInfo oFile in htmOutputFiles)
                {
                    if (File.Exists(oFile.FullName))
                    {
                        File.Delete(oFile.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }

        private string WorkingDirectory()
        {
            return Path.GetDirectoryName(DocumentSettings.InputFolder);
        }

        public void OpenProjectFile()
        {
            string options;

            if (IncludeFavorites)
            {
                options = @"0x63520,220,0x383e,[86,51,872,558],,,,,,,0";
            }
            else
            {
                options = @"0x62520,220,0x383e,[86,51,872,558],,,,,,,0";
            }

            sw.WriteLine("Auto Index=Yes");
            sw.WriteLine("Title=" + DocumentSettings.ProjectName);
            sw.WriteLine("Compatibility=1.1 or later");
            sw.WriteLine("Compiled file=" + CompiledHtmlFilename());
            sw.WriteLine("Contents file=" + ContentsFilename());
            sw.WriteLine("Default Window=MsdnHelp");
            sw.WriteLine("Default topic=" + Path.GetFileName(DefaultTopic));
            sw.WriteLine("Display compile progress=No");
            sw.WriteLine("Error log file=" + LogFilename());
            sw.WriteLine("Full-text search=Yes");
            sw.WriteLine("Index file=" + IndexFilename());
            sw.WriteLine("Language=0x409 English (United States)");
            sw.WriteLine("");
            sw.WriteLine("[WINDOWS]");
            string outString1 = @"MsdnHelp=".PadRight(0) + '"';
            string outString2 = string.Concat(outString1, DocumentSettings.ProjectName, " Help".PadRight(0) + '"', ",", '"', ContentsFilename(), '"', ",", '"', IndexFilename(), '"', ",,,,,,,", options);
            sw.WriteLine(outString2);
            sw.WriteLine("");
            sw.WriteLine("[FILES]");
        }

        public void AddFileToProject(string filename)
        {
            sw.WriteLine(filename);
        }

        public void CloseProjectFile()
        {
            sw.WriteLine("");
            sw.WriteLine("[INFOTYPES]");
            sw.Close();
        }

        public void OpenContentsFile()
        {
            toc.WriteComment("This document contains Table of Contents information for the HtmlHelp compiler.");
            toc.WriteStartElement("UL");
        }

        public void OpenBookInContents()
        {
            toc.WriteStartElement("UL");
        }

        public void AddFileToContents(string headingName, string htmlFilename, int? ImageNumber = null)
        {
            toc.WriteStartElement("LI");
            toc.WriteStartElement("OBJECT");
            toc.WriteAttributeString("type", "text/sitemap");
            toc.WriteStartElement("param");
            toc.WriteAttributeString("name", "Name");
            toc.WriteAttributeString("value", headingName.Replace("$", "."));
            toc.WriteEndElement();
            toc.WriteStartElement("param");
            toc.WriteAttributeString("name", "Local");
            toc.WriteAttributeString("value", Path.GetFileName(htmlFilename));
            toc.WriteEndElement();

            if (ImageNumber.HasValue)
            {
                toc.WriteStartElement("param");
                toc.WriteAttributeString("name", "ImageNumber");
                toc.WriteAttributeString("value", ImageNumber.Value.ToString());
                toc.WriteEndElement();
            }

            toc.WriteEndElement();
            toc.WriteEndElement();
        }

        public void CloseBookInContents()
        {
            toc.WriteEndElement();
        }

        public void CloseContentsFile()
        {
            toc.WriteEndElement();
            toc.Close();
        }

        public void WriteEmptyIndexFile()
        {
            XmlTextWriter i = new XmlTextWriter(PathToIndexFile(), null);

            i.WriteStartElement("HTML");
            i.WriteStartElement("BODY");
            i.WriteComment("");
            i.WriteEndElement();
            i.WriteEndElement();
            i.Close();
        }

        public string CompileProject()
        {
            string chmPath = PathToCompiledHtmlFile();
            string output = "";
            if (File.Exists(chmPath))
            {
                // File.Delete(chmPath);
            }
            Process helpCompileProcess = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = HtmlHelpCompiler;
            processStartInfo.Arguments = "\"" + Path.GetFullPath(PathToProjectFile()) + "\"";
            processStartInfo.ErrorDialog = false;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.WorkingDirectory = Path.GetDirectoryName(chmPath);
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.UseShellExecute = false;

            helpCompileProcess.StartInfo = processStartInfo;

            try
            {
                helpCompileProcess.Start();
                helpCompileProcess.WaitForExit();
            }
            catch (Exception e)
            {
                output += string.Format("An error occurred. See log file for details");
            }
            finally
            {
                helpCompileProcess.Close();
            }
            return (String.IsNullOrEmpty(output) ? "Process successfull" : output);
        }

        public bool CopyFile(string source, string target)
        {
            try
            {
                if (File.Exists(target))
                {
                    // File.Delete(target);
                }
                File.Copy(source, target, true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void CreateHTMPage(string sectionName, List<FileInfo> filegroup)
        {
            CreateArticleSectionsPage hp = new CreateArticleSectionsPage();
            hp.DocumentSettings = DocumentSettings;

            string fileName = (DocumentSettings.DocumentName + "." + sectionName + ".htm").Replace(" ", "");
            string fullPath = BuildPageFilePath(fileName);
            hp.FileName = fileName;
            hp.OpenFile();
            hp.WriteHTML(sectionName, filegroup);
            hp.CloseFile();
            this.AddFileToProject(fileName);
            this.AddFileToContents(sectionName, fullPath);
        }

        public void CreateHTMPageDetails(string sectionName, List<FileInfo> filegroup)
        {
            foreach (var fileItem in filegroup)
            {
                CreateArticlePageDetails hp = new CreateArticlePageDetails();
                hp.DocumentSettings = DocumentSettings;

                string fileName = (DocumentSettings.DocumentName + "." + sectionName + "." + fileItem.Name).Replace(" ", "");
                string fullPath = BuildPageFilePath(fileName);
                hp.FileName = fileName;
                hp.OpenFile();
                hp.WriteHTML(fileItem.Name, fullPath);
                hp.CloseFile();
                this.AddFileToProject(fileName);
                this.OpenBookInContents();
                this.AddFileToContents(fileItem.Name.Replace(".htm", ""), fileName, 11);
                this.CloseBookInContents();
            }
        }

        public void CreatePageSectionDetails(string character, List<FileInfo> filegroup)
        {
            CreateArticleSectionsPage hp = new CreateArticleSectionsPage();
            hp.DocumentSettings = DocumentSettings;

            hp.FilesInSection = this.FilesGrouping.Where(x => x.Key == character).ToList();
            foreach (IGrouping<string, FileInfo> grouping in hp.FilesInSection)
            {
                foreach (var fileinfo in grouping)
                {
                    string fileName = string.Concat(DocumentSettings.DocumentName, ".", character, ".", fileinfo.Name).Replace(" ", "");
                    string fullPath = BuildPageFilePath(fileName);

                    this.AddFileToProject(fileName);
                    this.AddFileToContents(character.ToUpper(), fullPath);
                }

                hp.OpenFile();
                hp.WriteHTML(character, grouping.ToList());
                hp.CloseFile();
            }
        }

        public void CreateContentsPage()
        {
            ContentsPage hp = new ContentsPage();
            hp.DocumentSettings = DocumentSettings;
            hp.FilesInSection = this.FilesGrouping;
            string fileName = BuildPageFileName(".Contents.htm");
            string fullPath = BuildPageFilePath(fileName);

            hp.FileName = fileName;

            hp.OpenFile();
            hp.WriteHTML(this.FilesGrouping);
            hp.CloseFile();
        }

        public void CreateIndexPage()
        {
            IndexPage hp = new IndexPage();
            hp.DocumentSettings = DocumentSettings;
            string fileName = BuildPageFileName(".Index.htm");
            string fullPath = BuildPageFilePath(fileName);
            hp.FileName = fileName;

            hp.OpenFile();
            hp.WriteHTML();
            hp.CloseFile();
        }

        public void CreateHomePage()
        {
            HomePage hp = new HomePage();
            hp.DocumentSettings = DocumentSettings;
            string fileName = BuildPageFileName(".Default.htm");
            string fullPath = BuildPageFilePath(fileName);
            hp.FileName = fileName;

            hp.OpenFile();

            hp.WriteHTML();
            hp.CloseFile();
            this.AddFileToProject(fileName);
            this.AddFileToContents(DocumentSettings.DocumentName, fullPath);
        }

        private string BuildPageFileName(string pageName)
        {
            return string.Concat(DocumentSettings.DocumentName, pageName).Replace(" ", "");
        }

        private string BuildPageFilePath(string fileName)
        {
            return Path.Combine(DocumentSettings.OutputFolder, fileName);
        }
    }
}