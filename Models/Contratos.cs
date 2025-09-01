using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{

    public class Contratos
    {
        [Display(Name = "Codigo Interno")]
        public int IdContrato { get; set; }
        //[Required]
        //[Display(Name = "Dirección")]
        //[Required(ErrorMessage = "La dirección es requerida")]
        public int? IdInquilino { get; set; }
        [Required]
        public int IdInmuebles { get; set; }
        [Required]
        public decimal Monto { get; set; }
        [Display(Name = "Ingreso")]
        public DateTime FechaDesde { get; set; }

        [Display(Name = "Egreso")]
        public DateTime FechaHasta { get; set; }
        [Display(Name = "Vigente")]
        public bool Vigente { get; set; }
        public decimal? Precio { get; set; }
        [Display(Name = "Inquilino")]
        public String? NombreInquilino { get; set; }
        [Display(Name = "Direccion del Inmueble")]
        public String? DireccionInmueble { get; set; }
    }
}