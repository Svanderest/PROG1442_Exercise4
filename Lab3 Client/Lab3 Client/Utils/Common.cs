using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Lab3_Client.Utils
{
    public static class Common
    {
        public static Uri DBUri = new Uri("http://localhost:50424//");

        //public static Uri DBUri = new Uri("https://nccorewebapi.azurewebsites.net//");

        internal async static void ShowMessage(string strTitle, string Msg)
        {
            ContentDialog diag = new ContentDialog()
            {
                Title = strTitle,
                Content = Msg,
                PrimaryButtonText = "Ok"
            };
            await diag.ShowAsync();
        }
        //DAVID Created 2 versions for MessageBox functionality
        internal async static Task<ContentDialogResult> ConfirmDialog(string strTitle, string Msg)
        {
            ContentDialog diag = new ContentDialog()
            {
                Title = strTitle,
                Content = Msg,
                PrimaryButtonText = "No",
                SecondaryButtonText = "Yes"
            };
            ContentDialogResult result = await diag.ShowAsync();
            return result;
        }
        //Added as per:
        //http://www.codeproject.com/Articles/825274/ASP-NET-Web-Api-Unwrapping-HTTP-Error-Results-and#Unwrapping-and-Handling-Errors-and-Exceptions-in-Web-Api
        public static ApiException CreateApiException(HttpResponseMessage response)
        {
            var httpErrorObject = response.Content.ReadAsStringAsync().Result;

            // Create an anonymous object to use as the template for deserialization:
            var anonymousErrorObject = new Dictionary<string, string[]>();

            // Deserialize:
            var deserializedErrorObject =
                JsonConvert.DeserializeAnonymousType(httpErrorObject, anonymousErrorObject);

            // Now wrap into an exception which best fullfills the needs of your application:
            var ex = new ApiException(response);
            
            var errors = deserializedErrorObject.Select(kvp => string.Join(". ", kvp.Value));
            for (int i = 0; i < errors.Count(); i++)
            {
                // Wrap the errors up into the base Exception.Data Dictionary:
                ex.Data.Add(i, errors.ElementAt(i));
            }

            return ex;
        }
    }
}
