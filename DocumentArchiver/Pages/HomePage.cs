﻿using System.IO;
using DocumentArchiver.Services;

namespace DocumentArchiver.Pages
{
    public class HomePage : BasePage
    {
        public override void WriteHTML()
        {
            StreamWriter streamWriter = new StreamWriter(fs);

            streamWriter.WriteLine("    <html dir='ltr' xmlns:dt='uuid:C2F41010-65B3-11d1-A29F-00AA00C14882' xmlns:mshelp='http://msdn.microsoft.com/mshelp' xmlns:xlink='http://www.w3.org/1999/xlink'>");
            streamWriter.WriteLine("    <head><meta content='text/html;charset=utf-8' http-equiv='Content-Type'></meta><link href='MSDN.css' rel='stylesheet' type='text/css'></link></head>");
            streamWriter.WriteLine("    <body id='bodyID' bottommargin='0' class='dtBODY' leftmargin='0' rightmargin='0' topmargin='0'>");
            streamWriter.WriteLine("    <div id='bannerrow1'><table id='topTable' width='100%'><tr id='headerTableRow3'><td></td></tr><tr id='headerTableRow1'>");
            streamWriter.WriteLine("    <td align='left'><h2><span id='runningHeaderText'>");
            streamWriter.WriteLine(string.Format("{0} Documentation", DocumentSettings.DocumentName));
            streamWriter.WriteLine("    </span></h2></td></tr><tr id='headerTableRow2'><td align='left'><h2><span id='nsrTitle'>");
            streamWriter.WriteLine("Created with Article Compiler");
            streamWriter.WriteLine("    </span></h2></td></tr><tr id='headerTableRow3'><td></td>	</tr></table></div>	<div id='nstext' valign='bottom'>");
            streamWriter.WriteLine("     <h4>Details</h4>");
            streamWriter.WriteLine(string.Format("<ul><li>Last Modified : {0}", DocumentSettings.ModifiedDate));
            streamWriter.WriteLine(string.Format("</li><li>Number of articles : {0}", DocumentSettings.NumberOfArticles));
            streamWriter.WriteLine("    </li></ul></div></body></html>   ");
            streamWriter.Close();
        }
    }
}