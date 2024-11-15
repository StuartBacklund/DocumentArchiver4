﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentArchiver.Services;

namespace DocumentArchiver.Pages
{
    public class CreateArticleSectionsPage : BasePage
    {
        private List<IGrouping<string, FileInfo>> fileGrouping;
        private string sectionName;

        public void WriteHTML(string _sectionName, List<FileInfo> fileinfoList)
        {
            sectionName = _sectionName;

            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine("		<div class='tablediv'>");
            streamWriter.WriteLine("		<table align='center' cellspacing='0' class='dtTABLE' border='1' width='90%'>");
            streamWriter.WriteLine("			<tr bgcolor='#cccccc'>");
            streamWriter.WriteLine("				<th width='100%'>Name of Article</th>");
            streamWriter.WriteLine("			</tr>");

            foreach (var filename in fileinfoList)
            {
                streamWriter.WriteLine("			<tr valign='top'>");
                streamWriter.WriteLine("				<td nowrap><img class='midvalign' src='Procedure.gif'>&nbsp;<a href='" + GetFileName(DocumentSettings.DocumentName, filename.Name, filename.Name.Substring(0, 1)) + "'><font size='2'>" + filename.Name.Replace(".htm", "") + "</font></a></td>");
                streamWriter.WriteLine("			</tr>");
            }

            streamWriter.WriteLine("		</table>");
            streamWriter.Close();
        }
    }
}