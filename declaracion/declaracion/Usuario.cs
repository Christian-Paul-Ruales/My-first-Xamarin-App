using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net;

namespace declaracion
{
     public class Usuario
    {
        public int id_usuario { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
      
        public string correo { get; set; }
        public int cliente_id { get; set; }
        
        public int vendedor_id { get; set; }


        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", id_usuario,usuario,clave,correo,cliente_id,vendedor_id);
        }

        /**
        public Usuario(int id, string user, string pass, string email, int client, int sellers) {
            this.id_usuario = id;
            this.usuario = user;
            this.clave = pass;
            this.correo = email;
            this.cliente_id = client;
            this.vendedor_id = sellers;

        }*/
       
        


    }
}
