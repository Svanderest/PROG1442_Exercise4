using Lab3_Client.Models;
using Lab3_Client.Repos;
using Lab3_Client.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Lab3_Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArtworkDetailsPage : Page
    {
        Artwork view;

        public ArtworkDetailsPage()
        {
            this.InitializeComponent();
            //This line prevents the page from rotating to landscape
            //DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            fillDropDown();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            view = (Artwork)e.Parameter;
            this.DataContext = view;

            if (view.ID == 0) //Adding
            {
                //Disable the delete button if adding
                btnDelete.IsEnabled = false;
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                view.ArtType = null;
                ArtworkRepository er = new ArtworkRepository();
                if (view.ID == 0)
                {
                    await er.AddArtwoork(view);
                }
                else
                {
                    await er.UpdateArtwork(view);
                }
                Frame.GoBack();
            }
            catch (AggregateException ex)
            {
                string errMsg = "";
                foreach (var exception in ex.InnerExceptions)
                {
                    errMsg += Environment.NewLine + exception.Message;
                }
                Common.ShowMessage("One or more exceptions has occurred:", errMsg);
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                //sb.AppendLine(string.Format(" HTTP Status Code: {0}", apiEx.StatusCode.ToString()));
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                Common.ShowMessage("Problem Saving the Patient:", sb.ToString());
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("connection with the server"))
                {
                    Common.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Common.ShowMessage("Error", "Could not complete operation.");
                }
            }

        }

        private void fillDropDown()
        {
            try
            {
                App thisApp = Application.Current as App;
                List<ArtType> types = thisApp.ArtTypes;
                TypeCombo.ItemsSource = types.Where(d => d.ID > 0).OrderBy(d => d.Type);
            }
            catch (Exception)
            {
                Common.ShowMessage("Error", "No Doctors Available.  Go back and refresh the Patient page.");
            }

        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string strTitle = "Confirm Delete";
            string strMsg = "Are you certain that you want to delete " + view.Name + "?";
            ContentDialogResult result = await Common.ConfirmDialog(strTitle, strMsg);
            if (result == ContentDialogResult.Secondary)
            {
                try
                {
                    view.ArtType = null;
                    ArtworkRepository er = new ArtworkRepository();
                    await er.DeleteArtwork(view);
                    Frame.GoBack();
                }
                catch (AggregateException ex)
                {
                    string errMsg = "";
                    foreach (var exception in ex.InnerExceptions)
                    {
                        errMsg += Environment.NewLine + exception.Message;
                    }
                    Common.ShowMessage("One or more exceptions has occurred:", errMsg);
                }
                catch (ApiException apiEx)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Errors:");
                    foreach (var error in apiEx.Errors)
                    {
                        sb.AppendLine("-" + error);
                    }
                    Common.ShowMessage("Problem Deleting the Patient:", sb.ToString());
                }
                catch (Exception)
                {
                    Common.ShowMessage("Error", "Error Deleting Patient");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
