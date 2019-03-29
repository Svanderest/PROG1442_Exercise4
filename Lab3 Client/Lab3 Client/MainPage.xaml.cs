using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Lab3_Client.Utils;
using Lab3_Client.Models;
using Lab3_Client.Repos;
using System.Text;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab3_Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>    
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            fillDropDown();
        }
        
        private async void fillDropDown()
        {
            //Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            ArtTypeRepository r = new ArtTypeRepository();
            try
            {
                List<ArtType> types = await r.GetArtTypes();
                //Add the All Option
                types.Add(new ArtType { ID = 0, Type = "All Types" });
                //Save them for the Patient Details 
                App thisApp = Application.Current as App;
                thisApp.ArtTypes = types;

                //Bind to the ComboBox
                TypeCombo.ItemsSource = types.OrderBy(d => d.Type);
                btnAdd.IsEnabled = true; //Since we have doctors, you can try to add new.
                showArtwork(null);
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                Common.ShowMessage("Could not complete operation:", sb.ToString());
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("connection with the server"))
                {
                    Common.ShowMessage("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.");
                }
                else
                {
                    Common.ShowMessage("Error", "Could not complete operation.");
                }
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }

        private async void showArtwork(int? TypeID)
        {
            //Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            ArtworkRepository r = new ArtworkRepository();
            try
            {
                List<Artwork> artworks;
                if (TypeID.GetValueOrDefault() > 0)
                {
                    artworks = await r.GetArtworkByType(TypeID.GetValueOrDefault());
                }
                else
                {
                    artworks = await r.GetArtwork();
                }
                artworkList.ItemsSource = artworks.OrderByDescending(e => e.ID);

            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                Common.ShowMessage("Could not complete operation:", sb.ToString());
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("server"))
                {
                    Common.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Common.ShowMessage("Error", "Could not complete operation.");
                }
            }
            finally
            {
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }

        private void TypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ArtType selType = (ArtType)TypeCombo.SelectedItem;
            showArtwork(selType?.ID);
        }

        private void artworkGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the detail page

            //Frame.Navigate(typeof(PatientDetailPage), (Artwork)e.ClickedItem);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            fillDropDown();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Note: I had to add this bit of code so I could set an
            //initial value to StartDate when I switched from using
            //Binding to x:Bind in the Details page!  Shows one way 
            //that the new x:Bind is limited in some ways.
            Artwork newWork = new Artwork();            

            // Navigate to the detail page

            //Frame.Navigate(typeof(PatientDetailPage), newWork);
        }
    }
}
