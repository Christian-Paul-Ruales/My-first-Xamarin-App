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
    public partial class lstFacturas : ContentPage
    {
        public string usuario { get; set; }
        public lstFacturas(string user)
        {
            this.usuario = user;
            
            InitializeComponent();
            lblUsuario.Text = usuario;
            btnAddFacture.Clicked += BtnAddFacture_Clicked;
            lstFacts.ItemsSource = GetFacturas(user);
            btnGenerarDeclaracion.Clicked += BtnGenerarDeclaracion_Clicked;
        }

        private void BtnGenerarDeclaracion_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new declaracion(this.usuario));
        }
        private async void BtnAddFacture_Clicked(object sender, EventArgs e)
        {
            factura f = new factura(0,0,0,0,0,0,0,"");
            GetFacturas(usuario);
           //await DisplayAlert("ayuda", "ayuda:" + usuario, "aceptar");
            await Navigation.PushAsync(new Factura(f,usuario,"n"));
            //throw new NotImplementedException();
        }

        public void lstItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myListView = (ListView)sender;
            factura myItem = (factura)myListView.SelectedItem;

            Navigation.PushAsync(new Factura(myItem,usuario,myItem.id_factura.ToString()));

            // await DisplayAlert("Producto", myItem.id_producto+ " "+myItem.detalle, "Aceptar");
            //int index = lstProductos.inde
        }
        public List<factura> GetFacturas(string usuario)
        {
            
            
            List<factura> auxiliar = new List<factura>();
            try
            {
                WebClient client = new WebClient();

            Uri uri = new Uri("http://192.168.1.4/php_declaracion/retornar_facturas.php");

            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("usuario", usuario);


            string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri, parameters));

         // DisplayAlert("retorno", pagesource, "aceptar");

            var json = JsonConvert.DeserializeObject(pagesource);
            JArray arreglo = new JArray(json);
               
                foreach (JArray facturas in arreglo) {
                    for (int i = 0; i < facturas.Count; i++)
                    {
                        factura factura = new factura(Convert.ToInt32(facturas[i]["id_factura"]), Convert.ToInt32(facturas[i]["usuario_id"]),
                            Convert.ToInt32(facturas[i]["vendedor_id"]), Convert.ToInt32(facturas[i]["cliente_id"]), Convert.ToInt32(facturas[i]["tipo_declaracion_id"]),
                            Convert.ToDouble(facturas[i]["iva"]), Convert.ToDouble(facturas[i]["total"]), facturas[i]["imagen"].ToString());
                        
                        auxiliar.Add(factura);

                    }
                }
            

                //string id_factur = arreglo[0]["mf"].ToString();

            }
            catch (Exception exc)
            {
                DisplayAlert("Error", exc.ToString(), "aceptar");
            }
            return auxiliar;
            

        }

    }
}