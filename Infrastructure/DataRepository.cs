using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Windows.Storage;

namespace Infrastructure
{
    public static class DataRepository
    {
        public static string GetInstallPath()
        {
            var installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var path = installFolder.Path;
            return path;
        }

        public static async Task<string> GetPath(string folderName)
        {
            string path = string.Empty;
            try
            {
                var installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                var folder = await installFolder.GetFolderAsync(folderName);      //  Throws exception if not found
                path = folder.Path;
            }
            catch (Exception e)
            {
                path = e.Message;
            }
            return path;
        }

        public static async Task<IList<string>> GetFileNames(string folderName)
        {
            IList<string> fileNames = new List<string>();
            try
            {
                var installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFolder dataFolder = null;
                if (folderName == null)
                    dataFolder = installFolder;
                else
                    dataFolder = await installFolder.GetFolderAsync(folderName);
                IReadOnlyList<StorageFile> dataFiles = await dataFolder.GetFilesAsync();
                foreach (var file in dataFiles)
                {
                    fileNames.Add(file.Name);
                }
            }
            catch (Exception e)
            {
                fileNames.Add(e.Message);
            }
            return fileNames;
        }

    }
}
