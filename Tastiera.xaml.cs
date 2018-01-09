using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ServiceModel;
using Windows.Devices.Input;
using Windows.UI.Popups;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using CSharp_Settings.Settings;
using Windows.UI.ViewManagement;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Provider;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.AccessCache;
using Windows.Storage.Search;
using Windows.ApplicationModel.Store;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Tastiera_Fonetica
{
    public sealed partial class Tastiera : Page
    {
        public IReadOnlyCollection<PointerDevice> pointColl;
        LicenseInformation licenseInformation;
        bool phKeyb = false;
        bool ascolta = true;
        bool muto = false;
        bool playincorso = false;
        StorageFile fileAperto;
        Rect _window;
        ResourceLoader rl = new ResourceLoader();
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Tastiera()
        {
            this.InitializeComponent();
            this.SizeChanged += Tastiera_SizeChanged;

            Object valueTast = localSettings.Values["tast"];
            if (valueTast == null)
            {
                //significa che è la prima volta, tutto il codice da eseguire SOLO la prima volta qui dentro
                //visualizzo l'intro
                var page = new Intro();
                Window.Current.Content = page;
            }
            else
            {
                //significa che non è la prima volta, tutto il codice da eseguire sempre (compresa la prima volta) qui dentro
                if ((String)valueTast == "True")
                {
                    //significa che c'è la tastiera fisica
                    riccoEditore.IsReadOnly = false;
                    btSu.Visibility = Visibility.Collapsed;
                    btGiu.Visibility = Visibility.Collapsed;
                    btDx.Visibility = Visibility.Collapsed;
                    btSx.Visibility = Visibility.Collapsed;
                    _btSu.Visibility = Visibility.Collapsed;
                    _btGiu.Visibility = Visibility.Collapsed;
                    _btDx.Visibility = Visibility.Collapsed;
                    _btSx.Visibility = Visibility.Collapsed;
                }
                else
                { }
                //settings
                _window = Window.Current.Bounds;
                Window.Current.SizeChanged += OnWindowSizeChanged;
                SettingsPane.GetForCurrentView().CommandsRequested += CommandsRequested;

                //license
                initializeLicense();
            }
        }

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

                    disableVocali();

                    var s1 = rl.GetString("scrittaPeriodoprova1");
                    var s2 = rl.GetString("scrittaPeriodoprova2");
                    var s3 = rl.GetString("scrittaAttenzione");
                    MessageDialog msgDialog = new MessageDialog(s1 + daysRemaining.ToString() + s2, s3);
                    msgDialog.ShowAsync();
                }
                else
                {
                    btCopriVocali.Visibility = Visibility.Collapsed;
                    _btCopriVocali.Visibility = Visibility.Collapsed;
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
                    btCopriVocali.Visibility = Visibility.Collapsed;
                    _btCopriVocali.Visibility = Visibility.Collapsed;
                    var s1 = rl.GetString("scrittaComprato");
                    var s2 = rl.GetString("scrittaGrazie");
                    MessageDialog msgDialog = new MessageDialog(s1, s2);
                    msgDialog.ShowAsync();
                }
            }
            else
            { }
        }

        void disableVocali()
        {
            if (vbBottoni.Visibility == Visibility.Collapsed)
            {
                #region disattiva vocali snapped
                _v0069.IsEnabled = false;
                _c__178.IsEnabled = false;
                _c__179.IsEnabled = false;
                _v0079.IsEnabled = false;
                _v0268.IsEnabled = false;
                _v0289.IsEnabled = false;
                _v026f.IsEnabled = false;
                _c__180.IsEnabled = false;
                _c__181.IsEnabled = false;
                _v0075.IsEnabled = false;
                _v026a.IsEnabled = false;
                _c__182.IsEnabled = false;
                _v0269.IsEnabled = false;
                _v028f.IsEnabled = false;
                _c__183.IsEnabled = false;
                _c__184.IsEnabled = false;
                _c__185.IsEnabled = false;
                _c__186.IsEnabled = false;
                _c__187.IsEnabled = false;
                _v028a.IsEnabled = false;
                _v0065.IsEnabled = false;
                _c__188.IsEnabled = false;
                _v0258.IsEnabled = false;
                _v00f8.IsEnabled = false;
                _v0259.IsEnabled = false;
                _v0275.IsEnabled = false;
                _v0264.IsEnabled = false;
                _c__189.IsEnabled = false;
                _c__190.IsEnabled = false;
                _v006f.IsEnabled = false;
                _c__191.IsEnabled = false;
                _c__192.IsEnabled = false;
                _c__193.IsEnabled = false;
                _c__194.IsEnabled = false;
                _v025c.IsEnabled = false;
                _v025e.IsEnabled = false;
                _c__195.IsEnabled = false;
                _c__196.IsEnabled = false;
                _c__197.IsEnabled = false;
                _c__198.IsEnabled = false;
                _v025b.IsEnabled = false;
                _c__199.IsEnabled = false;
                _c__200.IsEnabled = false;
                _v0153.IsEnabled = false;
                _v0250.IsEnabled = false;
                _c__201.IsEnabled = false;
                _v028c.IsEnabled = false;
                _c__202.IsEnabled = false;
                _c__203.IsEnabled = false;
                _v0254.IsEnabled = false;
                _v00e6.IsEnabled = false;
                _c__204.IsEnabled = false;
                _c__205.IsEnabled = false;
                _v0276.IsEnabled = false;
                _v0061.IsEnabled = false;
                _c__206.IsEnabled = false;
                _v0251.IsEnabled = false;
                _c__207.IsEnabled = false;
                _c__208.IsEnabled = false;
                _v0252.IsEnabled = false;
                _c__209.IsEnabled = false;
                _c__210.IsEnabled = false;
                _c__211.IsEnabled = false;
                _c__212.IsEnabled = false;
                _c__213.IsEnabled = false;
                _c__214.IsEnabled = false;
                _c__215.IsEnabled = false;
                _c__216.IsEnabled = false;
                _c__217.IsEnabled = false;
                _c__218.IsEnabled = false;
                _c__219.IsEnabled = false;
                _c__220.IsEnabled = false;
                _c__221.IsEnabled = false;
                _c__222.IsEnabled = false;
                _c__223.IsEnabled = false;
                _c__224.IsEnabled = false;
                _c__225.IsEnabled = false;
                _c__226.IsEnabled = false;
                _c__227.IsEnabled = false;
                _c__228.IsEnabled = false;
                _c__229.IsEnabled = false;
                _c__230.IsEnabled = false;
                _c__231.IsEnabled = false;
                _c__232.IsEnabled = false;
                _c__233.IsEnabled = false;
                _c__234.IsEnabled = false;
                _c__235.IsEnabled = false;
                _c__236.IsEnabled = false;
                _c__237.IsEnabled = false;
                _c__238.IsEnabled = false;
                #endregion
            }
            else
            {
                #region disattiva vocali not snapped
                v0069.IsEnabled = false;
                c__178.IsEnabled = false;
                c__179.IsEnabled = false;
                v0079.IsEnabled = false;
                v0268.IsEnabled = false;
                v0289.IsEnabled = false;
                v026f.IsEnabled = false;
                c__180.IsEnabled = false;
                c__181.IsEnabled = false;
                v0075.IsEnabled = false;
                v026a.IsEnabled = false;
                c__182.IsEnabled = false;
                v0269.IsEnabled = false;
                v028f.IsEnabled = false;
                c__183.IsEnabled = false;
                c__184.IsEnabled = false;
                c__185.IsEnabled = false;
                c__186.IsEnabled = false;
                c__187.IsEnabled = false;
                v028a.IsEnabled = false;
                v0065.IsEnabled = false;
                c__188.IsEnabled = false;
                v0258.IsEnabled = false;
                v00f8.IsEnabled = false;
                v0259.IsEnabled = false;
                v0275.IsEnabled = false;
                v0264.IsEnabled = false;
                c__189.IsEnabled = false;
                c__190.IsEnabled = false;
                v006f.IsEnabled = false;
                c__191.IsEnabled = false;
                c__192.IsEnabled = false;
                c__193.IsEnabled = false;
                c__194.IsEnabled = false;
                v025c.IsEnabled = false;
                v025e.IsEnabled = false;
                c__195.IsEnabled = false;
                c__196.IsEnabled = false;
                c__197.IsEnabled = false;
                c__198.IsEnabled = false;
                v025b.IsEnabled = false;
                c__199.IsEnabled = false;
                c__200.IsEnabled = false;
                v0153.IsEnabled = false;
                v0250.IsEnabled = false;
                c__201.IsEnabled = false;
                v028c.IsEnabled = false;
                c__202.IsEnabled = false;
                c__203.IsEnabled = false;
                v0254.IsEnabled = false;
                v00e6.IsEnabled = false;
                c__204.IsEnabled = false;
                c__205.IsEnabled = false;
                v0276.IsEnabled = false;
                v0061.IsEnabled = false;
                c__206.IsEnabled = false;
                v0251.IsEnabled = false;
                c__207.IsEnabled = false;
                c__208.IsEnabled = false;
                v0252.IsEnabled = false;
                c__209.IsEnabled = false;
                c__210.IsEnabled = false;
                c__211.IsEnabled = false;
                c__212.IsEnabled = false;
                c__213.IsEnabled = false;
                c__214.IsEnabled = false;
                c__215.IsEnabled = false;
                c__216.IsEnabled = false;
                c__217.IsEnabled = false;
                c__218.IsEnabled = false;
                c__219.IsEnabled = false;
                c__220.IsEnabled = false;
                c__221.IsEnabled = false;
                c__222.IsEnabled = false;
                c__223.IsEnabled = false;
                c__224.IsEnabled = false;
                c__225.IsEnabled = false;
                c__226.IsEnabled = false;
                c__227.IsEnabled = false;
                c__228.IsEnabled = false;
                c__229.IsEnabled = false;
                c__230.IsEnabled = false;
                c__231.IsEnabled = false;
                c__232.IsEnabled = false;
                c__233.IsEnabled = false;
                c__234.IsEnabled = false;
                c__235.IsEnabled = false;
                c__236.IsEnabled = false;
                c__237.IsEnabled = false;
                c__238.IsEnabled = false;

                #endregion
            }
        }

        private void btCopriVocali_Click(object sender, RoutedEventArgs e)
        {
            var s1 = rl.GetString("scrittaAttenzione");
            var s2 = rl.GetString("scrittaVocaliDisattivate");
            MessageDialog msgDialog = new MessageDialog(s2, s1);
            msgDialog.ShowAsync();
        }

        void Tastiera_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < e.NewSize.Height)
            {
                vbBottoni.Visibility = Visibility.Collapsed;
                _bottoni.Visibility = Visibility.Visible;
                btShowLab.Visibility = Visibility.Collapsed;
            }
            else
            {
                vbBottoni.Visibility = Visibility.Visible;
                _bottoni.Visibility = Visibility.Collapsed;
                btShowLab.Visibility = Visibility.Visible;
            }

            if (licenseInformation.IsActive)
            {
                if (licenseInformation.IsTrial)
                {
                    disableVocali();
                }
            }
        }

        #region settings

        private Popup _popUp;
        private const double WIDTH = 646;

        private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            _window = Window.Current.Bounds;
        }

        private void CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            var s = rl.GetString("scrittaImpostazioni");
            args.Request.ApplicationCommands.Add(new SettingsCommand(s, s, Handler));
        }

        private void Handler(IUICommand command)
        {
            _popUp = new Popup
            {
                Width = WIDTH,
                Height = _window.Height,
                IsLightDismissEnabled = true,
                IsOpen = true
            };
            _popUp.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            _popUp.Child = new Help_Settings { Width = WIDTH, Height = _window.Height };
            _popUp.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (_window.Width - WIDTH) : 0);
            _popUp.SetValue(Canvas.TopProperty, 0);
        }

        private void OnWindowActivated(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == CoreWindowActivationState.Deactivated)
                _popUp.IsOpen = false;
        }

        private void OnPopupClosed(object sender, object e)
        {
            Object valueTast = localSettings.Values["tast"];
            if ((String)valueTast == "True")
            {

                riccoEditore.IsReadOnly = false;
                phKeyb = true;
            }
            else
            {

                if ((String)valueTast == "False") 
                    riccoEditore.IsReadOnly = true;
                phKeyb = false;
            }
            Window.Current.Activated -= OnWindowActivated;
        }
        #endregion

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private async void btFono_click(object sender, RoutedEventArgs e)
        {
            string s = string.Empty;

            Button bt = new Button();
            bt = (Button)sender;

            Object valueTast = localSettings.Values["tast"];

            if (!bt.Content.ToString().Equals("--"))
            {
                riccoEditore.IsReadOnly = false;
                riccoEditore.Document.Selection.SetText(Windows.UI.Text.TextSetOptions.FormatRtf,bt.Content.ToString());

                if ((String)valueTast == "False")
                    riccoEditore.IsReadOnly = true;

                riccoEditore.Focus(Windows.UI.Xaml.FocusState.Programmatic);

                riccoEditore.Document.Selection.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out s);

                if (ascolta && s != "pf")
                {
                    char[] charSet;
                    string uni = string.Empty;
                    Uri url;

                    riccoEditore.Document.Selection.Collapse(false);
                    charSet = s.ToCharArray();

                    uni = ((int)charSet[0]).ToString("X4").ToLower();

                    url = new Uri("ms-appx:///mp3/" + uni + ".mp3", UriKind.Absolute);
                    await playSounds(url);
                }
                else
                {
                    riccoEditore.Document.Selection.Collapse(false);
                }
            }
        }

        private void btBacks_click(object sender, RoutedEventArgs e)
        {
            Object valueTast = localSettings.Values["tast"];

            riccoEditore.IsReadOnly = false;
            if (riccoEditore.Document.Selection.StartPosition == riccoEditore.Document.Selection.EndPosition)
                riccoEditore.Document.Selection.MoveStart(TextRangeUnit.Character, -1);
            riccoEditore.Document.Selection.Delete(TextRangeUnit.Character, 1);

            if ((String)valueTast == "False")
                riccoEditore.IsReadOnly = true;
            else
                riccoEditore.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void btSpazio_click(object sender, RoutedEventArgs e)
        {
            Object valueTast = localSettings.Values["tast"];

            riccoEditore.IsReadOnly = false;
            riccoEditore.Document.Selection.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, " ");
            riccoEditore.Document.Selection.Collapse(false);

            if ((String)valueTast == "False")
                riccoEditore.IsReadOnly = true;
            else
                riccoEditore.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void btInvio_click(object sender, RoutedEventArgs e)
        {
            Object valueTast = localSettings.Values["tast"];

            riccoEditore.IsReadOnly = false;
            riccoEditore.Document.Selection.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, "\n");
            riccoEditore.Document.Selection.Collapse(false);

            if ((String)valueTast == "False")
                riccoEditore.IsReadOnly = true;
            else
                riccoEditore.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }
        

        private void btShift_click(object sender, RoutedEventArgs e)
        {
            bool destraSi;
            if (vbBottoni.Visibility == Visibility.Visible)
                destraSi = c006d.IsEnabled;
            else
                destraSi = (_c006d.Visibility == Visibility.Visible);
            if (!destraSi)
            {
                if (vbBottoni.Visibility == Visibility.Visible)
                {
                    #region disable
                    c__219.IsEnabled = false;
                    c__1.IsEnabled = false;
                    c__90.IsEnabled = false;
                    c03c7.IsEnabled = false;
                    c__162.IsEnabled = false;
                    c__217.IsEnabled = false;
                    c__215.IsEnabled = false;
                    c0078.IsEnabled = false;
                    c__160.IsEnabled = false;
                    c__237.IsEnabled = false;
                    c__213.IsEnabled = false;
                    c__34.IsEnabled = false;
                    c__138.IsEnabled = false;
                    c__87.IsEnabled = false;
                    c__200.IsEnabled = false;
                    c0288.IsEnabled = false;
                    c__116.IsEnabled = false;
                    c__65.IsEnabled = false;
                    c__185.IsEnabled = false;
                    c__5.IsEnabled = false;
                    c__96.IsEnabled = false;
                    c__42.IsEnabled = false;
                    c__168.IsEnabled = false;
                    c0066.IsEnabled = false;
                    c__148.IsEnabled = false;
                    c__225.IsEnabled = false;
                    c__205.IsEnabled = false;
                    c0294.IsEnabled = false;
                    c__125.IsEnabled = false;
                    c__74.IsEnabled = false;
                    v0264.IsEnabled = false;
                    c__12.IsEnabled = false;
                    c__106.IsEnabled = false;
                    c__50.IsEnabled = false;
                    v0069.IsEnabled = false;
                    c00e7.IsEnabled = false;
                    c__158.IsEnabled = false;
                    c__235.IsEnabled = false;
                    c__211.IsEnabled = false;
                    c02a7.IsEnabled = false;
                    c__132.IsEnabled = false;
                    c__83.IsEnabled = false;
                    c__195.IsEnabled = false;
                    c__19.IsEnabled = false;
                    c__110.IsEnabled = false;
                    c__60.IsEnabled = false;
                    v026a.IsEnabled = false;
                    c__31.IsEnabled = false;
                    c__134.IsEnabled = false;
                    c__84.IsEnabled = false;
                    c__197.IsEnabled = false;
                    c0074.IsEnabled = false;
                    c__112.IsEnabled = false;
                    c__62.IsEnabled = false;
                    v0269.IsEnabled = false;
                    c__2.IsEnabled = false;
                    c__92.IsEnabled = false;
                    c0127.IsEnabled = false;
                    c__164.IsEnabled = false;
                    c__40.IsEnabled = false;
                    c__144.IsEnabled = false;
                    c__221.IsEnabled = false;
                    c__203.IsEnabled = false;
                    c0071.IsEnabled = false;
                    c__122.IsEnabled = false;
                    c__70.IsEnabled = false;
                    v0258.IsEnabled = false;
                    c__10.IsEnabled = false;
                    c__103.IsEnabled = false;
                    c026c.IsEnabled = false;
                    c__174.IsEnabled = false;
                    c0283.IsEnabled = false;
                    c__154.IsEnabled = false;
                    c__231.IsEnabled = false;
                    c__208.IsEnabled = false;
                    c__29.IsEnabled = false;
                    c__128.IsEnabled = false;
                    c__79.IsEnabled = false;
                    c__193.IsEnabled = false;
                    c0070.IsEnabled = false;
                    c0265.IsEnabled = false;
                    c__56.IsEnabled = false;
                    v026f.IsEnabled = false;
                    c__36.IsEnabled = false;
                    c__140.IsEnabled = false;
                    c__89.IsEnabled = false;
                    v0250.IsEnabled = false;
                    c0063.IsEnabled = false;
                    c__118.IsEnabled = false;
                    c__67.IsEnabled = false;
                    c__187.IsEnabled = false;
                    c__7.IsEnabled = false;
                    c__98.IsEnabled = false;
                    c__44.IsEnabled = false;
                    c__170.IsEnabled = false;
                    c03b8.IsEnabled = false;
                    c__150.IsEnabled = false;
                    c__227.IsEnabled = false;
                    v0061.IsEnabled = false;
                    c__26.IsEnabled = false;
                    c__126.IsEnabled = false;
                    c__76.IsEnabled = false;
                    c__190.IsEnabled = false;
                    c__13.IsEnabled = false;
                    c__107.IsEnabled = false;
                    c__52.IsEnabled = false;
                    c__179.IsEnabled = false;
                    c__33.IsEnabled = false;
                    c__136.IsEnabled = false;
                    c__86.IsEnabled = false;
                    v025b.IsEnabled = false;
                    c__21.IsEnabled = false;
                    c__114.IsEnabled = false;
                    c__63.IsEnabled = false;
                    c__183.IsEnabled = false;
                    c__3.IsEnabled = false;
                    c__94.IsEnabled = false;
                    c0068.IsEnabled = false;
                    c__166.IsEnabled = false;
                    c0278.IsEnabled = false;
                    c__146.IsEnabled = false;
                    c__223.IsEnabled = false;
                    v00e6.IsEnabled = false;
                    c__23.IsEnabled = false;
                    c__123.IsEnabled = false;
                    c__72.IsEnabled = false;
                    v0259.IsEnabled = false;
                    c__11.IsEnabled = false;
                    c__104.IsEnabled = false;
                    c__48.IsEnabled = false;
                    c__176.IsEnabled = false;
                    c0282.IsEnabled = false;
                    c__156.IsEnabled = false;
                    c__233.IsEnabled = false;
                    c__209.IsEnabled = false;
                    c024c.IsEnabled = false;
                    c__130.IsEnabled = false;
                    c__81.IsEnabled = false;
                    v025c.IsEnabled = false;
                    c__17.IsEnabled = false;
                    c0077.IsEnabled = false;
                    c__58.IsEnabled = false;
                    c__181.IsEnabled = false;
                    c__38.IsEnabled = false;
                    c__142.IsEnabled = false;
                    c__21.IsEnabled = false;
                    v028c.IsEnabled = false;
                    c006b.IsEnabled = false;
                    c__120.IsEnabled = false;
                    c__68.IsEnabled = false;
                    v0065.IsEnabled = false;
                    c__9.IsEnabled = false;
                    c__101.IsEnabled = false;
                    c__46.IsEnabled = false;
                    c__172.IsEnabled = false;
                    c0073.IsEnabled = false;
                    c__152.IsEnabled = false;
                    c__229.IsEnabled = false;
                    v0251.IsEnabled = false;
                    c____.IsEnabled = false;
                    c__127.IsEnabled = false;
                    c__77.IsEnabled = false;
                    c__191.IsEnabled = false;
                    c__15.IsEnabled = false;
                    c__109.IsEnabled = false;
                    c__54.IsEnabled = false;
                    v0268.IsEnabled = false;
                    c02a6.IsEnabled = false;
                    #endregion
                    #region enable
                    c02a3.IsEnabled = true;
                    c006d.IsEnabled = true;
                    c__91.IsEnabled = true;
                    c0281.IsEnabled = true;
                    c__163.IsEnabled = true;
                    c__218.IsEnabled = true;
                    c__216.IsEnabled = true;
                    c0263.IsEnabled = true;
                    c__161.IsEnabled = true;
                    c__238.IsEnabled = true;
                    c__214.IsEnabled = true;
                    c__35.IsEnabled = true;
                    c__139.IsEnabled = true;
                    c__88.IsEnabled = true;
                    v0153.IsEnabled = true;
                    c0256.IsEnabled = true;
                    c__117.IsEnabled = true;
                    c__66.IsEnabled = true;
                    c__186.IsEnabled = true;
                    c006e.IsEnabled = true;
                    c__97.IsEnabled = true;
                    c__43.IsEnabled = true;
                    c__169.IsEnabled = true;
                    c0076.IsEnabled = true;
                    c__149.IsEnabled = true;
                    c__226.IsEnabled = true;
                    v0276.IsEnabled = true;
                    c__25.IsEnabled = true;
                    c026d.IsEnabled = true;
                    c__75.IsEnabled = true;
                    c__189.IsEnabled = true;
                    c0274.IsEnabled = true;
                    c0279.IsEnabled = true;
                    c__51.IsEnabled = true;
                    c__178.IsEnabled = true;
                    c029d.IsEnabled = true;
                    c__159.IsEnabled = true;
                    c__236.IsEnabled = true;
                    c__212.IsEnabled = true;
                    c02a4.IsEnabled = true;
                    c__133.IsEnabled = true;
                    c2c71.IsEnabled = true;
                    c__196.IsEnabled = true;
                    c__20.IsEnabled = true;
                    c__111.IsEnabled = true;
                    c__61.IsEnabled = true;
                    c__182.IsEnabled = true;
                    c__32.IsEnabled = true;
                    c__135.IsEnabled = true;
                    c__85.IsEnabled = true;
                    c__198.IsEnabled = true;
                    c0064.IsEnabled = true;
                    c__113.IsEnabled = true;
                    c0042.IsEnabled = true;
                    v028f.IsEnabled = true;
                    c0271.IsEnabled = true;
                    c__93.IsEnabled = true;
                    c0295.IsEnabled = true;
                    c__165.IsEnabled = true;
                    c__41.IsEnabled = true;
                    c__145.IsEnabled = true;
                    c__222.IsEnabled = true;
                    v0254.IsEnabled = true;
                    c0047.IsEnabled = true;
                    c006c.IsEnabled = true;
                    c__71.IsEnabled = true;
                    v00f8.IsEnabled = true;
                    c0272.IsEnabled = true;
                    c028b.IsEnabled = true;
                    c026e.IsEnabled = true;
                    c__175.IsEnabled = true;
                    c0292.IsEnabled = true;
                    c__155.IsEnabled = true;
                    c__232.IsEnabled = true;
                    v0252.IsEnabled = true;
                    c__30.IsEnabled = true;
                    c__129.IsEnabled = true;
                    c__80.IsEnabled = true;
                    c__194.IsEnabled = true;
                    c0062.IsEnabled = true;
                    c006a.IsEnabled = true;
                    c__57.IsEnabled = true;
                    c__180.IsEnabled = true;
                    c__37.IsEnabled = true;
                    c__141.IsEnabled = true;
                    c027d.IsEnabled = true;
                    c__201.IsEnabled = true;
                    c025f.IsEnabled = true;
                    c__119.IsEnabled = true;
                    c0072.IsEnabled = true;
                    v028a.IsEnabled = true;
                    c__8.IsEnabled = true;
                    c__99.IsEnabled = true;
                    c__45.IsEnabled = true;
                    c__171.IsEnabled = true;
                    c00f0.IsEnabled = true;
                    c__151.IsEnabled = true;
                    c__228.IsEnabled = true;
                    c__206.IsEnabled = true;
                    c__27.IsEnabled = true;
                    c028e.IsEnabled = true;
                    c0052.IsEnabled = true;
                    v006f.IsEnabled = true;
                    c__14.IsEnabled = true;
                    c__108.IsEnabled = true;
                    c__53.IsEnabled = true;
                    v0079.IsEnabled = true;
                    c__33_2.IsEnabled = true;
                    c__137.IsEnabled = true;
                    c027e.IsEnabled = true;
                    c__199.IsEnabled = true;
                    c__22.IsEnabled = true;
                    c__115.IsEnabled = true;
                    c__64.IsEnabled = true;
                    c__184.IsEnabled = true;
                    c__4.IsEnabled = true;
                    c__95.IsEnabled = true;
                    c0266.IsEnabled = true;
                    c__167.IsEnabled = true;
                    c03b2.IsEnabled = true;
                    c__147.IsEnabled = true;
                    c__224.IsEnabled = true;
                    c__204.IsEnabled = true;
                    c__24.IsEnabled = true;
                    c__124.IsEnabled = true;
                    c__73.IsEnabled = true;
                    v0275.IsEnabled = true;
                    c014b.IsEnabled = true;
                    c__105.IsEnabled = true;
                    c__49.IsEnabled = true;
                    c__177.IsEnabled = true;
                    c0290.IsEnabled = true;
                    c__157.IsEnabled = true;
                    c__234.IsEnabled = true;
                    c__210.IsEnabled = true;
                    //c02a.IsEnabled = true;
                    c__131.IsEnabled = true;
                    c__82.IsEnabled = true;
                    v025e.IsEnabled = true;
                    c__18.IsEnabled = true;
                    c0270.IsEnabled = true;
                    c__59.IsEnabled = true;
                    v0075.IsEnabled = true;
                    c__39.IsEnabled = true;
                    c__143.IsEnabled = true;
                    c__220.IsEnabled = true;
                    c__202.IsEnabled = true;
                    c0261.IsEnabled = true;
                    c__121.IsEnabled = true;
                    c__69.IsEnabled = true;
                    c__188.IsEnabled = true;
                    c0273.IsEnabled = true;
                    c__102.IsEnabled = true;
                    c__47.IsEnabled = true;
                    c__173.IsEnabled = true;
                    c007a.IsEnabled = true;
                    c__230.IsEnabled = true;
                    c__207.IsEnabled = true;
                    c__28.IsEnabled = true;
                    c024c.IsEnabled = true;
                    c__78.IsEnabled = true;
                    c__192.IsEnabled = true;
                    c__16.IsEnabled = true;
                    c027b.IsEnabled = true;
                    c__55.IsEnabled = true;
                    v0289.IsEnabled = true;
                    c__153.IsEnabled = true;
                    #endregion
                }
                else
                {
                    #region invisible
                    _c__219.Visibility = Visibility.Collapsed;
                    _c__1.Visibility = Visibility.Collapsed;
                    _c__90.Visibility = Visibility.Collapsed;
                    _c03c7.Visibility = Visibility.Collapsed;
                    _c__162.Visibility = Visibility.Collapsed;
                    _c__217.Visibility = Visibility.Collapsed;
                    _c__215.Visibility = Visibility.Collapsed;
                    _c0078.Visibility = Visibility.Collapsed;
                    _c__160.Visibility = Visibility.Collapsed;
                    _c__237.Visibility = Visibility.Collapsed;
                    _c__213.Visibility = Visibility.Collapsed;
                    _c__34.Visibility = Visibility.Collapsed;
                    _c__138.Visibility = Visibility.Collapsed;
                    _c__87.Visibility = Visibility.Collapsed;
                    _c__200.Visibility = Visibility.Collapsed;
                    _c0288.Visibility = Visibility.Collapsed;
                    _c__116.Visibility = Visibility.Collapsed;
                    _c__65.Visibility = Visibility.Collapsed;
                    _c__185.Visibility = Visibility.Collapsed;
                    _c__5.Visibility = Visibility.Collapsed;
                    _c__96.Visibility = Visibility.Collapsed;
                    _c__42.Visibility = Visibility.Collapsed;
                    _c__168.Visibility = Visibility.Collapsed;
                    _c0066.Visibility = Visibility.Collapsed;
                    _c__148.Visibility = Visibility.Collapsed;
                    _c__225.Visibility = Visibility.Collapsed;
                    _c__205.Visibility = Visibility.Collapsed;
                    _c0294.Visibility = Visibility.Collapsed;
                    _c__125.Visibility = Visibility.Collapsed;
                    _c__74.Visibility = Visibility.Collapsed;
                    _v0264.Visibility = Visibility.Collapsed;
                    _c__12.Visibility = Visibility.Collapsed;
                    _c__106.Visibility = Visibility.Collapsed;
                    _c__50.Visibility = Visibility.Collapsed;
                    _v0069.Visibility = Visibility.Collapsed;
                    _c00e7.Visibility = Visibility.Collapsed;
                    _c__158.Visibility = Visibility.Collapsed;
                    _c__235.Visibility = Visibility.Collapsed;
                    _c__211.Visibility = Visibility.Collapsed;
                    _c02a7.Visibility = Visibility.Collapsed;
                    _c__132.Visibility = Visibility.Collapsed;
                    _c__83.Visibility = Visibility.Collapsed;
                    _c__195.Visibility = Visibility.Collapsed;
                    _c__19.Visibility = Visibility.Collapsed;
                    _c__110.Visibility = Visibility.Collapsed;
                    _c__60.Visibility = Visibility.Collapsed;
                    _v026a.Visibility = Visibility.Collapsed;
                    _c__31.Visibility = Visibility.Collapsed;
                    _c__134.Visibility = Visibility.Collapsed;
                    _c__84.Visibility = Visibility.Collapsed;
                    _c__197.Visibility = Visibility.Collapsed;
                    _c0074.Visibility = Visibility.Collapsed;
                    _c__112.Visibility = Visibility.Collapsed;
                    _c__62.Visibility = Visibility.Collapsed;
                    _v0269.Visibility = Visibility.Collapsed;
                    _c__2.Visibility = Visibility.Collapsed;
                    _c__92.Visibility = Visibility.Collapsed;
                    _c0127.Visibility = Visibility.Collapsed;
                    _c__164.Visibility = Visibility.Collapsed;
                    _c__40.Visibility = Visibility.Collapsed;
                    _c__144.Visibility = Visibility.Collapsed;
                    _c__221.Visibility = Visibility.Collapsed;
                    _c__203.Visibility = Visibility.Collapsed;
                    _c0071.Visibility = Visibility.Collapsed;
                    _c__122.Visibility = Visibility.Collapsed;
                    _c__70.Visibility = Visibility.Collapsed;
                    _v0258.Visibility = Visibility.Collapsed;
                    _c__10.Visibility = Visibility.Collapsed;
                    _c__103.Visibility = Visibility.Collapsed;
                    _c026c.Visibility = Visibility.Collapsed;
                    _c__174.Visibility = Visibility.Collapsed;
                    _c0283.Visibility = Visibility.Collapsed;
                    _c__154.Visibility = Visibility.Collapsed;
                    _c__231.Visibility = Visibility.Collapsed;
                    _c__208.Visibility = Visibility.Collapsed;
                    _c__29.Visibility = Visibility.Collapsed;
                    _c__128.Visibility = Visibility.Collapsed;
                    _c__79.Visibility = Visibility.Collapsed;
                    _c__193.Visibility = Visibility.Collapsed;
                    _c0070.Visibility = Visibility.Collapsed;
                    _c0265.Visibility = Visibility.Collapsed;
                    _c__56.Visibility = Visibility.Collapsed;
                    _v026f.Visibility = Visibility.Collapsed;
                    _c__36.Visibility = Visibility.Collapsed;
                    _c__140.Visibility = Visibility.Collapsed;
                    _c__89.Visibility = Visibility.Collapsed;
                    _v0250.Visibility = Visibility.Collapsed;
                    _c0063.Visibility = Visibility.Collapsed;
                    _c__118.Visibility = Visibility.Collapsed;
                    _c__67.Visibility = Visibility.Collapsed;
                    _c__187.Visibility = Visibility.Collapsed;
                    _c__7.Visibility = Visibility.Collapsed;
                    _c__98.Visibility = Visibility.Collapsed;
                    _c__44.Visibility = Visibility.Collapsed;
                    _c__170.Visibility = Visibility.Collapsed;
                    _c03b8.Visibility = Visibility.Collapsed;
                    _c__150.Visibility = Visibility.Collapsed;
                    _c__227.Visibility = Visibility.Collapsed;
                    _v0061.Visibility = Visibility.Collapsed;
                    _c__26.Visibility = Visibility.Collapsed;
                    _c__126.Visibility = Visibility.Collapsed;
                    _c__76.Visibility = Visibility.Collapsed;
                    _c__190.Visibility = Visibility.Collapsed;
                    _c__13.Visibility = Visibility.Collapsed;
                    _c__107.Visibility = Visibility.Collapsed;
                    _c__52.Visibility = Visibility.Collapsed;
                    _c__179.Visibility = Visibility.Collapsed;
                    _c__33.Visibility = Visibility.Collapsed;
                    _c__136.Visibility = Visibility.Collapsed;
                    _c__86.Visibility = Visibility.Collapsed;
                    _v025b.Visibility = Visibility.Collapsed;
                    _c__21.Visibility = Visibility.Collapsed;
                    _c__114.Visibility = Visibility.Collapsed;
                    _c__63.Visibility = Visibility.Collapsed;
                    _c__183.Visibility = Visibility.Collapsed;
                    _c__3.Visibility = Visibility.Collapsed;
                    _c__94.Visibility = Visibility.Collapsed;
                    _c0068.Visibility = Visibility.Collapsed;
                    _c__166.Visibility = Visibility.Collapsed;
                    _c0278.Visibility = Visibility.Collapsed;
                    _c__146.Visibility = Visibility.Collapsed;
                    _c__223.Visibility = Visibility.Collapsed;
                    _v00e6.Visibility = Visibility.Collapsed;
                    _c__23.Visibility = Visibility.Collapsed;
                    _c__123.Visibility = Visibility.Collapsed;
                    _c__72.Visibility = Visibility.Collapsed;
                    _v0259.Visibility = Visibility.Collapsed;
                    _c__11.Visibility = Visibility.Collapsed;
                    _c__104.Visibility = Visibility.Collapsed;
                    _c__48.Visibility = Visibility.Collapsed;
                    _c__176.Visibility = Visibility.Collapsed;
                    _c0282.Visibility = Visibility.Collapsed;
                    _c__156.Visibility = Visibility.Collapsed;
                    _c__233.Visibility = Visibility.Collapsed;
                    _c__209.Visibility = Visibility.Collapsed;
                    _c024c.Visibility = Visibility.Collapsed;
                    _c__130.Visibility = Visibility.Collapsed;
                    _c__81.Visibility = Visibility.Collapsed;
                    _v025c.Visibility = Visibility.Collapsed;
                    _c__17.Visibility = Visibility.Collapsed;
                    _c0077.Visibility = Visibility.Collapsed;
                    _c__58.Visibility = Visibility.Collapsed;
                    _c__181.Visibility = Visibility.Collapsed;
                    _c__38.Visibility = Visibility.Collapsed;
                    _c__142.Visibility = Visibility.Collapsed;
                    _c__21.Visibility = Visibility.Collapsed;
                    _v028c.Visibility = Visibility.Collapsed;
                    _c006b.Visibility = Visibility.Collapsed;
                    _c__120.Visibility = Visibility.Collapsed;
                    _c__68.Visibility = Visibility.Collapsed;
                    _v0065.Visibility = Visibility.Collapsed;
                    _c__9.Visibility = Visibility.Collapsed;
                    _c__101.Visibility = Visibility.Collapsed;
                    _c__46.Visibility = Visibility.Collapsed;
                    _c__172.Visibility = Visibility.Collapsed;
                    _c0073.Visibility = Visibility.Collapsed;
                    _c__152.Visibility = Visibility.Collapsed;
                    _c__229.Visibility = Visibility.Collapsed;
                    _v0251.Visibility = Visibility.Collapsed;
                    _c____.Visibility = Visibility.Collapsed;
                    _c__127.Visibility = Visibility.Collapsed;
                    _c__77.Visibility = Visibility.Collapsed;
                    _c__191.Visibility = Visibility.Collapsed;
                    _c__15.Visibility = Visibility.Collapsed;
                    _c__109.Visibility = Visibility.Collapsed;
                    _c__54.Visibility = Visibility.Collapsed;
                    _v0268.Visibility = Visibility.Collapsed;
                    _c02a6.Visibility = Visibility.Collapsed;
                    #endregion
                    #region visible
                    _c02a3.Visibility = Visibility.Visible;
                    _c006d.Visibility = Visibility.Visible;
                    _c__91.Visibility = Visibility.Visible;
                    _c0281.Visibility = Visibility.Visible;
                    _c__163.Visibility = Visibility.Visible;
                    _c__218.Visibility = Visibility.Visible;
                    _c__216.Visibility = Visibility.Visible;
                    _c0263.Visibility = Visibility.Visible;
                    _c__161.Visibility = Visibility.Visible;
                    _c__238.Visibility = Visibility.Visible;
                    _c__214.Visibility = Visibility.Visible;
                    _c__35.Visibility = Visibility.Visible;
                    _c__139.Visibility = Visibility.Visible;
                    _c__88.Visibility = Visibility.Visible;
                    _v0153.Visibility = Visibility.Visible;
                    _c0256.Visibility = Visibility.Visible;
                    _c__117.Visibility = Visibility.Visible;
                    _c__66.Visibility = Visibility.Visible;
                    _c__186.Visibility = Visibility.Visible;
                    _c006e.Visibility = Visibility.Visible;
                    _c__97.Visibility = Visibility.Visible;
                    _c__43.Visibility = Visibility.Visible;
                    _c__169.Visibility = Visibility.Visible;
                    _c0076.Visibility = Visibility.Visible;
                    _c__149.Visibility = Visibility.Visible;
                    _c__226.Visibility = Visibility.Visible;
                    _v0276.Visibility = Visibility.Visible;
                    _c__25.Visibility = Visibility.Visible;
                    _c026d.Visibility = Visibility.Visible;
                    _c__75.Visibility = Visibility.Visible;
                    _c__189.Visibility = Visibility.Visible;
                    _c0274.Visibility = Visibility.Visible;
                    _c0279.Visibility = Visibility.Visible;
                    _c__51.Visibility = Visibility.Visible;
                    _c__178.Visibility = Visibility.Visible;
                    _c029d.Visibility = Visibility.Visible;
                    _c__159.Visibility = Visibility.Visible;
                    _c__236.Visibility = Visibility.Visible;
                    _c__212.Visibility = Visibility.Visible;
                    _c02a4.Visibility = Visibility.Visible;
                    _c__133.Visibility = Visibility.Visible;
                    _c2c71.Visibility = Visibility.Visible;
                    _c__196.Visibility = Visibility.Visible;
                    _c__20.Visibility = Visibility.Visible;
                    _c__111.Visibility = Visibility.Visible;
                    _c__61.Visibility = Visibility.Visible;
                    _c__182.Visibility = Visibility.Visible;
                    _c__32.Visibility = Visibility.Visible;
                    _c__135.Visibility = Visibility.Visible;
                    _c__85.Visibility = Visibility.Visible;
                    _c__198.Visibility = Visibility.Visible;
                    _c0064.Visibility = Visibility.Visible;
                    _c__113.Visibility = Visibility.Visible;
                    _c0042.Visibility = Visibility.Visible;
                    _v028f.Visibility = Visibility.Visible;
                    _c0271.Visibility = Visibility.Visible;
                    _c__93.Visibility = Visibility.Visible;
                    _c0295.Visibility = Visibility.Visible;
                    _c__165.Visibility = Visibility.Visible;
                    _c__41.Visibility = Visibility.Visible;
                    _c__145.Visibility = Visibility.Visible;
                    _c__222.Visibility = Visibility.Visible;
                    _v0254.Visibility = Visibility.Visible;
                    _c0047.Visibility = Visibility.Visible;
                    _c006c.Visibility = Visibility.Visible;
                    _c__71.Visibility = Visibility.Visible;
                    _v00f8.Visibility = Visibility.Visible;
                    _c0272.Visibility = Visibility.Visible;
                    _c028b.Visibility = Visibility.Visible;
                    _c026e.Visibility = Visibility.Visible;
                    _c__175.Visibility = Visibility.Visible;
                    _c0292.Visibility = Visibility.Visible;
                    _c__155.Visibility = Visibility.Visible;
                    _c__232.Visibility = Visibility.Visible;
                    _v0252.Visibility = Visibility.Visible;
                    _c__30.Visibility = Visibility.Visible;
                    _c__129.Visibility = Visibility.Visible;
                    _c__80.Visibility = Visibility.Visible;
                    _c__194.Visibility = Visibility.Visible;
                    _c0062.Visibility = Visibility.Visible;
                    _c006a.Visibility = Visibility.Visible;
                    _c__57.Visibility = Visibility.Visible;
                    _c__180.Visibility = Visibility.Visible;
                    _c__37.Visibility = Visibility.Visible;
                    _c__141.Visibility = Visibility.Visible;
                    _c027d.Visibility = Visibility.Visible;
                    _c__201.Visibility = Visibility.Visible;
                    _c025f.Visibility = Visibility.Visible;
                    _c__119.Visibility = Visibility.Visible;
                    _c0072.Visibility = Visibility.Visible;
                    _v028a.Visibility = Visibility.Visible;
                    _c__8.Visibility = Visibility.Visible;
                    _c__99.Visibility = Visibility.Visible;
                    _c__45.Visibility = Visibility.Visible;
                    _c__171.Visibility = Visibility.Visible;
                    _c00f0.Visibility = Visibility.Visible;
                    _c__151.Visibility = Visibility.Visible;
                    _c__228.Visibility = Visibility.Visible;
                    _c__206.Visibility = Visibility.Visible;
                    _c__27.Visibility = Visibility.Visible;
                    _c028e.Visibility = Visibility.Visible;
                    _c0052.Visibility = Visibility.Visible;
                    _v006f.Visibility = Visibility.Visible;
                    _c__14.Visibility = Visibility.Visible;
                    _c__108.Visibility = Visibility.Visible;
                    _c__53.Visibility = Visibility.Visible;
                    _v0079.Visibility = Visibility.Visible;
                    _c__33_2.Visibility = Visibility.Visible;
                    _c__137.Visibility = Visibility.Visible;
                    _c027e.Visibility = Visibility.Visible;
                    _c__199.Visibility = Visibility.Visible;
                    _c__22.Visibility = Visibility.Visible;
                    _c__115.Visibility = Visibility.Visible;
                    _c__64.Visibility = Visibility.Visible;
                    _c__184.Visibility = Visibility.Visible;
                    _c__4.Visibility = Visibility.Visible;
                    _c__95.Visibility = Visibility.Visible;
                    _c0266.Visibility = Visibility.Visible;
                    _c__167.Visibility = Visibility.Visible;
                    _c03b2.Visibility = Visibility.Visible;
                    _c__147.Visibility = Visibility.Visible;
                    _c__224.Visibility = Visibility.Visible;
                    _c__204.Visibility = Visibility.Visible;
                    _c__24.Visibility = Visibility.Visible;
                    _c__124.Visibility = Visibility.Visible;
                    _c__73.Visibility = Visibility.Visible;
                    _v0275.Visibility = Visibility.Visible;
                    _c014b.Visibility = Visibility.Visible;
                    _c__105.Visibility = Visibility.Visible;
                    _c__49.Visibility = Visibility.Visible;
                    _c__177.Visibility = Visibility.Visible;
                    _c0290.Visibility = Visibility.Visible;
                    _c__157.Visibility = Visibility.Visible;
                    _c__234.Visibility = Visibility.Visible;
                    _c__210.Visibility = Visibility.Visible;
                    //c02a.Visibility = Visibility.Visible;
                    _c__131.Visibility = Visibility.Visible;
                    _c__82.Visibility = Visibility.Visible;
                    _v025e.Visibility = Visibility.Visible;
                    _c__18.Visibility = Visibility.Visible;
                    _c0270.Visibility = Visibility.Visible;
                    _c__59.Visibility = Visibility.Visible;
                    _v0075.Visibility = Visibility.Visible;
                    _c__39.Visibility = Visibility.Visible;
                    _c__143.Visibility = Visibility.Visible;
                    _c__220.Visibility = Visibility.Visible;
                    _c__202.Visibility = Visibility.Visible;
                    _c0261.Visibility = Visibility.Visible;
                    _c__121.Visibility = Visibility.Visible;
                    _c__69.Visibility = Visibility.Visible;
                    _c__188.Visibility = Visibility.Visible;
                    _c0273.Visibility = Visibility.Visible;
                    _c__102.Visibility = Visibility.Visible;
                    _c__47.Visibility = Visibility.Visible;
                    _c__173.Visibility = Visibility.Visible;
                    _c007a.Visibility = Visibility.Visible;
                    _c__230.Visibility = Visibility.Visible;
                    _c__207.Visibility = Visibility.Visible;
                    _c__28.Visibility = Visibility.Visible;
                    _c024c.Visibility = Visibility.Visible;
                    _c__78.Visibility = Visibility.Visible;
                    _c__192.Visibility = Visibility.Visible;
                    _c__16.Visibility = Visibility.Visible;
                    _c027b.Visibility = Visibility.Visible;
                    _c__55.Visibility = Visibility.Visible;
                    _v0289.Visibility = Visibility.Visible;
                    _c__153.Visibility = Visibility.Visible;
                    #endregion
                }
            }
                
            else
            {
                if (vbBottoni.Visibility == Visibility.Visible)
                {
                    #region enable
                    c__219.IsEnabled = true;
                    c__15.IsEnabled = true;
                    c__1.IsEnabled = true;
                    c02a6.IsEnabled = true;
                    c__90.IsEnabled = true;
                    c03c7.IsEnabled = true;
                    c__162.IsEnabled = true;
                    c__217.IsEnabled = true;
                    c__215.IsEnabled = true;
                    c0078.IsEnabled = true;
                    c__160.IsEnabled = true;
                    c__237.IsEnabled = true;
                    c__213.IsEnabled = true;
                    c__34.IsEnabled = true;
                    c__138.IsEnabled = true;
                    c__87.IsEnabled = true;
                    c__200.IsEnabled = true;
                    c0288.IsEnabled = true;
                    c__116.IsEnabled = true;
                    c__65.IsEnabled = true;
                    c__185.IsEnabled = true;
                    c__5.IsEnabled = true;
                    c__96.IsEnabled = true;
                    c__42.IsEnabled = true;
                    c__168.IsEnabled = true;
                    c0066.IsEnabled = true;
                    c__148.IsEnabled = true;
                    c__225.IsEnabled = true;
                    c__205.IsEnabled = true;
                    c0294.IsEnabled = true;
                    c__125.IsEnabled = true;
                    c__74.IsEnabled = true;
                    v0264.IsEnabled = true;
                    c__12.IsEnabled = true;
                    c__106.IsEnabled = true;
                    c__50.IsEnabled = true;
                    v0069.IsEnabled = true;
                    c00e7.IsEnabled = true;
                    c__158.IsEnabled = true;
                    c__235.IsEnabled = true;
                    c__211.IsEnabled = true;
                    c02a7.IsEnabled = true;
                    c__132.IsEnabled = true;
                    c__83.IsEnabled = true;
                    c__195.IsEnabled = true;
                    c__19.IsEnabled = true;
                    c__110.IsEnabled = true;
                    c__60.IsEnabled = true;
                    v026a.IsEnabled = true;
                    c__31.IsEnabled = true;
                    c__134.IsEnabled = true;
                    c__84.IsEnabled = true;
                    c__197.IsEnabled = true;
                    c0074.IsEnabled = true;
                    c__112.IsEnabled = true;
                    c__62.IsEnabled = true;
                    v0269.IsEnabled = true;
                    c__2.IsEnabled = true;
                    c__92.IsEnabled = true;
                    c0127.IsEnabled = true;
                    c__164.IsEnabled = true;
                    c__40.IsEnabled = true;
                    c__144.IsEnabled = true;
                    c__221.IsEnabled = true;
                    c__203.IsEnabled = true;
                    c0071.IsEnabled = true;
                    c__122.IsEnabled = true;
                    c__70.IsEnabled = true;
                    v0258.IsEnabled = true;
                    c__10.IsEnabled = true;
                    c__103.IsEnabled = true;
                    c026c.IsEnabled = true;
                    c__174.IsEnabled = true;
                    c0283.IsEnabled = true;
                    c__154.IsEnabled = true;
                    c__231.IsEnabled = true;
                    c__208.IsEnabled = true;
                    c__29.IsEnabled = true;
                    c__128.IsEnabled = true;
                    c__79.IsEnabled = true;
                    c__193.IsEnabled = true;
                    c0070.IsEnabled = true;
                    c0265.IsEnabled = true;
                    c__56.IsEnabled = true;
                    v026f.IsEnabled = true;
                    c__36.IsEnabled = true;
                    c__140.IsEnabled = true;
                    c__89.IsEnabled = true;
                    v0250.IsEnabled = true;
                    c0063.IsEnabled = true;
                    c__118.IsEnabled = true;
                    c__67.IsEnabled = true;
                    c__187.IsEnabled = true;
                    c__7.IsEnabled = true;
                    c__98.IsEnabled = true;
                    c__44.IsEnabled = true;
                    c__170.IsEnabled = true;
                    c03b8.IsEnabled = true;
                    c__150.IsEnabled = true;
                    c__227.IsEnabled = true;
                    v0061.IsEnabled = true;
                    c__26.IsEnabled = true;
                    c__126.IsEnabled = true;
                    c__76.IsEnabled = true;
                    c__190.IsEnabled = true;
                    c__13.IsEnabled = true;
                    c__107.IsEnabled = true;
                    c__52.IsEnabled = true;
                    c__179.IsEnabled = true;
                    c__33.IsEnabled = true;
                    c__136.IsEnabled = true;
                    c__86.IsEnabled = true;
                    v025b.IsEnabled = true;
                    c__21.IsEnabled = true;
                    c__114.IsEnabled = true;
                    c__63.IsEnabled = true;
                    c__183.IsEnabled = true;
                    c__3.IsEnabled = true;
                    c__94.IsEnabled = true;
                    c0068.IsEnabled = true;
                    c__166.IsEnabled = true;
                    c0278.IsEnabled = true;
                    c__146.IsEnabled = true;
                    c__223.IsEnabled = true;
                    v00e6.IsEnabled = true;
                    c__23.IsEnabled = true;
                    c__123.IsEnabled = true;
                    c__72.IsEnabled = true;
                    v0259.IsEnabled = true;
                    c__11.IsEnabled = true;
                    c__104.IsEnabled = true;
                    c__48.IsEnabled = true;
                    c__176.IsEnabled = true;
                    c0282.IsEnabled = true;
                    c__156.IsEnabled = true;
                    c__233.IsEnabled = true;
                    c__209.IsEnabled = true;
                    c024c.IsEnabled = true;
                    c__130.IsEnabled = true;
                    c__81.IsEnabled = true;
                    v025c.IsEnabled = true;
                    c__17.IsEnabled = true;
                    c0077.IsEnabled = true;
                    c__58.IsEnabled = true;
                    c__181.IsEnabled = true;
                    c__38.IsEnabled = true;
                    c__142.IsEnabled = true;
                    c__21.IsEnabled = true;
                    v028c.IsEnabled = true;
                    c006b.IsEnabled = true;
                    c__120.IsEnabled = true;
                    c__68.IsEnabled = true;
                    v0065.IsEnabled = true;
                    c__9.IsEnabled = true;
                    c__101.IsEnabled = true;
                    c__46.IsEnabled = true;
                    c__172.IsEnabled = true;
                    c0073.IsEnabled = true;
                    c__152.IsEnabled = true;
                    c__229.IsEnabled = true;
                    v0251.IsEnabled = true;
                    c____.IsEnabled = true;
                    c__127.IsEnabled = true;
                    c__77.IsEnabled = true;
                    c__191.IsEnabled = true;
                    c__15.IsEnabled = true;
                    c__109.IsEnabled = true;
                    c__54.IsEnabled = true;
                    v0268.IsEnabled = true;
                    #endregion
                    #region disable
                    c__153.IsEnabled = false;
                    c02a3.IsEnabled = false;
                    c006d.IsEnabled = false;
                    c__91.IsEnabled = false;
                    c0281.IsEnabled = false;
                    c__163.IsEnabled = false;
                    c__218.IsEnabled = false;
                    c__216.IsEnabled = false;
                    c0263.IsEnabled = false;
                    c__161.IsEnabled = false;
                    c__238.IsEnabled = false;
                    c__214.IsEnabled = false;
                    c__35.IsEnabled = false;
                    c__139.IsEnabled = false;
                    c__88.IsEnabled = false;
                    v0153.IsEnabled = false;
                    c0256.IsEnabled = false;
                    c__117.IsEnabled = false;
                    c__66.IsEnabled = false;
                    c__186.IsEnabled = false;
                    c006e.IsEnabled = false;
                    c__97.IsEnabled = false;
                    c__43.IsEnabled = false;
                    c__169.IsEnabled = false;
                    c0076.IsEnabled = false;
                    c__149.IsEnabled = false;
                    c__226.IsEnabled = false;
                    v0276.IsEnabled = false;
                    c__25.IsEnabled = false;
                    c026d.IsEnabled = false;
                    c__75.IsEnabled = false;
                    c__189.IsEnabled = false;
                    c0274.IsEnabled = false;
                    c0279.IsEnabled = false;
                    c__51.IsEnabled = false;
                    c__178.IsEnabled = false;
                    c029d.IsEnabled = false;
                    c__159.IsEnabled = false;
                    c__236.IsEnabled = false;
                    c__212.IsEnabled = false;
                    c02a4.IsEnabled = false;
                    c__133.IsEnabled = false;
                    c2c71.IsEnabled = false;
                    c__196.IsEnabled = false;
                    c__20.IsEnabled = false;
                    c__111.IsEnabled = false;
                    c__61.IsEnabled = false;
                    c__182.IsEnabled = false;
                    c__32.IsEnabled = false;
                    c__135.IsEnabled = false;
                    c__85.IsEnabled = false;
                    c__198.IsEnabled = false;
                    c0064.IsEnabled = false;
                    c__113.IsEnabled = false;
                    c0042.IsEnabled = false;
                    v028f.IsEnabled = false;
                    c0271.IsEnabled = false;
                    c__93.IsEnabled = false;
                    c0295.IsEnabled = false;
                    c__165.IsEnabled = false;
                    c__41.IsEnabled = false;
                    c__145.IsEnabled = false;
                    c__222.IsEnabled = false;
                    v0254.IsEnabled = false;
                    c0047.IsEnabled = false;
                    c006c.IsEnabled = false;
                    c__71.IsEnabled = false;
                    v00f8.IsEnabled = false;
                    c0272.IsEnabled = false;
                    c028b.IsEnabled = false;
                    c026e.IsEnabled = false;
                    c__175.IsEnabled = false;
                    c0292.IsEnabled = false;
                    c__155.IsEnabled = false;
                    c__232.IsEnabled = false;
                    v0252.IsEnabled = false;
                    c__30.IsEnabled = false;
                    c__129.IsEnabled = false;
                    c__80.IsEnabled = false;
                    c__194.IsEnabled = false;
                    c0062.IsEnabled = false;
                    c006a.IsEnabled = false;
                    c__57.IsEnabled = false;
                    c__180.IsEnabled = false;
                    c__37.IsEnabled = false;
                    c__141.IsEnabled = false;
                    c027d.IsEnabled = false;
                    c__201.IsEnabled = false;
                    c025f.IsEnabled = false;
                    c__119.IsEnabled = false;
                    c0072.IsEnabled = false;
                    v028a.IsEnabled = false;
                    c__8.IsEnabled = false;
                    c__99.IsEnabled = false;
                    c__45.IsEnabled = false;
                    c__171.IsEnabled = false;
                    c00f0.IsEnabled = false;
                    c__151.IsEnabled = false;
                    c__228.IsEnabled = false;
                    c__206.IsEnabled = false;
                    c__27.IsEnabled = false;
                    c028e.IsEnabled = false;
                    c0052.IsEnabled = false;
                    v006f.IsEnabled = false;
                    c__14.IsEnabled = false;
                    c__108.IsEnabled = false;
                    c__53.IsEnabled = false;
                    v0079.IsEnabled = false;
                    c__33_2.IsEnabled = false;
                    c__137.IsEnabled = false;
                    c027e.IsEnabled = false;
                    c__199.IsEnabled = false;
                    c__22.IsEnabled = false;
                    c__115.IsEnabled = false;
                    c__64.IsEnabled = false;
                    c__184.IsEnabled = false;
                    c__4.IsEnabled = false;
                    c__95.IsEnabled = false;
                    c0266.IsEnabled = false;
                    c__167.IsEnabled = false;
                    c03b2.IsEnabled = false;
                    c__147.IsEnabled = false;
                    c__224.IsEnabled = false;
                    c__204.IsEnabled = false;
                    c__24.IsEnabled = false;
                    c__124.IsEnabled = false;
                    c__73.IsEnabled = false;
                    v0275.IsEnabled = false;
                    c014b.IsEnabled = false;
                    c__105.IsEnabled = false;
                    c__49.IsEnabled = false;
                    c__177.IsEnabled = false;
                    c0290.IsEnabled = false;
                    c__157.IsEnabled = false;
                    c__234.IsEnabled = false;
                    c__210.IsEnabled = false;
                    c__131.IsEnabled = false;
                    c__82.IsEnabled = false;
                    v025e.IsEnabled = false;
                    c__18.IsEnabled = false;
                    c0270.IsEnabled = false;
                    c__59.IsEnabled = false;
                    v0075.IsEnabled = false;
                    c__39.IsEnabled = false;
                    c__143.IsEnabled = false;
                    c__220.IsEnabled = false;
                    c__202.IsEnabled = false;
                    c0261.IsEnabled = false;
                    c__121.IsEnabled = false;
                    c__69.IsEnabled = false;
                    c__188.IsEnabled = false;
                    c0273.IsEnabled = false;
                    c__102.IsEnabled = false;
                    c__47.IsEnabled = false;
                    c__173.IsEnabled = false;
                    c007a.IsEnabled = false;
                    c__230.IsEnabled = false;
                    c__207.IsEnabled = false;
                    c__28.IsEnabled = false;
                    c024c.IsEnabled = false;
                    c__78.IsEnabled = false;
                    c__192.IsEnabled = false;
                    c__16.IsEnabled = false;
                    c027b.IsEnabled = false;
                    c__55.IsEnabled = false;
                    v0289.IsEnabled = false;
                    #endregion
                }
                else
                {
                    #region visible
                    _c__219.Visibility = Visibility.Visible;
                    _c__1.Visibility = Visibility.Visible;
                    _c__90.Visibility = Visibility.Visible;
                    _c03c7.Visibility = Visibility.Visible;
                    _c__162.Visibility = Visibility.Visible;
                    _c__217.Visibility = Visibility.Visible;
                    _c__215.Visibility = Visibility.Visible;
                    _c0078.Visibility = Visibility.Visible;
                    _c__160.Visibility = Visibility.Visible;
                    _c__237.Visibility = Visibility.Visible;
                    _c__213.Visibility = Visibility.Visible;
                    _c__34.Visibility = Visibility.Visible;
                    _c__138.Visibility = Visibility.Visible;
                    _c__87.Visibility = Visibility.Visible;
                    _c__200.Visibility = Visibility.Visible;
                    _c0288.Visibility = Visibility.Visible;
                    _c__116.Visibility = Visibility.Visible;
                    _c__65.Visibility = Visibility.Visible;
                    _c__185.Visibility = Visibility.Visible;
                    _c__5.Visibility = Visibility.Visible;
                    _c__96.Visibility = Visibility.Visible;
                    _c__42.Visibility = Visibility.Visible;
                    _c__168.Visibility = Visibility.Visible;
                    _c0066.Visibility = Visibility.Visible;
                    _c__148.Visibility = Visibility.Visible;
                    _c__225.Visibility = Visibility.Visible;
                    _c__205.Visibility = Visibility.Visible;
                    _c0294.Visibility = Visibility.Visible;
                    _c__125.Visibility = Visibility.Visible;
                    _c__74.Visibility = Visibility.Visible;
                    _v0264.Visibility = Visibility.Visible;
                    _c__12.Visibility = Visibility.Visible;
                    _c__106.Visibility = Visibility.Visible;
                    _c__50.Visibility = Visibility.Visible;
                    _v0069.Visibility = Visibility.Visible;
                    _c00e7.Visibility = Visibility.Visible;
                    _c__158.Visibility = Visibility.Visible;
                    _c__235.Visibility = Visibility.Visible;
                    _c__211.Visibility = Visibility.Visible;
                    _c02a7.Visibility = Visibility.Visible;
                    _c__132.Visibility = Visibility.Visible;
                    _c__83.Visibility = Visibility.Visible;
                    _c__195.Visibility = Visibility.Visible;
                    _c__19.Visibility = Visibility.Visible;
                    _c__110.Visibility = Visibility.Visible;
                    _c__60.Visibility = Visibility.Visible;
                    _v026a.Visibility = Visibility.Visible;
                    _c__31.Visibility = Visibility.Visible;
                    _c__134.Visibility = Visibility.Visible;
                    _c__84.Visibility = Visibility.Visible;
                    _c__197.Visibility = Visibility.Visible;
                    _c0074.Visibility = Visibility.Visible;
                    _c__112.Visibility = Visibility.Visible;
                    _c__62.Visibility = Visibility.Visible;
                    _v0269.Visibility = Visibility.Visible;
                    _c__2.Visibility = Visibility.Visible;
                    _c__92.Visibility = Visibility.Visible;
                    _c0127.Visibility = Visibility.Visible;
                    _c__164.Visibility = Visibility.Visible;
                    _c__40.Visibility = Visibility.Visible;
                    _c__144.Visibility = Visibility.Visible;
                    _c__221.Visibility = Visibility.Visible;
                    _c__203.Visibility = Visibility.Visible;
                    _c0071.Visibility = Visibility.Visible;
                    _c__122.Visibility = Visibility.Visible;
                    _c__70.Visibility = Visibility.Visible;
                    _v0258.Visibility = Visibility.Visible;
                    _c__10.Visibility = Visibility.Visible;
                    _c__103.Visibility = Visibility.Visible;
                    _c026c.Visibility = Visibility.Visible;
                    _c__174.Visibility = Visibility.Visible;
                    _c0283.Visibility = Visibility.Visible;
                    _c__154.Visibility = Visibility.Visible;
                    _c__231.Visibility = Visibility.Visible;
                    _c__208.Visibility = Visibility.Visible;
                    _c__29.Visibility = Visibility.Visible;
                    _c__128.Visibility = Visibility.Visible;
                    _c__79.Visibility = Visibility.Visible;
                    _c__193.Visibility = Visibility.Visible;
                    _c0070.Visibility = Visibility.Visible;
                    _c0265.Visibility = Visibility.Visible;
                    _c__56.Visibility = Visibility.Visible;
                    _v026f.Visibility = Visibility.Visible;
                    _c__36.Visibility = Visibility.Visible;
                    _c__140.Visibility = Visibility.Visible;
                    _c__89.Visibility = Visibility.Visible;
                    _v0250.Visibility = Visibility.Visible;
                    _c0063.Visibility = Visibility.Visible;
                    _c__118.Visibility = Visibility.Visible;
                    _c__67.Visibility = Visibility.Visible;
                    _c__187.Visibility = Visibility.Visible;
                    _c__7.Visibility = Visibility.Visible;
                    _c__98.Visibility = Visibility.Visible;
                    _c__44.Visibility = Visibility.Visible;
                    _c__170.Visibility = Visibility.Visible;
                    _c03b8.Visibility = Visibility.Visible;
                    _c__150.Visibility = Visibility.Visible;
                    _c__227.Visibility = Visibility.Visible;
                    _v0061.Visibility = Visibility.Visible;
                    _c__26.Visibility = Visibility.Visible;
                    _c__126.Visibility = Visibility.Visible;
                    _c__76.Visibility = Visibility.Visible;
                    _c__190.Visibility = Visibility.Visible;
                    _c__13.Visibility = Visibility.Visible;
                    _c__107.Visibility = Visibility.Visible;
                    _c__52.Visibility = Visibility.Visible;
                    _c__179.Visibility = Visibility.Visible;
                    _c__33.Visibility = Visibility.Visible;
                    _c__136.Visibility = Visibility.Visible;
                    _c__86.Visibility = Visibility.Visible;
                    _v025b.Visibility = Visibility.Visible;
                    _c__21.Visibility = Visibility.Visible;
                    _c__114.Visibility = Visibility.Visible;
                    _c__63.Visibility = Visibility.Visible;
                    _c__183.Visibility = Visibility.Visible;
                    _c__3.Visibility = Visibility.Visible;
                    _c__94.Visibility = Visibility.Visible;
                    _c0068.Visibility = Visibility.Visible;
                    _c__166.Visibility = Visibility.Visible;
                    _c0278.Visibility = Visibility.Visible;
                    _c__146.Visibility = Visibility.Visible;
                    _c__223.Visibility = Visibility.Visible;
                    _v00e6.Visibility = Visibility.Visible;
                    _c__23.Visibility = Visibility.Visible;
                    _c__123.Visibility = Visibility.Visible;
                    _c__72.Visibility = Visibility.Visible;
                    _v0259.Visibility = Visibility.Visible;
                    _c__11.Visibility = Visibility.Visible;
                    _c__104.Visibility = Visibility.Visible;
                    _c__48.Visibility = Visibility.Visible;
                    _c__176.Visibility = Visibility.Visible;
                    _c0282.Visibility = Visibility.Visible;
                    _c__156.Visibility = Visibility.Visible;
                    _c__233.Visibility = Visibility.Visible;
                    _c__209.Visibility = Visibility.Visible;
                    _c024c.Visibility = Visibility.Visible;
                    _c__130.Visibility = Visibility.Visible;
                    _c__81.Visibility = Visibility.Visible;
                    _v025c.Visibility = Visibility.Visible;
                    _c__17.Visibility = Visibility.Visible;
                    _c0077.Visibility = Visibility.Visible;
                    _c__58.Visibility = Visibility.Visible;
                    _c__181.Visibility = Visibility.Visible;
                    _c__38.Visibility = Visibility.Visible;
                    _c__142.Visibility = Visibility.Visible;
                    _c__21.Visibility = Visibility.Visible;
                    _v028c.Visibility = Visibility.Visible;
                    _c006b.Visibility = Visibility.Visible;
                    _c__120.Visibility = Visibility.Visible;
                    _c__68.Visibility = Visibility.Visible;
                    _v0065.Visibility = Visibility.Visible;
                    _c__9.Visibility = Visibility.Visible;
                    _c__101.Visibility = Visibility.Visible;
                    _c__46.Visibility = Visibility.Visible;
                    _c__172.Visibility = Visibility.Visible;
                    _c0073.Visibility = Visibility.Visible;
                    _c__152.Visibility = Visibility.Visible;
                    _c__229.Visibility = Visibility.Visible;
                    _v0251.Visibility = Visibility.Visible;
                    _c____.Visibility = Visibility.Visible;
                    _c__127.Visibility = Visibility.Visible;
                    _c__77.Visibility = Visibility.Visible;
                    _c__191.Visibility = Visibility.Visible;
                    _c__15.Visibility = Visibility.Visible;
                    _c__109.Visibility = Visibility.Visible;
                    _c__54.Visibility = Visibility.Visible;
                    _v0268.Visibility = Visibility.Visible;
                    _c02a6.Visibility = Visibility.Visible;
                    #endregion
                    #region invisible
                    _c02a3.Visibility = Visibility.Collapsed;
                    _c006d.Visibility = Visibility.Collapsed;
                    _c__91.Visibility = Visibility.Collapsed;
                    _c0281.Visibility = Visibility.Collapsed;
                    _c__163.Visibility = Visibility.Collapsed;
                    _c__218.Visibility = Visibility.Collapsed;
                    _c__216.Visibility = Visibility.Collapsed;
                    _c0263.Visibility = Visibility.Collapsed;
                    _c__161.Visibility = Visibility.Collapsed;
                    _c__238.Visibility = Visibility.Collapsed;
                    _c__214.Visibility = Visibility.Collapsed;
                    _c__35.Visibility = Visibility.Collapsed;
                    _c__139.Visibility = Visibility.Collapsed;
                    _c__88.Visibility = Visibility.Collapsed;
                    _v0153.Visibility = Visibility.Collapsed;
                    _c0256.Visibility = Visibility.Collapsed;
                    _c__117.Visibility = Visibility.Collapsed;
                    _c__66.Visibility = Visibility.Collapsed;
                    _c__186.Visibility = Visibility.Collapsed;
                    _c006e.Visibility = Visibility.Collapsed;
                    _c__97.Visibility = Visibility.Collapsed;
                    _c__43.Visibility = Visibility.Collapsed;
                    _c__169.Visibility = Visibility.Collapsed;
                    _c0076.Visibility = Visibility.Collapsed;
                    _c__149.Visibility = Visibility.Collapsed;
                    _c__226.Visibility = Visibility.Collapsed;
                    _v0276.Visibility = Visibility.Collapsed;
                    _c__25.Visibility = Visibility.Collapsed;
                    _c026d.Visibility = Visibility.Collapsed;
                    _c__75.Visibility = Visibility.Collapsed;
                    _c__189.Visibility = Visibility.Collapsed;
                    _c0274.Visibility = Visibility.Collapsed;
                    _c0279.Visibility = Visibility.Collapsed;
                    _c__51.Visibility = Visibility.Collapsed;
                    _c__178.Visibility = Visibility.Collapsed;
                    _c029d.Visibility = Visibility.Collapsed;
                    _c__159.Visibility = Visibility.Collapsed;
                    _c__236.Visibility = Visibility.Collapsed;
                    _c__212.Visibility = Visibility.Collapsed;
                    _c02a4.Visibility = Visibility.Collapsed;
                    _c__133.Visibility = Visibility.Collapsed;
                    _c2c71.Visibility = Visibility.Collapsed;
                    _c__196.Visibility = Visibility.Collapsed;
                    _c__20.Visibility = Visibility.Collapsed;
                    _c__111.Visibility = Visibility.Collapsed;
                    _c__61.Visibility = Visibility.Collapsed;
                    _c__182.Visibility = Visibility.Collapsed;
                    _c__32.Visibility = Visibility.Collapsed;
                    _c__135.Visibility = Visibility.Collapsed;
                    _c__85.Visibility = Visibility.Collapsed;
                    _c__198.Visibility = Visibility.Collapsed;
                    _c0064.Visibility = Visibility.Collapsed;
                    _c__113.Visibility = Visibility.Collapsed;
                    _c0042.Visibility = Visibility.Collapsed;
                    _v028f.Visibility = Visibility.Collapsed;
                    _c0271.Visibility = Visibility.Collapsed;
                    _c__93.Visibility = Visibility.Collapsed;
                    _c0295.Visibility = Visibility.Collapsed;
                    _c__165.Visibility = Visibility.Collapsed;
                    _c__41.Visibility = Visibility.Collapsed;
                    _c__145.Visibility = Visibility.Collapsed;
                    _c__222.Visibility = Visibility.Collapsed;
                    _v0254.Visibility = Visibility.Collapsed;
                    _c0047.Visibility = Visibility.Collapsed;
                    _c006c.Visibility = Visibility.Collapsed;
                    _c__71.Visibility = Visibility.Collapsed;
                    _v00f8.Visibility = Visibility.Collapsed;
                    _c0272.Visibility = Visibility.Collapsed;
                    _c028b.Visibility = Visibility.Collapsed;
                    _c026e.Visibility = Visibility.Collapsed;
                    _c__175.Visibility = Visibility.Collapsed;
                    _c0292.Visibility = Visibility.Collapsed;
                    _c__155.Visibility = Visibility.Collapsed;
                    _c__232.Visibility = Visibility.Collapsed;
                    _v0252.Visibility = Visibility.Collapsed;
                    _c__30.Visibility = Visibility.Collapsed;
                    _c__129.Visibility = Visibility.Collapsed;
                    _c__80.Visibility = Visibility.Collapsed;
                    _c__194.Visibility = Visibility.Collapsed;
                    _c0062.Visibility = Visibility.Collapsed;
                    _c006a.Visibility = Visibility.Collapsed;
                    _c__57.Visibility = Visibility.Collapsed;
                    _c__180.Visibility = Visibility.Collapsed;
                    _c__37.Visibility = Visibility.Collapsed;
                    _c__141.Visibility = Visibility.Collapsed;
                    _c027d.Visibility = Visibility.Collapsed;
                    _c__201.Visibility = Visibility.Collapsed;
                    _c025f.Visibility = Visibility.Collapsed;
                    _c__119.Visibility = Visibility.Collapsed;
                    _c0072.Visibility = Visibility.Collapsed;
                    _v028a.Visibility = Visibility.Collapsed;
                    _c__8.Visibility = Visibility.Collapsed;
                    _c__99.Visibility = Visibility.Collapsed;
                    _c__45.Visibility = Visibility.Collapsed;
                    _c__171.Visibility = Visibility.Collapsed;
                    _c00f0.Visibility = Visibility.Collapsed;
                    _c__151.Visibility = Visibility.Collapsed;
                    _c__228.Visibility = Visibility.Collapsed;
                    _c__206.Visibility = Visibility.Collapsed;
                    _c__27.Visibility = Visibility.Collapsed;
                    _c028e.Visibility = Visibility.Collapsed;
                    _c0052.Visibility = Visibility.Collapsed;
                    _v006f.Visibility = Visibility.Collapsed;
                    _c__14.Visibility = Visibility.Collapsed;
                    _c__108.Visibility = Visibility.Collapsed;
                    _c__53.Visibility = Visibility.Collapsed;
                    _v0079.Visibility = Visibility.Collapsed;
                    _c__33_2.Visibility = Visibility.Collapsed;
                    _c__137.Visibility = Visibility.Collapsed;
                    _c027e.Visibility = Visibility.Collapsed;
                    _c__199.Visibility = Visibility.Collapsed;
                    _c__22.Visibility = Visibility.Collapsed;
                    _c__115.Visibility = Visibility.Collapsed;
                    _c__64.Visibility = Visibility.Collapsed;
                    _c__184.Visibility = Visibility.Collapsed;
                    _c__4.Visibility = Visibility.Collapsed;
                    _c__95.Visibility = Visibility.Collapsed;
                    _c0266.Visibility = Visibility.Collapsed;
                    _c__167.Visibility = Visibility.Collapsed;
                    _c03b2.Visibility = Visibility.Collapsed;
                    _c__147.Visibility = Visibility.Collapsed;
                    _c__224.Visibility = Visibility.Collapsed;
                    _c__204.Visibility = Visibility.Collapsed;
                    _c__24.Visibility = Visibility.Collapsed;
                    _c__124.Visibility = Visibility.Collapsed;
                    _c__73.Visibility = Visibility.Collapsed;
                    _v0275.Visibility = Visibility.Collapsed;
                    _c014b.Visibility = Visibility.Collapsed;
                    _c__105.Visibility = Visibility.Collapsed;
                    _c__49.Visibility = Visibility.Collapsed;
                    _c__177.Visibility = Visibility.Collapsed;
                    _c0290.Visibility = Visibility.Collapsed;
                    _c__157.Visibility = Visibility.Collapsed;
                    _c__234.Visibility = Visibility.Collapsed;
                    _c__210.Visibility = Visibility.Collapsed;
                    //c02a.Visibility = Visibility.Collapsed;
                    _c__131.Visibility = Visibility.Collapsed;
                    _c__82.Visibility = Visibility.Collapsed;
                    _v025e.Visibility = Visibility.Collapsed;
                    _c__18.Visibility = Visibility.Collapsed;
                    _c0270.Visibility = Visibility.Collapsed;
                    _c__59.Visibility = Visibility.Collapsed;
                    _v0075.Visibility = Visibility.Collapsed;
                    _c__39.Visibility = Visibility.Collapsed;
                    _c__143.Visibility = Visibility.Collapsed;
                    _c__220.Visibility = Visibility.Collapsed;
                    _c__202.Visibility = Visibility.Collapsed;
                    _c0261.Visibility = Visibility.Collapsed;
                    _c__121.Visibility = Visibility.Collapsed;
                    _c__69.Visibility = Visibility.Collapsed;
                    _c__188.Visibility = Visibility.Collapsed;
                    _c0273.Visibility = Visibility.Collapsed;
                    _c__102.Visibility = Visibility.Collapsed;
                    _c__47.Visibility = Visibility.Collapsed;
                    _c__173.Visibility = Visibility.Collapsed;
                    _c007a.Visibility = Visibility.Collapsed;
                    _c__230.Visibility = Visibility.Collapsed;
                    _c__207.Visibility = Visibility.Collapsed;
                    _c__28.Visibility = Visibility.Collapsed;
                    _c024c.Visibility = Visibility.Collapsed;
                    _c__78.Visibility = Visibility.Collapsed;
                    _c__192.Visibility = Visibility.Collapsed;
                    _c__16.Visibility = Visibility.Collapsed;
                    _c027b.Visibility = Visibility.Collapsed;
                    _c__55.Visibility = Visibility.Collapsed;
                    _v0289.Visibility = Visibility.Collapsed;
                    _c__153.Visibility = Visibility.Collapsed;
                    #endregion
                }
            }

            if (licenseInformation.IsActive)
            {
                if (licenseInformation.IsTrial)
                {
                    disableVocali();
                }
            }
        }

        private async void btPlay_click(object sender, RoutedEventArgs e)
        {
            if (!playincorso)
            {
                playincorso = true;
                muto = false;
                string s = string.Empty;
                char[] charSet;
                ObservableCollection<string> uniSet = new ObservableCollection<string>();
                Uri url;

                riccoEditore.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out s);
                charSet = s.ToCharArray();

                for (int i = 0; i < charSet.Length; i++)
                {
                    uniSet.Insert(i, ((int)charSet[i]).ToString("X4").ToLower());
                }

                for (int i = 0; i < uniSet.Count; i++)
                {
                    if (!muto)
                    {
                        if (uniSet.ElementAt(i).Equals("0020") || uniSet.ElementAt(i).Equals("000A"))
                        {
                            await Task.Delay(2000);
                        }
                        else
                        {
                            url = new Uri("ms-appx:///mp3/" + uniSet.ElementAt(i) + ".mp3", UriKind.Absolute);
                            await playSounds(url);
                        }
                    }
                }
                player.Stop();
                playincorso = false;
            }
        }

        private async Task playSounds(Uri url)
        {
            Windows.Storage.StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
            try
            {
                var a = url.LocalPath.Replace('/', '\\');
                var file = await installedLocation.GetFileAsync(a);
                player.Source = url;
                player.Play();
                await Task.Delay(2500);
            }
            catch (System.IO.FileNotFoundException fileNotFoundEx)
            {
                //file non esistente controlla se spazio
            }
        }

        private async void btStop_click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            muto = true;
            await aspDueSec();
            muto = false;
        }

        private Task aspDueSec()
        {
            return Task.Delay(2000);
        }

        private void btCopy_click(object sender, RoutedEventArgs e)
        {
            string s = String.Empty;
            riccoEditore.Document.Selection.Copy();
        }

        private void btCut_click(object sender, RoutedEventArgs e)
        {
            string s = String.Empty;
            Object valueTast = localSettings.Values["tast"];

            riccoEditore.IsReadOnly = false;
            riccoEditore.Document.Selection.Cut();
            if ((String)valueTast == "False")
                riccoEditore.IsReadOnly = true;
            else
                riccoEditore.Focus(Windows.UI.Xaml.FocusState.Programmatic);

        }

        private void btPaste_click(object sender, RoutedEventArgs e)
        {
            string s = String.Empty;
            Object valueTast = localSettings.Values["tast"];

            riccoEditore.IsReadOnly = false;
            if (riccoEditore.Document.CanPaste())
                riccoEditore.Document.Selection.Paste(0);
            else
            {
                var s1 = rl.GetString("scrittaNoIncolla");
                var s2 = rl.GetString("scrittaAttenzione");
                MessageDialog msgDialog = new MessageDialog(s1, s2);
                msgDialog.ShowAsync();
            }
            if ((String)valueTast == "False")
                riccoEditore.IsReadOnly = true;
            else
                riccoEditore.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            
        }

        private void btSave_click(object sender, RoutedEventArgs e)
        {
            salva();
        }

        private async void salva()
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            var s = rl.GetString("scrittaDocumentoWord");
            savePicker.FileTypeChoices.Add(s, new List<string>() { ".doc" });
            // Default file name if the user does not type one in or select a file to replace

            var s1 = rl.GetString("scrittaNewDoc");
            savePicker.SuggestedFileName = s1;
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file);
                // write to file

                Windows.Storage.Streams.IRandomAccessStream randAccStream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                riccoEditore.Document.SaveToStream(Windows.UI.Text.TextGetOptions.FormatRtf, randAccStream);


                // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                if (status == FileUpdateStatus.Complete)
                {
                    s = rl.GetString("scrittaSalvato");
                    var s2 = rl.GetString("scrittaBene");
                    MessageDialog msgDialog = new MessageDialog(s, s2);
                    await msgDialog.ShowAsync();
                }
                fileAperto = file;
                tbTitoloDoc.Text = fileAperto.Name;
            }
        }

        private void btOpen_click(object sender, RoutedEventArgs e)
        {
            /*string s = String.Empty;

            riccoEditore.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out s);
            if (s != "")
            {
                domanda_salvare();
            }
            else*/
            {
                apri();
            }
        }

        private async void apri()
        {
            string s = String.Empty;

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".doc");

            StorageFile file = await openPicker.PickSingleFileAsync();

            Object valueTast = localSettings.Values["tast"];

            if (file != null && file.Path!=fileAperto.Path)
            {
                s = await Windows.Storage.FileIO.ReadTextAsync(file);
                riccoEditore.IsReadOnly = false;
                riccoEditore.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, s);
                if ((String)valueTast == "False")
                    riccoEditore.IsReadOnly = true;
                fileAperto = file;
                tbTitoloDoc.Text = fileAperto.Name;
            }
        }

        /*private async void domanda_salvare()
        {
            var s = rl.GetString("scrittaVuoiSalvare");
            var s2 = rl.GetString("scrittaAttenzione");
            MessageDialog msgDialog = new MessageDialog(s, s2);

            //OK Button
            s = rl.GetString("scrittaSi");
            UICommand okBtn = new UICommand(s);
            okBtn.Invoked = OkBtnClick;
            msgDialog.Commands.Add(okBtn);

            //Cancel Button
            s = rl.GetString("scrittaNo");
            UICommand cancelBtn = new UICommand(s);
            cancelBtn.Invoked = CancelBtnClick;
            msgDialog.Commands.Add(cancelBtn);

            //Show message
            await msgDialog.ShowAsync();
        }

        private void OkBtnClick(IUICommand command)
        {
            salva();
            apri();
        }

        private void CancelBtnClick(IUICommand command)
        {
            apri();
        }*/

        private void cbPlay_Checked(object sender, RoutedEventArgs e)
        {
            ascolta = true;
        }

        private void cbPlay_Unchecked(object sender, RoutedEventArgs e)
        {
            ascolta = false;
        }

        private void btSu_click(object sender, RoutedEventArgs e)
        {
            riccoEditore.Document.Selection.MoveUp(TextRangeUnit.Line,1,false);
        }

        private void btGiu_click(object sender, RoutedEventArgs e)
        {
            riccoEditore.Document.Selection.MoveDown(TextRangeUnit.Line, 1, false);
        }

        private void btDx_click(object sender, RoutedEventArgs e)
        {
            riccoEditore.Document.Selection.MoveRight(TextRangeUnit.Character, 1, false);
        }

        private void btSx_click(object sender, RoutedEventArgs e)
        {
            riccoEditore.Document.Selection.MoveLeft(TextRangeUnit.Character, 1, false);
        }

        private void btGrassetto_Click(object sender, RoutedEventArgs e)
        {
            ITextSelection selectedText = riccoEditore.Document.Selection;
            Object valueTast = localSettings.Values["tast"];
            if (selectedText != null)
            {
                riccoEditore.IsReadOnly = false;
                ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.Bold = FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
                if ((String)valueTast == "False")
                    riccoEditore.IsReadOnly = true;
                else
                    riccoEditore.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            }
        }

        private void btCorsivo_Click(object sender, RoutedEventArgs e)
        {
            ITextSelection selectedText = riccoEditore.Document.Selection;
            Object valueTast = localSettings.Values["tast"];
            if (selectedText != null)
            {
                riccoEditore.IsReadOnly = false;
                ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.Italic = FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
                if ((String)valueTast == "False")
                    riccoEditore.IsReadOnly = true;
                else
                    riccoEditore.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            }
        }

        private void btSottolineato_Click(object sender, RoutedEventArgs e)
        {
            ITextSelection selectedText = riccoEditore.Document.Selection;
            Object valueTast = localSettings.Values["tast"];
            if (selectedText != null)
            {
                riccoEditore.IsReadOnly = false;
                ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                if (charFormatting.Underline == UnderlineType.Single)
                    charFormatting.Underline = UnderlineType.None;
                else
                    charFormatting.Underline = UnderlineType.Single;
                if ((String)valueTast == "False")
                    riccoEditore.IsReadOnly = true;
                else
                    riccoEditore.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            }
        }

        private void btColoreFont_Click(object sender, RoutedEventArgs e)
        {
            popColori.IsOpen = true;
            //btColoreFont.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }

        /*private void btColoreFont_LostFocus(object sender, RoutedEventArgs e)
        {
            popColori.IsOpen = false;
        }*/

        private void btColore_Click(object sender, RoutedEventArgs e)
        {
            Button btSource = new Button();
            btSource = (Button)e.OriginalSource;

            Object valueTast = localSettings.Values["tast"];

            ITextCharacterFormat format = riccoEditore.Document.Selection.CharacterFormat;
            BrushToColorConverter brushToColorConverter = new BrushToColorConverter();

            Windows.UI.Color coloreScelto = (Windows.UI.Color) brushToColorConverter.Convert(btSource.Background,null,null,null);
            
            riccoEditore.IsReadOnly = false;
            format.ForegroundColor = coloreScelto;
            
            if (riccoEditore.Document.Selection.StartPosition == riccoEditore.Document.Selection.EndPosition)
            {
                riccoEditore.Document.Selection.CharacterFormat = format;
            }
            if ((String)valueTast == "False")
                riccoEditore.IsReadOnly = true;
            else
                riccoEditore.Focus(Windows.UI.Xaml.FocusState.Programmatic);

            popColori.IsOpen = false;
        }

        private void btDimFont_Click(object sender, RoutedEventArgs e)
        {
            tbDimFont.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void riccoEditore_SelectionChanged(object sender, RoutedEventArgs e)
        {
            float size = riccoEditore.Document.Selection.CharacterFormat.Size;
            if (size > 0)
                tbDimFont.Text = size.ToString();
            else
                tbDimFont.Text = "";
        }

        private void tbDimFont_TextChanged(object sender, TextChangedEventArgs e)
        {
            ITextSelection selectedText = riccoEditore.Document.Selection;
            Object valueTast = localSettings.Values["tast"];
            if (selectedText != null)
            {
                riccoEditore.IsReadOnly = false;
                ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                float a;
                float.TryParse(tbDimFont.Text,out a);
                if (a != 0)
                {
                    try
                    {
                        charFormatting.Size = a;
                    }
                    catch (Exception ex) { }
                }
                selectedText.CharacterFormat = charFormatting;
                if ((String)valueTast == "False")
                    riccoEditore.IsReadOnly = true;
            }
        }

        private void tbDimFont_LostFocus(object sender, RoutedEventArgs e)
        {
            float a;
            float.TryParse(tbDimFont.Text, out a);
            if (a != 0)
                riccoEditore.Document.Selection.CharacterFormat.Size = a;
        }

        private void btShowLab_Click(object sender, RoutedEventArgs e)
        {
            string btText;
            bool nascondi;
            nascondi = (tbso14.Visibility == Visibility.Visible);

            if (nascondi)
            {
                //primariga
                primaRiga.Height = GridLength.Auto;

                #region nascondiEtichette
                //etichette sinistra
                tbsi01.Visibility = Visibility.Collapsed;
                tbsi02.Visibility = Visibility.Collapsed;
                tbsi03.Visibility = Visibility.Collapsed;
                tbsi04.Visibility = Visibility.Collapsed;
                tbsi05.Visibility = Visibility.Collapsed;
                tbsi06.Visibility = Visibility.Collapsed;
                tbsi07.Visibility = Visibility.Collapsed;
                tbsi08.Visibility = Visibility.Collapsed;
                tbsi09.Visibility = Visibility.Collapsed;
                tbsi10.Visibility = Visibility.Collapsed;
                tbsi101.Visibility = Visibility.Collapsed;
                tbsi11.Visibility = Visibility.Collapsed;
                tbsi12.Visibility = Visibility.Collapsed;

                //etichette destra
                tbd01.Visibility = Visibility.Collapsed;
                tbd02.Visibility = Visibility.Collapsed;
                tbd03.Visibility = Visibility.Collapsed;
                tbd04.Visibility = Visibility.Collapsed;
                tbd05.Visibility = Visibility.Collapsed;
                tbd06.Visibility = Visibility.Collapsed;
                tbd07.Visibility = Visibility.Collapsed;
                tbd08.Visibility = Visibility.Collapsed;
                tbd09.Visibility = Visibility.Collapsed;
                tbd10.Visibility = Visibility.Collapsed;
                tbd11.Visibility = Visibility.Collapsed;
                tbd12.Visibility = Visibility.Collapsed;
                tbd13.Visibility = Visibility.Collapsed;

                //etichette sopra
                tbso01.Visibility = Visibility.Collapsed;
                tbso02.Visibility = Visibility.Collapsed;
                tbso03.Visibility = Visibility.Collapsed;
                tbso04.Visibility = Visibility.Collapsed;
                tbso05.Visibility = Visibility.Collapsed;
                tbso06.Visibility = Visibility.Collapsed;
                tbso07.Visibility = Visibility.Collapsed;
                tbso08.Visibility = Visibility.Collapsed;
                tbso09.Visibility = Visibility.Collapsed;
                tbso10.Visibility = Visibility.Collapsed;
                tbso11.Visibility = Visibility.Collapsed;
                tbso12.Visibility = Visibility.Collapsed;
                tbso13.Visibility = Visibility.Collapsed;
                tbso14.Visibility = Visibility.Collapsed;
                tbso15.Visibility = Visibility.Collapsed;
                tbso16.Visibility = Visibility.Collapsed;
                tbso17.Visibility = Visibility.Collapsed;
                tbso18.Visibility = Visibility.Collapsed;
                tbso19.Visibility = Visibility.Collapsed;
                tbso20.Visibility = Visibility.Collapsed;
                tbso21.Visibility = Visibility.Collapsed;
                tbso22.Visibility = Visibility.Collapsed;
                #endregion

                if (c0070.IsEnabled)
                {
                    #region enable destra
                    c02a3.IsEnabled = true;
                    c006d.IsEnabled = true;
                    c__91.IsEnabled = true;
                    c0281.IsEnabled = true;
                    c__163.IsEnabled = true;
                    c__218.IsEnabled = true;
                    c__216.IsEnabled = true;
                    c0263.IsEnabled = true;
                    c__161.IsEnabled = true;
                    c__238.IsEnabled = true;
                    c__214.IsEnabled = true;
                    c__35.IsEnabled = true;
                    c__139.IsEnabled = true;
                    c__88.IsEnabled = true;
                    v0153.IsEnabled = true;
                    c0256.IsEnabled = true;
                    c__117.IsEnabled = true;
                    c__66.IsEnabled = true;
                    c__186.IsEnabled = true;
                    c006e.IsEnabled = true;
                    c__97.IsEnabled = true;
                    c__43.IsEnabled = true;
                    c__169.IsEnabled = true;
                    c0076.IsEnabled = true;
                    c__149.IsEnabled = true;
                    c__226.IsEnabled = true;
                    v0276.IsEnabled = true;
                    c__25.IsEnabled = true;
                    c026d.IsEnabled = true;
                    c__75.IsEnabled = true;
                    c__189.IsEnabled = true;
                    c0274.IsEnabled = true;
                    c0279.IsEnabled = true;
                    c__51.IsEnabled = true;
                    c__178.IsEnabled = true;
                    c029d.IsEnabled = true;
                    c__159.IsEnabled = true;
                    c__236.IsEnabled = true;
                    c__212.IsEnabled = true;
                    c02a4.IsEnabled = true;
                    c__133.IsEnabled = true;
                    c2c71.IsEnabled = true;
                    c__196.IsEnabled = true;
                    c__20.IsEnabled = true;
                    c__111.IsEnabled = true;
                    c__61.IsEnabled = true;
                    c__182.IsEnabled = true;
                    c__32.IsEnabled = true;
                    c__135.IsEnabled = true;
                    c__85.IsEnabled = true;
                    c__198.IsEnabled = true;
                    c0064.IsEnabled = true;
                    c__113.IsEnabled = true;
                    c0042.IsEnabled = true;
                    v028f.IsEnabled = true;
                    c0271.IsEnabled = true;
                    c__93.IsEnabled = true;
                    c0295.IsEnabled = true;
                    c__165.IsEnabled = true;
                    c__41.IsEnabled = true;
                    c__145.IsEnabled = true;
                    c__222.IsEnabled = true;
                    v0254.IsEnabled = true;
                    c0047.IsEnabled = true;
                    c006c.IsEnabled = true;
                    c__71.IsEnabled = true;
                    v00f8.IsEnabled = true;
                    c0272.IsEnabled = true;
                    c028b.IsEnabled = true;
                    c026e.IsEnabled = true;
                    c__175.IsEnabled = true;
                    c0292.IsEnabled = true;
                    c__155.IsEnabled = true;
                    c__232.IsEnabled = true;
                    v0252.IsEnabled = true;
                    c__30.IsEnabled = true;
                    c__129.IsEnabled = true;
                    c__80.IsEnabled = true;
                    c__194.IsEnabled = true;
                    c0062.IsEnabled = true;
                    c006a.IsEnabled = true;
                    c__57.IsEnabled = true;
                    c__180.IsEnabled = true;
                    c__37.IsEnabled = true;
                    c__141.IsEnabled = true;
                    c027d.IsEnabled = true;
                    c__201.IsEnabled = true;
                    c025f.IsEnabled = true;
                    c__119.IsEnabled = true;
                    c0072.IsEnabled = true;
                    v028a.IsEnabled = true;
                    c__8.IsEnabled = true;
                    c__99.IsEnabled = true;
                    c__45.IsEnabled = true;
                    c__171.IsEnabled = true;
                    c00f0.IsEnabled = true;
                    c__151.IsEnabled = true;
                    c__228.IsEnabled = true;
                    c__206.IsEnabled = true;
                    c__27.IsEnabled = true;
                    c028e.IsEnabled = true;
                    c0052.IsEnabled = true;
                    v006f.IsEnabled = true;
                    c__14.IsEnabled = true;
                    c__108.IsEnabled = true;
                    c__53.IsEnabled = true;
                    v0079.IsEnabled = true;
                    c__33_2.IsEnabled = true;
                    c__137.IsEnabled = true;
                    c027e.IsEnabled = true;
                    c__199.IsEnabled = true;
                    c__22.IsEnabled = true;
                    c__115.IsEnabled = true;
                    c__64.IsEnabled = true;
                    c__184.IsEnabled = true;
                    c__4.IsEnabled = true;
                    c__95.IsEnabled = true;
                    c0266.IsEnabled = true;
                    c__167.IsEnabled = true;
                    c03b2.IsEnabled = true;
                    c__147.IsEnabled = true;
                    c__224.IsEnabled = true;
                    c__204.IsEnabled = true;
                    c__24.IsEnabled = true;
                    c__124.IsEnabled = true;
                    c__73.IsEnabled = true;
                    v0275.IsEnabled = true;
                    c014b.IsEnabled = true;
                    c__105.IsEnabled = true;
                    c__49.IsEnabled = true;
                    c__177.IsEnabled = true;
                    c0290.IsEnabled = true;
                    c__157.IsEnabled = true;
                    c__234.IsEnabled = true;
                    c__210.IsEnabled = true;
                    c__131.IsEnabled = true;
                    c__82.IsEnabled = true;
                    v025e.IsEnabled = true;
                    c__18.IsEnabled = true;
                    c0270.IsEnabled = true;
                    c__59.IsEnabled = true;
                    v0075.IsEnabled = true;
                    c__39.IsEnabled = true;
                    c__143.IsEnabled = true;
                    c__220.IsEnabled = true;
                    c__202.IsEnabled = true;
                    c0261.IsEnabled = true;
                    c__121.IsEnabled = true;
                    c__69.IsEnabled = true;
                    c__188.IsEnabled = true;
                    c0273.IsEnabled = true;
                    c__102.IsEnabled = true;
                    c__47.IsEnabled = true;
                    c__173.IsEnabled = true;
                    c007a.IsEnabled = true;
                    c__230.IsEnabled = true;
                    c__207.IsEnabled = true;
                    c__28.IsEnabled = true;
                    c024c.IsEnabled = true;
                    c__78.IsEnabled = true;
                    c__192.IsEnabled = true;
                    c__16.IsEnabled = true;
                    c027b.IsEnabled = true;
                    c__55.IsEnabled = true;
                    v0289.IsEnabled = true;
                    c__153.IsEnabled = true;
                    #endregion
                }
                else
                {
                    #region enable sinistra
                    c__219.IsEnabled = true;
                    c__15.IsEnabled = true;
                    c__1.IsEnabled = true;
                    c02a6.IsEnabled = true;
                    c__90.IsEnabled = true;
                    c03c7.IsEnabled = true;
                    c__162.IsEnabled = true;
                    c__217.IsEnabled = true;
                    c__215.IsEnabled = true;
                    c0078.IsEnabled = true;
                    c__160.IsEnabled = true;
                    c__237.IsEnabled = true;
                    c__213.IsEnabled = true;
                    c__34.IsEnabled = true;
                    c__138.IsEnabled = true;
                    c__87.IsEnabled = true;
                    c__200.IsEnabled = true;
                    c0288.IsEnabled = true;
                    c__116.IsEnabled = true;
                    c__65.IsEnabled = true;
                    c__185.IsEnabled = true;
                    c__5.IsEnabled = true;
                    c__96.IsEnabled = true;
                    c__42.IsEnabled = true;
                    c__168.IsEnabled = true;
                    c0066.IsEnabled = true;
                    c__148.IsEnabled = true;
                    c__225.IsEnabled = true;
                    c__205.IsEnabled = true;
                    c0294.IsEnabled = true;
                    c__125.IsEnabled = true;
                    c__74.IsEnabled = true;
                    v0264.IsEnabled = true;
                    c__12.IsEnabled = true;
                    c__106.IsEnabled = true;
                    c__50.IsEnabled = true;
                    v0069.IsEnabled = true;
                    c00e7.IsEnabled = true;
                    c__158.IsEnabled = true;
                    c__235.IsEnabled = true;
                    c__211.IsEnabled = true;
                    c02a7.IsEnabled = true;
                    c__132.IsEnabled = true;
                    c__83.IsEnabled = true;
                    c__195.IsEnabled = true;
                    c__19.IsEnabled = true;
                    c__110.IsEnabled = true;
                    c__60.IsEnabled = true;
                    v026a.IsEnabled = true;
                    c__31.IsEnabled = true;
                    c__134.IsEnabled = true;
                    c__84.IsEnabled = true;
                    c__197.IsEnabled = true;
                    c0074.IsEnabled = true;
                    c__112.IsEnabled = true;
                    c__62.IsEnabled = true;
                    v0269.IsEnabled = true;
                    c__2.IsEnabled = true;
                    c__92.IsEnabled = true;
                    c0127.IsEnabled = true;
                    c__164.IsEnabled = true;
                    c__40.IsEnabled = true;
                    c__144.IsEnabled = true;
                    c__221.IsEnabled = true;
                    c__203.IsEnabled = true;
                    c0071.IsEnabled = true;
                    c__122.IsEnabled = true;
                    c__70.IsEnabled = true;
                    v0258.IsEnabled = true;
                    c__10.IsEnabled = true;
                    c__103.IsEnabled = true;
                    c026c.IsEnabled = true;
                    c__174.IsEnabled = true;
                    c0283.IsEnabled = true;
                    c__154.IsEnabled = true;
                    c__231.IsEnabled = true;
                    c__208.IsEnabled = true;
                    c__29.IsEnabled = true;
                    c__128.IsEnabled = true;
                    c__79.IsEnabled = true;
                    c__193.IsEnabled = true;
                    c0070.IsEnabled = true;
                    c0265.IsEnabled = true;
                    c__56.IsEnabled = true;
                    v026f.IsEnabled = true;
                    c__36.IsEnabled = true;
                    c__140.IsEnabled = true;
                    c__89.IsEnabled = true;
                    v0250.IsEnabled = true;
                    c0063.IsEnabled = true;
                    c__118.IsEnabled = true;
                    c__67.IsEnabled = true;
                    c__187.IsEnabled = true;
                    c__7.IsEnabled = true;
                    c__98.IsEnabled = true;
                    c__44.IsEnabled = true;
                    c__170.IsEnabled = true;
                    c03b8.IsEnabled = true;
                    c__150.IsEnabled = true;
                    c__227.IsEnabled = true;
                    v0061.IsEnabled = true;
                    c__26.IsEnabled = true;
                    c__126.IsEnabled = true;
                    c__76.IsEnabled = true;
                    c__190.IsEnabled = true;
                    c__13.IsEnabled = true;
                    c__107.IsEnabled = true;
                    c__52.IsEnabled = true;
                    c__179.IsEnabled = true;
                    c__33.IsEnabled = true;
                    c__136.IsEnabled = true;
                    c__86.IsEnabled = true;
                    v025b.IsEnabled = true;
                    c__21.IsEnabled = true;
                    c__114.IsEnabled = true;
                    c__63.IsEnabled = true;
                    c__183.IsEnabled = true;
                    c__3.IsEnabled = true;
                    c__94.IsEnabled = true;
                    c0068.IsEnabled = true;
                    c__166.IsEnabled = true;
                    c0278.IsEnabled = true;
                    c__146.IsEnabled = true;
                    c__223.IsEnabled = true;
                    v00e6.IsEnabled = true;
                    c__23.IsEnabled = true;
                    c__123.IsEnabled = true;
                    c__72.IsEnabled = true;
                    v0259.IsEnabled = true;
                    c__11.IsEnabled = true;
                    c__104.IsEnabled = true;
                    c__48.IsEnabled = true;
                    c__176.IsEnabled = true;
                    c0282.IsEnabled = true;
                    c__156.IsEnabled = true;
                    c__233.IsEnabled = true;
                    c__209.IsEnabled = true;
                    c024c.IsEnabled = true;
                    c__130.IsEnabled = true;
                    c__81.IsEnabled = true;
                    v025c.IsEnabled = true;
                    c__17.IsEnabled = true;
                    c0077.IsEnabled = true;
                    c__58.IsEnabled = true;
                    c__181.IsEnabled = true;
                    c__38.IsEnabled = true;
                    c__142.IsEnabled = true;
                    c__21.IsEnabled = true;
                    v028c.IsEnabled = true;
                    c006b.IsEnabled = true;
                    c__120.IsEnabled = true;
                    c__68.IsEnabled = true;
                    v0065.IsEnabled = true;
                    c__9.IsEnabled = true;
                    c__101.IsEnabled = true;
                    c__46.IsEnabled = true;
                    c__172.IsEnabled = true;
                    c0073.IsEnabled = true;
                    c__152.IsEnabled = true;
                    c__229.IsEnabled = true;
                    v0251.IsEnabled = true;
                    c____.IsEnabled = true;
                    c__127.IsEnabled = true;
                    c__77.IsEnabled = true;
                    c__191.IsEnabled = true;
                    c__15.IsEnabled = true;
                    c__109.IsEnabled = true;
                    c__54.IsEnabled = true;
                    v0268.IsEnabled = true;
                    #endregion
                }

                #region FontSize
                c__219.FontSize = 10;
                c__153.FontSize = 10;
                c02a3.FontSize = 10;
                c006d.FontSize = 10;
                c__91.FontSize = 10;
                c0281.FontSize = 10;
                c__163.FontSize = 10;
                c__218.FontSize = 10;
                c__216.FontSize = 10;
                c0263.FontSize = 10;
                c__161.FontSize = 10;
                c__238.FontSize = 10;
                c__214.FontSize = 10;
                c__35.FontSize = 10;
                c__139.FontSize = 10;
                c__88.FontSize = 10;
                v0153.FontSize = 10;
                c0256.FontSize = 10;
                c__117.FontSize = 10;
                c__66.FontSize = 10;
                c__186.FontSize = 10;
                c006e.FontSize = 10;
                c__97.FontSize = 10;
                c__43.FontSize = 10;
                c__169.FontSize = 10;
                c0076.FontSize = 10;
                c__149.FontSize = 10;
                c__226.FontSize = 10;
                v0276.FontSize = 10;
                c__25.FontSize = 10;
                c026d.FontSize = 10;
                c__75.FontSize = 10;
                c__189.FontSize = 10;
                c0274.FontSize = 10;
                c0279.FontSize = 10;
                c__51.FontSize = 10;
                c__178.FontSize = 10;
                c029d.FontSize = 10;
                c__159.FontSize = 10;
                c__236.FontSize = 10;
                c__212.FontSize = 10;
                c02a4.FontSize = 10;
                c__133.FontSize = 10;
                c2c71.FontSize = 10;
                c__196.FontSize = 10;
                c__20.FontSize = 10;
                c__111.FontSize = 10;
                c__61.FontSize = 10;
                c__182.FontSize = 10;
                c__32.FontSize = 10;
                c__135.FontSize = 10;
                c__85.FontSize = 10;
                c__198.FontSize = 10;
                c0064.FontSize = 10;
                c__113.FontSize = 10;
                c0042.FontSize = 10;
                v028f.FontSize = 10;
                c0271.FontSize = 10;
                c__93.FontSize = 10;
                c0295.FontSize = 10;
                c__165.FontSize = 10;
                c__41.FontSize = 10;
                c__145.FontSize = 10;
                c__222.FontSize = 10;
                v0254.FontSize = 10;
                c0047.FontSize = 10;
                c006c.FontSize = 10;
                c__71.FontSize = 10;
                v00f8.FontSize = 10;
                c0272.FontSize = 10;
                c028b.FontSize = 10;
                c026e.FontSize = 10;
                c__175.FontSize = 10;
                c0292.FontSize = 10;
                c__155.FontSize = 10;
                c__232.FontSize = 10;
                v0252.FontSize = 10;
                c__30.FontSize = 10;
                c__129.FontSize = 10;
                c__80.FontSize = 10;
                c__194.FontSize = 10;
                c0062.FontSize = 10;
                c006a.FontSize = 10;
                c__57.FontSize = 10;
                c__180.FontSize = 10;
                c__37.FontSize = 10;
                c__141.FontSize = 10;
                c027d.FontSize = 10;
                c__201.FontSize = 10;
                c025f.FontSize = 10;
                c__119.FontSize = 10;
                c0072.FontSize = 10;
                v028a.FontSize = 10;
                c__8.FontSize = 10;
                c__99.FontSize = 10;
                c__45.FontSize = 10;
                c__171.FontSize = 10;
                c00f0.FontSize = 10;
                c__151.FontSize = 10;
                c__228.FontSize = 10;
                c__206.FontSize = 10;
                c__27.FontSize = 10;
                c028e.FontSize = 10;
                c0052.FontSize = 10;
                v006f.FontSize = 10;
                c__14.FontSize = 10;
                c__108.FontSize = 10;
                c__53.FontSize = 10;
                v0079.FontSize = 10;
                c__33_2.FontSize = 10;
                c__137.FontSize = 10;
                c027e.FontSize = 10;
                c__199.FontSize = 10;
                c__22.FontSize = 10;
                c__115.FontSize = 10;
                c__64.FontSize = 10;
                c__184.FontSize = 10;
                c__4.FontSize = 10;
                c__95.FontSize = 10;
                c0266.FontSize = 10;
                c__167.FontSize = 10;
                c03b2.FontSize = 10;
                c__147.FontSize = 10;
                c__224.FontSize = 10;
                c__204.FontSize = 10;
                c__24.FontSize = 10;
                c__124.FontSize = 10;
                c__73.FontSize = 10;
                v0275.FontSize = 10;
                c014b.FontSize = 10;
                c__105.FontSize = 10;
                c__49.FontSize = 10;
                c__177.FontSize = 10;
                c0290.FontSize = 10;
                c__157.FontSize = 10;
                c__234.FontSize = 10;
                c__210.FontSize = 10;
                c__131.FontSize = 10;
                c__82.FontSize = 10;
                v025e.FontSize = 10;
                c__18.FontSize = 10;
                c0270.FontSize = 10;
                c__59.FontSize = 10;
                v0075.FontSize = 10;
                c__39.FontSize = 10;
                c__143.FontSize = 10;
                c__220.FontSize = 10;
                c__202.FontSize = 10;
                c0261.FontSize = 10;
                c__121.FontSize = 10;
                c__69.FontSize = 10;
                c__188.FontSize = 10;
                c0273.FontSize = 10;
                c__102.FontSize = 10;
                c__47.FontSize = 10;
                c__173.FontSize = 10;
                c007a.FontSize = 10;
                c__230.FontSize = 10;
                c__207.FontSize = 10;
                c__28.FontSize = 10;
                c024c.FontSize = 10;
                c__78.FontSize = 10;
                c__192.FontSize = 10;
                c__16.FontSize = 10;
                c027b.FontSize = 10;
                c__55.FontSize = 10;
                v0289.FontSize = 10;
                c__15.FontSize = 10;
                c__1.FontSize = 10;
                c02a6.FontSize = 10;
                c__90.FontSize = 10;
                c03c7.FontSize = 10;
                c__162.FontSize = 10;
                c__217.FontSize = 10;
                c__215.FontSize = 10;
                c0078.FontSize = 10;
                c__160.FontSize = 10;
                c__237.FontSize = 10;
                c__213.FontSize = 10;
                c__34.FontSize = 10;
                c__138.FontSize = 10;
                c__87.FontSize = 10;
                c__200.FontSize = 10;
                c0288.FontSize = 10;
                c__116.FontSize = 10;
                c__65.FontSize = 10;
                c__185.FontSize = 10;
                c__5.FontSize = 10;
                c__96.FontSize = 10;
                c__42.FontSize = 10;
                c__168.FontSize = 10;
                c0066.FontSize = 10;
                c__148.FontSize = 10;
                c__225.FontSize = 10;
                c__205.FontSize = 10;
                c0294.FontSize = 10;
                c__125.FontSize = 10;
                c__74.FontSize = 10;
                v0264.FontSize = 10;
                c__12.FontSize = 10;
                c__106.FontSize = 10;
                c__50.FontSize = 10;
                v0069.FontSize = 10;
                c00e7.FontSize = 10;
                c__158.FontSize = 10;
                c__235.FontSize = 10;
                c__211.FontSize = 10;
                c02a7.FontSize = 10;
                c__132.FontSize = 10;
                c__83.FontSize = 10;
                c__195.FontSize = 10;
                c__19.FontSize = 10;
                c__110.FontSize = 10;
                c__60.FontSize = 10;
                v026a.FontSize = 10;
                c__31.FontSize = 10;
                c__134.FontSize = 10;
                c__84.FontSize = 10;
                c__197.FontSize = 10;
                c0074.FontSize = 10;
                c__112.FontSize = 10;
                c__62.FontSize = 10;
                v0269.FontSize = 10;
                c__2.FontSize = 10;
                c__92.FontSize = 10;
                c0127.FontSize = 10;
                c__164.FontSize = 10;
                c__40.FontSize = 10;
                c__144.FontSize = 10;
                c__221.FontSize = 10;
                c__203.FontSize = 10;
                c0071.FontSize = 10;
                c__122.FontSize = 10;
                c__70.FontSize = 10;
                v0258.FontSize = 10;
                c__10.FontSize = 10;
                c__103.FontSize = 10;
                c026c.FontSize = 10;
                c__174.FontSize = 10;
                c0283.FontSize = 10;
                c__154.FontSize = 10;
                c__231.FontSize = 10;
                c__208.FontSize = 10;
                c__29.FontSize = 10;
                c__128.FontSize = 10;
                c__79.FontSize = 10;
                c__193.FontSize = 10;
                c0070.FontSize = 10;
                c0265.FontSize = 10;
                c__56.FontSize = 10;
                v026f.FontSize = 10;
                c__36.FontSize = 10;
                c__140.FontSize = 10;
                c__89.FontSize = 10;
                v0250.FontSize = 10;
                c0063.FontSize = 10;
                c__118.FontSize = 10;
                c__67.FontSize = 10;
                c__187.FontSize = 10;
                c__7.FontSize = 10;
                c__98.FontSize = 10;
                c__44.FontSize = 10;
                c__170.FontSize = 10;
                c03b8.FontSize = 10;
                c__150.FontSize = 10;
                c__227.FontSize = 10;
                v0061.FontSize = 10;
                c__26.FontSize = 10;
                c__126.FontSize = 10;
                c__76.FontSize = 10;
                c__190.FontSize = 10;
                c__13.FontSize = 10;
                c__107.FontSize = 10;
                c__52.FontSize = 10;
                c__179.FontSize = 10;
                c__33.FontSize = 10;
                c__136.FontSize = 10;
                c__86.FontSize = 10;
                v025b.FontSize = 10;
                c__21.FontSize = 10;
                c__114.FontSize = 10;
                c__63.FontSize = 10;
                c__183.FontSize = 10;
                c__3.FontSize = 10;
                c__94.FontSize = 10;
                c0068.FontSize = 10;
                c__166.FontSize = 10;
                c0278.FontSize = 10;
                c__146.FontSize = 10;
                c__223.FontSize = 10;
                v00e6.FontSize = 10;
                c__23.FontSize = 10;
                c__123.FontSize = 10;
                c__72.FontSize = 10;
                v0259.FontSize = 10;
                c__11.FontSize = 10;
                c__104.FontSize = 10;
                c__48.FontSize = 10;
                c__176.FontSize = 10;
                c0282.FontSize = 10;
                c__156.FontSize = 10;
                c__233.FontSize = 10;
                c__209.FontSize = 10;
                c024c.FontSize = 10;
                c__130.FontSize = 10;
                c__81.FontSize = 10;
                v025c.FontSize = 10;
                c__17.FontSize = 10;
                c0077.FontSize = 10;
                c__58.FontSize = 10;
                c__181.FontSize = 10;
                c__38.FontSize = 10;
                c__142.FontSize = 10;
                c__21.FontSize = 10;
                v028c.FontSize = 10;
                c006b.FontSize = 10;
                c__120.FontSize = 10;
                c__68.FontSize = 10;
                v0065.FontSize = 10;
                c__9.FontSize = 10;
                c__101.FontSize = 10;
                c__46.FontSize = 10;
                c__172.FontSize = 10;
                c0073.FontSize = 10;
                c__152.FontSize = 10;
                c__229.FontSize = 10;
                v0251.FontSize = 10;
                c____.FontSize = 10;
                c__127.FontSize = 10;
                c__77.FontSize = 10;
                c__191.FontSize = 10;
                c__15.FontSize = 10;
                c__109.FontSize = 10;
                c__54.FontSize = 10;
                v0268.FontSize = 10;
                #endregion

                #region opacity --
                c__1.Opacity = .7;
                c__2.Opacity = .7;
                c__3.Opacity = .7;
                c__4.Opacity = .7;
                c__5.Opacity = .7;
                c__7.Opacity = .7;
                c__8.Opacity = .7;
                c__9.Opacity = .7;
                c__10.Opacity = .7;
                c__11.Opacity = .7;
                c__12.Opacity = .7;
                c__13.Opacity = .7;
                c__14.Opacity = .7;
                c__15.Opacity = .7;
                c__16.Opacity = .7;
                c__17.Opacity = .7;
                c__18.Opacity = .7;
                c__19.Opacity = .7;
                c__20.Opacity = .7;
                c__21.Opacity = .7;
                c__22.Opacity = .7;
                c__23.Opacity = .7;
                c__24.Opacity = .7;
                c__25.Opacity = .7;
                c__26.Opacity = .7;
                c__27.Opacity = .7;
                c__28.Opacity = .7;
                c__29.Opacity = .7;
                c__30.Opacity = .7;
                c__31.Opacity = .7;
                c__32.Opacity = .7;
                c__33.Opacity = .7;
                c__33_2.Opacity = .7;
                c__34.Opacity = .7;
                c__35.Opacity = .7;
                c__36.Opacity = .7;
                c__37.Opacity = .7;
                c__38.Opacity = .7;
                c__39.Opacity = .7;
                c__40.Opacity = .7;
                c__41.Opacity = .7;
                c__42.Opacity = .7;
                c__43.Opacity = .7;
                c__44.Opacity = .7;
                c__45.Opacity = .7;
                c__46.Opacity = .7;
                c__47.Opacity = .7;
                c__48.Opacity = .7;
                c__49.Opacity = .7;
                c__50.Opacity = .7;
                c__51.Opacity = .7;
                c__52.Opacity = .7;
                c__53.Opacity = .7;
                c__54.Opacity = .7;
                c__55.Opacity = .7;
                c__56.Opacity = .7;
                c__57.Opacity = .7;
                c__58.Opacity = .7;
                c__59.Opacity = .7;
                c__60.Opacity = .7;
                c__61.Opacity = .7;
                c__62.Opacity = .7;
                c__63.Opacity = .7;
                c__64.Opacity = .7;
                c__65.Opacity = .7;
                c__66.Opacity = .7;
                c__67.Opacity = .7;
                c__68.Opacity = .7;
                c__69.Opacity = .7;
                c__70.Opacity = .7;
                c__71.Opacity = .7;
                c__72.Opacity = .7;
                c__73.Opacity = .7;
                c__74.Opacity = .7;
                c__75.Opacity = .7;
                c__76.Opacity = .7;
                c__77.Opacity = .7;
                c__78.Opacity = .7;
                c__79.Opacity = .7;
                c__80.Opacity = .7;
                c__81.Opacity = .7;
                c__82.Opacity = .7;
                c__83.Opacity = .7;
                c__84.Opacity = .7;
                c__85.Opacity = .7;
                c__86.Opacity = .7;
                c__87.Opacity = .7;
                c__88.Opacity = .7;
                c__89.Opacity = .7;
                c__90.Opacity = .7;
                c__91.Opacity = .7;
                c__92.Opacity = .7;
                c__93.Opacity = .7;
                c__94.Opacity = .7;
                c__95.Opacity = .7;
                c__96.Opacity = .7;
                c__97.Opacity = .7;
                c__98.Opacity = .7;
                c__99.Opacity = .7;
                c__101.Opacity = .7;
                c__102.Opacity = .7;
                c__103.Opacity = .7;
                c__104.Opacity = .7;
                c__105.Opacity = .7;
                c__106.Opacity = .7;
                c__107.Opacity = .7;
                c__108.Opacity = .7;
                c__109.Opacity = .7;
                c__110.Opacity = .7;
                c__111.Opacity = .7;
                c__112.Opacity = .7;
                c__113.Opacity = .7;
                c__114.Opacity = .7;
                c__115.Opacity = .7;
                c__116.Opacity = .7;
                c__117.Opacity = .7;
                c__118.Opacity = .7;
                c__119.Opacity = .7;
                c__120.Opacity = .7;
                c__121.Opacity = .7;
                c__122.Opacity = .7;
                c__123.Opacity = .7;
                c__124.Opacity = .7;
                c__125.Opacity = .7;
                c__126.Opacity = .7;
                c__127.Opacity = .7;
                c__128.Opacity = .7;
                c__129.Opacity = .7;
                c__130.Opacity = .7;
                c__131.Opacity = .7;
                c__132.Opacity = .7;
                c__133.Opacity = .7;
                c__139.Opacity = .7;
                c__141.Opacity = .7;
                c__155.Opacity = .7;
                c__165.Opacity = .7;
                c__174.Opacity = .7;
                c__175.Opacity = .7;
                c__176.Opacity = .7;
                c__177.Opacity = .7;
                c__178.Opacity = .7;
                c__179.Opacity = .7;
                c__180.Opacity = .7;
                c__181.Opacity = .7;
                c__182.Opacity = .7;
                c__183.Opacity = .7;
                c__184.Opacity = .7;
                c__185.Opacity = .7;
                c__186.Opacity = .7;
                c__187.Opacity = .7;
                c__188.Opacity = .7;
                c__189.Opacity = .7;
                c__190.Opacity = .7;
                c__191.Opacity = .7;
                c__192.Opacity = .7;
                c__193.Opacity = .7;
                c__194.Opacity = .7;
                c__195.Opacity = .7;
                c__196.Opacity = .7;
                c__197.Opacity = .7;
                c__198.Opacity = .7;
                c__199.Opacity = .7;
                c__200.Opacity = .7;
                c__201.Opacity = .7;
                c__202.Opacity = .7;
                c__203.Opacity = .7;
                c__204.Opacity = .7;
                c__205.Opacity = .7;
                c__206.Opacity = .7;
                c__207.Opacity = .7;
                c__208.Opacity = .7;
                c__218.Opacity = .7;
                c__224.Opacity = .7;
                #endregion

                btShift.Visibility = Visibility.Collapsed;

                btText = rl.GetString("mostraLab");
                btShowLab.Content = btText;
            }

            else //if(!nascondi) le etichette devono essere mostrate
            {
                //primariga
                primaRiga.Height = new GridLength(40);

                #region mostraEtichette
                //etichette sinistra
                tbsi01.Visibility = Visibility.Visible;
                tbsi02.Visibility = Visibility.Visible;
                tbsi03.Visibility = Visibility.Visible;
                tbsi04.Visibility = Visibility.Visible;
                tbsi05.Visibility = Visibility.Visible;
                tbsi06.Visibility = Visibility.Visible;
                tbsi07.Visibility = Visibility.Visible;
                tbsi08.Visibility = Visibility.Visible;
                tbsi09.Visibility = Visibility.Visible;
                tbsi10.Visibility = Visibility.Visible;
                tbsi101.Visibility = Visibility.Visible;
                tbsi11.Visibility = Visibility.Visible;
                tbsi12.Visibility = Visibility.Visible;

                //etichette destra
                tbd01.Visibility = Visibility.Visible;
                tbd02.Visibility = Visibility.Visible;
                tbd03.Visibility = Visibility.Visible;
                tbd04.Visibility = Visibility.Visible;
                tbd05.Visibility = Visibility.Visible;
                tbd06.Visibility = Visibility.Visible;
                tbd07.Visibility = Visibility.Visible;
                tbd08.Visibility = Visibility.Visible;
                tbd09.Visibility = Visibility.Visible;
                tbd10.Visibility = Visibility.Visible;
                tbd11.Visibility = Visibility.Visible;
                tbd12.Visibility = Visibility.Visible;
                tbd13.Visibility = Visibility.Visible;

                //etichette sopra
                tbso01.Visibility = Visibility.Visible;
                tbso02.Visibility = Visibility.Visible;
                tbso03.Visibility = Visibility.Visible;
                tbso04.Visibility = Visibility.Visible;
                tbso05.Visibility = Visibility.Visible;
                tbso06.Visibility = Visibility.Visible;
                tbso07.Visibility = Visibility.Visible;
                tbso08.Visibility = Visibility.Visible;
                tbso09.Visibility = Visibility.Visible;
                tbso10.Visibility = Visibility.Visible;
                tbso11.Visibility = Visibility.Visible;
                tbso12.Visibility = Visibility.Visible;
                tbso13.Visibility = Visibility.Visible;
                tbso14.Visibility = Visibility.Visible;
                tbso15.Visibility = Visibility.Visible;
                tbso16.Visibility = Visibility.Visible;
                tbso17.Visibility = Visibility.Visible;
                tbso18.Visibility = Visibility.Visible;
                tbso19.Visibility = Visibility.Visible;
                tbso20.Visibility = Visibility.Visible;
                tbso21.Visibility = Visibility.Visible;
                tbso22.Visibility = Visibility.Visible; 
                #endregion

                #region enable
                c02a3.IsEnabled = false;
                c006d.IsEnabled = false;
                c__91.IsEnabled = false;
                c0281.IsEnabled = false;
                c__163.IsEnabled = false;
                c__218.IsEnabled = false;
                c__216.IsEnabled = false;
                c0263.IsEnabled = false;
                c__161.IsEnabled = false;
                c__238.IsEnabled = false;
                c__214.IsEnabled = false;
                c__35.IsEnabled = false;
                c__139.IsEnabled = false;
                c__88.IsEnabled = false;
                v0153.IsEnabled = false;
                c0256.IsEnabled = false;
                c__117.IsEnabled = false;
                c__66.IsEnabled = false;
                c__186.IsEnabled = false;
                c006e.IsEnabled = false;
                c__97.IsEnabled = false;
                c__43.IsEnabled = false;
                c__169.IsEnabled = false;
                c0076.IsEnabled = false;
                c__149.IsEnabled = false;
                c__226.IsEnabled = false;
                v0276.IsEnabled = false;
                c__25.IsEnabled = false;
                c026d.IsEnabled = false;
                c__75.IsEnabled = false;
                c__189.IsEnabled = false;
                c0274.IsEnabled = false;
                c0279.IsEnabled = false;
                c__51.IsEnabled = false;
                c__178.IsEnabled = false;
                c029d.IsEnabled = false;
                c__159.IsEnabled = false;
                c__236.IsEnabled = false;
                c__212.IsEnabled = false;
                c02a4.IsEnabled = false;
                c__133.IsEnabled = false;
                c2c71.IsEnabled = false;
                c__196.IsEnabled = false;
                c__20.IsEnabled = false;
                c__111.IsEnabled = false;
                c__61.IsEnabled = false;
                c__182.IsEnabled = false;
                c__32.IsEnabled = false;
                c__135.IsEnabled = false;
                c__85.IsEnabled = false;
                c__198.IsEnabled = false;
                c0064.IsEnabled = false;
                c__113.IsEnabled = false;
                c0042.IsEnabled = false;
                v028f.IsEnabled = false;
                c0271.IsEnabled = false;
                c__93.IsEnabled = false;
                c0295.IsEnabled = false;
                c__165.IsEnabled = false;
                c__41.IsEnabled = false;
                c__145.IsEnabled = false;
                c__222.IsEnabled = false;
                v0254.IsEnabled = false;
                c0047.IsEnabled = false;
                c006c.IsEnabled = false;
                c__71.IsEnabled = false;
                v00f8.IsEnabled = false;
                c0272.IsEnabled = false;
                c028b.IsEnabled = false;
                c026e.IsEnabled = false;
                c__175.IsEnabled = false;
                c0292.IsEnabled = false;
                c__155.IsEnabled = false;
                c__232.IsEnabled = false;
                v0252.IsEnabled = false;
                c__30.IsEnabled = false;
                c__129.IsEnabled = false;
                c__80.IsEnabled = false;
                c__194.IsEnabled = false;
                c0062.IsEnabled = false;
                c006a.IsEnabled = false;
                c__57.IsEnabled = false;
                c__180.IsEnabled = false;
                c__37.IsEnabled = false;
                c__141.IsEnabled = false;
                c027d.IsEnabled = false;
                c__201.IsEnabled = false;
                c025f.IsEnabled = false;
                c__119.IsEnabled = false;
                c0072.IsEnabled = false;
                v028a.IsEnabled = false;
                c__8.IsEnabled = false;
                c__99.IsEnabled = false;
                c__45.IsEnabled = false;
                c__171.IsEnabled = false;
                c00f0.IsEnabled = false;
                c__151.IsEnabled = false;
                c__228.IsEnabled = false;
                c__206.IsEnabled = false;
                c__27.IsEnabled = false;
                c028e.IsEnabled = false;
                c0052.IsEnabled = false;
                v006f.IsEnabled = false;
                c__14.IsEnabled = false;
                c__108.IsEnabled = false;
                c__53.IsEnabled = false;
                v0079.IsEnabled = false;
                c__33_2.IsEnabled = false;
                c__137.IsEnabled = false;
                c027e.IsEnabled = false;
                c__199.IsEnabled = false;
                c__22.IsEnabled = false;
                c__115.IsEnabled = false;
                c__64.IsEnabled = false;
                c__184.IsEnabled = false;
                c__4.IsEnabled = false;
                c__95.IsEnabled = false;
                c0266.IsEnabled = false;
                c__167.IsEnabled = false;
                c03b2.IsEnabled = false;
                c__147.IsEnabled = false;
                c__224.IsEnabled = false;
                c__204.IsEnabled = false;
                c__24.IsEnabled = false;
                c__124.IsEnabled = false;
                c__73.IsEnabled = false;
                v0275.IsEnabled = false;
                c014b.IsEnabled = false;
                c__105.IsEnabled = false;
                c__49.IsEnabled = false;
                c__177.IsEnabled = false;
                c0290.IsEnabled = false;
                c__157.IsEnabled = false;
                c__234.IsEnabled = false;
                c__210.IsEnabled = false;
                c__131.IsEnabled = false;
                c__82.IsEnabled = false;
                v025e.IsEnabled = false;
                c__18.IsEnabled = false;
                c0270.IsEnabled = false;
                c__59.IsEnabled = false;
                v0075.IsEnabled = false;
                c__39.IsEnabled = false;
                c__143.IsEnabled = false;
                c__220.IsEnabled = false;
                c__202.IsEnabled = false;
                c0261.IsEnabled = false;
                c__121.IsEnabled = false;
                c__69.IsEnabled = false;
                c__188.IsEnabled = false;
                c0273.IsEnabled = false;
                c__102.IsEnabled = false;
                c__47.IsEnabled = false;
                c__173.IsEnabled = false;
                c007a.IsEnabled = false;
                c__230.IsEnabled = false;
                c__207.IsEnabled = false;
                c__28.IsEnabled = false;
                c024c.IsEnabled = false;
                c__78.IsEnabled = false;
                c__192.IsEnabled = false;
                c__16.IsEnabled = false;
                c027b.IsEnabled = false;
                c__55.IsEnabled = false;
                v0289.IsEnabled = false;
                c__153.IsEnabled = false;
                #endregion

                #region FontSize
                c02a3.FontSize = 7;
                c006d.FontSize = 7;
                c__91.FontSize = 7;
                c0281.FontSize = 7;
                c__163.FontSize = 7;
                c__218.FontSize = 7;
                c__216.FontSize = 7;
                c0263.FontSize = 7;
                c__161.FontSize = 7;
                c__238.FontSize = 7;
                c__214.FontSize = 7;
                c__35.FontSize = 7;
                c__139.FontSize = 7;
                c__88.FontSize = 7;
                v0153.FontSize = 7;
                c0256.FontSize = 7;
                c__117.FontSize = 7;
                c__66.FontSize = 7;
                c__186.FontSize = 7;
                c006e.FontSize = 7;
                c__97.FontSize = 7;
                c__43.FontSize = 7;
                c__169.FontSize = 7;
                c0076.FontSize = 7;
                c__149.FontSize = 7;
                c__226.FontSize = 7;
                v0276.FontSize = 7;
                c__25.FontSize = 7;
                c026d.FontSize = 7;
                c__75.FontSize = 7;
                c__189.FontSize = 7;
                c0274.FontSize = 7;
                c0279.FontSize = 7;
                c__51.FontSize = 7;
                c__178.FontSize = 7;
                c029d.FontSize = 7;
                c__159.FontSize = 7;
                c__236.FontSize = 7;
                c__212.FontSize = 7;
                c02a4.FontSize = 7;
                c__133.FontSize = 7;
                c2c71.FontSize = 7;
                c__196.FontSize = 7;
                c__20.FontSize = 7;
                c__111.FontSize = 7;
                c__61.FontSize = 7;
                c__182.FontSize = 7;
                c__32.FontSize = 7;
                c__135.FontSize = 7;
                c__85.FontSize = 7;
                c__198.FontSize = 7;
                c0064.FontSize = 7;
                c__113.FontSize = 7;
                c0042.FontSize = 7;
                v028f.FontSize = 7;
                c0271.FontSize = 7;
                c__93.FontSize = 7;
                c0295.FontSize = 7;
                c__165.FontSize = 7;
                c__41.FontSize = 7;
                c__145.FontSize = 7;
                c__222.FontSize = 7;
                v0254.FontSize = 7;
                c0047.FontSize = 7;
                c006c.FontSize = 7;
                c__71.FontSize = 7;
                v00f8.FontSize = 7;
                c0272.FontSize = 7;
                c028b.FontSize = 7;
                c026e.FontSize = 7;
                c__175.FontSize = 7;
                c0292.FontSize = 7;
                c__155.FontSize = 7;
                c__232.FontSize = 7;
                v0252.FontSize = 7;
                c__30.FontSize = 7;
                c__129.FontSize = 7;
                c__80.FontSize = 7;
                c__194.FontSize = 7;
                c0062.FontSize = 7;
                c006a.FontSize = 7;
                c__57.FontSize = 7;
                c__180.FontSize = 7;
                c__37.FontSize = 7;
                c__141.FontSize = 7;
                c027d.FontSize = 7;
                c__201.FontSize = 7;
                c025f.FontSize = 7;
                c__119.FontSize = 7;
                c0072.FontSize = 7;
                v028a.FontSize = 7;
                c__8.FontSize = 7;
                c__99.FontSize = 7;
                c__45.FontSize = 7;
                c__171.FontSize = 7;
                c00f0.FontSize = 7;
                c__151.FontSize = 7;
                c__228.FontSize = 7;
                c__206.FontSize = 7;
                c__27.FontSize = 7;
                c028e.FontSize = 7;
                c0052.FontSize = 7;
                v006f.FontSize = 7;
                c__14.FontSize = 7;
                c__108.FontSize = 7;
                c__53.FontSize = 7;
                v0079.FontSize = 7;
                c__33_2.FontSize = 7;
                c__137.FontSize = 7;
                c027e.FontSize = 7;
                c__199.FontSize = 7;
                c__22.FontSize = 7;
                c__115.FontSize = 7;
                c__64.FontSize = 7;
                c__184.FontSize = 7;
                c__4.FontSize = 7;
                c__95.FontSize = 7;
                c0266.FontSize = 7;
                c__167.FontSize = 7;
                c03b2.FontSize = 7;
                c__147.FontSize = 7;
                c__224.FontSize = 7;
                c__204.FontSize = 7;
                c__24.FontSize = 7;
                c__124.FontSize = 7;
                c__73.FontSize = 7;
                v0275.FontSize = 7;
                c014b.FontSize = 7;
                c__105.FontSize = 7;
                c__49.FontSize = 7;
                c__177.FontSize = 7;
                c0290.FontSize = 7;
                c__157.FontSize = 7;
                c__234.FontSize = 7;
                c__210.FontSize = 7;
                c__131.FontSize = 7;
                c__82.FontSize = 7;
                v025e.FontSize = 7;
                c__18.FontSize = 7;
                c0270.FontSize = 7;
                c__59.FontSize = 7;
                v0075.FontSize = 7;
                c__39.FontSize = 7;
                c__143.FontSize = 7;
                c__220.FontSize = 7;
                c__202.FontSize = 7;
                c0261.FontSize = 7;
                c__121.FontSize = 7;
                c__69.FontSize = 7;
                c__188.FontSize = 7;
                c0273.FontSize = 7;
                c__102.FontSize = 7;
                c__47.FontSize = 7;
                c__173.FontSize = 7;
                c007a.FontSize = 7;
                c__230.FontSize = 7;
                c__207.FontSize = 7;
                c__28.FontSize = 7;
                c024c.FontSize = 7;
                c__78.FontSize = 7;
                c__192.FontSize = 7;
                c__16.FontSize = 7;
                c027b.FontSize = 7;
                c__55.FontSize = 7;
                v0289.FontSize = 7;
                c__15.FontSize = 7;
                c__1.FontSize = 7;
                c02a6.FontSize = 7;
                c__90.FontSize = 7;
                c03c7.FontSize = 7;
                c__162.FontSize = 7;
                c__217.FontSize = 7;
                c__215.FontSize = 7;
                c0078.FontSize = 7;
                c__160.FontSize = 7;
                c__237.FontSize = 7;
                c__213.FontSize = 7;
                c__34.FontSize = 7;
                c__138.FontSize = 7;
                c__87.FontSize = 7;
                c__200.FontSize = 7;
                c0288.FontSize = 7;
                c__116.FontSize = 7;
                c__65.FontSize = 7;
                c__185.FontSize = 7;
                c__5.FontSize = 7;
                c__96.FontSize = 7;
                c__42.FontSize = 7;
                c__168.FontSize = 7;
                c0066.FontSize = 7;
                c__148.FontSize = 7;
                c__225.FontSize = 7;
                c__205.FontSize = 7;
                c0294.FontSize = 7;
                c__125.FontSize = 7;
                c__74.FontSize = 7;
                v0264.FontSize = 7;
                c__12.FontSize = 7;
                c__106.FontSize = 7;
                c__50.FontSize = 7;
                v0069.FontSize = 7;
                c00e7.FontSize = 7;
                c__158.FontSize = 7;
                c__235.FontSize = 7;
                c__211.FontSize = 7;
                c02a7.FontSize = 7;
                c__132.FontSize = 7;
                c__83.FontSize = 7;
                c__195.FontSize = 7;
                c__19.FontSize = 7;
                c__110.FontSize = 7;
                c__60.FontSize = 7;
                v026a.FontSize = 7;
                c__31.FontSize = 7;
                c__134.FontSize = 7;
                c__84.FontSize = 7;
                c__197.FontSize = 7;
                c0074.FontSize = 7;
                c__112.FontSize = 7;
                c__62.FontSize = 7;
                v0269.FontSize = 7;
                c__2.FontSize = 7;
                c__92.FontSize = 7;
                c0127.FontSize = 7;
                c__164.FontSize = 7;
                c__40.FontSize = 7;
                c__144.FontSize = 7;
                c__221.FontSize = 7;
                c__203.FontSize = 7;
                c0071.FontSize = 7;
                c__122.FontSize = 7;
                c__70.FontSize = 7;
                v0258.FontSize = 7;
                c__10.FontSize = 7;
                c__103.FontSize = 7;
                c026c.FontSize = 7;
                c__174.FontSize = 7;
                c0283.FontSize = 7;
                c__154.FontSize = 7;
                c__231.FontSize = 7;
                c__208.FontSize = 7;
                c__29.FontSize = 7;
                c__128.FontSize = 7;
                c__79.FontSize = 7;
                c__193.FontSize = 7;
                c0070.FontSize = 7;
                c0265.FontSize = 7;
                c__56.FontSize = 7;
                v026f.FontSize = 7;
                c__36.FontSize = 7;
                c__140.FontSize = 7;
                c__89.FontSize = 7;
                v0250.FontSize = 7;
                c0063.FontSize = 7;
                c__118.FontSize = 7;
                c__67.FontSize = 7;
                c__187.FontSize = 7;
                c__7.FontSize = 7;
                c__98.FontSize = 7;
                c__44.FontSize = 7;
                c__170.FontSize = 7;
                c03b8.FontSize = 7;
                c__150.FontSize = 7;
                c__227.FontSize = 7;
                v0061.FontSize = 7;
                c__26.FontSize = 7;
                c__126.FontSize = 7;
                c__76.FontSize = 7;
                c__190.FontSize = 7;
                c__13.FontSize = 7;
                c__107.FontSize = 7;
                c__52.FontSize = 7;
                c__179.FontSize = 7;
                c__33.FontSize = 7;
                c__136.FontSize = 7;
                c__86.FontSize = 7;
                v025b.FontSize = 7;
                c__21.FontSize = 7;
                c__114.FontSize = 7;
                c__63.FontSize = 7;
                c__183.FontSize = 7;
                c__3.FontSize = 7;
                c__94.FontSize = 7;
                c0068.FontSize = 7;
                c__166.FontSize = 7;
                c0278.FontSize = 7;
                c__146.FontSize = 7;
                c__223.FontSize = 7;
                v00e6.FontSize = 7;
                c__23.FontSize = 7;
                c__123.FontSize = 7;
                c__72.FontSize = 7;
                v0259.FontSize = 7;
                c__11.FontSize = 7;
                c__104.FontSize = 7;
                c__48.FontSize = 7;
                c__176.FontSize = 7;
                c0282.FontSize = 7;
                c__156.FontSize = 7;
                c__233.FontSize = 7;
                c__209.FontSize = 7;
                c024c.FontSize = 7;
                c__130.FontSize = 7;
                c__81.FontSize = 7;
                v025c.FontSize = 7;
                c__17.FontSize = 7;
                c0077.FontSize = 7;
                c__58.FontSize = 7;
                c__181.FontSize = 7;
                c__38.FontSize = 7;
                c__142.FontSize = 7;
                c__21.FontSize = 7;
                v028c.FontSize = 7;
                c006b.FontSize = 7;
                c__120.FontSize = 7;
                c__68.FontSize = 7;
                v0065.FontSize = 7;
                c__9.FontSize = 7;
                c__101.FontSize = 7;
                c__46.FontSize = 7;
                c__172.FontSize = 7;
                c0073.FontSize = 7;
                c__152.FontSize = 7;
                c__229.FontSize = 7;
                v0251.FontSize = 7;
                c____.FontSize = 7;
                c__127.FontSize = 7;
                c__77.FontSize = 7;
                c__191.FontSize = 7;
                c__15.FontSize = 7;
                c__109.FontSize = 7;
                c__54.FontSize = 7;
                v0268.FontSize = 7;
                #endregion

                #region opacity --
                c__1.Opacity = 1;
                c__2.Opacity = 1;
                c__3.Opacity = 1;
                c__4.Opacity = 1;
                c__5.Opacity = 1;
                c__7.Opacity = 1;
                c__8.Opacity = 1;
                c__9.Opacity = 1;
                c__10.Opacity = 1;
                c__11.Opacity = 1;
                c__12.Opacity = 1;
                c__13.Opacity = 1;
                c__14.Opacity = 1;
                c__15.Opacity = 1;
                c__16.Opacity = 1;
                c__17.Opacity = 1;
                c__18.Opacity = 1;
                c__19.Opacity = 1;
                c__20.Opacity = 1;
                c__21.Opacity = 1;
                c__22.Opacity = 1;
                c__23.Opacity = 1;
                c__24.Opacity = 1;
                c__25.Opacity = 1;
                c__26.Opacity = 1;
                c__27.Opacity = 1;
                c__28.Opacity = 1;
                c__29.Opacity = 1;
                c__30.Opacity = 1;
                c__31.Opacity = 1;
                c__32.Opacity = 1;
                c__33.Opacity = 1;
                c__33_2.Opacity = 1;
                c__34.Opacity = 1;
                c__35.Opacity = 1;
                c__36.Opacity = 1;
                c__37.Opacity = 1;
                c__38.Opacity = 1;
                c__39.Opacity = 1;
                c__40.Opacity = 1;
                c__41.Opacity = 1;
                c__42.Opacity = 1;
                c__43.Opacity = 1;
                c__44.Opacity = 1;
                c__45.Opacity = 1;
                c__46.Opacity = 1;
                c__47.Opacity = 1;
                c__48.Opacity = 1;
                c__49.Opacity = 1;
                c__50.Opacity = 1;
                c__51.Opacity = 1;
                c__52.Opacity = 1;
                c__53.Opacity = 1;
                c__54.Opacity = 1;
                c__55.Opacity = 1;
                c__56.Opacity = 1;
                c__57.Opacity = 1;
                c__58.Opacity = 1;
                c__59.Opacity = 1;
                c__60.Opacity = 1;
                c__61.Opacity = 1;
                c__62.Opacity = 1;
                c__63.Opacity = 1;
                c__64.Opacity = 1;
                c__65.Opacity = 1;
                c__66.Opacity = 1;
                c__67.Opacity = 1;
                c__68.Opacity = 1;
                c__69.Opacity = 1;
                c__70.Opacity = 1;
                c__71.Opacity = 1;
                c__72.Opacity = 1;
                c__73.Opacity = 1;
                c__74.Opacity = 1;
                c__75.Opacity = 1;
                c__76.Opacity = 1;
                c__77.Opacity = 1;
                c__78.Opacity = 1;
                c__79.Opacity = 1;
                c__80.Opacity = 1;
                c__81.Opacity = 1;
                c__82.Opacity = 1;
                c__83.Opacity = 1;
                c__84.Opacity = 1;
                c__85.Opacity = 1;
                c__86.Opacity = 1;
                c__87.Opacity = 1;
                c__88.Opacity = 1;
                c__89.Opacity = 1;
                c__90.Opacity = 1;
                c__91.Opacity = 1;
                c__92.Opacity = 1;
                c__93.Opacity = 1;
                c__94.Opacity = 1;
                c__95.Opacity = 1;
                c__96.Opacity = 1;
                c__97.Opacity = 1;
                c__98.Opacity = 1;
                c__99.Opacity = 1;
                c__101.Opacity = 1;
                c__102.Opacity = 1;
                c__103.Opacity = 1;
                c__104.Opacity = 1;
                c__105.Opacity = 1;
                c__106.Opacity = 1;
                c__107.Opacity = 1;
                c__108.Opacity = 1;
                c__109.Opacity = 1;
                c__110.Opacity = 1;
                c__111.Opacity = 1;
                c__112.Opacity = 1;
                c__113.Opacity = 1;
                c__114.Opacity = 1;
                c__115.Opacity = 1;
                c__116.Opacity = 1;
                c__117.Opacity = 1;
                c__118.Opacity = 1;
                c__119.Opacity = 1;
                c__120.Opacity = 1;
                c__121.Opacity = 1;
                c__122.Opacity = 1;
                c__123.Opacity = 1;
                c__124.Opacity = 1;
                c__125.Opacity = 1;
                c__126.Opacity = 1;
                c__127.Opacity = 1;
                c__128.Opacity = 1;
                c__129.Opacity = 1;
                c__130.Opacity = 1;
                c__131.Opacity = 1;
                c__132.Opacity = 1;
                c__133.Opacity = 1;
                c__139.Opacity = 1;
                c__141.Opacity = 1;
                c__155.Opacity = 1;
                c__165.Opacity = 1;
                c__174.Opacity = 1;
                c__175.Opacity = 1;
                c__176.Opacity = 1;
                c__177.Opacity = 1;
                c__178.Opacity = 1;
                c__179.Opacity = 1;
                c__180.Opacity = 1;
                c__181.Opacity = 1;
                c__182.Opacity = 1;
                c__183.Opacity = 1;
                c__184.Opacity = 1;
                c__185.Opacity = 1;
                c__186.Opacity = 1;
                c__187.Opacity = 1;
                c__188.Opacity = 1;
                c__189.Opacity = 1;
                c__190.Opacity = 1;
                c__191.Opacity = 1;
                c__192.Opacity = 1;
                c__193.Opacity = 1;
                c__194.Opacity = 1;
                c__195.Opacity = 1;
                c__196.Opacity = 1;
                c__197.Opacity = 1;
                c__198.Opacity = 1;
                c__199.Opacity = 1;
                c__200.Opacity = 1;
                c__201.Opacity = 1;
                c__202.Opacity = 1;
                c__203.Opacity = 1;
                c__204.Opacity = 1;
                c__205.Opacity = 1;
                c__206.Opacity = 1;
                c__207.Opacity = 1;
                c__208.Opacity = 1;
                c__218.Opacity = 1;
                c__224.Opacity = 1;
                #endregion

                btShift.Visibility = Visibility.Visible;

                btText = rl.GetString("nascondiLab");
                btShowLab.Content = btText;
            }

            if (licenseInformation.IsActive)
            {
                if (licenseInformation.IsTrial)
                {
                    disableVocali();
                }
            }
        }

        private void btShowBt_Click(object sender, RoutedEventArgs e)
        {
            bool nascondi;
            string btText;

            nascondi = (vbBottoni.Visibility == Visibility.Visible || _bottoni.Visibility == Visibility.Visible);
            if (nascondi)
            {
                if (Window.Current.Bounds.Width < Window.Current.Bounds.Height)
                {
                    _bottoni.Visibility = Visibility.Collapsed;
                }
                else
                {
                    vbBottoni.Visibility = Visibility.Collapsed;
                }
                btText = rl.GetString("mostraBt");
                btShowBt.Content = btText;
            }
            else
            {
                if (Window.Current.Bounds.Width < Window.Current.Bounds.Height)
                {
                    _bottoni.Visibility = Visibility.Visible;
                }
                else
                {
                    vbBottoni.Visibility = Visibility.Visible;
                }
                btText = rl.GetString("nascondiBt");
                btShowBt.Content = btText;
            }
        }

        private int contaBackslash(string s)
        {
            char[] charArr = s.ToCharArray();
            int n = 0;
            for(int i =0;i<s.Length;i++)
            {
                if (charArr[i].Equals('\r'))
                    n += 1;
            }
            return n;
        }
    }
}
