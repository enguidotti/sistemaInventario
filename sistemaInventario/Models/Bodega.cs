//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sistemaInventario.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bodega
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bodega()
        {
            this.StockBodega = new HashSet<StockBodega>();
        }
    
        public int id_bodega { get; set; }
        public string nombre { get; set; }
        public string ubicacion { get; set; }
        public Nullable<int> fono { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockBodega> StockBodega { get; set; }
    }
}