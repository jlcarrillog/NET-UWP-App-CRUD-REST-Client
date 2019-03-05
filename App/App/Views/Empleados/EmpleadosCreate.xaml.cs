using App.Data;
using App.Models;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App.Views
{
    public sealed partial class EmpleadosCreate : Page
    {
        private Empleado Model = new Empleado();
        private bool nombre = false;
        private bool edad = false;
        public EmpleadosCreate()
        {
            this.InitializeComponent();
            LoadingControl.IsActive = false;
        }
        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Empleados));
        }
        private async void Guardar(object sender, RoutedEventArgs e)
        {
            LoadingControl.IsActive = true;
            Model.EmpleadoID = Guid.NewGuid();
            await new EmpleadosDataService().Add(Model);
            this.Frame.Navigate(typeof(Empleados));
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
