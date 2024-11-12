using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentArchiver.Services;

namespace DocumentArchiver.Pages
{
    public class ContentsPage : BasePage
    {
        private List<IGrouping<string, FileInfo>> filesInSection;

        public void WriteHTML(List<IGrouping<string, FileInfo>> filesInSection)
        {
            this.filesInSection = filesInSection;

            string contentsFile = (DocumentSettings.ProjectName + ".Contents.htm").Replace(" ", "");
            string defaultFile = (DocumentSettings.ProjectName + ".Default.htm").Replace(" ", "");

            StreamWriter s = new StreamWriter(fs);

            s.WriteLine("<html>");
            s.WriteLine("  <head>");
            s.WriteLine("    <META http-equiv='Content-Type' content='text/html; charset=utf-8'>");
            s.WriteLine("    <title>Contents</title>");
            s.WriteLine("    <meta name='GENERATOR' content='Microsoft Visual Studio.NET 7.0'>");
            s.WriteLine("    <meta name='vs_targetSchema' content='http://schemas.microsoft.com/intellisense/ie5'>");
            s.WriteLine("    <link rel='stylesheet' type='text/css' href='tree.css'>");
            s.WriteLine("    <script src='tree.js' language='javascript' type='text/javascript'>");
            s.WriteLine("    </script>");
            s.WriteLine("  </head>");
            s.WriteLine("  <body id='docBody' style='background-color: #6699CC; color: White; margin: 0px 0px 0px 0px;' onload='resizeTree()' onresize='resizeTree()' onselectstart='return false;'>");
            s.WriteLine("    <div style='font-family: verdana; font-size: 8pt; cursor: pointer; margin: 6 4 8 2; text-align: right' onmouseover='this.style.textDecoration=underline;' onmouseout='this.style.textDecoration=none;' onclick='syncTree(window.parent.frames[1].document.URL)'>sync toc</div>");
            s.WriteLine("    <div id='tree' style='top: 35px; left: 0px;' class='treeDiv'>");
            s.WriteLine("      <div id='treeRoot' onselectstart='return false' ondragstart='return false'>");
            s.WriteLine("        <div class='treeNode'>");
            s.WriteLine("          <img src='treenodeplus.gif' class='treeLinkImage' onclick='expandCollapse(this.parentNode)'>");
            s.WriteLine("          <a href='" + defaultFile + "' target='main' class='treeUnselected' onclick='clickAnchor(this)'>" + DocumentSettings.ProjectName + "</a>");
            s.WriteLine("          <div class='treeSubnodesHidden'>");

            var sectionList = filesInSection.Select(x => x.Key.Distinct()).ToList();
            // Menu items
            foreach (var item in sectionList.AsEnumerable())
            {
                string prefix = item.FirstOrDefault().ToString();

                s.WriteLine("            <div class='treeNode'>");
                s.WriteLine("              <img src='treenodeplus.gif' class='treeLinkImage' onclick='expandCollapse(this.parentNode)'>");
                s.WriteLine("              <a href='" + DocumentSettings.ProjectName + "." + prefix + ".htm' target='main' class='treeUnselected' onclick='clickAnchor(this)'>" + prefix + "</a>");
                s.WriteLine("                <div class='treeSubnodesHidden'>");

                foreach (IGrouping<string, FileInfo> grouping in filesInSection.Where(x => x.Key == prefix))
                {
                    foreach (var filename in grouping)
                    {
                        s.WriteLine("                <div class='treeNode'>");
                        s.WriteLine("                  <img src='treenodedot.gif' class='treeNoLinkImage'>");
                        s.WriteLine("                  <a href='" + filename.ToString() + "' target='main' class='treeUnselected' onclick='clickAnchor(this)'>" + filename.Name + "</a>");
                        s.WriteLine("                </div>");
                    }
                }

                s.WriteLine("              </div>");
                s.WriteLine("            </div>");
            }

            s.WriteLine("          </div>");
            s.WriteLine("          </div>");
            s.WriteLine("        </div>");
            s.WriteLine("      </div>");
            s.WriteLine("    </div></body></html>");
            s.Close();
        }
    }
}