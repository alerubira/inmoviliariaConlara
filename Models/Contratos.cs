using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{

    public class Contratos
    {
        [Key]
        [Display(Name = "Codigo Interno")]
        public int IdContrato { get; set; }
        //[Required]
        //[Display(Name = "Dirección")]
        //[Required(ErrorMessage = "La dirección es requerida")]
        [Required(ErrorMessage = "El inquilino es obligatorio")]
        public int? IdInquilino { get; set; }
        [Required(ErrorMessage = " El inmueble es obligatorio")]
        public int IdInmuebles { get; set; }
        [Required(ErrorMessage = "El monto es obligatorio")]
        [Display(Name = "Monto de la cuota")]
        public decimal Monto { get; set; }
        [Display(Name = "Ingreso")]
        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaDesde { get; set; }

        [Display(Name = "Egreso")]
        [Required(ErrorMessage = "La fecha de egreso es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaHasta { get; set; }
        [Display(Name = "Vigente")]
        public bool Vigente { get; set; }
        [Display(Name = "Cantidad de Cuotas")]
        [Required(ErrorMessage = "La cantidad de cuotas es obligatoria")]
        public Int32 CantidadCuotas { get; set; }
        [Display(Name = "Cantidad Cuotas Pagas")]
        [Required(ErrorMessage = "La cantidad de cuotas pagas es obligatoria")]
        public Int32 CuotasPagas { get; set; }
        [Required(ErrorMessage = "El mes de inicio es obligatorio")]
        public Int32 MesInicio { get; set; }

        public decimal? Precio { get; set; }
        [Display(Name = "Inquilino")]
        public String? NombreInquilino { get; set; }
        [Display(Name = "Direccion del Inmueble")]
        public String? DireccionInmueble { get; set; }
        [Display(Name = "Usuario que realizo el Alta")]
        [Required(ErrorMessage = "El usuario que realiza el alta es obligatorio")]
        public int UsuariAlta { get; set; }
        [Display(Name = "Usuario que realizo la Baja")]
        public int? UsuarioBaja { get; set; }
        public bool Existe { get; set; }
        
        [Display(Name = "Usuario que realizo el Alta")]
        public String? mailUsuarioAlta { get; set; }
        [Display(Name = "Usuario que realizo la Baja")]
        public String? mailUsuarioBaja { get; set; }
    }
}