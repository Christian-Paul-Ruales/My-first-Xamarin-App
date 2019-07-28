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
    public partial class declaracion : ContentPage
    {
        public string usuario { get; set; }

        public string detalle { get; set; }
        public double valor { get; set; }
        public double iva { get; set; }
       // public double cantidad { get; set; }
        public string tipo_declaracion { get; set; }

        public declaracion(string user)
        {
            InitializeComponent();
            this.usuario = user;
            //  mostrar(user);
            GenerarDeclaracion(user);
            GuardarDeclaracion.Clicked += GuardarDeclaracion_Clicked;


        }

        private void GuardarDeclaracion_Clicked(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            Uri uri2 = new Uri("http://192.168.1.4/php_declaracion/guardar_declaracion.php");

            NameValueCollection parameters2 = new NameValueCollection();

            parameters2.Add("detalle", detalle);
            parameters2.Add("valor", valor.ToString());
            parameters2.Add("iva", iva.ToString());
            parameters2.Add("tipo_declaracion", tipo_declaracion);
            parameters2.Add("usuario", usuario);


            string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri2, parameters2));

        }

        public void GenerarDeclaracion(string User) {

            WebClient client = new WebClient();

            Uri uri = new Uri("http://192.168.1.4/php_declaracion/declaracion.php");

            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("usuario", User);


            string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri, parameters));

            //DisplayAlert("retorno", pagesource, "aceptar");

            var json = JsonConvert.DeserializeObject(pagesource);
            JArray arreglo = new JArray(json);

            double impuesto_pagar_renta = Convert.ToDouble(arreglo[0][6]) + Convert.ToDouble(arreglo[0][7]);

            string[] misdatos = new string[] { "Gastos Vivienda: "+ arreglo[0][0].ToString(), "Gastos Alimentacion: " + arreglo[0][1].ToString(),
                "Gastos Vestimenta: " + arreglo[0][2].ToString(),"Gastos Educacion: " + arreglo[0][3].ToString(),"Gastos Salud: " + arreglo[0][4].ToString(),
                "Total: " + arreglo[0][5].ToString(), "Impuesto a la Fraccion Basica: " + arreglo[0][6].ToString(),"Impuesto a la Fraccion Excedente: " + arreglo[0][7].ToString(),"Impuesto a la Renta: "+impuesto_pagar_renta};
            string detail = "Impuesto a la Renta";
            string value = impuesto_pagar_renta.ToString();
            double iVaA = 0;
            int tipo_declaracion = 1;
            foreach (JArray array_d in arreglo) {
                if (array_d.Count > 8)
                {
                     detail = "Impuesto Iva";
                    misdatos = new string[] { "Gastos Vivienda: "+ arreglo[0][0].ToString(), "Gastos Alimentacion: " + arreglo[0][1].ToString(),
                "Gastos Vestimenta: " + arreglo[0][2].ToString(),"Gastos Educacion: " + arreglo[0][3].ToString(),"Gastos Salud: " + arreglo[0][4].ToString(),
                "Total: " + arreglo[0][5].ToString(), "Impuesto a la Fraccion Basica: " + arreglo[0][6].ToString(),"Impuesto a la Fraccion Excedente: " + arreglo[0][7].ToString(),"Impuesto a la Renta: "+impuesto_pagar_renta,
                    "Iva Compras: " +arreglo[0][8].ToString(),  "Iva Ventas: " +arreglo[0][9].ToString(),  "Declaracion de Iva: " +arreglo[0][10].ToString()};

                     value = arreglo[0][10].ToString();
                    iVaA = Convert.ToDouble(arreglo[0][8]);
                     tipo_declaracion = 2;

                }

            }
            this.detalle = detail;
            this.valor = Convert.ToDouble(value);
            this.iva = iVaA;
            this.tipo_declaracion = tipo_declaracion.ToString();

            lstDetalle.ItemsSource = misdatos;



        }






    }
}