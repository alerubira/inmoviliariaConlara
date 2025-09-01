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
    }
}