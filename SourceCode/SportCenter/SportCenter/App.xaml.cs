using System;
using SportCenter.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportCenter {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            MainPage = new NoInternet();
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
