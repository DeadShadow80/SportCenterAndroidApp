using Plugin.Connectivity;
using SportCenter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportCenter.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoInternet : ContentPage
    {
        public NoInternet()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Eroare", "Aplicația nu poate funcționa fără o conexiune activă la internet, închideți aplicația, conectați-vă la internet și încercați iar.", "Ok");
                
            }
            else
            {
                Application.Current.MainPage = new MainPage();
                Application.Current.MainPage.BindingContext = new MainPageViewModel();
            }    
        }
    }
}