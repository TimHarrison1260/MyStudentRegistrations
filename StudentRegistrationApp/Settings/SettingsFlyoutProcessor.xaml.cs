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
using Windows.UI.ApplicationSettings;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace StudentRegistrationApp.Settings
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SettingsFlyoutProcessor : StudentRegistrationApp.Common.LayoutAwarePage
    {

        ////  The container for the custom content
        //private Popup settingsPopup;
        ////  desired width for the settingsUI. UI guidelines specify this
        ////  shuld be 346 or 646 depending on needs
        //private double settingsWidth = 646;
        ////  used to determine the correct height to ensure our custon UI fills the screen
        //private Rect windowBounds;


        public SettingsFlyoutProcessor()
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



        ///// <summary>
        ///// Handler for the CommandsRequested of the application settings.
        ///// </summary>
        //void onCommandsRequested(SettingsPane settingsPane, SettingsPaneCommandsRequestedEventArgs eventArgs)
        //{
        //    //  Instantiate the handler that will process the command requested.
        //    UICommandInvokedHandler handler = new UICommandInvokedHandler(onSettingsCommand);

        //    //  Instantiate the command to handle the command and add it to the applicationcommands collection.
        //    //  This one is for the general settings 
        //    SettingsCommand generalCommand = new SettingsCommand("generalSettings", "General", handler);
        //    eventArgs.Request.ApplicationCommands.Add(generalCommand);
        //    //  This one is for the help settings.
        //    SettingsCommand helpCommand = new SettingsCommand("helpPage", "Help", handler);
        //    eventArgs.Request.ApplicationCommands.Add(helpCommand);
        //}

        //void onSettingsCommand(IUICommand command)
        //{
        //    SettingsCommand settingsCommand = (SettingsCommand)command;

        //    //  Create a Popup window which will contain our flyout.
        //    settingsPopup = new Popup();
        //    settingsPopup.Closed += OnPopupClosed;
        //    Window.Current.Activated += OnWindowActivated;
        //    settingsPopup.IsLightDismissEnabled = true;
        //    settingsPopup.Width = settingsWidth;
        //    //settingsPopup.Height = windowBounds.Height;
        //    settingsPopup.Height = Window.Current.Bounds.Height;

        //    //  Add the proper animations for the flyout
        //    settingsPopup.ChildTransitions = new Windows.UI.Xaml.Media.Animation.TransitionCollection();
        //    settingsPopup.ChildTransitions.Add(new Windows.UI.Xaml.Media.Animation.PaneThemeTransition()
        //    {
        //        Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ? EdgeTransitionLocation.Right : EdgeTransitionLocation.Left
        //    });

        //    //  Create a SettingsFlyout the same dimensions as the Popup.
        //    Settings.SettingsFlyout mypane = new Settings.SettingsFlyout();
        //    mypane.Width = settingsWidth;
        //    //mypane.Height = windowBounds.Height;
        //    mypane.Height = Window.Current.Bounds.Height;
        //    //  Place the SettingsFlyout inside out Popup window.
        //    settingsPopup.Child = mypane;

        //    //  Define the location of our Popup
        //    settingsPopup.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (windowBounds.Width - settingsWidth) : 0);
        //    settingsPopup.SetValue(Canvas.TopProperty, 0);
        //    //  now, after all that stuff, open the Flyout
        //    settingsPopup.IsOpen = true;
        //}

        //private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        //{
        //    if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
        //    {
        //        settingsPopup.IsOpen = false;
        //    }
        //}

        //void OnPopupClosed(object sender, object e)
        //{
        //    Window.Current.Activated -= OnWindowActivated;
        //} 

    }
}
