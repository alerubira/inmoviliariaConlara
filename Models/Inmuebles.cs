using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
  //[Table("Inmuebles")]
  public class Inmuebles
  {
    [Display(Name = "Codigo Interno")]
    public int IdInmueble { get; set; }
    //[Required]
    [Display(Name = "Dirección")]
    [Required(ErrorMessage = "La dirección es requerida")]
    public string? Direccion { get; set; }
    [Required]
    public int Ambientes { get; set; }
    [Required]
    public int Superficie { get; set; }
    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }
    [Display(Name = "Dueño")]
    public int IdPropietario { get; set; }
    [Display(Name = "Tipo de Inmueble")]
    public int IdTipoInmueble { get; set; }
    public decimal Precio { get; set; }
    public bool Habilitado { get; set; }
    [Display(Name = "Dueño")]
    public Propietario? Duenio { get; set; }
    [Display(Name = "Tipo de Inmueble")]
    public String? TipoInmueble { get; set; }
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