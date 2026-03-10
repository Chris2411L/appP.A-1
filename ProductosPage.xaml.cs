using System;
using System.Linq;
using Microsoft.Maui.Controls;
using appP.A.Models;
using appP.A.Services;

namespace appP.A
{
    public partial class ProductosPage : ContentPage
    {
        public ProductosPage(Categoria categoria)
        {
            InitializeComponent();
            this.Title = categoria.Nombre;
            // Asegúrate de que AppData.Productos no sea nulo
            ProductosList.ItemsSource = AppData.Productos.Where(p => p.Categoria == categoria.Nombre).ToList();
        }

        private async void OnAgregarAlCarritoClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var producto = (Producto)button.CommandParameter;

            if (producto != null)
            {
                AppData.CarritoActual.Agregar(producto);
                await button.ScaleTo(1.1, 100);
                await button.ScaleTo(1.0, 100);
            }
        }

        private async void OnVerCarritoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarritoPage());
        }
    }
}