using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class Producto : ContentPage
    {
        public string usuario { get; set; }
        public string factura { get; set; }



        ////////////////////////////
        public int id_producto { get; set; }
        public string detalle { get; set; }
        public double valor_individual { get; set; }
        public double iva { get; set; }
        public int cantidad_total { get; set; }
        public double valor_total { get; set; }

        public Producto(string user, string id_factura)
        {
            InitializeComponent();
            this.usuario = user;
            //retornar.Clicked += Retornar_Clicked;
            this.factura = id_factura;
            lbl_id_factura.Text = id_factura;
            btnAgregarProducto.Clicked += GuardarProducto;


        }

        private void GuardarProducto(object sender, EventArgs e)
        {
            string id_factura = lbl_id_factura.Text;
            string detalle = txtProductoN.Text;
            string valor_individual = txtPrecio_Unidad.Text;
            string iva = txtiva_total.Text;
            string Cantidad = SLDCantidad.Value.ToString();
            string ValorTotal = txtcostofinal.Text;

            try {
                WebClient client = new WebClient();

                Uri uri = new Uri("http://192.168.1.4/php_declaracion/guardar_producto.php");

              

                NameValueCollection parameters = new NameValueCollection();

                parameters.Add("id_factura", id_factura);
                parameters.Add("nombre", detalle);
                parameters.Add("valor_individual", valor_individual);
                parameters.Add("iva", iva);
                parameters.Add("cantidad", Cantidad);
                parameters.Add("total", ValorTotal);


                string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri, parameters));
                DisplayAlert("Aviso", pagesource, "Aceptar");
                factura fa = new factura(0,0,0,0,0,0,0,"");
                DisplayAlert("alerta",fa.ToString()+" - "+ this.usuario+" - "+this.factura,"perfecto");
                Navigation.PushAsync(new Factura(fa,this.usuario, this.factura));

                // await DisplayAlert("retorno", pagesource,"aceptar");
                //  var json = JsonConvert.DeserializeObject(pagesource);



            } catch (Exception exs) {


                DisplayAlert("Error", exs.ToString() , "Aceptar");
            }
           


        }
        void GenerarValorTortal(object sender, ValueChangedEventArgs e)
        {
            //DisplayAlert("HJJH",SLDCantidad.Value+"","JJJ");
            

                double valor_individual = Convert.ToDouble(txtPrecio_Unidad.Text);
                double cantidad = Convert.ToDouble(SLDCantidad.Value);
                double precio_sin_iva = (valor_individual * cantidad);
                double iva = ((precio_sin_iva * 12) / 100);
                double total = precio_sin_iva + iva;

                txtiva_total.Text = iva.ToString();

                txtcostofinal.Text = total.ToString();
            
        }
        void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPrecio_Unidad.Text.Equals("")==false && SLDCantidad.Value > 0) {
            
            double valor_individual = Convert.ToDouble(txtPrecio_Unidad.Text);
            double cantidad = Convert.ToInt32(SLDCantidad.Value);
            double precio_sin_iva = (valor_individual*cantidad);
            double iva = ((precio_sin_iva * 12) / 100);
            double total = precio_sin_iva + iva;

            txtiva_total.Text = iva.ToString();

            txtcostofinal.Text = total.ToString();
            }
        }

       
        
    }
}