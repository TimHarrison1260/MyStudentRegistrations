using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

using System.IO;
using Windows.Storage;

namespace StudentRegistrationApp.Model
{
    public static class Helpers
    {
        public static async Task<StorageFile> GetFile(string filename)
        {
            StorageFile file = null;

            //  Access variable for the local storage system, where we're going to store the Student records.
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            //  Get the files available from the localFolder
            Windows.Storage.Search.StorageFileQueryResult fileResults = localFolder.CreateFileQuery();

            //  Get the list of files from the query against the localFolder
            var fileList = await fileResults.GetFilesAsync();

            //  Look for our file in the results.
            file = fileList.SingleOrDefault(f => f.Name == filename);

            return file;
        }

        public static async Task<StorageFile> CreateFile(string filename)
        {
            StorageFile file = null;

            //  Access variable for the local storage system, where we're going to store the Student records.
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            //  Create the file in the local folder
            file = await localFolder.CreateFileAsync(filename);

            return file;
        }
    }
}
