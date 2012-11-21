using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;

using StudentRegistrationApp.BusinessEntities;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace StudentRegistrationApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : StudentRegistrationApp.Common.LayoutAwarePage
    {
        //  An instance of a Student for the form to use and to pass to the display page.
        BusinessEntities.Student student;
        long currentStudentId = 0;




        //  The container for the custom content
        private Popup settingsPopup;
        //  desired width for the settingsUI. UI guidelines specify this
        //  shuld be 346 or 646 depending on needs
        private double settingsWidth = 646;
        //  used to determine the correct height to ensure our custon UI fills the screen
        private Rect windowBounds;



        public MainPage()
        {
            this.InitializeComponent();
            windowBounds = Window.Current.Bounds;
            SettingsPane.GetForCurrentView().CommandsRequested += onCommandsRequested;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            //  Load the combo box with the YearOfStudy enumeration, Names not Values.
            //foreach (var i in Enum.GetValues(typeof(Model.YearOfStudyEnum)).Cast<Model.YearOfStudyEnum>())
            //{
            //    this.cbYearOfStudyInput.Items.Add(i);
            //}

            this.cbYearOfStudyInput.ItemsSource = Enum.GetValues(typeof( Model.YearOfStudyEnum)).Cast<Model.YearOfStudyEnum>();
            this.cbYearOfStudyInput.SelectedItem = Model.YearOfStudyEnum.First;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //  Instantiate the business layer and inject the repository.
            BusinessLayer.IStudentBLL BLL = new BusinessLayer.StudentBLL((App.Current as App).GetRepository());
            //  Get the student Id passed as the parameter.
            long parameterId = 0;
            if (e.Parameter != null && e.Parameter != string.Empty)
            {
                parameterId = (long)e.Parameter;
                //  Get the student record
                student = BLL.GetStudentById(parameterId);
            }
            else
            {
                student = new BusinessEntities.Student();
            }

            if (student != null)
            {
                //  Set the form values to display the student record.
                SetFormValues();

                //  Set the button descriptions to Save and Cancel, make the Cancel button visible.
                this.RegisterStudent.Content = "Save";
            }
        }


        /// <summary>
        /// Handles the displayRegisteredStudents button Click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayRegisteredStudents_Click(object sender, RoutedEventArgs e)
        {
            //  Code to navigate to the Display Registered Students "StudentDisplayPage".
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(StudentDisplayPage), student);
            }
        }

        /// <summary>
        /// Handles the Register button Click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            //  Place code here to register the Student
            //  Instantiate the Business Layer to process the input.
            BusinessLayer.IStudentBLL BLL = new BusinessLayer.StudentBLL((App.Current as App).GetRepository());
            string msg = string.Empty;

            try
            {
                GetFormValues();

                //  If the key field in Student is 0, we're adding a new student, otherwise we're editing a student
                if (student.Id == 0)
                {
                    //  Add the student record to the datastore (Student Repository)
                    BLL.AddStudent(student);
                    msg = string.Format("{0} {1} has been added as a student", student.Firstname, student.Surname);
                }
                else
                {
                    //  Update the student.
                    BLL.UpdateStudent(student);
                    msg = string.Format("{0} {1} has been Updated.", student.Firstname, student.Surname);
                }

                //  Reset the panel to accept a new Student entry
                this.ResetForm();

                //  Set confirmation message.
                this.lblConfirmMsg.Text = msg;
            }
            catch (RequiredDetailsNotEnteredException eNoData)
            {
                this.lblErrorMessage.Text = eNoData.Message;
            }
            catch (DateOfBirthException eDob)
            {
                this.lblErrorMessage.Text = eDob.Message;
            }
        }


        /// <summary>
        /// Initialises the Register Student form.
        /// </summary>
        /// <remarks>
        /// Move this to a helper class so that the code is separate.
        /// Referencing the items on the form is not obvious to me
        /// at the moment, MUST investigate this further.
        /// </remarks>
        private void ResetForm() 
        {
            student = new Student();
            this.StudentEntry.DataContext = student;

            //this.txtFirstNameInput.Text = string.Empty;
            //this.txtSurnameInput.Text = string.Empty;
            //this.txtDoBInput.Text = string.Empty;
            //this.txtCourseTitleInput.Text = string.Empty;
            //this.txtAddressInput.Text = string.Empty;
            //this.txtTownInput.Text = string.Empty;
            //this.txtCountyInput.Text = string.Empty;
            //this.txtPostCodeInput.Text = string.Empty;

            //this.cbYearOfStudyInput.SelectedIndex = 0;
            //this.txtPhoneHomeInput.Text = string.Empty;
            //this.txtPhoneMobileInput.Text = string.Empty;
            //this.txtEmailHomeInput.Text = string.Empty;
            //this.txtEmailStudentInput.Text = string.Empty;

            this.currentStudentId = 0;
        }

        private void SetFormValues()
        {
            //  Bind the student record to the form
            this.StudentEntry.DataContext = student;

            //this.txtFirstNameInput.Text = student.Firstname;
            //this.txtSurnameInput.Text = student.Surname;
            //this.txtDoBInput.Text = student.DOB.ToString();
            //this.txtCourseTitleInput.Text = student.Course;
            //this.txtAddressInput.Text = student.Address.Address1;
            //this.txtTownInput.Text = student.Address.Town;
            //this.txtCountyInput.Text = student.Address.County;
            //this.txtPostCodeInput.Text = student.Address.PostCode;

            //this.cbYearOfStudyInput.SelectedIndex = ((int)student.YearOfStudy - 1);
            //this.txtPhoneHomeInput.Text = student.Contact.HomePhone;
            //this.txtPhoneMobileInput.Text = student.Contact.MobilePhone;
            //this.txtEmailHomeInput.Text = student.Contact.HomeEmail;
            //this.txtEmailStudentInput.Text = student.Contact.StudentEmail;

            this.currentStudentId = student.Id;
        }


        private void GetFormValues()
        {
            //  create a new instance of a Student record if it's not already loaded
            //  ie. we're in Add new student mode and not edit student mode.
            //if (currentStudentId == 0) student = new Student();
            ////  load Student record from onscreen values
            //student.Firstname = this.txtFirstNameInput.Text;
            //student.Surname = this.txtSurnameInput.Text;
            //student.DOB = this.txtDoBInput.Text;
            //student.Course = this.txtCourseTitleInput.Text;
            //if (cbYearOfStudyInput.SelectedValue == null)
            //{
            //    student.YearOfStudy = BusinessEntities.YearOfStudyEnum.First;
            //}
            //else
            //{
            //    student.YearOfStudy = (BusinessEntities.YearOfStudyEnum)this.cbYearOfStudyInput.SelectedValue;
            //}

            //student.Address = new BusinessEntities.Address();
            //student.Address.Address1 = this.txtAddressInput.Text;
            //student.Address.Town = this.txtTownInput.Text;
            //student.Address.County = this.txtCountyInput.Text;
            //student.Address.PostCode = this.txtPostCodeInput.Text;

            //student.Contact = new BusinessEntities.ContactDetail();
            //student.Contact.HomePhone = this.txtPhoneHomeInput.Text;
            //student.Contact.MobilePhone = this.txtPhoneMobileInput.Text;
            //student.Contact.HomeEmail = this.txtEmailHomeInput.Text;
            //student.Contact.StudentEmail = this.txtEmailStudentInput.Text;
        }

        private void btnSearchClick(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
                this.Frame.Navigate(typeof(SearchPage));
        }



        /*
         * Settings Charm related routines.
         * This handler is registered in constructor for this page.
         */

        /// <summary>
        /// Handler for the CommandsRequested of the application settings.
        /// </summary>
        void onCommandsRequested(SettingsPane settingsPane, SettingsPaneCommandsRequestedEventArgs eventArgs)
        {
            
            //  Instantiate the handler that will process the command requested.
            UICommandInvokedHandler handler = new UICommandInvokedHandler(onSettingsCommand);

            //  Instantiate the command to handle the command and add it to the applicationcommands collection.
            //  This one is for the general settings 
            SettingsCommand generalCommand = new SettingsCommand("generalSettings", "General", handler);

            var result = eventArgs.Request.ApplicationCommands.Count;
            if (result == 0)
                eventArgs.Request.ApplicationCommands.Add(generalCommand);

            //  This one is for the help settings.
            SettingsCommand helpCommand = new SettingsCommand("helpPage", "Help", handler);
            eventArgs.Request.ApplicationCommands.Add(helpCommand);
        }

        void onSettingsCommand(IUICommand command)
        {
            SettingsCommand settingsCommand = (SettingsCommand)command;

            //  Create a Popup window which will contain our flyout.
            settingsPopup = new Popup();
            settingsPopup.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            settingsPopup.IsLightDismissEnabled = true;
            settingsPopup.Width = settingsWidth;
            settingsPopup.Height = windowBounds.Height;
            //settingsPopup.Height = Window.Current.Bounds.Height;

            //  Add the proper animations for the flyout
            settingsPopup.ChildTransitions = new Windows.UI.Xaml.Media.Animation.TransitionCollection();
            settingsPopup.ChildTransitions.Add(new Windows.UI.Xaml.Media.Animation.PaneThemeTransition()
            {
                Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ? EdgeTransitionLocation.Right : EdgeTransitionLocation.Left
            });

            //  Create a SettingsFlyout the same dimensions as the Popup.
            Settings.SettingsFlyout mypane = new Settings.SettingsFlyout();
            mypane.Width = settingsWidth;
            mypane.Height = windowBounds.Height;
            //mypane.Height = Window.Current.Bounds.Height;
            //  Place the SettingsFlyout inside out Popup window.
            settingsPopup.Child = mypane;

            //  Define the location of our Popup
            settingsPopup.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (windowBounds.Width - settingsWidth) : 0);
            settingsPopup.SetValue(Canvas.TopProperty, 0);
            //  now, after all that stuff, open the Flyout
            settingsPopup.IsOpen = true;
        }



        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                settingsPopup.IsOpen = false;
            }
        }

        void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
        } 


    }
}
