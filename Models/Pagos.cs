using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
  //[Table("Inmuebles")]
  public class Pagos
  {
    [Display(Name = "Numero de Pago")]
    public int IdPagos { get; set; }
    //[Required]
    [Display(Name = "Codigo de Contrato")]
    [Required(ErrorMessage = "El contrato es obligatorio")]
    public int IdContratos { get; set; }
    [Required(ErrorMessage = "La fecha de pago es obligatoria")]
    public DateTime FechaPago { get; set; }
    [Required(ErrorMessage = "El importe es obligatorio")]
    [Display(Name = "Importe Sugerido")]
      public decimal Importe { get; set; }
  
   
   
    [Display(Name = "Direccion Inmueble")]
    public String? DireccionInmueble { get; set; }
   
		/*[ForeignKey(nameof(idPropietario))]
   [BindNever]
    public Propietario? Duenio { get; set; }
    public string? Portada { get; set; }
    [NotMapped]//Para EF
    public IFormFile? PortadaFile { get; set; }
    [ForeignKey(nameof(Imagen.InmuebleId))]
    public IList<Imagen> Imagenes { get; set; } = new List<Imagen>();
    [NotMapped]
    public bool Habilitado { get; set; } = true;*/
  }
}