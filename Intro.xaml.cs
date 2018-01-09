using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=234238

namespace Tastiera_Fonetica
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class Intro : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        ResourceLoader rl = new ResourceLoader();
        LicenseInformation licenseInformation;

        public Intro()
        {
            this.InitializeComponent();
            initializeLicense();
        }

        /// <summary>
        /// Richiamato quando la pagina sta per essere visualizzata in un Frame.
        /// </summary>
        /// <param name="e">Dati dell'evento in cui vengono descritte le modalità con cui la pagina è stata raggiunta. La proprietà
        /// Parameter viene in genere utilizzata per configurare la pagina.</param>

        void initializeLicense()
        {
            // Initialize the license info for use in the app that is uploaded to the Store.
            // uncomment for release
            licenseInformation = CurrentApp.LicenseInformation;

            // Initialize the license info for testing.
            // comment the next line for release
            //licenseInformation = CurrentAppSimulator.LicenseInformation;

            // Register for the license state change event.
            licenseInformation.LicenseChanged += new LicenseChangedEventHandler(licenseChangedEventHandler);

            if (licenseInformation.IsActive)
            {
                if (licenseInformation.IsTrial)
                {
                    var longDateFormat = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("longdate");
                    var daysRemaining = (licenseInformation.ExpirationDate - DateTime.Now).Days;

                    MessageDialog msgDialog = new MessageDialog("Grazie per aver acquistato la versione di prova della tastiera fonetica! \n\r La versione trial dura " + "quantigiornidura.toString()" + " .", "Versione di prova");
                    //msgDialog.ShowAsync();
                }
            }
        }

        void licenseChangedEventHandler()
        {
            reloadLicense();
        }


        void reloadLicense()
        {
            if (licenseInformation.IsActive)
            {
                if (!licenseInformation.IsTrial)
                {
                    MessageDialog msgDialog = new MessageDialog("Grazie per aver comprato la versione completa della Tastiera Fonetica!!", "Grazie");
                    //msgDialog.ShowAsync();
                }
            }
            else
            { }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void btSi_Click(object sender, RoutedEventArgs e)
        {
            localSettings.Values["tast"] = "True";
            var page = new Tastiera();
            Window.Current.Content = page;
        }

        private async void btNo_Click(object sender, RoutedEventArgs e)
        {
            var s = rl.GetString("consiglioTast");
            var s2 = rl.GetString("scrittaAttenzione");
            MessageDialog msgDialog = new MessageDialog(s, s2);
            await msgDialog.ShowAsync();

            localSettings.Values["tast"] = "False";
            var page = new Tastiera();
            Window.Current.Content = page;
        }

        private void btHowToGo_Click(object sender, RoutedEventArgs e)
        {
            var page = new Howtogo();
            Window.Current.Content = page;
        }

        private void btWhatIs_Click(object sender, RoutedEventArgs e)
        {
            var page = new Whatis();
            Window.Current.Content = page;
        }

    }
}
