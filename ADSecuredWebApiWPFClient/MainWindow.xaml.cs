using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADSecuredWebApiWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            string authority = "https://fs.corp.contoso.com/adfs";
            string resourceURI = "https://web1.corp.contoso.com/ADSecuredWebApi";
            string clientID = "E1CF1107-FF90-4228-93BF-26052DD2C714"; // add this oauth clientid to adfs >> Add-AdfsClient -Name "ADSecuredWebApiWPFClient1" -ClientId "E1CF1107-FF90-4228-93BF-26052DD2C714" -RedirectUri "http://anarbitraryreturnuri/"
            string clientReturnURI = "http://anarbitraryreturnuri/";

            AuthenticationContext ac = new AuthenticationContext(authority, false);
            AuthenticationResult ar = ac.AcquireToken(resourceURI, clientID, new Uri(clientReturnURI));

            string authHeader = ar.CreateAuthorizationHeader();
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44300//api/TodoList");
            request.Headers.TryAddWithoutValidation("Authorization", authHeader);
            HttpResponseMessage response = await client.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();
            MessageBox.Show(responseString);
        }
    }
}
