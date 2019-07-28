using System;
using System.Collections.Generic;
using System.Text;

namespace declaracion
{
    public class producto
    {
        public int id_producto { get; set; }
        public int factura_id { get; set; }
        public string detalle { get; set; }
        public double valor_individual { get; set; }
        public double iva { get; set; }
        public int cantidad_total { get; set; }
        public double valor_total { get; set; }

        public producto(int id_product, int id_factura, string detail, double individual_value, double IVA, int total_quantity, double total_value) {
            this.id_producto = id_product;
            this.factura_id = id_factura;
            this.detalle = detail;
            this.valor_individual = individual_value;
            this.iva = IVA;
            this.cantidad_total = total_quantity;
            this.valor_total = total_value;
    
        }
    }
}
