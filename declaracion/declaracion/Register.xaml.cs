using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MySql.Data.MySqlClient;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net;
namespace declaracion
{
    
    public partial class Page1 : ContentPage
    {

       // public event EventHandler <> OnCreateContact;
        public Page1()
        {
            InitializeComponent();
            btnRegister.Clicked += Registrarse;
            
            
        }
        const string URLinsertUser = "http://192.168.1.4/php_declaracion/usuario.php";
        public async void Registrarse(object sender, EventArgs e) {

            if (string.IsNullOrEmpty(txtRUser.Text)) {
                 await DisplayAlert("Error","Falta ingresar Datos","Aceptar");

               
                
                return;
                    }

           

            WebClient client = new WebClient();
            Uri uri = new Uri(URLinsertUser);

            NameValueCollection parameters = new NameValueCollection();
            
            parameters.Add("usuario", txtRUser.Text);
            parameters.Add("clave", txtRPass.Text);
            parameters.Add("email", txtREmail.Text);
            parameters.Add("sueldo", txtRSalario.Text);
            parameters.Add("cedula", txtRCedula.Text);
            int selectedIndex = pcDeclaracion.SelectedIndex;
            txtRCedula.Text = (string)pcDeclaracion.ItemsSource[selectedIndex];
           
            parameters.Add("declaracion", (string)pcDeclaracion.ItemsSource[selectedIndex]);
            // client.UploadValuesCompleted += Client_UploadValuesCompleted;
            //client.UploadValuesAsync(uri,parameters);
            string pagesource = Encoding.UTF8.GetString(client.UploadValues(uri, parameters));
            await DisplayAlert("Aviso", ""+pagesource,"Aceptar");
            txtRUser.Text = string.Empty;
            txtRPass.Text = string.Empty;
            txtREmail.Text = string.Empty;
        }

       /** private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            // throw new NotImplementedException();
            if (on) { }
        }*/
    }
}