using System;
using System.Collections.Generic;
using System.Linq;

using System.IO;
using Windows.Storage;
using System.Xml.Serialization;

namespace StudentRegistrationApp.Model
{
    public class SearchRepository : ISearchRepository
    {
        //  hold the database of Searches
        private IList<SearchCriteria> db = null;

        private StorageFile searchesFile = null;
        private const string searchesFileName = "searches.xml";

        public IList<SearchCriteria> GetSearches()
        {
            //  If nothing is loaded, then pull the search 
            //  criteria in from the file
            if (db == null) LoadDB();
            return db;
        }


        public void AddSearch(SearchCriteria search)
        {
            if (search != null)
                db.Add(search);
        }

        public void SaveSearches()
        {
            //  Call the SaveDB to overwrite the file completely
            SaveDB();
        }


        public async void LoadDB()
        {
            try
            {
                StudentRegistrationApp.Model.Helpers helper = new Helpers();
                searchesFile = await helper.GetFile(searchesFileName);
//                searchesFile = await StudentRegistrationApp.Model.Helpers.GetFile(searchesFileName);

                if (searchesFile == null)
                {
                    //  Create the file for students as the file doesn't exist
                    searchesFile = await helper.CreateFile(searchesFileName);
//                    searchesFile = await StudentRegistrationApp.Model.Helpers.CreateFile(searchesFileName);
                    //  Initialise the DB
                    db = new List<SearchCriteria>();
                }
                else
                {
                    // file exists, so load the data from the file.

                    //  initialise the db.
                    db = new List<SearchCriteria>();


                    //  Create a StreamReader to read the contents of the file.
                    using (StreamReader readStream = new StreamReader(await searchesFile.OpenStreamForReadAsync()))
                    {
                        //  Stream cannot be empty, causes exception if we deserialise empty stream.
                        if (!readStream.EndOfStream)            
                        {
                            //  Set up the types for deserialising
                            Type[] extraTypes = new Type[1];
                            extraTypes[0] = typeof(SearchCriteria);
                            XmlSerializer serializer = new XmlSerializer(typeof(List<SearchCriteria>), extraTypes);
                            db = (List<SearchCriteria>)serializer.Deserialize(readStream);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string mess = e.Message;
            }
        }


        private async void SaveDB()
        {
            try
            {
                //  only save the searches if some are defined.
                if ((db != null) && (db.Count != 0))
                {
                    Stream fileStream = await searchesFile.OpenStreamForWriteAsync();
                    //  Set up a StreamWriter to write stuff to it.
                    using (StreamWriter saveStream = new StreamWriter(fileStream))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<SearchCriteria>));
                        serializer.Serialize(saveStream, db);
                    }
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
        }

    }
} 
