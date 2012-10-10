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

using Windows.UI.Popups;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace StudentRegistrationApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class StudentDisplayPage : StudentRegistrationApp.Common.LayoutAwarePage
    {
        long currentDisplayId = 0;

        public StudentDisplayPage()
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
                this.cbYearOfStudy.Items.Add(i);
            }
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
        /// Reads in the parameter passed to this page and uses it to load
        /// the Student to display on this page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            BusinessLayer.IStudentBLL BLL = new BusinessLayer.StudentBLL((App.Current as App).GetRepository());

            BusinessEntities.Student student = (BusinessEntities.Student)e.Parameter;

            if (student != null)
            {
                //  We do this here, not because we have the full student record we need, but because
                //  eventually we'll be passing an Id instead of the student itself, so the logic is 
                //  right for now.
                BusinessEntities.Student result = BLL.GetStudent(student.Firstname, student.Surname);

                //  Now move the results to the fields.
                RefreshUI(result);
            }
            else
            {
                student = BLL.FirstStudent();
                if (student != null) RefreshUI(student);
            }
        }


        private void btfFirst_Click(object sender, RoutedEventArgs e)
        {
            BusinessLayer.IStudentBLL BLL = new BusinessLayer.StudentBLL((App.Current as App).GetRepository());
            var student = BLL.FirstStudent();
            if (student != null) RefreshUI(student);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BusinessLayer.IStudentBLL BLL = new BusinessLayer.StudentBLL((App.Current as App).GetRepository());
            var student = BLL.PreviousStudent();
            if (student != null) RefreshUI(student);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            BusinessLayer.IStudentBLL BLL = new BusinessLayer.StudentBLL((App.Current as App).GetRepository());
            var student = BLL.NextStudent();
            if (student != null) RefreshUI(student);
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            BusinessLayer.IStudentBLL BLL = new BusinessLayer.StudentBLL((App.Current as App).GetRepository());
            var student = BLL.LastStudent();
            if (student != null) RefreshUI(student);
        }



        private void RefreshUI(BusinessEntities.Student student)
        {

            //  Preserve Id of current Item being displayed
            currentDisplayId = student.Id;

            //  refresh the UI components with the student data.
            this.txtFirstName.Text = student.Firstname;
            this.txtSurname.Text = student.Surname;
            this.txtDoB.Text = student.DOB.ToString();
            this.cbYearOfStudy.SelectedIndex = ((int)student.YearOfStudy - 1);
            this.txtCourseTitle.Text = student.Course;

            this.txtAddress.Text = student.Address.Address1;
            this.txtTown.Text = student.Address.Town;
            this.txtCounty.Text = student.Address.County;
            this.txtPostCode.Text = student.Address.PostCode;

            this.txtPhoneHome.Text = student.Contact.HomePhone;
            this.txtPhoneMobile.Text = student.Contact.MobilePhone;
            this.txtEmailHome.Text = student.Contact.HomeEmail;
            this.txtEmailStudent.Text = student.Contact.StudentEmail;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                //  Navigate to the main page and take the tudent currently displayed
                //  The current student details are displayed and therefore available,
                //  however, we only need to take the FirstName and Surname across as
                //  the mainpage can find the appropriate record.
                this.Frame.Navigate(typeof(MainPage), currentDisplayId);

            }
        }

        private void btnDeleteClick(object sender, RoutedEventArgs e)
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog("Are you sure you want to delete this Student Record?");
            messageDialog.Commands.Add(new UICommand("Yes", (command) => {
                BusinessLayer.IStudentBLL BLL = new BusinessLayer.StudentBLL((App.Current as App).GetRepository());
                var studentToDelete = BLL.GetStudentById(currentDisplayId);
                BLL.DeleteStudent(studentToDelete);
                var student = BLL.PreviousStudent();
                RefreshUI(student);
            }));
            messageDialog.Commands.Add(new UICommand("No", (command) => {}));
            messageDialog.DefaultCommandIndex = 1;
            messageDialog.ShowAsync();
        }

        private void btnSearchClick(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
                this.Frame.Navigate(typeof(SearchPage));
        }

    }
}
