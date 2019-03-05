using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace App.Views
{
    public sealed partial class MainPage : Page
    {
        public string Title;
        public MainPage()
        {
            this.InitializeComponent();
            Navigation.Header = "Inicio";
        }
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Error: " + e.SourcePageType.FullName);
        }

        private void Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                Navigation.Header = "Configuración";
                rootFrame.Navigate(typeof(Settings));
            }
            else
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                string pageName = ((string)selectedItem.Tag);
                sender.Header = pageName;
                switch (pageName)
                {
                    case "Empleados":
                        rootFrame.Navigate(typeof(Empleados));
                        break;
                    default:
                        this.Frame.Navigate(typeof(MainPage));
                        break;

                }
            }
        }
    }
}
