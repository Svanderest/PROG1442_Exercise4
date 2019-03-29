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
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
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
                thisApp.ActiveArtTypes = types;
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
    }
}
