using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace declaracion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class updateProducto : ContentPage
    {
        public producto producto { get; set; }
        public string id_factura { get; set; }
        public string id_usuario { get; set; }
        public updateProducto(producto prod,string id_user)
        {
            InitializeComponent();
            this.producto = prod;
            this.id_factura = prod.factura_id.ToString();
            this.id_usuario = id_user;
            
            lbl_id_factura.Text = prod.factura_id.ToString();
            txtUProductoN.Text = prod.detalle;
            txtUPrecio_Unidad.Text = prod.valor_individual.ToString();
            USLDCantidad.Value = prod.cantidad_total;
            txtUiva_total.Text = prod.iva.ToString();
            txtUcostofinal.Text = prod.valor_total.ToString();

            btnActualizarProducto.Clicked += BtnActualizarProducto_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;

        }

        private async void  BtnEliminar_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Eliminar Producto", "Estas seguro de Eliminar "+producto.detalle+"?", "SI", "NO");
            if (answer==true) { 
            WebClient client = new WebClient();

            Uri uri = new Uri("http://192.168.1.4/php_declaracion/eliminar_producto.php");



            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("id_producto", producto.id_producto.ToString());
                parameters.Add("detalle", producto.detalle);
                string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri, parameters));
                await DisplayAlert("Aviso", pagesource, "Aceptar");
                factura fac = new factura(0,0,0,0,0,0,0,"");
                await Navigation.PushAsync(new Factura(fac, this.id_usuario, this.id_factura));
            }
            //throw new NotImplementedException();
        }

        private void BtnActualizarProducto_Clicked(object sender, EventArgs e)
        {
            string id_factura = lbl_id_factura.Text;
            string detalle = txtUProductoN.Text;
            string valor_individual = txtUPrecio_Unidad.Text;
            string iva = txtUiva_total.Text;
            string Cantidad = USLDCantidad.Value.ToString();
            string ValorTotal = txtUcostofinal.Text;

            try
            {
                WebClient client = new WebClient();

                Uri uri = new Uri("http://192.168.1.4/php_declaracion/actualizar_producto.php");



                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("id_producto", producto.id_producto.ToString());
                parameters.Add("factura_id", id_factura);
                parameters.Add("detalle", detalle);
                parameters.Add("valor_individual", valor_individual);
                parameters.Add("iva", iva);
                parameters.Add("cantidad_total", Cantidad);
                parameters.Add("valor_total", ValorTotal);


                string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri, parameters));
                DisplayAlert("Aviso", pagesource, "Aceptar");
                factura fa = new factura(0,0,0,0,0,0,0,"");
                Navigation.PushAsync(new Factura(fa,this.id_usuario, this.id_factura));

                // await DisplayAlert("retorno", pagesource,"aceptar");
                //  var json = JsonConvert.DeserializeObject(pagesource);



            }
            catch (Exception exs)
            {


                DisplayAlert("Error", exs.ToString(), "Aceptar");
            }
            //throw new NotImplementedException();
        }

        void uOnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            //DisplayAlert("HJJH",SLDCantidad.Value+"","JJJ");


            double valor_individual = Convert.ToDouble(txtUPrecio_Unidad.Text);
            double cantidad = Convert.ToDouble(USLDCantidad.Value);
            double precio_sin_iva = (valor_individual * cantidad);
            double iva = ((precio_sin_iva * 12) / 100);
            double total = precio_sin_iva + iva;

            txtUiva_total.Text = iva.ToString();

            txtUcostofinal.Text = total.ToString();
        }

        void UEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUPrecio_Unidad.Text.Equals("") == false && USLDCantidad.Value > 0)
            {

                double valor_individual = Convert.ToDouble(txtUPrecio_Unidad.Text);
                double cantidad = Convert.ToInt32(USLDCantidad.Value);
                double precio_sin_iva = (valor_individual * cantidad);
                double iva = ((precio_sin_iva * 12) / 100);
                double total = precio_sin_iva + iva;

                txtUiva_total.Text = iva.ToString();

                txtUcostofinal.Text = total.ToString();
            }
        }

    }
}