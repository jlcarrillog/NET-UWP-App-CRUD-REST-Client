using App.Data;
using App.Models;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace App
{
    public sealed partial class EmpleadosEdit : Page
    {
        private Empleado Model = new Empleado();
        private bool nombre = false;
        private bool edad = false;
        public EmpleadosEdit()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                Model.EmpleadoID = (Guid)e.Parameter;
                Datos();
            }
        }
        private async Task Datos()
        {
            Model = await new EmpleadosDataService().Find(Model.EmpleadoID);
            this.DataContext = Model;
            LoadingControl.IsActive = false;
        }
        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EmpleadosDetails), (sender as Button).Tag);
        }
        private async void Guardar(object sender, RoutedEventArgs e)
        {
            LoadingControl.IsActive = true;
            await new EmpleadosDataService().Update(Model);
            this.Frame.Navigate(typeof(EmpleadosDetails), Model.EmpleadoID);
            LoadingControl.IsActive = false;
        }
        private void Nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Nombre.Text))
            {
                nombre = false;
                NombreLabel.Text = Nombre.Header + " es requerido.";
            }
            else
            {
                nombre = true;
                NombreLabel.Text = "";
                Model.Nombre = Nombre.Text;
            }
            if (nombre && edad)
            {
                ButtonGuardar.IsEnabled = true;
            }
            else
            {
                ButtonGuardar.IsEnabled = false;
            }
        }
        private void Edad_TextChanged(object sender, TextChangedEventArgs e)
        {
            int v;
            if (string.IsNullOrWhiteSpace(Edad.Text))
            {
                edad = false;
                EdadLabel.Text = Edad.Header + " es requerido.";
            }
            else if (!int.TryParse(Edad.Text, out v))
            {
                edad = false;
                EdadLabel.Text = Edad.Header + " es numero.";
            }
            else
            {
                edad = true;
                EdadLabel.Text = "";
                Model.Edad = v;

            }
            if (nombre && edad)
            {
                ButtonGuardar.IsEnabled = true;
            }
            else
            {
                ButtonGuardar.IsEnabled = false;
            }
        }
    }
}
