using System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
namespace CSharp_Settings.Settings
{
    public sealed partial class Help_Settings
    {
        bool tast;
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public Help_Settings()
        {
            InitializeComponent();
            Object valueTast = localSettings.Values["tast"];
            if (valueTast.ToString().Equals("True"))
                tast = true;
            else
                tast = false;

            tsTast.DataContext = tast;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Parent is Popup)
                ((Popup)Parent).IsOpen = false;
            SettingsPane.Show();
        }

        private void tsTast_Toggled(object sender, RoutedEventArgs e)
        {
            if(tsTast.IsOn)
                localSettings.Values["tast"] = "True";
            else
                localSettings.Values["tast"] = "False";
        }
    }
}