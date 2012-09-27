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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace StudentRegistrationApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : StudentRegistrationApp.Common.LayoutAwarePage
    {
        //  An instance of a Student for the form to use and to pass to the display page.
        Model.Student student;

        public MainPage()
        {
            this.InitializeComponent();
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
            foreach (var i in Enum.GetValues(typeof(Model.YearOfStudyEnum)).Cast<Model.YearOfStudyEnum>())
            {
                this.cbYearOfStudyInput.Items.Add(i);
            }

            //this.YearOfStudyInput.DataContext = Enum.GetValues(typeof( Model.YearOfStudyEnum)).Cast<Model.YearOfStudyEnum>();
            //this.YearOfStudyInput.SelectedItem = Model.YearOfStudyEnum.First;
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
            
            //  Create Student record
            student = new Model.Student();

            student.Firstname = this.txtFirstNameInput.Text;
            student.Surname = this.txtSurnameInput.Text;
//            student.DOB = this.txtDoBInput.Text;
            student.Course = this.txtCourseTitleInput.Text;
            student.YearOfStudy = (Model.YearOfStudyEnum) this.cbYearOfStudyInput.SelectedValue;

            student.Address = new Model.Address();
            student.Address.Address1 = this.txtAddressInput.Text;
            student.Address.Town = this.txtTownInput.Text;
            student.Address.County = this.txtCountyInput.Text;
            student.Address.PostCode = this.txtPostCodeInput.Text;

            student.Contact = new Model.ContactDetail();
            student.Contact.HomePhone = this.txtPhoneHomeInput.Text;
            student.Contact.MobilePhone = this.txtPhoneMobileInput.Text;
            student.Contact.HomeEmail = this.txtEmailHomeInput.Text;
            student.Contact.StudentEmail = this.txtEmailStudentInput.Text;

            //  Add the student record to the datastore (Student Repository)
            BLL.AddStudent(student);

            //  Set the record just added to the current display item.

            //  Reset the panel to accept a new student entry
            this.ResetForm();

            //  Set confirmation message.
            this.lblConfirmMsg.Text = string.Format("{0} {1} has been added as a student", student.Firstname, student.Surname);

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
            this.txtFirstNameInput.Text = string.Empty;
            this.txtSurnameInput.Text = string.Empty;
            this.txtDoBInput.Text = string.Empty;
            this.txtCourseTitleInput.Text = string.Empty;
            this.txtAddressInput.Text = string.Empty;
            this.txtTownInput.Text = string.Empty;
            this.txtCountyInput.Text = string.Empty;
            this.txtPostCodeInput.Text = string.Empty;

            //this.YearOfStudyInput.SelectedItem = Enum.GetName(typeof(Model.YearOfStudyEnum),Model.YearOfStudyEnum.First);
            //  It seems we cant set the selected item, or at least its not working for me at the moment.
            //var thisValue = Enum.ToObject(typeof(Model.YearOfStudyEnum), 1);
            //this.YearOfStudyInput.SelectedItem = thisValue;
            //  However, setting the selectedindex does work so we'll leave it at this for the moment, until I know better.
            this.cbYearOfStudyInput.SelectedIndex = 0;
            this.txtPhoneHomeInput.Text = string.Empty;
            this.txtPhoneMobileInput.Text = string.Empty;
            this.txtEmailHomeInput.Text = string.Empty;
            this.txtEmailStudentInput.Text = string.Empty;
        }
    }
}
