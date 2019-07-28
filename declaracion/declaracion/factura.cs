using System;
using System.Collections.Generic;
using System.Text;

namespace declaracion
{
   public  class factura
    {
        public int id_factura { get; set; }
        public int usuario_id { get; set; }
        public int vendedor_id { get; set; }
        public int cliente_id { get; set; }
        public int tipo_declaracion_id { get; set; }
        public double iva { get; set; }
        public double total { get; set; }
        public string imagen { get; set; }

        public factura(int id_f,int u_id, int v_id,int cl_id, int t_d, double IVA, double tota,string image) {

            this.id_factura = id_f;
            this.usuario_id = u_id;
            this.vendedor_id = v_id;
            this.cliente_id = cl_id;
            this.tipo_declaracion_id = t_d;
            this.iva = IVA;
            this.total = tota;
            this.imagen = image;

        }
    }
}
