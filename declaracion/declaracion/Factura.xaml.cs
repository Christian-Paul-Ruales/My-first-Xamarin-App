using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tesseract;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
//using System.Web.UI.WebControls.Image;

namespace declaracion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Factura : ContentPage
    {
        public string usuario { get; set; }
        public string id_factura { get; set; }

        public string imagen { get; set; }


        ////////////////
        public int usuario_id { get; set; }
        public int vendedor_id { get; set; }
        public int cliente_id { get; set; }
        public int tipo_declaracion_id { get; set; }
        public double iva_total { get; set; }
        public double valor_total { get; set; }
        public Factura(factura f,string user, string id_f)
        {
            InitializeComponent();
            this.usuario = user;

            CameraButton.Clicked += EscanearFactura;
            btnAddProduct.Clicked += AnadirProducto;
            btnSave.Clicked += GuardarFactura;
            if (id_f == "n")
            {
                guardarFactura(user);
            }
            else {
                this.id_factura = id_f;
                lstProductos.ItemsSource = GetProductos(id_factura);
                total(this.id_factura);
                if (f.id_factura != 0)
                {
                    lblIDFactura.Text = f.id_factura.ToString();
                    // txtCIcliente.Text = 
                    txtIva.Text = f.iva.ToString();
                    txtTotal.Text = f.total.ToString();

                }
                else {
                    
                    lblIDFactura.Text = this.id_factura;

                    
                }
               

            }
            
            cliente_o_vendedor(user);

            ////////////////////////////////////////////////////////
            ///

           
           
        }

        

        private void GuardarFactura(object sender, EventArgs e)
        {


            WebClient Webclient = new WebClient();
            try
            {
                string path = this.imagen;
                // webclient.UploadDataAsync(uri, mybyte);
                byte[] result = Webclient.UploadFile("http://192.168.1.4/php_declaracion/cargar.php", "POST", path);
                string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
                 DisplayAlert("Estado", s, "Aceptar");
            }
            catch (Exception except)
            {
                 DisplayAlert("No se ha podido subir la imagen", except.Message, ":(");

            }


            WebClient client = new WebClient();

            Uri uri = new Uri("http://192.168.1.4/php_declaracion/guardar_factura.php");
            
            NameValueCollection parameters = new NameValueCollection();
            
            parameters.Add("id_factura", this.id_factura);

            parameters.Add("cedula", txtCIcliente.Text);
            parameters.Add("RUC", txtRUCcliente.Text);
            parameters.Add("imagen", imagen);
            int id_tipo_declaracion = 0;
            if (pcTipoDeclaracion.SelectedIndex==0) {
                id_tipo_declaracion = 1;

            }
            if (pcTipoDeclaracion.SelectedIndex == 1)
            {
                id_tipo_declaracion = 2;

            }
            parameters.Add("declaracion", id_tipo_declaracion.ToString());


            int id_tipo_factura = 0;

            if (pcTipoFactura.SelectedIndex == 0)
            {
                id_tipo_factura = 1;

            }
            if (pcTipoFactura.SelectedIndex == 1)
            {
                id_tipo_factura = 2;

            }
            if (pcTipoFactura.SelectedIndex == 2)
            {
                id_tipo_factura = 3;

            }
            if (pcTipoFactura.SelectedIndex == 3)
            {
                id_tipo_factura = 4;

            }
            if (pcTipoFactura.SelectedIndex == 4)
            {
                id_tipo_factura = 5;

            }

            parameters.Add("tipo_factura", id_tipo_factura.ToString());

            parameters.Add("iva", txtIva.Text);
            parameters.Add("total", txtTotal.Text);

            parameters.Add("imagen",imagen);


            string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri, parameters));

            DisplayAlert("pagesource", pagesource, "Aceptar");
            //var json = JsonConvert.DeserializeObject(pagesource);
            //JArray arreglo = new JArray(json);

            // WebClient webclient = new WebClient();

            Navigation.PushAsync(new lstFacturas(usuario));


        }

        const string URLinsertUser = "http://192.168.1.4/php_declaracion/cliente_vendedor.php";

        public void total(string id_factura) {
            WebClient client = new WebClient();

            Uri uri = new Uri("http://192.168.1.4/php_declaracion/total_factura.php");

            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("id_factura", this.id_factura);


            string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri, parameters));
            //DisplayAlert("pagesource", pagesource, "Aceptar");
            var json = JsonConvert.DeserializeObject(pagesource);
            JArray arreglo = new JArray(json);
            string sin_iva = arreglo[0]["valor_semi"].ToString();
            string iva_total = arreglo[0]["iva_total"].ToString();
            string total = arreglo[0]["valor_total"].ToString();
            // DisplayAlert("Variables",sin_iva+" "+iva_total+" "+total,"Aceptar");
            txtIva.Text=iva_total;
            txtTotal.Text = total;
        }
        public  List<producto> GetProductos(string id_factura) {
            List<producto> auxiliar = new List<producto>();

            WebClient client = new WebClient();

            Uri uri = new Uri("http://192.168.1.4/php_declaracion/retornar_productos.php");

            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("id_factura", id_factura);


            string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri, parameters));

             DisplayAlert("retorno", pagesource,"aceptar");

            var json = JsonConvert.DeserializeObject(pagesource);
            JArray arreglo = new JArray(json);

            //JArray arr = arreglo[0];

            foreach ( JArray arr in arreglo) {
                for (int i = 0; i < arr.Count; i++)
                {
                    producto p = new producto(Convert.ToInt32(arr[i]["id_producto"]), Convert.ToInt32(arr[i]["factura_id"]), arr[i]["detalle"].ToString(),
                    Convert.ToDouble(arr[i]["valor_individual"]), Convert.ToDouble(arr[i]["iva"]), Convert.ToInt32(arr[i]["cantidad_total"]),
                    Convert.ToDouble(arr[i]["valor_total"]));
                    // arreglo.Add(p);
                    auxiliar.Add(p);


                }

                
            }

            

            //string id_factur = arreglo[0]["mf"].ToString();


            return auxiliar;


        }



        public void ModificarProducto(object sender, ItemTappedEventArgs e)
        {
            var myListView = (ListView)sender;
            producto myItem = (producto) myListView.SelectedItem;

             Navigation.PushAsync(new updateProducto( myItem, this.usuario));
            
           // await DisplayAlert("Producto", myItem.id_producto+ " "+myItem.detalle, "Aceptar");
            //int index = lstProductos.inde
            }


        public void guardarFactura(string usuario) {

            try
            {
                WebClient client = new WebClient();

                Uri uri = new Uri("http://192.168.1.4/php_declaracion/crearFactura.php");

                NameValueCollection parameters = new NameValueCollection();

                parameters.Add("usuario", usuario);
               

                string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri, parameters));

                // DisplayAlert("retorno", pagesource,"aceptar");

                var json = JsonConvert.DeserializeObject(pagesource);
                JArray arreglo = new JArray(json);
                string id_factur = arreglo[0]["mf"].ToString();
                lblIDFactura.Text = id_factur;
                this.id_factura = id_factur;
            }
            catch (Exception excep) {
                DisplayAlert("Excepsion agregar factura", excep.Message, "Entendido");
            }

        }
        public void cliente_o_vendedor(string usuario) {
            //txtRUCcliente.Text = "sexo";
            try { 
            WebClient client = new WebClient();

            Uri uri = new Uri(URLinsertUser);

            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("usuario", usuario);

            string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri, parameters));
           // await DisplayAlert("retorno", pagesource,"aceptar");
            var json = JsonConvert.DeserializeObject(pagesource);
            JArray arreglo = new JArray(json);
            string declaracion = arreglo[0][0].ToString();
                if(declaracion == "renta"){
                    txtCIcliente.Text = arreglo[0][1].ToString();
                    pcTipoDeclaracion.SelectedIndex = 0;
                }
                if (declaracion == "iva")
                {
                    txtCIcliente.Text = arreglo[0][1].ToString();
                    pcTipoDeclaracion.SelectedIndex = 1;
                }
                //txtRUCcliente.Text = "final";

            }
            catch (Exception ex) {
               //DisplayAlert("retorno", ex.Message, "aceptar");
                txtRUCcliente.Text = ex.Message;
            }


        }
        private async void AnadirProducto(object sender, EventArgs e)
        {
         
            await Navigation.PushAsync(new Producto(usuario,id_factura));

        }

        private async void EscanearFactura(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() { SaveToAlbum=true, Name="fact.jpg" });

            this.imagen = photo.Path;
            

            PhotoImage.Source = ImageSource.FromStream(() =>
            {
                var stream = photo.GetStream();
                photo.Dispose();
                return stream;
            });
            // var seven = photo.Path;

            lblDirectorio.Text = this.imagen;



        }

      

     

    }
}