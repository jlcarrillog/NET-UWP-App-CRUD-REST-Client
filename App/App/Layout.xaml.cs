using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Layout : Page
    {
        public string Title;
        public Layout()
        {
            this.InitializeComponent();
            Navigation.Header = "Inicio" ;
            rootFrame.Navigate(typeof(MainPage));
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
                rootFrame.Navigate(typeof(SampleSettings));
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
                        rootFrame.Navigate(typeof(MainPage));
                        break;

                }
            }
        }
    }
}
