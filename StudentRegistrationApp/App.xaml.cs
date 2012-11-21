using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.UI.Popups;        //  for the UICommand, required to manipulate the SettingsPane.
using Windows.UI.ApplicationSettings;   //  For settings commands.

using Windows.Storage;              // for the local storage stuff.

using Infrastructure;


// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace StudentRegistrationApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private Model.IRepository Repository = new Model.StudentRepository();
        private Model.ISearchRepository searchRepository = new Model.SearchRepository();

        ////  The container for the custom content
        //private Popup settingsPopup;
        ////  desired width for the settingsUI. UI guidelines specify this
        ////  shuld be 346 or 646 depending on needs
        //private double settingsWidth = 646;
        ////  used to determine the correct height to ensure our custon UI fills the screen
        //private Rect windowBounds;


        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            //  Set up the datastore via the Repository for the moment.
            //  Here we would ultimately set up the 
            //  DI Container so that we can inject the instances.
            Model.IRepository Repository = new Model.StudentRepository();
            searchRepository = new Model.SearchRepository();

            CheckFolders();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            //  Call the loadstudents method.
            BusinessLayer.IStudentBLL BLL = new BusinessLayer.StudentBLL((App.Current as App).GetRepository());
            BLL.LoadStudents();
            BusinessLayer.ISearchBLL searchBll = new BusinessLayer.SearchBLL((App.Current as App).GetSearchRepository());
            searchBll.LoadSearches();

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity

            //  Sae the student details before suspending.
            BusinessLayer.IStudentBLL BLL = new BusinessLayer.StudentBLL((App.Current as App).GetRepository());
            BLL.PersistStudents();
            //RemoveFile();
            //this.PersistStudents();

            //  Save the searches
            BusinessLayer.ISearchBLL searchBll = new BusinessLayer.SearchBLL((App.Current as App).GetSearchRepository());
            searchBll.SaveSearches();

            deferral.Complete();
        }

        /// <summary>
        /// Leave this here, it would NEVER be used within an application like this.
        /// </summary>
        private async void RemoveFile()
        {
            //  Get the file
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile studentFile = await localFolder.GetFileAsync("student.txt");
            await studentFile.DeleteAsync();
        }



        //public async void PersistStudents()
        //{
        //    try
        //    {
        //        //  Access variable for the local storage system, where we're going to store the Student records.
        //        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        //        //  Get a reference to the file (assuming it's there).
        //        StorageFile studentFile = await localFolder.GetFileAsync("student.txt");

        //        //  Set up a StreamWriter to write stuff to it.
        //        using (Stream outputStream = await studentFile.OpenStreamForWriteAsync())
        //        {
        //            using (StreamWriter saveStream = new StreamWriter(outputStream))
        //            {
        //                //  Save each student in the array as a line in the file.
        //                saveStream.WriteLine("Hi this should be the first line in the file");
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        string msg = e.Message;
        //    }
        //}





        /****************************************************************************************
         * My stuff added to act as IoC container, a singleton for each injected method / object.
         * **************************************************************************************/

        /// <summary>
        /// Returns the instance of the Repository used by the DAL.
        /// </summary>
        /// <returns>Concrete instance of the repository (StudentRepository)</returns>
        public Model.IRepository GetRepository()
        {
            return Repository;
        }

        public Model.ISearchRepository GetSearchRepository()
        {
            return searchRepository;
        }



        private async void CheckFolders()
        {
            /*
             * *    The dataRepository class is in a referenced project
             * *    and the values returned apply only to this project
             * *    as the owning, or executing, project.
             * *
             * *
             */

            string installFolder = DataRepository.GetInstallPath();
            IList<string> installFolderFileNames = await DataRepository.GetFileNames(null);

            string assetsFolder = await DataRepository.GetPath("Assets");                               //  OK
            string dataFolderInINfrastructureProject = await DataRepository.GetPath("DataFolder");      //  Throws exception
            IList<string> assetFolderFileNames = await DataRepository.GetFileNames("Assets");

            string helpersFolder = await DataRepository.GetPath("Helpers");                             //  Throws exception
            IList<string> helperFolderFileNames = await DataRepository.GetFileNames("Helpers");

            string commonFolder = await DataRepository.GetPath("Common");                               //  OK
            IList<string> commonFolderFileNames = await DataRepository.GetFileNames("Common");

            string modelFolder = await DataRepository.GetPath("Model");                                 //  Throws exception
            IList<string> modelFolderFileNames = await DataRepository.GetFileNames("Model");

            string settingsFolder = await DataRepository.GetPath("Settings");                           //  OK
            IList<string> settingsFolderFileNames = await DataRepository.GetFileNames("Settings");

        }




    }
}
