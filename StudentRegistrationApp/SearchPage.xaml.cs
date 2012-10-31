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
    public sealed partial class SearchPage : StudentRegistrationApp.Common.LayoutAwarePage
    {

        //  Instantiate the Business Layer to process the input.
        private readonly BusinessLayer.IStudentBLL BLL = new BusinessLayer.StudentBLL((App.Current as App).GetRepository());


        public SearchPage()
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
            foreach (var i in Enum.GetValues(typeof(BusinessEntities.SearchCategoriesEnum)).Cast<BusinessEntities.SearchCategoriesEnum>())
            {
                this.cboCategories.Items.Add(i);
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //  get the search criteria
            var searchString = this.txtSearchValue.Text;
            var searchCategory = (this.cboCategories.Items.Count() > 0) ? this.cboCategories.SelectedValue.ToString() : string.Empty;

            //  get the students matching the search criteria from the business model.
            var students = BLL.SearchStudents(searchString, searchCategory);

            //  Display the results on the search page
            this.lstSearchResults.ItemsSource = students;
        }

        private void btnNewSearch_Click(object sender, RoutedEventArgs e)
        {
            //  Initialise the search Criteria (NOT including the Category or Previous Searches DropDown boxes
            this.txtSearchValue.Text = string.Empty;
            if (this.cboCategories.Items.Count() > 0)
                this.cboCategories.SelectedValue = null;
            this.lstSearchResults.ItemsSource = null;
        }

        private void BtnSaveSearch_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
