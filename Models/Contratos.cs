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
        [Required(ErrorMessage = "El inquilino es obligatorio")]
        public int? IdInquilino { get; set; }
        [Required(ErrorMessage =" El inmueble es obligatorio")]
        public int IdInmuebles { get; set; }
        [Required(ErrorMessage = "El monto es obligatorio")]
        public decimal Monto { get; set; }
        [Display(Name = "Ingreso")]
        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        public DateTime FechaDesde { get; set; }

        [Display(Name = "Egreso")]
        [Required(ErrorMessage = "La fecha de egreso es obligatoria")]
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