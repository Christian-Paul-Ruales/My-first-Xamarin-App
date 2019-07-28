using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json.Linq;

namespace declaracion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login_page : ContentPage
    {
        public login_page()
        {
            InitializeComponent();
            btnStart.Clicked += IniciarSesion;
          //  btnreg.Clicked += Btnreg_Clicked;
            btnregister.Clicked += Btnregister_Clicked;

        }

        private async void Btnregister_Clicked(object sender, EventArgs e)
        {
            // await NavigationPage(new Page1());
            await Navigation.PushAsync(new Page1());
        }

       
        const string URLinsertUser = "http://192.168.1.4/php_declaracion/usuario.php";
        public string BuscarUsuario(string username, string password)
        {
            const string URLinsertUser = "http://192.168.1.4/php_declaracion/usuario.php";

            WebClient webclient = new WebClient();
            Uri uri = new Uri(URLinsertUser);

            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("usernameL", username);
            parameters.Add("passwordL", password);

            //webclient.UploadValuesAsync(uri, parameters);

            string pagesource = Encoding.UTF8.GetString(webclient.UploadValues(uri, parameters));
            //message_login = pagesource;
            return pagesource;

        }
        private async void IniciarSesion(object sender, EventArgs e){

            try {
                Usuario user = new Usuario();
        
                var json = JsonConvert.DeserializeObject(BuscarUsuario(txtUser.Text, txtPass.Text));
                JArray arreglo = new JArray(json);
                
                //await DisplayAlert("Bienvenido", ""+ arreglo[0]["usuario"] + " "+ arreglo[0]["correo"], "OK");
                await Navigation.PushAsync(new lstFacturas(arreglo[0]["usuario"].ToString()));
            }
            catch(Exception e1) {
                await DisplayAlert("Ha ocurrido un error", "Error:"+e1.Message, "Aceptar");
            }
            

          

        }


    }
}