using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{

    public class Multas
    {
        [Display(Name = "Codigo Interno")]
        public int IdMulta { get; set; }
       
        [Required(ErrorMessage = "El Contrato  es obligatorio")]
        public int IdContrato { get; set; }
        [Display(Name = "Fecha de ejecucion multa")]
        [Required(ErrorMessage = "La fecha dela multa es obligatoria")]
        public DateTime FechaMulta { get; set; }

        [Display(Name = "Fecha original de finalizacion")]
        [Required(ErrorMessage = "La fecha  es obligatoria")]
        public DateTime FechaHastaContrato { get; set; }

         [Display(Name = "Nueva Fecha de finalizacion")]
        [Required(ErrorMessage = "La fecha  es obligatoria")]
        public DateTime NuevaFechaHastaContrato { get; set; }

       
        [Required(ErrorMessage = "El Importe de la cuota es obligatorio")]
        [Display(Name = "Monto de la cuota")]
        public decimal ImporteCuota { get; set; }
         [Required(ErrorMessage = "El Importe de la multa es obligatorio")]
        [Display(Name = "Monto de la Multa")]
        public decimal? ImporteMulta { get; set; }
       
        [Display(Name = "Cantidad de Cuotas Adeudadas")]
        [Required(ErrorMessage = "La cantidad de cuotas es obligatoria")]
        public Int32 CuotasAdeudadas { get; set; }
        [Display(Name = "Multa Pagada")]
       
       public bool Pagada { get; set; }

    
        [Display(Name = "Inquilino")]
        public String? NombreInquilino { get; set; }
        [Display(Name = "Direccion del Inmueble")]
        public String? DireccionInmueble { get; set; }
    }
}