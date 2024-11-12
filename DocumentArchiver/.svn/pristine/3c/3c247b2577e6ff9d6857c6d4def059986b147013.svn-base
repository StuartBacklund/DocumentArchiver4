using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace DocumentArchiver.Services
{
    public class FileSystemExplorerService
    {
        public static IList<FileInfo> GetChildFiles(string directory)
        {
            try
            {
                return (from x in Directory.GetFiles(directory)
                        select new FileInfo(x)).ToList();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
            return new List<FileInfo>();
        }

        public static IList<DirectoryInfo> GetChildDirectories(string directory)
        {
            try
            {
                return (from x in Directory.GetDirectories(directory)
                        select new DirectoryInfo(x)).ToList();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
            return new List<DirectoryInfo>();
        }

        public static IList<DriveInfo> GetRootDirectories()
        {
            return (from x in DriveInfo.GetDrives() select x).ToList();
        }
    }
}